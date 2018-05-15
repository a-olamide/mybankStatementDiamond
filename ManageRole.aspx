<%@ Page Title="Manage Roles" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ManageRole.aspx.vb" Inherits="ManageRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
           <table width="100%">
                            <tr><td><div class="page-header" style="padding-top:14px;padding-left:7px">
<h1  style="font-size: 15px;" >
MANAGE USER ROLE <small   style="font-size: 14px;" >
<i class="icon-double-angle-right"></i>
Manage user role information 
</small>
</h1></div> </td></tr></table> 
                         <div class="widget-box" 
        style="vertical-align:top; padding-left: 20px; width:1100px" runat="server" id="divFirst">
                         <div class="widget-header widget-header-flat" 
         style=" width:1100px" align="left"><h4>USER ROLE MANAGEMENT</h4>
</div>
                                                <div class="widget-body" style="height:325px; vertical-align:top; width:1114px">
<div class="widget-main" style="vertical-align:top">
<table width="100%" style="color:Gray">
<tr>
<td width="30%" valign="top">

<table><tr>
<td>AD Role</td>  <td>
                      <asp:TextBox ID="tbADRole" runat="server" Width="165px"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="cErrFirstName" runat="server" 
                          ControlToValidate="tbADRole" ErrorMessage="*" ForeColor="Red" 
                          ValidationGroup="Insert"></asp:RequiredFieldValidator>
                  </td>
</tr>
  
   

              
              
               
              <tr>
                  <td >
                      System Role</td>
                  <td>
                      <asp:DropDownList ID="ddlRole" runat="server"  Width="165px"  
                          DataTextField="Name" DataValueField="RoleID" Enabled="False" >
                      </asp:DropDownList>
                      <asp:Label ID="sErrRole" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
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
                AutoGenerateColumns="False" EmptyDataText="No Roles assigned yet" 
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
                          <asp:Label ID="lblUserid" runat="server" Text='<%#Bind("ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:BoundField DataField="ADTitle" HeaderText="AD Title" 
                                    SortExpression="ID" ItemStyle-Font-Size="12px">
                                    <HeaderStyle BackColor="White"  />
                                    
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SystemRole" HeaderText="System Role" 
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
                            </asp:GridView></asp:Panel>
</td></tr>
</table></div></div></div>


            


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


