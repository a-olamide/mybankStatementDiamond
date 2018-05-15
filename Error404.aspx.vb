
Partial Class Error404
    Inherits System.Web.UI.Page

    Protected Sub Reload_Click(sender As Object, e As EventArgs) Handles Reload.Click
        Response.Redirect("Login.aspx")
    End Sub
End Class
