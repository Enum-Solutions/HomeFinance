<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CreateProperty.aspx.cs" Inherits="BN_HomeFinance.CreateProperty" %>

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

        .amenities {
            padding-left: 2%;
            border: solid 1px var(--gray);
            height: 200px;
        }
    </style>

    <script src="Assets/js/Scripts/ckeditor/ckeditor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
        <div class="container rounded bg-white">
            <div class="row">
                <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
                <div class="col-12 col-sm-12 col-md-10 col-lg-10 border-right">
                    <div class="row p-3 py-5 mb-3">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="text-right">Property</h3>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" runat="server" id="divUsers" visible="false">
                            <asp:Label ID="lblBuilder" runat="server">Select Builder<span style="color:red">*</span></asp:Label>
                            <asp:DropDownList CssClass="form-select" ID="ddlUser" runat="server" DataTextField="FullName" DataValueField="UserID"></asp:DropDownList>
                            <asp:RequiredFieldValidator ValidationGroup="validateControls" InitialValue="0" runat="server" ControlToValidate="ddlUser"
                                Display="Dynamic" ErrorMessage="Required Field" ForeColor="Red" />
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblPropertyName" runat="server">Property Name<span style="color:red">*</span></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtPropertyName" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ValidationGroup="validateControls" runat="server" ControlToValidate="txtPropertyName"
                                Display="Dynamic" ErrorMessage="Required Field" ForeColor="Red" />--%>
                            <span id="RFQPropertyName" style="color: red; display: none">Required Field</span>
                        </div>
                        <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                            <asp:Label ID="lblPropertyType" runat="server">Property Type<span style="color:red">*</span></asp:Label>
                            <asp:DropDownList CssClass="form-select" ID="ddlPropertyType" runat="server" DataTextField="PropertyType" DataValueField="PropertyTypeID">
                            </asp:DropDownList>
                            <span id="RFQPropertyType" style="color: red; display: none">Required Field</span>
                            <%--<asp:RequiredFieldValidator ValidationGroup="validateControls" runat="server" ControlToValidate="ddlPropertyType"
                                Display="Dynamic" ErrorMessage="Required Field" ForeColor="Red" InitialValue="0" />--%>
                        </div>
                        <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                            <asp:Label ID="lblPropertyValue" runat="server">Property Value<span style="color:red">*</span></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtPropertyValue" runat="server" Style="margin-bottom: 0px" TextMode="Number"></asp:TextBox>
                            <span id="RFQPropertyValue" style="color: red; display: none">Required Field</span>
                            <%--<asp:RequiredFieldValidator ValidationGroup="validateControls" runat="server" ControlToValidate="txtPropertyValue"
                                Display="Dynamic" ErrorMessage="Required Field" ForeColor="Red" />--%>
                            <%--                            <asp:RegularExpressionValidator ValidationGroup="validateControls"
                                ControlToValidate="txtPropertyValue" runat="server"  ForeColor="Red" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+">
                                </asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                            <asp:Label ID="lblGovernorate" runat="server">Governorate<span style="color:red">*</span></asp:Label>
                            <asp:DropDownList CssClass="form-select" ID="ddlGovernorate" runat="server" onchange="PopulateWilayats(this)"
                                DataTextField="Governorate" DataValueField="GovernorateID">
                            </asp:DropDownList>
                            <span id="RFQGovernorate" style="color: red; display: none">Required Field</span>
                            <%--<asp:RequiredFieldValidator ValidationGroup="validateControls" runat="server" ControlToValidate="ddlGovernorate"
                                Display="Dynamic" ErrorMessage="Required Field" ForeColor="Red" InitialValue="0" />--%>
                        </div>
                        <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                            <asp:Label ID="lblWilayat" runat="server">Wilayat<span style="color:red">*</span></asp:Label>
                            <asp:DropDownList CssClass="form-select" ID="ddlWilayat" runat="server" DataTextField="Wilayat" DataValueField="WilayatID">
                            </asp:DropDownList>
                            <span id="RFQWilayat" style="color: red; display: none">Required Field</span>
                            <%--<asp:RequiredFieldValidator ValidationGroup="validateControls" runat="server" ControlToValidate="ddlWilayat" Display="Dynamic"
                                ErrorMessage="Required Field" ForeColor="Red" InitialValue="0" />--%>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblPropertyAddress" runat="server">Complete Address<span style="color:red">*</span></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtPropertyAddress" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ValidationGroup="validateControls" Display="Dynamic" runat="server" ControlToValidate="txtPropertyAddress"
                                ErrorMessage="Required Field" ForeColor="Red" />--%>
                            <span id="RFQAddress" style="color: red; display: none">Required Field</span>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblPropertyDesc" runat="server" Text="Property Description"></asp:Label>
                            <textarea id="txtPropertyDesc" name="PropDesc" class="form-control ckeditor" rows="3" runat="server"></textarea><br />
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblGeoLocation" runat="server" Text="Geo Location"></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtGeoLocation" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                            <asp:Label ID="lblBedroom" runat="server" Text="Bedrooms"></asp:Label>
                            <asp:TextBox CssClass="form-control" TextMode="Number" ID="txtBedroom" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ControlToValidate="txtBedroom" runat="server"
                                ErrorMessage="Only Numbers allowed" ValidationExpression="\d+" ForeColor="Red"> </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                            <asp:Label ID="lblBathroom" runat="server" Text="Bathroom"></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtBathroom" TextMode="Number" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ControlToValidate="txtBathroom" runat="server"
                                ErrorMessage="Only Numbers allowed" ValidationExpression="\d+" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                            <asp:Label ID="lblGarage" runat="server" Text="Garage"></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtGarage" TextMode="Number" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ControlToValidate="txtGarage" runat="server"
                                ErrorMessage="Only Numbers allowed" ValidationExpression="\d+" ForeColor="Red"> </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                            <asp:Label ID="lblArea" runat="server" Text="Area in Meter/Sq."></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtArea" TextMode="Number" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" ControlToValidate="txtArea" runat="server"
                                ErrorMessage="Only Numbers allowed" ValidationExpression="\d+" ForeColor="Red"> </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12" id="divUploader" runat="server">
                            <asp:Label ID="lblPropertyImages" runat="server" Text="Upload Images"></asp:Label>
                            <div class="t-c w-100">
                                <img src="/Assets/img/background-default.png" height="150" width="150" class="rounded-circle" />
                                <br />
                                <input type="button" onclick="ClickUploader()" value="Upload Images" class="btn btn-primary" />
                                <br />
                                <label class="image-size mt-3">Recommended Image Size 1116 x 500</label>
                            </div>
                            <asp:FileUpload ID="fileUploadPropertyImages" accept="image/*" onchange="ShowImages(event)" runat="server" CssClass="d-none" multiple="multiple" />
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="row" id="divImages">
                                <%= HTMLImages %>
                            </div>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-4">
                            <asp:Label ID="lblAmenities" runat="server" Text="Amenities"></asp:Label>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 overflow-auto amenities br-2">
                            <asp:CheckBoxList ID="chkboxAmenities" CssClass="chk-amenities" runat="server" DataTextField="Amenity" RepeatDirection="vertical" DataValueField="AmenityID"></asp:CheckBoxList>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-2">
                            <asp:Button CssClass="btn btn-primary" OnClientClick="return checkValidation()" ID="AddPropertyBtn" ValidationGroup="validateControls" runat="server" Text="Create Property" OnClick="AddPropertyBtn_Click" />
                            <asp:Button CssClass="btn btn-primary" OnClientClick="return checkValidation()" ID="btnUpdateProperty" ValidationGroup="validateControls" runat="server" Text="Update Property" OnClick="btnUpdateProperty_Click" />
                            <asp:Button CssClass="btn btn-secondary" ID="btnDeleteProperty" runat="server" Text="Delete Property" OnClick="btnDeleteProperty_Click" />
                            <asp:Button CssClass="btn btn-secondary" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
                <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
                <asp:HiddenField ID="HDImages" runat="server" />
            </div>
        </div>
    </div>
    <%--<input type="button" value="Uplaod" onclick="uploaddd()" />--%>
    <script type="text/javascript">
        var images = [];

        $(document).ready(function () {
            var dd = document.getElementById("<%= ddlGovernorate.ClientID%>");
            dd.options[0].style.display = "none";
        })

        function ClickUploader() {
            
            $("#<%= fileUploadPropertyImages.ClientID%>").click();
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

        function PopulateWilayats(element) {
           
            
            var value = element.options[element.selectedIndex].value;
            PageMethods.GetWilayats(value, OnSuccess);
        }
        
        function OnSuccess(response, userContext, methodName) {
            
            var json = JSON.parse(response);
            /*var firstWilayat = json.Wilayat[0].Wilayat;*/
            $("#<%=ddlWilayat.ClientID%> option:selected").text("Choose Wilayat");
            if (json.Wilayat.length > 0) {
                var wilayats = document.getElementById("<%= ddlWilayat.ClientID%>");
                wilayats.disabled = false;
                for (var i = 0; i < wilayats.options.length; i++) {
                    for (var j = 0; j < json.Wilayat.length; j++) {
                        if (json.Wilayat[j].WilayatID == parseInt(wilayats.options[i].value)) {
                            wilayats.options[i].style.display = "block";
                            break;
                        }
                        else if (j == (json.Wilayat.length - 1)) {
                            wilayats.options[i].style.display = "none";
                        }
                    }
                }
            }
        }

        function checkValidation() {
            
            var error = 0;
            var property = $("#<%=txtPropertyName.ClientID%>");
            var type = $("#<%=ddlPropertyType.ClientID%>");
            var value = $("#<%=txtPropertyValue.ClientID%>");
            var governorate = $("#<%=ddlGovernorate.ClientID%>");
            var wilayat = $("#<%=ddlWilayat.ClientID%>");
            var address = $("#<%=txtPropertyAddress.ClientID%>");
            document.getElementById("RFQPropertyName").style.display = "none";
            document.getElementById("RFQPropertyType").style.display = "none";
            document.getElementById("RFQPropertyValue").style.display = "none";
            document.getElementById("RFQGovernorate").style.display = "none";
            document.getElementById("RFQWilayat").style.display = "none";
            document.getElementById("RFQAddress").style.display = "none";

            if (property.val() == "") {
                error++;
                property.focus();
                document.getElementById("RFQPropertyName").style.display = "block";
            }
            else if (type.val() == "0") {
                error++;
                type.focus();
                document.getElementById("RFQPropertyType").style.display = "block";
            }
            else if (value.val() == "") {
                error++;
                value.focus();
                document.getElementById("RFQPropertyValue").style.display = "block";
            }
            else if (governorate.val() == "0") {
                error++;
                governorate.focus();
                document.getElementById("RFQGovernorate").style.display = "block";
            }
            else if (wilayat.val() == "0") {
                error++;
                wilayat.focus();
                document.getElementById("RFQWilayat").style.display = "block";
            }
            else if (address.val() == "") {
                error++;
                address.focus();
                document.getElementById("RFQAddress").style.display = "block";
            }
            if (error > 0) {
                return false;
            }
            else {
                upload();
                return true;
            }
        }

        function upload(ID) {
            
            for (var i = 0; i < images.length; i++) {
                var reader = new FileReader();
                reader.onload = function () {
                    PageMethods.UploadPropertyImages(reader.result, ID, OnSuccess);
                }
                reader.readAsDataURL(images[i]);
            }
            <%--document.getElementById("<%= HDImages.ClientID%>") = images;--%>
        }
    </script>
</asp:Content>
