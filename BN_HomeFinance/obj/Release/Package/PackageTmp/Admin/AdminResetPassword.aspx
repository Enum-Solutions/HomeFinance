<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="AdminResetPassword.aspx.cs" Inherits="BN_HomeFinance.Admin.AdminResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container rounded bg-white">
        <div class="row">
            <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
            <div class="col-12 col-sm-12 col-md-10 col-lg-10 border-right">
                <div class="p-3 py-5 mb-3">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h3 runat="server" class="text-right" id="header">Reset Password for</h3>
                        <asp:HiddenField ID="HDLogin" runat="server" />
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <label>New Password</label>
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <label>Confirm New Password</label>
                        <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-4">
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" OnClientClick="return Validate();" Text="Update Password" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-4" runat="server" id="dvError">
                        <h1 style="width: 100%; text-align: center">User Not Found.</h1>
                    </div>
                </div>
            </div>
            <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
        </div>
    </div>

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
</asp:Content>

