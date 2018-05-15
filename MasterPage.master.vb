Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Net

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage 
     
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try  
            'If Session("USERID") Is Nothing Then Response.Redirect("login.aspx")
            If Not IsPostBack Then 
                ResetMenu(Session("Role"))
                liActivePage1.Visible = True
                liActivePage1.InnerHtml = " <i class=""icon-home home-icon""></i> " + Session("Company")
                lbActivePage2.Text = ContentPlaceHolder1.Page.Title


                If ContentPlaceHolder1.Page.Title = "Manage User" Then
                    liManageUsers.Attributes.Add("class", "active")
                    If Session("Role") <> "Admin" Then Response.Redirect("Dashboard.aspx")
                    liActivePage2.Visible = True
                ElseIf ContentPlaceHolder1.Page.Title = "Manage Roles" Then
                    liManageRole.Attributes.Add("class", "active")
                    If Session("Role") <> "Admin" Then Response.Redirect("Login.aspx")
                    liActivePage2.Visible = True
                ElseIf ContentPlaceHolder1.Page.Title = "eStatements" Then
                    If Session("Role") <> "Initiator" Then Response.Redirect("Login.aspx")
                    liDashBoard.Attributes.Add("Class", "active")
                    liActivePage2.Visible = True
                ElseIf ContentPlaceHolder1.Page.Title = "Request Statement" Then
                    If Session("Role") <> "Reviewer" Then Response.Redirect("Login.aspx")
                    liRequest.Attributes.Add("Class", "active")
                    liActivePage2.Visible = True
                ElseIf ContentPlaceHolder1.Page.Title = "Preview" Then
                    ' If Session("Role") <> "Initiator" Then Response.Redirect("Preview.aspx")
                    liPreview.Attributes.Add("Class", "active")
                    liActivePage2.Visible = True
                ElseIf ContentPlaceHolder1.Page.Title = "Approval" Then
                    If Session("Role") <> "Approver" Then Response.Redirect("Login.aspx")
                    liApproval.Attributes.Add("Class", "active")
                    liActivePage2.Visible = True

                ElseIf ContentPlaceHolder1.Page.Title = "Audit Logs" Then
                    liAuditLogs.Attributes.Add("Class", "active")
                    If Session("Role") = "Admin" Or Session("Role") = "Auditor" Then

                    Else
                        Response.Redirect("Dashboard.aspx")
                    End If

                    liActivePage2.Visible = True
                ElseIf ContentPlaceHolder1.Page.Title = "eStatementAudit" Then
                    lieStatementAudit.Attributes.Add("Class", "active")
                    If Session("Role") = "Auditor" Then

                    Else
                        Response.Redirect("Login.aspx")
                    End If

                    liActivePage2.Visible = True
                ElseIf ContentPlaceHolder1.Page.Title = "User Guide" Then
                    liUserGuide.Attributes.Add("Class", "active")
                    liActivePage2.Visible = True
                ElseIf ContentPlaceHolder1.Page.Title = "Pricing" Then
                    liPricing.Attributes.Add("Class", "active")
                    If Session("Role") <> "Admin" Then Response.Redirect("Dashboard.aspx")
                    liActivePage2.Visible = True
                ElseIf ContentPlaceHolder1.Page.Title = "Approval" Then
                    liPricing.Attributes.Add("Class", "active")
                    If Session("Role") <> "Approver" Then Response.Redirect("Dashboard.aspx")
                    liActivePage2.Visible = True
                ElseIf ContentPlaceHolder1.Page.Title = "Report" Then
                    liReport.Attributes.Add("Class", "active")
                    If Session("Role") = "Initiator" Then Response.Redirect("Dashboard.aspx")
                    liActivePage2.Visible = True
                ElseIf ContentPlaceHolder1.Page.Title = "Outbox" Then
                    liOutbox.Attributes.Add("Class", "active")
                    ' If Session("Role") = "Initiator" Then Response.Redirect("Dashboard.aspx")
                    liActivePage2.Visible = True
                End If

                spanUserRole.InnerHtml = Session("NAME") & " (" & Session("BranchName") & ") | " & Session("Role")


                ' lbUserInfo.Text = Session("NAME") & " (" & Session("Branch").ToString.Split("(")(0) & ") | " & Session("Role") & " " & Session("Mode")
            End If
        Catch ex As Exception
            'tblShowError.Visible = True
            lblShowError.Text = ex.Message
        End Try
    End Sub

    Private Sub ResetMenu(ByVal roleid As String)
        Try
            If roleid = "Admin" Then 'Admin 
                liManageUsers.Visible = True
                liAuditLogs.Visible = True
                liPricing.Visible = True
                liReport.Visible = True
                liManageRole.Visible = True
                liOutbox.Visible = True
            ElseIf roleid = "Initiator" Then 'Officer          
                liDashBoard.Visible = True
                liPreview.Visible = True
                liOutbox.Visible = True
            ElseIf roleid = "Approver" Then 'Officer  
                liPreview.Visible = True
                liApproval.Visible = True
                liReport.Visible = True
                liOutbox.Visible = True
            ElseIf roleid = "Auditor" Then
                liAuditLogs.Visible = True
                liUserGuide.Visible = True
                lieStatementAudit.Visible = True
                liReport.Visible = True
            ElseIf roleid = "Reviewer" Then
                liPreview.Visible = True
                liRequest.Visible = True
                liUserGuide.Visible = True
            End If
        Catch ex As Exception
            tblShowError.Visible = True
            lblShowError.Text = ex.Message
        End Try

    End Sub
   
End Class

