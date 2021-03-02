<%@ Page Title="MOL - Register" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="MyOwnList.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://www.google.com/recaptcha/api.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="divRegister">
        <div class="auth_divs">
            <h2>Do you already have an account?</h2>
            <h3>Click</h3>
            <h2>
                <asp:Button runat="server" ID="btnDoLogin" Text="HERE!" CssClass="btn btn-link" OnClick="btnDoLogin_Click" /></h2>
        </div>
        <div class="auth_divs">
            <label>Username:</label>
            <asp:TextBox ID="tbUsernameRegister" CssClass="form-control" runat="server"></asp:TextBox>
            <label>Email:</label>
            <asp:TextBox ID="tbEmailRegister" TextMode="Email" CssClass="form-control" runat="server"></asp:TextBox>
            <label>Password:</label>
            <asp:TextBox ID="tbPasswordRegister" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
            <label>Confirm Password:</label>
            <asp:TextBox ID="tbPassword2Register" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
            <label>Avatar:</label>
            <asp:FileUpload ID="fuImage" runat="server" />
            <div class="body">
                <asp:Label ID="lbMessageRegister" runat="server" Text=""></asp:Label>
                <div class="g-recaptcha" data-sitekey="6LdEp2waAAAAAJxlIcpAyXQklx_Wq2zkFR3W8Xk8"></div>
            </div>

            <asp:Button runat="server" ID="btnRegister" Text="Register" CssClass="btn btn-info" OnClick="btnRegister_Click" />
        </div>
    </div>

</asp:Content>
