Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports InfoSoftGlobal
Imports System.Data.SqlClient

Public Class FusionChart
    Dim dt As DataTable = New DataTable("Chart")
    Dim X, Y As String
    Dim GraphWidth As String = "450"
    Dim GraphHeight As String = "420"
    Dim color(12) As String


    Public Function createGraph() As String
        ConfigureColors()
        LoadGraphData()
        Return CreateBarGraph()
    End Function


    'protected void Page_Load(object sender, EventArgs e)
    '{
    '    ConfigureColors()
    '    LoadGraphData()
    '    CreateLineGraph()
    '    CreateBarGraph()
    '    CreatePieGraph()
    '    CreateDoughnutGraph()
    '}

    Private Sub ConfigureColors()
        color(0) = "AFD8F8"
        color(1) = "F6BD0F"
        color(2) = "8BBA00"
        color(3) = "FF8E46"
        color(4) = "008E8E"
        color(5) = "D64646"
        color(6) = "8E468E"
        color(7) = "588526"
        color(8) = "B3AA00"
        color(9) = "008ED6"
        color(10) = "9D080D"
        color(11) = "A186BE"
    End Sub



    Public Function CreateBarGraph() As String
        Dim strCaption As String = "Month wise Product Distribution"
        Dim strSubCaption As String = "For the year 2010"
        Dim xAxis As String = "Month"
        Dim yAxis As String = "Qnty"
        Dim strXML As String = Nothing


        strXML = "<graph caption='" + strCaption + "' subCaption='" + strSubCaption + "' decimalPrecision='0' pieSliceDepth='30' formatNumberScale='0' xAxisName='" + xAxis + "'  xAxisName='" + xAxis + "' yAxisName='" + yAxis + "' rotateNames='1'>"

        Dim i As Int16 = 0

        For Each dr2 As DataRow In dt.Rows
            strXML += "<set name='" + dr2(0).ToString() + "' value='" + dr2(1).ToString() + "' color='" + color(i) + "'  link=&quot;JavaScript:myJS('" + dr2("Month").ToString() + ", " + dr2("Qnty").ToString() + "'); &quot;/>"

        Next


        'Finally, close <graph> element
        strXML += "</graph>"
        Return strXML
        'Dim a As String = FusionCharts.RenderChartHTML(
        '    "FusionCharts/FCF_Column3D.swf",
        '    "",
        '    strXML,
        '    "mygraph1",
        '    GraphWidth, GraphHeight,
        '    False
        '    )

    End Function



    Private Sub LoadGraphData()
        Dim dc As DataColumn = New DataColumn("Month", Type.GetType("System.String"))
        Dim dc1 As DataColumn = New DataColumn("Qnty", Type.GetType("System.String"))
        dt.Columns.Add(dc)
        dt.Columns.Add(dc1)
        Dim dr1 As DataRow = dt.NewRow()
        dr1(0) = "January"
        dr1(1) = 8465
        dt.Rows.Add(dr1)
        Dim dr2 As DataRow = dt.NewRow()
        dr2(0) = "February"
        dr2(1) = 9113
        dt.Rows.Add(dr2)
        Dim dr3 As DataRow = dt.NewRow()
        dr3(0) = "March"
        dr3(1) = 18305
        dt.Rows.Add(dr3)
        Dim dr4 As DataRow = dt.NewRow()
        dr4(0) = "April"
        dr4(1) = 23839
        dt.Rows.Add(dr4)
        Dim dr5 As DataRow = dt.NewRow()
        dr5(0) = "May"
        dr5(1) = 11167
        dt.Rows.Add(dr5)
        Dim dr6 As DataRow = dt.NewRow()
        dr6(0) = "June"
        dr6(1) = 8838
        dt.Rows.Add(dr6)
        Dim dr7 As DataRow = dt.NewRow()
        dr7(0) = "July"
        dr7(1) = 10800
        dt.Rows.Add(dr7)
        Dim dr8 As DataRow = dt.NewRow()
        dr8(0) = "August"
        dr8(1) = 12115
        dt.Rows.Add(dr8)
        Dim dr9 As DataRow = dt.NewRow()
        dr9(0) = "September"
        dr9(1) = 7298
        dt.Rows.Add(dr9)
        Dim dr10 As DataRow = dt.NewRow()
        dr10(0) = "October"
        dr10(1) = 13186
        dt.Rows.Add(dr10)
        Dim dr11 As DataRow = dt.NewRow()
        dr11(0) = "November"
        dr11(1) = 10460
        dt.Rows.Add(dr11)
        Dim dr12 As DataRow = dt.NewRow()
        dr12(0) = "December"
        dr12(1) = 9964
        dt.Rows.Add(dr12)
    End Sub
End Class