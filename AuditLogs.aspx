<%@ Page Title="Audit Logs" Language="VB" MasterPageFile="~/MasterPage.master"    EnableViewStateMac="false" 
AutoEventWireup="false" CodeFile="AuditLogs.aspx.vb" Inherits="AuditLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="page-header" style="padding-top:14px;padding-left:7px">
	 <h1 style="font-size: 15px;" >
								Audit Logs<small style="font-size: 14px; font-weight:inherit ">
									<i class="icon-double-angle-right"></i>
                                      view all system activity 
								</small>
							</h1><%-- <span class="pull-right" style="margin-top: -20px;padding-right:40px">
                            	Filter by <asp:TextBox ID="txtUname"  Width="150px" runat="server" placeholder="username"></asp:TextBox>
                                  	
                    <%-- <asp:linkbutton ID="bSave" runat="server" 
                                CssClass="btn btn-sm btn-purple" CausesValidation="False" ><i class="icon-filter"></i></asp:linkbutton>

 <asp:TextBox ID="txtSdate"  Width="120px" runat="server" placeholder="Start Date" class="date-picker" data-date-format="M dd, yyyy" ></asp:TextBox>
                    <asp:TextBox ID="txtEDate"  Width="120px" runat="server" placeholder="End Date" class="date-picker" data-date-format="M dd, yyyy" ></asp:TextBox>              	
                     <asp:linkbutton ID="lnkFilter" runat="server" 
                                CssClass="btn btn-sm btn-purple" CausesValidation="False" ><i class="icon-filter"></i></asp:linkbutton>


							      </span> --%>
         
        
             
				 			</div>		<div style="padding-left:30px;padding-right:30px;width:100%">	
                                     
                              			 
                                         <div class="widget-header">
												<h5 class="widget-title bigger lighter">
													<i class="ace-icon fa fa-table"></i>
													Event History
												</h5>
                                              <table class="pull-right" style="margin-top:6px;padding-right:10px">
                                                         <tr>

                                                             <td style="vertical-align:middle">Filter by  </td><td>&nbsp;</td>
                                                              
     <td>  <asp:Panel ID="panel2" runat="server" DefaultButton="bSave"	>
         <asp:TextBox ID="txtUname" class=""   Width="200px" runat="server" placeholder="Username" Height="26px"></asp:TextBox></td>
                                  	
                   <td>&nbsp;</td>
                  <td>&nbsp;</td><td> 
                       <asp:TextBox placeholder="Start Date" class="form-control date-picker" data-date-format="M dd, yyyy" Height="26px" ID="txtStartDate" Width="120px" runat="server"></asp:TextBox></td>
                                   <%-- <asp:Panel ID="panel1" runat="server" DefaultButton="bSave">	--%>
                      <td>&nbsp;</td> <td> to </td>  <td>&nbsp;</td>  <td>
                          
             <asp:TextBox placeholder="End Date" data-date-format="M dd, yyyy" ID="txtEndDate" class="form-control date-picker"   Width="120px" Height="26px" runat="server" ></asp:TextBox></td>
                                  	
                   <td>&nbsp;</td>  <td><asp:linkbutton ID="bSave" runat="server" Height="26px"
                                CssClass="btn btn-sm btn-purple" CausesValidation="False" ><i class="icon-filter"></i></asp:linkbutton>
                       </asp:panel>
                                    </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
                                    <%--</asp:Panel>--%>
</tr></table>  
										<asp:GridView runat="server" ID="gvReport" Visible="false"></asp:GridView>		 
											</div>
											<div class="widget-body pull-left" style="width:100%">
												<div class="widget-main no-padding">
													<table class="table table-striped table-bordered table-hover"> 
														<thead class="thin-border-bottom">
															<tr>
																<th>
																	<i class="ace-icon fa fa-user"></i>
																	User
																</th>

																<th>
																	Destination
																</th>
																<th >Role</th>
																<th >Ticket</th>
																<th >Action</th>
																<th >Timestamp</th>
                                                                <th >IP</th>
                                                                <th >Branch</th>
															</tr>
														</thead><tbody id="tb1" runat="server">

                                                             
														</tbody>
                                                        <tfoot  id="tbActivityFoot" runat="server"><tr>
                                                            <td colspan="8" style="text-align:right;font-style:italic">Displaying 
                                                             <asp:Label ID="lblTotal" runat="server">0</asp:Label> record(s) | 
                                                            <asp:LinkButton runat="server" ID="lnkExport">Export</asp:LinkButton></td></tr></tfoot>
													</table>
												</div>
											</div> 
										</div> 
     
</asp:Content>

