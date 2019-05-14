<%@ Page Language="C#" MasterPageFile="~/Arkham.master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="COMPE561_Lab08.Orders" title="Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Place an order</h1>

    <!--Error message label-->
    <asp:Label ID="ErrorMessage" ForeColor="Red" Text="" Visible="false" runat="server" />

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
    </p>

    <!--Shopping cart gridview-->
    Shopping cart:
    <hr />
    <p>
        <asp:GridView id="cartGrid" runat="server" AutoGenerateColumns="false">
            <columns>
                <asp:boundfield datafield="title" headertext="Title"/>
                <asp:boundfield datafield="isbn" headertext="ISBN"/>
                <asp:boundfield datafield="price" headertext="Price($)"/>
                <asp:boundfield datafield="qty" headertext="Quantity"/>
                <asp:boundfield datafield="linetotal" headertext="Line Total($)"/>
            </columns>
        </asp:GridView>
    </p>
    <hr />

    <!--QoL: Warning label-->
    <asp:Label ID="WarningLabel" ForeColor="Purple" Text="WARNING: Please use the quantity field to order 
        multiple copies of one title; 'duplicate' rows will be ignored!" Visible="true" runat="server" />

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