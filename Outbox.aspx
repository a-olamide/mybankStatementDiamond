<%@ Page Title="Outbox" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Outbox.aspx.vb" Inherits="Outbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    <script type="text/javascript">
        window.jQuery || document.write("<script src='assets/js/jquery-2.0.3.min.js'>" + "<" + "/script>");
</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="page-header" style="padding-top:14px;padding-left:7px">
<h1 style="font-size: 15px;" >
Outbox<small style="font-size: 14px; font-weight:inherit ">
<i class="icon-double-angle-right"></i>
                                      Transactions pending delivery
</small>
</h1> </div> 
                      
                            <div style="padding-left:30px;padding-right:30px;width:100%" id="Div3"> 
                                    <div class="widget-header"> 
                                        <div class="widget-title bigger lighter">

<i class="ace-icon fa fa-table"></i>
List of statements pending delivery <asp:Label ID="lblUser" runat="server"></asp:Label>
</div>
<%--<span class="pull-right" style="margin-top:6px;padding-right:10px">--%>
                           
                                                <table class="pull-right" style="margin-top:6px;padding-right:10px">
                                                         <tr><td><asp:DropDownList Visible="false"  ID="ddlBranch" AutoPostBack="true" class=""   Width="200px" runat="server" ></asp:DropDownList></td>
                                 
                   <td>&nbsp;</td>
                                
                   <td>&nbsp;</td>  <td><asp:linkbutton ID="bSave" runat="server" Height="26px"
                                CssClass="btn btn-sm btn-purple" CausesValidation="False" ><i class="icon-filter"></i></asp:linkbutton></td>
                                    <%--</asp:Panel>--%>
</tr></table>   
<%-- </span> --%>
<asp:GridView runat="server" ID="gvReport" Visible="false"></asp:GridView>
</div>
<div class="widget-body" style="width:100%">
<div class="widget-main no-padding">

                                                


<table class="table table-striped table-bordered table-hover"> 
<thead class="thin-border-bottom" id="tbActivityHead" runat="server">
</thead>
                                                        <tbody  id="tbActivityBody" runat="server">
</tbody>
                                                        <tfoot  id="tbActivityFoot" runat="server"><tr>
                                                            <td colspan="8" style="text-align:right;font-style:italic">Displaying 
                                                             <asp:Label ID="lblTotal" runat="server">0</asp:Label> record(s)
                                                            <%--<asp:LinkButton runat="server" ID="lnkExport">Export</asp:LinkButton>--%>

                                                            </td></tr>

       </tfoot>
</table>
</div>
</div> </div>  
  
                                         
<script src="js/SmartNotification.min.js" type="text/javascript"></script>
      <script type="text/javascript" type="text/javascript">
     
       
         function jLog(value) {


             var obj = {};
             obj.value = value;

             var jsonData = JSON.stringify(obj);
             $.ajax({
                 type: "POST",
                 url: "DashBoard.aspx/jLog",
                 data: jsonData,
                 contentType: "application/json; charset=utf-8",
                 dataType: "json", // dataType is json format
                 success: OnSuccess,
                 
             });
         } </script>
</asp:Content>