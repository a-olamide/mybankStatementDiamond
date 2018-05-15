<%@ Application Language="VB" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>


<script runat="server">
    Public lastrun As DateTime
    Public Shared dt1 As DataTable
    'Private Shared LastRun As DateTime
    Public ResendStatement As System.Threading.Thread = Nothing
    'Public PrepStatementThread As System.Threading.Thread = Nothing
    'Public EmailStatementThread As System.Threading.Thread = Nothing
    Public oWS As New net.mybankstatement.WebService()

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' LastRun = DateTime.Now
        ResendStatement = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf runUnsent))
        ResendStatement.IsBackground = True
        ResendStatement.Start()
    End Sub
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs

    End Sub
    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started

        'If Not Context.Session Is Nothing And Session.IsNewSession And Request.Headers.HasKeys() Then
        '    HttpContext.Current.Response.Redirect("Login.aspx")
        'End If
    End Sub
    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
        HttpContext.Current.Response.Redirect("Login.aspx")
    End Sub
    Private Sub runUnsent()
        While (True)
            System.Threading.Thread.Sleep(1000)
            Try

                Try
                    'BLL.receiveInbox()
                Catch ex As Exception
                End Try
                Try
                    'This is for getting requested statement

                    ' BLL.GetRequestedActivity()
                Catch ' ex As Exception
                End Try
                Try
                    'This is for getting requested statement
                    ' BLL.GetInitiatedActivity()
                Catch ' ex As Exception
                End Try
                Try
                    'This is for corporate test
                    ' BLL.GetApprovedCorporateActivity()
                Catch ' ex As Exception
                End Try
                Try
                    'This is for corporate test
                    '  BLL.GetPreRegActivity()
                Catch ' ex As Exception
                End Try
                Try
                    'UpdateDestination()
                Catch ex As Exception
                End Try
                Try
                    ' UpdateOtherFormat()
                Catch ex As Exception
                End Try
                Try
                    'sendTicketMailAlert()
                Catch ex As Exception
                End Try
                Try
                    'sendSMSAlert()
                Catch ex As Exception
                End Try
                Try
                    ' sendTicketMailAlertCorporate()
                Catch ex As Exception
                End Try
                Try
                    ' sendApprovalMailAlert()
                Catch ex As Exception
                End Try
                Try
                    ' runUnsentFiles()
                Catch ex As Exception
                End Try
                Try
                    ' checkViewedPDF()
                Catch ex As Exception
                End Try

                Dim startM As Int16 = Convert.ToInt16(WebConfigurationManager.AppSettings("startMinute").ToString())
                Dim endM As Int16 = Convert.ToInt16(WebConfigurationManager.AppSettings("EndMinute").ToString())
                If DateTime.Now.Minute >= startM And DateTime.Now.Minute <= endM Then

                    Try
                        '  UpdateDestination()
                    Catch ex As Exception
                    End Try
                    Try
                        '  UpdateOtherFormat()
                    Catch ex As Exception
                    End Try

                End If
                If DateTime.Now.Hour = 5 Then
                    'Try
                    '   ' UpdateRole()
                    'Catch
                    'End Try
                    Try
                        '  sendException()
                    Catch ex As Exception
                    End Try
                End If
            Catch ex As Exception
            End Try
        End While
    End Sub
    Private Sub runUnsentFiles()
        Dim dt As System.Data.DataTable = BLL._selectUnsentData()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Try
                    BLL.ResendFile(dt.Rows(i).Item("TICKETNO"), dt.Rows(i).Item("DESTINATIONID"), dt.Rows(i).Item("SOURCEID"),
                                    dt.Rows(i).Item("type"), dt.Rows(i).Item("status"))
                Catch
                End Try
            Next
        End If
    End Sub
    Private Sub sendException()
        Try
            '   oWS._insertException(BLL._selectExceptionLog)
            '   BLL._deleteExceptionLog()
        Catch ex As Exception
            ' BLL._insertExceptionLog()
        End Try
    End Sub
    Private Sub sendTicketMailAlert()
        'Dim FiveMinutesAgo As DateTime = DateTime.Now.AddMinutes(-5)
        'If LastRun < FiveMinutesAgo Then       
        Dim dt As System.Data.DataTable = BLL._selectUnsentTicketMailAlert()
        If dt.Rows.Count > 0 Then
            Try
                For i As Integer = 0 To dt.Rows.Count - 1
                    'Dim path As String = "~\statement\" & dt.Rows(i).Item(1) & ".pdf"
                    'Dim path As String = HttpContext.Current.Server.MapPath("~\statement\" & dt.Rows(i).Item(1) & ".pdf")
                    Try
                        Dim path As String = Nothing
                        Dim both As String = "0"
                        'This means no additional attachment
                        'both = 2 means add requested statement
                        'both = 1 means add bank generated statement
                        Dim format As String = Nothing
                        Try
                            format = BLL._SelectMailOption(dt.Rows(i).Item(1))
                        Catch ex As Exception
                            format = "0_0_0"
                        End Try

                        If dt.Rows(i).Item(4).ToString = "0" Then

                            path = System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" & dt.Rows(i).Item(1) & ".pdf")
                        ElseIf BLL._SelectTicketStatusFromTicketID(dt.Rows(i).Item(1)) = "-1" Then
                            path = Nothing
                        ElseIf dt.Rows(i).Item(5).ToString = "info" Then
                            path = Nothing
                            both = "0"
                        ElseIf dt.Rows(i).Item(5).ToString = "request" Then
                            path = "D:\mybankStatementRepository\statementRequestTicket\" & dt.Rows(i).Item(1) & ".pdf"
                            both = "0"
                        ElseIf format.Split("_")(1) = "1" Then
                            path = System.Web.Hosting.HostingEnvironment.MapPath("~\ticket\" & dt.Rows(i).Item(1) & ".pdf")
                            both = format.Split("_")(0)
                        Else
                            path = System.Web.Hosting.HostingEnvironment.MapPath("~\receipt\" & dt.Rows(i).Item(1) & ".pdf")
                            both = format.Split("_")(0)
                        End If
                        If (SenderMail.Send(dt.Rows(i).Item(2), dt.Rows(i).Item(3), path, dt.Rows(i).Item(0), dt.Rows(i).Item(1).ToString, both)) Then
                            BLL._deleteSentTicketMailAlert(dt.Rows(i).Item(1))
                        End If
                    Catch
                    End Try
                Next
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub sendSMSAlert()
        'Dim FiveMinutesAgo As DateTime = DateTime.Now.AddMinutes(-5)
        'If LastRun < FiveMinutesAgo Then       
        Dim dt As System.Data.DataTable = BLL._selectUnsentSMSAlert()
        If dt.Rows.Count > 0 Then
            Try
                For i As Integer = 0 To dt.Rows.Count - 1

                    Try
                        'If Utility.SendSMS(dt.Rows(i).Item(0), dt.Rows(i).Item(3), dt.Rows(i).Item(1)) = "00" Then
                        '    BLL._deleteSentSMSAlert(dt.Rows(i).Item(1))
                        'End If
                        If Utility.InsertSMS(dt.Rows(i).Item(0), dt.Rows(i).Item(3), dt.Rows(i).Item(1), dt.Rows(i).Item(5)) = True Then
                            BLL._deleteSentSMSAlert(dt.Rows(i).Item(1))
                        End If
                    Catch
                    End Try
                Next
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub sendTicketMailAlertCorporate()
        'Dim FiveMinutesAgo As DateTime = DateTime.Now.AddMinutes(-5)
        'If LastRun < FiveMinutesAgo Then      
        Dim dt As System.Data.DataTable = BLL._selectUnsentTicketMailAlertCorporate()
        If dt.Rows.Count > 0 Then
            Try
                For i As Integer = 0 To dt.Rows.Count - 1
                    Try
                        Dim both As String = Nothing
                        Dim path As String = Nothing

                        If (SenderMail.Send(dt.Rows(i).Item("subject"), dt.Rows(i).Item("body"), path, dt.Rows(i).Item("receiver"), dt.Rows(i).Item("ticketID").ToString, both)) Then
                            BLL._deleteSentTicketMailAlertCorporate(dt.Rows(i).Item("id"))
                        End If
                    Catch

                    End Try
                Next

            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Sub sendApprovalMailAlert()
        'Dim FiveMinutesAgo As DateTime = DateTime.Now.AddMinutes(-5)
        'If LastRun < FiveMinutesAgo Then        
        Dim dt As System.Data.DataTable = BLL._selectUnsentApprovalMailAlert()
        If dt.Rows.Count > 0 Then
            Try
                For i As Integer = 0 To dt.Rows.Count - 1
                    Try

                        If (SenderMail.Send(dt.Rows(i).Item(2), dt.Rows(i).Item(3), Nothing, dt.Rows(i).Item(0), Nothing)) Then
                            BLL._deleteSentApprovalMailAlert(dt.Rows(i).Item(1))
                        End If
                    Catch
                    End Try
                Next
            Catch ex As Exception
            End Try
        End If
    End Sub


    Private Sub UpdateDestination()
        Try
            ' Dim senderid As Integer = 1

            'Dim dr As SqlDataReader = BLL._selectCompany()
            'While dr.Read()
            '    If Not IsDBNull(dr.Item("Domain")) Then
            '        senderid = dr.Item("SenderID")
            '    End If
            'End While
            'Dim proxyVal As New WebProxy("http://10.0.0.120:80", True)
            'oWS.Proxy = proxyVal
            Dim dt2 As System.Data.DataTable = oWS.GetNewDestination("8")
            If dt2.Rows.Count > 0 Then
                For k As Integer = 0 To dt2.Rows.Count - 1
                    Try

                        BLL._insertDestination(dt2.Rows(k).Item(0), dt2.Rows(k).Item(1), dt2.Rows(k).Item(2), dt2.Rows(k).Item(3), dt2.Rows(k).Item(4),
                                               dt2.Rows(k).Item(5), dt2.Rows(k).Item(6), dt2.Rows(k).Item(7), dt2.Rows(k).Item(8), dt2.Rows(k).Item(9))

                        If dt2.Rows(k).Item(10) = "1" Then
                            BLL._InsertDestinationFormat(dt2.Rows(k).Item(0), "1", "1")
                        End If
                    Catch ex As Exception

                    End Try
                Next
            End If
            'BLL._insertDestination(dt.Rows(i).Item(0), dt.Rows(i).Item(1), dt.Rows(i).Item(2), dt.Rows(i).Item(3), dt.Rows(i).Item(4))
            BLL._updateLastSystemUpdate(Now)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub UpdateOtherFormat()
        Try
            Dim senderid As Integer = 1

            'Dim dr As SqlDataReader = BLL._selectCompany()
            'While dr.Read()
            '    If Not IsDBNull(dr.Item("Domain")) Then
            '        senderid = dr.Item("SenderID")
            '    End If
            'End While
            Try

                Dim dt2 As System.Data.DataTable = oWS.GetOtherNewFormat(senderid)
                If dt2.Rows.Count > 0 Then
                    For k As Integer = 0 To dt2.Rows.Count - 1


                        BLL._InsertDestinationFormat(dt2.Rows(k).Item(0), dt2.Rows(k).Item(1), dt2.Rows(k).Item(2))

                    Next
                    oWS._UpdateNewFormat(senderid)
                End If
            Catch ex As Exception

            End Try
            'BLL._insertDestination(dt.Rows(i).Item(0), dt.Rows(i).Item(1), dt.Rows(i).Item(2), dt.Rows(i).Item(3), dt.Rows(i).Item(4))
            BLL._updateLastSystemUpdate(Now)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub UpdateRole()
        Try
            Dim senderid As Integer = 1

            'Dim dr As SqlDataReader = BLL._selectCompany()
            'While dr.Read()
            '    If Not IsDBNull(dr.Item("Domain")) Then
            '        senderid = dr.Item("SenderID")
            '    End If
            'End While

            Dim dt2 As System.Data.DataTable = oWS.GetNewRole(senderid)
            If dt2.Rows.Count > 0 Then
                For k As Integer = 0 To dt2.Rows.Count - 1
                    Try
                        BLL._insertStatementRole(dt2.Rows(k).Item(0), dt2.Rows(k).Item(1))
                    Catch ex As Exception

                    End Try
                Next
            End If
            BLL._updateLastSystemUpdate(Now)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub checkViewedPDF()
        Try
            Dim ticketNos As String = ""
            Dim dt21 As DataTable = BLL._selectUnviewedPDf()
            If dt21.Rows.Count = 0 Then
                Exit Sub
            Else
                For i As Integer = 0 To dt21.Rows.Count - 1
                    If i = dt21.Rows.Count - 1 Then ticketNos += dt21.Rows(i).Item(0) & "-8" Else ticketNos += dt21.Rows(i).Item(0) & "-8,"
                Next
                Dim newValue As String = prepareTicketNosInQuote(ticketNos)
                If newValue.Length > 1 Then
                    Dim dt As DataTable = oWS._selectViewedPDF(newValue, "mybankStatement88*")
                    If dt.Rows.Count = 0 Then
                        Exit Sub
                    Else
                        For i As Integer = 0 To dt.Rows.Count - 1

                            BLL._updateViewPDF(dt.Rows(i).Item(0).ToString().Split("-")(0), Convert.ToDateTime(dt.Rows(i).Item(1)))
                        Next
                    End If
                End If


            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function prepareTicketNosInQuote(commaSepTicketNos) As String
        Try
            Dim newValue As String = ""
            Dim strArr() As String = Trim(commaSepTicketNos.Replace(" ", "")).Split(",")
            For i As Int16 = 0 To strArr.Length - 1
                newValue &= "'" & strArr(i) & "',"
            Next
            newValue = newValue.Substring(0, newValue.Length - 1)
            Return newValue
        Catch ex As Exception
            Return ""
        End Try
    End Function
</script>