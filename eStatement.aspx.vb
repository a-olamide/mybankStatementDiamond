
Imports System.Data
Imports System.Web.Services

Partial Class eStatement
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'acctetails.DataSource = DAL_API.AccountDetails("0040776395")
        'acctetails.DataBind()
        '' transacdetail.DataSource = DAL_API.TransactionDetails("0040776395", "05-Jan-2014", "05-Jan-2015")
        ' transacdetail.DataBind()



        If Session("USERID") Is Nothing Then Response.Redirect("login.aspx")
        If Not IsPostBack Then

            If Session("Role") = "Auditor" Then

                lblUser.Text = Session("BranchName")
                SelectActivities("All")

            ElseIf Session("Role") = "Admin" Then
                Response.Redirect("CreateUser.aspx")
            Else

                Response.Redirect("login.aspx")

            End If
        End If
    End Sub
    Private Sub SelectActivities(ByVal type_ As String)
        If HttpContext.Current.Session("Branch") Is Nothing Then HttpContext.Current.Response.Redirect("Login.aspx")
        Dim dt As DataTable = Nothing
        If type_ = "All" Then
            dt = BLL._selectSentActivity(Session("Branch"))
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(7) < 3 Then lblTop.Text = dt.Rows(0).Item(7) Else lblTop.Text = "3"
            End If

        ElseIf type_ = "Filter-All" Then
            dt = BLL._selectSentActivitybyFilter(Session("Branch"), "Filter-All")
            If dt.Rows.Count > 0 Then
                lblTop.Text = dt.Rows(0).Item(7)
            End If

        End If

        tbActivityBody.InnerHtml = ""
        If dt.Rows.Count = 0 Then
            tbActivityFoot.Visible = False
            tbActivityHead.Visible = False
            tbActivityBody.InnerHtml = "<tr><td colspan=""9"">No statements have been sent from Diamond bank</td></tr>"
        Else
            For i As Integer = 0 To dt.Rows.Count - 1
                tbActivityBody.InnerHtml += "<tr><td>" & dt.Rows(i).Item(1) & "</td><td>" & dt.Rows(i).Item(2) & "</td><td>" & dt.Rows(i).Item(3) &
                    "</td><td>" & dt.Rows(i).Item(4) & "</td><td># " & dt.Rows(i).Item(0) & "</td><td> " & dt.Rows(i).Item(5) & "</td><td><a " & Utility.getStatusStyle(dt.Rows(i).Item(10).ToString.ToLower) & " style=""cursor:none"" > " & dt.Rows(i).Item(10) & "</a></td><td>" & dt.Rows(i).Item(11).ToString() &
                    "</td><td><a style=""cursor:pointer"" onclick=""jPending('" & dt.Rows(i).Item(3) & "%" & dt.Rows(i).Item(0) & "%" & dt.Rows(i).Item(8) & "')"" >"
                'If dt.Rows(i).Item(8) = "Print" Then tbActivityBody.InnerHtml += "')"" href='statement/" & dt.Rows(i).Item(0) & ".pdf'>" Else tbActivityBody.InnerHtml += "')"" href=""javascript:void"" >"
                'If dt.Rows(i).Item(8) = "Print" Then tbActivityBody.InnerHtml += "')"" href='statement/" & dt.Rows(i).Item(0) & ".pdf' target=""_blank"">" Else tbActivityBody.InnerHtml += "')"" href=""javascript:void"" >"
                tbActivityBody.InnerHtml += dt.Rows(i).Item(8) & "</a></td></tr>"
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

        If type_.ToLower = "filter-all" Then
            type_ = type_
        Else
            type_ = type_.Split("-")(0)
        End If
        dt = BLL._selectSentActivitybyFilter(HttpContext.Current.Session("Branch"), type_)

        If dt Is Nothing Then
            result = "<tr><td colspan=""9"">Error populating table</td></tr>"
            Return "0~/" & "Total~/" & result
        ElseIf dt.Rows.Count = 0 Then
            result = "<tr><td colspan=""9"">No statements with ticket No or account number was found.</td></tr>"
            Return "0~/" & "Total~/" & result

        Else
            row = dt.Rows.Count()
            total = dt.Rows(0).Item(7)
            For i As Integer = 0 To dt.Rows.Count - 1
                result += "<tr><td>" & dt.Rows(i).Item(1) & "</td><td>" & dt.Rows(i).Item(2) & "</td><td>" & dt.Rows(i).Item(3) &
                    "</td><td>" & dt.Rows(i).Item(4) & "</td><td># " & dt.Rows(i).Item(0) & "</td><td " & Utility.getStatusStyle(dt.Rows(i).Item(10).ToString.ToLower) & ">" & dt.Rows(i).Item(10) & "</td><td>" & dt.Rows(i).Item(11) & "</td><td>" & dt.Rows(i).Item(5).ToString() &
                    "</td><td><a style=""cursor:pointer"" onclick=""jPending('" & dt.Rows(i).Item(3) & "%" & dt.Rows(i).Item(0) & "%" & dt.Rows(i).Item(8) & "')"" >"
                'If dt.Rows(i).Item(8) = "Print" Then tbActivityBody.InnerHtml += "')"" href='statement/" & dt.Rows(i).Item(0) & ".pdf'>" Else tbActivityBody.InnerHtml += "')"" href=""javascript:void"" >"
                'If dt.Rows(i).Item(8) = "Print" Then tbActivityBody.InnerHtml += "')"" href='statement/" & dt.Rows(i).Item(0) & ".pdf' target=""_blank"">" Else tbActivityBody.InnerHtml += "')"" href=""javascript:void"" >"
                result += dt.Rows(i).Item(8) & "</a></td></tr>"
            Next
            Return row & "~/" & total & "~/" & result
        End If
    End Function
    'Protected Sub lbCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancel.Click
    '    Response.Redirect("eStatement.aspx")
    'End Sub

    'Protected Sub lbCancelRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelRequest.Click
    '    Response.Redirect("eStatement.aspx")
    'End Sub

End Class
