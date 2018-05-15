Imports iTextSharp.text.pdf
Imports System.Data
Imports iTextSharp.text
Imports System.Globalization

Public Class itsEvents
    Inherits PdfPageEventHelper
    Public dtTransaction As DataTable
    Public dtAccount As DataTable
    Public PstartDate As String
    Public PendDate As String
    Public PtotalCt As Double
    Public PtotalDt As Double
    Public Pdestination As String
    Public ticketNoHeader As String
    Dim subTitleFont2 As Font = FontFactory.GetFont("Arial", 8, Font.NORMAL, New BaseColor(119, 136, 153))

    Public Overrides Sub OnStartPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document)



        If Pdestination = "0" Or Pdestination = "" Or Pdestination.ToLower = "e-mail" Or Pdestination = "M" Or Pdestination.ToLower = "corporatem" Then
            Dim name As String = dtAccount.Rows(0).Item(4)
            Dim address As String = dtAccount.Rows(0).Item(2)
            Dim ndate As String = PstartDate & " - " & PendDate
            Dim logo = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\Header.png"))
            logo.ScalePercent(67)
            logo.SetAbsolutePosition(0, 750)
            document.Add(logo)

            If writer.PageNumber > 1 Then
                Dim logoBlackHeader = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\BlackHeader.png"))
                logoBlackHeader.ScalePercent(67)
                logoBlackHeader.Alignment = Element.ALIGN_CENTER
                logoBlackHeader.SetAbsolutePosition(125, 715)
                document.Add(logoBlackHeader)
            End If
            document.Add(New Paragraph(" ", subTitleFont2))
            document.Add(New Paragraph(" ", subTitleFont2))
            Dim cb As PdfContentByte = writer.DirectContent
            cb.SetFontAndSize(Format.customfont, 15)
            cb.SetRGBColorFill(255, 255, 255)
            ' we tell the ContentByte we're ready to draw text
            cb.BeginText()
            ' we draw some text on a certain position
            cb.SetTextMatrix(40, 790)
            cb.ShowText(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtAccount.Rows(0).Item("Name").ToString))

            ' we tell the contentByte, we've finished drawing text
            cb.EndText()

            Dim cbPeriod As PdfContentByte = writer.DirectContent
            cbPeriod.SetFontAndSize(Format.customfont, 12)
            cbPeriod.SetRGBColorFill(255, 255, 255)
            ' we tell the ContentByte we're ready to draw text
            cbPeriod.BeginText()
            ' we draw some text on a certain position
            cbPeriod.SetTextMatrix(155, 772)
            cbPeriod.ShowText(CultureInfo.CurrentCulture.TextInfo.ToTitleCase("(" & ndate & ")"))

            ' we tell the contentByte, we've finished drawing text
            cbPeriod.EndText()
            If writer.PageNumber > 1 Then
                Dim cbAcctDetails As PdfContentByte = writer.DirectContent
                cbAcctDetails.SetFontAndSize(Format.customfont, 11)
                cbAcctDetails.SetRGBColorFill(255, 255, 255)
                ' we tell the ContentByte we're ready to draw text
                cbAcctDetails.BeginText()
                ' we draw some text on a certain position
                cbAcctDetails.SetTextMatrix(145, 730)
                cbAcctDetails.ShowText(Utility.reformatnuban(dtAccount.Rows(0).Item("NUBAN")) & "-(" & dtAccount.Rows(0).Item("TYPE").ToString.ToUpper & " ACCOUNT - " & dtAccount.Rows(0).Item("CATEGORY").ToString.ToUpper & ") - " & dtAccount.Rows(0).Item("CURRENCY"))

                ' we tell the contentByte, we've finished drawing text
                cbAcctDetails.EndText()
            End If


            'Dim logo10 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\HorizontalLine.png"))
            'logo10.ScalePercent(45)
            '' logo10.SetAbsolutePosition(40, 1000)
            'logo10.Alignment = 1
            'document.Add(logo10)
            'document.Add(New Paragraph(" ", subTitleFont2))
            'document.Add(New Paragraph(" ", subTitleFont2))
            ''document.Add(New Paragraph("RC No. " & HttpContext.Current.Session("RCNO"), subTitleFont2))

            'Dim nuban As String = dtAccount.Rows(0).Item(1)
            'Dim typelist As String() = {dtAccount.Rows(0).Item(5)}
            'Dim addresslist As String() = {dtAccount.Rows(0).Item(2)}
            ''  Dim header As String = HttpContext.Current.Session("aHeader")
            '' Dim value As String = HttpContext.Current.Session("aValue")
            'Dim edate As String = PstartDate & " - " & PendDate

            ''j = HttpContext.Current.Session("Pageheader")

            'document.Add(gettableoverview(typelist(0), edate, 1, 0, addresslist(0), name))
            'Dim RoyalBlue As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#000080")

            'Dim black As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#000000")
            'Dim iblue As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#000099")
            'Dim Green As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#b9d507")
            'Dim Gray As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#A8A8A8")
            'Dim blue As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#0000FF")
            'Dim fontpath As String = System.Web.Hosting.HostingEnvironment.MapPath("~\styles\")
            'Dim customfont As BaseFont = BaseFont.CreateFont(fontpath + "GIL_____.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)

            'Dim font7Blue As Font = New Font(customfont, 7, 0, New BaseColor(RoyalBlue.R, RoyalBlue.G, RoyalBlue.B))
            'Dim font7Black As Font = New Font(customfont, 7, 0, New BaseColor(black.R, black.G, black.B))
            'Dim font11Blue As Font = New Font(customfont, 11, 0, New BaseColor(RoyalBlue.R, RoyalBlue.G, RoyalBlue.B))
            'Dim font7iblue As Font = New Font(customfont, 7, Font.ITALIC, New BaseColor(223, 22, 14))

            'Dim c1 As New Chunk(" ", font7Black)
            'Dim c3 As New Chunk("page no ", font7iblue)
            'Dim c4 As New Chunk(HttpContext.Current.Session("PAGESIZE").ToString, font7iblue)
            'Dim p1 As New Phrase(c1)

            'p1.Add(c3)
            'p1.Add(c4)
            'Dim cell_ As New PdfPCell(p1)
            'cell_.BorderWidth = "0"
            'Dim widths() As Single = {90.0F, 10.0F}
            'Dim table22 As New PdfPTable(2)
            'table22.SetWidths(widths)
            ''  table22.LockedWidth = True
            'Dim k As New PdfPCell
            'k.BorderWidth = "0"
            'table22.AddCell(k)
            'table22.AddCell(cell_)
            'table22.WidthPercentage = 100.0F
            Dim sm As PdfPTable = getSocialMedia()
            sm.WriteSelectedRows(0, -1, 200, 150, writer.DirectContent)
            ' document.Add(getSocialMedia)
            document.Add(New Paragraph(" ", subTitleFont2))
            document.Add(New Paragraph(" ", subTitleFont2))
            Dim logo2 = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~\images\Advert\" & "footer.png"))
            logo2.ScalePercent(66)
            logo2.SetAbsolutePosition(0, 40)

            document.Add(logo2)
            Dim ft As PdfPTable = getFooterInfoTable(writer)
            ft.WriteSelectedRows(0, -1, 55, 45, writer.DirectContent)
            ' HttpContext.Current.Session("PAGESIZE") += 1
        Else
            If writer.PageNumber > 1 Then
                Dim ch As New Chunk("mybankStatement ® | " & Now.Date() & " | " & ticketNoHeader & "-8" & " | Page " & writer.PageNumber, subTitleFont2)
                document.Add(ch)
                document.Add(New Paragraph(" ", subTitleFont2))
            Else
                document.Add(New Paragraph(" ", subTitleFont2))
                document.Add(New Paragraph(" ", subTitleFont2))
            End If
        End If


    End Sub

    'Public Overrides Sub OnEndPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document)
    '    If Pdestination = "0" Or Pdestination = "" Or Pdestination.ToLower = "e-mail" Or Pdestination = "M" Then
    '        Dim table As New PdfPTable(1)
    '        table.TotalWidth = 500.0F
    '        Dim table1 As New PdfPTable(1)
    '        table1.TotalWidth = 500.0F
    '        Dim cell As New PdfPCell()
    '        Dim cell1 As New PdfPCell()
    '        Dim p As New Paragraph()
    '        Dim p1 As New Paragraph()
    '        Dim fb = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\facebook.jpg"))
    '        fb.ScalePercent(60)
    '        Dim twitter = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\twitter.jpg"))
    '        twitter.ScalePercent(60)
    '        Dim instagram = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Instagram.jpg"))
    '        instagram.ScalePercent(60)
    '        Dim Utube = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\uTube.jpg"))
    '        Utube.ScalePercent(60)
    '        Dim linkIn = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\linkedIn.png"))
    '        linkIn.ScalePercent(60)
    '        Dim first As New Chunk("copyrigt © 2014", EcoFont)

    '        Dim space1 As New Chunk(" ")
    '        Dim web As New Chunk("www.unionbankng.com", link)
    '        web.SetAnchor("https://www.unionbankng.com")


    '        Dim remain As New Chunk("All right reserved.", EcoFont)

    '        p1.Add(first)
    '        p1.Add(space1)
    '        p1.Add(web)
    '        p1.Add(space1)
    '        p1.Add(remain)
    '        cell1.AddElement(p1)
    '        cell1.BorderWidth = "0"
    '        ' cell1.HorizontalAlignment = 1
    '        p.Add(New Chunk(fb, 0, 0))
    '        p.Add(New Chunk("unionbankng", linkN).SetAnchor("https://www.facebook.com/unionbankng"))
    '        p.Add(New Chunk(twitter, 0, 0))
    '        p.Add(New Chunk("@unionbank_ng", linkN).SetAnchor("https://www.twitter.com/unionbank_ng"))
    '        p.Add(New Chunk(linkIn, 0, 0))
    '        p.Add(New Chunk("unionbankng", linkN).SetAnchor("https://www.linkedin.com/unionbankng"))
    '        p.Add(New Chunk(instagram, 0, 0))
    '        p.Add(New Chunk("@unionbankng", linkN).SetAnchor("https://www.instagram.com/unionbankng"))
    '        p.Add(New Chunk(Utube, 0, 0))
    '        p.Add(New Chunk("unionbankng", linkN).SetAnchor("https://www.youtube.com/unionbankng"))
    '        cell.AddElement(p)
    '        cell.BorderWidth = "0"
    '        cell.HorizontalAlignment = 0
    '        table1.AddCell(cell1)
    '        table.AddCell(cell)
    '        table1.WriteSelectedRows(0, -1, 180, 100, writer.DirectContent())
    '        table.WriteSelectedRows(0, -1, 95, 70, writer.DirectContent())
    '        'document.Add(table)
    '    End If
    'End Sub

     Private Function getSocialMedia() As PdfPTable
        Dim table As New PdfPTable(1)
        table.TotalWidth = 500.0F
        ' Dim table1 As New PdfPTable(1)
        'table1.TotalWidth = 500.0F
        Dim cell As New PdfPCell()
        '  Dim cell1 As New PdfPCell()
        Dim p As New Paragraph()
        ' Dim p1 As New Paragraph()
        Dim fb = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\fb.png"))
        'fb.ScalePercent(60)
        Dim twitter = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\twitter.png"))
        ' twitter.ScalePercent(60)
        Dim google = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\google.png"))
        ' google.ScalePercent(60)
        Dim linkIn = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\linkedIn.png"))
        ' linkIn.ScalePercent(60)
        Dim Utube = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\uTube.png"))
        ' Utube.ScalePercent(60)
        Dim instagram = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Instagram.png"))
        ' instagram.ScalePercent(60)
        Dim blogger = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\blogger.png"))
        ' blogger.ScalePercent(60)

        'Dim first As New Chunk("copyrigt © 2014", EcoFont)

        'Dim space1 As New Chunk(" ")
        'Dim web As New Chunk("www.unionbankng.com", link)
        'web.SetAnchor("https://www.unionbankng.com")


        'Dim remain As New Chunk("All right reserved.", EcoFont)

        'p1.Add(first)
        'p1.Add(space1)
        'p1.Add(web)
        'p1.Add(space1)
        'p1.Add(remain)
        ' cell1.AddElement(p1)
        'cell1.BorderWidth = "0"
        ' cell1.HorizontalAlignment = 1
        p.Add(New Chunk(fb, 0, 0).SetAnchor("https://www.facebook.com/ubagroup"))
        p.Add(New Chunk("     ", Format.bodyFont8))
        p.Add(New Chunk(twitter, 0, 0).SetAnchor("https://twitter.com/UBAGroup"))
        p.Add(New Chunk("     ", Format.bodyFont8))
        p.Add(New Chunk(google, 0, 0).SetAnchor("https://plus.google.com/+Ubagroup"))
        p.Add(New Chunk("     ", Format.bodyFont8))
        p.Add(New Chunk(linkIn, 0, 0).SetAnchor("https://www.linkedin.com/company/uba"))
        p.Add(New Chunk("     ", Format.bodyFont8))
        p.Add(New Chunk(instagram, 0, 0).SetAnchor("https://www.instagram.com/ubagroup"))
        p.Add(New Chunk("     ", Format.bodyFont8))
        p.Add(New Chunk(Utube, 0, 0).SetAnchor("https://www.youtube.com/user/UBAGroupVideos"))

        cell.AddElement(p)
        cell.BorderWidth = "0"
        cell.HorizontalAlignment = 0

        table.AddCell(cell)

        Return table
    End Function
    Private Function getFooterInfoTable(ByVal writer As iTextSharp.text.pdf.PdfWriter) As PdfPTable
        Dim _dt As New DataTable
        'k.UserName = ConfigurationManager.AppSettings("Username").ToString() : k.Password = ConfigurationManager.AppSettings("Password").ToString()

        Dim table As PdfPTable = New PdfPTable(2)
        table.TotalWidth = 510.0F
        Dim widths() As Single = {1.0F, 1.0F}
        table.SetWidths(widths)
        table.LockedWidth = True
        Dim cell1 As New PdfPCell()
        Dim cell2 As New PdfPCell()
        Dim p1 As New Paragraph("Africa's global bank", Format.font10Blacki)
        cell1.AddElement(p1)
        cell1.BorderWidth = 0
        cell1.HorizontalAlignment = Element.ALIGN_LEFT
        table.AddCell(cell1)

        Dim p2 As New Paragraph(PstartDate & " - " & PendDate & " Bank Statement - " & writer.PageNumber, Format.font10Blacki)
        cell2.AddElement(p2)
        cell2.BorderWidth = 0
        cell2.HorizontalAlignment = Element.ALIGN_RIGHT
        table.AddCell(cell2)
        Return table
    End Function
    Private Function gettableoverview(header As String, edate As String, col As Integer, num As Integer, address As String, name As String) As PdfPTable
        Dim table As PdfPTable = New PdfPTable(2)
        table.TotalWidth = 510.0F
        table.LockedWidth = True
        table.HorizontalAlignment = 1
        Dim widths() As Single = {3.0F, 2.0F}
        table.SetWidths(widths)
        Dim c1 As New Chunk(name, Format.font14)
        Dim c2 As New Chunk("Bank Statement", Format.font10)

        '  Dim c4 As New Chunk(pagesizes, font11Blue)
        Dim p1 As New Phrase(c1)
        p1.Add(Environment.NewLine)
        p1.Add(c2)
        p1.Add(Environment.NewLine)

        Dim cell_ As New PdfPCell(p1)
        cell_.MinimumHeight = 17.0F
        cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
        cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
        cell_.PaddingLeft = 0.0F
        cell_.SetLeading(0, 1.2F)
        '   table.AddCell(getcell2_(name & "," & address, 1))
        table.AddCell(cell_)
        table.AddCell(getcellaccountoverview(header, edate, col, num))
        Return table
    End Function
    Private Function getcellaccountoverview(Header As String, edate As String, col As Integer, num As Integer) As PdfPCell
        If col = 1 Then
            Dim text_ As String = ""
            If num = "0" Then
                text_ = Header.ToUpper
            Else
                text_ = Header.ToUpper
            End If

            Dim c1 As New Chunk(text_, Format.font14)
            Dim c2 As New Chunk(edate, Format.font10)
            Dim c3 As New Chunk("page no ", Format.font10)
            '  Dim c4 As New Chunk(pagesizes, font11Blue)
            Dim p1 As New Phrase(c1)
            p1.Add(Environment.NewLine)
            p1.Add(c2)
            p1.Add(Environment.NewLine)
            ''  p1.Add(c3)
            'p1.Add(c4)
            Dim cell_ As New PdfPCell(p1)
            cell_.HorizontalAlignment = 2
            cell_.PaddingRight = 0.0F

            cell_.SetLeading(0, 1.4F)
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        End If
    End Function
End Class
