
Imports System.Data

Partial Class Outbox
    Inherits System.Web.UI.Page

    Private Sub SelectActivities()
        If HttpContext.Current.Session("Branch") Is Nothing Then HttpContext.Current.Response.Redirect("Login.aspx")
        Dim byBranch As String = ""
        Dim dt As DataTable
        If Session("Role") = "Admin" And ddlBranch.SelectedIndex > 0 Then
            byBranch = ddlBranch.SelectedItem.Value.ToString()
            dt = BLL._selectOutboxAdmin(byBranch)
        ElseIf Session("Role") = "Admin" And ddlBranch.SelectedIndex = 0 Then
            dt = BLL._selectOutboxAdmin("")
        Else
            dt = BLL._selectOutboxAdmin(HttpContext.Current.Session("Branch"))
        End If
        ' dt = BLL._selectReport(Session("BranchName"), CDate(txtStartDate.Text), CDate(txtEndDate.Text), byBranch)


        tbActivityBody.InnerHtml = ""
        tbActivityHead.InnerHtml = ""
        If dt.Rows.Count = 0 Then

            tbActivityFoot.Visible = False
            tbActivityHead.Visible = False
            tbActivityBody.InnerHtml = "<tr><td colspan=""8"">No items in outbox.</td></tr>"
        Else
            tbActivityHead.InnerHtml += "<tr><th>" & "Ticket No." & "</th><th>" & "Name" & "</th><th>" & "Initiator." & "</th><th>" & "Destination" &
                        "</th><th>" & "Date" & "</th><th>" & "Branch" & "</th></tr>"
            lblTotal.Text = dt.Rows.Count.ToString()
            For i As Integer = 0 To dt.Rows.Count - 1
                tbActivityBody.InnerHtml += "<tr><td>" & dt.Rows(i).Item(0) & "</td><td>" & dt.Rows(i).Item(1) & "</td><td>" & dt.Rows(i).Item(2) &
                    "</td><td>" & dt.Rows(i).Item(3) & "</td><td>" & dt.Rows(i).Item(4) & "</td><td>" & dt.Rows(i).Item(5) & "</td>"
            Next
            'If CDbl(dt.Rows(0).Item(7)) > 3 Then lblTop.Text = "3" Else lblTop.Text = dt.Rows(0).Item(7)
            'lblTotal.Text = dt.Rows(0).Item(7)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("USERID") Is Nothing Then Response.Redirect("login.aspx")
        If Session("Role") = "Admin" Then
            ' Session("BranchName") = "UBN HEAD OFFICE"
            ddlBranch.Visible = True


        End If

        If Not IsPostBack Then
            If Session("Role") = "Admin" Then


                'If Session("Role") <> "Manager" Then Response.Redirect("dashboard.aspx")
                ' lblUser.Text = Session("BranchName")
                'lblTotal.Text = totalCnt
                ddlBranch.DataSource = BLL._selectDistinctBranchName()
                ddlBranch.DataTextField = "branchName"
                ddlBranch.DataValueField = "branchName"
                ddlBranch.DataBind()
                ddlBranch.Items.Insert(0, "--Select Branch--")
                ddlBranch.SelectedValue = 0
            Else
                SelectActivities()
            End If
        End If
    End Sub

    Protected Sub bSave_Click(sender As Object, e As EventArgs) Handles bSave.Click

        SelectActivities()
        tbActivityFoot.Visible = True
    End Sub

    'Protected Sub lnkExport_Click(sender As Object, e As EventArgs) Handles lnkExport.Click
    '    Dim dt As DataTable
    '    Dim byBranch As String = ""
    '    If Session("Role") = "Admin" And ddlBranch.SelectedIndex > 0 Then
    '        byBranch = ddlBranch.SelectedItem.Value.ToString()
    '        dt = BLL._selectReportAdminExport(txtStartDate.Text, txtEndDate.Text, byBranch)
    '    ElseIf Session("Role") = "Admin" And ddlBranch.SelectedIndex = 0 Then
    '        dt = BLL._selectReportAdminExport(txtStartDate.Text, txtEndDate.Text, "")
    '    Else
    '        dt = BLL._selectReportExport(txtStartDate.Text, txtEndDate.Text, HttpContext.Current.Session("Branch"))
    '    End If
    '    'gvReport.DataSource = BLL._selectReportExport(Session("BranchName"), CDate(txtStartDate.Text), CDate(txtEndDate.Text), byBranch)
    '    gvReport.DataSource = dt
    '    gvReport.DataBind()
    '    Utility.exportReport(gvReport, Session("USERID").ToString.Split("@")(0).Replace(".", "") & Date.Now.ToShortDateString & Date.Now.ToShortTimeString & ".xls")

    'End Sub


End Class