<%@ Page Title="Manage User" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CreateUser.aspx.vb" Inherits="CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
           <table width="100%">
                            <tr><td><div class="page-header" style="padding-top:14px;padding-left:7px">
<h1  style="font-size: 15px;" >
MANAGE USER <small   style="font-size: 14px;" >
<i class="icon-double-angle-right"></i>
Manage user information 
</small>
</h1></div> </td></tr></table> 
                         <div class="widget-box" 
        style="vertical-align:top; padding-left: 20px; width:1100px" runat="server" id="divFirst">
                         <div class="widget-header widget-header-flat" 
         style=" width:1100px" align="left"><h4>USER MANAGEMENT</h4>
                                <span class="pull-right" style="margin-top:6px;padding-right:10px">
                            	  <asp:TextBox runat="server" type="text" ID="txtFlter2" style="width:150px;margin-left:10px;"></asp:TextBox>
                                                    
                                                 
                                  	
                     <asp:LinkButton runat="server" ID="aSearch" class="btn btn-sm btn-purple"><i class="icon-filter"></i></asp:LinkButton></span>

								<span style="display:inline;padding-top:10px;margin-left:10px;" class="pull-right">Search by name, solID, or username	</span>	
                             <span class="pull-right" style="padding:10px">
             <asp:LinkButton runat="server" ID="lnkResetInfo"> Reset All info  <i class="icon-refresh"></i></asp:LinkButton></span>
</div>
                                                <div class="widget-body" style="height:390px; vertical-align:top; width:1114px;">
<div class="widget-main" style="vertical-align:top">
<table width="100%" style="color:Gray">
<tr>
<td width="30%" valign="top">

<table><tr>
<td>First Name</td>  <td>
                      <asp:TextBox ID="tbFirstName" runat="server" Width="165px"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="cErrFirstName" runat="server" 
                          ControlToValidate="tbFirstName" ErrorMessage="*" ForeColor="Red" 
                          ValidationGroup="Insert"></asp:RequiredFieldValidator>
                  </td>
</tr>
  <tr>
                  <td>
                      Surname</td>
                  <td>
                      <asp:TextBox ID="tbSurname"  Width="165px" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="cErrSurname" runat="server" 
                          ControlToValidate="tbSurname" ErrorMessage="*" ForeColor="Red" 
                          ValidationGroup="Insert"></asp:RequiredFieldValidator>
                  </td>
              </tr>
   <tr>
                 
              </tr>

              <tr>
                  <td class>
                      Email</td>
                  <td>
                      <asp:TextBox ID="tbUserName"  Width="165px" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="cErrUserName" runat="server" 
                          ControlToValidate="tbUserName" ErrorMessage="*" ForeColor="Red" 
                          ValidationGroup="Insert"></asp:RequiredFieldValidator>
                      <asp:Label ID="sErrUserName" runat="server" ForeColor="Red" Text="*" 
                          Visible="False"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class>
                      Branch</td>
                  <td>
                      <asp:TextBox ID="txtBranch"  Width="165px" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                          ControlToValidate="txtBranch" ErrorMessage="*" ForeColor="Red" 
                          ValidationGroup="Insert"></asp:RequiredFieldValidator>
                      <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*" 
                          Visible="False"></asp:Label>
                  </td>
              </tr>
               <tr>
                  <td class>
                      Branch Code</td>
                  <td>
                      <asp:TextBox ID="txtBranchCode"  Width="165px" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                          ControlToValidate="txtBranchCode" ErrorMessage="*" ForeColor="Red" 
                          ValidationGroup="Insert"></asp:RequiredFieldValidator>
                      <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*" 
                          Visible="False"></asp:Label>
                  </td>
              </tr>
    <tr>
                  <td class>
                      Sol ID</td>
                  <td>
                      <asp:TextBox ID="txtSolID"  Width="165px" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                          ControlToValidate="txtBranchCode" ErrorMessage="*" ForeColor="Red" 
                          ValidationGroup="Insert"></asp:RequiredFieldValidator>
                      <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*" 
                          Visible="False"></asp:Label>
                  </td>
              </tr>
    <tr>
                  <td class>
                      Supervisor Email</td>
                  <td>
                      <asp:TextBox ID="txtSupervisor"  Width="165px" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                          ControlToValidate="txtSupervisor" ErrorMessage="*" ForeColor="Red" 
                          ValidationGroup="Insert"></asp:RequiredFieldValidator>
                      <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*" 
                          Visible="False"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td >
                      Company Role</td>
                  <td>
                      <asp:DropDownList ID="ddlRole" runat="server"  Width="165px"  
                          DataTextField="Name" DataValueField="RoleID" Enabled="False" >
                      </asp:DropDownList>
                      <asp:Label ID="sErrRole" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                  </td>
              </tr> 
              
              
              <tr  ID="trAdmin" runat="server" visible="false">
                  <td >
                      Admin</td>
                  <td>
                     <asp:CheckBox ID="cbAdmin" Visible="true " runat="server" />
                  </td>
              </tr>
              <tr  ID="trActive" runat="server" >
                  <td >
                      Active</td>
                  <td>
                     <asp:CheckBox ID="cbActive" Visible="true " runat="server" />
                  </td>
              </tr>
              <tr>
                  <td >
                      <asp:Label ID="lID" runat="server" Text="0" Visible="False"></asp:Label>
                  </td>
                  <td>
                     <asp:linkbutton ID="bSave" runat="server" Text="Create" 
                                CssClass="btn btn-sm btn-success" CausesValidation="False" ></asp:linkbutton>&nbsp; &nbsp; <asp:LinkButton id="lnkRefresh" CausesValidation="false" runat="server"  Text="Refresh" CssClass="btn btn-sm btn-info" Width="85px">
Refresh <i class="icon-refresh"></i>
</asp:LinkButton>
                  </td>
              </tr>   
</table>

</td> <td width="70%" valign="top" rowspan="15"><asp:Panel Height="250px" ScrollBars="Auto" runat="server" ID="pnl1">
<asp:GridView ID="gvUsers" runat="server" BackColor="White" BorderColor="#F0F0F0"  BorderWidth="1px" ForeColor="Gray"
                                BorderStyle="Solid"  GridLines="None" Width="100%" CaptionAlign="Left"
                            CssClass="cGview" 
                AutoGenerateColumns="False" EmptyDataText="No user created" 
             Font-Size="12px" ShowHeader="true">
           <AlternatingRowStyle BackColor="White" ForeColor="Gray" />
                          
        <Columns>
             <asp:TemplateField  ItemStyle-HorizontalAlign="Center"  ><HeaderStyle  BackColor="White"/>
                                        <ItemTemplate> 
                           
                                           <asp:LinkButton  ID="btnEdit" ToolTip="Edit User" CommandArgument="<%# Container.DataItemIndex %>" runat="server" Text="Edit" Font-Underline="true"  CausesValidation="False">
                                           </asp:LinkButton>
                          
                                  
                                     
                                </ItemTemplate>
                                           <ItemStyle   />
                                  </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Userid" Visible= "false" >
                                    <ItemTemplate>
                          <asp:Label ID="lblUserid" runat="server" Text='<%#Bind("Email")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:BoundField DataField="FirstName" HeaderText="Name" 
                                    SortExpression="ID" ItemStyle-Font-Size="12px">
                                    <HeaderStyle BackColor="White"  />
                                    
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Surname" HeaderText="Surname" 
                                    SortExpression="ID" ItemStyle-Font-Size="12px">
                                    <HeaderStyle BackColor="White"  />
                                    
                                    </asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email" 
                                    SortExpression="ID" ItemStyle-Font-Size="12px">
                                    <HeaderStyle BackColor="White"  />
                                    
                                    </asp:BoundField>
                <asp:BoundField DataField="BranchName" HeaderText="Branch" 
                                    SortExpression="ID" ItemStyle-Font-Size="12px">
                                    <HeaderStyle BackColor="White"  />
                                    
                                    </asp:BoundField>                    
                                     <asp:CheckBoxField DataField="Admin" HeaderText="Admin" 
                                            SortExpression="Admin" HeaderStyle-BackColor="White" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                            
           
            
            <asp:BoundField DataField="Role" HeaderText="Role" 
                                    SortExpression="ID" ItemStyle-Font-Size="12px">
                                    <HeaderStyle BackColor="White"  />
                                    
                                    </asp:BoundField>

              <asp:CheckBoxField DataField="Status" HeaderText="Active" 
                                            SortExpression="Active" HeaderStyle-BackColor="White" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                  
   
             
                              <asp:TemplateField ><HeaderStyle  BackColor="White"/>
                                <ItemTemplate >
                                    <center>
                                       <span runat="server" id="img"  class="Label" >
                                           <asp:LinkButton ID="imgDelete" CommandArgument="<%# Container.DataItemIndex %>" runat="server" style="visibility: inherit" OnClientClick="if (confirm('Are you sure you want to delete this user?')==true)return true;else return false;" CausesValidation="False">
                                           <i class="icon-trash grey"></i></asp:LinkButton>
                                     
                                           </span>
                                    
                                    </center>
                                </ItemTemplate>
                            </asp:TemplateField>  
   
                                        
        </Columns> <FooterStyle BackColor="#CCCCCC" />
              <AlternatingRowStyle BackColor="#EEEEEE" />
                                <HeaderStyle BackColor="#0099CC" Font-Bold="True" ForeColor="Gray" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <RowStyle BorderColor="#CCCCCC" />
                                <SortedAscendingCellStyle BackColor="#F9F9F9" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
    <br />
    <span class="pull-right" ><asp:linkbutton runat="server" ID="exportUser">Export</asp:linkbutton></span>
                                                </asp:Panel>
</td></tr>
</table></div></div></div>


            
    <asp:GridView ID="gvExportUser" Visible="false" runat="server"></asp:GridView>

            <div class="col-xs-12 col-sm-3 widget-container-span" runat="server" style="display:none" id="pnlInfo">
<div class="widget-box light-border">
<div class="widget-header header-color-dark">
 
<div class="widget-toolbar" style="vertical-align:middle">
<asp:LinkButton ID="lnkRedirect" runat="server" ForeColor="White" CausesValidation="false"> <i class="icon-remove"></i></asp:LinkButton> 
</div>
</div>

<div class="widget-body">
<div class="widget-main padding-6" align="center">
<div class="alert alert-info" align="center">  <asp:Label runat="server" ID="lblInfo" Font-Bold="True" Text="ydfasd"></asp:Label> </div>
</div>
</div>
</div>
</div>
    
       
 
                 <div class="widget-box" id = "pnlReasonForCancellation" runat="server" 
                                style="display:none; width: 400px;">
<div class="widget-header">
<h5>
                                                    <asp:Label ID="lblReasonHeader" runat="server"  ></asp:Label></h5>
                                                    </div> 

                                        </div>
                                               <table runat="server" id="tblShowError" visible="false"><tr><td> <asp:Label runat="server" ID="lblShowError" ForeColor="Red" ></asp:Label></td></tr></table>
  
</asp:Content>


