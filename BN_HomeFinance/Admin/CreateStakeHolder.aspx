<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="CreateStakeHolder.aspx.cs" Inherits="BN_HomeFinance.Admin.CreateStakeHolder" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container rounded bg-white mb-5">
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
                        <h3 class="text-right">User Profile</h3>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblUserType" runat="server" Text="User Type"></asp:Label>
                        <asp:Label ID="Label12" ForeColor="Red" runat="server" Text="*"></asp:Label>
                        <asp:DropDownList ID="ddlUserType" CssClass="form-select" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFQUserType" ControlToValidate="ddlUserType" InitialValue="-1" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="Label1" runat="server" Text="Full Name"></asp:Label>
                        <asp:Label ID="Label3" ForeColor="Red" runat="server" Text="*"></asp:Label>
                        <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFQFullName" ControlToValidate="txtFullName" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="Label2" runat="server" Text="Enter Login"></asp:Label>
                        <asp:Label ID="Label11" ForeColor="Red" runat="server" Text="*"></asp:Label>
                        <asp:TextBox ID="txtFullLoginName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFQLoginName" ControlToValidate="txtFullLoginName" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblPassword" runat="server" Text="Enter Password"></asp:Label>
                        <asp:Label ID="lblPasswordMandatory" ForeColor="Red" runat="server" Text="*"></asp:Label>
                        <asp:TextBox ID="txtFullPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFQPassword" ControlToValidate="txtFullPassword" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                        <asp:Label ID="lblConfirmPasswordMandatory" ForeColor="Red" runat="server" Text="*"></asp:Label>
                        <asp:TextBox autocomplete="off" ID="txtConfirmPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFQConfirm" ControlToValidate="txtConfirmPassword" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
                        <asp:Label ID="Label13" ForeColor="Red" runat="server" Text="*"></asp:Label>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFQEmail" ControlToValidate="txtEmail" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
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
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
                        <asp:Label ID="Label7" runat="server" Text="City/Wilayat"></asp:Label>
                        <asp:TextBox ID="txtCity" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
                        <asp:Label ID="Label8" runat="server" Text="Contact No."></asp:Label>
                        <asp:TextBox ID="txtContact" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
                        <asp:Label ID="Label9" runat="server" Text="Address"></asp:Label>
                        <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
                        <asp:Label ID="Label10" runat="server" Text="Location Pin"></asp:Label>
                        <asp:TextBox ID="txtPin" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
                        <asp:Label ID="lblTextArea" runat="server" Text="About"></asp:Label>
                        <textarea id="txtAbout" class="form-control" runat="server" rows="2"></textarea>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="Label4" runat="server" Text="User Status"></asp:Label>
                        <asp:DropDownList ID="ddlStatus" CssClass="form-select" runat="server">
                            <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                            <asp:ListItem Text="InActive" Value="InActive"></asp:ListItem>
                            <asp:ListItem Text="Locked" Value="Locked"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-1">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary profile-button" OnClick="btnSubmit_Click" ValidationGroup="Val" OnClientClick="return Validate();" runat="server" Text="Create Profile" />
                        <asp:Button ID="btnUpdate" CssClass="btn btn-primary profile-button" OnClick="btnUpdate_Click" ValidationGroup="Val" Visible="false" runat="server" Text="Update" />
                        <asp:Button ID="btnDelete" CssClass="btn btn-primary profile-button" OnClick="btnDelete_Click" Visible="false" runat="server" Text="Delete" />
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
            <div class="col-12 col-sm-12 col-md-3 col-lg-3 d-none">
                <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                    <asp:Image ID="imgAttachmentCover" CssClass="rounded-circle mt-5" Width="150" Height="150" ImageUrl="~/Assets/img/background-default.png" runat="server" /><br />
                    <h5 class="font-weight-bold">Cover Picture</h5>
                    <br />
                    <input type="button" class="btn btn-primary" onclick="ClickUploaderCover();" id="changeImageCover" value="Change Image" />
                    <br />
                    <div id="divAttachmentCover" runat="server">
                        <input type="button" class="btn btn-primary" onclick="ClickUploaderCover();" id="UploadClickCover" value="Upload Image" />
                        <asp:FileUpload ID="fuImageCover" CssClass="d-none" onchange="UploadFileCover(this);" AllowMultiple="false" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function Validate() {
            var P = document.getElementById("<%= txtFullPassword.ClientID%>");
            var CP = document.getElementById("<%= txtConfirmPassword.ClientID%>");
            var LG = document.getElementById("<%= txtFullLoginName.ClientID%>");

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
        window.onload = Check;

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

</asp:Content>
