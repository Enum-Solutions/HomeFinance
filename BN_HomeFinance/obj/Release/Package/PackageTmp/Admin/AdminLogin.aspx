<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="BN_HomeFinance.Admin.AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Finance</title>
    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">

    <!-- Favicons -->
    <link href="/Assets/img/title-logo.png" rel="icon">
    <link href="/assets/img/apple-touch-icon.png" rel="apple-touch-icon">

    <!--  CSS Files -->
    <link rel="stylesheet" href="/Assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/Assets/css/Style.css" />
    <link rel="stylesheet" href="/Assets/css/datatables.min.css" />

    <!-- Template Main CSS File -->

    <script src="https://kit.fontawesome.com/89a09ea878.js"></script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="/Assets/js/bootstrap.min.js"></script>

    <script src="/Assets/js/bootstrap.bundle.min.js"></script>

    <script src="/Assets/js/datatables.min.js"></script>
    <style>
        body {
            background: url('../Assets/img/Background.jpg') !important;
            background-repeat: no-repeat !important;
            backdrop-filter: blur(5px);
            background-size: cover !important;
        }

        #blurry-effect {
            filter: blur(108px);
        }
    </style>
</head>
<body>

    <form id="form1" class="body-login" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
        <div class="container container-login shadow-login br">
            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c">
                    <img src="/Assets/img/logo.png" alt="Logo" class="img-fluid-s-login" />
                </div>
            </div>
            <div class="row">
                <h2 class="h2 w-100 t-c">Admin Login</h2>
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
            <div class="row mt-1 t-c" runat="server" id="divError">
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
            </div>
            <div class="row mt-4"></div>
        </div>
    </form>

</body>
</html>
