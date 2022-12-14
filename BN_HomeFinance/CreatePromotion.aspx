<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CreatePromotion.aspx.cs" Inherits="BN_HomeFinance.CreatePromotion" %>

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
    <div class="container">
        <div class="container rounded bg-white">
            <div class="row">
                <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
                <div class="col-12 col-sm-12 col-md-10 col-lg-10 border-right">
                    <div class="p-3 py-5 mb-3">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="text-right">Promotions</h3>
                        </div>
                        <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="btnUpload_Click" Style="display: none" />
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c">
                            <div class="carousel-item active">
                                <asp:Image ID="imgAttachment" Height="200" CssClass="d-block w-100" runat="server" />
                            </div>
                            <input type="button" class="btn btn-primary mt-2" onclick="ClickUploader();" id="changeImage" value="Change Image" />
                            <div id="divAttachment" class="w-100 t-c" runat="server">
                                <asp:Image ID="imgUploader" onclick="ClickUploader();" ImageUrl="~/Assets/img/background-default.png" Width="150" Height="150" runat="server" />
                                <br />
                                <input type="button" class="btn btn-primary" onclick="ClickUploader();" id="UploadClick" value="Upload Image" />
                                <asp:FileUpload ID="FUPromotionImage"  accept="image/*" CssClass="d-none" onchange="UploadFile(this);" AllowMultiple="false" runat="server" />
                            </div>
                                <br /><label class="image-size mt-3 w-100 t-c">Recommended Image Size 1000 x 350</label>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblHeader" runat="server" Text="Promotion Name"></asp:Label>
                            <label class="mandatory">*</label>
                            <asp:TextBox CssClass="form-control" ID="txtHeader" placeholder="Promotion Name" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFQHeader" ControlToValidate="txtHeader" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
                            <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                            <label class="mandatory">*</label>
                            <textarea id="txtDescription" class="form-control" placeholder="Promotion Description" runat="server" rows="4"></textarea>
                            <%--<asp:RequiredFieldValidator ID="RFQDescription" ControlToValidate="txtDescription" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                            <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                <asp:ListItem Text="InActive" Value="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-2">
                            <asp:Button ID="btnSubmit" ValidationGroup="Val" CssClass="btn btn-primary" OnClick="btnSubmit_Click" runat="server" Text="Submit" />
                            <asp:Button ID="btnUpdate" ValidationGroup="Val" CssClass="btn btn-primary" Visible="false" OnClick="btnUpdate_Click" runat="server" Text="Update" />
                            <asp:Button ID="btnDelete" CssClass="btn btn-secondary" Visible="false" OnClick="btnDelete_Click" runat="server" Text="Delete" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" runat="server" Text="Cancel" />
                        </div>
                    </div>
                </div>
                <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUpload.ClientID %>").click();
            }
        }

        window.onload = Check;

        function Check() {
            var btn = document.getElementById("<%= divAttachment.ClientID%>");

            if (btn.style.display != "none") {
                document.getElementById("changeImage").style.display = "none";
            }
        }

        function ClickUploader() {
            debugger;
            $("#<%= FUPromotionImage.ClientID%>").click();
        }
    </script>
</asp:Content>
