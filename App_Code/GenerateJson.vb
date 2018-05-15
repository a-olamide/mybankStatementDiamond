Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Globalization
Imports System.IO
Public Class GenerateJson
    Private TransactionDT As DataTable 'Transaction details
    Private AccountDT As DataTable    'Acccount detials
    Private startDate As String
    Private endDate As String
    Private Destination As String
    Private Name As String
    Private TicketNo As String
    Private type As String
    Private Applicants As String()
    Private password As String
    Private countryCode As String
    

    Public Sub New(ByVal dtTransaction As DataTable, ByVal dtAccount As DataTable, ByVal PstartDate As String, ByVal PendDate As String, _
                 ByVal PDestination As String, ByVal PName As String, ByVal PticketNo As String, ByVal PType As String, ByVal PApplicants As String(), Ppassword As String, PcountryCode As String)
        TransactionDT = dtTransaction
        AccountDT = dtAccount
        startDate = PstartDate
        endDate = PendDate
        Destination = PDestination
        Name = PName
        TicketNo = PticketNo
        Applicants = PApplicants
        password = Ppassword
        countryCode = PcountryCode
        type = PType
    End Sub
    'ensure ur path has trailing \
    Public Function generateAndSaveJsonStatementAsTXTFile(Optional ByVal path As String = Nothing) As String
        Try
            If path Is Nothing Then
                path = ("D:\mybankStatementRepository\statementJSON\" & TicketNo + ".txt")
            Else
                path = path & TicketNo + ".txt"
            End If

            Dim createText As String = generateJsonStatementAsString()
            '+ Environment.NewLine
            File.WriteAllText(path, createText)
            ' End If
            Return "success"
            'catch file not found
        Catch ex As Exception
            Return "fail"
        End Try
    End Function

    Public Sub generateAndSaveZipedPasswordedJSON(Optional ByVal path As String = Nothing)
        generateAndSaveJsonStatementAsTXTFile(path)
        Dim sourcefile As String = Nothing
        Dim destfile As String = Nothing
        If path Is Nothing Then
            If (File.Exists(("D:\mybankStatementRepository\statementJSON\" & TicketNo + ".zip"))) Then
                File.Delete(("D:\mybankStatementRepository\statementJSON\" & TicketNo + ".zip"))
            End If
            sourcefile = ("D:\mybankStatementRepository\statementJSON\" & TicketNo + ".txt")
            destfile = ("D:\mybankStatementRepository\statementJSON\" & TicketNo + ".zip")
        Else
            If (File.Exists(path & TicketNo & ".zip")) Then
                File.Delete(path & TicketNo & ".zip")
            End If
            sourcefile = (path & TicketNo + ".txt")
            destfile = (path & TicketNo + ".zip")
        End If

        Utility.ToZIPJson(sourcefile, destfile, password, TicketNo.ToString())
        File.Delete(sourcefile)
    End Sub
    Public Function generateJsonStatementAsString() As String
        Try
            Dim tenor As Integer = ((Year(CDate(endDate)) - Year(CDate(startDate))) * 12) + (Month(CDate(endDate)) - Month(CDate(startDate)))
            Dim totalDt As Double = Convert.ToDouble(TransactionDT.Compute("SUM(DebitAmount)", String.Empty))
            Dim totalCt As Decimal = Convert.ToDouble(TransactionDT.Compute("SUM(CreditAmount)", String.Empty))
            Dim ticketFull As String = TicketNo & "-8"
            Dim createText As String = "{ ""status"": ""success"",""Name"": """ & AccountDT.Rows(0).Item("Name").ToString.Replace(",", "") & """,""Nuban"": """ & AccountDT.Rows(0).Item(3) & _
                       """,""AccountCategory"": """ & AccountDT.Rows(0).Item(9).Replace(",", "") & """,""AccountType"": """ & AccountDT.Rows(0).Item(8).ToString.Replace(",", "") & """,""TicketNo"": """ & ticketFull & """,""AvailableBal"": """ & AccountDT.Rows(0).Item(11) & """,""BookBal"": """ & AccountDT.Rows(0).Item(10) & """,""TotalCredit"": """ & totalCt.ToString & """,""TotalDebit"": """ & totalDt.ToString & """,""Tenor"": """ & tenor & """,""Period"": """ & startDate.Replace(",", "") & " to " & endDate.Replace(",", "") & """,""Currency"": """ & AccountDT.Rows(0).Item("currency") & _
                       """,""Applicants"": """ & getApplicants() & """,""Signatories"": " & getJsonSigDetails() & ",""Details"": " & _
                       getJsonTransactionDetailsAsStringArray() & "}"
            Return createText
        Catch ex As Exception
            Return "{ ""status"": ""fail"" }"
        End Try

    End Function
    Public Function getJsonTransactionDetailsAsStringArray() As String
        Try
            Dim transactionList As New List(Of TransactionDetails)()
            For Each dr As DataRow In TransactionDT.Rows
                transactionList.Add(New TransactionDetails(dr("TransactionDate"), dr("ValueDate"), dr("Narration"), dr("DebitAmount"), dr("CreditAmount"), dr("TransactionBalance")))
            Next
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim str As String = serializer.Serialize(transactionList)
            Return str
        Catch ex As Exception
            Return "[]"
        End Try
    End Function

    Private Function getJsonSigDetails() As String
        Try
            Dim res As String = "["
            ' If AccountDT.Rows(0).Item("Category").ToString.ToLower.Contains("individual") Then
            '  res &= "{""Name"": """ & AccountDT.Rows(0).Item("Name").ToString.Replace(",", "").Replace("&", "and") & """,""BVN"":""" & AccountDT.Rows(0).Item("BVN") & """}]"
            'Else
            Dim dtSig As DataTable
            '  If HttpContext.Current.Session("tblSignatories") Is Nothing Then
            dtSig = BLL._selectSignatoriesNew(TicketNo)

            If dtSig Is Nothing Or dtSig.Rows.Count = 0 Then
                res &= ""

            Else
                For i As Integer = 0 To dtSig.Rows.Count - 1
                    ' res &= """Nuban"": """ & dtSig.Rows(i).Item("Name") & """,""BVN"":""" & dtSig.Rows(i).Item("Name") & """}"
                    If i = dtSig.Rows.Count - 1 Then res &= "{""Name"": """ & dtSig.Rows(i).Item("Name").ToString.Replace(",", "") & """,""BVN"":""" & dtSig.Rows(i).Item("BVN") & """}" Else res &= "{""Name"": """ & dtSig.Rows(i).Item("Name").ToString.Replace(",", "") & """,""BVN"":""" & dtSig.Rows(i).Item("BVN") & """}" & ","
                Next

            End If
            'End If
            res &= "]"
            Return res
        Catch ex As Exception
            Return "[]"
        End Try

    End Function

    Private Function getApplicants() As String
        Try
            Dim res As String = ""
            If type.ToLower = "request" Then

                If Applicants.Count = 0 Then
                    res = "N/A"
                Else
                    For i As Integer = 0 To Applicants.Count - 1
                        If i = Applicants.Count - 1 Then res += Applicants(i) Else res += Applicants(i) & "|"
                    Next
                End If
                Return res
            Else

                Dim dt21 As DataTable = BLL._selectApplicant(TicketNo)
                If dt21.Rows.Count = 0 Then
                    res = "N/A"
                Else
                    For i As Integer = 0 To dt21.Rows.Count - 1
                        If i = dt21.Rows.Count - 1 Then res += dt21.Rows(i).Item(1) Else res += dt21.Rows(i).Item(1) & "|"
                    Next
                End If
                Return res

            End If

        Catch ex As Exception
            Return "fail"
        End Try
    End Function
    
End Class
