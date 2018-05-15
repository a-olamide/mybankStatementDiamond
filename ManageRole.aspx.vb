Imports System.Data.SqlClient

Partial Class ManageRole
    Inherits System.Web.UI.Page


    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Try


            Dim active As Int16

            If cbActive.Checked = True Then
                active = 1
            Else
                active = 0
            End If
            Try
                If ddlRole.SelectedValue.ToString = "0" Or ddlRole.SelectedIndex = 0 Or tbADRole.Text = "" Then
                    Utility.ShowSweet(Me, "Invalid Input")
                Else
                    If bSave.Text = "Update" Then
                        BLL._insertADROle(lID.Text, RTrim(LTrim(tbADRole.Text)), ddlRole.SelectedItem.ToString, active)
                        Utility.ShowSweet(Me, "User Updated")
                    Else
                        BLL._insertADROle(0, RTrim(LTrim(tbADRole.Text)), ddlRole.SelectedItem.ToString, active)
                        Utility.ShowSweet(Me, "User Inserted")
                    End If
                    gvUsers.DataSource = BLL._selectADRole(1)
                    gvUsers.DataBind()
                    clearFields()
                End If
            Catch
                Utility.ShowSweet(Me, "User Not Inserted")
            End Try

               



        Catch ex As Exception
            tblShowError.Visible = True
            lblShowError.Text = ex.Message
        End Try

    End Sub
    Private Sub clearFields()


        tbADRole.Text = ""

        ddlRole.SelectedIndex = 0

        cbActive.Checked = False
        bSave.Text = "Create"
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
                    ddlRole.SelectedValue = "0"
                    ddlRole.Enabled = True

                    gvUsers.DataSource = BLL._selectADRole(1)
                    gvUsers.DataBind()


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
                BLL._deleteADROle(Userid)
                gvUsers.DataSource = BLL._selectADRole(1)
                gvUsers.DataBind()

            ElseIf CType(e.CommandSource, LinkButton).ID = "btnEdit" Then
                Userid = CType(gvUsers.Rows(rowe.RowIndex).FindControl("lblUserid"), Label).Text.ToString

                dr = BLL._selectADRolerbyID(Userid)
                While dr.Read()
                    If Not IsDBNull(dr.Item("ADTitle")) Then
                        tbADRole.Text = dr.Item("ADTitle")
                    Else
                        tbADRole.Text = "N/A"
                    End If
                  
                  
                    
                    If Not IsDBNull(dr.Item("Status")) Then
                        cbActive.Checked = dr.Item("Status")
                    End If




                    ddlRole.DataSource = BLL._selectRole
                    ddlRole.DataTextField = "Role"
                    ddlRole.DataValueField = "Role"
                    ddlRole.DataBind()
                    ddlRole.Items.Insert(0, "--Select--")
                    ddlRole.SelectedIndex = ddlRole.Items.IndexOf(ddlRole.Items.FindByText(dr.Item("SystemRole")))
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


End Class
