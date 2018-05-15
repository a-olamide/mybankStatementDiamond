Imports Microsoft.VisualBasic
Imports iTextSharp.text
Imports System.Data
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Globalization

Public Class GenerateTicket
    Private ReceiptNo As String
    Private AccountName As String
    Private Destination As String
    Private DestCode As String
    Private type As String = Nothing
    Private passcode As String

    Public Sub New(ByVal PReceiptNo As String, ByVal PAccountName As String, ByVal PDestination As String, ByVal PDestCode As String)
        ReceiptNo = PReceiptNo
        AccountName = PAccountName
        Destination = PDestination
        DestCode = PDestCode
    End Sub
    Public Sub New(ByVal PReceiptNo As String, ByVal PAccountName As String, ByVal PDestination As String, ByVal PDestCode As String, ByVal Ptype As String, ByVal Ppasscode As String)
        ReceiptNo = PReceiptNo
        AccountName = PAccountName
        Destination = PDestination
        DestCode = PDestCode
        type = Ptype
        passcode = Ppasscode
    End Sub

    Public Function getTicket() As String
        Try
            Dim dtCompany As DataTable = BLL._selectCompanyName
            Dim companyname As String = dtCompany.Rows(0).Item(1).ToString
            Dim doc As Document = New Document(PageSize.A4, 20, 20, 25, 20)
            If IsNothing(type) Then
                System.Web.HttpContext.Current.Session("PAGESIZE") = -1
            End If
            Dim pdfWrite As PdfWriter = Nothing
            If type Is Nothing Then
                pdfWrite = PdfWriter.GetInstance(doc, New FileStream(HttpContext.Current.Server.MapPath("~\receipt\" + ReceiptNo + ".pdf"), FileMode.Create))
            Else
                pdfWrite = PdfWriter.GetInstance(doc, New FileStream("D:\mybankStatementRepository\statementRequestTicket\" + ReceiptNo + ".pdf", FileMode.Create))
            End If
            Dim ev As New itsEvents
            ev.Pdestination = Destination
            ev.ticketNoHeader = ReceiptNo

            pdfWrite.PageEvent = ev
            doc.AddAuthor(companyname)
            doc.AddTitle("Bank Statement ticket for " & CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AccountName) & " , " & companyname)
            doc.AddCreator("mybankStatement®")
            doc.AddSubject("Bank Statement ticket for " & companyname)
            doc.AddKeywords("Bank Statement ticket")
            doc.Open()
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            Dim p1 As New Paragraph("mybankStatement®", Format.Font1)
            p1.Alignment = Element.ALIGN_CENTER
            doc.Add(New Paragraph(p1))
            Dim p2 As New Paragraph("Ticket No. " & ReceiptNo & "-8", Format.Font2)
            p2.Alignment = Element.ALIGN_CENTER
            doc.Add(New Paragraph(p2))
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            doc.Add(New Paragraph("", Format.subTitleFont))
            Dim p5 As Paragraph

            p5 = New Paragraph("Password: " & passcode, Format.passwordFont)
            p5.Alignment = Element.ALIGN_CENTER
            doc.Add(New Paragraph(p5))
            doc.Add(New Paragraph("", Format.subTitleFont))
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            Dim p3 As New Paragraph("Account Name: " & CultureInfo.CurrentCulture.TextInfo.ToTitleCase(HttpUtility.HtmlDecode(AccountName)), Format.Font3)
            p3.Alignment = Element.ALIGN_CENTER
            doc.Add(New Paragraph(p3))

            Dim p6 As New Paragraph("Destination: " & Destination, Format.Font3)
            p6.Alignment = Element.ALIGN_CENTER
            doc.Add(New Paragraph(p6))

            Dim logo = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Receipt.png"))
            logo.ScalePercent(60)
            logo.SetAbsolutePosition(-25, 500)
            doc.Add(logo)
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            Dim logo2 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\hr.png"))
            logo2.ScalePercent(60)
            logo2.SetAbsolutePosition(110, 450)
            doc.Add(logo2)
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            Dim logo4 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\paid.png"))
            logo4.ScalePercent(60)
            logo4.SetAbsolutePosition(210, 360)
            doc.Add(logo4)
            Dim logo3 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\hr.png"))
            logo3.ScalePercent(60)
            logo3.SetAbsolutePosition(110, 340)
            doc.Add(logo3)
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            doc.Add(New Paragraph(" ", Format.subTitleFont))
            doc.Add(New Paragraph(" ", Format.subTitleFont))

            'Dim pInfo2 As New Paragraph()
            'Dim complaint2 As New Chunk("Please for support call our Diamond bank support team on +234-803-830-3398 or contact us via email at ", Format.supportFont)
            'Dim emailLink2 As New Chunk("support@Diamondbankng.com", Format.link)
            'emailLink2.SetAnchor("support@Diamondbankng.com")
            'emailLink2.Font.SetStyle("ITALIC")

            '' Dim cell5 As New Chunk("You can also send us an email at", Format.infoText)
            'pInfo2.Add(complaint2)
            'pInfo2.Add(emailLink2)
            '' p1.Add(cell5)
            ''pInfo.SetAlignment("Center")
            'pInfo2.Alignment = Element.ALIGN_CENTER
            'doc.Add(pInfo2)


            Dim pInfo As New Paragraph()
            Dim complaint As New Chunk("And for further enquires and verification services call our support team on +234-803-830-3398 or contact us via email at ", Format.supportFont)
            Dim emailLink As New Chunk("support@wallzandqueenltd.com", Format.link)
            emailLink.SetAnchor("mailto:support@wallzandqueenltd.com")
            emailLink.Font.SetStyle("ITALIC")

            ' Dim cell5 As New Chunk("You can also send us an email at", Format.infoText)
            pInfo.Add(complaint)
            pInfo.Add(emailLink)
            ' p1.Add(cell5)
            'pInfo.SetAlignment("Center")
            pInfo.Alignment = Element.ALIGN_CENTER
            doc.Add(pInfo)
            Dim footer = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\ticketFooter.png"))
            footer.ScalePercent(60)
            footer.SetAbsolutePosition(0, 0)
            doc.Add(footer)
            doc.Close()
            Return "True"
        Catch ex As Exception
            Try
                BLL._insertExceptionLog(System.Web.HttpContext.Current.Session("SenderID"), ex.Message.ToString(), Now, System.Web.HttpContext.Current.Session("Company"), HttpContext.Current.Session("BranchName"))

            Catch

            End Try
            Return "False"
        End Try
    End Function

End Class
