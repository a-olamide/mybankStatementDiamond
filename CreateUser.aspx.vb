Imports System.Data.SqlClient

Partial Class CreateUser
    Inherits System.Web.UI.Page


    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Try
            If Utility.ValidateEmail(tbUserName.Text) = False Then
                Utility.ShowSweet(Me, "Username not a valid email address")
            ElseIf tbFirstName.Text = "" Or tbSurname.Text = "" Or tbUserName.Text = "" Or ddlRole.SelectedIndex = 0 Or txtBranch.Text = "" Or txtBranchCode.Text = ""  Then
                Utility.ShowSweet(Me, "Only the telephone field can be blank. Branchcode must be numeric")

            Else
                Dim admin As Int16
                Dim active As Int16
                If cbAdmin.Checked = True Then
                    admin = 1
                Else
                    admin = 0
                End If
                If cbActive.Checked = True Then
                    active = 1
                Else
                    active = 0
                End If
                Try
                    Dim salt As String = Utility.createSalt(10)
                    BLL._insertNewUserAdmin(tbFirstName.Text, tbSurname.Text, RTrim(LTrim(tbUserName.Text)).ToLower, ddlRole.SelectedItem.ToString, txtBranch.Text, admin, "", active, txtBranchCode.Text, txtSupervisor.Text, salt, "test", txtSolID.Text)
                    If bSave.Text = "Update" Then
                        Utility.ShowSweet(Me, "User Updated")
                    Else
                        Utility.ShowSweet(Me, "User Inserted")
                    End If
                    gvUsers.DataSource = BLL._selectUsers(1)
                    gvUsers.DataBind()
                    clearFields()
                Catch
                    Utility.ShowSweet(Me, "User Not Inserted")
                End Try
            End If





        Catch ex As Exception
            tblShowError.Visible = True
            lblShowError.Text = ex.Message
        End Try

    End Sub
    Private Sub clearFields()

        tbUserName.Text = ""
        tbFirstName.Text = ""

        tbSurname.Text = ""
        ddlRole.SelectedIndex = 0
        txtBranch.Text = ""
        txtBranchCode.Text = ""
        txtSupervisor.Text = ""
        cbAdmin.Checked = False
        cbActive.Checked = False
        bSave.Text = "Create"
    End Sub



    Protected Sub tbUserName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbUserName.TextChanged

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                If Session("ROLE") = "Admin" Then
                    ddlRole.DataSource = BLL._selectRole
                    ddlRole.DataTextField = "Role"
                    ddlRole.DataValueField = "Role"
                    ddlRole.DataBind()
                    ddlRole.Items.Insert(0, "--Select--")
                    ddlRole.SelectedIndex = "0"
                    ddlRole.Enabled = True

                    gvUsers.DataSource = BLL._selectUsers(3)
                    gvUsers.DataBind()
                    trAdmin.Visible = True

                End If

            End If

        Catch ex As Exception
            tblShowError.Visible = True
            lblShowError.Text = ex.Message
        End Try


    End Sub


    Protected Sub gvUsers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvUsers.RowCommand
        Try
            Dim Userid As String
            Dim dr As SqlDataReader = Nothing
            Dim e_ As Integer = e.CommandArgument()
            Dim rowe = gvUsers.Rows(e_)
            If CType(e.CommandSource, LinkButton).ID = "imgDelete" Then
                Userid = CType(gvUsers.Rows(rowe.RowIndex).FindControl("lblUserid"), Label).Text.ToString
                BLL._deleteUser(Userid)
                gvUsers.DataSource = BLL._selectUsers(1)
                gvUsers.DataBind()

            ElseIf CType(e.CommandSource, LinkButton).ID = "btnEdit" Then
                Userid = CType(gvUsers.Rows(rowe.RowIndex).FindControl("lblUserid"), Label).Text.ToString
                dr = BLL._selectUserbyID(Userid)
                While dr.Read()
                    If Not IsDBNull(dr.Item("FirstName")) Then
                        tbFirstName.Text = dr.Item("FirstName")
                    Else
                        tbFirstName.Text = Nothing
                    End If
                    If Not IsDBNull(dr.Item("Surname")) Then
                        tbSurname.Text = dr.Item("Surname")
                    Else
                        tbSurname.Text = Nothing
                    End If
                   
                    If Not IsDBNull(dr.Item("Email")) Then

                        tbUserName.Text = dr.Item("Email")
                    Else
                        tbUserName.Text = Nothing
                    End If
                    If Not IsDBNull(dr.Item("BranchName")) Then

                        txtBranch.Text = dr.Item("BranchName")
                    Else
                        txtBranch.Text = Nothing
                    End If
                    If Not IsDBNull(dr.Item("BranchID")) Then

                        txtBranchCode.Text = dr.Item("BranchID")
                    Else
                        txtBranchCode.Text = Nothing
                    End If
                    If Not IsDBNull(dr.Item("solID")) Then

                        txtSolID.Text = dr.Item("solID")
                    Else
                        txtSolID.Text = Nothing
                    End If
                    If Not IsDBNull(dr.Item("supervisor")) Then

                        txtSupervisor.Text = dr.Item("supervisor")
                    Else
                        txtSupervisor.Text = Nothing
                    End If

                    If Not IsDBNull(dr.Item("Status")) Then
                        cbActive.Checked = dr.Item("Status")
                    End If
                    cbAdmin.Checked = dr.Item("Admin")



                    ddlRole.DataSource = BLL._selectRole
                    ddlRole.DataTextField = "Role"
                    ddlRole.DataValueField = "Role"
                    ddlRole.DataBind()
                    ddlRole.Items.Insert(0, "--Select--")
                    ddlRole.SelectedIndex = ddlRole.Items.IndexOf(ddlRole.Items.FindByText(dr.Item("Role")))
                    lID.Text = Userid
                    bSave.Text = "Update"
                End While
            End If

        Catch ex As Exception
            tblShowError.Visible = True
            lblShowError.Text = ex.Message
        End Try

    End Sub

    Protected Sub lnkRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRefresh.Click
        Try

            clearFields()
            bSave.Text = "Create"
        Catch ex As Exception
            tblShowError.Visible = True
            lblShowError.Text = ex.Message
        End Try
    End Sub




    Protected Sub lnkRedirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRedirect.Click
        clearFields()
    End Sub


    Protected Sub exportUser_Click(sender As Object, e As EventArgs) Handles exportUser.Click
        gvExportUser.DataSource = BLL._selectUsersReport()
        gvExportUser.DataBind()

        Utility.exportUser(gvExportUser, Session("USERID").ToString.Split("@")(0).Replace(".", "") & "UserInfo" & Date.Now.ToShortDateString & Date.Now.ToShortTimeString & ".xls")

    End Sub

    Protected Sub lnkResetInfo_Click(sender As Object, e As EventArgs) Handles lnkResetInfo.Click
        BLL._resetUserProfile()

        gvUsers.DataSource = BLL._selectUsers(3)
        gvUsers.DataBind()

    End Sub
    Private Sub aSearch_Click(sender As Object, e As EventArgs) Handles aSearch.Click
        gvUsers.DataSource = BLL._selectUsersbyfilter(txtFlter2.Text)
        gvUsers.DataBind()
        trAdmin.Visible = True

    End Sub
End Class
