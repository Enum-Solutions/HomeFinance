<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="BN_HomeFinance.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Finance | Reset Password</title>

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
            background: var(--purple);
        }

        .form-control:focus {
            box-shadow: none;
            border-color: #BA68C8
        }

        .profile-button {
            background: rgb(99, 39, 120);
            box-shadow: none;
            border: none
        }

            .profile-button:hover {
                background: #682773
            }

            .profile-button:focus {
                background: #682773;
                box-shadow: none
            }

            .profile-button:active {
                background: #682773;
                box-shadow: none
            }

        .back:hover {
            color: #682773;
            cursor: pointer
        }

        .labels {
            font-size: 11px
        }

        .add-experience:hover {
            background: #BA68C8;
            color: #fff;
            cursor: pointer;
            border: solid 1px #BA68C8
        }

        span {
            font-size: 0.7rem;
        }

        .border-right {
            border-right: solid 1px var(--gray);
        }

        .carousel-item {
            margin-right: 0% !important;
        }

        #imgUploader:hover {
            cursor: pointer !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container rounded bg-white">
            <div class="row">
                <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
                <div class="col-12 col-sm-12 col-md-10 col-lg-10 border-right">
                    <div class="p-3 py-5 mb-3">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="text-right" runat="server" id="header">Reset Password</h3>
                            <asp:HiddenField ID="HDLogin" runat="server" />
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblOldPassword" runat="server" Text="Old Password"></asp:Label><label class="mandatory"> *</label>
                            <asp:TextBox CssClass="form-control" ID="txtOldPassword" TextMode="Password" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFQOldPassword" ForeColor="Red" Display="Dynamic" ControlToValidate="txtOldPassword" ValidationGroup="Val" runat="server" ErrorMessage="This is a required field."></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblPassword" runat="server" Text="New Password"></asp:Label><label runat="server" id="lblMandatoryPassword" class="mandatory"> *</label>
                            <asp:TextBox CssClass="form-control" ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm New Password"></asp:Label><label runat="server" id="lblMandatoryConfirmPassword" class="mandatory"> *</label>
                            <asp:TextBox CssClass="form-control" ID="txtConfirmPassword" TextMode="Password" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-4">
                            <asp:Button ID="btnReset" CssClass="btn btn-primary" OnClick="btnReset_Click" ValidationGroup="Val" OnClientClick="return Validate();" runat="server" Text="Reset Password" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-Secondary" OnClick="btnCancel_Click" runat="server" Text="Cancel" />
                        </div>
                    </div>
                </div>
                <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
            </div>
        </div>
    </form>

    <script>
        function Validate() {
            debugger;
            var P = document.getElementById("<%= txtPassword.ClientID%>");
            var CP = document.getElementById("<%= txtConfirmPassword.ClientID%>");

            if (P.value != '' && CP.value != '') {
                if (P.value == CP.value) {
                    var LG = document.getElementById("<%= HDLogin.ClientID%>");
                    if (LG.value.toUpperCase() != P.value.toUpperCase()) {
                        return true;
                    }
                    else {
                        alert("Your Password cannot be same as your Login.");
                        return false;
                    }
                }
                else {
                    alert("Password and Confirm Passwords does'nt Match.");
                    return false;
                }
            }
            else {
                alert("Please Enter Correct Value.");
                return false;
            }
        }
    </script>
</body>
</html>
