Imports System.Data
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Partial Class Monitor
    Inherits System.Web.UI.Page
    Sub LoadAllPeriod()
        '    '   Dim dr As SqlDataReader = BLL._selectPeriodYear()
        '    With ddlbackupyear
        '        .DataSource = CType(BLL._selectPeriodYear()(0), DataTable)
        '        .DataBind()
        '        .SelectedValue = Now.Year
        '    End With
        With ddlsendyear
            .DataSource = BLL._selectPeriodYear()
            .DataTextField = "year"
            .DataValueField = "year"
            .DataBind()
            .SelectedValue = Now.Year
        End With
        With ddlgenerateyear
            .DataSource = BLL._selectPeriodYear()
            .DataTextField = "year"
            .DataValueField = "year"
            .DataBind()
            .SelectedValue = Now.Year
        End With
        '    With ddlbackupmonth
        '        .DataSource = CType(BLL._selectPeriodMonth(ddlbackupyear.SelectedValue)(0), DataTable)
        '        .DataBind()
        '        .SelectedValue = Now.Month - 1
        '    End With
        With ddlsendmonth
            '.DataSource = CType(BLL._selectPeriodMonth(ddlsendyear.SelectedValue)(0), DataTable)
            .DataSource = BLL._selectPeriodMonth(ddlsendyear.SelectedValue)
            .DataTextField = "month"
            .DataValueField = "monthNo"
            .DataBind()
            .SelectedValue = Now.Month - 1
        End With
        With ddlgeneratemonth
            '.DataSource = CType(BLL._selectPeriodMonth(ddlgenerateyear.SelectedValue)(0), DataTable)
            .DataSource = BLL._selectPeriodMonth(ddlgenerateyear.SelectedValue)
            .DataTextField = "month"
            .DataValueField = "monthNo"
            .DataBind()
            .SelectedValue = Now.Month - 1
        End With

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadAlltriggerbuttons()
        If Not Me.IsPostBack Then
            LoadBackupdetails()
            Loadgeneratedetails()
            Loadsenddetails()
        End If

        If Not Me.IsPostBack Then
            LoadAllPeriod()

        End If
    End Sub
    Sub Loadbackupprogresstask(status As Integer)
        Try
            Dim result(0 To 3) As String
            If status = BLL.Trigger.Begin Then
                result = BLL._selectstatuscount(BLL.Tasks.Backup)
                backupheadertask.Attributes.Remove("data-percent")
                backupheadertask.Attributes.Add("data-percent", "backing up - " & result(1) & "%")
                backupprogresstask.Attributes.Remove("style")
                backupprogresstask.Attributes.Add("style", "width:" & result(1) & "%")
            Else
                backupheadertask.Attributes.Remove("data-percent")
                backupheadertask.Attributes.Add("data-percent", "Not running")
                backupprogresstask.Attributes.Remove("style")
                backupprogresstask.Attributes.Add("style", "width:0%")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Sub Loadgenerateprogresstask(status As Integer)
        Try
            Dim result(0 To 3) As String
            If status = BLL.Trigger.Begin Then
                result = BLL._selectstatuscount(BLL.Tasks.Generation)
                generateheadertask.Attributes.Remove("data-percent")
                generateheadertask.Attributes.Add("data-percent", "Generating-->")
                '  generateheadertask.Attributes.Add("data-percent", "Generating - " & result(1) & "%")
                generateprogresstask.Attributes.Remove("style")
                generateprogresstask.Attributes.Add("style", "width:" & result(1) & "%")
            Else
                generateheadertask.Attributes.Remove("data-percent")
                generateheadertask.Attributes.Add("data-percent", "Not running")
                generateprogresstask.Attributes.Remove("style")
                generateprogresstask.Attributes.Add("style", "width:0%")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Sub Loadsendprogresstask(status As Integer)
        Try
            Dim result(0 To 3) As String
            If status = BLL.Trigger.Begin Then
                result = BLL._selectstatuscount(BLL.Tasks.Sending)
                divheadersend.Attributes.Remove("data-percent")
                divheadersend.Attributes.Add("data-percent", "Sending-->")
                divprogresssend.Attributes.Remove("style")
                divprogresssend.Attributes.Add("style", "width:" & result(1) & "%")
            Else
                divheadersend.Attributes.Remove("data-percent")
                divheadersend.Attributes.Add("data-percent", "Not running")
                divprogresssend.Attributes.Remove("style")
                divprogresssend.Attributes.Add("style", "width:0%")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Sub Loadgeneratedetails()
        Dim dt As DataTable = BLL._selecttrigger
        If dt.Rows(1).Item("Stat") = BLL.Trigger.Begin Then
            Loadgenerateprogresstask(BLL.Trigger.Begin)
        Else
            Loadgenerateprogresstask(BLL.Trigger.Drop)
        End If
    End Sub
    Sub Loadsenddetails()
        Dim dt As DataTable = BLL._selecttrigger
        If dt.Rows(2).Item("Stat") = BLL.Trigger.Begin Then
            Loadsendprogresstask(BLL.Trigger.Begin)
        Else
            Loadsendprogresstask(BLL.Trigger.Drop)
        End If
    End Sub
    Sub LoadBackupdetails()
        Dim dt As DataTable = BLL._selecttrigger
        If dt.Rows(0).Item("Stat") = BLL.Trigger.Begin Then
            Loadbackupprogresstask(BLL.Trigger.Begin)
        Else
            Loadbackupprogresstask(BLL.Trigger.Drop)
        End If

    End Sub

    'Protected Sub timerbackuptask_Tick(sender As Object, e As EventArgs) Handles timerbackuptask.Tick
    '    Dim commandstringcounter As String = "select count(id) from _tblcust"
    '    _aTotal.InnerHtml = "1,000,000"
    '    _aSuccess.InnerHtml = CDbl(SqlHelper.ExecuteScalar(BLL.Connectionstring, CommandType.Text, commandstringcounter)).ToString("#,###")
    '    divheaderbackup.Attributes.Remove("data-percent")
    '    Dim val As String = CDbl(_aSuccess.InnerHtml / _aTotal.InnerHtml * 100).ToString("##")
    '    divheaderbackup.Attributes.Add("data-percent", val & "%")
    '    divprogressbackup.Attributes.Remove("style")
    '    divprogressbackup.Attributes.Remove("class")
    '    If val < 40 Then
    '        divprogressbackup.Attributes.Add("class", "progress-bar progress-bar-danger")
    '    ElseIf val > 40 And val < 60 Then

    '        divprogressbackup.Attributes.Add("class", "progress-bar progress-bar-yellow")
    '    ElseIf val > 60 Then

    '        divprogressbackup.Attributes.Add("class", "progress-bar progress-bar-success")
    '    End If
    '    divprogressbackup.Attributes.Add("style", "width:" & val & "%")
    '    '  _aTotal.InnerHtml = Date.Now
    'End Sub

    Protected Sub lnkstartgenerate_Click(sender As Object, e As EventArgs) Handles lnkstartgenerate.Click
        Try
            If BLL._changetrigger(BLL.Tasks.Generation, BLL.Trigger.Begin) > 0 Then
                Startgenerating()
                '  Loadgeneratedetails()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkstopbackup_Click(sender As Object, e As EventArgs) Handles lnkstopbackup.Click
        Try
            If BLL._changetrigger(BLL.Tasks.Backup, BLL.Trigger.Drop) > 0 Then
                Stopbackingup()
                '    LoadBackupdetails()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkstartbackup_Click(sender As Object, e As EventArgs) Handles lnkstartbackup.Click
        Try
            If BLL._changetrigger(BLL.Tasks.Backup, BLL.Trigger.Begin) > 0 Then
                Startbackingup()
                ' LoadBackupdetails()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkstopgenerate_Click(sender As Object, e As EventArgs) Handles lnkstopgenerate.Click
        Try
            If BLL._changetrigger(BLL.Tasks.Generation, BLL.Trigger.Drop) > 0 Then
                Stopgenerating()
                Loadgeneratedetails()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkstartsending_Click(sender As Object, e As EventArgs) Handles lnkstartsending.Click
        Try
            If BLL._changetrigger(BLL.Tasks.Sending, BLL.Trigger.Begin) > 0 Then
                Startsending()
                Loadsenddetails()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkstopsending_Click(sender As Object, e As EventArgs) Handles lnkstopsending.Click
        Try
            If BLL._changetrigger(BLL.Tasks.Sending, BLL.Trigger.Drop) > 0 Then
                Stopsending()
                Loadsenddetails()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub Startbackingup()
        ddlbackupselect.Enabled = False
        lnkstopbackup.Enabled = True
        lnkstartbackup.Enabled = False
        lnkstartbackup.Text = "<i class=""icon-play grey""  style=""margin-left:-5px;font-size:15px""></i>"
        lnkstopbackup.Text = "<i class=""icon-pause green""  style=""margin-left:-5px;font-size:15px""></i>"
    End Sub
    Sub Stopbackingup()
        ddlbackupselect.Enabled = True
        lnkstopbackup.Enabled = False
        lnkstartbackup.Enabled = True
        lnkstartbackup.Text = "<i class=""icon-play green""  style=""margin-left:-5px;font-size:15px""></i>"
        lnkstopbackup.Text = "<i class=""icon-pause grey""  style=""margin-left:-5px;font-size:15px""></i>"
    End Sub
    Sub Startgenerating()
        ddlgenerateselect.Enabled = False
        lnkstopgenerate.Enabled = True
        lnkstartgenerate.Enabled = False
        lnkstartgenerate.Text = "<i class=""icon-play grey""  style=""margin-left:-5px;font-size:15px""></i>"
        lnkstopgenerate.Text = "<i class=""icon-pause green""  style=""margin-left:-5px;font-size:15px""></i>"
    End Sub
    Sub Stopgenerating()
        ddlgenerateselect.Enabled = True
        lnkstopgenerate.Enabled = False
        lnkstartgenerate.Enabled = True
        lnkstartgenerate.Text = "<i class=""icon-play green""  style=""margin-left:-5px;font-size:15px""></i>"
        lnkstopgenerate.Text = "<i class=""icon-pause grey""  style=""margin-left:-5px;font-size:15px""></i>"
    End Sub
    Sub Stopsending()
        ddlmailselect.Enabled = True
        lnkstopsending.Enabled = False
        lnkstartsending.Enabled = True
        lnkstartsending.Text = "<i class=""icon-play green""  style=""margin-left:-5px;font-size:15px""></i>"
        lnkstopsending.Text = "<i class=""icon-pause grey""  style=""margin-left:-5px;font-size:15px""></i>"
    End Sub
    Sub Startsending()
        ddlmailselect.Enabled = False
        lnkstopsending.Enabled = True
        lnkstartsending.Enabled = False
        lnkstartsending.Text = "<i class=""icon-play grey""  style=""margin-left:-5px;font-size:15px""></i>"
        lnkstopsending.Text = "<i class=""icon-pause green""  style=""margin-left:-5px;font-size:15px""></i>"
    End Sub
    Sub LoadAlltriggerbuttons()
        Dim dt As DataTable = BLL._selecttrigger
        If dt.Rows(0).Item("Stat") = BLL.Trigger.Begin Then
            Startbackingup()
        Else
            Stopbackingup()
        End If
        If dt.Rows(BLL.Tasks.Generation - 1).Item("Stat") = BLL.Trigger.Begin Then
            Startgenerating()
        Else
            Stopgenerating()
        End If
        If dt.Rows(BLL.Tasks.Sending - 1).Item("Stat") = BLL.Trigger.Begin Then
            Startsending()
        Else
            Stopsending()
        End If
    End Sub

    Protected Sub ddlbackupmonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlbackupmonth.SelectedIndexChanged
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Function _selectrecord(status As Integer, tasks As Integer) As SqlDataReader
        Return BLL._selectrecords(ddlbackupmonth.SelectedItem.Text.ToString, ddlbackupyear.SelectedItem.Text.ToString, status, tasks)
    End Function
    Sub Loadbackupsummary()
        Try
            Dim result(0 To 3) As String
            result = BLL._selectstatuscount(BLL.Tasks.Backup)
            divheaderbackup.Attributes.Remove("data-percent")
            divheaderbackup.Attributes.Add("data-percent", result(1) & "% backed up")
            divprogressbackup.Attributes.Remove("style")
            divprogressbackup.Attributes.Remove("class")
            _abackuptotal.InnerHtml = result(3)
            _abackupsuccessful.InnerHtml = result(0)
            _abackupfailure.InnerHtml = result(2)
            _abackuppending.InnerHtml = CDbl(result(2) / result(3) * 100).ToString("#,###.##") & "%"
            If Val(result(1)) < 40 Then
                divprogressbackup.Attributes.Add("class", "progress-bar progress-bar-danger")
            ElseIf Val(result(1)) > 40 And Val(result(1)) < 60 Then

                divprogressbackup.Attributes.Add("class", "progress-bar progress-bar-yellow")
            ElseIf Val(result(1)) > 60 Then

                divprogressbackup.Attributes.Add("class", "progress-bar progress-bar-success")
            End If
            divprogressbackup.Attributes.Add("style", "width:" & Val(result(1)) & "%")
        Catch ex As Exception

        End Try
    End Sub
    Sub Loadgeneratesummary()
        Try
            Dim result(0 To 3) As String
            result = BLL._selectstatuscount(BLL.Tasks.Generation)
            '   divheader.Attributes.Remove("data-percent")
            divheadergenerate.Attributes.Add("data-percent", result(1) & "% generated")
            divprogressgenerate.Attributes.Remove("style")
            divprogressgenerate.Attributes.Remove("class")
            _ageneratetotal.InnerHtml = result(3)
            _ageneratesuccessful.InnerHtml = result(0)
            _ageneratefailed.InnerHtml = result(2)
            _ageneratepending.InnerHtml = CDbl(result(2) / result(3) * 100).ToString("#,###.##") & "%"
            If Val(result(1)) < 40 Then
                divprogressgenerate.Attributes.Add("class", "progress-bar progress-bar-danger")
            ElseIf Val(result(1)) > 40 And Val(result(1)) < 60 Then

                divprogressgenerate.Attributes.Add("class", "progress-bar progress-bar-yellow")
            ElseIf Val(result(1)) > 60 Then

                divprogressgenerate.Attributes.Add("class", "progress-bar progress-bar-success")
            End If
            divprogressgenerate.Attributes.Add("style", "width:" & Val(result(1)) & "%")
        Catch ex As Exception

        End Try
    End Sub
    Sub Loadsendsummary()
        Try
            Dim result(0 To 3) As String
            result = BLL._selectstatuscount(BLL.Tasks.Sending)
            '   divheader.Attributes.Remove("data-percent")
            divaheadersend.Attributes.Add("data-percent", result(1) & "% sent")
            divaprogresssend.Attributes.Remove("style")
            divaprogresssend.Attributes.Remove("class")
            _asendtotal.InnerHtml = result(3)
            _asendsuccessful.InnerHtml = result(0)
            _asendfailed.InnerHtml = result(2)
            _asendpending.InnerHtml = CDbl(result(2) / result(3) * 100).ToString("#,###.##") & "%"
            If Val(result(1)) < 40 Then
                divaprogresssend.Attributes.Add("class", "progress-bar progress-bar-danger")
            ElseIf Val(result(1)) > 40 And Val(result(1)) < 60 Then

                divaprogresssend.Attributes.Add("class", "progress-bar progress-bar-yellow")
            ElseIf Val(result(1)) > 60 Then

                divaprogresssend.Attributes.Add("class", "progress-bar progress-bar-success")
            End If
            divaprogresssend.Attributes.Add("style", "width:" & Val(result(1)) & "%")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub timerbackuptask_Tick(sender As Object, e As EventArgs) Handles timerbackuptask.Tick
        Try
            LoadBackupdetails()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub timergeneratetask_Tick(sender As Object, e As EventArgs) Handles timergeneratetask.Tick
        Try
            Loadgeneratedetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub timersendtask_Tick(sender As Object, e As EventArgs) Handles timersendtask.Tick
        Try
            Loadsenddetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub timerbackupsummary_Tick(sender As Object, e As EventArgs) Handles timerbackupsummary.Tick
        Try
            Dim dt As DataTable = BLL._selecttrigger
            If dt.Rows(0).Item("Stat") = BLL.Trigger.Begin Then
                Loadbackupsummary()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub timergeneratesummary_Tick(sender As Object, e As EventArgs) Handles timergeneratesummary.Tick
        Try
            Dim dt As DataTable = BLL._selecttrigger
            If dt.Rows(1).Item("Stat") = BLL.Trigger.Begin Then
                Loadgeneratesummary()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub timersendsummary_Tick(sender As Object, e As EventArgs) Handles timersendsummary.Tick
        Try
            Dim dt As DataTable = BLL._selecttrigger
            If dt.Rows(2).Item("Stat") = BLL.Trigger.Begin Then
                Loadsendsummary()
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
