Imports Microsoft.VisualBasic
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports iTextSharp.text.html
Imports System.Globalization

Public Class Format

    Public Shared Font1 As Font = FontFactory.GetFont("Arial", 30, 1)
    Public Shared Font_1 As Font = FontFactory.GetFont("Gill Sans MT", 30, 1)
    Public Shared font7 As Font = New Font(customfont, 7)
    Public Shared fontpath As String = System.Web.Hosting.HostingEnvironment.MapPath("~\Styles\")
    Public Shared customfont As BaseFont = BaseFont.CreateFont(fontpath + "GIL_____.TTF", BaseFont.CP1252, BaseFont.EMBEDDED)

    Public Shared Font2 As Font = FontFactory.GetFont("Arial", 20, 1)
    Public Shared Font3 As Font = FontFactory.GetFont("Arial", 14)
    Public Shared passwordFont As Font = FontFactory.GetFont("Courier New", 14)
    Public Shared ecoNormalFont As Font = FontFactory.GetFont("Arial", 10)
    Public Shared TableFont As Font = FontFactory.GetFont("Arial", 10)
    Public Shared TableFont2 As Font = FontFactory.GetFont("Arial", 10)
    Public Shared bdrColor As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#e7eaec")
    Public Shared hdrbgColor As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#F5F5F6")
    Public Shared midbgColor As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#F9F9F9")
    Public Shared TableFontbold As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, New BaseColor(0, 0, 0))
    Public Shared EcoFontbold As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, New BaseColor(255, 255, 255))
    Public Shared EcoFontbold2 As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, New BaseColor(255, 255, 255))
    Public Shared headFont As Font = FontFactory.GetFont("Arial", 14, Font.UNDERLINE, New BaseColor(0, 0, 0))
    Public Shared EcoFont As Font = FontFactory.GetFont("Arial", 10, Font.NORMAL, New BaseColor(0, 0, 0))
    Public Shared supportFont As Font = FontFactory.GetFont("Arial", 10, Font.ITALIC, New BaseColor(0, 0, 0))
    Public Shared link As Font = FontFactory.GetFont("Arial", 10, Font.UNDERLINE, New BaseColor(0, 0, 128))
    Public Shared linkN As Font = FontFactory.GetFont("Arial", 10, Font.NORMAL, New BaseColor(0, 0, 128))
    ' Public Shared info As Font = FontFactory.GetFont("Arial", 10, Font.NORMAL, New BaseColor(45, 179, 179))
    Public Shared info As BaseColor = New BaseColor(45, 179, 179)
    Public Shared unionBg As BaseColor = New BaseColor(26, 164, 225)
    Public Shared unionBg2 As BaseColor = New BaseColor(147, 227, 227)
    Public Shared infoText As Font = FontFactory.GetFont("Arial", 10, Font.NORMAL, New BaseColor(255, 255, 255))
    Public Shared titleFont As Font = FontFactory.GetFont("Arial", 14)
    Public Shared subTitleFont As Font = FontFactory.GetFont("Arial", 8)
    Public Shared endingMessageFont As Font = FontFactory.GetFont("Arial", 10, Font.ITALIC)
    Public Shared bodyFont As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL)
    Public Shared bodyFont2 As Font = FontFactory.GetFont("Arial", 12, Font.UNDERLINE)
    'Private Public Shared akp As AsymmetricKeyParameter
    'Private Public Shared path As String = System.Web.Hosting.HostingEnvironment.MapPath("~\certificate\Backup.p12")
    'Private Public Shared password As String = "wallz"
    'Private Public Shared chain As Org.BouncyCastle.X509.X509Certificate()
    Public Shared White As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
    Public Shared black As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#000000")
    Public Shared Green As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#b9d507")
    Public Shared Gray As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#A8A8A8")
    Public Shared blue As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#0000FF")
    Public Shared webdingsfont As BaseFont = BaseFont.CreateFont(fontpath + "webdings.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
    Public Shared iblue As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#000099")
    Public Shared RoyalBlue As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#000080")
    Public Shared bodyFont8 As Font = FontFactory.GetFont("Gill Sans MT", 11, Font.NORMAL, New BaseColor(RoyalBlue.R, RoyalBlue.G, RoyalBlue.B))


    Public Shared bodyFont6 As Font = FontFactory.GetFont("Arial", 8, Font.NORMAL)
    Public Shared bodyFont2P As Font = FontFactory.GetFont("Arial", 12, Font.UNDERLINE)

    Public Shared bodyFontP As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL)
    Public Shared bodyFont3 As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL)
    Public Shared bodyFont4 As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL)
    Public Shared bodyFont5 As Font = FontFactory.GetFont("Arial", 10, Font.NORMAL)
    Public Shared bodyFont9 As Font = FontFactory.GetFont("Arial", 13, 0.7, New BaseColor(212, 46, 11))
    Public Shared bodyFont12 As Font = FontFactory.GetFont("Arial", 8, 0.7, New BaseColor(212, 46, 11))
    Public Shared bodyFont10 As Font = FontFactory.GetFont("Arial", 10, 0, New BaseColor(128, 128, 128))
    Public Shared bodyFont7 As Font = FontFactory.GetFont("Arial", 8, Font.ITALIC)
    Public Shared font8italic As Font = New Font(bodyFont7)
    Public Shared font6 As Font = New Font(customfont, 6)
    Public Shared font8 As Font = New Font(bodyFont6)
    Public Shared font10 As Font = New Font(bodyFont5)
    Public Shared font15 As Font = New Font(bodyFont9)
    Public Shared font11 As Font = New Font(bodyFont4)
    Public Shared font14 As Font = New Font(bodyFont3)
    Public Shared font12 As Font = New Font(bodyFont3)
    Public Shared font13 As Font = New Font(bodyFont10)
    '  Public Shared font7Blue As Font = New Font(customfont, 7, 0, New BaseColor(Green))
    Public Shared font7Blue As Font = New Font(customfont, 7, 0, New BaseColor(223, 22, 14))
    Public Shared font11Blue As Font = New Font(bodyFont8)
    Public Shared font8White As Font = New Font(customfont, 8, 0, New BaseColor(White.R, White.G, White.B))
    Public Shared font10Blacki As Font = New Font(customfont, 10, Font.ITALIC, New BaseColor(0, 0, 0))
    Public Shared font8Blacki As Font = New Font(customfont, 8, Font.ITALIC, New BaseColor(0, 0, 0))
    Public Shared font12White As Font = New Font(customfont, 12, 0, New BaseColor(White.R, White.G, White.B))
    Public Shared font12Whitei As Font = New Font(customfont, 12, Font.ITALIC, New BaseColor(White.R, White.G, White.B))
    Public Shared font10White As Font = New Font(customfont, 10, 0, New BaseColor(0, 0, 0))
    Public Shared font8blue As Font = New Font(customfont, 8, 0, New BaseColor(223, 22, 14))
    Public Shared font10black As Font = New Font(customfont, 10, 0, New BaseColor(0, 0, 0))
    Public Shared font12black As Font = New Font(customfont, 14, 0, New BaseColor(0, 0, 0))
    Public Shared font12blackBold As Font = New Font(customfont, 13, Font.BOLD, New BaseColor(20, 32, 32))
    Public Shared font30White As Font = New Font(customfont, 30, 0, New BaseColor(255, 255, 255))
    Public Shared font8blueitalic As Font = New Font(customfont, 8, Font.ITALIC, New BaseColor(223, 22, 14))
    Public Shared fontSymbols As Font = New Font(webdingsfont, 8)

    Public Shared Function getcell2(ByVal value As String, ByVal col As Integer) As PdfPCell
        Dim cell_ As New PdfPCell(New Phrase(value, Format.TableFont))
        cell_.BorderColor = New BaseColor(Format.bdrColor.R, Format.bdrColor.G, Format.bdrColor.B)
        If col = 1 Then
            cell_.MinimumHeight = 17.0F
            cell_.BackgroundColor = New BaseColor(Format.hdrbgColor.R, Format.hdrbgColor.G, Format.hdrbgColor.B)
            cell_.PaddingLeft = 10.0F
        ElseIf col = 2 Then
            cell_.MinimumHeight = 17.0F
            cell_.PaddingLeft = 10.0F
            cell_.PaddingRight = 10.0F
        ElseIf col = 11 Then
            cell_.BackgroundColor = New BaseColor(Format.hdrbgColor.R, Format.hdrbgColor.G, Format.hdrbgColor.B)
            cell_.PaddingLeft = 10.0F
        ElseIf col = 21 Then
            cell_.PaddingLeft = 10.0F
            cell_.PaddingRight = 10.0F
        ElseIf col = 3 Then
            cell_.MinimumHeight = 17.0F
            cell_.HorizontalAlignment = 2
            cell_.PaddingLeft = 10.0F
            cell_.PaddingRight = 10.0F
        ElseIf col = 4 Then
            cell_.MinimumHeight = 17.0F

            cell_.Colspan = 2
        End If
        Return cell_
    End Function

    Public Shared Function getcell(ByVal value As String, ByVal col As Integer) As PdfPCell
        Dim cell_ As New PdfPCell(New Phrase(value, Format.TableFont2))
        cell_.MinimumHeight = 19.0F
        cell_.BorderColor = New BaseColor(Format.bdrColor.R, Format.bdrColor.G, Format.bdrColor.B)
        If col = 1 Then
            cell_.BackgroundColor = New BaseColor(Format.hdrbgColor.R, Format.hdrbgColor.G, Format.hdrbgColor.B)
        ElseIf col = 11 Then
            cell_.BackgroundColor = New BaseColor(Format.hdrbgColor.R, Format.hdrbgColor.G, Format.hdrbgColor.B)
            cell_.HorizontalAlignment = 1
        ElseIf col = 2 Then
            cell_.HorizontalAlignment = 0
        ElseIf col = 22 Then
            cell_.HorizontalAlignment = 0
            cell_.BackgroundColor = New BaseColor(240, 240, 240)
        ElseIf col = 4 Then
            cell_.HorizontalAlignment = 2
        ElseIf col = 42 Then
            cell_.HorizontalAlignment = 2
            cell_.BackgroundColor = New BaseColor(240, 240, 240)
        ElseIf col = 3 Then
            cell_.BackgroundColor = New BaseColor(Format.midbgColor.R, Format.midbgColor.G, Format.midbgColor.B)
        ElseIf col = 33 Then
            cell_.BackgroundColor = Format.unionBg2
        ElseIf col = 21 Then
            cell_.HorizontalAlignment = 2
        ElseIf col = 31 Then
            cell_.BackgroundColor = New BaseColor(Format.midbgColor.R, Format.midbgColor.G, Format.midbgColor.B)
            cell_.HorizontalAlignment = 2
        ElseIf col = 313 Then
            cell_.BackgroundColor = Format.unionBg2
            cell_.HorizontalAlignment = 2
        ElseIf col = 12 Then
            cell_.BorderWidth = "0"
            cell_.BorderWidthLeft = "1"
            cell_.BorderWidthRight = "1"
            cell_.BorderColor = BaseColor.BLACK
            cell_.MinimumHeight = 19.0F

        ElseIf col = 13 Then
            cell_.HorizontalAlignment = 2
            cell_.BorderWidth = "0"
            cell_.BorderWidthLeft = "1"
            cell_.BorderWidthRight = "1"
            cell_.BorderColor = BaseColor.BLACK
            cell_.MinimumHeight = 19.0F

        ElseIf col = 121 Then
            cell_.BorderWidth = "0"
            cell_.BorderWidthLeft = "1"
            cell_.BorderWidthRight = "1"
            cell_.BorderWidthBottom = "1"
            cell_.BorderColor = BaseColor.BLACK
            cell_.MinimumHeight = 19.0F
        ElseIf col = 131 Then
            cell_.HorizontalAlignment = 2
            cell_.BorderWidth = "0"
            cell_.BorderWidthLeft = "1"
            cell_.BorderWidthRight = "1"
            cell_.BorderColor = BaseColor.BLACK
            cell_.BorderWidthBottom = "1"
            cell_.MinimumHeight = 19.0F
        End If


        Return cell_
    End Function

    Public Shared Function getcell2_(ByVal value As String, ByVal col As Integer) As PdfPCell
        If col = 1 Then
            Dim c1 As New Chunk(value.Split(",")(0), Format.font15)
            Dim c2 As New Chunk(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.Split(",")(1)), Format.font13)
            Dim p1 As New Phrase(c1)
            p1.Add(Environment.NewLine)
            p1.Add(c2)

            Dim cell_ As New PdfPCell(p1)
            cell_.MinimumHeight = 17.0F
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.PaddingLeft = 0.0F
            cell_.SetLeading(0, 1.2F)
            Return cell_
        ElseIf col = 50 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font10))
            cell_.MinimumHeight = 17.0F
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.PaddingLeft = 0.0F
            cell_.SetLeading(0, 0.0F)
            Return cell_
        ElseIf col = 2 Then
            Dim c1 As New Chunk("ACCOUNT OVERVIEW", Format.font14)
            Dim c2 As New Chunk(value, Format.font10)
            Dim c3 As New Chunk("page no ", Format.font10) 'Format.font7Blue
            Dim c4 As New Chunk(HttpContext.Current.Session("PAGESIZE").ToString, Format.font10) 'Format.font11Blue
            Dim p1 As New Phrase(c1)
            p1.Add(Environment.NewLine)
            p1.Add(c2)
            p1.Add(Environment.NewLine)
            '  p1.Add(c3)
            ' p1.Add(c4)
            Dim cell_ As New PdfPCell(p1)
            cell_.HorizontalAlignment = 2
            cell_.PaddingRight = 21.0F
            cell_.SetLeading(0, 1.4F)
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        ElseIf col = 20 Then
            Dim c1 As New Chunk("1. SAVINGS ACCOUNT - GENERAL", Format.font14)
            Dim c2 As New Chunk(value, Format.font10)
            Dim c3 As New Chunk("page no ", Format.font7Blue)
            Dim c4 As New Chunk(HttpContext.Current.Session("PAGESIZE").ToString, Format.font11Blue)
            Dim p1 As New Phrase(c1)
            p1.Add(Environment.NewLine)
            p1.Add(c2)
            p1.Add(Environment.NewLine)
            ' p1.Add(c3)
            ' p1.Add(c4)
            Dim cell_ As New PdfPCell(p1)
            cell_.HorizontalAlignment = 2
            cell_.PaddingRight = 21.0F
            cell_.SetLeading(0, 1.4F)
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_

        ElseIf col = 21 Then
            Dim c1 As New Chunk("2. CURRENT ACCOUNT - STAFF", Format.font14)
            Dim c2 As New Chunk(value, Format.font10)
            Dim c3 As New Chunk("page no ", Format.font7Blue)
            Dim c4 As New Chunk(HttpContext.Current.Session("PAGESIZE").ToString, Format.font11Blue)
            Dim p1 As New Phrase(c1)
            p1.Add(Environment.NewLine)
            p1.Add(c2)
            p1.Add(Environment.NewLine)
            ' p1.Add(c3)
            'p1.Add(c4)
            Dim cell_ As New PdfPCell(p1)
            cell_.HorizontalAlignment = 2
            cell_.PaddingRight = 21.0F
            cell_.SetLeading(0, 1.4F)
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        ElseIf col = 211 Then
            Dim c1 As New Chunk("4. DOMICILIARY ACCOUNT GENERAL", Format.font14)
            Dim c2 As New Chunk(value, Format.font10)
            Dim c3 As New Chunk("page no ", Format.font7Blue)
            Dim c4 As New Chunk(HttpContext.Current.Session("PAGESIZE").ToString, Format.font11Blue)
            Dim p1 As New Phrase(c1)
            p1.Add(Environment.NewLine)
            p1.Add(c2)
            p1.Add(Environment.NewLine)
            'p1.Add(c3)
            'p1.Add(c4)
            Dim cell_ As New PdfPCell(p1)
            cell_.HorizontalAlignment = 2
            cell_.PaddingRight = 21.0F
            cell_.SetLeading(0, 1.4F)
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        ElseIf col = 212 Then
            Dim c1 As New Chunk("4. DOMICILIARY ACCOUNT GENERAL", Format.font14)
            Dim c2 As New Chunk(value, Format.font10)
            Dim c3 As New Chunk("page no ", Format.font7Blue)
            Dim c4 As New Chunk(HttpContext.Current.Session("PAGESIZE").ToString, Format.font11Blue)
            Dim p1 As New Phrase(c1)
            p1.Add(Environment.NewLine)
            p1.Add(c2)
            p1.Add(Environment.NewLine)
            ' p1.Add(c3)
            'p1.Add(c4)
            Dim cell_ As New PdfPCell(p1)
            cell_.HorizontalAlignment = 2
            cell_.PaddingRight = 21.0F
            cell_.SetLeading(0, 1.4F)
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        ElseIf col = 3 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8))
            cell_.MinimumHeight = 17.0F
            cell_.PaddingLeft = 20.0F
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.SetLeading(0, 1.2F)
            Return cell_
        ElseIf col = 31 Then
            Dim c1 As New Chunk("", Format.font8)
            Dim p1 As New Phrase(c1)
            For i As Integer = 0 To value.Split(",").Length - 1
                Dim c2 As New Chunk("4", Format.fontSymbols)
                Dim c3 As New Chunk(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.Split(",")(i)), Format.font8)
                Dim c4 As New Chunk(Environment.NewLine, Format.font8)
                Dim c5 As New Chunk(" ", Format.font8)
                p1.Add(c2)
                p1.Add(c5)
                p1.Add(c3)
                p1.Add(c4)
            Next
            Dim cell_ As New PdfPCell(New Phrase(p1))
            cell_.MinimumHeight = 17.0F
            cell_.PaddingLeft = 20.0F
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.SetLeading(0, 1.2F)
            Return cell_
        ElseIf col = 4 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8White))
            cell_.HorizontalAlignment = 1
            cell_.MinimumHeight = 16.0F
            cell_.BorderColor = New BaseColor(Format.Green.R, Format.Green.G, Format.Green.B)
            cell_.BackgroundColor = New BaseColor(Format.Green.R, Format.Green.G, Format.Green.B)
            Return cell_
        ElseIf col = 40 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font10White))
            cell_.HorizontalAlignment = 1
            cell_.MinimumHeight = 16.0F
            cell_.BorderColor = New BaseColor(Format.Green.R, Format.Green.G, Format.Green.B)
            cell_.BackgroundColor = New BaseColor(Format.Green.R, Format.Green.G, Format.Green.B)
            Return cell_
        ElseIf col = 5 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8))
            cell_.HorizontalAlignment = 1
            cell_.MinimumHeight = 16.0F
            cell_.BorderColor = New BaseColor(Format.Green.R, Format.Green.G, Format.Green.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        ElseIf col = 6 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8))
            cell_.MinimumHeight = 16.0F
            cell_.PaddingLeft = 5.0F
            cell_.BorderColor = New BaseColor(Format.Green.R, Format.Green.G, Format.Green.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        ElseIf col = 7 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8))
            cell_.MinimumHeight = 16.0F
            cell_.HorizontalAlignment = 1
            cell_.PaddingRight = 5.0F
            cell_.BorderColor = New BaseColor(Format.Green.R, Format.Green.G, Format.Green.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        ElseIf col = 8 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font7))
            cell_.MinimumHeight = 16.0F
            cell_.HorizontalAlignment = 2
            cell_.PaddingLeft = 5.0F
            cell_.BorderColor = New BaseColor(Format.Green.R, Format.Green.G, Format.Green.B)
            cell_.BorderWidth = "0"
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        ElseIf col = 9 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8))
            cell_.MinimumHeight = 16.0F
            cell_.HorizontalAlignment = HorizontalAlign.Center
            cell_.PaddingRight = 25.0F
            cell_.BorderColor = New BaseColor(Format.Green.R, Format.Green.G, Format.Green.B)
            cell_.BorderWidth = "0"
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        ElseIf col = 14 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8italic))
            cell_.MinimumHeight = 16.0F
            cell_.HorizontalAlignment = HorizontalAlign.Right
            cell_.PaddingLeft = 36.0F
            cell_.BorderColor = New BaseColor(Format.Green.R, Format.Green.G, Format.Green.B)
            cell_.BorderWidth = "0"
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        ElseIf col = 1401 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8))
            cell_.MinimumHeight = 16.0F
            cell_.HorizontalAlignment = 3
            '   cell_.PaddingLeft = 43.0F
            cell_.BorderColor = New BaseColor(Format.Green.R, Format.Green.G, Format.Green.B)
            cell_.BorderWidth = "0"
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            Return cell_
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function getCellSummary(ByVal value As String, ByVal col As Integer) As PdfPCell
        If col = 1 Then
            Dim p As New Phrase(value, Format.font12black)
            Dim cell_ As New PdfPCell(p)
            cell_.MinimumHeight = 25
            cell_.BorderWidth = 0
            cell_.BorderColor = New BaseColor(193, 193, 193)
            'cell_.BorderWidthLeft = 1
            cell_.BorderWidthTop = 1
            cell_.BackgroundColor = New BaseColor(255, 255, 255)
            Return cell_
        ElseIf col = 11 Then
            Dim p As New Phrase(value, Format.font12black)
            Dim cell_ As New PdfPCell(p)
            cell_.MinimumHeight = 25
            cell_.BorderWidth = 0
            cell_.BorderColorTop = New BaseColor(193, 193, 193)
            'cell_.BorderWidthLeft = 1
            'cell_.BorderColorLeft = New BaseColor(226, 35, 26)

            cell_.BorderWidthTop = 1
            cell_.BackgroundColor = New BaseColor(220, 220, 220)
            Return cell_
        ElseIf col = 2 Then
            Dim p As New Phrase(value, Format.font12blackBold)
            Dim cell_ As New PdfPCell(p)
            cell_.MinimumHeight = 25
            cell_.BorderWidth = 0
            cell_.BorderColor = New BaseColor(193, 193, 193)
            cell_.BorderWidthRight = 1
            cell_.BorderWidthTop = 1
            cell_.Colspan = 2
            cell_.HorizontalAlignment = Element.ALIGN_RIGHT
            cell_.BackgroundColor = New BaseColor(255, 255, 255)
            Return cell_
        ElseIf col = 22 Then
            Dim p As New Phrase(value, Format.font12blackBold)
            Dim cell_ As New PdfPCell(p)
            cell_.MinimumHeight = 25
            cell_.BorderWidth = 0
            cell_.BorderColor = New BaseColor(193, 193, 193)
            cell_.BorderWidthRight = 1
            cell_.BorderWidthTop = 1
            cell_.Colspan = 2
            cell_.HorizontalAlignment = Element.ALIGN_RIGHT
            cell_.BackgroundColor = New BaseColor(220, 220, 220)
            Return cell_
        ElseIf col = 3 Then
            Dim p As New Phrase(value, Format.font12black)
            Dim cell_ As New PdfPCell(p)
            cell_.MinimumHeight = 25
            cell_.BorderWidth = 0
            cell_.BorderColor = New BaseColor(193, 193, 193)
            cell_.BorderWidthBottom = 1
            cell_.BorderWidthTop = 1
            cell_.BackgroundColor = New BaseColor(255, 255, 255)
            Return cell_
        ElseIf col = 33 Then
            Dim p As New Phrase(value, Format.font12black)
            Dim cell_ As New PdfPCell(p)
            cell_.MinimumHeight = 25
            cell_.BorderWidth = 0
            cell_.BorderColor = New BaseColor(193, 193, 193)
            cell_.BorderWidthBottom = 1
            cell_.BorderWidthTop = 1
           
            cell_.BackgroundColor = New BaseColor(220, 220, 220)
            Return cell_
        ElseIf col = 4 Then

            Dim p As New Phrase(value, Format.font12blackBold)
            Dim cell_ As New PdfPCell(p)
            cell_.MinimumHeight = 25
            cell_.BorderWidth = 0
            cell_.BorderColor = New BaseColor(193, 193, 193)
            cell_.BorderWidthBottom = 1
            cell_.BorderWidthTop = 1
            cell_.BorderWidthRight = 1
            cell_.Colspan = 2
            cell_.HorizontalAlignment = Element.ALIGN_RIGHT
            cell_.BackgroundColor = New BaseColor(255, 255, 255)
            Return cell_
        ElseIf col = 44 Then

            Dim p As New Phrase(value, Format.font12blackBold)
            Dim cell_ As New PdfPCell(p)
            cell_.MinimumHeight = 25
            cell_.BorderWidth = 0
            cell_.BorderColor = New BaseColor(193, 193, 193)
            cell_.BorderWidthBottom = 1
            cell_.BorderWidthTop = 1
            cell_.BorderWidthRight = 1
            cell_.Colspan = 2
            cell_.HorizontalAlignment = Element.ALIGN_RIGHT
            cell_.BackgroundColor = New BaseColor(220, 220, 220)
            Return cell_
        End If

    End Function
    Public Shared Function getcell3(ByVal value As String, ByVal col As Integer) As PdfPCell
        If col = 1 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font10black))
            cell_.MinimumHeight = 20.0F
            ' cell_.BorderColor = New BaseColor(255, 255, 255)
            cell_.BackgroundColor = New BaseColor(255, 221, 219)

            cell_.BorderWidth = "0"
            cell_.HorizontalAlignment = Element.ALIGN_CENTER
            cell_.VerticalAlignment = Element.ALIGN_MIDDLE
            '  cell_.SetLeading(0, 1.4F)
            Return cell_
        ElseIf col = 250 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font11Blue))
            cell_.MinimumHeight = 17.0F
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BorderWidth = "0"
            cell_.VerticalAlignment = Element.ALIGN_RIGHT
            cell_.SetLeading(0, 1.4F)
            Return cell_
        ElseIf col = 101 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8blue))
            cell_.MinimumHeight = 8.0F
            cell_.BorderColor = New BaseColor(Format.black.R, Format.black.G, Format.black.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BorderWidthTop = "0"
            cell_.BorderWidthRight = "0"
            cell_.BorderWidthLeft = "0"
            cell_.BorderWidthBottom = "0"
            cell_.VerticalAlignment = Element.ALIGN_LEFT
            cell_.SetLeading(0, 1.4F)
            Return cell_
        ElseIf col = 2 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8blue))
            cell_.MinimumHeight = 17.0F
            cell_.BorderColor = New BaseColor(Format.black.R, Format.black.G, Format.black.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BorderWidthTop = "0"
            cell_.BorderWidthRight = "0"
            cell_.BorderWidthLeft = "0"
            cell_.BorderWidthBottom = "0"
            cell_.VerticalAlignment = Element.ALIGN_LEFT
            cell_.SetLeading(0, 1.4F)
            Return cell_
        ElseIf col = 3 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8))
            cell_.MinimumHeight = 17.0F
            cell_.BorderColor = New BaseColor(Format.black.R, Format.black.G, Format.black.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BorderWidthRight = "0"
            cell_.BorderWidthLeft = "0"
            cell_.VerticalAlignment = Element.ALIGN_LEFT
            cell_.SetLeading(0, 1.4F)
            Return cell_
        ElseIf col = 4 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8blue))
            cell_.MinimumHeight = 17.0F
            cell_.BorderColor = New BaseColor(Format.black.R, Format.black.G, Format.black.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BorderWidthRight = "0"
            cell_.BorderWidthLeft = "0"
            cell_.BorderWidthTop = "0"
            cell_.VerticalAlignment = Element.ALIGN_LEFT
            cell_.SetLeading(0, 1.4F)
            Return cell_
        ElseIf col = 5 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8))
            cell_.MinimumHeight = 17.0F
            cell_.BorderColor = New BaseColor(Format.black.R, Format.black.G, Format.black.B)
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BorderWidthRight = "0"
            cell_.BorderWidthLeft = "0"
            cell_.BorderWidthTop = "0"
            cell_.VerticalAlignment = Element.ALIGN_LEFT
            cell_.SetLeading(0, 1.4F)
            Return cell_
        ElseIf col = 6 Then
            Dim cell_ As New PdfPCell(New Phrase(value, Format.font8blue))
            cell_.MinimumHeight = 17.0F
            cell_.BorderColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.PaddingRight = 48.0F
            cell_.BackgroundColor = New BaseColor(Format.White.R, Format.White.G, Format.White.B)
            cell_.BorderWidth = "0"
            cell_.HorizontalAlignment = Element.ALIGN_LEFT
            cell_.SetLeading(0, 1.4F)
            Return cell_
        Else
            Return Nothing
        End If
    End Function
End Class
