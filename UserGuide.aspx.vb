
Partial Class UserGuide
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Role") = "Admin" Then iframe1.Attributes.Add("src", "Documentation/mybankStatementUserGuide.pdf")
    End Sub

End Class
