<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CreateProduct.aspx.cs" Inherits="BN_HomeFinance.CreateProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Assets/js/Scripts/ckeditor/ckeditor.js"></script>
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
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="HDImages" runat="server"></asp:HiddenField>

        <div class="container rounded bg-white">
            <div class="row">
                <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
                <div class="col-12 col-sm-12 col-md-10 col-lg-10 border-right">
                    <div class="p-3 py-5 mb-3 row">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="text-right" id="header" runat="server">Create Product</h3>
                        </div>
                         <div class="col-12 col-sm-12 col-md-12 col-lg-12" runat="server" id="divUser" visible="false">
                            <asp:Label ID="Label1" runat="server">Vendor<span style="color:red">*</span></asp:Label>
                            <asp:DropDownList CssClass="form-select" ID="ddlUser" runat="server" DataTextField="FullName" DataValueField="UserID"></asp:DropDownList>
                            <asp:RequiredFieldValidator ValidationGroup="Val" runat="server" InitialValue="0" ControlToValidate="ddlUser"
                                Display="Dynamic" ErrorMessage="Required Field" ForeColor="Red" />
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblProductName" runat="server">Product Name<span style="color:red">*</span></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtProductName" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ValidationGroup="Val" runat="server" ControlToValidate="txtProductName"
                                Display="Dynamic" ErrorMessage="Required Field" ForeColor="Red" />--%>
                            <span id="RFQProduct" style="color: red; display: none">Required Field</span>
                        </div>
                        <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                            <asp:Label ID="lblCategory" runat="server">Category<span style="color:red">*</span></asp:Label>
                            <asp:DropDownList CssClass="form-select" ID="ddlCategory" runat="server"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ValidationGroup="Val" runat="server" InitialValue="-1" ControlToValidate="ddlCategory"
                                Display="Dynamic" ErrorMessage="Required Field" ForeColor="Red" />--%>
                            <span id="RFQCategory" style="color: red; display: none">Required Field</span>
                        </div>
                        <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                            <asp:Label ID="lblPrice" runat="server">Price<span style="color:red">*</span></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtPrice" onchange="format(this);" TextMode="Number" step="any" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ValidationGroup="Val" runat="server" ControlToValidate="txtPrice"
                                Display="Dynamic" ErrorMessage="Required Field" ForeColor="Red" />--%>
                            <span id="RFQPrice" style="color: red; display: none">Required Field</span>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
                            <asp:Label ID="lblQuantity" runat="server" Text="Available Quantity"></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtQuantity" TextMode="Number" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblDescription" runat="server">Product Description <span style="color:red">*</span></asp:Label>
                            <textarea id="txtDescription" class="form-control ckeditor" runat="server" style="width: 100%; height: 134px" rows="4"></textarea>
                            <asp:RequiredFieldValidator ValidationGroup="Val" runat="server" ControlToValidate="txtDescription"
                                Display="Dynamic" ErrorMessage="Required Field" ForeColor="Red" />
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
                            <asp:Label ID="lblDelivery" runat="server" Text="Dileverable?"></asp:Label>
                            <asp:DropDownList CssClass="form-control" ID="ddlDelivery" onchange="DeliveryToggle(this);" runat="server">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
                            <asp:Label ID="lblDeliveryTime" runat="server" Text="Delivery Time"></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtDeliveryTime" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
                            <asp:Label ID="lblDeliveryCharges" runat="server" Text="Delivery Charges"></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtDeliveryCharges" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 d-none">
                            <asp:Label ID="lblIsAvailable" runat="server" Text="Availability"></asp:Label>
                            <asp:DropDownList CssClass="form-control" ID="ddlIsAvailable" runat="server">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" id="divUploader" runat="server">
                            <asp:Label ID="Images" runat="server" Text="Upload Images"></asp:Label>
                            <div class="t-c w-100">
                                <img src="/Assets/img/background-default.png" height="150" width="150" class="rounded-circle" />
                                <br />
                                <input type="button" onclick="ClickUploader()" value="Upload Images" class="btn btn-primary" />
                            </div>
                            <br /><label class="image-size mt-3 w-100 t-c">Recommended Image Size 550 x 490</label>
                            <asp:FileUpload ID="FUImages" accept="image/*" CssClass="d-none" AllowMultiple="true" onchange="ShowImages(event)" runat="server" />
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="row" id="divImages">
                                <%= HTMLImages %>
                            </div>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-4">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" OnClientClick="checkValidation()" ValidationGroup="Val" OnClick="btnSubmit_Click" runat="server" Text="Submit" />
                            <asp:Button ID="btnUpdate" CssClass="btn btn-primary" OnClientClick="checkValidation()" ValidationGroup="Val" OnClick="btnUpdate_Click" Visible="false" runat="server" Text="Update" />
                            <asp:Button ID="btnDelete" CssClass="btn btn-secondary" OnClick="btnDelete_Click" Visible="false" runat="server" Text="Delete" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" runat="server" Text="Cancel" />
                        </div>
                    </div>
                </div>
                <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var images = [];

        function upload() {
             
            for (var i = 0; i < images.length; i++) {
                var reader = new FileReader();
                reader.onload = function () {
                    PageMethods.UploadProductImages(reader.result, OnSuccess);
                }
                reader.readAsDataURL(images[i]);
            }
        }

        function ClickUploader() {
             
            $("#<%= FUImages.ClientID%>").click();
        }

        function ShowImages(event) {
             
            var div = $("#divImages");

            for (var i = 0; i < event.target.files.length; i++) {
                images.push(event.target.files[i]);
            }

            document.getElementById("divImages").innerHTML = "";

            for (var i = 0; i < images.length; i++) {
                $($.parseHTML("<div class='col-4 col-sm-3 col-md-3 col-lg-2 mt-2 t-c'><span style='display:none'>" + i + "</span><img class='prop-img w-100' src= " + URL.createObjectURL(images[i]) + "><a href='javascript:void(0)' onclick='RemoveFromArray(this)'>Remove</a></div>")).appendTo(div);
            }
        }

        function RemoveFromArray(Element) {
             
            var index = parseInt(Element.parentElement.getElementsByTagName("span")[0].innerText);

            images.splice(index, 1);

            Element.parentElement.parentElement.innerHTML = "";

            var div = $("#divImages");

            for (var i = 0; i < images.length; i++) {
                $($.parseHTML("<div class='col-4 col-sm-3 col-md-3 col-lg-2 mt-2 t-c'><span style='display:none'>" + i + "</span><img class='prop-img w-100' src= " + URL.createObjectURL(images[i]) + "><a href='javascript:void(0)' onclick='RemoveFromArray(this)'>Remove</a></div>")).appendTo(div);
            }
        }

        function checkValidation() {
             
            var error = 0;
            var product = $("#<%=txtProductName.ClientID%>");
            var category = $("#<%=ddlCategory.ClientID%>");
            var value = $("#<%=txtPrice.ClientID%>");

            document.getElementById("RFQProduct").style.display = "none";
            document.getElementById("RFQCategory").style.display = "none";
            document.getElementById("RFQPrice").style.display = "none";

            if (product.val() == "") {
                error++;
                product.focus();
                document.getElementById("RFQProduct").style.display = "block";
            }
            else if (category.val() == "0") {
                error++;
                category.focus();
                document.getElementById("RFQCategory").style.display = "block";
            }
            else if (value.val() == "") {
                error++;
                value.focus();
                document.getElementById("RFQPrice").style.display = "block";
            }

            if (error > 0) {
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>
