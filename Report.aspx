<%@ Page Title="Report" Language="VB" MasterPageFile="~/MasterPage.master"  MaintainScrollPositionOnPostback="true"   EnableViewStateMac="false" 
    AutoEventWireup="false" CodeFile="Report.aspx.vb" Inherits="Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="page-header" style="padding-top:14px;padding-left:7px">
	 <h1 style="font-size: 15px;" >
								Report<small style="font-size: 14px; font-weight:inherit ">
									<i class="icon-double-angle-right"></i>
                                      View Transaction Report
								</small>
							</h1> 	</div>	
                      
                            	<div style="padding-left:30px;padding-right:30px;width:100%" id="Div3">	
                                    <div class="widget-header"> 
                                        <div class="widget-title bigger lighter">

													<i class="ace-icon fa fa-table"></i>
													Recent Transaction Report from <asp:Label ID="lblUser" runat="server"></asp:Label>
												</div>
												 <%--<span class="pull-right" style="margin-top:6px;padding-right:10px">--%>
                            	
                                                <table class="pull-right" style="margin-top:6px;padding-right:10px">
                                                         <tr><td style="vertical-align:middle">Filter</td><td style="vertical-align:middle"><asp:TextBox ToolTip="Filter by Account number or ticketNo" ID="txtFilter"  Width="120px" runat="server" ></asp:TextBox></td><td>&nbsp;</td><td><asp:DropDownList Visible="false"  ID="ddlBranch" AutoPostBack="true" class=""   Width="120px" runat="server" ></asp:DropDownList></td>
                                  	
                   <td>&nbsp;</td>
                                                           <td style="vertical-align:middle">Filter by date from    </td><td>&nbsp;</td><td>  <asp:TextBox  class="form-control date-picker" data-date-format="M dd, yyyy"  ID="txtStartDate" Width="120px" runat="server"></asp:TextBox></td>
                                   <%-- <asp:Panel ID="panel1" runat="server" DefaultButton="bSave">	--%>
                                                   <td>&nbsp;</td> <td> to </td>  <td>&nbsp;</td>  <td> <asp:TextBox data-date-format="M dd, yyyy" ID="txtEndDate" class="form-control date-picker"   Width="120px" runat="server" ></asp:TextBox></td>
                                  	
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
															<%--<tr>
																<th>
																	<i class="ace-icon fa fa-user"></i>
																	Name
																</th>

																<th>
																	Ticket No.
																</th>
																<th >Destination</th> 
																<th >Pages</th>
																<th >Bank Charge</th> 
																<th >Additional Charge</th>
                                                                <th>Total</th>
                                                                <th >Time</th>
															</tr>--%>
														</thead>
                                                        <tbody  id="tbActivityBody" runat="server">
														 	</tbody><tfoot  id="tbActivityFoot" runat="server"><tr>
                                                            <td colspan="9" style="text-align:right;font-style:italic">Displaying 
                                                             <asp:Label ID="lblTotal" runat="server">0</asp:Label> record(s) | 
                                                            <asp:LinkButton runat="server" ID="lnkExport">Export</asp:LinkButton></td></tr></tfoot>
													</table>
												</div>
											</div> 	</div>  
								   
                                         
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
