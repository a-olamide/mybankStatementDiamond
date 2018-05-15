Imports System.Data
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html





Imports Microsoft.ApplicationBlocks.Data

Public Class PDF
    Public Shared val As DataTable

    'Public Shared akp As AsymmetricKeyParameter
    'Public Shared path As String = System.Web.Hosting.HostingEnvironment.MapPath("~\certificate\Backup.p12")
    'Public Shared password As String = "wallz"
    'Public Shared chain As Org.BouncyCastle.X509.X509Certificate()
    



    Public Shared Function generatePDF(ByVal dt As DataTable, ByVal dt2 As DataTable, ByVal startDate As String, ByVal endDate As String, ByVal Destination As String, ByVal Role As String, ByVal Name As String, ByVal Address As String, ByVal ticketNo As String, Optional tdApplicants As String = Nothing) As String()

        Dim dtCompany As DataTable = BLL._selectCompanyName
        Dim companyname As String = dtCompany.Rows(0).Item(1).ToString

        'This must be included in eco build
        'Dim totalDt As Double = Convert.ToDouble(dt.Compute("SUM(DR_AMT)", String.Empty))
        'Dim totalCt As Double = Convert.ToDouble(dt.Compute("SUM(CR_AMT)", String.Empty))

        Try
            System.Web.HttpContext.Current.Session("PAGESIZE") = -1
        Catch
        End Try
        ' Dim pdfWrite As PdfWriter


        
        If (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" + ticketNo + ".pdf"))) Then
            File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" + ticketNo + ".pdf"))
        End If
        

        'If Destination = "0" Or Destination = "" Or Destination.ToLower = "e-mail" Or Destination.ToLower = "corporatem" Then
        '    Dim filepath = ""
        '    If Destination = "0" Or Destination = "" Then
        '        filepath = "PRINTITEMS"
        '        Dim pdf As New GenerateCustomPDF("2457", Address, Name, startDate, endDate, "", dt, dt2, dt2.Rows(0).Item(1), filepath, totalDt, totalCt, Destination, Role, ticketNo)
        '        pdf.generatePrintPDF()
        '    ElseIf Destination.ToLower = "e-mail" Or Destination = "M" Then
        '        filepath = "EMAILITEMS"
        '        Dim pdf As New GenerateCustomPDF("2457", Address, Name, startDate, endDate, "", dt, dt2, dt2.Rows(0).Item("Nuban"), filepath, totalDt, totalCt, Destination, Role, ticketNo)
        '        pdf.generateEmailPDF()
        '    End If
        '    Dim pagecount As Integer


        '    Dim ppath As String = ""

        '    pagecount = CInt(System.Web.HttpContext.Current.Session("PAGESIZE"))



        '    '   Return {CDbl(dt.Rows(0).Item(6)), CDbl(dt.Rows(0).Item(7)), pagecount.ToString()}
        '    Return {CDbl("12345.45").ToString, CDbl("12345.45").ToString, "7"}
        'Else
        Dim pdf As GeneratePDFService = New GeneratePDFService(dt, dt2, startDate, endDate, Destination, Role, Name, Address, ticketNo, Nothing)
        Return pdf.GeneratePDF()
        '  End If




    End Function
    

    
    Private Function getTable3(ByVal dt2 As DataTable, ByVal pagecount As Integer, ByVal destination As Integer) As PdfPTable
        Dim table As PdfPTable = New PdfPTable(2)
        table.TotalWidth = 300.0F
        table.LockedWidth = True
        table.HorizontalAlignment = 1
        Dim widths() As Single = {2.0F, 3.0F}
        table.SetWidths(widths)
        table.AddCell(Format.getcell2("", 4))
        Dim cell_ As New PdfPCell(New Phrase("More Info", Format.TableFontbold))
        cell_.Colspan = 2
        cell_.PaddingLeft = 10.0F
        cell_.FixedHeight = 22.0F
        cell_.BorderColor = New BaseColor(Format.bdrColor.R, Format.bdrColor.G, Format.bdrColor.B)
        cell_.BackgroundColor = New BaseColor(Format.hdrbgColor.R, Format.hdrbgColor.G, Format.hdrbgColor.B)
        table.AddCell(cell_)
        table.AddCell(Format.getcell2("Page Count", 1))
        table.AddCell(Format.getcell2(pagecount, 3))
        table.AddCell(Format.getcell2("Currency", 1))
        table.AddCell(Format.getcell2("NGN", 3))
        table.AddCell(Format.getcell2("Bank Charges", 1))
        table.AddCell(Format.getcell2((50 * pagecount).ToString("#,##0.00"), 3))
        Dim details As String() = BLL._selectDestinationName(destination)
        table.AddCell(Format.getcell2("Destination", 1))
        table.AddCell(Format.getcell2(details(0), 2))
        table.AddCell(Format.getcell2("Destination Charges", 1))
        table.AddCell(Format.getcell2(CDbl(details(1)).ToString("#,##0.00"), 3))
        table.AddCell(Format.getcell2("Total Cost", 11))
        table.AddCell(Format.getcell2((CDbl(details(1)) + (50 * pagecount)).ToString("#,##0.00"), 3))
        table.AddCell(Format.getcell2("", 4))
        table.AddCell(Format.getcell2("Name", 1))
        table.AddCell(Format.getcell2("", 2))
        table.AddCell(Format.getcell2("Signature", 1))
        table.AddCell(Format.getcell2("", 2))
        table.AddCell(Format.getcell2("Date", 1))
        table.AddCell(Format.getcell2("", 2))
        Return table
    End Function
    
  
    
    Public Shared Function Renamefile(filepath As String) As String
        Try
            Dim ini As String = filepath
            Dim dup As String = ini.Split("_")(0) & ".pdf"
            File.Copy(ini, dup)
            Return dup
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Sub AddWaterMark(ByVal ticketNo As String)
        Dim InputFile As String = System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" & ticketNo & ".pdf")
        Dim OutputFile As String = System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" & ticketNo & "_wm.pdf")

        Using input As FileStream = New FileStream(InputFile, FileMode.Open, FileAccess.Read, FileShare.Read)
            Using output As Stream = New FileStream(OutputFile, FileMode.Create, FileAccess.Write, FileShare.None)
                Dim reader As New PdfReader(input)
                Dim pstamp As New PdfStamper(reader, output)
                For pageindex = 1 To reader.NumberOfPages

                    pstamp.FormFlattening = False
                    Dim pageRectangle As iTextSharp.text.Rectangle = reader.GetPageSizeWithRotation(pageindex)
                    Dim pdfData As PdfContentByte = pstamp.GetUnderContent(pageindex)
                    pdfData.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 80)
                    Dim graphicsState As PdfGState = New PdfGState()
                    graphicsState.FillOpacity = 0.8F
                    pdfData.SetGState(graphicsState)
                    pdfData.BeginText()

                    pdfData.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "For Preview only", pageRectangle.Width / 2, pageRectangle.Height / 2, 45.0F)

                    pdfData.EndText()
                Next
                pstamp.Close()
                reader.Close()
            End Using
        End Using
    End Sub
    
    
    Public Shared Function getTicket(ByVal nuban As String, startdate As String, enddate As String, ID As String, stype As String, ByVal dt2 As DataTable, ByVal applicant As String) As String()
        Try
            ' bSearch_Click(sender, e)
            'stype1 = stype

            Dim dt As DataTable = BLL._selectTransactionsTest(CDate(startdate), CDate(enddate), nuban)
            ' Dim dt As DataTable = DAL_API.TransactionDetails(nuban, CDate(startdate).ToString("dd-MMM-yyyy"), CDate(enddate).ToString("dd-MMM-yyyy"))
            'Dim dt As DataTable = BLL._selectAccount(Trim(txtAccountSearch.Text), Session("USERID"), Session("NAME"), 0, Session("BranchName"))

            Dim result As String() = Nothing
            Dim res As String() = PDF.generatePDF(dt, dt2, startdate, enddate, stype, "", dt2.Rows(0).Item(7), dt2.Rows(0).Item(12), ID, applicant)

            Return res
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
