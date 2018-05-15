Imports System.Data

Partial Class Report
    Inherits System.Web.UI.Page


    Private totalCnt As Int16 = 0

    Private Sub SelectActivities()
        If HttpContext.Current.Session("Branch") Is Nothing Then HttpContext.Current.Response.Redirect("Login.aspx")
        Dim byBranch As String = ""
        Dim dt As DataTable
        If (Session("Role") = "Admin" Or Session("Role") = "Auditor") And ddlBranch.SelectedIndex > 0 Then
            byBranch = ddlBranch.SelectedItem.Value.ToString() '.Substring(4, 5)
            dt = BLL._selectReportAdmin(txtStartDate.Text, txtEndDate.Text, byBranch, txtFilter.Text.ToString.Split("-")(0))
        ElseIf (Session("Role") = "Admin" Or Session("Role") = "Auditor") And ddlBranch.SelectedIndex = 0 Then
            dt = BLL._selectReportAdmin(txtStartDate.Text, txtEndDate.Text, "", txtFilter.Text.ToString.Split("-")(0))
        Else
            dt = BLL._selectReport(txtStartDate.Text, txtEndDate.Text, HttpContext.Current.Session("Branch"), txtFilter.Text.ToString.Split("-")(0))
        End If
        ' dt = BLL._selectReport(Session("BranchName"), CDate(txtStartDate.Text), CDate(txtEndDate.Text), byBranch)


        tbActivityBody.InnerHtml = ""
        tbActivityHead.InnerHtml = ""
        If dt.Rows.Count = 0 Then

            tbActivityFoot.Visible = False
            tbActivityHead.Visible = False
            tbActivityFoot.Visible = False
            tbActivityBody.InnerHtml = "<tr><td colspan=""9"">No records.</td></tr>"
        Else
            tbActivityFoot.Visible = True
            tbActivityHead.Visible = True
            tbActivityFoot.Visible = True
            tbActivityHead.InnerHtml += "<tr><th><i class=""ace-icon fa fa-user""></i>" & "Name" & "</th><th>" & "Ticket No." & "</th><th>" & "Destination" & _
                        "</th><th>" & "Pages" & "</th><th>" & "Bank Charge" & "</th><th>" & "Additional Charge" & "</th><th>" & "VAT" & _
                       "</th><th>" & "Total" & "</th><th>" & "Date" & "</th></tr>"
            lblTotal.Text = dt.Rows(0).Item(8)
            For i As Integer = 0 To dt.Rows.Count - 1
                tbActivityBody.InnerHtml += "<tr><td>" & dt.Rows(i).Item(0) & "</td><td>" & dt.Rows(i).Item(1) & "</td><td>" & dt.Rows(i).Item(2) & _
                    "</td><td>" & dt.Rows(i).Item(3) & "</td><td>" & "NGN " & CDbl(dt.Rows(i).Item(4)).ToString("#,##0.00") & "</td><td>" & "NGN " & CDbl(dt.Rows(i).Item(5)).ToString("#,##0.00") & _
                    "</td><td>" & "NGN " & CDbl(dt.Rows(i).Item("Vat")).ToString("#,##0.00") & "</td><td>" & "NGN " & CDbl(dt.Rows(i).Item(6)).ToString("#,##0.00") & "</td><td>" & CDate(dt.Rows(i).Item(7)).ToString("dd MMM, yyyy")
            Next
            'If CDbl(dt.Rows(0).Item(7)) > 3 Then lblTop.Text = "3" Else lblTop.Text = dt.Rows(0).Item(7)
            'lblTotal.Text = dt.Rows(0).Item(7)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("USERID") Is Nothing Then Response.Redirect("login.aspx")
        If (Session("Role") = "Admin" Or Session("Role") = "Auditor") Then
            ' Session("BranchName") = "UBN HEAD OFFICE"
            ddlBranch.Visible = True


        End If

        If Not IsPostBack Then
            If (Session("Role") = "Admin" Or Session("Role") = "Auditor") Or Session("Role") = "Approver" Then

                '    '   Dim adclass As New ADAuth.Service


                '    'If Session("Role") <> "Manager" Then Response.Redirect("dashboard.aspx")
                '    ' lblUser.Text = Session("BranchName")
                '    'lblTotal.Text = totalCnt

                ddlBranch.DataSource = BLL._selectDistinctBranchName()
                '    ddlBranch.DataSource = adclass.GetBranches().Tables(0)
                ddlBranch.DataTextField = "BRANCHNAME"
                ddlBranch.DataValueField = "BRANCHNAME"

                ddlBranch.DataBind()
                ddlBranch.Items.Insert(0, "--Select Branch--")
                ddlBranch.SelectedValue = 0
            Else
                Response.Redirect("login.aspx")
            End If
        End If
    End Sub

    Protected Sub bSave_Click(sender As Object, e As EventArgs) Handles bSave.Click
        ' CDate(txtStartDate.Text), CDate(txtEndDate.Text)
        If txtStartDate.Text = "" Or txtEndDate.Text = "" Then
            SelectActivities()
        ElseIf Month(CDate(txtStartDate.Text)) = Month(CDate(txtEndDate.Text)) Then
            SelectActivities()
        Else
            Utility.ShowSweet(Me, "You can only generate report in the same month")
        End If

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs) Handles lnkExport.Click
        Dim dt As DataTable
        Dim byBranch As String = ""
        If (Session("Role") = "Admin" Or Session("Role") = "Auditor") And ddlBranch.SelectedIndex > 0 Then
            byBranch = ddlBranch.SelectedItem.Value.ToString()
            dt = BLL._selectReportAdminExport(txtStartDate.Text, txtEndDate.Text, byBranch, txtFilter.Text.ToString.Split("-")(0))
        ElseIf (Session("Role") = "Admin" Or Session("Role") = "Auditor") And ddlBranch.SelectedIndex = 0 Then
            dt = BLL._selectReportAdminExport(txtStartDate.Text, txtEndDate.Text, "", txtFilter.Text.ToString.Split("-")(0))
        Else
            dt = BLL._selectReportExport(txtStartDate.Text, txtEndDate.Text, HttpContext.Current.Session("Branch"), txtFilter.Text.ToString.Split("-")(0))
        End If
        'gvReport.DataSource = BLL._selectReportExport(Session("BranchName"), CDate(txtStartDate.Text), CDate(txtEndDate.Text), byBranch)
        gvReport.DataSource = dt
        gvReport.DataBind()
        Utility.exportReport(gvReport, Session("USERID").ToString.Split("@")(0).Replace(".", "") & Date.Now.ToShortDateString & Date.Now.ToShortTimeString & ".xls")

    End Sub
End Class
