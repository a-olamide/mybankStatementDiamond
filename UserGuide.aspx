<%@ Page Title="User Guide" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="UserGuide.aspx.vb" Inherits="UserGuide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <iframe id="iframe1" runat="server" src="Documentation/mybankStatementUserGuide.pdf" width="100%" height="700px"></iframe>
    
</asp:Content>

