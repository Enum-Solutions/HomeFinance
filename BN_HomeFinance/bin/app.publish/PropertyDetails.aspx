<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PropertyDetails.aspx.cs" Inherits="BN_HomeFinance.PropertyDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type="date"]::before {
            color: #999999;
            content: attr(placeholder);
        }

        input[type="date"] {
            color: #ffffff;
        }

            input[type="date"]:focus, input[type="date"]:valid {
                color: #666666;
            }

                input[type="date"]:focus::before, input[type="date"]:valid::before {
                    content: "" !important;
                }

        a {
            text-decoration: none;
        }

        body {
            font-family: Arial;
            margin: 0;
        }

        * {
            box-sizing: border-box;
        }

        img {
            vertical-align: middle;
        }

        .slideshowcontainer {
            position: relative;
        }

        .mySlides {
            display: none;
        }

        .cursor {
            cursor: pointer;
        }

        .prev,
        .next {
            cursor: pointer;
            position: absolute;
            top: 40%;
            width: auto;
            padding: 16px;
            margin-top: -50px;
            color: white;
            font-weight: bold;
            font-size: 20px;
            border-radius: 0 3px 3px 0;
            user-select: none;
            -webkit-user-select: none;
        }

        .next {
            right: 0;
            border-radius: 3px 0 0 3px;
        }

            .prev:hover,
            .next:hover {
                background-color: rgba(0, 0, 0, 0.8);
            }

        .numbertext {
            color: #f2f2f2;
            font-size: 12px;
            padding: 8px 12px;
            position: absolute;
            top: 0;
        }

        .caption-container {
            text-align: center;
            background-color: #222;
            padding: 2px 16px;
            color: white;
        }

        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        .column {
            float: left;
            width: 16.66%;
        }

        .demo {
            opacity: 0.6;
        }

            .active,
            .demo:hover {
                opacity: 1;
            }

        input[type=text], select, textarea {
            width: 100%;
            padding-left: 10px;
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 6px;
            box-sizing: border-box;
            margin-top: 6px;
            margin-bottom: 16px;
            resize: vertical;
        }

        #btnSendEmail, input[type=submit] {
            background-color: #6E288C !important;
            color: white;
            padding: 12px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            padding: 15px 25px 15px 25px;
            border-radius: 25px !important;
            letter-spacing: 1px !important;
        }

        #btnSendEmailinput, [type=submit]:hover {
            background-color: #8A53A4 !important;
        }

        #btnSendEmail, input[type=button] {
            background-color: #6E288C !important;
            color: white;
            padding: 12px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            padding: 15px 25px 15px 25px;
            border-radius: 25px !important;
            letter-spacing: 1px !important;
        }

        #btnSendEmailinput, [type=button]:hover {
            background-color: #8A53A4 !important;
        }

        .map {
            border: 1px solid #888888;
            border-radius: 15px;
            max-height: 475px;
            height: 475px;
            width: 100%;
        }

        .body-headings {
            font-size: 28px;
        }

        .page-headings {
            color: #6E298B;
            font-size: 38px;
            margin-top:;
        }

        .heading-desc {
            color: #6A6B6B;
            font-size: 17px;
            margin-top: 7px;
        }

        .formContainer {
            border: 1px solid #888888;
            padding: 10px;
            box-shadow: 0px 2px 18px 5px #888888;
            border-radius: 15px;
            padding-left: 40px;
            padding-right: 40px;
            padding-top: 35px;
        }

        .user-info-section {
            margin-bottom: 40px;
        }

        .logo-section {
            border-radius: 6px;
            width: 22.33333%
        }

        .desc-section {
            line-height: 28px;
            margin-left: -5px;
        }

        .grey-title-space {
            background-color: #F4EFEF;
            padding-left: 2.3%;
            padding-top: 5px;
        }

        .quick-summary-elements {
            float: right;
            margin: 0;
        }

        .new-section {
            padding: 10px 0 10px 0;
        }

        .innerContainer {
            padding: 2% 2.3% 0 2.3%;
        }

        .slideshowcontainer {
            padding-left: 0px;
            padding-right: 0px;
            width: 100% !Important;
        }

        #next {
            color: white;
            cursor: pointer;
        }

        #prev {
            color: white;
            cursor: pointer;
        }

        .gallery-trigger-container {
            padding-top: 3%;
        }

        .input-group-text {
            height: 36px !important;
            border-radius: 0 !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
    <div class="container">
        <%= HTML%>
        <div class="row new-section">
            <div class="col-12 col-sm-12 col-md-12 col-lg-6 col-xl-6">
                <h4 class="body-headings">Location</h4>
                <iframe id="geo" runat="server" loading="lazy" class="map"></iframe>
            </div>
            <div class="ol-12 col-sm-12 col-md-12 col-lg-6 col-xl-6">
                <h4 class="body-headings">Contact Now</h4>
                <div class="formContainer">
                    <div class="row user-info-section">
                        <div class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6 logo-section">
                            <asp:Image ID="userLogo" runat="server" Height="85px" Width="90px" class="rounded-circle" />
                        </div>

                        <div class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6 desc-section">
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                            <br />
                            <img src="Assets/img/pinlocation.png" alt="Alternate Text" height="20" width="20" /><asp:Label ID="lblAddress" runat="server"></asp:Label><br />
                            <img src="Assets/img/whatsappPNG.png" alt="Alternate Text" height="20" width="20" /><asp:Label ID="lblPhone" runat="server"></asp:Label>
                        </div>
                    </div>
                    <asp:TextBox ID="txtName" runat="server" placeholder="&nbsp;&nbsp;Name"></asp:TextBox>

                    <asp:TextBox ID="txtEmail" runat="server" placeholder="&nbsp;&nbsp;Email"></asp:TextBox>

                    <textarea id="TextAreaComments" cols="20" rows="3" runat="server" placeholder="Comments"></textarea>
                    <br />
                    <asp:Button ID="btnSendEmail" runat="server" Text="Send Message" OnClick="btnSendEmail_Click" />
                    <br />
                    <br />
                </div>
            </div>
        </div>

        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body bg-gray">
                        <div class="row">
                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 pad-2-7">
                                <div class="row shadow bg-white br p-0-5 c-w fs-10 calc-main">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-4">
                                        <h5 class="w-100 c-g">
                                            <img src="Assets/img/calculator (1).png" />
                                            Calculator Installment</h5>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-2 c-g">
                                        Category
                        <asp:DropDownList ID="ddlPropertyType" CssClass="form-select" runat="server" DataTextField="PropertyType" DataValueField="PropertyTypeID">
                        </asp:DropDownList>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-2 c-g date-picker-custom">
                                        Date of Birth
                                        <div class="input-group">
                                            <input id="dob" class="form-control datepicker mt-0" readonly placeholder="Date of Birth" type="text">
                                            <div class="input-group-append">
                                                <span class="input-group-text bg-yellow text-white b-0"><i class="fa fa-calendar-alt" id="Cal"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-2 c-g">
                                        Total Value
                        <asp:TextBox ID="txtTotalValue" TextMode="Number" CssClass="form-control" runat="server" placeholder="Total Value"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-2 c-g">
                                        Financing Amount
                        <asp:TextBox ID="txtFinancingAmount" runat="server" TextMode="Number" CssClass="form-control" placeholder="Financing Amount"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-2 c-g">
                                        Tenure
                                        <asp:DropDownList ID="ddlTenure" CssClass="form-select" runat="server" DataTextField="Tenure" DataValueField="ID">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-2 c-g">
                                        Down Payment
                            <input type="text" class="form-control" id="downpayment" disabled>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-2 c-g">
                                        Rate
                            <input type="text" class="form-control" id="rate" disabled>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-2 t-c">
                                        <input type="button" value="Calculate" class="btn btn-calculate c-w bg-yellow" onclick="Calculate();" />
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-2 mb-3 t-c c-g fs-7">
                                        This Calculator is Tentative and Just for Information.<br />
                                        Taxes , Registration Charges etc. to be paid by Customers.
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col col-12 col-sm-12 col-md-12 col-lg-12" id="Calc-Grid" style="display: none">
                                        <table class="table-responsive w-100 mt-2 table-Calc">
                                            <tr>
                                                <td class="bg-purple c-w">Installment/Month</td>
                                                <td class="bg-purple c-w">Total Profit</td>
                                            </tr>
                                            <tr>
                                                <td class="b-1"><span id="sp_Installment"></span></td>
                                                <td class="b-1"><span id="sp_Profit"></span></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="Scripts/bootstrap-datepicker.min.js"></script>
    <link rel="stylesheet" href="Content/bootstrap-datepicker.css" />
    <script>
        $(document).ready(function () {
            var tenure = document.getElementById("<%=ddlTenure.ClientID%>");
            tenure.options[0].style.display = "none";

            $('#dob').datepicker({endDate: new Date()});
        });

        let Locale = Intl.NumberFormat('en-IN');

        function Calculate() {
            var type = document.getElementById("<%=ddlPropertyType.ClientID%>").value;
            var dob = document.getElementById("dob").value;
            var value = document.getElementById("<%=txtTotalValue.ClientID%>").value;
            var financingamount = document.getElementById("<%=txtFinancingAmount.ClientID%>").value;
            var tenure = document.getElementById("<%=ddlTenure.ClientID%>").value;
            var downpayment = document.getElementById("downpayment");

            var D = ((new Date()).getFullYear() - (new Date(dob)).getFullYear());

            if (type != '0') {
                if (dob != '') {
                    if ((parseInt(D) + parseInt(tenure)) < 60) {
                        if (tenure != '0') {
                            if (value != '') {
                                if (financingamount != '') {
                                    if (parseInt(financingamount) <= (parseInt(value) * 0.75)) {
                                        debugger;
                                        downpayment.value = parseInt(value) - parseInt(financingamount);
                                        PageMethods.GetInterestRate(type, tenure, OnSuccessRate);
                                    }
                                    else {
                                        alert('Financing Amount should be equal or less than the 75% of Total Value');
                                    }
                                }
                                else {
                                    alert('Please Enter Financing Amount');
                                }
                            }
                            else {
                                alert('Please Enter Total Value');
                            }
                        }
                        else {
                            alert('Please Select Tenure');
                        }
                    }
                    else {
                        alert('You are not eligible for the Loan Property as your total span will exceed 60 years of age.');
                    }
                }
                else {
                    alert('Please Select Date of Birth');
                }
            }
            else {
                alert('Please Select Type');
            }
        }
        function OnSuccessRate(response, userContext, methodName) {
            debugger;
            var value = document.getElementById("<%=txtTotalValue.ClientID%>").value;
            var downpayment = document.getElementById("downpayment").value;
            var tenure = document.getElementById("<%=ddlTenure.ClientID%>").value;
            var spinstallment = document.getElementById("sp_Installment");
            var spprofit = document.getElementById("sp_Profit");
            var grid = document.getElementById("Calc-Grid");

            if (response != '' && response != null && response != 0) {
                document.getElementById("rate").value = response + "%";
                var tot = parseInt(value) + ((parseInt(value) / 100) * parseInt(response));
                spinstallment.innerText = Locale.format(Math.round((parseInt(tot) - parseInt(downpayment)) / (parseInt(tenure) * 12))) + " OMR";
                spprofit.innerText = Locale.format(Math.round((parseInt(value) / 100) * parseInt(response))) + " OMR";
                grid.style.display = "block";
            }
            else {
                grid.style.display = "none";
                alert('Rate for the spcified terms could not be found.');
            }
        }

    </script>

    <script src="Assets/js/sliderjs.js"></script>
</asp:Content>
