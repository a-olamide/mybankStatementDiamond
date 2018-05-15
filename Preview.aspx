<%@ Page Title="Preview" Language="VB" MasterPageFile="~/MasterPage.master"  MaintainScrollPositionOnPostback="true"   EnableViewStateMac="false" 
AutoEventWireup="false" CodeFile="Preview.aspx.vb" Inherits="Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="page-header" style="padding-top:14px;padding-left:7px">
	 <h1 style="font-size: 15px;" >
								eStatements<small style="font-size: 14px; font-weight:inherit ">
									<i class="icon-double-angle-right"></i>
                                      Review bank statements 
								</small>
							</h1> 	</div>	
                      
                            	<div style="padding-left:30px;padding-right:30px;width:100%" id="Div3">	
                                    <div class="widget-header"> 
												 <span >
                            	Filter by ticket no.<asp:Panel ID="panel1" runat="server" DefaultButton="bSave">	
                                                       <asp:TextBox ID="txtTicketNo" Width="150px" runat="server"></asp:TextBox>
                                  	
                     <asp:linkbutton ID="bSave" runat="server" Height="26px"
                                CssClass="btn btn-sm btn-purple" CausesValidation="False" ><i class="icon-filter"></i></asp:linkbutton>
                                    </asp:Panel></span> 
				 		
											</div>
											<div class="widget-body" style="width:100%">
												<div class="widget-main no-padding">
													<table class="table table-striped table-bordered table-hover"> 
														<thead class="thin-border-bottom" id="tbActivityHead" runat="server">
															<%--<tr>
																<th>
																	<i class="ace-icon fa fa-user"></i>
																	Account Name
																</th>

																<th>
																	Ticket No.
																</th>
																<th >Source Bank</th> 
																<th >Country</th>
																<th >User</th> 
																<th >Last Modified Date</th>
                                                                <th>Status</th>
                                                                <th >CSV</th>
															</tr>--%>
														</thead>
                                                        <tbody  id="tbActivityBody" runat="server">
														 	</tbody><tfoot  id="tbActivityFoot" runat="server"><tr>
                                                            <td colspan="8" style="text-align:right;font-style:italic">Displaying top 
                                                            <asp:Label ID="lblTop" runat="server">3</asp:Label> of <asp:Label ID="lblTotal" runat="server">50</asp:Label> | 
                                                            <a href="javascript:void()" id="aViewAll" runat="server" >View all</a></td></tr></tfoot>
													</table>
												</div>
											</div> 	</div>  
					         <table>
                                 <tbody id="tb" runat="server">
                                     <%--<tr runat="server"><td runat="server">Omokore J. Ayodele</td><td>1000069</td><td>Access Bank Plc</td><td>Nigeria</td><td>omokore.ayodele@gmail.com</td><td>12/10/2015 4:11:00 PM</td><td runat="server"><asp:LinkButton runat="server" OnClick="download_ServerClick" CommandArgument="1000069-11">Download</asp:LinkButton></td></tr>--%>
                                 </tbody>

					         </table>
		<script src="js/SmartNotification.min.js" type="text/javascript"></script>
      	<script type="text/javascript" type="text/javascript">
      	    function jLog(value) {


      	        var obj = {};
      	        obj.value = value;

      	        var jsonData = JSON.stringify(obj);
      	        $.ajax({
      	            type: "POST",
      	            url: "Preview.aspx/jLog",
      	            data: jsonData,
      	            contentType: "application/octet-stream",
      	            dataType: "json", // dataType is json format
      	            success: function (response) {
      	                
      	            },
      	            error: function (response) {
      	                
      	            }
      	            
      	        });
      	    } </script>
</asp:Content>

