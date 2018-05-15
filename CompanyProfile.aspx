<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CompanyProfile.aspx.vb" Inherits="CompanyProfile" %>

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
                Company Name
                <br />
                <asp:TextBox runat="server" ID="txtCompany" Width="150px" Enabled="false"></asp:TextBox>
                <br />
                </td></tr>
            <tr><td>
                Domain
                <br />
                <asp:TextBox runat="server" ID="txtDomain" Width="150px" Enabled="false"></asp:TextBox>
                <br />
                </td></tr>
            <tr>
                <td>
                Country
                <br />
                <asp:TextBox runat="server" ID="txtCountry" Width="150px" Enabled="false"></asp:TextBox>
                <br />
                </td>
            </tr>
            <tr>
                <td>
                RCNO
                <br />
                <asp:TextBox runat="server" ID="txtRCNO" Width="150px" Enabled="false"></asp:TextBox>
                <br />
                </td>
            </tr>
            <tr>
                <td>Maker Checker
                    <br />
                    <asp:CheckBox runat="server" ID="chkChecker" CssClass="btn btn-sm" AutoPostBack="true" />
                   <%-- &nbsp; &nbsp;  <asp:LinkButton runat="server" ID="lnkChecker" CssClass="btn btn-sm btn-success">Change <i class="icon-edit"></i></asp:LinkButton>--%>
                </td> 
            </tr>
            
            
        </table>
               </td> 
        
                        </tr></table>
</asp:Content>

