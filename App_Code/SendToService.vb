Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Net

Public Class SendToService
    Private oFileInfo As FileInfo
    Private TicketNo As String
    Private type As String = "bank"

    Public Sub New(FileName As FileInfo, PTicketNo As String)
        oFileInfo = FileName
        TicketNo = PTicketNo

    End Sub

    Public Sub New(FileName As FileInfo, PTicketNo As String, Ptype As String)
        oFileInfo = FileName
        TicketNo = PTicketNo
        type = Ptype
    End Sub

    Public Function SendPDF() As String
        Dim oFile As File
        'Dim oFileStream As FileStream
        Dim oWS As New net.mybankstatement.WebService()

        Dim strReturn As String = ""
        'Value returned from the webservice
        Dim outcome As String = ""
        Dim bolResult As Boolean = False
        Dim details As String() = Nothing
        Dim tenor As String = Nothing
        Dim IP As String = Nothing
        Dim oFileByte As Byte()
        Dim subject As String = ""
        Dim body As String = ""
        'Result returned from the webservice 
        Try
            'Make sure that the file exists before trying to upload
            If File.Exists(oFileInfo.FullName) = False Then
                Return "File doesnt exist"

            End If

            Using oFileStream As FileStream = File.Open(oFileInfo.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read)

                'declare the byte array of the file
                oFileByte = New Byte(oFileStream.Length - 1) {}

                'break the file into bytes and place into the byte object
                oFileStream.Read(oFileByte, 0, oFileStream.Length)
            End Using
            'upload the file to the webservice
            details = BLL._selectMetaData(TicketNo)
            'Dim proxyVal As New WebProxy("http://10.0.0.120:80", True)
            'oWS.Proxy = proxyVal
            Dim applicants As String = "N/A"
            Try
                applicants = BLL._selectApplicantsString(TicketNo)
            Catch ex As Exception

            End Try
            bolResult = oWS.SendStatementAndSignAndApplicant(details(0), details(1), IP, details(2), details(3), Now.ToString, details(4), details(5) _
                       , details(6), tenor, details(9), details(10), details(11), details(12), details(13), details(14), details(15) _
                      , details(17), details(21), "mybankStatement88*", oFileInfo.Name, oFileByte, strReturn, details(20), applicants)

            'display the results
            If bolResult = True Then
                If details(18).ToLower = "n/a" Then
                    If type = "request" Then
                        BLL._insertAuditLogs(details(1), details(3), "Request", TicketNo, "Email not valid", Now)
                    Else
                        BLL._insertAuditLogs(details(1), details(3), "Request", TicketNo, "Email not valid", Now)
                        BLL._insertSMSAlert(TicketNo, details(5), details(3), details(13), details(15), details(14), details(23), "E-Statement", "Your eStatement has been sent to " + details(3) + " TicketNo:" + TicketNo + " Psw:" + details(20) + " Fee:" + CDbl(details(12)).ToString("#,##0") + " " + details(22), "0")
                    End If
                Else
                    subject = "Bank statement Ticket"
                    If type = "request" Then
                        BLL._insertAuditLogs(details(1), details(3), "Request", TicketNo, "Statement sent", Now)
                    Else
                        body = "Hello " & details(5) & ", <br /><br />Your bank statement for the period of " & CDate(details(7)).ToString("dd-MMM-yyyy") & " to " & CDate(details(8)).ToString("dd-MMM-yyyy") & " has been sent to " & details(3) & ".<br /><br />To authorize their access, hand over the attached ticket. <br /><br />No of Pages : <b>" + details(9).ToString & "</b><br /><br />Total Charges : <b>" + CDbl(details(12)).ToString("#,##0") + " " + details(22) + "</b><br /><br /> Note : If your statement is attached, open with the password in the ticket attached. <br /> <br />Regards"
                        BLL._insertTicketMailAlert(TicketNo, details(5), details(3), details(13), details(15), details(14), details(18), subject, body, "0", type)
                        BLL._insertSMSAlert(TicketNo, details(5), details(3), details(13), details(15), details(14), details(23), "E-Statement", "Your eStatement has been sent to " + details(3) + " TicketNo:" + TicketNo + " Psw:" + details(20) + " Fee:" + CDbl(details(12)).ToString("#,##0") + " " + details(22), "0")
                        BLL._insertAuditLogs(details(1), details(3), "Initiator", TicketNo, "Statement sent", Now)
                    End If

                End If
                BLL._UpdatePws(TicketNo, "")
                If (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" + TicketNo + "_wm.pdf"))) Then
                    File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" + TicketNo + "_wm.pdf"))
                End If

                Return "File Delivered successfully"
            Else
                Utility.LogException(details(1) & " " & strReturn & " " & Now.ToString)
                Return strReturn
            End If


        Catch ex As System.Net.WebException
            outcome = BLL._insertStatementUnsentLog(details(0), details(13), details(14), "pdf", "0")

            If outcome = "Fail" Then Return "fail" Else Return "File Sent successfully"
            ' BLL._insertExceptionLog(details(14), ex.Message.ToString(), Now, details(15), HttpContext.Current.Session("BranchName"))
            Utility.LogException(details(1) & " " & ex.Message.ToString() & " " & Now.ToString)
        Catch ex As System.Web.Services.Protocols.SoapException

            outcome = BLL._insertStatementUnsentLog(details(0), details(13), details(14), "pdf", "0")


            If outcome = "Fail" Then Return "fail" Else Return "File Sent successfully"
            '  BLL._insertExceptionLog(details(14), ex.Message.ToString(), Now, details(15), HttpContext.Current.Session("BranchName"))
            Utility.LogException(details(1) & " " & "***** Sending Statement failed*** " & ex.Message.ToString() & " " & Now.ToString)
        Catch ex As Exception
            'display errors
            ' BLL._insertExceptionLog(details(14), ex.Message.ToString(), Now, details(15), HttpContext.Current.Session("BranchName"))
            Utility.LogException(details(1) & " " & "***** Sending Statement failed*** " & ex.Message())
            BLL._insertAuditLogs(details(1), details(3), "Initiator", TicketNo, "Statement sending failed", Now.ToString)
            Return ex.Message.ToString
        Finally
            'cleanup
            oWS = Nothing
            oFile = Nothing
            'oFileStream.Close()
        End Try
    End Function

End Class
