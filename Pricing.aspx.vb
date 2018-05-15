Imports System.Data

Partial Class Pricing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("USERID") Is Nothing Then Response.Redirect("login.aspx")
        If (Session("Role") = "Officer") Then Response.Redirect("Login.aspx")
        If (Session("Role") = "Reviewer") Then Response.Redirect("Login.aspx")
        If Session("Role") = "Auditor" Then
            tdprice.Visible = False
        End If
        If Not IsPostBack Then
            Dim item_ As New System.Web.UI.WebControls.ListItem("Choose...", "0")


            tbActivityBody.InnerHtml = ""
            Dim dt = BLL.__Selectpricing()
            If dt.Rows.Count = 0 Then
                tbActivityFoot.Visible = False
                tbActivityHead.Visible = False
                tbActivityBody.InnerHtml = "<tr><td colspan=""4"">No pricing has been set.</td></tr>"
            Else
                For i As Integer = 0 To dt.Rows.Count - 1
                    tbActivityBody.InnerHtml += "<tr><td>" & dt.Rows(i).Item(1) & "</td><td>" & dt.Rows(i).Item(2) & "</td><td>" & dt.Rows(i).Item(3) & _
                        "</td><td>" & dt.Rows(i).Item(4) & "</td><td>" & dt.Rows(i).Item(5) & "</td><td>" & dt.Rows(i).Item(6) & "</td><td>" & dt.Rows(i).Item(7) & "</td><td>" & dt.Rows(i).Item(8) & "</td></tr>"
                Next
            End If
        End If
    End Sub

    Protected Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Try
            Dim Flat As String
            Dim staff As String
            Dim mgt As String
            If chkFlat.Checked = True Then
                Flat = 1
            Else
                Flat = 0
            End If
            If chkStaff.Checked = True Then
                staff = 1
            Else
                staff = 0
            End If
            If chkMgt.Checked = True Then
                mgt = 1
            Else
                mgt = 0
            End If
            Dim valPrice As Boolean = IsNumeric(txtpricing.Text)
            Dim valVendor As Boolean = IsNumeric(txtVendor.Text)
            If txtpricing.Text = "" Or txtVendor.Text = "" Then
                Utility.ShowSweet(Me, "The Price, Vendor share and country feild cannot be empty")
            ElseIf valPrice = False Or valVendor = False Then
                Utility.ShowSweet(Me, "Price or vendor input must be numeric")
            ElseIf CInt(txtpricing.Text) < 1 Then
                Utility.ShowSweet(Me, "Price or vendor input invalid")
            ElseIf CDbl(txtVendor.Text) > CDbl(txtpricing.Text) Then
                Utility.ShowSweet(Me, "Vendor's share cannot be more than the total price")
            Else
                BLL.__Insertpricing(txtpricing.Text, Session("Userid"), Flat, staff, mgt, txtVendor.Text)
                tbActivityBody.InnerHtml = ""
                Dim dt = BLL.__Selectpricing()
                If dt.Rows.Count = 0 Then
                    tbActivityFoot.Visible = False
                    tbActivityHead.Visible = False
                    tbActivityBody.InnerHtml = "<tr><td colspan=""4"">No pricing has been set.</td></tr>"
                Else
                    tbActivityHead.Visible = True
                    For i As Integer = 0 To dt.Rows.Count - 1
                        tbActivityBody.InnerHtml += "<tr><td>" & dt.Rows(i).Item(1) & "</td><td>" & dt.Rows(i).Item(2) & "</td><td>" & dt.Rows(i).Item(3) & _
                            "</td><td>" & dt.Rows(i).Item(4) & "</td><td>" & dt.Rows(i).Item(5) & "</td><td>" & dt.Rows(i).Item(6) & "</td><td>" & dt.Rows(i).Item(7) & "</td><td>" & dt.Rows(i).Item(8) & "</td></tr>"
                    Next
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
