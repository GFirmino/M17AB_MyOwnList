<%@ Page Title="MOL - Login" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="MyOwnList.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://www.google.com/recaptcha/api.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divLogin">
        <div class="auth_divs">
            <h2>Don't have an account yet?</h2>
            <h3>Click</h3>
            <h2>
                <asp:Button runat="server" ID="btnDoRegister" Text="HERE!" CssClass="btn btn-link" OnClick="btnDoRegister_Click" /></h2>
        </div>
        <div class="auth_divs">
            <label>Email:</label>
            <asp:TextBox ID="tbEmailLogin" TextMode="Email" CssClass="form-control" runat="server"></asp:TextBox>
            <label>Password:</label>
            <asp:TextBox ID="tbPasswordLogin" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>

            <div class="body">
                <asp:Label ID="lbMessageLogin" runat="server" Text=""></asp:Label>
                <div class="g-recaptcha" data-sitekey="6LdEp2waAAAAAJxlIcpAyXQklx_Wq2zkFR3W8Xk8"></div>
            </div>

            <asp:Button runat="server" ID="btnLogin" Text="Login" CssClass="btn btn-info" OnClick="btnLogin_Click" />
        </div>
    </div>
</asp:Content>
