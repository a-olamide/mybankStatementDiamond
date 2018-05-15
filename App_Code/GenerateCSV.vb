Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Globalization
Imports System.IO
Public Class GenerateCSV
    Private dt As DataTable 'Transaction details
    Private dt2 As DataTable    'Acccount detials
    Private startDate As String
    Private endDate As String
    Private Destination As String
    Private Role As String
    Private Name As String
    Private Address As String
    Private ticketNo As String
    Private StatementPasscode As String
    Private type As String
    Private countryCode As String
    Private RequestApplicants As String() = Nothing

    Public Sub New(ByVal dtTransaction As DataTable, ByVal dtAccount As DataTable, ByVal PstartDate As String, ByVal PendDate As String, _
                 ByVal PDestination As String, ByVal PRole As String, ByVal PName As String, ByVal PAddress As String, _
                 ByVal PticketNo As String, ByVal Ptype As String, ByVal PpassCode As String, ByVal PCountryCode As String)
        dt = dtTransaction
        dt2 = dtAccount
        startDate = PstartDate
        endDate = PendDate
        Destination = PDestination
        Role = PRole
        Name = PName
        Address = PAddress
        ticketNo = PticketNo
        StatementPasscode = PpassCode
        type = Ptype
        countryCode = PCountryCode

    End Sub
    'Ensure your path has a trailing \
    Public Sub New(ByVal dtTransaction As DataTable, ByVal dtAccount As DataTable, ByVal PstartDate As String, ByVal PendDate As String, _
                 ByVal PDestination As String, ByVal PRole As String, ByVal PName As String, ByVal PAddress As String, _
                 ByVal PticketNo As String, ByVal Ptype As String, ByVal PpassCode As String, ByVal PCountryCode As String, ByVal PApplicants As String())
        dt = dtTransaction
        dt2 = dtAccount
        startDate = PstartDate
        endDate = PendDate
        Destination = PDestination
        Role = PRole
        Name = PName
        Address = PAddress
        ticketNo = PticketNo
        StatementPasscode = PpassCode
        type = Ptype
        countryCode = PCountryCode
        RequestApplicants = PApplicants
    End Sub
    Public Sub generateAndSaveZipedPasswordedCSV(Optional ByVal path As String = Nothing)
        generateZippedPasswordedCSV(GenerateCSV())
        Dim sourcefile As String = Nothing
        Dim destfile As String = Nothing
        If path Is Nothing Then
            If (File.Exists(("D:\mybankStatementRepository\statementCSV\" & ticketNo + ".zip"))) Then
                File.Delete(("D:\mybankStatementRepository\statementCSV\" & ticketNo + ".zip"))
            End If
            sourcefile = ("D:\mybankStatementRepository\statementCSV\" & ticketNo + ".csv")
            destfile = ("D:\mybankStatementRepository\statementCSV\" & ticketNo + ".zip")
        Else
            If (File.Exists(path & ticketNo & ".zip")) Then
                File.Delete(path & ticketNo & ".zip")
            End If
            sourcefile = (path & ticketNo + ".csv")
            destfile = (path & ticketNo + ".zip")
        End If

        Utility.ToZIP(sourcefile, destfile, StatementPasscode, ticketNo.ToString())
        File.Delete(sourcefile)
    End Sub
    'Ensure your path has a trailing \
    Public Sub generateZippedPasswordedCSV(generatedCSV As DataTable, Optional ByVal path As String = Nothing)
        Dim sw As StreamWriter = Nothing
        If path Is Nothing Then
            sw = New StreamWriter(("D:\mybankStatementRepository\statementCSV\" & ticketNo & ".csv"), False)

        Else
            sw = New StreamWriter(path & ticketNo & ".csv", False)

        End If

        For Each dr As DataRow In generatedCSV.Rows
            For i As Int16 = 0 To generatedCSV.Columns.Count - 1
                If Not Convert.IsDBNull(dr(i)) Then
                    Dim value As String = dr(i).ToString()
                    If value.Contains(",") Then
                        'value = String.Format("\{0}\", value)
                        value = value.Replace(",", ";")
                        sw.Write(value)
                    Else
                        sw.Write(dr(i).ToString())
                    End If

                End If
                If i < generatedCSV.Columns.Count - 1 Then
                    sw.Write(",")
                End If



            Next
            sw.Write(sw.NewLine)
        Next
        sw.Close()

    End Sub
    'This simply return datatable that has the content of the csv
    Public Function GenerateCSV() As DataTable
        Dim dt1 As New DataTable()
        Dim dr As DataRow
        Dim subColumn As DataColumn
        Dim valColumn, Column3, Column4, Column5, Column6 As DataColumn
        Dim totalDt As Double = Convert.ToDouble(dt.Compute("SUM(DebitAmount)", String.Empty))
        Dim totalCt As Decimal = Convert.ToDouble(dt.Compute("SUM(CreditAmount)", String.Empty))
        'dt1 = New DataTable()
        subColumn = New DataColumn("Sub", System.Type.GetType("System.String"))
        valColumn = New DataColumn("Value", System.Type.GetType("System.String"))
        Column3 = New DataColumn("col3", System.Type.GetType("System.String"))
        Column4 = New DataColumn("col4", System.Type.GetType("System.String"))
        Column5 = New DataColumn("col5", System.Type.GetType("System.String"))
        Column6 = New DataColumn("col6", System.Type.GetType("System.String"))
        dt1.Columns.Add(subColumn)
        dt1.Columns.Add(valColumn)
        dt1.Columns.Add(Column3)
        dt1.Columns.Add(Column4)
        dt1.Columns.Add(Column5)
        dt1.Columns.Add(Column6)
        dr = dt1.NewRow()
        dr("Sub") = "mybankStatement® - Summary Info & Transaction Details"
        dr("Value") = ""
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)
        dr = dt1.NewRow()
        dr("Sub") = "Ticket No."
        dr("Value") = "'" & ticketNo & "-8"
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)
        Dim applicants As String = Nothing
        If RequestApplicants Is Nothing Then
            Dim dt21 As DataTable = BLL._selectApplicant(ticketNo)
            If dt21.Rows.Count = 0 Then
                applicants = "N/A"
            Else
                For i As Integer = 0 To dt21.Rows.Count - 1
                    If i = dt21.Rows.Count - 1 Then applicants += dt21.Rows(i).Item(1) Else applicants += dt21.Rows(i).Item(1) & ","
                Next
            End If
        Else
            If RequestApplicants.Count = 0 Then
                applicants = "N/A"
            Else
                For i As Integer = 0 To RequestApplicants.Count - 1
                    If i = RequestApplicants.Count - 1 Then applicants += RequestApplicants(i) Else applicants += RequestApplicants(i) & ","
                Next
            End If
        End If


        
        dr = dt1.NewRow()
        dr("Sub") = "Applicant(s)"
        dr("Value") = applicants
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)

        Dim dtSig As DataTable = Nothing

        Dim signatory As String = ""
        Try

            '    If dt2.Rows(0).Item("Category").ToString.ToLower.Contains("individual") Then
            '  signatory = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dt2.Rows(0).Item("Name")) + " - BVN (" + dt2.Rows(0).Item("BVN") + ") "
            'Else
            dtSig = BLL._selectSignatoriesNew(ticketNo)

            If dtSig Is Nothing Or dtSig.Rows.Count = 0 Then
                signatory = "N/A"
            Else
                For i As Integer = 0 To dtSig.Rows.Count - 1
                    If i = dtSig.Rows.Count - 1 Then signatory += dtSig.Rows(i).Item("Name") & " BVN - " & dtSig.Rows(i).Item("BVN") Else signatory += dtSig.Rows(i).Item("Name") & " BVN - " & dtSig.Rows(i).Item("BVN") & ", "
                Next
            End If
            'End If


        Catch ex As Exception
            signatory = "N/A"
        End Try
        dr = dt1.NewRow()
        dr("Sub") = "Signatories(s)"
        dr("Value") = signatory
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)
        ' Dim dtSig As DataTable = HttpContext.Current.Session("tblSignatories")

        Dim tenor As Integer = ((Year(CDate(endDate)) - Year(CDate(startDate))) * 12) + (Month(CDate(endDate)) - Month(CDate(startDate)))
        dr = dt1.NewRow()
        dr("Sub") = "Tenor"
        dr("Value") = tenor
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)
        dr = dt1.NewRow()
        dr("Sub") = "Period"
        dr("Value") = startDate & " to " & endDate
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)
        dr = dt1.NewRow()
        dr("Sub") = "Type"
        dr("Value") = dt2.Rows(0).Item(8)
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)
        dr = dt1.NewRow()
        dr("Sub") = "Category"
        dr("Value") = dt2.Rows(0).Item(9)
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)
        dr = dt1.NewRow()
        dr("Sub") = "Account No."
        dr("Value") = dt2.Rows(0).Item(3)
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Sub") = "Currency"
        dr("Value") = dt2.Rows(0).Item(23)
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Sub") = "Available Balance"
        dr("Value") = CDbl(dt2.Rows(0).Item(11))
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Sub") = "Book Balance"
        dr("Value") = CDbl(dt2.Rows(0).Item(10))
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Sub") = "Total Debit"
        dr("Value") = totalCt
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)
        dr = dt1.NewRow()
        dr("Sub") = "Total Credit"
        dr("Value") = totalDt
        dr("col3") = ""
        dr("col4") = ""
        dr("col5") = ""
        dr("col6") = ""
        dt1.Rows.Add(dr)
        dr = dt1.NewRow()
        dr("Sub") = "Tran. Date"
        dr("Value") = "Value date"
        dr("col3") = "Transaction details"
        dr("col4") = "Debit"
        dr("col5") = "Credit"
        dr("col6") = "Balance"
        dt1.Rows.Add(dr)
        For k As Integer = 0 To dt.Rows.Count - 1
            dr = dt1.NewRow()
            dr("Sub") = dt.Rows(k).Item(2).ToString
            dr("Value") = dt.Rows(k).Item(3).ToString
            dr("col3") = dt.Rows(k).Item(1).ToString
            dr("col4") = dt.Rows(k).Item(4).ToString
            dr("col5") = dt.Rows(k).Item(5).ToString
            dr("col6") = dt.Rows(k).Item(6).ToString
            dt1.Rows.Add(dr)
        Next
        'Dim gv As New GridView
        'gv.DataSource = dt1
        'gv.DataBind()


        Return dt1
    End Function
End Class
