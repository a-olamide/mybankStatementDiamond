Imports Microsoft.VisualBasic
Imports System.Data
Imports System
Imports System.IO
Imports System.Web.Services
Imports System.Net
Imports System.Xml
Imports System.Threading.Tasks
Imports Newtonsoft.Json.Linq
Imports System.Web.Script.Serialization

Public Class DAL_API
    Inherits System.Web.UI.Page

    Public Shared Function ValidateAccountWithPhone(ByVal accountNo As String, ByVal phone As String, ByVal countryCode As String) As String
        Try
            Return Nothing
        Catch ex As Exception
            BLL._insertTicketMailAlertCorporate(Nothing, "Error Encountered", "support@wallzandqueenltd.com", "Account-Phone Validation (Diamond)", "</b> Error while validating account with phone. details below : </br></br> " & ex.Message(), "1", Nothing)
            Return "noservice"
        End Try
    End Function


    Public Shared Function AccountDetails(ByVal accountNo As String) As DataTable

        Try
            Dim tran As String = "{""Details"": [{ ""AccountNumber"": ""String"", ""Narration"": ""String"", ""TransactionDate"": ""2018-02-27T13:33:17.265Z"", ""ValueDate"": ""2018-02-27T13:33:17.265Z"",""DebitAmount"": 0, ""CreditAmount"": 0,  ""TransactionBalance"": 0 }], ""ResponseCode"": ""string"", ""ResponseMessage"": ""string""}"

            Return Nothing
        Catch ex As Exception
            BLL._insertTicketMailAlertCorporate(Nothing, "Error Encountered", "support@wallzandqueenltd.com", "Account Equiry Error (Diamond)", "</b> Account Enquiry service error. details below : </br></br> " & ex.Message(), "1", Nothing)
            Utility.LogException(HttpContext.Current.Session("USERID") + " " + HttpContext.Current.Session("NAME") + "***** Calling Account Enquiry Service *** " + ex.Message())
            Return Nothing
        End Try

    End Function


    Public Shared Function Signatories(ByVal accountNo As String) As DataTable
        Dim dtsignatories As New DataTable()
        Dim dr As DataRow
        Dim name1, nuban1, dob1, bvn1 As DataColumn
        ' Dim Nuban, TRA_DATE, VAL_DATE, Trans_Description, DR_AMT, CR_AMT, TRA_BAL As DataColumn
        Dim nuban, name, bvn, dob As String

        name1 = New DataColumn("Name", System.Type.GetType("System.String"))
        nuban1 = New DataColumn("Nuban", System.Type.GetType("System.String"))
        dob1 = New DataColumn("DOB", System.Type.GetType("System.String"))
        bvn1 = New DataColumn("BVN", System.Type.GetType("System.String"))


        dtsignatories.Columns.Add(name1)
        dtsignatories.Columns.Add(nuban1)
        dtsignatories.Columns.Add(dob1)
        dtsignatories.Columns.Add(bvn1)

        dr = dtsignatories.NewRow()
        Try
            name = "N/A"
            nuban = "N/A"
            dob = "N/A"
            bvn = "N/A"
            dr("Name") = name
            dr("Nuban") = nuban
            dr("DOB") = dob
            dr("BVN") = bvn
            dtsignatories.Rows.Add(dr)

            Return dtsignatories
        Catch ex As Exception
            dr("Name") = "Not Available"
            dr("Nuban") = "Not Available"
            dr("DOB") = "Not Available"
            dr("BVN") = "Not Available"
            dtsignatories.Rows.Add(dr)
            Return dtsignatories
        End Try
    End Function

    Public Shared Function TransactionDetails(ByVal accounttNo As String, ByVal startDate As String, ByVal endDate As String) As DataTable
        Return Nothing

    End Function

    Public Shared Function Billing(ByVal CustacctNo As String, ByVal CustAmount As Decimal, ByVal WQacctNo As String,
                                   ByVal WQAmount As Decimal, ByVal DiamondAcctNo As String, ByVal DiamondAmount As Decimal,
                                   ByVal VatacctNo As String, ByVal VATAmount As Decimal, ByVal ticket As String,
                                   ByVal Narration As String) As String
        Try
            Dim serv As New DiamondService.Service
            Dim resArr() As String = serv.Billing(CustacctNo, DiamondAcctNo, CustAmount - VATAmount, "Charges for " & Narration, "Payment for " & Narration, Guid.NewGuid().ToString("N") & ticket)
            Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(resArr(1))

            Dim res As String = j("ResponseCode")
            If res = "00" Then
                Try
                    'Dim resArr1() As String = serv.Billing(DiamondAcctNo, WQacctNo, WQAmount, "Charges for " & Narration, "Payment for " & Narration, Guid.NewGuid().ToString("N") & ticket)
                    'Dim j1 As Object = New JavaScriptSerializer().Deserialize(Of Object)(resArr1(1))

                    'Dim res1 As String = j1("ResponseCode")
                    'If res1 <> "00" Then
                    BLL._updateNoCredit("W", "1", ticket)
                  '  End If
                Catch ex As Exception
                    BLL._updateNoCredit("W", "1", ticket)
                End Try
                Try
                    Dim resArrVat() As String = serv.Billing(CustacctNo, DiamondAcctNo, VATAmount, "VAT Charges for " & Narration, "Payment for " & Narration, Guid.NewGuid().ToString("N") & ticket)
                    Dim jVat As Object = New JavaScriptSerializer().Deserialize(Of Object)(resArrVat(1))

                    Dim resVat As String = jVat("ResponseCode")
                    If resVat <> "00" Then
                        BLL._updateNoCredit("V", "1", ticket)
                    End If
                Catch ex As Exception
                    BLL._updateNoCredit("V", "1", ticket)
                End Try
                'Try
                '    Dim resArr2() As String = serv.Billing(DiamondAcctNo, VatacctNo, VATAmount, "VAT Charges for " & Narration, "VAT Payment for " & Narration, Guid.NewGuid().ToString("N") & ticket)
                '    Dim j2 As Object = New JavaScriptSerializer().Deserialize(Of Object)(resArr2(1))

                '    Dim res2 As String = j2("ResponseCode")
                '    If res2 <> "00" Then
                '        BLL._updateNoCredit("V", "1", ticket)
                '    End If
                'Catch ex As Exception
                '    BLL._updateNoCredit("V", "1", ticket)
                'End Try
                Return "00"
            Else

                Return j("ResponseMessage")
            End If

        Catch ex As Exception
            BLL._insertTicketMailAlertCorporate(Nothing, "Error Encountered", "support@wallzandqueenltd.com", "Posting service Error (Diamond)", "</b> Error encountered while calling Posting service at Diamond Bank. details below : </br></br> " & ex.Message(), "1", Nothing)
            Utility.LogException(HttpContext.Current.Session("USERID") + " " + HttpContext.Current.Session("NAME") + "***** Calling Send to service posting service *** " + ex.Message())

            Return ex.Message()
        End Try
    End Function
    Public Shared Function Billing(ByVal CustacctNo As String, ByVal CustAmount As Decimal, ByVal DiamondAcctNo As String, ByVal DiamondAmount As Decimal,
                                   ByVal VATAcctNo As String, ByVal VATAmount As Decimal, ByVal ticket As String, ByVal Narration As String) As String
        Try
            Dim serv As New DiamondService.Service
            Dim resArr() As String = serv.Billing(CustacctNo, DiamondAcctNo, CustAmount - VATAmount, Narration, Narration, Utility.Get4RadomPassword() & ticket)
            Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(resArr(1))


            Dim res As String = j("ResponseCode")


            If res = "00" Then
                Try
                    Dim resArr1() As String = serv.Billing(CustacctNo, VATAcctNo, VATAmount, Narration, Narration, Utility.Get4RadomPassword() & ticket)
                    Dim j1 As Object = New JavaScriptSerializer().Deserialize(Of Object)(resArr1(1))

                    Dim res1 As String = j1("ResponseCode")
                    If res1 <> "00" Then
                        BLL._updateNoCredit("V", "1", ticket)
                    End If
                Catch ex As Exception
                    BLL._updateNoCredit("V", "1", ticket)
                End Try
                Return "00"
            Else
                Return j("ResponseMessage")
                ' Return allData("ResponseMessage")
            End If

        Catch ex As Exception
            BLL._insertTicketMailAlertCorporate(Nothing, "Error Encountered", "support@wallzandqueenltd.com", "Posting service Error (Diamond)", "</b> Error encountered while calling Posting service at Diamond Bank. details below : </br></br> " & ex.Message(), "1", Nothing)
            Utility.LogException(HttpContext.Current.Session("USERID") + " " + HttpContext.Current.Session("NAME") + "***** Calling Send to service posting service *** " + ex.Message())

            Return ex.Message()
        End Try
    End Function




End Class
