<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="BN_HomeFinance.Controls.Signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Assets/img/title-logo.png" rel="icon" />
    <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon" />

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
    </style>
    <script src="Assets/js/Scripts/ckeditor/ckeditor.js"></script>
</head>
<body>
    <form id="form" runat="server">
        <div class="container rounded bg-white mb-5 mt-5">
            <div class="row">
                <div class="col-12 col-sm-12 col-md-3 col-lg-3 border-right">
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <asp:Image ID="imgAttachment" CssClass="rounded-circle mt-5" Width="150" Height="150" ImageUrl="~/Assets/img/default-image.jpg" runat="server" />
                        <br />
                        <h5 class="font-weight-bold">Profile Picture</h5>
                        <br />
                        <input type="button" class="btn btn-primary" onclick="ClickUploader();" id="changeImage" value="Change Image" />
                        <br />
                        <div id="divAttachment" runat="server">
                            <input type="button" class="btn btn-primary" onclick="ClickUploader();" id="UploadClick" value="Upload Image" />
                            <asp:FileUpload ID="fuImage" accept="image/*" CssClass="d-none" onchange="UploadFile(this);" AllowMultiple="false" runat="server" />
                        </div>
                        <br />
                        <label class="image-size mt-3">Recommended Image Size 200 x 200 (ratio 1:1)</label>
                    </div>
                </div>
                <div class="col-12 col-sm-12 col-md-6 col-lg-6 border-right">
                    <div class="p-3 py-5">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="text-right" runat="server" id="Heading">Sign Up</h3>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="Label1" runat="server" Text="Full Name"></asp:Label>
                            <asp:Label ID="Label3" ForeColor="Red" runat="server" Text="*"></asp:Label>
                            <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFQFullName" ControlToValidate="txtFullName" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="Label2" runat="server" Text="Login Name"></asp:Label>
                            <asp:Label ID="Label11" ForeColor="Red" runat="server" Text="*"></asp:Label>
                            <asp:TextBox ID="txtLoginName" MaxLength="8" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFQLoginName" ControlToValidate="txtLoginName" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                            <asp:Label ID="lblPasswordMandatory" ForeColor="Red" runat="server" Text="*"></asp:Label>
                            <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFQPassword" ControlToValidate="txtPassword" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                            <asp:Label ID="lblConfirmPasswordMandatory" ForeColor="Red" runat="server" Text="*"></asp:Label>
                            <asp:TextBox ID="txtConfirmPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFQConfirm" ControlToValidate="txtConfirmPassword" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label><asp:Label ID="Label4" ForeColor="Red" runat="server" Text="*"></asp:Label>
                            <asp:TextBox ID="txtEmail" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFQEmail" ControlToValidate="txtEmail" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" runat="server" id="divCountry" visible="false">
                            <asp:Label ID="Label6" runat="server" Text="Country"></asp:Label>
                            <asp:DropDownList ID="ddlCountry" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Select Country" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Oman" Value="Oman"></asp:ListItem>
                                <asp:ListItem Text="UAE" Value="UAE"></asp:ListItem>
                                <asp:ListItem Text="Kuwair" Value="Kuwait"></asp:ListItem>
                                <asp:ListItem Text="Saudia Arabia" Value="Saudia Arabia"></asp:ListItem>
                                <asp:ListItem Text="Qatar" Value="Qatar"></asp:ListItem>
                                <asp:ListItem Text="Bahrain" Value="Bahrain"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" runat="server" id="divWilayat" visible="false">
                            <asp:Label ID="Label7" runat="server" Text="City/Wilayat"></asp:Label>
                            <asp:TextBox ID="txtCity" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" runat="server" id="divContact" visible="false">
                            <asp:Label ID="Label8" runat="server" Text="Contact No."></asp:Label>
                            <asp:TextBox ID="txtContact" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" runat="server" id="divAddress" visible="false">
                            <asp:Label ID="Label9" runat="server" Text="Address"></asp:Label>
                            <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" runat="server" id="divAbout" visible="false">
                            <asp:Label ID="lblTextArea" runat="server" Text="About"></asp:Label>
                            <textarea id="txtAbout" class="form-control ckeditor" name="PropDesc" runat="server" rows="2"></textarea>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-1">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary profile-button" OnClick="btnSubmit_Click" ValidationGroup="Val" OnClientClick="return Validate();" runat="server" Text="Sign Up" />
                            <asp:Button ID="btnUpdate" CssClass="btn btn-primary profile-button" OnClick="btnUpdate_Click" ValidationGroup="Val" Visible="false" runat="server" Text="Update" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-primary profile-button" OnClick="btnCancel_Click" Visible="false" runat="server" Text="Delete" />
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-1">
                            <asp:Label ID="lblError" runat="server" Text="*" Visible="false" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Button ID="btnUpload" OnClick="btnUpload_Click" Text="Upload" runat="server" Style="display: none" />
                            <asp:Button ID="btnUploadCover" OnClick="btnUploadCover_Click" Text="Upload" runat="server" Style="display: none" />
                            <asp:HyperLink ID="hplResetPassword" Visible="false" runat="server">Reset Password</asp:HyperLink>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-12 col-md-3 col-lg-3">
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <asp:Image ID="imgAttachmentCover" CssClass="rounded-circle mt-5" Width="150" Height="150" ImageUrl="~/Assets/img/background-default.png" runat="server" /><br />
                        <h5 class="font-weight-bold">Cover Picture</h5>
                        <br />
                        <input type="button" class="btn btn-primary" onclick="ClickUploaderCover();" id="changeImageCover" value="Change Image" />
                        <br />
                        <div id="divAttachmentCover" runat="server">
                            <input type="button" class="btn btn-primary" onclick="ClickUploaderCover();" id="UploadClickCover" value="Upload Image" />
                            <asp:FileUpload ID="fuImageCover" accept="image/*" CssClass="d-none" onchange="UploadFileCover(this);" AllowMultiple="false" runat="server" />
                        </div>
                        <label class="image-size mt-3">Recommended Image Size 1092 x 276 (ratio 4:1)</label>
                    </div>
                </div>
            </div>
        </div>

    </form>

    <script>
        window.onload = Check;

        function Validate() {
            var P = document.getElementById("<%= txtPassword.ClientID%>");
            var CP = document.getElementById("<%= txtConfirmPassword.ClientID%>");
            var LG = document.getElementById("<%= txtLoginName.ClientID%>");
            debugger;
            if (LG.value.length > 2) {
                if (P.value != '' && CP.value != '') {
                    if (P.value == CP.value) {
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
            else {
                $(LG).focus();
                alert("Login Name Length should be minimum 3 Characters");
                return false;
            }
        }

            function UploadFile(fileUpload) {
                if (fileUpload.value != '') {
                    document.getElementById("<%=btnUpload.ClientID %>").click();
                }
            }

            function UploadFileCover(fileUpload) {
                if (fileUpload.value != '') {
                    document.getElementById("<%=btnUploadCover.ClientID %>").click();
                }
            }

            function Check() {
                var btn = document.getElementById("<%= divAttachment.ClientID%>");

            if (btn.style.display != "none") {
                document.getElementById("changeImage").style.display = "none";
            }

            var btn2 = document.getElementById("<%= divAttachmentCover.ClientID%>");

                if (btn2.style.display != "none") {
                    document.getElementById("changeImageCover").style.display = "none";
                }
            }

            function ClickUploader() {
                debugger;
                $("#<%= fuImage.ClientID%>").click();
            }

            function ClickUploaderCover() {
                debugger;
                $("#<%= fuImageCover.ClientID%>").click();
            }
    </script>
</body>
</html>
