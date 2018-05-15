Imports System.Data
Imports System
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Collections
Imports iTextSharp.text.html.simpleparser
Imports System.Globalization
Imports System.Net
Imports System.Data.SqlClient
Imports System.Web.Services

Partial Class Preview
    Inherits System.Web.UI.Page
    Dim Font1 As Font = FontFactory.GetFont("Arial", 30, 1)
    Dim Font2 As Font = FontFactory.GetFont("Arial", 20, 1)
    Dim Font3 As Font = FontFactory.GetFont("Arial", 14)
    Dim TableFont As Font = FontFactory.GetFont("Arial", 10)
    Dim TableFont2 As Font = FontFactory.GetFont("Arial", 9)
    Dim bdrColor As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#e7eaec")
    Dim hdrbgColor As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#F5F5F6")
    Dim midbgColor As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#F9F9F9")
    Dim TableFontbold As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, New BaseColor(51, 51, 51))
    Dim titleFont As Font = FontFactory.GetFont("Arial", 14)
    Dim subTitleFont As Font = FontFactory.GetFont("Arial", 8)
    Dim endingMessageFont As Font = FontFactory.GetFont("Arial", 10, Font.ITALIC)
    Dim bodyFont As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("USERID") Is Nothing Then Response.Redirect("login.aspx")
        If Not IsPostBack Then
            Try
                BLL.receiveInbox()
            Catch
            End Try

            'If Not Request.IsSecureConnection Then
            '    Dim redirectUrl As String = Request.Url.ToString().Replace("http:", "https:")
            '    Response.Redirect(redirectUrl)
            'End If

            '  tb.InnerHtml = "<tr runat=""server""><td runat=""server"">Omokore J. Ayodele</td><td>1000069</td><td>Access Bank Plc</td><td>Nigeria</td><td>omokore.ayodele@gmail.com</td><td>12/10/2015 4:11:00 PM</td><td runat=""server""><asp:LinkButton runat=""server"" OnClick=""download_ServerClick"" CommandArgument=""1000069-11"">Download</asp:LinkButton></td></tr>"

        End If
        If IsNothing(Session("type")) Then
            SelectActivities("All")
        Else
            SelectActivities("Filter-All")

        End If


    End Sub
    Private Sub SelectActivities(ByVal type_ As String)
        Dim statusCSV As String
        Dim dt As DataTable = Nothing
        If type_ = "All" Then
            dt = BLL._selectReceived()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(11) < 3 Then lblTop.Text = dt.Rows(0).Item(11) Else lblTop.Text = "3"
            End If
        ElseIf type_ = "Filter" Then
            dt = BLL._selectReceivedByFilter(txtTicketNo.Text, "Filter")
            If dt.Rows.Count > 0 Then
                lblTop.Text = dt.Rows(0).Item(11)
            End If
        ElseIf type_ = "Filter-All" Then
            dt = BLL._selectReceivedByFilter(txtTicketNo.Text, "Filter-All")
            If dt.Rows.Count > 0 Then
                lblTop.Text = dt.Rows(0).Item(11)
            End If
        End If
        tbActivityBody.InnerHtml = ""
        tbActivityHead.InnerHtml = ""
        If dt.Rows.Count = 0 Then
            tbActivityFoot.Visible = False
            tbActivityHead.Visible = False
            tbActivityBody.InnerHtml = "<tr><td colspan=""8"">No statements available for preview.</td></tr>"
        Else

            statusCSV = BLL._selectCSVStatusByCompany(Session("SenderID"))
            If statusCSV = "True" Then
                tbActivityHead.InnerHtml += "<tr><th><i class=""ace-icon fa fa-user""></i>" & "Account Name" & "</th><th>" & "Ticket No." & "</th><th>" & "Source Bank" & _
                           "</th><th>" & "Country" & "</th><th>" & "User" & "</th><th>" & "Date Sent" & _
                         "</th><th>" & "Status" & "</th><th>" & "CSV" & "</th></tr>"
            Else
                tbActivityHead.InnerHtml += "<tr><th><i class=""ace-icon fa fa-user""></i>" & "Account Name" & "</th><th>" & "Ticket No." & "</th><th>" & "Source Bank" & _
                           "</th><th>" & "Country" & "</th><th>" & "User" & "</th><th>" & "Date Sent" & _
                         "</th><th>" & "Status" & "</th></tr>"

            End If
            ' Dim tbl As New HtmlTable()

            Dim row As HtmlTableRow
            Dim cell1 As HtmlTableCell
            Dim cell2 As HtmlTableCell
            Dim cell3 As HtmlTableCell
            Dim cell4 As HtmlTableCell
            Dim cell5 As HtmlTableCell
            Dim cell6 As HtmlTableCell
            Dim cell7 As HtmlTableCell
            Dim cell8 As HtmlTableCell
            For i As Integer = 0 To dt.Rows.Count - 1
                row = New HtmlTableRow()

                cell1 = New HtmlTableCell()
                cell1.InnerText = dt.Rows(i).Item(0)
                cell2 = New HtmlTableCell()
                cell2.InnerText = dt.Rows(i).Item(1)
                cell3 = New HtmlTableCell()
                cell3.InnerText = dt.Rows(i).Item(2)
                cell4 = New HtmlTableCell()
                cell4.InnerText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dt.Rows(i).Item(3).ToString())
                cell5 = New HtmlTableCell()
                cell5.InnerText = dt.Rows(i).Item(4)
                cell6 = New HtmlTableCell()
                cell6.InnerText = dt.Rows(i).Item(5).ToString()
                cell7 = New HtmlTableCell()
                Dim lnkbtn As New LinkButton
                lnkbtn.ID = dt.Rows(i).Item(1) & "-" & dt.Rows(i).Item(10)

                lnkbtn.CommandArgument = dt.Rows(i).Item(1) & "-" & dt.Rows(i).Item(10)
                lnkbtn.Text = "Download"
                AddHandler lnkbtn.Click, AddressOf Me.download_ServerClick
               


                cell7.Controls.Add(lnkbtn)

                row.Cells.Add(cell1)
                row.Cells.Add(cell2)
                row.Cells.Add(cell3)
                row.Cells.Add(cell4)
                row.Cells.Add(cell5)
                row.Cells.Add(cell6)
                row.Cells.Add(cell7)

                ' myTable.Rows.Add(row)

                If statusCSV = "True" Then
                    If dt.Rows(i).Item(7) <> "N/A" Then
                        cell8 = New HtmlTableCell()
                        Dim lnkbtnCSV As New LinkButton
                        lnkbtnCSV.ID = dt.Rows(i).Item(1) & "-" & dt.Rows(i).Item(10) & "csv"

                        lnkbtnCSV.CommandArgument = dt.Rows(i).Item(1) & "-" & dt.Rows(i).Item(10)
                        lnkbtnCSV.Text = "Download"
                        AddHandler lnkbtnCSV.Click, AddressOf Me.downloadCSV_ServerClick

                        cell8.Controls.Add(lnkbtnCSV)
                        row.Cells.Add(cell8)
                    Else
                        cell8 = New HtmlTableCell()
                        cell8.InnerText = "N/A"
                        row.Cells.Add(cell8)
                    End If
                    tbActivityBody.Controls.Add(row)

                Else
                    tbActivityBody.Controls.Add(row)
                End If
            Next
            'If CDbl(dt.Rows(0).Item(11)) > 3 Then lblTop.Text = "3" Else lblTop.Text = dt.Rows(0).Item(11)
            lblTotal.Text = dt.Rows(0).Item(11)
        End If
    End Sub



    Protected Sub bSave_Click(sender As Object, e As System.EventArgs) Handles bSave.Click
        If txtTicketNo.Text = "" Then
            'SelectActivities("All")
            Session("type") = "Filter-All"
            SelectActivities("Filter-All")
        ElseIf IsNumeric(txtTicketNo.Text) Then
            Session("type") = "Filter"
            SelectActivities("Filter")
        Else
            Utility.ShowSweet(Me, "Wrong input. Input should be numeric")
        End If
    End Sub


    Protected Sub aViewAll_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles aViewAll.ServerClick
        If aViewAll.InnerText = "View Top 3" Then
            Session("type") = "All"
            SelectActivities("All")
            aViewAll.InnerText = "View all"
        ElseIf aViewAll.InnerText = "View all" Then
            Session("type") = "Filter-All"
            SelectActivities("Filter-All")
            aViewAll.InnerText = "View Top 3"
        End If
    End Sub


    'Protected Sub lbCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancel.Click
    '    Response.Redirect("Preview.aspx")
    'End Sub

    'Protected Sub lbCancelRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelRequest.Click
    '    Response.Redirect("Preview.aspx")
    'End Sub

    'Protected Sub txtAccountNo_TextChanged(sender As Object, e As EventArgs) Handles txtAccountNo.TextChanged

    'End Sub
    <WebMethod()> _
    Public Shared Function jLog(ByVal value As String) As String
        Dim result As String = ""
        Dim pre As New Preview
       
        Dim IP As String = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList(0).ToString()
        BLL._insertAuditLogs(HttpContext.Current.Session("NAME"), HttpContext.Current.Session("USERID") & " " & IP & " : " & pre.GetIP(),
                             HttpContext.Current.Session("Role"), value, "View", Now)
        Return result
    End Function

    Protected Sub download_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            
            Dim btn As LinkButton = CType(sender, LinkButton)
            Dim value As String = btn.CommandArgument
            If File.Exists(HttpContext.Current.Server.MapPath("~\received\" & value & ".pdf")) Then
                HttpContext.Current.Response.ContentType = "application/pdf"
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" & value & ".pdf")
                HttpContext.Current.Response.TransmitFile(HttpContext.Current.Server.MapPath("~\received\" & value & ".pdf"))
                HttpContext.Current.Response.End()
            Else
                Utility.ShowSweet(Me, "File does not exists. File may have been removed")
            End If

            
        Catch ex As Exception
            Utility.ShowSweet(Me, "Error downloading file. File may have been removed")
        End Try
        
    End Sub

    Protected Sub downloadCSV_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim value As String = btn.CommandArgument


        HttpContext.Current.Response.ContentType = "application/pdf"
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" & value & ".pdf")
        HttpContext.Current.Response.TransmitFile(HttpContext.Current.Server.MapPath("~\receivedCSV\" & value & ".pdf"))
        HttpContext.Current.Response.End()
    End Sub
    Private Function GetIP() As String
        Dim externalip As String = ""
        externalip = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If externalip = "" Or externalip Is Nothing Then
            externalip = (HttpContext.Current.Request.ServerVariables("REMOTE_ADDR"))
        End If
        Return externalip
    End Function
End Class
