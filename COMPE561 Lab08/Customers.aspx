<%@ Page Language="C#" MasterPageFile="~/Arkham.master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="COMPE561_Lab08.Customers" title="Arkham Customer Directory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Customer Directory</h1>
    <asp:Repeater id="customersRepeater" runat="server">
        
        <ItemTemplate>
            Customer ID: <strong><%# Eval("CustId")%></strong><br />
            Name: <strong><%# Eval("FirstName")%> <%# Eval("LastName")%></strong><br />
            Email: <strong><%# Eval("Email")%></strong><br />
            Phone: <strong><%# Eval("Phone")%></strong>
        </ItemTemplate>

        <SeparatorTemplate>
            <hr />
        </SeparatorTemplate>

    </asp:Repeater>
</asp:Content>
