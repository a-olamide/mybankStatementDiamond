
Partial Class ErrorPage
    Inherits System.Web.UI.Page

    Protected Sub Reload_Click(sender As Object, e As EventArgs) Handles Reload.Click
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
End Class
