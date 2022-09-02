<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BN_HomeFinance.Controls.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <title>Home Finance</title>

    <!-- Favicons -->
    <link href="Assets/img/title-logo.png" rel="icon">
    <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">

    <!--  CSS Files -->
    <link rel="stylesheet" href="Assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="Assets/css/Style.css" />
    <link rel="stylesheet" href="Assets/css/datatables.min.css" />

    <!-- Template Main CSS File -->

    <script src="https://kit.fontawesome.com/89a09ea878.js"></script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="Assets/js/bootstrap.min.js"></script>

    <script src="Assets/js/bootstrap.bundle.min.js"></script>

    <script src="Assets/js/datatables.min.js"></script>
    <style>
        body {
            background: url('../Assets/img/Background.jpg') !important;
            background-repeat: no-repeat !important;
            background-size: cover !important;
            backdrop-filter: blur(5px);
        }
    </style>
</head>
<body>
    <%--<video autoplay muted loop id="myVideo">
        <source src="Assets/video/Video.mp4" type="video/mp4">
    </video>--%>
    <form id="form" class="body-login" runat="server">
        
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
        <div class="container container-login shadow-login br">

            <div class="row">
                <div>
                    <a href="<%= BN_HomeFinance.Helper.pg_Index %>" class="text-decoration-none c-b" style="float:right;"><img src="Assets/img/close.png" height="10" width="10"/></a>
                </div>
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c">
                    <img src="Assets/img/logo.png" alt="Logo" class="img-fluid-s-login" />
                </div>
            </div>
            <div class="row">
                <h2 class="h2 w-100 t-c">Login</h2>
            </div>
            <div class="row mt-4">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                    <asp:Label ID="lblLogin" runat="server" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtLoginName" CssClass="form-control" placeholder="Enter Username" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFQUserName" runat="server" ErrorMessage="* Please Enter Username" ForeColor="Red" ValidationGroup="login" ControlToValidate="txtLoginName" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row mt-4">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="Enter Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFQPassword" runat="server" ErrorMessage="* Please Enter Password" ForeColor="Red" ValidationGroup="login" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row mt-4">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="login" runat="server" Text="Login" />
                </div>
            </div>
             <div class="row mt-1 t-c" runat="server" id="div1">
                 <asp:LinkButton ID="btnGoogle" OnClick="btnGoogle_Click" runat="server" Text="or Login With Google"></asp:LinkButton>
            </div>
            <div class="row mt-1 t-c" runat="server" id="divError">
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
            </div>
            <div class="row mt-4 mb-4">
                <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                    <a href="<%= BN_HomeFinance.Helper.pg_Signup %>" class="text-decoration-none c-b">Signup</a>
                </div>
                <div class="col-6 col-sm-6 col-md-6 col-lg-6 f-r">
                    <a href="<%= BN_HomeFinance.Helper.pg_ForgotPassword %>" class="text-decoration-none c-b f-r">Forgot Password?</a>
                </div>
            </div>
            <div class="row mt-4"></div>
        </div>
    </form>
</body>
</html>
