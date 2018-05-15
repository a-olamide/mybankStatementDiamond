Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports System.Web.Mail
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Web.Configuration
Imports System.Net.Mime

Public Class SenderMail
    Public Shared Function Send(ByVal subject As String, ByVal message As String, ByVal attachementfilepath As String, ByVal ReceiverEmail As String, ByVal ticket As String,
                           Optional ByVal both As String = "0", Optional ByVal MailsToCopy As String = Nothing) As Boolean
        Dim multiple As String = "0"
        'If ticket IsNot Nothing Then
        '    Dim mailOpt As String = BLL._SelectMailOption(ticket)
        '    If mailOpt.Split("_")(1) = 1 Then
        '        multiple = "1"
        '    Else
        '        multiple = mailOpt.Split("_")(0)
        '    End If

        'End If
        Dim flag As Boolean = False

        Dim sender As String = WebConfigurationManager.AppSettings("SenderEmail").ToString()
        Dim senderpassword As String = WebConfigurationManager.AppSettings("SenderPassword").ToString()
        Dim receiver As String = ReceiverEmail 'WebConfigurationManager.AppSettings("ReceiverEmail").ToString()
        Dim mailportnumber As Integer = Integer.Parse(WebConfigurationManager.AppSettings("MailPortNumber").ToString())
        Dim mailserver As String = WebConfigurationManager.AppSettings("MailServer").ToString()
        Dim mailreceivers As String() = Nothing
        Dim deliverystatus As String = "0"

        Dim mailmsg As New System.Net.Mail.MailMessage
        Dim mimeType As ContentType = New System.Net.Mime.ContentType("text/html")
        'Decode the html in the txtBody TextBox...  

        Dim body As String = HttpUtility.HtmlDecode(message)
        Dim alternate As AlternateView = AlternateView.CreateAlternateViewFromString(body, mimeType)
        mailmsg.AlternateViews.Add(alternate)
        mailmsg.SubjectEncoding = Encoding.UTF8
        mailmsg.From = New System.Net.Mail.MailAddress(sender, "mybankStatement (Diamond Bank)", Encoding.UTF8)
        Dim receivers As String() = receiver.Split(",")
        For i As Integer = 0 To receiver.Split(",").Count - 1
            If receivers(i).Contains("@") Then
                mailmsg.To.Add(New System.Net.Mail.MailAddress(receivers(i)))
            End If
        Next

        If Not IsNothing(MailsToCopy) Then
            If Not MailsToCopy.Contains("nbsp") Then
                mailmsg.CC.Add(MailsToCopy)

            End If
        End If
        'Try
        If Not IsNothing(attachementfilepath) Then
            Dim strm As New MemoryStream(File.ReadAllBytes(attachementfilepath))
            Dim contype As New System.Net.Mime.ContentType
            contype.MediaType = System.Net.Mime.MediaTypeNames.Application.Octet
            contype.Name = "Ticket.pdf"
            Dim atc As New System.Net.Mail.Attachment(strm, contype)
            mailmsg.Attachments.Add(atc)

            If both = "1" Then
                Dim strm2 As New MemoryStream(File.ReadAllBytes("D:\mybankStatementRepository\statement\" & ticket & ".pdf"))
                Dim contype2 As New System.Net.Mime.ContentType
                contype2.MediaType = System.Net.Mime.MediaTypeNames.Application.Octet
                contype2.Name = "Bank Statement.pdf"
                Dim atc2 As New System.Net.Mail.Attachment(strm2, contype2)
                mailmsg.Attachments.Add(atc2)

            End If
        End If
        'Catch ex As Exception

        '    Return flag
        'End Try

        Try
            Dim client As New System.Net.Mail.SmtpClient(mailserver, mailportnumber)
            client.EnableSsl = False
            client.UseDefaultCredentials = False
            client.Credentials = New System.Net.NetworkCredential(sender, senderpassword)
            ' client.DeliveryFormat = SmtpDeliveryFormat.SevenBit,
            '   client.DeliveryMethod = SmtpDeliveryMethod.Network

            mailmsg.IsBodyHtml = True '//false if the message body contains code
            mailmsg.Priority = Mail.MailPriority.High
            mailmsg.Subject = subject
            mailmsg.Body = body
            client.Send(mailmsg)

            mailmsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess

            mailmsg.Attachments.Clear()
            mailmsg.Dispose()
            flag = True
            Return True

        Catch ex As Exception
            ' Throw New SmtpFailedRecipientException("The following problem occurred when attempting to " + "send your email: " + ex.Message)
            Return False
        Finally

            mailmsg = Nothing
            '  Dim fil2 As New FileInfo(attachementfilepath)
            '   If (fil2.Exists) Then

            '  fil2.Delete()
            '  End If
        End Try

    End Function
End Class
