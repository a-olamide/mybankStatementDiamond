Imports System.Globalization
Imports System.Data
Imports System.Web.Services

Partial Class RequestStatement
    Inherits System.Web.UI.Page

    Dim selectedBank As String = ""
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim item_ As New System.Web.UI.WebControls.ListItem("Choose...", "0")
            ddlBankName.DataTextField = "Name"
            ddlBankName.DataValueField = "ID"
            ddlBankName.DataSource = BLL._selectRequestDestination()
            'This is perculiar to Diamond since your country determines what destination you can send to
            'ddlCountry.DataSource = BLL._selectUBAUserCountry(HttpContext.Current.Session("Country").ToString)
            ddlBankName.DataBind()
            If ddlBankName.Items.FindByValue("0") Is Nothing Then ddlBankName.Items.Add(item_)
            ddlBankName.SelectedValue = "0"
        End If
        '  txtAccountSearch.Text = Nothing
        ' If IsNothing(Session("type")) Then

        '  SelectRequest("All")
        'Else

        '    SelectRequest("Filter-All")
        'End If
    End Sub

    'Private Sub SelectRequest(ByVal type_ As String)
    '    '  HttpContext.Current.Session("CompanyID") = "14"
    '    Dim dt As DataTable = Nothing
    '    If type_ = "All" Then
    '        '  dt = GetTableRequest()
    '        dt = Option2BLL._selectRequest(HttpContext.Current.Session("CompanyID"))
    '        If dt.Rows.Count > 0 Then
    '            If dt.Rows(0).Item(11) < 3 Then lblRequestTop.Text = dt.Rows(0).Item(11) Else lblRequestTop.Text = "3"
    '        End If
    '    ElseIf type_ = "Filter" Then
    '        ' dt = FilterRequestTable(txtFlter2.Text)
    '        dt = Option2BLL._selectRequestByFilter(HttpContext.Current.Session("CompanyID"), txtFlter2.Text)
    '        If dt.Rows.Count > 0 Then
    '            '   lblTotal.Text = dt.Rows(0).Item(11)
    '        End If
    '    ElseIf type_ = "Filter-All" Then
    '        '  dt = GetTableRequest()

    '        dt = Option2BLL._selectRequestByFilter(HttpContext.Current.Session("CompanyID"), "Filter-All")
    '        If dt.Rows.Count > 0 Then
    '            ' lblTotal.Text = dt.Rows(0).Item(11)
    '        End If
    '    End If
    '    tbRequest.InnerHtml = ""
    '    thRequest.InnerHtml = ""
    '    If dt.Rows.Count = 0 Then
    '        thRequest.Visible = False

    '        tfRequest.Visible = False
    '        tbRequest.InnerHtml = "<tr><td colspan=""9"">No request sent yet.</td></tr>"
    '    Else
    '        thRequest.Visible = True
    '        thRequest.InnerHtml += "<tr><th>" & "Request ID." & "</th><th><i class=""ace-icon fa fa-user""></i>" & "Account Name" & "</th><th>" & "Account No." & "</th><th>" & "Bank" &
    '                           "</th><th>" & "Period" & "</th><th>" & "User" & "</th><th>" & "Timestamp" &
    '                         "</th><th>" & "Status" & "</th><th>" & "Action" & "</th></tr>"
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            tbRequest.InnerHtml += "<tr><td>" & dt.Rows(i).Item(12) & "</td><td><i class=""ace-icon fa fa-user""></i>" & dt.Rows(i).Item(0) & "</td><td>" & dt.Rows(i).Item(9) & "</td><td>" & dt.Rows(i).Item(2) &
    '                           "</td><td>" & dt.Rows(i).Item(3) & "</td><td>" & dt.Rows(i).Item(4) & "</td><td>" & dt.Rows(i).Item(5) &
    '                         "</td><td>" & dt.Rows(i).Item(6) & "</td><td><a style=""cursor:pointer"" onclick=""jPending('" & dt.Rows(i).Item(12) & "%" & dt.Rows(i).Item(6) & "')"" >"
    '            tbRequest.InnerHtml += dt.Rows(i).Item(13) & "</a></td></tr>"

    '        Next

    '        'If CDbl(dt.Rows(0).Item(11)) > 3 Then lblTop.Text = "3" Else lblTop.Text = dt.Rows(0).Item(11)
    '        lblRequestTotal.Text = dt.Rows(0).Item(11)
    '    End If
    'End Sub


    Private Function GetIP() As String
        Dim externalip As String = ""
        externalip = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If externalip = "" Or externalip Is Nothing Then
            externalip = (HttpContext.Current.Request.ServerVariables("REMOTE_ADDR"))
        End If
        Return externalip
    End Function

    Protected Sub lnkIcnRemove_Click(sender As Object, e As EventArgs) Handles lnkIcnRemove.Click
        lblNotif.Visible = False
        lnkIcnRemove.Visible = False
    End Sub

    'Protected Sub lnkConfirm_Click(sender As Object, e As EventArgs) Handles lnkConfirm.Click
    '    Try
    '        Dim oWS As New net.mybankstatement.WebService()
    '        If Option2BLL.updateNewRequestStatus(txtRequestID.Text, "Confirming") = True Then
    '            oWS.updateRequestStatus(txtRequestID.Text, "Confirm", txtTicket.Text, txtPsw.Text, Nothing, "PreReg123*8!")
    '            lblConfirm.Text = "Request confirmed and statement generation request has been sent to the issuing bank. You can view the statement in your Inbox within 5 minites."
    '            spnConform.Visible = True
    '            HttpContext.Current.Response.Redirect("RequestStatement.aspx")
    '        Else
    '            lblConfirm.Text = "Cannot update database"
    '            spnConform.Visible = True
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Protected Sub lnkConfirmRomove_Click(sender As Object, e As EventArgs) Handles lnkConfirmRomove.Click
        lblConfirm.Visible = False
        lnkConfirmRomove.Visible = False
    End Sub

    Private Sub clearInput()
        txtAccountSearch.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""
        ddlBankName.SelectedIndex = 0
    End Sub



    'Protected Sub lnkSearch2_Click(sender As Object, e As EventArgs) Handles lnkSearch2.Click
    '    If txtFlter2.Text = "" Then
    '        'SelectActivities("All")
    '        Session("type") = "Filter-All"
    '        SelectRequest("Filter-All")
    '    ElseIf IsNumeric(txtFlter2.Text) Then
    '        Session("type") = "Filter"

    '        SelectRequest("Filter")
    '    Else
    '        Utility.ShowSweet(Me, "Wrong input. Input should be numeric")
    '    End If
    'End Sub

    <WebMethod()> _
    Public Shared Function RequestStatement(ByVal Nuban As String, BankID As String, StartDate As String, _
                                            EndDate As String, Role As String, ApplicantNames As String, Country As String, tel As String) As String
        Dim res As String = Nothing
        Try

            Dim oWS As New net.mybankstatement.WebService()
            Dim ApplicantNameArr() As String = ApplicantNames.Split("|")
            If HttpContext.Current.Session("USERID") Is Nothing Then
                HttpContext.Current.Response.Redirect("Login.aspx")
                Return "-1" '& " <a class=""panel-close close"" data-hide=""alert"">×</a> <i class=""fa fa-coffee""></i> System time-out. Please refresh."
            Else
                Dim accountName As String = "Olamide Hardcode"

                'Then call the statementRequest API. This should call our service on the hub
                '   Dim val As Boolean = Option2BLL.insertStatementRequest(res, Nuban, BankID, HttpContext.Current.Session("CompanyID"), StartDate, EndDate, Role, HttpContext.Current.Session("USERID"), accountName, ApplicantNames)
                Dim val As String = oWS.insertStatementRequest(Nuban, BankID, Nothing, HttpContext.Current.Session("SenderID"), StartDate, EndDate, Role, HttpContext.Current.Session("USERID"), Nothing, Country, tel, ApplicantNameArr, "PreReg123*8!")
                If val = "-1" Then
                    'Send email to users
                    BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), HttpContext.Current.Session("SenderID"), HttpContext.Current.Session("Role"), Nothing, "Request Failed", Now)

                    Return "0" ' & " <a class=""panel-close close"" data-hide=""alert"">×</a> <i class=""fa fa-coffee""></i> Request failed due to service failure"

                    '   Return FilterActivity("")
                    ' Return "<b>Request sent succesfully</b>"
                Else
                    res = Option2BLL.insertStatementRequestNibss(val, Nuban, BankID, HttpContext.Current.Session("SenderID"), StartDate, _
                                                                 EndDate, Role, HttpContext.Current.Session("USERID"), Nothing, _
                                                                 HttpContext.Current.Session("Company"), Country, tel, ApplicantNameArr)
                    Dim str As String = FilterActivity("")
                    BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), HttpContext.Current.Session("Company"), HttpContext.Current.Session("Role"), Nothing, "Request Sent", Now)


                    Return str '& "~/<a class=""panel-close close"" data-hide=""alert"">×</a> <i class=""fa fa-coffee""></i>Statement Request succesful"
                    '  BLL.updateInitiatedRequestStatus(res, "Fail")

                    ' Return "<b>Request was not delivered</b>"
                End If
            End If

            'First verify if the account is valid. If not return the account name



        Catch ex1 As System.Net.WebException
            Return "1" ' & " <a class=""panel-close close"" data-hide=""alert"">×</a> <i class=""fa fa-coffee""></i> Service unreachable. Check your internet connection or contact support"

        Catch ex As Exception
            Return "2" '& " <a class=""panel-close close"" data-hide=""alert"">×</a> <i class=""fa fa-coffee""></i> Error encountered during statement request."
        End Try
    End Function

    <WebMethod()> _
    Public Shared Function Confirm(ByVal requestID As String, ByVal ticketNo As String, ByVal psw As String) As String
        Try
            Dim oWS As New net.mybankstatement.WebService()
            Dim res As Boolean = oWS._validateTicketPassword(requestID, "Confirm", ticketNo, psw, Nothing, "PreReg123*8!")
            If res = True Then

                If oWS._confirmRequestStatus(requestID, "Confirm", ticketNo, psw, Nothing, "PreReg123*8!") = True Then
                    If Option2BLL.updateNewRequestStatus(requestID, "Confirming") = True Then

                        Dim str As String = FilterActivity("")
                        BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), HttpContext.Current.Session("SenderID"), HttpContext.Current.Session("Role"), ticketNo, "Request Confirmation Sent", Now)

                        Return str & "~/<a class=""panel-close close"" data-hide=""alert"">×</a> <i class=""fa fa-coffee""></i>Request confirmed and statement generation request has been sent to the issuing bank. You can view the statement in your Inbox within 5 minites."
                        '  lblConfirm.Text = "Request confirmed and statement generation request has been sent to the issuing bank. You can view the statement in your Inbox within 5 minites."
                        ' spnConform.Visible = True
                        ' HttpContext.Current.Response.Redirect("RequestStatement.aspx")
                    Else
                        BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), HttpContext.Current.Session("SenderID"), HttpContext.Current.Session("Role"), ticketNo, "Request Confirmation Failed", Now)

                        Return "0"
                        '  lblConfirm.Text = "Cannot update database"
                        ' spnConform.Visible = True
                    End If
                Else
                    Return "notupdated"
                End If
            ElseIf res = "0" Then
                Return "invalid"
            Else
                Return "validateError"
            End If

        Catch ex As Exception
            Return "1"
        End Try
    End Function

    <WebMethod()> _
    Public Shared Function FilterActivity(ByVal filter As String) As String
        Dim dt As DataTable = Nothing
        Dim result As String = ""
        Dim row As String = ""
        Dim total As String = ""

        dt = BLL._selectRequestByFilter(filter)

        If dt.Rows.Count = 0 Then
            result = "<tr><td colspan=""10"">No request sent yet.</td></tr>"
            Return "0~/" & "Total~/" & result
        Else
            row = dt.Rows.Count
            total = dt.Rows(0).Item(11)
            result = row & "~/" & total & "~/" & "<tr><th></th><th>" & "Request ID." & "</th><th><i class=""ace-icon fa fa-user""></i>" & "Account Name" & "</th><th>" & "Account No." & "</th><th>" & "Bank" & _
                              "</th><th>" & "Period" & "</th><th>" & "User" & "</th><th>" & "Timestamp" & _
                            "</th><th>" & "Status" & "</th><th>" & "Action" & "</th></tr>"
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item(6).ToString.ToLower = "error" Or dt.Rows(i).Item(6).ToString.ToLower = "fail" Then
                    result += "<tr><td><a style=""cursor:pointer"" onclick=""jDelete('" & dt.Rows(i).Item(12) & "%" & dt.Rows(i).Item(6) & "')"" >"
                    result += "<i class=""icon-trash red"" title=""Delete Request""></i></a></td><td>" & dt.Rows(i).Item(12) & "</td><td><i class=""ace-icon fa fa-user""></i>" & dt.Rows(i).Item(0) & "</td><td>" & dt.Rows(i).Item(9) & "</td><td>" & dt.Rows(i).Item(2) & _
                                   "</td><td>" & dt.Rows(i).Item(3) & "</td><td>" & dt.Rows(i).Item(4).ToString.Split("@")(0) & "</td><td>" & dt.Rows(i).Item(5) & _
                                 "</td><td>" & dt.Rows(i).Item(6) & "</td><td><a style=""cursor:pointer"" onclick=""jPending('" & dt.Rows(i).Item(12) & "%" & dt.Rows(i).Item(6) & "')"" >"
                    result += dt.Rows(i).Item(13) & "</a></td></tr>"
                Else
                    result += "<tr><td>&nbsp;</td><td>" & dt.Rows(i).Item(12) & "</td><td><i class=""ace-icon fa fa-user""></i>" & dt.Rows(i).Item(0) & "</td><td>" & dt.Rows(i).Item(9) & "</td><td>" & dt.Rows(i).Item(2) & _
                                   "</td><td>" & dt.Rows(i).Item(3) & "</td><td>" & dt.Rows(i).Item(4).ToString.Split("@")(0) & "</td><td>" & dt.Rows(i).Item(5) & _
                                 "</td><td>" & dt.Rows(i).Item(6) & "</td><td><a style=""cursor:pointer"" onclick=""jPending('" & dt.Rows(i).Item(12) & "%" & dt.Rows(i).Item(6) & "')"" >"
                    result += dt.Rows(i).Item(13) & "</a></td></tr>"
                End If
            Next
            Return result
        End If
    End Function
    <WebMethod()> _
    Public Shared Function JDelete(ByVal id As String) As String
        Return BLL._deleteRequest(id)

    End Function

    <WebMethod()> _
    Public Shared Function BillCustomer(ByVal id As String) As String
        Try
            Dim oWS As New net.mybankstatement.WebService()
          
            If oWS.updateRequestStatus(id, "BillReady", Nothing, Nothing, Nothing, "PreReg123*8!") = True Then
                If Option2BLL.updateNewRequestStatus(id, "Charging") = True Then

                    Dim str As String = FilterActivity("")
                    BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), HttpContext.Current.Session("SenderID"), HttpContext.Current.Session("Role"), Nothing, "Billing request sent", Now)

                    Return str & "~/<a class=""panel-close close"" data-hide=""alert"">×</a> <i class=""fa fa-coffee""></i>Billing request has been sent"
                    '  lblConfirm.Text = "Request confirmed and statement generation request has been sent to the issuing bank. You can view the statement in your Inbox within 5 minites."
                    ' spnConform.Visible = True
                    ' HttpContext.Current.Response.Redirect("RequestStatement.aspx")
                Else
                    BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), HttpContext.Current.Session("SenderID"), HttpContext.Current.Session("Role"), Nothing, "Billing Request Failed", Now)

                    Return "0"
                    '  lblConfirm.Text = "Cannot update database"
                    ' spnConform.Visible = True
                End If
            Else
                Return "notupdated"
            End If
           

        Catch ex As Exception
            Return "1"
        End Try

    End Function

End Class
