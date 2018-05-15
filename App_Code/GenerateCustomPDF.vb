Imports Microsoft.VisualBasic
Imports System.Data
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports System.Globalization
Imports System.Web.UI.DataVisualization.Charting
Imports Microsoft.ApplicationBlocks.Data
Imports iTextSharp.text.pdf.richmedia
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Pkcs

Public Class GenerateCustomPDF
    Private RCNO As String
    Private address As String
    Private Name As String
    Private StartDate As String
    Private EndDate As String
    Private types As String
    Private dt As DataTable
    Private dt1 As DataTable
    Private nuban As String
    Private fpath As String
    Private totalDt As Double
    Private totalCt As Double
    Private destination As String
    Private role As String
    Private ticketNo As String

    Private val1, dtchart, dtsignatory As New DataTable
    Dim pdfWrite As PdfWriter = Nothing
    Dim doc As Document = New Document(PageSize.A4, 16, 16, 90, 155)
    'HttpContext.Current.Session("PAGESIZE") = 0
    Public Shared akp As AsymmetricKeyParameter
    Public Shared path As String = System.Web.Hosting.HostingEnvironment.MapPath("~\certificate\Backup.p12")
    Public Shared password As String = "wallz"
    Public Shared chain As Org.BouncyCastle.X509.X509Certificate()

    Public Sub New(ByVal PRCNO As String, ByVal Paddress As String, ByVal PName As String,
                                 ByVal PStartDate As String, ByVal PEndDate As String, ByVal Ptypes As String,
                                 ByVal Pdt As DataTable, Pdt1 As DataTable, Pnuban As String, Pfpath As String,
                                 PtotalDt As Double, PtotalCt As Double, Pdestination As String, Prole As String, PticketNo As String)

        RCNO = PRCNO
        address = Paddress
        Name = PName
        StartDate = PStartDate
        EndDate = PEndDate
        types = Ptypes
        dt = Pdt
        dt1 = Pdt1
        nuban = Pnuban
        fpath = Pfpath
        totalCt = PtotalCt
        totalDt = PtotalDt
        destination = Pdestination
        role = Prole
        ticketNo = PticketNo

    End Sub

    Public Function generateEmailPDF() As String()
        Try
            'this  part is needed when you want to include the add water part part
            'pdfWrite = PdfWriter.GetInstance(doc, New FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & nuban & "_enc.pdf"), FileMode.Create))
            pdfWrite = PdfWriter.GetInstance(doc, New FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & nuban & "_enc.pdf"), FileMode.Create))

            pdfWrite.SetPdfVersion(PdfWriter.PDF_VERSION_1_7)
            pdfWrite.AddDeveloperExtension(PdfDeveloperExtension.ADOBE_1_7_EXTENSIONLEVEL3)

            Dim ev As New itsEvents

            ev.Pdestination = destination
            ev.dtTransaction = Nothing
            ev.dtAccount = dt1
            ev.PendDate = EndDate
            ev.PstartDate = StartDate
            ev.PtotalCt = totalCt
            ev.PtotalDt = totalDt
            ev.ticketNoHeader = ticketNo
            pdfWrite.PageEvent = ev

            doc.AddAuthor("Diamond Bank PLC")
            doc.AddTitle("Bank Statement for " & CultureInfo.CurrentCulture.TextInfo.ToTitleCase(HttpUtility.HtmlDecode(Name)) & " , Diamond Bank PLC")
            doc.AddCreator("mybankStatement® xtra")
            doc.AddSubject("Bank Statement for Diamond Bank PLC")
            doc.AddKeywords("")
            doc.Open()

            doc.Add(New Paragraph("Hello " & CultureInfo.CurrentCulture.TextInfo.ToTitleCase(HttpUtility.HtmlDecode(Name)) & ", ", Format.font12black))
            doc.Add(New Paragraph("Here is your statement for the specified period", Format.TableFont))



            'Dim logo1 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\HorizontalLine.png"))
            'logo1.ScalePercent(37)
            'If Len(address) + Len(Name) < 100 Then

            '    logo1.SetAbsolutePosition(40, 670)
            'Else

            '    logo1.SetAbsolutePosition(40, 660)
            'End If
            '' doc.Add(logo1)

            'doc.Add(getAccountOverview2())

            'doc.Add(New Paragraph(" ", Format.font7))
            'doc.Add(New Paragraph(" ", Format.font7))
            'Dim logo10 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\HorizontalLine.png"))
            'logo10.ScalePercent(45)
            '' logo10.SetAbsolutePosition(40, 1000)
            'logo10.Alignment = 1


            'doc.Add(getAccountOverview3())
            'doc.NewPage()

            'doc.Add(New Paragraph(" ", Format.Font_1))


            'Dim day As String = DateTime.Now.ToString("dd/MM/yyyy")
            'Dim media As New RichMediaAnnotation(pdfWrite, New Rectangle(80, 300, 550, 550))

            'Dim fs As PdfFileSpecification = PdfFileSpecification.FileEmbedded(pdfWrite, System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\s.swf"), "s.swf", Nothing)
            'Dim asset As PdfIndirectReference = media.AddAsset("s.swf", fs)
            'Dim configuration As New RichMediaConfiguration(PdfName.FLASH)
            'Dim instance As New RichMediaInstance(PdfName.FLASH)
            'Dim flashVars As New RichMediaParams()
            'Dim vars As [String] = day
            ''--------------------
            'flashVars.FlashVars = vars
            'instance.Params = flashVars
            'instance.Asset = asset
            'configuration.AddInstance(instance)

            ''--------------------
            'Dim configurationRef As PdfIndirectReference = media.AddConfiguration(configuration)
            'Dim activation As New RichMediaActivation()
            'activation.Configuration = configurationRef
            'media.Activation = activation
            ''--------------------
            'Dim richMediaAnnotation As PdfAnnotation = media.CreateAnnotation()

            'richMediaAnnotation.Flags = PdfAnnotation.FLAGS_PRINT

            'pdfWrite.AddAnnotation(richMediaAnnotation)
            'Dim fs As PdfFileSpecification = PdfFileSpecification.FileEmbedded(pdfWrite, System.Web.Hosting.HostingEnvironment.MapPath("~\images\pdfSound.mp4"), "bailey.mpg", Nothing)
            ''create and add a movie annotation to PDF document
            'pdfWrite.AddAnnotation(PdfAnnotation.CreateScreen(pdfWrite, New Rectangle(80, 300, 550, 550), "Bailey", fs, "video/mp4", True))





            ' Dim ch As New Charts

            'Dim grp As New FusionChart()


            'Dim media2 As New RichMediaAnnotation(pdfWrite, New Rectangle(80, 300, 550, 550))

            'Dim fs2 As PdfFileSpecification = PdfFileSpecification.FileEmbedded(pdfWrite, System.Web.Hosting.HostingEnvironment.MapPath("~\FusionCharts\FCF_Column3D.swf"), "s2.swf", Nothing)
            'Dim asset2 As PdfIndirectReference = media.AddAsset("s2.swf", fs)
            'Dim configuration2 As New RichMediaConfiguration(PdfName.FLASH)
            'Dim instance2 As New RichMediaInstance(PdfName.FLASH)
            'Dim flashVars2 As New RichMediaParams()
            'Dim vars2 As [String] = grp.createGraph()
            ''--------------------
            'flashVars2.FlashVars = vars2
            'instance2.Params = flashVars2
            'instance2.Asset = asset2
            'configuration2.AddInstance(instance2)

            ''--------------------
            'Dim configurationRef2 As PdfIndirectReference = media2.AddConfiguration(configuration2)
            'Dim activation2 As New RichMediaActivation()
            'activation2.Configuration = configurationRef2
            'media2.Activation = activation2
            ''--------------------
            'Dim richMediaAnnotation2 As PdfAnnotation = media2.CreateAnnotation()

            'richMediaAnnotation2.Flags = PdfAnnotation.FLAGS_PRINT

            'pdfWrite.AddAnnotation(richMediaAnnotation2)

            'Dim logo11 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\horizontallinespace.png"))
            'logo11.ScalePercent(45)
            'logo11.Alignment = 1

            'doc.Add(New Paragraph(" ", Format.font7))
            'doc.Add(New Paragraph(" ", Format.font7))
            'doc.Add(logo10)
            'doc.Add(New Paragraph(" ", Format.font7))

            'doc.Add(getAccountOverview2statement())
            doc.Add(New Paragraph(" ", Format.font7))
            doc.Add(New Paragraph(" ", Format.font7))
            doc.Add(getAccountDetailsTable())

            doc.Add(New Paragraph(" ", Format.font7))
            doc.Add(New Paragraph(" ", Format.font7))
            doc.Add(getAccountSummaryTable())
            doc.Add(New Paragraph(" ", Format.font7))
            doc.Add(New Paragraph(" ", Format.font7))
            Dim logo2 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\advert.png"))

            logo2.ScalePercent(70)
            logo2.Alignment = 1
            ' logo2.SetAbsolutePosition(40, 120)
            doc.Add(logo2)
            Dim vertWhite = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\whiteBgVert.png"))
            vertWhite.ScalePercent(67)
            vertWhite.BackgroundColor = New BaseColor(255, 255, 255)

            vertWhite.SetAbsolutePosition(37.5, 508.5)
            pdfWrite.DirectContent.AddImage(vertWhite)


            Dim HorWhite = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\whiteBgHor.png"))
            HorWhite.ScalePercent(67)
            HorWhite.BackgroundColor = New BaseColor(255, 255, 255)

            HorWhite.SetAbsolutePosition(39, 510)
            pdfWrite.DirectContent.AddImage(HorWhite)
            'doc.Add(vertWhite)
            doc.Add(getAccountOverview3statement(dt))
            doc.Add(New Paragraph(" ", Format.font7))

            doc.Add(New Paragraph(" ", Format.font7))

            Dim chart1 As New Chart

            chart1 = Loadchart(GeneratescriptWithdrawal(dt), "debit")
            Dim chartimage As iTextSharp.text.Image
            Dim chart2 As New Chart

            chart2 = Loadchart(GeneratescriptDeposit(dt), "credit")
            Dim chartimage2 As iTextSharp.text.Image
            Dim ptextchart As New PdfPTable(1)
            Dim pcharta As New PdfPTable(2)
            ptextchart.TotalWidth = 510.0F
            ptextchart.LockedWidth = True

            ptextchart.WidthPercentage = 100.0F


            pcharta.TotalWidth = 600.0F
            pcharta.LockedWidth = True

            ' pcharta.WidthPercentage = 100.0F
            Using Stream As New MemoryStream
                chart1.SaveImage(Stream, ChartImageFormat.Png)
                chartimage = iTextSharp.text.Image.GetInstance(Stream.GetBuffer())
                chartimage.ScalePercent(75.0F)
            End Using
            Using Stream2 As New MemoryStream
                chart2.SaveImage(Stream2, ChartImageFormat.Png)
                chartimage2 = iTextSharp.text.Image.GetInstance(Stream2.GetBuffer())
                chartimage2.ScalePercent(75.0F)
            End Using
            doc.NewPage()
            If dt.Rows.Count > 1 And dtchart.Rows.Count <> 0 Then
                Dim ptext As New PdfPCell(getthetext())
                Dim pchart As New PdfPCell(chartimage)
                Dim pchart2 As New PdfPCell(chartimage2)
                ptext.BorderWidth = "0"
                pchart.BorderWidth = "0"
                pchart.HorizontalAlignment = 1
                pchart2.BorderWidth = "0"
                pchart2.HorizontalAlignment = 1
                ptext.HorizontalAlignment = 1
                ptextchart.AddCell(ptext)
                pcharta.AddCell(pchart)
                pcharta.AddCell(pchart2)

                doc.Add(ptextchart)
                doc.Add(pcharta)
            End If

            doc.Close()
            pdfWrite.Close()
            'Dim InputFile As String = ("C:\inetpub\wwwroot\mybankStatement_UBA\EMAILITEMS\4077336701.pdf")
            'Using input As Stream = New FileStream(InputFile, FileMode.Open, FileAccess.Read, FileShare.Read)

            '    Dim reader As New PdfReader(input)
            '    Dim sz As Rectangle = reader.GetPageSize(1)
            '    Dim wd As Int16 = sz.Width
            '    Dim ht As Int16 = sz.Height

            'End Using
            'uncomment this tto include watermark
            '  If (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & nuban & ".pdf"))) Then
            'File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & nuban & ".pdf"))
            '  End If
            '  AddUBAwatermark(System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & nuban & "_enc.pdf"), System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & nuban & ".pdf"))
            My.Computer.FileSystem.CopyFile(System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & nuban & "_enc.pdf"),
             System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" + ticketNo.ToString() + ".pdf"))

            processCert(dt1.Rows(0).Item("NUBAN"), "1234")
            Return {HttpContext.Current.Session("PAGESIZE"), fpath & "\" & nuban & "_enc.pdf"}
        Catch ex As Exception

            Return Nothing
        Finally
            pdfWrite.Close()

        End Try
    End Function


    Public Function generatePrintPDF() As String()
        Try
            pdfWrite = PdfWriter.GetInstance(doc, New FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & HttpContext.Current.Session("REQUESTID") & "_enc.pdf"), FileMode.Create))
            Dim ev As New itsEvents
            ev.Pdestination = destination
            ev.dtTransaction = dt
            ev.dtAccount = dt1
            ev.PendDate = EndDate
            ev.PstartDate = StartDate
            ev.PtotalCt = totalCt
            ev.PtotalDt = totalDt
            ev.ticketNoHeader = HttpContext.Current.Session("REQUESTID")
            pdfWrite.PageEvent = ev

            doc.AddAuthor("Diamond Bank Plc")
            doc.AddTitle("Bank Statement for " & CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Name) & " , Diamond Bank Plc")
            doc.AddCreator("mybankStatement® xtra")
            doc.AddSubject("Bank Statement for Diamond Bank PLC")
            doc.AddKeywords("")
            doc.Open()

            doc.Add(New Paragraph(" ", Format.Font_1))

            Dim logo1 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\HorizontalLine.png"))
            logo1.ScalePercent(37)
            If Len(address) + Len(Name) < 100 Then

                logo1.SetAbsolutePosition(40, 670)
            Else

                logo1.SetAbsolutePosition(40, 660)
            End If
            ' doc.Add(logo1)

            doc.Add(getAccountOverview2())

            doc.Add(New Paragraph(" ", Format.font7))
            doc.Add(New Paragraph(" ", Format.font7))
            Dim logo10 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\HorizontalLine.png"))
            logo10.ScalePercent(45)

            logo10.Alignment = 1


            doc.Add(getAccountOverview3())
            'doc.NewPage()
            Dim logo2 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\Front.png"))

            logo2.ScalePercent(60)
            ' logo2.SetAbsolutePosition(40, 120)

            ' doc.Add(New Paragraph(" ", Format.Font_1))
            Dim logo11 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\horizontallinespace.png"))
            logo11.ScalePercent(45)
            logo11.Alignment = 1

            doc.Add(New Paragraph(" ", Format.font7))
            doc.Add(New Paragraph(" ", Format.font7))
            doc.Add(logo10)
            doc.Add(New Paragraph(" ", Format.font7))
            ' doc.Add(New Paragraph(" ", Format.font7))
            Dim logo111 = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\horizontallinespace.png"))
            doc.Add(getAccountOverview2statement())
            logo111.ScalePercent(45)
            doc.Add(New Paragraph(" ", Format.font7))
            '  doc.Add(New Paragraph(" ", Format.font7))
            doc.Add(logo11)

            doc.Add(New Paragraph(" ", Format.font7))
            doc.Add(New Paragraph(" ", Format.font7))

            doc.Add(getAccountOverview3statement(dt))
            doc.Add(New Paragraph(" ", Format.font7))
            'doc.NewPage()
            ' doc.Add(getthetext())
            doc.Add(New Paragraph(" ", Format.font7))

            'Dim chart1 As New Chart

            'chart1 = Loadchart(Generatescript(dt), "withdrawal")
            'Dim chartimage As iTextSharp.text.Image
            'Dim ptextchart As New PdfPTable(1)

            'Dim pcharta As New PdfPTable(2)
            'ptextchart.TotalWidth = 510.0F
            'ptextchart.LockedWidth = True

            'ptextchart.WidthPercentage = 100.0F


            'pcharta.TotalWidth = 600.0F
            'pcharta.LockedWidth = True
            ''    Dim widthschart1() As Single = {6.0F}
            ''   pcharta.SetWidths(widthschart1)
            'pcharta.WidthPercentage = 100.0F
            'Using Stream As New MemoryStream


            '    chart1.SaveImage(Stream, ChartImageFormat.Png)
            '    chartimage = iTextSharp.text.Image.GetInstance(Stream.GetBuffer())
            '    chartimage.ScalePercent(75.0F)
            'End Using

            'doc.NewPage()
            'If dt.Rows.Count > 1 And Len(nuban) = 10 And dtchart.Rows.Count <> 0 Then


            '    Dim ptext As New PdfPCell(getthetext())
            '    Dim pchart As New PdfPCell(chartimage)
            '    ptext.BorderWidth = "0"
            '    pchart.BorderWidth = "0"
            '    pchart.HorizontalAlignment = 1
            '    ptext.HorizontalAlignment = 1
            '    ptextchart.AddCell(ptext)
            '    pcharta.AddCell(pchart)
            '    pcharta.AddCell(pchart)

            '    doc.Add(ptextchart)
            '    doc.Add(pcharta)
            'End If

            doc.Close()
            pdfWrite.Close()

            If (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & HttpContext.Current.Session("REQUESTID") & ".pdf"))) Then
                File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & HttpContext.Current.Session("REQUESTID") & ".pdf"))
            End If
            AddUBAwatermark(System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & HttpContext.Current.Session("REQUESTID") & "_enc.pdf"), System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & HttpContext.Current.Session("REQUESTID") & ".pdf"))
            My.Computer.FileSystem.CopyFile(System.Web.Hosting.HostingEnvironment.MapPath("~\" & fpath & "\" & HttpContext.Current.Session("REQUESTID") & ".pdf"),
             System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" + HttpContext.Current.Session("REQUESTID").ToString() + ".pdf"))

            Return {HttpContext.Current.Session("PAGESIZE"), fpath & "\" & HttpContext.Current.Session("REQUESTID") & "_enc.pdf"}

        Catch ex As Exception
            Return Nothing
        Finally
            pdfWrite.Close()

        End Try
    End Function

    Private Function getAccountDetailsTable() As PdfPTable
        Dim _dt As New DataTable
        'k.UserName = ConfigurationManager.AppSettings("Username").ToString() : k.Password = ConfigurationManager.AppSettings("Password").ToString()

        Dim table As PdfPTable = New PdfPTable(5)
        table.TotalWidth = 510.0F
        Dim widths() As Single = {2.0F, 2.0F, 2.0F, 2.0F, 2.0F}
        table.SetWidths(widths)
        table.LockedWidth = True
        table.SpacingAfter = 0
        table.SpacingBefore = 0
        table.KeepRowsTogether(5)
        Dim cell1 As New PdfPCell()
        ' Dim cellImage As New PdfPCell()
        Dim cell2 As New PdfPCell()
        Dim img1 As Image = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\FoldSquare.png"))
        img1.Alignment = iTextSharp.text.Image.ALIGN_LEFT
        img1.ScaleToFit(30.0F, 30.0F)

        Dim imgCell1 As New iTextSharp.text.pdf.PdfPCell(img1)
        imgCell1.HorizontalAlignment = Element.ALIGN_LEFT
        imgCell1.BackgroundColor = New BaseColor(226, 35, 26)
        imgCell1.Border = iTextSharp.text.Rectangle.NO_BORDER

        cell2.BackgroundColor = New BaseColor(226, 35, 26)
        cell1.Rowspan = 4
        cell1.Colspan = 2
        cell1.BorderWidth = 0
        cell2.Border = 0
        '' cell1.BorderWidth = 2

        'cell1.BorderWidthLeft = 2
        'cell1.BorderWidthBottom = 2


        'cell1.BorderColorRight = New BaseColor(255, 255, 255)
        cell1.BackgroundColor = New BaseColor(226, 35, 26)


        'cell1.BorderColorBottom = New BaseColor(226, 35, 26)
        'cell1.BorderColorLeft = New BaseColor(255, 255, 255)

        Dim p1 As New Paragraph("Account No:", Format.font12Whitei)
        p1.Alignment = 1
        Dim p2 As New Paragraph(Utility.reformatnuban(dt1.Rows(0).Item("NUBAN")), Format.font30White)
        p2.Alignment = 1

        cell1.AddElement(p1)
        cell1.AddElement(p2)

        cell1.VerticalAlignment = Element.ALIGN_CENTER
        cell1.VerticalAlignment = Element.ALIGN_MIDDLE
        cell1.BorderWidth = 0
        table.AddCell(cell1)


        table.AddCell(Format.getCellSummary("Account Type:", 1))
        table.AddCell(Format.getCellSummary(dt1.Rows(0).Item("TYPE"), 2))
        table.AddCell(Format.getCellSummary("Currency:", 11))
        table.AddCell(Format.getCellSummary(dt1.Rows(0).Item("Currency"), 22))
        table.AddCell(Format.getCellSummary("Debit:", 1))
        table.AddCell(Format.getCellSummary(CDbl(totalDt).ToString("#,##0.00"), 2))
        table.AddCell(Format.getCellSummary("Credit:", 11))
        table.AddCell(Format.getCellSummary(CDbl(totalDt).ToString("#,##0.00"), 22))
        table.AddCell(imgCell1)
        table.AddCell(cell2)
        table.AddCell(Format.getCellSummary("Balance:", 3))
        table.AddCell(Format.getCellSummary(CDbl(dt1.Rows(0).Item("AVL_BAL")).ToString("#,##0.00"), 4))
        Return table
    End Function
    Private Function getAccountSummaryTable() As PdfPTable
        Dim _dt As New DataTable
        'k.UserName = ConfigurationManager.AppSettings("Username").ToString() : k.Password = ConfigurationManager.AppSettings("Password").ToString()

        Dim table As PdfPTable = New PdfPTable(3)
        table.TotalWidth = 510.0F
        Dim widths() As Single = {1.0F, 50.0F, 50.0F}
        table.SetWidths(widths)
        table.LockedWidth = True
        Dim cell1 As New PdfPCell()

        cell1.Rowspan = 4
        cell1.BorderWidth = 1
        cell1.BorderWidthRight = 1
        cell1.BorderColor = New BaseColor(226, 35, 26)
        cell1.BackgroundColor = New BaseColor(226, 35, 26)
        Dim p1 As New Paragraph("", Format.font12Whitei)
        cell1.AddElement(p1)
        ' cell1.BorderWidth = 0
        table.AddCell(cell1)


        table.AddCell(Format.getCellSummary("Debit Turnover:", 1))
        table.AddCell(Format.getCellSummary(CDbl(totalDt).ToString("#,##0.00"), 2))
        table.AddCell(Format.getCellSummary("Credit Turnover:", 11))
        table.AddCell(Format.getCellSummary(CDbl(totalCt).ToString("#,##0.00"), 22))
        table.AddCell(Format.getCellSummary("Balance (Uncleared):", 1))
        table.AddCell(Format.getCellSummary(CDbl(dt1.Rows(0).Item("CLR_BAL")).ToString("#,##0.00"), 2))
        table.AddCell(Format.getCellSummary("Balance (Available):", 33))
        table.AddCell(Format.getCellSummary(CDbl(dt1.Rows(0).Item("AVL_BAL")).ToString("#,##0.00"), 44))
        Return table
    End Function
    Private Function getAccountOverview2statement() As PdfPTable
        Try
            'Dim hb As New HBCeStatementAPISoapClient
            'Dim k As New AuthHeader
            ''   adpt.Fill(ds)

            Dim _dt As New DataTable
            'k.UserName = ConfigurationManager.AppSettings("Username").ToString() : k.Password = ConfigurationManager.AppSettings("Password").ToString()

            Dim table As PdfPTable = New PdfPTable(5)
            table.TotalWidth = 550.0F
            Dim widths() As Single = {4.6F, 8.5F, 1.0F, 5.5F, 3.5F}
            table.SetWidths(widths)
            table.LockedWidth = True
            '  Dim dt1 As DataTable



            '    table.HorizontalAlignment = 0
            Dim j As String() = types.Split(",").Distinct().ToArray()
            'Dim text_ As String = ""
            'For i As Integer = 0 To j.Length - 1
            '    text_ += j(i).ToLower & Environment.NewLine
            'Next
            Dim j1 As String = ""
            For i As Integer = 0 To j.Length - 1
                j1 += j(i).ToLower & ","
            Next
            j1 = j1.Substring(0, j1.Length - 1)
            table.AddCell(Format.getcell2_("Name", 14))
            table.AddCell(Format.getcell2_(dt1.Rows(0).Item("Name"), 1401))
            table.AddCell(Format.getcell2_("", 3))
            table.AddCell(Format.getcell2_("Debit Turnover", 14))
            table.AddCell(Format.getcell2_(totalDt.ToString("#,###.00"), 9))

            table.AddCell(Format.getcell2_("Account Number ", 14))
            table.AddCell(Format.getcell2_(Utility.reformatnuban(nuban), 1401))
            table.AddCell(Format.getcell2_("", 3))
            table.AddCell(Format.getcell2_("Credit Turnover", 14))
            table.AddCell(Format.getcell2_(totalCt.ToString("#,###.00"), 9))

            table.AddCell(Format.getcell2_("Currency       ", 14))
            table.AddCell(Format.getcell2_(dt1.Rows(0).Item(11), 1401))
            table.AddCell(Format.getcell2_("", 3))
            table.AddCell(Format.getcell2_("Balance (uncleared)", 14))
            table.AddCell(Format.getcell2_(CDbl(0).ToString("#,###.00"), 9))

            table.AddCell(Format.getcell2_("", 14))
            table.AddCell(Format.getcell2_("", 9))
            table.AddCell(Format.getcell2_("", 3))
            table.AddCell(Format.getcell2_("Balance (available)", 14))
            table.AddCell(Format.getcell2_(CDbl(dt1.Rows(0).Item("AVL_BAL")).ToString("#,###.00"), 9))
            '  CDbl(CDbl(Session("cr")).ToString("#,###.00") - CDbl(Session("dr")).ToString("#,###.00")).ToString("#,###.00")
            Return table

        Catch ex As Exception

        End Try
    End Function

    Private Function getAccountOverview3() As PdfPTable
        Try
            Dim table As PdfPTable = New PdfPTable(6)
            table.TotalWidth = 510.0F
            table.LockedWidth = True
            table.HorizontalAlignment = 1
            Dim widths() As Single = {3.0F, 6.4F, 2.0F, 3.0F, 3.0F, 3.0F}
            table.SetWidths(widths)
            table.AddCell(Format.getcell2_("ACCOUNT NO", 4))
            table.AddCell(Format.getcell2_("ACCOUNT TYPE", 4))
            table.AddCell(Format.getcell2_("CURRENCY", 4))
            table.AddCell(Format.getcell2_("DEBIT", 4))
            table.AddCell(Format.getcell2_("CREDIT", 4))
            table.AddCell(Format.getcell2_("BALANCE", 4))
            ' Dim val As Integer() = {1, 2, 5, 3, 4, 6}

            table.AddCell(Format.getcell2_(dt1.Rows(0).Item(1), 7))
            table.AddCell(Format.getcell2_(dt1.Rows(0).Item(5), 7))
            table.AddCell(Format.getcell2_(dt1.Rows(0).Item(11), 7))
            table.AddCell(Format.getcell2_(totalDt.ToString("#,##0.00"), 7))
            table.AddCell(Format.getcell2_(totalCt.ToString("#,##0.00"), 7))
            table.AddCell(Format.getcell2_(CDbl(dt1.Rows(0).Item(9)).ToString("#,##0.00"), 7))


            Return table

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function getAccountOverview2() As PdfPTable
        Dim table As PdfPTable = New PdfPTable(1)
        table.TotalWidth = 500.0F
        table.LockedWidth = True
        table.HorizontalAlignment = 0

        table.AddCell(Format.getcell2_("Dear " & HttpUtility.HtmlDecode(Name), 3))
        table.AddCell(Format.getcell2_("Here is your statement for the specified period.", 3))

        Return table
    End Function


    Private Function getAccountOverview3statement(dt As DataTable) As PdfPTable
        Try

            Dim table As PdfPTable = New PdfPTable(6)
            table.TotalWidth = 510.0F
            table.LockedWidth = True
            table.HorizontalAlignment = 1
            table.HeaderRows = 1
            Dim headerTable As PdfPTable = New PdfPTable(6)

            Dim cellHead As New PdfPCell(New Phrase("         "))
            cellHead.Colspan = 6
            cellHead.BackgroundColor = New BaseColor(226, 35, 26)
            cellHead.FixedHeight = 5

            cellHead.Border = PdfPCell.NO_BORDER
            Dim cellheader As PdfPCell

            Dim widths() As Single = {2.5F, 2.5F, 6.55F, 2.65F, 2.65F, 2.65F}
            Dim widths2() As Single = {2.5F, 2.5F, 6.55F, 2.65F, 2.65F, 2.65F}
            table.SetWidths(widths)
            headerTable.SetWidths(widths2)
            headerTable.AddCell(cellHead)
            headerTable.AddCell(Format.getcell3("TRANS DATE", 1))
            headerTable.AddCell(Format.getcell3("VALUE DATE", 1))
            headerTable.AddCell(Format.getcell3("NARRATION", 1))
            headerTable.AddCell(Format.getcell3("DEBIT", 1))
            headerTable.AddCell(Format.getcell3("CREDIT", 1))
            headerTable.AddCell(Format.getcell3("BALANCE", 1))
            'headerTable.AddCell(Format.getcell3("", 1))
            'headerTable.AddCell(Format.getcell3("", 1))
            'headerTable.AddCell(Format.getcell3("", 1))
            'headerTable.AddCell(Format.getcell3("", 1))
            'headerTable.AddCell(Format.getcell3("", 1))
            'headerTable.AddCell(Format.getcell3("", 1))
            headerTable.DefaultCell.Border = 1

            cellheader = New PdfPCell(headerTable)
            cellheader.BackgroundColor = New BaseColor(255, 221, 219)
            cellheader.Colspan = 6


            cellheader.Border = PdfPCell.NO_BORDER
            table.AddCell(cellheader)
            'table.AddCell(getcell3(dt.Rows(0).Item(0), 2))
            'table.AddCell(getcell3(dt.Rows(0).Item(1), 2))
            'table.AddCell(getcell3(dt.Rows(0).Item(2), 2))
            'table.AddCell(getcell3(dt.Rows(0).Item(3), 2))
            'table.AddCell(getcell3(dt.Rows(0).Item(4), 2))
            'table.AddCell(getcell3(dt.Rows(0).Item(5), 2))

            For i = 0 To dt.Rows.Count - 1
                For j = 0 To 5
                    If j > 2 Then
                        If i Mod 2 = 0 Then
                            If Not IsDBNull(dt.Rows(i).Item(j)) Then
                                table.AddCell(Format.getcell(CDbl(dt.Rows(i).Item(j)).ToString("#,##0.00"), 4))
                            Else
                                table.AddCell(Format.getcell("", 4))
                            End If
                        Else
                            If Not IsDBNull(dt.Rows(i).Item(j)) Then
                                table.AddCell(Format.getcell(CDbl(dt.Rows(i).Item(j)).ToString("#,##0.00"), 42))
                            Else
                                table.AddCell(Format.getcell("", 4))
                            End If
                        End If

                    ElseIf j = 0 Or j = 1 Then
                        If i Mod 2 = 0 Then
                            If Not IsDBNull(dt.Rows(i).Item(j)) Then
                                table.AddCell(Format.getcell(CDate(dt.Rows(i).Item(j)).ToString("dd-MMM-yyyy"), 2))
                            Else
                                table.AddCell(Format.getcell("", 2))
                            End If
                        Else
                            If Not IsDBNull(dt.Rows(i).Item(j)) Then
                                table.AddCell(Format.getcell(CDate(dt.Rows(i).Item(j)).ToString("dd-MMM-yyyy"), 22))
                            Else
                                table.AddCell(Format.getcell("", 2))
                            End If
                        End If

                    Else
                        If i Mod 2 = 0 Then
                            If Not IsDBNull(dt.Rows(i).Item(j)) Then
                                table.AddCell(Format.getcell(dt.Rows(i).Item(j), 2))
                            Else
                                table.AddCell(Format.getcell("", 2))
                            End If
                        Else
                            If Not IsDBNull(dt.Rows(i).Item(j)) Then
                                table.AddCell(Format.getcell(dt.Rows(i).Item(j), 22))
                            Else
                                table.AddCell(Format.getcell("", 2))
                            End If
                        End If


                    End If
                Next
            Next
            Return table

        Catch ex As Exception

        End Try
    End Function
    Private Shared Function getAccountOverview10(ByVal name As String, ByVal address As String, ByVal enddate As String) As PdfPTable
        Dim table As PdfPTable = New PdfPTable(2)
        table.TotalWidth = 600.0F
        table.LockedWidth = True
        table.HorizontalAlignment = 2
        Dim widths() As Single = {3.0F, 3.0F}
        table.SetWidths(widths)
        table.AddCell(Format.getcell2_(Environment.NewLine & name & Environment.NewLine & address, 50))
        table.AddCell(Format.getcell2_(enddate, 20))
        HttpContext.Current.Session("Pageheader") = table
        Return table
    End Function

    Public Function Generatescript(dt As DataTable) As String
        Try
            Dim ds As New DataSet

            Dim script As String = " create table #table1 (Amount nvarchar(255) COLLATE DATABASE_DEFAULT, narration nvarchar(255) COLLATE DATABASE_DEFAULT)"
            script = script + "  insert into #table1 (amount, narration)  "
            For h = 0 To dt.Rows.Count - 1
                If Not IsDBNull(dt.Rows(h).Item(2)) Then
                    script += " select '" & "a" & "' , '" & dt.Rows(h).Item(2).ToString.Replace("'", "") & "'  union all "
                End If

            Next
            script = script.Substring(0, script.Length - 12)
            Dim hh As String = script
            Return script
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GeneratescriptWithdrawal(dt As DataTable) As String
        Try
            Dim ds As New DataSet

            Dim script As String = " create table #table1 (Amount nvarchar(255) COLLATE DATABASE_DEFAULT, narration nvarchar(255) COLLATE DATABASE_DEFAULT , debit nvarchar(255) COLLATE DATABASE_DEFAULT)"
            script = script + "  insert into #table1 (amount, narration,debit)  "
            For h = 0 To dt.Rows.Count - 1
                If Not IsDBNull(dt.Rows(h).Item(2)) Then
                    script += " select '" & "aaa" & "' , '" & dt.Rows(h).Item(2).ToString.Replace("'", "") & "' , '" & dt.Rows(h).Item(3).ToString.Replace("'", "") & "'  union all "
                End If

            Next
            script = script.Substring(0, script.Length - 12)
            Dim hh As String = script
            Return script
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GeneratescriptDeposit(dt As DataTable) As String
        Try
            Dim ds As New DataSet

            Dim script As String = " create table #table1 (Amount nvarchar(255) COLLATE DATABASE_DEFAULT, narration nvarchar(255) COLLATE DATABASE_DEFAULT, credit nvarchar(255) COLLATE DATABASE_DEFAULT)"
            script = script + "  insert into #table1 (amount, narration,credit)  "
            For h = 0 To dt.Rows.Count - 1
                If Not IsDBNull(dt.Rows(h).Item(2)) Then
                    script += " select '" & "aaa" & "' , '" & dt.Rows(h).Item(2).ToString.Replace("'", "") & "' , '" & dt.Rows(h).Item(4).ToString.Replace("'", "") & "'  union all "
                End If

            Next
            script = script.Substring(0, script.Length - 12)
            Dim hh As String = script
            Return script
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Function Loadchart(script As String, type As String) As Chart
        Try
            Dim chart1 As New Chart
            '   Dim x() As String = New String() {"ATM", "POS", "WEB", "MOBILE", "CASH"}
            '   Dim y() As Integer = New Integer() {200, 112, 55, 96, 23}
            chart1.ChartAreas.Add("ChartArea1")
            chart1.Series.Add("Series1")

            Dim dt As New DataTable
            chart1.Legends.Add("Legend1")
            Dim aaaa As New Legend
            ' chart1.Legends(0).MaximumAutoSize
            'chart1.Legends(0).DockedToChartArea =
            chart1.Legends(0).TitleAlignment = Drawing.StringAlignment.Center
            chart1.Legends(0).Alignment = Drawing.StringAlignment.Center
            dt = getdoc(script, type)
            dtchart = dt
            Dim x(0 To dt.Rows.Count - 1) As String
            Dim y(0 To dt.Rows.Count - 1) As Integer
            Dim ll, la As String
            chart1.Series(0)("PieLabelStyle") = "Disabled"
            For i = 0 To dt.Rows.Count - 1
                ll = dt.Rows(i).Item(0)
                la = dt.Rows(i).Item(1)
                x(i) = dt.Rows(i).Item(0)
                y(i) = dt.Rows(i).Item(1)
            Next

            chart1.Series(0).Points.DataBindXY(x, y)
            Dim colors() As String = {"#d42e12", "#424040", "#424040", "#abbac3", "#394557", "#7ec0ee", "#33FF99", "#58595B", "#0000FF", "#ADD8e6"}
            For h = 0 To dt.Rows.Count - 1
                If dt.Rows(h).Item(0).ToString = "ATM" Then
                    chart1.Series(0).Points(h).Color = System.Drawing.ColorTranslator.FromHtml(colors(1))
                ElseIf dt.Rows(h).Item(0).ToString.Contains("Cash") Then
                    chart1.Series(0).Points(h).Color = System.Drawing.ColorTranslator.FromHtml(colors(7))
                ElseIf dt.Rows(h).Item(0).ToString.Contains("POS") Then
                    chart1.Series(0).Points(h).Color = System.Drawing.ColorTranslator.FromHtml(colors(0))
                ElseIf dt.Rows(h).Item(0).ToString.Contains("Funds") Then
                    chart1.Series(0).Points(h).Color = System.Drawing.ColorTranslator.FromHtml(colors(2))
                ElseIf dt.Rows(h).Item(0).ToString.Contains("Mobile") Then
                    chart1.Series(0).Points(h).Color = System.Drawing.ColorTranslator.FromHtml(colors(3))
                ElseIf dt.Rows(h).Item(0).ToString.Contains("Automated") Then
                    chart1.Series(0).Points(h).Color = System.Drawing.ColorTranslator.FromHtml(colors(1))
                ElseIf dt.Rows(h).Item(0).ToString.Contains("Web") Then
                    chart1.Series(0).Points(h).Color = System.Drawing.ColorTranslator.FromHtml(colors(5))


                ElseIf dt.Rows(h).Item(0).ToString.Contains("Cheque") Then
                    chart1.Series(0).Points(h).Color = System.Drawing.ColorTranslator.FromHtml(colors(4))
                Else
                    chart1.Series(0).Points(h).Color = System.Drawing.ColorTranslator.FromHtml(colors(8))
                End If
            Next
            chart1.Series(0).ChartType = SeriesChartType.Pie

            chart1.Series(0).Label = "#VAL"
            chart1.Series(0).LegendText = "#VALX" + " (" + "#PERCENT{P1}" + ")"
            Dim myresult As String
            myresult = chart1.Series(0).LegendText

            chart1.Series(0).LegendText = Trim(myresult)
            chart1.Series(0).IsValueShownAsLabel = True
            aaaa.Docking = Docking.Bottom


            aaaa.LegendStyle = LegendStyle.Column
            ' aaaa.TableStyle = LegendTableStyle.Auto
            aaaa.Alignment = Drawing.StringAlignment.Center
            aaaa.IsEquallySpacedItems = False
            aaaa.IsTextAutoFit = True
            '    aaaa.TextWrapThreshold = 100
            'chart1.Width = 800


            chart1.Legends(0) = aaaa
            '    chart1.Series(0).Label  = "#PERCENT{P1}", point.YValues[0], point.AxisLabel)
            ' chart1.Series(0).Label = "#PERCENT{P1}"
            ' chart1.Series(0).LegendText = "#VALX"


            chart1.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
            ' chart1.Legends(0).Enabled = True
            Return chart1
        Catch ex As Exception

        End Try
    End Function





    Private Shared Function getdoc(script As String, type As String) As DataTable
        Dim ds As New DataSet
        Dim gdvtest As New GridView
        If type.ToLower = "debit" Then
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "GeneratechartDebit", script, type)

        Else
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "GeneratechartCredit", script, type)

        End If


        Return ds.Tables(0)
    End Function

    Private Function AddUBAwatermark(InputFile As String, OutputFile As String) As String
        'Dim InputFile As String = filename
        'Dim OutputFile As String = RenFile(InputFile)
        Dim input As Stream = New FileStream(InputFile, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim reader As New PdfReader(input)

        Using output As Stream = New FileStream(OutputFile, FileMode.Create, FileAccess.Write, FileShare.None)
            Dim pstamp As New PdfStamper(reader, output)

            '   Using pdfStamper As New PdfStamper(reader, output)

            For pageindex = 2 To reader.NumberOfPages

                pstamp.FormFlattening = False
                Dim pageRectangle As iTextSharp.text.Rectangle = reader.GetPageSizeWithRotation(pageindex)
                Dim pdfData As PdfContentByte = pstamp.GetUnderContent(pageindex)
                pdfData.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10)
                Dim graphicsState As PdfGState = New PdfGState()
                graphicsState.FillOpacity = 0.8F
                pdfData.SetGState(graphicsState)
                pdfData.BeginText()

                Dim Jpeg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\Advert\watermarknew.png"))
                Dim Width As Single = pageRectangle.Width
                Dim Height As Single = pageRectangle.Height
                Jpeg.ScaleToFit(Width, Height)
                '  Jpeg.SetAbsolutePosition(Width / 2 - Jpeg.Width / 2, Height / 2 - Jpeg.Height / 2)

                ' Jpeg.ScalePercent(50)
                Jpeg.Alignment = 1


                Jpeg.SetAbsolutePosition(-10, 100)
                ' Jpeg.Rotation = 45

                pdfData.AddImage(Jpeg)

                pdfData.EndText()
            Next
            pstamp.Close()
            '    End Using
            output.Close()
            output.Dispose()
            input.Close()
            reader.Close()
            File.Delete(InputFile)
            Return OutputFile
        End Using
    End Function

    Private Shared Function getthetext() As PdfPTable
        Try
            Dim table As PdfPTable = New PdfPTable(2)
            table.TotalWidth = 600.0F
            Dim widths() As Single = {4.0F, 4.0F}
            table.SetWidths(widths)
            '  table.LockedWidth = True
            '   table.WidthPercentage = 100.0F
            'If dt.Rows.Count <= 1 Then
            'Else
            table.AddCell(Format.getcell3("The pie chart below shows a breakdown of the withdrawal channels", 6))
            table.AddCell(Format.getcell3("The pie chart below shows a breakdown of the Deposit channels", 6))

            '  End If


            Return table
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function processCert(ByVal nuban As String, ByVal Stmtpasscode As String) As String()
        Dim alias_ As String = ""
        Dim pk12 As Pkcs12Store
        Dim fileData As Byte() = Nothing
        Dim noOfPges As Int16 = Nothing
        'First we'll read the certificate file

        pk12 = New Pkcs12Store(New FileStream(path, FileMode.Open, FileAccess.Read), password.ToCharArray())
        Dim i As String
        For Each i In pk12.Aliases
            If pk12.IsKeyEntry(i) Then
                alias_ = i.ToString
                Exit For
            End If
        Next

        akp = pk12.GetKey(alias_).Key() '''''
        Dim ce As X509CertificateEntry() = pk12.GetCertificateChain(alias_)
        chain = New Org.BouncyCastle.X509.X509Certificate(ce.Length - 1) {}
        For k As Integer = 0 To ce.Length - 1
            chain(k) = ce(k).Certificate()
        Next
        If (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~\EMAILITEMS" & "\" & dt1.Rows(0).Item("NUBAN") & ".pdf"))) Then
            File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~\EMAILITEMS" & "\" & dt1.Rows(0).Item("NUBAN") & ".pdf"))
        End If
        Dim InputFile As String = (System.Web.Hosting.HostingEnvironment.MapPath("~\EMAILITEMS" & "\" & dt1.Rows(0).Item("NUBAN") & "_enc.pdf"))
        Dim OutputFile As String = (System.Web.Hosting.HostingEnvironment.MapPath("~\EMAILITEMS" & "\" & dt1.Rows(0).Item("NUBAN") & ".pdf"))
        Dim passcode As String = Nothing

        passcode = Stmtpasscode

        Try

            ' System.Web.HttpContext.Current.Session("PASSCODE") = passcode
        Catch ex As Exception

        End Try
        Using input As Stream = New FileStream(InputFile, FileMode.Open, FileAccess.Read, FileShare.Read)
            Using output As Stream = New FileStream(OutputFile, FileMode.Create, FileAccess.Write, FileShare.None)
                Dim reader As New PdfReader(input)
                noOfPges = reader.NumberOfPages
                PdfEncryptor.Encrypt(reader, output, True, passcode, passcode, PdfWriter.AllowScreenReaders)
            End Using
        End Using
        'temporary storing of password
        '   BLL._UpdatePws(ticketNo, passcode)
        ' Dim password_ As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(passcode)
        '  System.IO.File.Delete(("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf"))

        Return {passcode, noOfPges}



    End Function
End Class
