Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Net

Public Class SendCSVtoService
    Private oFileInfo As FileInfo
    Private TicketNo As String
    Private details As String() = Nothing
    Public Sub New(FileName As FileInfo, PTicketNo As String)
        oFileInfo = FileName
        TicketNo = PTicketNo

    End Sub

    Public Function doCSVFileUploadViaWebService() As String
        Dim outcome As String = ""
        Dim details As String() = Nothing
        Dim oFileByte As Byte()
        Dim oFile As File
        'Dim oFileStream As FileStream
        Dim oWS As New net.mybankstatement.WebService()
        Dim strReturn As String = ""
        Try

            Try


                'Make sure that the file exists before trying to upload
                If File.Exists(oFileInfo.FullName) = False Then
                    Return "CSV File doesnt exist"
                    'Throw New Exception("CSV File doesnt exist")
                End If

                Using oFileStream As FileStream = File.Open(oFileInfo.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read)

                    'declare the byte array of the file
                    oFileByte = New Byte(oFileStream.Length - 1) {}

                    'break the file into bytes and place into the byte object
                    oFileStream.Read(oFileByte, 0, oFileStream.Length)
                End Using
                details = BLL._selectMetaData(TicketNo)
                'Dim proxyVal As New WebProxy("http://10.0.0.120:80", True)
                'oWS.Proxy = proxyVal
                oWS.SendCSVStatement(TicketNo, "8", "mybankStatement88*", oFileInfo.Name, oFileByte, strReturn)

                If strReturn = "File sent successfully" Then
                    Return "File Delivered successfully"

                Else
                    outcome = BLL._insertStatementUnsentLog(details(0), details(13), "8", "csv", "0")
                    If outcome = "Fail" Then Return "fail" Else Return "File Sent successfully"
                End If


            Catch ex As System.Net.WebException
                outcome = BLL._insertStatementUnsentLog(details(0), details(13), details(14), "csv", "0")
                If outcome = "Fail" Then Return "fail" Else Return "File Sent successfully"
                BLL._insertExceptionLog(details(14), ex.Message.ToString(), Now, details(15), HttpContext.Current.Session("BranchName"))
                Utility.LogException(details(1) & " " & details(2) & " " & "***** Re-Sending Statement failed*** " & ex.Message())
            Catch ex As System.Web.Services.Protocols.SoapException
                BLL._insertExceptionLog(details(14), ex.Message.ToString(), Now, details(15), HttpContext.Current.Session("BranchName"))
                Utility.LogException(details(1) & " " & details(2) & " " & "***** Re-Sending Statement failed*** " & ex.Message())
                outcome = BLL._insertStatementUnsentLog(details(0), details(13), details(14), "csv", "0")
                If outcome = "Fail" Then Return "fail" Else Return "File Sent successfully"

            Catch ex As Exception
                'display errors
                BLL._insertExceptionLog(details(14), ex.Message.ToString(), Now, details(15), HttpContext.Current.Session("BranchName"))
                Utility.LogException(details(1) & " " & details(2) & " " & "***** Re-Sending Statement failed*** " & ex.Message())

                Return ex.Message.ToString()
            Finally
                'cleanup
                oWS = Nothing
                oFile = Nothing
                'oFileStream.Close()
            End Try
            'Else
            '    Return "File Delivered successfully"
            'End If
        Catch ex As Exception
            BLL._insertExceptionLog(details(14), ex.Message.ToString(), Now, details(15), HttpContext.Current.Session("BranchName"))
            Utility.LogException(details(1) & " " & details(2) & " " & "***** Re-Sending Statement failed*** " & ex.Message())
            Return ex.Message.ToString()
        End Try
    End Function
End Class
