<%@ Page Title="Pricing" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Pricing.aspx.vb" Inherits="Pricing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="page-header" style="padding-top:14px;padding-left:7px">
	 <h1 style="font-size: 15px;" >
								eStatements<small style="font-size: 14px; font-weight:inherit ">
									<i class="icon-double-angle-right"></i>
                                     Add and view pricing
                                  
								</small>
							</h1> 	</div>	
    <table width="100%"><tr><td width="30%" runat="server" id="tdprice" style="padding-left:100px">
        <table>
            <tr><td>
                Price
                <br />
                <asp:TextBox runat="server" ID="txtpricing" Width="150px"></asp:TextBox>
                <br />
                </td></tr>
            <tr><td>
                Vendor Share
                <br />
                <asp:TextBox runat="server" ID="txtVendor" Width="150px"></asp:TextBox>
                <br />
                </td></tr>
            <tr>
                <td>Flat
                    <br />
                    <asp:CheckBox runat="server" ID="chkFlat" CssClass="btn btn-sm" />
                </td>
            </tr>
            <tr>
                <td>Staff Waiver
                    <br />
                    <asp:CheckBox runat="server" ID="chkStaff" CssClass="btn btn-sm" />
                </td>
            </tr>
            <tr>
                <td>Management Waiver
                    <br />
                    <asp:CheckBox runat="server" ID="chkMgt" CssClass="btn btn-sm" />
                </td>
            </tr>
            
            <tr>
                <td>&nbsp;
                    <br />
                    <asp:LinkButton runat="server" ID="btnadd" CssClass="btn btn-sm">Add</asp:LinkButton>
                </td>
            </tr>
            
        </table>
               </td> 
        <td width="50%" style="padding-right:300px; padding-left:17px"  valign="top">
            <table class="table table-striped table-bordered table-hover"> 
														<thead class="thin-border-bottom" id="tbActivityHead" runat="server">
															<tr>
																<th>
																	<i class="ace-icon fa fa-user"></i>
																	Price
																</th>

																<th>
																	User name
																</th>
																<th >Date Implemented</th> 
																<th >Status</th> 
                                                                <th >Mode</th>
                                                                <th >Staff</th>
                                                                <th >Management</th> 
                                                                <th >Vendor Share</th>
                                                               
															</tr>
														</thead>
                                                        <tbody  id="tbActivityBody" runat="server">
														 	</tbody><tfoot  id="tbActivityFoot" visible="false" runat="server"><tr>
                                                            <td colspan="8" style="text-align:right;font-style:italic">Displaying top 
                                                            <asp:Label ID="lblTop" runat="server">3</asp:Label> of <asp:Label ID="lblTotal" runat="server">50</asp:Label> | 
                                                            <a href="javascript:void()" id="aViewAll" runat="server" >View Top 100</a></td></tr></tfoot>
													</table>
        </td>
                        </tr></table>
</asp:Content>

