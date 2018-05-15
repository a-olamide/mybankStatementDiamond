Imports System.Data
Imports System
Partial Class AuditLogs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        '    Dim dt As DataTable = BLL._selectAuditLogs()
        '    If dt.Rows.Count = 0 Then
        '        tb1.InnerHtml = "<tr><td colspan=""7"">No existing logs.</td></tr>"
        '    Else 
        '        tb1.InnerHtml = "<tr> <td>"  
        '        For i As Integer = 0 To dt.Rows.Count - 1
        '            tb1.InnerHtml += dt.Rows(i).Item(0) & "</td><td>" & dt.Rows(i).Item(1) & _
        '                        "</td><td>" & dt.Rows(i).Item(2) & "</td><td>" & dt.Rows(i).Item(3) & "</td><td>" & dt.Rows(i).Item(4) & "</td><td>" & dt.Rows(i).Item(5) & "</td><td>" & dt.Rows(i).Item(6) & "</td></tr><tr><td>"
        '        Next
        '        tb1.InnerHtml = tb1.InnerHtml.Substring(0, tb1.InnerHtml.Length - 17)
        '        tb1.InnerHtml += "</td></tr>"
        '    End If
        'End If
    End Sub
    Private Sub SelectActivities()
        Dim dtMain As DataTable = BLL._selectAuditLogs(txtUname.Text, txtStartDate.Text, txtEndDate.Text)

        If dtMain.Rows.Count = 0 Then
            tb1.InnerHtml = "<tr><td colspan=""7"">No existing logs.</td></tr>"
        Else
            tb1.InnerHtml = ""
            For i As Integer = 0 To dtMain.Rows.Count - 1
                tb1.InnerHtml += "<tr> <td>" & dtMain.Rows(i).Item(0) & "</td><td>" & dtMain.Rows(i).Item(1) &
                            "</td><td>" & dtMain.Rows(i).Item(2) & "</td><td>" & dtMain.Rows(i).Item(3) & "</td><td>" & dtMain.Rows(i).Item(4) & "</td><td>" & dtMain.Rows(i).Item(5) & "</td><td>" & dtMain.Rows(i).Item(6) & "</td><td>" & dtMain.Rows(i).Item(8) & "</td></tr>"
            Next
            '  tb1.InnerHtml = tb1.InnerHtml.Substring(0, tb1.InnerHtml.Length - 17)
            '  tb1.InnerHtml += "</td></tr>"
            lblTotal.Text = dtMain.Rows(0).Item(7)
        End If
    End Sub
    Protected Sub bSave_Command(sender As Object, e As CommandEventArgs) Handles bSave.Command
        
            SelectActivities()

        tbActivityFoot.Visible = True
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs) Handles lnkExport.Click

        gvReport.DataSource = BLL._selectAuditLogsReport(txtUname.Text, txtStartDate.Text, txtEndDate.Text)
        gvReport.DataBind()
        Utility.exportAuditReport(gvReport, Session("USERID").ToString.Split("@")(0).Replace(".", "") & "Audit" & Date.Now.ToShortDateString & Date.Now.ToShortTimeString & ".xls")

    End Sub
End Class
