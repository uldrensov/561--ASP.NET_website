<%@ Page Language="C#" MasterPageFile="~/Arkham.master" AutoEventWireup="true" CodeBehind="BooksByAuthor.aspx.cs" Inherits="COMPE561_Lab08.BooksByAuthor" Title="Books by Author"%> 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Search for books by author</h1>

    <!--Dropdown menu-->
    <p>
        Select author:
        <br />
        <asp:DropDownList id="authorList" runat="server" CssClass="dropdownmenu" />
    </p>

    <!--Submit button-->
    <p>
        <asp:Button id="submitButton" runat="server" CssClass="button" Text="Confirm" OnClick="submitButton_Click" />
    </p>

    <!--Display repeater-->
    <p>
        <asp:Repeater id="authorRepeater" runat="server">
        
        <ItemTemplate>
            Title: <strong><%# Eval("Title")%></strong><br />
            Author: <strong><%# Eval("FirstName")%> <%# Eval("LastName")%></strong><br />
            ISBN: <strong><%# Eval("Isbn")%></strong><br />
            Genre: <strong><%# Eval("Type")%></strong>
        </ItemTemplate>

        <SeparatorTemplate>
            <hr />
        </SeparatorTemplate>

    </asp:Repeater>
    </p>
</asp:Content>