<%@ Page Language="C#" MasterPageFile="~/Arkham.master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="COMPE561_Lab08.Orders" title="Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Place an order</h1>
    <asp:Label ID="ErrorMessage" ForeColor="Red" runat="server" />

    <!--Customer select dropdown-->
    <p>
        Select customer:
        <br />
        <asp:DropDownList id="custList" runat="server" CssClass="dropdownmenu" />
    </p>

    <!--Book select dropdown-->
    <p>
        Select book title:
        <br />
        <asp:DropDownList id="bookList" runat="server" CssClass="dropdownmenu" />
    </p>

    <!--Quantity textbox w/ error check-->
    <p>
        Quantity:
        <asp:TextBox id="qTextBox" runat="server" CssClass="textbox" />

        <asp:RequiredFieldValidator id="qNumReq" runat="server" ControlToValidate="qTextBox"
            ErrorMessage="<br />Error: Required field!" Display="Dynamic" />

        <asp:CompareValidator id="qNumCheck" runat="server" ControlToValidate="qTextBox"
            Operator="DataTypeCheck" Type="Integer" ErrorMessage="<br />Error: Value must be a number!" Display="Dynamic" />
    </p>

    <!--Shopping cart repeater-->
    <p>
        My cart:<br />
        <asp:Repeater id="Cart" runat="server">
            <ItemTemplate>
                <asp:Textbox ID ="titleLabel" runat="server"
                    Text = <%# Eval("title")%> />
                <br />

                <asp:Label ID ="isbnLabel" runat="server">
                    <%# Eval("isbn")%>
                </asp:Label><hr />
            </ItemTemplate>
        </asp:Repeater>
    </p>

    <!--Add to cart, empty cart buttons-->
    <p>
        <asp:Button id="addButton" runat="server" CssClass="button" Text="Add to cart" OnClick="addButton_Click" />
        <asp:Button id="emptyButton" runat="server" CssClass="button" Text="Empty cart" OnClick="resetButton_Click" />
    </p>

    <!--Submit button-->
    <p>
        <asp:Button id="submitButton" runat="server" CssClass="button" Text="Purchase" OnClick="submitButton_Click" />
    </p>
</asp:Content>