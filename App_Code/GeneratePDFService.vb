Imports Microsoft.VisualBasic
Imports System.Data
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.Globalization
Imports System.IO
Imports Org.BouncyCastle.Pkcs
Imports Org.BouncyCastle.Crypto

Public Class GeneratePDFService
    Private dt As DataTable 'Transaction details
    Private dt2 As DataTable    'Acccount detials
    Private startDate As String
    Private endDate As String
    Private Destination As String
    Private Role As String
    Private Name As String
    Private Address As String
    Private ticketNo As String
    Private StatementPasscode As String = Nothing
    Private tdApplicants As String()
    Private countryCode As String

    Public Shared akp As AsymmetricKeyParameter
    Public Shared path As String = System.Web.Hosting.HostingEnvironment.MapPath("~\certificate\Backup.p12")
    Public Shared password As String = "wallz"
    Public Shared chain As Org.BouncyCastle.X509.X509Certificate()
    Public Sub New(ByVal Pdt As DataTable, ByVal Pdt2 As DataTable, ByVal PstartDate As String, ByVal PendDate As String, _
                   ByVal PDestination As String, ByVal PRole As String, ByVal PName As String, ByVal PAddress As String, _
                   ByVal PticketNo As String, ByVal PtdApplicants As String())
        dt = Pdt
        dt2 = Pdt2
        startDate = PstartDate
        endDate = PendDate
        Destination = PDestination
        Role = PRole
        Name = PName
        Address = PAddress
        ticketNo = PticketNo
        tdApplicants = PtdApplicants



    End Sub
    Public Sub New(ByVal Pdt As DataTable, ByVal Pdt2 As DataTable, ByVal PstartDate As String, ByVal PendDate As String, _
                 ByVal PDestination As String, ByVal PRole As String, ByVal PName As String, ByVal PAddress As String, _
                 ByVal PticketNo As String, ByVal PtdApplicants As String(), ByVal PpassCode As String, ByVal PcountryCode As String)
        dt = Pdt
        dt2 = Pdt2
        startDate = PstartDate
        endDate = PendDate
        Destination = PDestination
        Role = PRole
        Name = PName
        Address = PAddress
        ticketNo = PticketNo
        tdApplicants = PtdApplicants
        StatementPasscode = PpassCode
        countryCode = PcountryCode

    End Sub

    Public Function GeneratePDF() As String()

        Dim dtCompany As DataTable = BLL._selectCompanyName
        Dim companyname As String = dtCompany.Rows(0).Item(1).ToString

        'This must be included in eco build
        Dim totalDt As Double = Convert.ToDouble(dt.Compute("SUM(DebitAmount)", String.Empty))
        Dim totalCt As Double = Convert.ToDouble(dt.Compute("SUM(CreditAmount)", String.Empty))

        Try
            System.Web.HttpContext.Current.Session("PAGESIZE") = -1
        Catch
        End Try
        Dim pdfWrite As PdfWriter
        Dim doc As Document = New Document(PageSize.A4, 20, 20, 25, 100)
        If Destination.ToLower = "request" Then
            doc = New Document(PageSize.A4, 20, 20, 25, 100)
            If (File.Exists(("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf"))) Then
                File.Delete(("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf"))
            End If

            pdfWrite = PdfWriter.GetInstance(doc, New FileStream(("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf"), FileMode.Create))

        Else
            If (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" + ticketNo + ".pdf"))) Then
                File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" + ticketNo + ".pdf"))
            End If

            doc = New Document(PageSize.A4, 20, 20, 25, 100)
            If (File.Exists(("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf"))) Then
                File.Delete(("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf"))
            End If
            If (File.Exists(("D:\mybankStatementRepository\statement\" + ticketNo + "N.pdf"))) Then
                File.Delete(("D:\mybankStatementRepository\statement\" + ticketNo + "N.pdf"))
            End If
            pdfWrite = PdfWriter.GetInstance(doc, New FileStream(("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf"), FileMode.Create))

        End If


        'Dim pdfWrite1 As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" + ticketNo + ".pdf"), FileMode.Create))

        Dim ev As New itsEvents
        ev.Pdestination = Destination
        ev.dtTransaction = Nothing
        ev.dtAccount = dt2
        ev.PendDate = endDate
        ev.PstartDate = startDate
        ev.PtotalCt = totalCt
        ev.PtotalDt = totalDt
        ev.ticketNoHeader = ticketNo
        pdfWrite.PageEvent = ev
        'Dim ev1 As New itsEvents
        'pdfWrite1.PageEvent = ev1
        doc.AddAuthor(companyname)
        doc.AddTitle("Bank Statement for " & CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dt2.Rows(0).Item(7)) & ", " & companyname)
        doc.AddCreator("mybankStatement®")
        doc.AddSubject("Bank Statement for " & companyname)
        doc.AddKeywords("Bank Statement")
        doc.Open()

        Dim logo = iTextSharp.text.Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~\images\logo.png"))
        logo.ScalePercent(60)
        logo.SetAbsolutePosition(-15, 750)
        doc.Add(logo)
        doc.Add(New Paragraph(" ", Format.subTitleFont))
        doc.Add(New Paragraph(companyname.ToUpper))
        'Dont forget to put ur RC. No
        doc.Add(New Paragraph(" ", Format.subTitleFont))

        doc.Add(New Paragraph("RC No. " & "2392", Format.subTitleFont))

        doc.Add(New Paragraph(" ", Format.subTitleFont))
        'doc.Add(getTable2(dt2, totalCt, totalDt, startDate, endDate, Destination, Role, Name, Address))

        doc.Add(getTable2(totalDt, totalCt))

        doc.Add(New Paragraph(" ", Format.subTitleFont))
        doc.Add(New Paragraph(" ", Format.subTitleFont))
        ' Dim transactionSummary As New TransactionSummary(BLL._selectTransactionSummary(CDate(startDate), CDate(endDate), dt2.Rows(0).Item(3)))
        Try
            Dim transactionSummary As New TransactionSummary(TransSummary(dt))

            doc.Add(transactionSummary.getTranactionSummary())
            doc.Add(New Paragraph(" ", Format.font7))
            doc.Add(New Paragraph(" ", Format.font7))
        Catch ex As Exception

        End Try

        doc.Add(getTable(dt, Destination))

        doc.Close()
        '  If IsNothing(tdApplicants) Then
        If Destination.ToLower <> "request" Then
            My.Computer.FileSystem.CopyFile(("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf"),
                  System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" + ticketNo + ".pdf"))

        End If

        ' End If
        'This returns passcode as element 1 and no of pages as element 2
        Dim newPasscode As String() = Nothing
        If IsNumeric(Destination) Then
            If CInt(Destination) > 0 Then
                newPasscode = processCert(ticketNo, StatementPasscode)
            End If
        ElseIf Destination.ToLower = "request" Then
            newPasscode = processCert(ticketNo, StatementPasscode)
        End If
        ' Dim pagecount As Integer = newPasscode(1)


        '  Dim ppath As String = ""
        'Try


        '    pagecount = CInt(System.Web.HttpContext.Current.Session("PAGESIZE"))
        'Catch ex As Exception
        '    pagecount = newPasscode(1)
        'End Try


        'Return {CDbl(dt.Rows(0).Item(6)), CDbl(dt.Rows(0).Item(7)), pagecount.ToString(), newPasscode(0), newPasscode(1)}
        Return {totalDt.ToString, totalCt.ToString, newPasscode(1).ToString(), newPasscode(0), newPasscode(1)}

    End Function

    Private Function processCert(ByVal ticketNo As String, ByVal Stmtpasscode As String) As String()
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
        Dim InputFile As String = Nothing
        Dim OutputFile As String = Nothing
        Dim OutputFile1 As String = Nothing
        'If Destination.ToLower = "request" Then
        '    If (File.Exists(("D:\mybankStatementRepository\statement\" + ticketNo + ".pdf"))) Then
        '        File.Delete(("D:\mybankStatementRepository\statement\" + ticketNo + ".pdf"))
        '    End If
        '    InputFile = ("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf")
        '    OutputFile = ("D:\mybankStatementRepository\statement\" + ticketNo & ".pdf")
        'Else
        If (File.Exists(("D:\mybankStatementRepository\statement\" + ticketNo + ".pdf"))) Then
            File.Delete(("D:\mybankStatementRepository\statement\" + ticketNo + ".pdf"))
        End If
        InputFile = ("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf")
        OutputFile = ("D:\mybankStatementRepository\statement\" + ticketNo & ".pdf")
        '  End If

        If (File.Exists(("D:\mybankStatementRepository\statement\" + ticketNo + "N.pdf"))) Then
            File.Delete(("D:\mybankStatementRepository\statement\" + ticketNo + "N.pdf"))
        End If

        OutputFile1 = ("D:\mybankStatementRepository\statement\" + ticketNo & "N.pdf")
        Dim passcode As String = Nothing
        If ((StatementPasscode Is Nothing) Or StatementPasscode = "") Then
            passcode = Utility.Get4RadomPassword()
        Else
            passcode = Stmtpasscode
        End If


        Using input As Stream = New FileStream(InputFile, FileMode.Open, FileAccess.Read, FileShare.Read)
            Using output As Stream = New FileStream(OutputFile1, FileMode.Create, FileAccess.Write, FileShare.None)

                Dim reader As New PdfReader(input)
                noOfPges = reader.NumberOfPages
                'select two pages from the original document
                'reader.SelectPages("1-2")
                'create PdfStamper object to write to get the pages from reader 
                Dim stamper As New PdfStamper(reader, output)
                'PdfContentByte from stamper to add content to the pages over the original content
                Dim pbover As PdfContentByte = stamper.GetOverContent(1)
                'add content to the page using ColumnText
                Dim subTitleFont2 As Font = FontFactory.GetFont("Arial", 8, Font.NORMAL, New BaseColor(119, 136, 153))
                Dim ch As New Chunk("mybankStatement ® | " & Now.Date() & " | " & ticketNo & "-8" & " | " & passcode & " | Page " & "1", subTitleFont2)
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, New Phrase(ch), 20, 820, 0)
                'PdfContentByte from stamper to add content to the pages under the original content
                Dim pbunder As PdfContentByte = stamper.GetUnderContent(1)

                stamper.Close()
                '  PdfEncryptor.Encrypt(reader, output, True, passcode, passcode, PdfWriter.AllowScreenReaders)

            End Using

        End Using

        Using input As Stream = New FileStream(OutputFile1, FileMode.Open, FileAccess.Read, FileShare.Read)
            Using output As Stream = New FileStream(OutputFile, FileMode.Create, FileAccess.Write, FileShare.None)

                Dim reader As New PdfReader(input)
                noOfPges = reader.NumberOfPages
                PdfEncryptor.Encrypt(reader, output, True, passcode, passcode, PdfWriter.AllowScreenReaders)

            End Using

        End Using
        'temporary storing of password
        BLL._UpdatePws(ticketNo, passcode)
        ' Dim password_ As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(passcode)
        'If Destination.ToLower = "request" Then
        '    System.IO.File.Delete(("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf"))
        'Else
        Try
            System.IO.File.Delete(("D:\mybankStatementRepository\statement\" + ticketNo + "_enc.pdf"))

            ' End If
            System.IO.File.Delete(("D:\mybankStatementRepository\statement\" + ticketNo + "N.pdf"))
        Catch ex As Exception

        End Try


        Return {passcode, noOfPges.ToString()}



    End Function

    Private Function getTable2(dr As Double, cr As Double) As PdfPTable
        Dim table As PdfPTable = New PdfPTable(2)
        table.TotalWidth = 300.0F
        table.LockedWidth = True
        table.HorizontalAlignment = 1
        Dim widths() As Single = {2.0F, 3.0F}
        table.SetWidths(widths)
        Dim cell_ As New PdfPCell(New Phrase("Summary Info", Format.TableFontbold))
        cell_.Colspan = 2
        cell_.PaddingLeft = 10.0F
        'cell_.FixedHeight = 17.0F
        cell_.BorderColor = New BaseColor(Format.bdrColor.R, Format.bdrColor.G, Format.bdrColor.B)
        cell_.BackgroundColor = New BaseColor(Format.hdrbgColor.R, Format.hdrbgColor.G, Format.hdrbgColor.B)
        table.AddCell(cell_)
        table.AddCell(Format.getcell2("Role", 1))
        table.AddCell(Format.getcell2(Role, 2))

        table.AddCell(Format.getcell2("Applicant(s)", 11))
        Dim applicants As String = Nothing
        If Destination.ToLower = "request" Then
            If IsNothing(tdApplicants) Or tdApplicants.Count = 0 Then
                applicants = "N/A"
            Else

                For i As Int16 = 0 To tdApplicants.Count - 1
                    If i = tdApplicants.Count - 1 Then applicants += tdApplicants(i) Else applicants += tdApplicants(i) & Environment.NewLine
                Next
            End If


        Else
            Dim dt21 As DataTable = BLL._selectApplicant(ticketNo)

            If dt21.Rows.Count = 0 Then
                applicants = "N/A"
            Else
                For i As Integer = 0 To dt21.Rows.Count - 1
                    If i = dt21.Rows.Count - 1 Then applicants += dt21.Rows(i).Item(1) Else applicants += dt21.Rows(i).Item(1) & Environment.NewLine
                Next
            End If
        End If

        table.AddCell(Format.getcell2(HttpUtility.HtmlDecode(applicants), 21))
        table.AddCell(Format.getcell2("", 4))
        table.AddCell(Format.getcell2("Account name", 1))
        table.AddCell(Format.getcell2(HttpUtility.HtmlDecode(Name), 2))
        table.AddCell(Format.getcell2("Address", 1))
        table.AddCell(Format.getcell2(HttpUtility.HtmlDecode(Address), 2))
        table.AddCell(Format.getcell2("Signatories", 11))
        Dim dt1 As DataTable = BLL._selectSignatoriesNew(ticketNo)
        Dim signatory As String = ""
        Dim bvnToDB As String = ""
        Try

            If dt1 Is Nothing Or dt1.Rows.Count = 0 Then
                signatory = "N/A"
            Else
                For i As Integer = 0 To dt1.Rows.Count - 1
                    If i = dt1.Rows.Count - 1 Then
                        signatory += System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dt1.Rows(i).Item(0).ToString) + " - BVN (" + dt1.Rows(i).Item("BVN") + ") "
                        bvnToDB += dt1.Rows(i).Item(3).ToString
                    Else
                        signatory += System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dt1.Rows(i).Item(0).ToString) + " - BVN (" + dt1.Rows(i).Item("BVN") + ") " & Environment.NewLine
                        bvnToDB += dt1.Rows(i).Item(3).ToString + "|"
                    End If
                Next
            End If

        Catch ex As Exception
            signatory = "N/A"
            bvnToDB = ""
        End Try


        Try
            BLL._updateActivityWithBVN(bvnToDB, ticketNo)
        Catch ex As Exception

        End Try
        table.AddCell(Format.getcell2(HttpUtility.HtmlDecode(signatory), 21))
        table.AddCell(Format.getcell2("Tenor", 1))
        Dim tenor As Integer = ((Year(CDate(endDate)) - Year(CDate(startDate))) * 12) + (Month(CDate(endDate)) - Month(CDate(startDate)))
        table.AddCell(Format.getcell2(tenor.ToString & " Months", 2))
        table.AddCell(Format.getcell2("Period", 1))
        table.AddCell(Format.getcell2(startDate & " to " & endDate, 2))
        table.AddCell(Format.getcell2("Type", 1))
        table.AddCell(Format.getcell2(dt2.Rows(0).Item(8), 2))
        table.AddCell(Format.getcell2("Category", 1))
        table.AddCell(Format.getcell2(dt2.Rows(0).Item(9), 2))
        table.AddCell(Format.getcell2("Account No.", 1))
        table.AddCell(Format.getcell2(dt2.Rows(0).Item(3), 2))
        table.AddCell(Format.getcell2("Currency", 1))
        table.AddCell(Format.getcell2(dt2.Rows(0).Item(23), 3))
        table.AddCell(Format.getcell2("", 4))
        table.AddCell(Format.getcell2("Available Balance", 1))
        table.AddCell(Format.getcell2(CDbl(dt2.Rows(0).Item(11)).ToString("#,##0.00"), 3))
        table.AddCell(Format.getcell2("Book Balance", 1))
        table.AddCell(Format.getcell2(CDbl(dt2.Rows(0).Item(10)).ToString("#,##0.00"), 3))
        table.AddCell(Format.getcell2("Total Debit", 1))
        table.AddCell(Format.getcell2(CDbl(dr).ToString("#,##0.00"), 3))
        table.AddCell(Format.getcell2("Total Credit", 1))
        table.AddCell(Format.getcell2(CDbl(cr).ToString("#,##0.00"), 3))
        Return table
    End Function

    Private Function getTable(ByVal dt As DataTable, ByVal Destination As String) As PdfPTable
        Dim table As PdfPTable = New PdfPTable(6)

        table.TotalWidth = 550.0F
        table.LockedWidth = True
        Dim widths() As Single = {0.8F, 0.8F, 2.5F, 1.3F, 1.3F, 1.3F}
        table.SetWidths(widths)
        Dim headertable As New PdfPTable(6)
        Dim cellheader As PdfPCell
        If Destination = "0" Or Destination = "" Then
            Dim trDt As New PdfPCell(New Phrase("Tran. date", Format.EcoFontbold2))
            trDt.BorderWidth = "0"
            trDt.HorizontalAlignment = 1
            Dim VlDt As New PdfPCell(New Phrase("Value date", Format.EcoFontbold2))
            VlDt.BorderWidth = "0"
            VlDt.HorizontalAlignment = 1
            Dim TranDt As New PdfPCell(New Phrase("Transaction details", Format.EcoFontbold2))
            TranDt.BorderWidth = "0"
            TranDt.HorizontalAlignment = 1
            Dim Dbt1 As New PdfPCell(New Phrase("Debit", Format.EcoFontbold2))
            Dbt1.BorderWidth = "0"
            Dbt1.HorizontalAlignment = 1
            Dim Ct As New PdfPCell(New Phrase("Credit", Format.EcoFontbold2))
            Ct.BorderWidth = "0"
            Ct.HorizontalAlignment = 1
            Dim bal As New PdfPCell(New Phrase("Balance", Format.EcoFontbold2))
            bal.BorderWidth = "0"
            bal.HorizontalAlignment = 1

            headertable.AddCell(trDt)
            headertable.AddCell(VlDt)
            headertable.AddCell(TranDt)
            headertable.AddCell(Dbt1)
            headertable.AddCell(Ct)
            headertable.AddCell(bal)
            headertable.SetWidths(widths)

            table.SetWidths(widths)

            cellheader = New PdfPCell(headertable)
            cellheader.Colspan = 6
            cellheader.BorderWidth = "0"
            cellheader.PaddingBottom = 5.0F
        Else
            headertable.AddCell(Format.getcell("Tran. date", 1))
            headertable.AddCell(Format.getcell("Value date", 1))
            headertable.AddCell(Format.getcell("Transaction details", 1))
            headertable.AddCell(Format.getcell("Debit", 11))
            headertable.AddCell(Format.getcell("Credit", 11))
            headertable.AddCell(Format.getcell("Balance", 11))
            headertable.SetWidths(widths)

            table.SetWidths(widths)

            cellheader = New PdfPCell(headertable)
            cellheader.Colspan = 6
        End If


        table.AddCell(cellheader)
        table.HeaderRows = 1
        For i = 0 To dt.Rows.Count - 1 Step +2
            For j = 1 To 6
                If j <= 2 Then
                    table.AddCell(Format.getcell(dt.Rows(i).Item(j + 1), 2))
                ElseIf j = 3 Then
                    table.AddCell(Format.getcell(dt.Rows(i).Item(j - 2), 2))
                ElseIf j > 3 Then
                    If j = 4 Or j = 5 Then
                        If dt.Rows(i).Item(j) = 0.00 Then
                            table.AddCell(Format.getcell("", 21))
                        Else
                            table.AddCell(Format.getcell(CDbl(dt.Rows(i).Item(j)).ToString("#,##0.00"), 21))
                        End If
                    Else
                        table.AddCell(Format.getcell(CDbl(dt.Rows(i).Item(j)).ToString("#,##0.00"), 21))

                    End If
                Else
                    table.AddCell(Format.getcell(dt.Rows(i).Item(j), 2))
                End If
            Next
            If i + 1 < dt.Rows.Count Then
                For k = 1 To 6
                    If k <= 2 Then
                        table.AddCell(Format.getcell(dt.Rows(i + 1).Item(k + 1), 2))
                    ElseIf k = 3 Then
                        table.AddCell(Format.getcell(dt.Rows(i + 1).Item(k - 2), 2))
                    ElseIf k > 3 Then
                        If k = 4 Or k = 5 Then
                            If dt.Rows(i + 1).Item(k) = 0.00 Then
                                table.AddCell(Format.getcell("", 21))
                            Else
                                table.AddCell(Format.getcell(CDbl(dt.Rows(i + 1).Item(k)).ToString("#,##0.00"), 21))
                            End If
                        Else
                            table.AddCell(Format.getcell(CDbl(dt.Rows(i + 1).Item(k)).ToString("#,##0.00"), 21))

                        End If
                        ' table.AddCell(Format.getcell(CDbl(dt.Rows(i + 1).Item(k)).ToString("#,##0.00"), 31))
                    Else
                        table.AddCell(Format.getcell(dt.Rows(i + 1).Item(k), 3))
                    End If
                Next
            End If
        Next
        Return table
    End Function

    Private Function TransSummary(ByVal dt As DataTable) As DataTable
        Dim dt1 As New DataTable()
        Dim dr As DataRow
        Dim TransYear, TransMonth, TotalDebit, TotalCredit, Mon As DataColumn
        'dt1 = New DataTable()
        TransYear = New DataColumn("TransYear", Type.GetType("System.String"))
        TransMonth = New DataColumn("TransMonth", Type.GetType("System.String"))
        TotalDebit = New DataColumn("TotalDebit", Type.GetType("System.Decimal"))
        TotalCredit = New DataColumn("TotalCredit", Type.GetType("System.Decimal"))
        Mon = New DataColumn("Mon", Type.GetType("System.String"))

        dt1.Columns.Add(TransYear)
        dt1.Columns.Add(TransMonth)
        dt1.Columns.Add(TotalDebit)
        dt1.Columns.Add(TotalCredit)
        dt1.Columns.Add(Mon)

        Dim groups =
     From j In dt
     Group By x = New With {Key .var1 = Year(CDate(j.Item(2))), Key .var2 = Month(CDate(j.Item(2))), Key .var3 = CDate(j.Item(2)).ToString("MMM", CultureInfo.InvariantCulture)} Into g = Group
     Order By x.var2
     Select New With {
         .TYear = x.var1,
         .TMonth = x.var2,
         .TMon = x.var3,
         .TDt = g.Sum(Function(r) r.Item(4)),
         .TCt = g.Sum(Function(r) r.Item(5))
     }
        For Each x In groups
            dr = dt1.NewRow()
            dr("TransYear") = x.TYear
            dr("TransMonth") = x.TMon
            dr("TotalDebit") = x.TDt
            dr("TotalCredit") = x.TCt
            dr("Mon") = x.TMonth
            dt1.Rows.Add(dr)
                '  Console.WriteLine("Month:{0} {1} {2} {3}", x.Month, x.Sales, x.Leads, x.Gross)
        Next
        Return dt1
    End Function
End Class
