Imports System.Data
Imports System
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Collections
Imports iTextSharp.text.html.simpleparser
Imports System.Globalization
Imports System.Web.Services
Imports System.Text
Imports iTextSharp.text.xml.xmp
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Class DashBoard
    Inherits System.Web.UI.Page
    Public Shared val As DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'acctetails.DataSource = DAL_API.AccountDetails("0040776395")
        'acctetails.DataBind()
        '' transacdetail.DataSource = DAL_API.TransactionDetails("0040776395", "05-Jan-2014", "05-Jan-2015")
        ' transacdetail.DataBind()


        txtApplicants.Attributes.Add("onkeydown", "return (event.keyCode!=13);")
        If Session("USERID") Is Nothing Then Response.Redirect("login.aspx")
        If Not IsPostBack Then
            If Session("Role") = "Reviewer" Then Response.Redirect("Preview.aspx")
            If Session("Role") = "Reviewer" Or Session("Role") = "Initiator" Or Session("Role") = "Approver" Then

                lblUser.Text = Session("BranchName")
                SelectActivities("All")
                Try
                    loadDDL()
                Catch ex As Exception
                    Utility.LogException(HttpContext.Current.Session("NAME").ToString & HttpContext.Current.Session("USERID").ToString & "Error loading Country dropdown. User's country may not have been captured")
                End Try
            ElseIf Session("Role") = "Admin" Then
                Response.Redirect("CreateUser.aspx")
            Else

                Response.Redirect("login.aspx")

            End If
        End If
    End Sub
    Protected Sub bSearch_Click(sender As Object, e As System.EventArgs) Handles bSearch.Click
        Try
            If Trim(txtAccountSearch.Text) = "" Then
                Utility.ShowSweet(Me, "Please enter account Number")
            ElseIf Not IsNumeric(Trim(txtAccountSearch.Text)) Then
                Utility.ShowSweet(Me, "Invalid account number.")
            Else
                '  Dim tranDetails As String = "{""Details"": [{ ""AccountNumber"": ""1111111111"", ""Narration"": ""Opening Balance"", ""TransactionDate"": ""2018-02-27T13:33:17.265Z"", ""ValueDate"": ""2018-02-27T13:33:17.265Z"",""DebitAmount"": 0, ""CreditAmount"": 0,  ""TransactionBalance"": 0 }], ""ResponseCode"": ""OO"", ""ResponseMessage"": ""string""}"
                'Dim tran As String = "{""Details"": { ""AccountNumber"": ""1111111111"", ""AccountName"": ""Akinoso Olamide"", ""AccountType"": ""Savings"", ""AccountCategory"": ""Individual"", ""BookBalance"": 500000, ""AvailableBalance"": 500000, ""Address"": ""(4, Aladeselu Street, Lekki Lagos"", ""Email"": ""a.olamide@wallzandqueenltd.com"",  ""Telephone"": ""0709883""}, ""ResponseCode"": ""00"", ""ResponseMessage"": ""Aproved""}"
                Dim serv As New DiamondService.Service()
                Dim resArr() As String = serv.getAccountDetails(Trim(txtAccountSearch.Text))
                If resArr(0) = "00" Then
                    Dim acct As String = ""
                    Dim accType As String = ""
                    Dim AccCat As String = ""
                    Dim acctName As String = ""
                    Dim accEmail As String = ""
                    Dim address As String = ""
                    Dim avl_bal As String = ""
                    Dim clr_bal As String = ""
                    Dim currency As String = ""
                    Dim phone As String = ""
                    Try
                        Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(resArr(1))
                        ' Dim j As AccountEnqResponse = New JavaScriptSerializer().Deserialize(Of AccountEnqResponse)(resArr(1))
                        ' Dim details As AccountEnq = New JavaScriptSerializer().Deserialize(Of AccountEnq)(j(0)("Details"))
                        ' Dim myJ = JsonConvert.DeserializeObject(Of AccountEnqResponse)(resArr(1))
                        ' Dim namee As String = j("Details")("AccountName")
                        '  For Each kvp As KeyValuePair(Of String, AccountEnqResponse) In myJ
                        Dim allData As JObject = JObject.Parse(j)

                        ' Dim acctObj As AccountEnq = JsonConvert.DeserializeObject(Of AccountEnq)(allData("Details"))
                        '  Next
                        If allData("ResponseCode") = "00" Then
                            acct = allData("Details")("AccountNumber")
                            accType = allData("Details")("AccountType")
                            AccCat = allData("Details")("AccountCategory")
                            acctName = allData("Details")("AccountName")
                            accEmail = allData("Details")("Email")
                            address = allData("Details")("Address")
                            avl_bal = allData("Details")("AvailableBalance").ToString()
                            clr_bal = allData("Details")("BookBalance").ToString()
                            currency = allData("Details")("Currency")
                            phone = allData("Details")("Telephone")

                            Dim Email As String = ""
                            If accEmail Is Nothing Then
                                Email = "N/A"
                            ElseIf Utility.ValidateEmail(accEmail) Then
                                Email = accEmail
                            Else
                                Email = "N/A"
                            End If

                            Dim dt As DataTable = BLL._selectAccountOra(Trim(txtAccountSearch.Text), acctName, accType,
                                            AccCat, clr_bal, avl_bal, address, Email, Session("USERID"), Session("NAME"),
                                      0, Session("Branch"), "", currency, Nothing, phone)


                            If dt.Rows.Count = 0 Then
                                tbAccount.InnerHtml = "<tr><td colspan=""2"">No existing account For " & txtAccountSearch.Text & ".</td></tr>"
                                tbCriteria.Style.Add("display", "none")
                                dv1.Style.Add("display", "none")
                                dv2.Style.Add("display", "none")
                            Else
                                dv1.Style.Add("display", "")
                                dv2.Style.Add("display", "")
                                Session("REQUESTID") = dt.Rows(0).Item(0)
                                tbAccount.InnerHtml = "<tr> <td >Ticket No</td><td id=""tdTicket__""> " & dt.Rows(0).Item(0) & " </td>	</tr><tr>" &
                                "<td >Account No</td><td > " & dt.Rows(0).Item(1) & " </td>	</tr><tr><td >Account Name</td>" &
                            "<td id=""tdAcctName_"">" & dt.Rows(0).Item(2) & " </td></tr><tr><td >Address</td>" &
                            "<td id=""tdAddress_"">" & dt.Rows(0).Item(12) &
                            "</td></tr><tr><td>Signatories</td><td>"

                                'Dim tranDetails As String = "{""Details"": [{ ""AccountNumber"": ""1111111111"", ""Narration"": ""Opening Balance"", ""TransactionDate"": ""2018-02-27T13:33:17.265Z"", ""ValueDate"": ""2018-02-27T13:33:17.265Z"",""DebitAmount"": 0, ""CreditAmount"": 0,  ""TransactionBalance"": 0 }], ""ResponseCode"": ""OO"", ""ResponseMessage"": ""string""}"
                                Dim dt1 As DataTable = Nothing
                                Try
                                    Dim servSig As New DiamondService.Service()
                                    Dim resArrSig() As String = servSig.getSignatoriesDetails(Trim(txtAccountSearch.Text))
                                    If resArrSig(0) = "00" Then
                                        Dim jSig As Object = New JavaScriptSerializer().Deserialize(Of Object)(resArrSig(1))
                                        Dim allDataSig As JObject = JObject.Parse(jSig)
                                        dt1 = Utility.jsonToDataTable(JsonConvert.SerializeObject(allDataSig("signatories")))
                                    End If
                                Catch ex As Exception

                                End Try


                                ' Dim dt1 As DataTable = BLL._selectSignatories(dt.Rows(0).Item(1))

                                If dt1 Is Nothing Then
                                    tbAccount.InnerHtml += "<i>Signatory Not available</i>"
                                ElseIf dt1.Rows.Count = 0 Then
                                    tbAccount.InnerHtml += "<i>No signatory</i>"
                                Else
                                    For i As Integer = 0 To dt1.Rows.Count - 1
                                        tbAccount.InnerHtml += dt1.Rows(i).Item(0) & "<br />"
                                        Dim bvn As String = ""
                                        If IsDBNull(dt1.Rows(i).Item(1)) = True Then
                                            bvn = "N/A"
                                        Else
                                            bvn = dt1.Rows(i).Item(1)
                                        End If
                                        BLL._insertSignatories(dt.Rows(0).Item(0), dt1.Rows(i).Item(0), bvn, dt1.Rows(i).Item(3), dt1.Rows(i).Item(4), dt1.Rows(i).Item(2), Trim(txtAccountSearch.Text))
                                    Next
                                    tbAccount.InnerHtml = tbAccount.InnerHtml.Substring(0, tbAccount.InnerHtml.Length - 6)
                                End If
                                tbAccount.InnerHtml += "</td></tr><tr><td >Type</td><td>" & dt.Rows(0).Item(3) & "</td></tr><tr><td>Category</td>" &
                            "<td>" & dt.Rows(0).Item(4) & "</td></tr><tr><td>Cleared balance</td><td> " & dt.Rows(0).Item(14) & " " & CDbl(dt.Rows(0).Item(5)).ToString("#,##0.00") &
                            "</td></tr><tr><td >Available balance</td><td> " & dt.Rows(0).Item(14) & " " & CDbl(dt.Rows(0).Item(6)).ToString("#,##0.00") & "</td> </tr>"
                                tbCriteria.Style.Add("display", "")
                                selectApplicants()
                                txtApplicants.Text = dt.Rows(0).Item(2)
                            End If
                            Session("tbAccount") = tbAccount.InnerHtml
                            SelectActivities("All")
                            BLL._insertAuditLogsBranch(HttpContext.Current.Session("USERID"), "N/A", HttpContext.Current.Session("Role"), HttpContext.Current.Session("REQUESTID"), "Initiate Statement", Now, HttpContext.Current.Session("Branch"))

                            ' ElseIf j("ResponseCode") = "00" Then 'check the code for account not exists
                        ElseIf allData("ResponseCode") = "25" Then
                            Utility.ShowSweet(Me, "Record Not found. Account number may be invalid")
                            tbAccount.InnerHtml = "<tr><td colspan=""2"">No existing account For " & txtAccountSearch.Text & ".</td></tr>"
                            tbCriteria.Style.Add("display", "none")
                            dv1.Style.Add("display", "none")
                            dv2.Style.Add("display", "none")
                        Else

                            Utility.ShowSweet(Me, "Oops! There seems To be a problem fetching account detials. Details: " & allData("ResponseMessage").ToString())
                            tbAccount.InnerHtml = "<tr><td colspan=""2"">No existing account For " & txtAccountSearch.Text & ".</td></tr>"
                            tbCriteria.Style.Add("display", "none")
                            dv1.Style.Add("display", "none")
                            dv2.Style.Add("display", "none")
                        End If


                    Catch ex As Exception
                        Utility.ShowSweet(Me, "Error encountered while fetching account detials.")
                        Utility.LogException(Now.ToString() & "  " & ex.Message())
                        tbAccount.InnerHtml = "<tr><td colspan=""2"">No existing account For " & txtAccountSearch.Text & ".</td></tr>"
                        tbCriteria.Style.Add("display", "none")
                        dv1.Style.Add("display", "none")
                        dv2.Style.Add("display", "none")
                    End Try
                Else

                    Utility.ShowSweet(Me, "Oops! There seems To be a problem fetching account detials. Details: " & resArr(0).ToString())
                    tbAccount.InnerHtml = "<tr><td colspan=""2"">No existing account For " & txtAccountSearch.Text & ".</td></tr>"
                    tbCriteria.Style.Add("display", "none")
                    dv1.Style.Add("display", "none")
                    dv2.Style.Add("display", "none")
                End If


            End If
        Catch ex As Exception
            Utility.ShowSweet(Me, "Oops! There seems To be a problem fetching account detials.")

            tbCriteria.Style.Add("display", "none")
            dv1.Style.Add("display", "none")
            dv2.Style.Add("display", "none")
            Utility.LogException(Now.ToString & " " & HttpContext.Current.Session("USERID") & " Error " & ex.Message())
        End Try

    End Sub
    Private Sub loadDDL()
        Dim item_ As New System.Web.UI.WebControls.ListItem("Choose...", "0")
        ddlCountry.DataTextField = "Country"
        ddlCountry.DataValueField = "CountryCode"
        ddlCountry.DataSource = BLL._selectCountry()
        'This is perculiar to Diamond since your country determines what destination you can send to
        'ddlCountry.DataSource = BLL._selectUBAUserCountry(HttpContext.Current.Session("Country").ToString)
        ddlCountry.DataBind()
        ddlCountry.SelectedValue = Session("CountryCode")

        ddlRole.DataTextField = "Role"
        ddlRole.DataValueField = "Role"
        ddlRole.DataSource = BLL._selectStatementRole()
        ddlRole.DataBind()
        'If ddlRole.Items.FindByValue("0") Is Nothing Then ddlRole.Items.Add(item_)
        'ddlRole.SelectedValue = "0"
        ddlCategory.DataTextField = "Category"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataSource = BLL._selectCategory(ddlCountry.SelectedValue)
        ddlCategory.DataBind()
        If ddlCategory.Items.FindByValue("0") Is Nothing Then ddlCategory.Items.Add(item_)
        ddlCategory.SelectedValue = "0"

        ddlCompany.DataTextField = "Name"
        ddlCompany.DataValueField = "ID"
        ddlCompany.DataSource = BLL._selectDestinationByID(ddlCountry.SelectedValue, ddlCategory.SelectedValue)
        ddlCompany.DataBind()
        If ddlCompany.Items.FindByValue("0") Is Nothing Then ddlCompany.Items.Add(item_)
        ddlCompany.SelectedValue = "0"
    End Sub
    Protected Sub lbDeleteRequest_Click(sender As Object, e As System.EventArgs) Handles lbDeleteRequest.Click
        BLL._deleteActivity(Session("REQUESTID"))
        tbAccount.InnerHtml = ""
        tbCriteria.Style.Add("display", "none")
        dv1.Style.Add("display", "none")
        dv2.Style.Add("display", "none")
        tbCriteria.Style.Add("display", "")
        SelectActivities("All")
        txtAccountSearch.Text = ""
    End Sub

    Private Sub selectApplicants()
        Dim dt2 As DataTable = BLL._selectApplicant(Session("REQUESTID"))
        If dt2.Rows.Count = 0 Then
            tbApplicants.InnerHtml = "<tr><td style=""padding:0px""><i>No applicant</i></td></tr>"
        Else
            For i As Integer = 0 To dt2.Rows.Count - 1
                If i = 0 Then tbApplicants.InnerHtml = ""
                tbApplicants.InnerHtml += "<tr><td style=""padding:0px"">" & dt2.Rows(i).Item(1) &
                    "</td><td style=""padding:0px 0px 0px 10px"" ><img id=""imgDelete" & dt2.Rows(i).Item(0) &
                    """ style=""cursor:pointer"" onclick=""jDeleteApplicant('" & dt2.Rows(i).Item(0) & "_" & Session("REQUESTID") & "')"" src=""Delete.gif"" height=""15px"" width=""15px""/> </td></tr>"
            Next
        End If
    End Sub
    Private Sub SelectActivities(ByVal type_ As String)
        If HttpContext.Current.Session("Branch") Is Nothing Then HttpContext.Current.Response.Redirect("Login.aspx")
        Dim dt As DataTable = Nothing
        If type_ = "All" Then
            dt = BLL._selectActivity(Session("Branch"))
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(7) < 3 Then lblTop.Text = dt.Rows(0).Item(7) Else lblTop.Text = "3"
            End If

        ElseIf type_ = "Filter-All" Then
            dt = BLL._selectActivitybyFilter(Session("Branch"), "Filter-All")
            If dt.Rows.Count > 0 Then
                lblTop.Text = dt.Rows(0).Item(7)
            End If

        End If

        tbActivityBody.InnerHtml = ""
        If dt.Rows.Count = 0 Then
            tbActivityFoot.Visible = False
            tbActivityHead.Visible = False
            tbActivityBody.InnerHtml = "<tr><td colspan=""8"">No statements have been generated by " & lblUser.Text & ".</td></tr>"
        Else
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item(8) = "Sent" Or dt.Rows(i).Item(8) = "Test" Then
                    tbActivityBody.InnerHtml += "<tr><td>" & dt.Rows(i).Item(1) & "</td><td>" & dt.Rows(i).Item(2) & "</td><td>" & dt.Rows(i).Item(3) &
                    "</td><td>" & dt.Rows(i).Item(4) & "</td><td># " & dt.Rows(i).Item(0) & "</td><td>" & dt.Rows(i).Item(5).ToString() &
                    "</td><td><a title""Click to print ticket for customer to tender to destined organization."" " & Utility.getStatusStyle(dt.Rows(i).Item(8).ToString.ToLower) & "  style=""cursor:pointer"" onclick=""jPending('" & dt.Rows(i).Item(3) & "%" & dt.Rows(i).Item(0) & "%" & dt.Rows(i).Item(8) & "')"" >"
                    tbActivityBody.InnerHtml += "Ticket" & "</a>   |   <a " & Utility.getStatusStyle(dt.Rows(i).Item(8).ToString.ToLower) & " title""Click to print statement only if customer insist."" style=""cursor:pointer"" onclick=""jPending('" & dt.Rows(i).Item(3) & "%" & dt.Rows(i).Item(0) & "%" & "PrintS" & "')"" >"
                    tbActivityBody.InnerHtml += "Statement" & "</a></td></tr>"
                Else
                    tbActivityBody.InnerHtml += "<tr><td>" & dt.Rows(i).Item(1) & "</td><td>" & dt.Rows(i).Item(2) & "</td><td>" & dt.Rows(i).Item(3) &
                    "</td><td>" & dt.Rows(i).Item(4) & "</td><td># " & dt.Rows(i).Item(0) & "</td><td>" & dt.Rows(i).Item(5).ToString() &
                    "</td><td><a " & Utility.getStatusStyle(dt.Rows(i).Item(8).ToString.ToLower) & " style=""cursor:pointer"" onclick=""jPending('" & dt.Rows(i).Item(3) & "%" & dt.Rows(i).Item(0) & "%" & dt.Rows(i).Item(8) & "')"" >"
                    tbActivityBody.InnerHtml += dt.Rows(i).Item(8) & "</a></td></tr>"
                End If

            Next
            'If CDbl(dt.Rows(0).Item(7)) > 3 Then lblTop.Text = "3" Else lblTop.Text = dt.Rows(0).Item(7)
            lblTotal.Text = dt.Rows(0).Item(7)
        End If
    End Sub
    <WebMethod()>
    Public Shared Function FilterActivity(ByVal type_ As String) As String
        Dim dt As DataTable = Nothing
        Dim result As String = ""
        Dim row As String = ""
        Dim total As String = ""
        If HttpContext.Current.Session("Branch") Is Nothing Then HttpContext.Current.Response.Redirect("Login.aspx")


        dt = BLL._selectActivitybyFilter(HttpContext.Current.Session("Branch"), type_)

        If dt Is Nothing Then
            result = "<tr><td colspan=""8"">Error populating table</td></tr>"
            Return "0~/" & "Total~/" & result
        ElseIf dt.Rows.Count = 0 Then
            result = "<tr><td colspan=""8"">No statements have been generated by " & HttpContext.Current.Session("Branch").ToString & ".</td></tr>"
            Return "0~/" & "Total~/" & result

        Else
            row = dt.Rows.Count()
            total = dt.Rows(0).Item(7)
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item(8) = "Sent" Or dt.Rows(i).Item(8) = "Test" Then
                    result += "<tr><td>" & dt.Rows(i).Item(1) & "</td><td>" & dt.Rows(i).Item(2) & "</td><td>" & dt.Rows(i).Item(3) &
                   "</td><td>" & dt.Rows(i).Item(4) & "</td><td># " & dt.Rows(i).Item(0) & "</td><td>" & dt.Rows(i).Item(5).ToString() &
                   "</td><td><a " & Utility.getStatusStyle(dt.Rows(i).Item(8).ToString.ToLower) & " style=""cursor:pointer"" onclick=""jPending('" & dt.Rows(i).Item(3) & "%" & dt.Rows(i).Item(0) & "%" & dt.Rows(i).Item(8) & "')"" >"
                    result += "Ticket" & "</a><a " & Utility.getStatusStyle(dt.Rows(i).Item(8).ToString.ToLower) & " style=""cursor:pointer"" onclick=""jPending('" & dt.Rows(i).Item(3) & "%" & dt.Rows(i).Item(0) & "%" & "PrintS" & "')"" >"
                    result += "Statement" & "</a></td></tr>"
                Else
                    result += "<tr><td>" & dt.Rows(i).Item(1) & "</td><td>" & dt.Rows(i).Item(2) & "</td><td>" & dt.Rows(i).Item(3) &
                   "</td><td>" & dt.Rows(i).Item(4) & "</td><td># " & dt.Rows(i).Item(0) & "</td><td>" & dt.Rows(i).Item(5).ToString() &
                   "</td><td><a " & Utility.getStatusStyle(dt.Rows(i).Item(8).ToString.ToLower) & "style=""cursor:pointer"" onclick=""jPending('" & dt.Rows(i).Item(3) & "%" & dt.Rows(i).Item(0) & "%" & dt.Rows(i).Item(8) & "')"" >"
                    result += dt.Rows(i).Item(8) & "</a></td></tr>"
                End If

            Next
            Return row & "~/" & total & "~/" & result
        End If
    End Function
    <WebMethod()>
    Public Shared Function addApplicant(ByVal value As String) As String
        If HttpContext.Current.Session("REQUESTID") Is Nothing Then HttpContext.Current.Response.Redirect("Login.aspx")
        BLL._insertApplicant(value, HttpContext.Current.Session("REQUESTID"))
        Dim result As String = Nothing

        Dim dt As DataTable = BLL._selectAccountbyRequestID(HttpContext.Current.Session("REQUESTID"))

        result = "<tr> <td >Ticket No</td><td id=""tdTicket__""> " & dt.Rows(0).Item(0) & " </td></tr><tr>" &
                "<td >Account No</td><td > " & dt.Rows(0).Item(1) & " </td></tr><tr><td >Account Name</td>" &
            "<td id=""tdAcctName_"">" & dt.Rows(0).Item(2) & " </td></tr><tr><td >Address</td>" &
            "<td id=""tdAddress_"">" & dt.Rows(0).Item(8) & "</td></tr><tr><td>Signatories</td><td>"
        Dim dt1 As DataTable = BLL._selectSignatoriesNew(dt.Rows(0).Item(0))
        'Dim dt1 As DataTable = HttpContext.Current.Session("tblSignatories")
        'Dim dt1 As DataTable = DALOracle.GetSignatories(dt.Rows(0).Item(1))

        '  Dim dt1 As DataTable
        ' If HttpContext.Current.Session("tblSignatories") Is Nothing Then
        '  dt1 = DALOracle.GetSignatories(dt.Rows(0).Item(1))
        'Else
        '    dt1 = HttpContext.Current.Session("tblSignatories")
        'End If

        If dt1.Rows.Count = 0 Then
            result += "<i>No signatory</i>"
        Else
            For i As Integer = 0 To dt1.Rows.Count - 1
                result += dt1.Rows(i).Item(0) & "<br />"
            Next
            result = result.Substring(0, result.Length - 6)
        End If
        result += "</td></tr><tr><td >Type</td><td>" & dt.Rows(0).Item(3) & "</td></tr><tr><td>Category</td>" &
        "<td>" & dt.Rows(0).Item(4) & "</td></tr><tr><td>Cleared balance</td><td> " & dt.Rows(0).Item(10) & " " & CDbl(dt.Rows(0).Item(5)).ToString("#,##0.00") &
        "</td></tr><tr><td >Available balance</td><td> " & dt.Rows(0).Item(10) & " " & CDbl(dt.Rows(0).Item(6)).ToString("#,##0.00") & "</td> </tr>"



        Dim dt2 As DataTable = BLL._selectApplicant(HttpContext.Current.Session("REQUESTID"))
        If dt2.Rows.Count = 0 Then
            result += "%<tr><td style=""padding:0px""><i>No applicant</i></td></tr>"
        Else
            For i As Integer = 0 To dt2.Rows.Count - 1
                If i = 0 Then result += "%"
                result += "<tr><td style=""padding:0px"">" & dt2.Rows(i).Item(1) & "</td><td style=""padding:0px 0px 0px10px""><img id=""imgDelete" & dt2.Rows(i).Item(0) &
                        """ onclick=""jDeleteApplicant('" & dt2.Rows(i).Item(0) & "_" & HttpContext.Current.Session("REQUESTID") & "')"" src=""Delete.gif"" height=""15px"" width=""15px"" style=""cursor:pointer""/> </td></tr>"
            Next
        End If
        'Return String.Format("index: {0}{2}Value: {1}", index, result, Environment.NewLine)
        Return result
    End Function
   <WebMethod()>
    Public Shared Function _ApprovalInfo(ByVal ticket As String) As String
        Dim customerBal As Double = 500000.0
        Dim sendStatus As String = ""
        HttpContext.Current.Session("REQUESTID") = ticket
        ticket = Trim(ticket)
        Try
            Dim result As String = ""
            'If (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~\Authorization\" + ticket + "_wm.pdf"))) Then
            '    result += "%" & "Authorization/" & ticket & "_wm.pdf"
            'Else
            result += "%" & "Authorization/" & ticket & ".pdf"
            ' End If

            Dim dt7 As DataTable = BLL._ApprovalInfo(ticket)
            If customerBal >= CDbl(dt7.Rows(0).Item(16).ToString) Then
                sendStatus = "1"
            Else
                sendStatus = "0"
            End If
            Dim tdWaive As String = "No"
            If dt7.Rows(0).Item("waiveCharge").ToString = "1" Then
                tdWaive = "Yes"
            End If
            If result.Split("%")(0) = "0" Then
                Return "False%" & result.Split("%")(1)
            Else
                result = "True%" & result.Split("%")(1) & "%" & dt7.Rows(0).Item(0) & "%" & HttpUtility.HtmlDecode(dt7.Rows(0).Item(1)) & "%" & dt7.Rows(0).Item(2) & "-14" & "%" & dt7.Rows(0).Item(3)

                Dim dt2 As DataTable = BLL._selectApplicant(ticket)
                If dt2.Rows.Count = 0 Then
                    result += "%"
                Else
                    For i As Integer = 0 To dt2.Rows.Count - 1
                        If i = 0 Then result += "%"
                        result += HttpUtility.HtmlDecode(dt2.Rows(i).Item(1)) & "<br />"
                    Next
                End If
                result += "%" & dt7.Rows(0).Item(4) & "%" & dt7.Rows(0).Item(5) & "%" & dt7.Rows(0).Item(6)

                Dim dt1 As DataTable = BLL._selectSignatoriesNew(ticket)
                ' Dim dt1 As DataTable = DALOracle.GetSignatories(dt7.Rows(0).Item(0))
                If dt1.Rows.Count = 0 Then
                    result += "%<i>No signatory</i>"
                Else
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        If i = 0 Then result += "%"
                        result += HttpUtility.HtmlDecode(dt1.Rows(i).Item(0)) & "<br />"
                    Next
                    result = result.Substring(0, result.Length - 6)
                End If


                result += "%" & dt7.Rows(0).Item(7) & "%" & dt7.Rows(0).Item(8) &
                    "%" & dt7.Rows(0).Item(19) & " " & CDbl(dt7.Rows(0).Item(9)).ToString("#,##0.00") & "%" & dt7.Rows(0).Item(19) & " " & CDbl(dt7.Rows(0).Item(10)).ToString("#,##0.00") & "%" &
                    dt7.Rows(0).Item(19) & " " & CDbl(dt7.Rows(0).Item(11)).ToString("#,##0.00") & "%" _
                    & dt7.Rows(0).Item(19) & " " & CDbl(dt7.Rows(0).Item(12)).ToString("#,##0.00") & "%" _
                    & CDbl(dt7.Rows(0).Item(13)).ToString("#,##0") & "%" _
                    & "NGN " & CDbl(dt7.Rows(0).Item(14)).ToString("#,##0.00") & "%" & "NGN " & CDbl(dt7.Rows(0).Item(15)).ToString("#,##0.00") &
                    "%" & "NGN " & CDbl(dt7.Rows(0).Item(16)).ToString("#,##0.00") & "%" & "NGN " &
                    CDbl(dt7.Rows(0).Item(17)).ToString("#,##0.00") & "%" & sendStatus & "%" & CDbl(BLL._getCostPerPage()).ToString("#,##0.00") & "%" &
                tdWaive & "%" & dt7.Rows(0).Item("debitAccount")
                Return result
            End If
        Catch ex As Exception
            Return "False%" & ex.Message
        End Try
    End Function

    <WebMethod()>
    Public Shared Function _MakePayment(ByVal ticket As String) As String
        'customerBal can be gotten from account enquiry. else, it will be detected during billing
        'Dim customerBal As Double = 50000000.0
        'Dim sendStatus As String = ""
        '  Dim solID As String = BLL._selectSolIDFromBranchID(HttpContext.Current.Session("USERID"))
        Dim wqAmount As Decimal
        Dim DiamondAMount As Decimal
        HttpContext.Current.Session("REQUESTID") = ticket
        Dim newTicket As String = BLL._SelectTicketStatusFromTicketID(ticket)
        Try
            Dim result As String = ""
            'result += "%" & "Authorization/" & ticket & ".pdf"

            Dim dt7 As DataTable = BLL._ApprovalInfo(ticket)
            'If customerBal >= CDbl(dt7.Rows(0).Item(16).ToString) Then
            '    sendStatus = "1"
            'Else
            '    sendStatus = "0"
            'End If
            Dim validPrice As String = BLL._selectvalidprice()

            If validPrice.Split("|")(1).ToString = "0" Then
                wqAmount = (CDbl(dt7.Rows(0).Item("page")) * CDec(validPrice.Split("|")(4)) + (0.7 * CDec(dt7.Rows(0).Item("additional")))) * 1.05
            Else
                wqAmount = (CDec(validPrice.Split("|")(4)) + 0.7 * (CDec(dt7.Rows(0).Item("total")))) * 1.05
            End If
            DiamondAMount = ((CDec(validPrice.Split("|")(0)) - CDec(validPrice.Split("|")(4))) * CDbl(dt7.Rows(0).Item("page")) + 0.3 * (CDec(dt7.Rows(0).Item("additional"))))
            Dim vatAmount As Decimal = CDec(dt7.Rows(0).Item("total")) - (wqAmount + DiamondAMount)

            If dt7.Rows(0).Item(0).ToString = "0029846503" Then
                If newTicket = "1" Then
                    result = "True%" & "ticket/" & ticket & ".pdf"
                Else
                    result = "True%" & "receipt/" & ticket & ".pdf"
                End If
                BLL._UpdateEmailAndSMSAlert(ticket, "1")
                BLL._updateTestCharge(ticket)
                BLL._insertAuditLogsBranch(ticket, dt7.Rows(0).Item(3), HttpContext.Current.Session("Role"), ticket, "Sent", Now, HttpContext.Current.Session("Branch"))

            Else

                Dim billRes As String = ""
                If wqAmount = 0 Then
                    billRes = DAL_API.Billing(dt7.Rows(0).Item(0), CDbl(dt7.Rows(0).Item(16)),
                                                        WebConfigurationManager.AppSettings("DiamondAcctNo").ToString(), DiamondAMount,
                                               WebConfigurationManager.AppSettings("VATAcctNo").ToString(), vatAmount,
                                                        ticket.ToString, dt7.Rows(0).Item(0) & "/mybankStatement STMNT CHRG")

                Else
                    billRes = DAL_API.Billing(dt7.Rows(0).Item(0), CDbl(dt7.Rows(0).Item(16)),
                                                        WebConfigurationManager.AppSettings("wqAcctNo").ToString(), wqAmount,
                                                        WebConfigurationManager.AppSettings("DiamondAcctNo").ToString(), DiamondAMount,
                                                        WebConfigurationManager.AppSettings("VATAcctNo").ToString(), vatAmount, ticket.ToString, dt7.Rows(0).Item(0) & "/mybankStatement STMNT CHRG")

                End If

                If billRes.ToLower = "success" Then
                    Dim sentResult As String = sendRequestToService(ticket)
                    If sentResult.Split("%")(0) = "True" Then
                        If newTicket = "1" Then
                            result = "True%" & "ticket/" & ticket & ".pdf"
                        Else
                            result = "True%" & "receipt/" & ticket & ".pdf"
                        End If

                        BLL._updateStatus(ticket, "SENT")
                        BLL._insertAuditLogsBranch(HttpContext.Current.Session("USERID"), dt7.Rows(0).Item(3), HttpContext.Current.Session("Role"), ticket, "Sent", Now, HttpContext.Current.Session("Branch"))
                        BLL._UpdateEmailAndSMSAlert(ticket.ToString, "1")
                    Else
                        BLL._updateStatus(ticket, "NOT SENT")
                        BLL._insertAuditLogsBranch(HttpContext.Current.Session("USERID"), dt7.Rows(0).Item(3), HttpContext.Current.Session("Role"), ticket, "Not Sent", Now, HttpContext.Current.Session("Branch"))
                        'BLL._UpdateEmailAndSMSAlert(ticketNo.ToString, "1")
                    End If
                ElseIf billRes.ToLower = "insfund" Then
                    BLL._updateStatus(ticket, "Make Payment")
                    BLL._insertAuditLogsBranch(HttpContext.Current.Session("USERID"), dt7.Rows(0).Item(3), HttpContext.Current.Session("Role"), HttpContext.Current.Session("REQUESTID"), "Transaction Failed due to insufficient fund", Now, HttpContext.Current.Session("Branch"))
                    Return "False%" & "Insufficient Fund"

                Else
                    BLL._updateStatus(ticket, "Make Payment")
                    BLL._insertAuditLogsBranch(HttpContext.Current.Session("USERID"), dt7.Rows(0).Item(3), HttpContext.Current.Session("Role"), HttpContext.Current.Session("REQUESTID"), "Transaction Failed (Service)", Now, HttpContext.Current.Session("Branch"))
                    Return "False%" & "Error :" + billRes
                End If
            End If
            Return result
        Catch ex As Exception
            Return "False%" & ex.Message
        End Try
    End Function
    <WebMethod()>
    Public Shared Function LoadDestinations(country As String, ByVal value As String) As String
        Dim result As String = Nothing

        Dim dt2 As New DataTable
        dt2.Load(BLL._selectDestinationByID(country, value))


        For i As Integer = 0 To dt2.Rows.Count - 1
            If i = 0 Then result += "<option value='0'>Choose...</option>"

            result &= "<option value='" & dt2.Rows(i).Item("ID").ToString & "'>" & dt2.Rows(i).Item("Name").ToString & "</option> "
        Next

        Return result
    End Function
    <WebMethod()>
    Public Shared Function getDestination(ByVal category As String) As List(Of System.Web.UI.WebControls.ListItem)

        Dim customers As New List(Of System.Web.UI.WebControls.ListItem)()

        Using sdr As SqlDataReader = BLL._selectDestinationByID(Utility.getCountryCode2FromCountry(HttpContext.Current.Session("Country")), category)
            While sdr.Read()
                customers.Add(New System.Web.UI.WebControls.ListItem() With {
                  .Value = sdr("ID").ToString(),
                  .Text = sdr("Name").ToString()
                })
            End While
        End Using

        Return customers

    End Function
    <WebMethod()>
    Public Shared Function Ticket(ByVal value As String) As String
        Dim result As String = ""

        result = BLL._SelectTicketStatusFromTicketID(value)
        Return result
    End Function

    <WebMethod()>
    Public Shared Function openTicket(ByVal value As String) As String
        Dim result As String = ""

        'result = BLL._SelectTicketStatusFromTicketID(value)
        Return result
    End Function
   <WebMethod()>
    Public Shared Function Pending(ByVal index As String, ByVal value As String) As String

        HttpContext.Current.Session("REQUESTID") = value.Split("%")(1)
        Dim result As String = Nothing
        Dim dt As DataTable = BLL._selectAccount(value.Split("%")(0), HttpContext.Current.Session("USERID"), HttpContext.Current.Session("NAME"), value.Split("%")(1), HttpContext.Current.Session("Branch"))

        If dt.Rows.Count = 0 Then
            result = "False%<tr><td colspan=""2"">No existing account for " & value & ".</td></tr>"
            HttpContext.Current.Session("tbAccount") = result.Replace("False%", "")
        Else
            result = "True%<tr> <td >Ticket No</td><td id=""tdTicket__""> " & dt.Rows(0).Item(0) & " </td> </tr><tr>" &
                "<td >Account No</td><td > " & dt.Rows(0).Item(1) & " </td> </tr><tr><td >Account Name</td>" &
            "<td id=""tdAcctName_"">" & dt.Rows(0).Item(2) & " </td></tr><tr><td >Address</td>" &
            "<td id=""tdAddress_"">" & dt.Rows(0).Item(12) & "</td></tr><tr><td>Signatories</td><td>"
            ' Dim dt1 As DataTable = DALOracle.GetSignatories(dt.Rows(0).Item(1))



            Dim dt1 As DataTable = BLL._selectSignatoriesNew(dt.Rows(0).Item(0))
            ' Dim dt1 As DataTable = HttpContext.Current.Session("tblSignatories")
            If dt1.Rows.Count = 0 Then
                result += "<i>No signatory</i>"
            Else
                For i As Integer = 0 To dt1.Rows.Count - 1
                    result += dt1.Rows(i).Item(0) & "<br />"
                Next
                result = result.Substring(0, result.Length - 6)
            End If
            result += "</td></tr><tr><td >Type</td><td>" & dt.Rows(0).Item(3) & "</td></tr><tr><td>Category</td>" &
            "<td>" & dt.Rows(0).Item(4) & "</td></tr><tr><td>Cleared balance</td><td> " & dt.Rows(0).Item(14) & " " & CDbl(dt.Rows(0).Item(5)).ToString("#,##0.00") &
            "</td></tr><tr><td >Available balance</td><td> " & dt.Rows(0).Item(14) & " " & CDbl(dt.Rows(0).Item(6)).ToString("#,##0.00") & "</td> </tr>"
            Dim dt2 As DataTable = BLL._selectApplicant(dt.Rows(0).Item(0))
            HttpContext.Current.Session("tbAccount") = result.Replace("True%", "")
            If dt2.Rows.Count = 0 Then
                result += "%<tr><td style=""padding:0px""><i>No applicant</i></td></tr>"

            Else
                For i As Integer = 0 To dt2.Rows.Count - 1
                    If i = 0 Then result += "%"
                    result += "<tr><td style=""padding:0px"">" & dt2.Rows(i).Item(1) &
                        "</td><td style=""padding:0px 0px 0px 10px""><img style=""cursor:pointer"" id=""imgDelete" & dt2.Rows(i).Item(0) & """ src=""Delete.gif"" onclick=""jDeleteApplicant('" & dt2.Rows(i).Item(0) &
                        "_" & dt.Rows(0).Item(0) & "')"" height=""15px"" width=""15px""/> </td></tr>"
                Next
            End If
        End If
        Return result & "%" & dt.Rows(0).Item(8) & "%" & dt.Rows(0).Item(9) & "%" & dt.Rows(0).Item(10) & "%" & dt.Rows(0).Item(11)

    End Function
    <WebMethod()>
    Public Shared Function deleteApplicant(ByVal value As String) As String
        Dim result As String = Nothing
        BLL._deleteApplicant(value.Split("_")(0))
        Dim dt2 As DataTable = BLL._selectApplicant(value.Split("_")(1))
        If dt2.Rows.Count = 0 Then
            result += "<tr><td style=""padding:0px""><i>No applicant</i></td></tr>"
        Else
            For i As Integer = 0 To dt2.Rows.Count - 1
                If i = 0 Then result += ""
                result += "<tr><td style=""padding:0px"">" & dt2.Rows(i).Item(1) & "</td><td style=""padding:0px 0px 0px 10px""><img id=""imgDelete" & dt2.Rows(i).Item(0) &
                    """ onclick=""jDeleteApplicant('" & dt2.Rows(i).Item(0) & "_" & value.Split("_")(1) & "')"" src=""Delete.gif"" height=""15px"" width=""15px"" style=""cursor:pointer""/> </td></tr>"
            Next
        End If
        Return result
    End Function

    <WebMethod()>
    Public Shared Function mailOption(ByVal value As String) As String

        Dim result As String = BLL._SelectMailOption(value.Split("-")(0)).Split("_")(1)


        Return result
    End Function
    <WebMethod()>
    Public Shared Function ViewComment(ByVal ticket As String) As String

        Dim result As String = BLL._selectDeclinedComment(ticket)
        If result Is Nothing Then
            result = "Comment not available"
        End If

        Return result
    End Function
    <WebMethod()>
    Public Shared Function Recall(ByVal id As String) As String
        Return BLL._updateStatus(id, "Pending")
    End Function

    <WebMethod()>
    Public Shared Function sendStatement(ByVal value As String) As String
        Dim result As String = Nothing
        Dim ticketNo As String = value.Split("_")(4)
        ' If ticketNo Is Nothing Then HttpContext.Current.Response.Redirect("Login.aspx")
        Dim dtDetails As DataRow = BLL._selectActivitybyRequestID(ticketNo).Rows(0)
        Dim wqAmount As Decimal = 0.0
        Dim custAmount As Decimal = CDec(dtDetails.Item(21))
        ' Dim solID As String = BLL._selectSolIDFromBranchID(HttpContext.Current.Session("USERID"))
        ' wqAmount = dtDetails.Item(20) + (CDbl(BLL._selectvalidprice().Split("|")(4)) * dtDetails.Item(18))
        Dim DiamondAmount As Decimal = custAmount
        If value.Split("_")(3) = "1" Then
            BLL._UpdateMailOption(ticketNo, "1")
        End If
        If BLL._selectCheckerstatus = "0" Or HttpContext.Current.Session("Role") = "Approver" Then
            If value.Split("_")(2).ToLower = "print" Or value.Split("_")(2).ToLower = "awt. print" Then


            ElseIf value.Split("_")(2).ToLower = "e-mail" Or value.Split("_")(2).ToLower = "awt. mail" Then

            Else
                Dim newTicket As String = BLL._SelectTicketStatusFromTicketID(ticketNo)
                Dim validPrice As String = BLL._selectvalidprice()
                If validPrice.Split("|")(1).ToString = "0" Then
                    wqAmount = (CDec(dtDetails.Item(18)) * CDec(validPrice.Split("|")(4)) + (0.7 * CDec(dtDetails.Item(20)))) ' * 1.05
                Else
                    wqAmount = (CDec(validPrice.Split("|")(4)) + 0.7 * (CDec(dtDetails.Item(20)))) '* 1.05
                End If
                DiamondAmount = ((CDec(validPrice.Split("|")(0)) - CDec(validPrice.Split("|")(4))) * CDec(dtDetails.Item(18)) + 0.3 * (CDec(dtDetails.Item(20))))
                Dim vatAmount As Decimal = CDec(dtDetails.Item(21)) - (wqAmount + DiamondAmount)

                If dtDetails.Item(3).ToString = "0029846503" Then
                    Dim sentResult As String = sendRequestToService(ticketNo)
                    If sentResult.Split("%")(0) = "True" Then
                        If newTicket = "1" Then
                            result = "True%" & "ticket/" & ticketNo & ".pdf"
                        Else
                            result = "True%" & "receipt/" & ticketNo & ".pdf"
                        End If
                        BLL._UpdateEmailAndSMSAlert(ticketNo.ToString, "1")
                        ' BLL._updateStatus(ticketNo, "Test")
                        BLL._updateTestCharge(ticketNo)
                        BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), value.Split("_")(1), HttpContext.Current.Session("Role"), ticketNo, "Sent", Now)

                    End If

                Else
                    Dim billRes As String = ""
                    If wqAmount = 0 Then
                        billRes = DAL_API.Billing(dtDetails.Item(3), custAmount - vatAmount,
                                                        WebConfigurationManager.AppSettings("DiamondAcctNo").ToString(), DiamondAmount,
                                                   WebConfigurationManager.AppSettings("VATAcctNo").ToString(), vatAmount,
                                                        ticketNo.ToString, dtDetails.Item(3) & "/mybankStatement STMNT CHRG")

                    Else
                        billRes = DAL_API.Billing(dtDetails.Item(3), custAmount,
                                                        WebConfigurationManager.AppSettings("wqAcctNo").ToString(), wqAmount,
                                                        WebConfigurationManager.AppSettings("DiamondAcctNo").ToString(), DiamondAmount,
                                                        WebConfigurationManager.AppSettings("VATAcctNo").ToString(), vatAmount, ticketNo.ToString, dtDetails.Item(3) & "/mybankStatement STMNT CHRG")

                    End If
                    If billRes.ToLower = "success" Then
                        Dim sentResult As String = sendRequestToService(ticketNo)
                        If sentResult.Split("%")(0) = "True" Then
                            If newTicket = "1" Then
                                result = "True%" & "ticket/" & ticketNo & ".pdf"
                            Else
                                result = "True%" & "receipt/" & ticketNo & ".pdf"
                            End If

                            BLL._updateStatus(ticketNo, "SENT")
                            BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), value.Split("_")(1), HttpContext.Current.Session("Role"), ticketNo, "Sent", Now)
                            BLL._UpdateEmailAndSMSAlert(ticketNo.ToString, "1")
                        Else
                            BLL._updateStatus(ticketNo, "NOT SENT")
                            BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), value.Split("_")(1), HttpContext.Current.Session("Role"), ticketNo, "Not Sent", Now)
                            'BLL._UpdateEmailAndSMSAlert(ticketNo.ToString, "1")
                        End If

                    ElseIf billRes.ToLower = "insfund" Then
                        BLL._updateStatus(ticketNo, "Make Payment")
                        BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), value.Split("_")(1), HttpContext.Current.Session("Role"), ticketNo, "Transaction Failed. Insufficient fund (Diamond Billing Service)", Now)
                        Return "False%" & "Insufficient Fund"
                    Else
                        BLL._updateStatus(ticketNo, "Make Payment")
                        BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), value.Split("_")(1), HttpContext.Current.Session("Role"), ticketNo, "Transaction Failed (Diamond Billing Service)", Now)
                        result = "False%" & "Transaction Failed due to " + billRes
                    End If
                End If

                Return result
            End If
        Else
            If value.Split("_")(2).ToLower = "print" Or value.Split("_")(2).ToLower = "awt. print" Then
                BLL._updateStatus(ticketNo, "Awt. Print")
                BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), value.Split("_")(1), HttpContext.Current.Session("Role"), ticketNo, "Print Approval Request", Now)
                Try

                    If HttpContext.Current.Session("ApprovalInfo") IsNot Nothing Then

                        Dim subject As String = "Bank statement requires approval"
                        Dim body As String = "Dear " & HttpContext.Current.Session("ApprovalInfo").ToString.Split("%")(0) & ", <br /><br />  A bank statement has been sent to you from <b>" & HttpContext.Current.Session("Name") & "</b> with ticket ID  <b>" & ticketNo & " for approval to print.<br /><br /> Login at http://10.0.0.229:91/Login.aspx <br /><br />Regards"
                        BLL._insertApprovalMailAlert(HttpContext.Current.Session("ApprovalInfo").ToString.Split("%")(1), HttpContext.Current.Session("ApprovalInfo").ToString.Split("%")(0), HttpContext.Current.Session("Name"), HttpContext.Current.Session("USERID"), ticketNo, "PRINT", subject, body, "1")
                    End If

                Catch
                    Dim ds As New DashBoard
                    Utility.ShowSweet(ds.Page, "An email could not be sent for approval")
                End Try

                Return "Approval%Printing of "
            ElseIf value.Split("_")(2).ToLower = "e-mail" Or value.Split("_")(2).ToLower = "awt. mail" Then
                BLL._updateStatus(ticketNo, "Awt. Mail")

                Try
                    If HttpContext.Current.Session("ApprovalInfo") IsNot Nothing Then
                        Dim subject As String = "Bank statement requires approval"
                        Dim body As String = "Dear " & HttpContext.Current.Session("ApprovalInfo").ToString.Split("%")(0) & ", <br /><br />  A bank statement has been sent to you from <b> http://10.0.0.229:91/Login.aspx <br /><br />Regards"

                        BLL._insertApprovalMailAlert(HttpContext.Current.Session("ApprovalInfo").ToString.Split("%")(1), HttpContext.Current.Session("ApprovalInfo").ToString.Split("%")(0), HttpContext.Current.Session("Name"), HttpContext.Current.Session("USERID"), ticketNo, "EMAIL", subject, body, "1")

                    End If
                Catch ex As Exception
                    Dim ds As New DashBoard
                    Utility.ShowSweet(ds.Page, "An email could not be sent for approval")
                End Try
                Return "Approval%Emailing to "
            Else
                BLL._updateStatus(ticketNo, "Awt. Sent")
                BLL._insertAuditLogs(HttpContext.Current.Session("USERID"), value.Split("_")(1), HttpContext.Current.Session("Role"), ticketNo, "Sent Approval Request", Now)
                Try
                    If HttpContext.Current.Session("ApprovalInfo") IsNot Nothing And  Utility.ValidateEmail(HttpContext.Current.Session("ApprovalInfo")) = true Then
                        Dim subject As String = "Bank statement requires approval"
                        Dim body As String = "Dear User " &  ", <br /><br />  A bank statement has been sent to you from <b>" & HttpContext.Current.Session("Name") & "</b> with ticket ID  <b>" & ticketNo & " for approval.<br /><br /> Login at http://10.0.0.229:91/Login.aspx <br /><br />Regards"

                       BLL._insertApprovalMailAlert("User", HttpContext.Current.Session("ApprovalInfo").ToString, HttpContext.Current.Session("Name"), HttpContext.Current.Session("USERID"), ticketNo, "SENT", subject, body, "1")

                    End If
                Catch ex As Exception
                    Dim ds As New DashBoard
                    Utility.ShowSweet(ds.Page, "Approval email not sent. Please inform your line manager")
                End Try
                Return "Approval%"
            End If

        End If

    End Function
    Shared Function sendRequestToService(ByVal ticketNo As String) As String
        Try
            Dim oFileInfo As FileInfo = New FileInfo("D:\mybankStatementRepository\statement\" & ticketNo & ".pdf")
            'put condition for sending CSV
            Dim sendToService As New SendToService(oFileInfo, ticketNo.ToString)
            Dim outcome As String = sendToService.SendPDF()
            '     Dim outcome As String = BLL.doFileUploadViaWebService(oFileInfo, ticketNo)            
            If outcome.ToLower = "file sent successfully" Or outcome.ToLower = "file delivered successfully" Then
                Dim statusCSV As String
                statusCSV = BLL._selectCSVStatus(ticketNo)

                If statusCSV = "1" Then
                    Dim csvFileInfo As FileInfo = New FileInfo(("D:\mybankStatementRepository\statementCSV\" & ticketNo & ".zip"))
                    Dim sendCsvToService As New SendCSVtoService(csvFileInfo, ticketNo)
                    sendCsvToService.doCSVFileUploadViaWebService()
                End If
                If BLL._GetFormat("1", ticketNo) = "1" Then
                    Dim jsonFileInfo As FileInfo = New FileInfo(("D:\mybankStatementRepository\statementJSON\" & ticketNo & ".zip"))
                    Dim sendJsonToService As New SendJsonToService(jsonFileInfo, ticketNo)
                    sendJsonToService.doJSONFileUploadViaWebService()
                End If
                Return "True%" & outcome
            Else
                Return "False%" & outcome
            End If

        Catch ex As Exception
            Return "False%" & ex.Message()
        End Try
    End Function
    <WebMethod()>
    Public Shared Function _updateRequest(ByVal startDate As String, ByVal endDate As String, ByVal Destination As String, ByVal Role As String, ByVal Name As String, ByVal Address As String, ByVal Nuban As String, ByVal ticketNo As String, ByVal waive As String, ByVal debitAccount As String) As String
        Dim result As String = ""
        Dim cr As Double = 0
        Dim dr As Double = 0
        Dim PageCount As Integer = 0
        Dim vat As Double = 0.0
        Dim Basic As Double = 0
        Dim Additional As Double = 0
        Dim Total As Double = 0
        Dim Unitprice As Double
        Dim totalCharge As Double
        Dim PriceMode As String
        Dim noOfPages As Int16 = 0
        Dim validPrice As String = ""
        Dim dt7 As DataTable = Nothing
        ticketNo = Trim(ticketNo)
        Try
            'Dim acctdetail As DataTable = DAL_API.AccountDetails(Nuban)
            'Dim CustomerBal As Double = CDbl(acctdetail.Rows(0).Item(9))
            Dim dt2 As DataTable

            dt2 = BLL._selectActivitybyRequestID(ticketNo)
            ' End If
            Dim dt As DataTable = Nothing
            'Dim tranDetails As String = "{""Details"": [{ ""AccountNumber"": ""1111111111"", ""Narration"": ""Opening Balance"", ""TransactionDate"": ""2018-02-27T13:33:17.265Z"", ""ValueDate"": ""2018-02-27T13:33:17.265Z"",""DebitAmount"": 0, ""CreditAmount"": 0,  ""TransactionBalance"": 0 }], ""ResponseCode"": ""OO"", ""ResponseMessage"": ""string""}"
            Dim serv As New DiamondService.Service()
            Dim resArr() As String = serv.getTransactionDetails(Trim(Nuban), Convert.ToDateTime(startDate), Convert.ToDateTime(endDate))
            If resArr(0) = "00" Then



                Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(resArr(1))
                Dim allData As JObject = JObject.Parse(j)
                dt = Utility.jsonToDataTable(JsonConvert.SerializeObject(allData("Details")))

                ' dt = BLL._selectTransactionsTest(CDate(startDate), CDate(endDate), dt2.Rows(0).Item("NUBAN".ToString))
                ' dt = DAL_API.TransactionDetails(dt2.Rows(0).Item("NUBAN"), CDate(startDate).ToString("dd-MMM-yyyy"), CDate(endDate).ToString("dd-MMM-yyyy"))
                '  End If
                If dt Is Nothing Then
                    result = "0%" & "Cannot fetch transaction details from core banking. If persist contact IT for support. Details: " & j("ResponseMessage")
                ElseIf dt.Rows.Count = 0 Then
                    result = "0%" & "No transactions available for " & Name & " from " & startDate & " to " & endDate
                Else
                    Dim output_ As String() = PDF.generatePDF(dt, dt2, startDate, endDate, Destination, Role, Name, Address, ticketNo.ToString())
                    validPrice = BLL._selectvalidprice()
                    Unitprice = CDbl(validPrice.Split("|")(0)) - CDbl(validPrice.Split("|")(4))
                    PriceMode = validPrice.Split("|")(1)
                    'Note that acct category is item(7) for print and emial while it is item(9) for sent to service
                    If dt2.Rows(0).Item(7).ToString.ToLower = "ind_stf" Or dt2.Rows(0).Item(7).ToString.ToLower = "ind_mgt" Then
                        noOfPages = 0
                    ElseIf PriceMode = "1" Then
                        noOfPages = 1
                    Else
                        noOfPages = output_(2)
                    End If
                    'The 0 and "" represent print ehile email represnt the mail option
                    If Destination = "0" Or Destination = "" Or Destination.ToLower = "e-mail" Then
                        '  Dim details As String() = BLL._selectDestinationName(Destination)
                        Basic = noOfPages * Unitprice
                        ' Additional = details(1)
                        Additional = 0.0
                        totalCharge = Basic + Additional
                        ' vat = (noOfPages * Unitprice * 0.05)
                        Total = (Basic + Additional) * 1.05
                        vat = (Basic + Additional) * 0.05
                        dt7 = BLL._updateRequest(CDate(startDate), CDate(endDate), Destination, Role, ticketNo,
                                                     output_(0), output_(1), output_(2), Basic, Additional, Total, vat)
                    Else
                        Dim details As String() = BLL._selectDestinationName(Destination)
                        'Billing. when account holder is a staff and waiver is enabled
                        'If dt2.Rows(0).Item(9).ToString.ToLower = "ind_stf" Or dt2.Rows(0).Item(9).ToString.ToLower = "ind_mgt" Then
                        '    Unitprice = validPrice.Split("|")(4)
                        'End If
                        Basic = output_(2) * Unitprice
                        Additional = CDec(details(1))
                        totalCharge = Basic + Additional
                        Total = (Basic + Additional) * 1.05
                        vat = (Basic + Additional) * 0.05
                        dt7 = BLL._updateRequest(CDate(startDate), CDate(endDate), Destination, Role, ticketNo,
                                                     output_(0), output_(1), output_(2), Basic, Additional, Total, vat, waive, debitAccount)
                        'Dim senderid As Integer = 0

                        'Dim drSender As SqlDataReader = BLL._selectCompany()
                        'While drSender.Read()
                        '    If Not IsDBNull(drSender.Item("Domain")) Then
                        '        senderid = drSender.Item("SenderID")
                        '    End If
                        'End While
                        Dim ticket As New GenerateTicket(ticketNo, Name, details(0), HttpContext.Current.Session("SenderID"), Nothing, output_(3))
                        ticket.getTicket()
                        ' PDF.generatePDFReceipt(ticketNo, Name, details(0), Destination)
                        If details(3) = "1" Then
                            Utility.MergeTwoPdfsToSingle(("D:\mybankStatementRepository\statement\" & ticketNo.ToString() + ".pdf"), System.Web.Hosting.HostingEnvironment.MapPath("~\receipt\" & ticketNo.ToString() + ".pdf"), System.Web.Hosting.HostingEnvironment.MapPath("~\ticket\" & ticketNo.ToString() + ".pdf"), HttpContext.Current.Session("PASSCODE"))
                        End If
                        If details(2) = "True" Then
                            Try
                                Dim csv As New GenerateCSV(dt, dt2, startDate, endDate, Destination, Role, Name, Address, ticketNo, "bank", output_(3), Utility.getCountryCodeFromCountry(HttpContext.Current.Session("Country")))
                                csv.generateAndSaveZipedPasswordedCSV()
                            Catch ex As Exception

                            End Try



                        End If

                        'Json Format
                        If BLL._GetFormat("1", ticketNo) = "1" Then
                            Try
                                Dim json As New GenerateJson(dt, dt2, startDate, endDate, Destination, Name, ticketNo, "bank", Nothing, output_(3), Utility.getCountryCodeFromCountry(HttpContext.Current.Session("Country")))

                                json.generateAndSaveZipedPasswordedJSON()

                            Catch ex As Exception
                            End Try
                        End If

                    End If
                    '   PDF.AddWaterMark(ticketNo.ToString())
                    result += "%" & "Authorization/" & ticketNo & ".pdf"
                    'Utility.RotatePDF(Server.MapPath("~\receipt\" & ticketNo.ToString() + ".pdf"), Server.MapPath("~\RotateTicket\" & ticketNo.ToString() + ".pdf"))
                    'Utility.RotatePDFPassWord(Server.MapPath("~\statement\" & ticketNo.ToString() + ".pdf"), Server.MapPath("~\RotateStatement\" & ticketNo.ToString() + ".pdf"), HttpContext.Current.Session("PASSCODE"))

                End If
                'The argument required is the format ID


                Dim sendStatus As String = "1"
                'If CustomerBal >= Total Then
                '    sendStatus = "1"
                'Else
                '    sendStatus = "0"
                'End If
                If debitAccount = "" Then
                    debitAccount = Nuban
                End If
                Dim tdWaive As String = "No"
                If waive = 1 Then
                    tdWaive = "Yes"
                End If
                If result.Split("%")(0) = "0" Then
                    Return "False%" & result.Split("%")(1)
                Else
                    result = "True%" & result.Split("%")(1) & "%" & dt7.Rows(0).Item(0) & "%" & HttpUtility.HtmlDecode(dt7.Rows(0).Item(1)) & "%" & dt7.Rows(0).Item(2) & "-14" & "%" & dt7.Rows(0).Item(3)

                    Dim dtApplicant As DataTable = BLL._selectApplicant(ticketNo)
                    If dtApplicant.Rows.Count = 0 Then
                        result += "%"
                    Else
                        For i As Integer = 0 To dtApplicant.Rows.Count - 1
                            If i = 0 Then result += "%"
                            result += HttpUtility.HtmlDecode(dtApplicant.Rows(i).Item(1)) & "<br />"
                        Next
                    End If
                    result += "%" & dt7.Rows(0).Item(4) & "%" & dt7.Rows(0).Item(5) & "%" & dt7.Rows(0).Item(6)

                    Dim dt1 As DataTable = BLL._selectSignatoriesNew(ticketNo)
                    '    Dim dt1 As DataTable
                    '  If HttpContext.Current.Session("tblSignatories") Is Nothing Then
                    '  dt1 = DALOracle.GetSignatories(Nuban)
                    '    Else
                    '    dt1 = HttpContext.Current.Session("tblSignatories")
                    'End If


                    If dt1.Rows.Count = 0 Then
                        result += "%<i>No signatory</i>"
                    Else
                        For i As Integer = 0 To dt1.Rows.Count - 1
                            If i = 0 Then result += "%"
                            result += HttpUtility.HtmlDecode(dt1.Rows(i).Item(0)) & "<br />"
                        Next
                        result = result.Substring(0, result.Length - 6)
                    End If


                    result += "%" & dt7.Rows(0).Item(7) & "%" & dt7.Rows(0).Item(8) &
                        "%" & dt7.Rows(0).Item(18) & " " & " " & CDbl(dt7.Rows(0).Item(9)).ToString("#,##0.00") & "%" & dt7.Rows(0).Item(18) & " " & " " & CDbl(dt7.Rows(0).Item(10)).ToString("#,##0.00") & "%" &
                        dt7.Rows(0).Item(18) & " " & " " & CDbl(dt7.Rows(0).Item(11)).ToString("#,##0.00") & "%" _
                        & dt7.Rows(0).Item(18) & " " & " " & CDbl(dt7.Rows(0).Item(12)).ToString("#,##0.00") & "%" _
                        & CDbl(dt7.Rows(0).Item(13)).ToString("#,##0") & "%" _
                        & "NGN " & " " & CDbl(dt7.Rows(0).Item(14)).ToString("#,##0.00") & "%" & "NGN " & " " & CDbl(dt7.Rows(0).Item(15)).ToString("#,##0.00") &
                        "%" & "NGN " & " " & CDbl(dt7.Rows(0).Item(16)).ToString("#,##0.00") & "%" & "NGN " & " " &
                        CDbl(vat.ToString("#,##0.00")) & "%" & sendStatus & "%" & CDbl(Basic).ToString("#,##0.00") & "%" & "NGN " & " " &
                        CDbl(totalCharge).ToString("#,##0.00") & "%" & tdWaive & "%" & debitAccount
                    BLL._updateStatus(ticketNo, "Ready")
                    Return result
                End If
            End If

        Catch ex As Exception
            Return "False%" & ex.Message
        End Try
    End Function



    Protected Sub lbCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancel.Click
        Response.Redirect("Dashboard.aspx")
    End Sub

    Protected Sub lbCancelRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelRequest.Click
        Response.Redirect("Dashboard.aspx")
    End Sub
    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If HttpContext.Current.Session("REQUESTID") Is Nothing Then HttpContext.Current.Response.Redirect("Login.aspx")
        BLL._updateStatus(HttpContext.Current.Session("REQUESTID"), "Pending")
        Response.Redirect("Dashboard.aspx")
    End Sub
    'Protected Sub aViewAll_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles aViewAll.ServerClick
    '    If aViewAll.InnerText = "View Top 3" Then
    '        SelectActivities("All")
    '        aViewAll.InnerText = "View all"
    '    ElseIf aViewAll.InnerText = "View all" Then
    '        SelectActivities("Filter-All")
    '        aViewAll.InnerText = "View Top 3"
    '    End If
    'End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
        Dim item_ As New System.Web.UI.WebControls.ListItem("Choose...", "0")

        ddlCategory.DataTextField = "Category"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataSource = BLL._selectCategory(ddlCountry.SelectedValue)
        ddlCategory.DataBind()
        If ddlCategory.Items.FindByValue("0") Is Nothing Then ddlCategory.Items.Add(item_)
        ddlCategory.SelectedValue = "0"

        ddlCompany.DataTextField = "Name"
        ddlCompany.DataValueField = "ID"
        ddlCompany.DataSource = BLL._selectDestinationByID(ddlCountry.SelectedValue, ddlCategory.SelectedValue)
        ddlCompany.DataBind()
        If ddlCompany.Items.FindByValue("0") Is Nothing Then ddlCompany.Items.Add(item_)
        ddlCompany.SelectedValue = "0"
        dv1.Style.Add("display", "")
        dv2.Style.Add("display", "")
        tbCriteria.Style.Add("display", "")
        tbApplicants.InnerHtml = ""
        selectApplicants()
        tbAccount.InnerHtml = Session("tbAccount")
    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        Dim item_ As New System.Web.UI.WebControls.ListItem("Choose...", "0")
        ddlCompany.DataTextField = "Name"
        ddlCompany.DataValueField = "ID"
        ddlCompany.DataSource = BLL._selectDestinationByID(ddlCountry.SelectedValue, ddlCategory.SelectedValue)
        ddlCompany.DataBind()
        If ddlCompany.Items.FindByValue("0") Is Nothing Then ddlCompany.Items.Add(item_)
        ddlCompany.SelectedValue = "0"
        dv1.Style.Add("display", "")
        dv2.Style.Add("display", "")
        tbCriteria.Style.Add("display", "")
        selectApplicants()
        tbAccount.InnerHtml = Session("tbAccount")
    End Sub





End Class
