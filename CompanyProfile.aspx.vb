Imports System.Data

Partial Class CompanyProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("USERID") Is Nothing Then Response.Redirect("login.aspx")
        If (Session("Role") <> "Admin") Then Response.Redirect("Login.aspx")

        If Not Me.IsPostBack Then
            loadInfo()
        End If

    End Sub


    Private Sub loadInfo()
        Dim dt As DataRow = BLL.__SelectCompanyInfo().Rows(0)
        txtCompany.Text = dt.Item(0)
        txtCountry.Text = dt.Item(2)
        txtRCNO.Text = dt.Item(3)
        txtDomain.Text = dt.Item(1)
        chkChecker.Checked = CBool(dt.Item(4))
    End Sub



    Protected Sub chkChecker_CheckedChanged(sender As Object, e As EventArgs) Handles chkChecker.CheckedChanged
        Dim chk As String
        If chkChecker.Checked = True Then
            chk = "1"
        Else
            chk = "0"
        End If

        BLL.__UpdateMakerChecker(chk)
        loadInfo()
    End Sub

    
End Class
