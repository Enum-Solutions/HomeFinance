<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="BN_HomeFinance.Index" %>

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
            text-decoration:none !important;
        }
    </style>
    <script type="text/javascript" src="Scripts/bootstrap-datepicker.min.js"></script>
    <link rel="stylesheet" href="Content/bootstrap-datepicker.css" />
    <%--<link rel="stylesheet" href="Content/bootstrap.min.css" />--%>
    <script>
        $(document).ready(function () {
            $('#dob').datepicker({ endDate: new Date() });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="HDCurrentUser" runat="server" />
    <asp:HiddenField ID="HDFullName" runat="server" />
    <asp:HiddenField ID="HDUrl" runat="server" />

    <asp:HiddenField ID="HDChatID" runat="server" />

    <asp:HiddenField ID="HDPropValMax" runat="server" />
    <asp:HiddenField ID="HDProdValMax" runat="server" />
    <asp:HiddenField ID="HDPropAreaMax" runat="server" />

    <asp:HiddenField ID="HDAreaMin" runat="server" />
    <asp:HiddenField ID="HDAreaMax" runat="server" />
    <asp:HiddenField ID="HDPropertyMin" runat="server" />
    <asp:HiddenField ID="HDPropertyMax" runat="server" />
    <asp:HiddenField ID="HDProductMin" runat="server" />
    <asp:HiddenField ID="HDProductMax" runat="server" />

    <!--  Section Banner -->
    <section id="sec-Banner">
        <div class="row">
            <div id="carouselExampleDark" class="carousel carousel-dark slide" data-bs-ride="carousel">
                <div class="carousel-indicators">
                    <%= HtmlCarouselIndicators %>
                </div>
                <div class="carousel-inner">
                    <%= HtmlCaroseul %>
                </div>
                <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Previous</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Next</span>
                </button>
            </div>
        </div>
    </section>
    <!--  Section Banner  -->

    <!--  Section Messages  -->
    <section id="sec-Messages" class="section-8">
        <div class="row mt-5">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 section-2">
                <div class="row">
                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                        <h2>
                            <img src="Assets/img/anouncements_icon.png" style="height: 46px; width: 46px" />&nbsp; Announcements</h2>
                    </div>
                </div>
            </div>
        </div>
        <div class="row t-c">
            <%= HtmlMessages %>
        </div>
    </section>
    <!--  Section Messages  -->

    <!-- Calculator & Search Start -->
    <section id="sec-Calc-Sear" class="section mt-4">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <h1 class="c-g">What is <strong class="c-p">Manzil?</strong></h1>
            </div>
            <div class="col-12 col-sm-12 col-md-7 col-lg-8 search-panel">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <p>
                            Bank Nizwa is Oman’s first dedicated Islamic bank, with fully Shari’a compliant products and services.
                        </p>
                        <p>
                            The Bank offers an entire portfolio of commercial banking services, in accordance with the 
                            license issued by the Central Bank of Oman (CBO) and the Banking Law promulgated by the 
                            Royal Decree No. 114/2000.
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-4 search-inner">
                        <ul class="nav nav-tabs" id="Search-Tab" role="tablist">
                            <li class="nav-item" role="presentation">
                                <button class="nav-link active" style="border-radius: 100px" id="profile-tab" data-bs-toggle="tab" data-bs-target="#sProperty" type="button" role="tab" aria-controls="sProperty" aria-selected="false">
                                    <img src="Assets/img/search_property.png" />&nbsp;&nbsp;Search Property</button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="contact-tab" style="border-radius: 100px" data-bs-toggle="tab" data-bs-target="#sProduct" type="button" role="tab" aria-controls="sProduct" aria-selected="false">
                                    <img src="Assets/img/search_product.png" />&nbsp;&nbsp;Search Product</button>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div id="sProperty" class="tab-pane fade show active" role="tabpanel" aria-labelledby="home-tab">
                                <div class="row">
                                    <div class="col-12 col-sm-12 col-md-6 col-lg-6 mt-2 s-pad-left">
                                        <asp:Label ID="lblGovernorate" runat="server">Governorate</asp:Label>
                                        <asp:DropDownList ID="ddlGovernorate" runat="server" CssClass="form-select mt-3 c-g"
                                            DataTextField="Governorate" DataValueField="GovernorateID" onchange="PopulateWilayats(this)">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFQGovernorate" ControlToValidate="ddlGovernorate" ValidationGroup="ValSearch" InitialValue="0" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-6 col-lg-6 mt-2 s-pad-right">
                                        <asp:Label ID="lblWilayat" runat="server">Wilayat</asp:Label>
                                        <button disabled type="button" class="form-select mt-3 c-g" id="ddMenuWilayat" data-bs-toggle="dropdown" aria-expanded="false">Wilayat</button>
                                        <div class="dropdown-menu" id="wilayatDropdown">
                                            <asp:CheckBoxList ID="LBWilayat" RepeatDirection="vertical" CssClass="chk-amenities allowSelectAll" runat="server" DataTextField="Wilayat" DataValueField="WilayatID">
                                                
                                            </asp:CheckBoxList>
                                        
                                        
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-sm-12 col-md-6 col-lg-6 mt-2 s-pad-left">
                                        <asp:Label ID="lblPropertyType" runat="server">Property Type</asp:Label>
                                        <button type="button" class="form-select mt-3 c-g" id="dropdownMenu22" data-bs-toggle="dropdown" aria-expanded="false">Type</button>
                                        <div class="dropdown-menu">
                                            <asp:CheckBoxList ID="LBPropertyType" CssClass="chk-amenities" RepeatDirection="vertical" runat="server" DataTextField="PropertyType" DataValueField="PropertyTypeID"></asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-6 col-lg-6 mt-2 s-pad-right">
                                        <asp:Label ID="lblBedroom" runat="server" Text="Bedrooms"></asp:Label>
                                        <button type="button" class="form-select mt-3 c-g" id="dropdownMenu212" data-bs-toggle="dropdown" aria-expanded="false">Bedrooms</button>
                                        <div class="dropdown-menu">
                                            <asp:CheckBoxList ID="LBBedroom" RepeatDirection="vertical" CssClass="chk-amenities" runat="server" DataTextField="PropertyType" DataValueField="PropertyTypeID">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>More than 5</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-sm-12 col-md-6 col-lg-6 mt-2 s-pad-left">
                                        <label for="range-1b">Area in Sq. Meters:</label>
                                        <div class="wrapper mt-3">
                                            <div class="values">
                                                <span id="rangearea1">0</span>
                                                <span id="rangearea2" class="f-r">100</span>
                                            </div>
                                            <div class="range-container">
                                                <div class="slider-track-area"></div>
                                                <input type="range" min="0" max="100" value="0" id="silderarea1" oninput="slideOneArea()">
                                                <input type="range" min="0" max="100" value="100" id="silderarea2" oninput="slideTwoArea()">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-6 col-lg-6 mt-2 s-pad-right">
                                        <label for="range-1a">Price Range (OMR):</label>
                                        <div class="wrapper mt-3">
                                            <div class="values">
                                                <span id="rangeProperty1">0</span>
                                                <span id="rangeProperty2" class="f-r">100</span>
                                            </div>
                                            <div class="range-container">
                                                <div class="slider-track-property"></div>
                                                <input type="range" min="0" max="100" value="0" id="silderProperty1" oninput="slideOneProperty()">
                                                <input type="range" min="0" max="100" value="100" id="silderProperty2" oninput="slideTwoProperty()">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-4">
                                        <asp:Button ID="btnSearchProperty" ValidationGroup="ValSearch" CssClass="btn-search" runat="server" Text="Search Property" OnClick="btnSearchProperty_Click" />
                                    </div>
                                </div>
                            </div>
                            <div id="sProduct" class="tab-pane fade" role="tabpanel" aria-labelledby="home-tab">
                                <div class="row">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-4">
                                        <asp:Label ID="lblProductCategory" CssClass="mt-4" runat="server">Product Category</asp:Label>
                                        <button type="button" class="form-select mt-4" id="dropdownMenu234" data-bs-toggle="dropdown" aria-expanded="false">Product Category</button>
                                        <div class="dropdown-menu">
                                            <asp:CheckBoxList ID="LBProductCategory" CssClass="chk-amenities" runat="server"></asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                        <label class="mt-4">Product Price Min/Max</label>
                                        <div class="wrapper mt-4">
                                            <div class="values">
                                                <span id="rangeProduct1">0</span>
                                                <span id="rangeProduct2" class="f-r">100</span>
                                            </div>
                                            <div class="range-container">
                                                <div class="slider-track-product"></div>
                                                <input type="range" min="0" max="100" value="0" id="silderProduct1" oninput="slideOneProduct()">
                                                <input type="range" min="0" max="100" value="100" id="silderProduct2" oninput="slideTwoProduct()">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-4">
                                        <asp:Button ID="btnSearchProduct" CssClass="btn-search" runat="server" Text="Search Product" OnClick="btnSearchProduct_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-5 col-lg-4">
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
                            <input class="form-control datepicker" readonly placeholder="Date of Birth" type="text" id="dob">
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
                        Profit Rate
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
    </section>
    <!-- Calculator & Search End -->

    <!-- Section Hot Properties -->
    <section id="sec-HotProps" class="section-9 mt-4">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 section-2">
                <div class="row">
                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                        <h2>
                            <img src="Assets/img/hot_properties.png" />&nbsp; Hot Properties</h2>
                    </div>
                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                        <a href="<%= BN_HomeFinance.Helper.pg_AllProperties %>" class="c-p f-r see-all shadow">See All</a>
                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <div class="row">
                    <%= HtmlHotProps %>
                </div>
            </div>
        </div>
    </section>
    <!-- Section Hot Properties -->

    <!-- Section Builders-->
    <section id="sec-Builders" class="section">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <div class="row">
                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                        <h2 class="w-100">Partner Builders</h2>
                    </div>
                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                        <asp:HyperLink ID="hpSeeBuilders" CssClass="c-p f-r see-all" runat="server">See All</asp:HyperLink>
                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <div class="row">
                    <div class="col-0 col-sm-0 col-md-1 col-lg-1">
                        <button class="next-prev next-prev-big shadow" id="prev-builder-bg" type="button">
                            <img src="Assets/img/arrow_left_dark.png" />
                        </button>
                    </div>
                    <div class="col-12 col-sm-12 col-md-10 col-lg-10 scroll" id="builder-list">
                        <table class="table-responsive table-partner">
                            <tr>
                                <%= HTMLTopBuilders %>
                            </tr>
                        </table>
                    </div>
                    <div class="col-0 col-sm-0 col-md-1 col-lg-1">
                        <button class="next-prev next-prev-big shadow" style="left: 86%" id="next-builder-bg" type="button">
                            <img src="Assets/img/arrow_right_dark.png" />
                        </button>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-2 d-none" id="next-prev-buttons-builders">
                        <div class="row">
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 t-r">
                                <button class="next-prev next-prev-sm shadow" id="prev-builder-sm" type="button">
                                    <img src="Assets/img/arrow_left_dark.png" />
                                </button>
                            </div>
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 t-l">
                                <button class="next-prev next-prev-sm shadow" id="next-builder-sm" type="button">
                                    <img src="Assets/img/arrow_right_dark.png" />
                                </button>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </section>
    <!-- Section Builders-->

    <!-- Section Vendors -->
    <section id="sec-Vendors" class="section mt-5">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <div class="row">
                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                        <h2 class="w-100">Partner Vendors</h2>
                    </div>
                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                        <asp:HyperLink ID="hpSeeVendors" CssClass="c-p f-r see-all" runat="server">See All</asp:HyperLink>
                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <div class="row mt-4">
                    <div class="col-0 col-sm-0 col-md-1 col-lg-1">
                        <button class="next-prev next-prev-big shadow" id="prev-vendor-bg" type="button">
                            <img src="Assets/img/arrow_left_dark.png" />
                        </button>
                    </div>
                    <div class="col-12 col-sm-12 col-md-10 col-lg-10 scroll" id="vendor-list">
                        <table class="table-responsive table-partner">
                            <tr>
                                <%= HTMLTopVendors %>
                            </tr>
                        </table>
                    </div>
                    <div class="col-0 col-sm-0 col-md-1 col-lg-1">
                        <button class="next-prev next-prev-big shadow" style="left: 86%" id="next-vendor-bg" type="button">
                            <img src="Assets/img/arrow_right_dark.png" />
                        </button>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-2 d-none" id="next-prev-buttons-vendor">
                        <div class="row">
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 t-r">
                                <button class="next-prev next-prev-sm shadow" id="prev-vendor-sm" type="button">
                                    <img src="Assets/img/arrow_left_dark.png" />
                                </button>
                            </div>
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 t-l">
                                <button class="next-prev next-prev-sm shadow" id="next-vendor-sm" type="button">
                                    <img src="Assets/img/arrow_right_dark.png" />
                                </button>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </section>
    <!-- Section Vendors -->

    <!-- Section Chat-->
    <section id="sec-Chat" class="section mt-5">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <div class="row">
                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                        <h2>Ask Your Query</h2>
                    </div>
                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                        <a href="<%= BN_HomeFinance.Helper.pg_ChatList %>" class="c-p f-r see-all">See All</a>
                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 sec-Chat mt-4">
                <div class="row mt-3 mb-4">
                    <div class="col-8 col-sm-9 col-md-10 col-lg-10" style="padding-right: 1%">
                        <textarea class="form-control text-Chat" placeholder="Type here..." rows="2"></textarea>
                    </div>
                    <div class="col-4 col-sm-3 col-md-2 col-lg-2">
                        <button class="btn bg-yellow btn-chat-submit" type="button" onclick="CreateChat(this);">Submit</button>
                    </div>
                </div>
                <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="shadow-lg card">
                        <div class="card-body">
                            <%= HtmlChat  %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- Section Chat-->

    <!-- Modal Login-->
    <div class="modal" id="login" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered modal-dialog-popup" role="document">
            <div class="modal-content modal-content-popup">
                <div class="modal-header bg-purple c-w">
                    <h5 class="modal-title">Bank Nizwa - Login</h5>
                </div>
                <div class="modal-body modal-body-login">
                    <div class="row">
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c">
                            <img src="Assets/img/logo.png" alt="Logo" class="img-fluid-s-login" />
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-4">
                            <label>Username</label>
                            <asp:TextBox ID="txtUserName" placeholder="Username" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFQUserName" ValidationGroup="Val" runat="server" ForeColor="Red" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="* This is Required Field"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-4">
                            <label>Password</label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Password" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFQPassword" ValidationGroup="Val" runat="server" ForeColor="Red" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="* This is Required Field"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-4">
                            <asp:Button ID="btnLogin" ValidationGroup="Val" CssClass="btn btn-primary" runat="server" OnClick="btnLogin_Click" Text="Login" />
                            <asp:Button ID="btnSignup" CssClass="btn btn-secondary" runat="server" Text="Signup" OnClick="btnSignup_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Login-->

    <!-- Scripts -->
    <script>


        var user = document.getElementById("<%= HDCurrentUser.ClientID%>").value;
        var fullName = document.getElementById("<%= HDFullName.ClientID%>").value;
        var url = document.getElementById("<%= HDUrl.ClientID%>").value;

        function route(url) {

            location.href = url;
        }

        $(document).ready(function () {
            var PropVal = document.getElementById("<%= HDPropValMax.ClientID%>").value;
            $('#rangeProperty2').prop('max', parseInt(PropVal));
            $('#silderProperty1').prop('max', parseInt(PropVal));
            $('#silderProperty2').prop('max', parseInt(PropVal));
            document.getElementById('silderProperty2').value = parseInt(PropVal);

            var PropArea = document.getElementById("<%= HDPropAreaMax.ClientID%>").value;
            $('#rangearea2').prop('max', parseInt(PropArea));
            $('#silderarea1').prop('max', parseInt(PropArea));
            $('#silderarea2').prop('max', parseInt(PropArea));
            document.getElementById('silderarea2').value = parseInt(PropArea);

            var ProdVal = document.getElementById("<%= HDProdValMax.ClientID%>").value;
            $('#rangeProduct2').prop('max', parseInt(ProdVal));
            $('#sildeProduct1').prop('max', parseInt(ProdVal));
            $('#silderProduct2').prop('max', parseInt(ProdVal));
            document.getElementById('silderProduct2').value = parseInt(ProdVal);

            slideOneArea();
            slideTwoArea();
            slideOneProperty();
            slideTwoProperty();
            slideOneProduct();
            slideTwoProduct();

            var ddlGovernorate = document.getElementById("<%= ddlGovernorate.ClientID%>");
            ddlGovernorate.options[0].style.display = "none";

            var ddlPropertyType = document.getElementById("<%= ddlPropertyType.ClientID%>");
            ddlPropertyType.options[0].style.display = "none";

            var ddlTenure = document.getElementById("<%= ddlTenure.ClientID%>");
            ddlTenure.options[0].style.display = "none";

            if (ddlGovernorate.options[0].selected != true) {
                document.getElementById("ddMenuWilayat").disabled = false;
                PopulateWilayats(document.getElementById("<%= ddlGovernorate.ClientID%>"));

            }
            else {
                document.getElementById("ddMenuWilayat").disabled = true;
            }
        });

        //Slider Area Landing Page
        let Locale = Intl.NumberFormat('en-IN');

        let sliderOne = document.getElementById("silderarea1");
        let sliderTwo = document.getElementById("silderarea2");
        let displayValOne = document.getElementById("rangearea1");
        let displayValTwo = document.getElementById("rangearea2");
        let minGap = 0;
        let sliderTrack = document.querySelector(".slider-track-area");
        let sliderMaxValue = 0;

        function slideOneArea() {
            sliderMaxValue = parseInt(document.getElementById("silderarea1").max);
            if (parseInt(sliderTwo.value) - parseInt(sliderOne.value) <= minGap) {
                sliderOne.value = parseInt(sliderTwo.value) - minGap;
            }
            displayValOne.textContent = sliderOne.value;
            document.getElementById("<%= HDAreaMin.ClientID %>").value = sliderOne.value;
            fillColorArea();

        }

        function slideTwoArea() {
            if (parseInt(sliderTwo.value) - parseInt(sliderOne.value) <= minGap) {
                sliderTwo.value = parseInt(sliderOne.value) + minGap;
            }
            displayValTwo.textContent = sliderTwo.value;
            document.getElementById("<%= HDAreaMax.ClientID %>").value = sliderTwo.value;
            fillColorArea();
        }

        function fillColorArea() {
            var percent1 = (sliderOne.value / sliderMaxValue) * 100;
            var percent2 = (sliderTwo.value / sliderMaxValue) * 100;
            sliderTrack.style.background = `linear-gradient(to right, rgb(229, 229, 229) ${percent1}% , rgb(132, 25, 220) ${percent1}% ,
            rgb(132, 25, 220) ${percent2}% , rgb(229, 229, 229) ${percent2}%)`;

            console.log(sliderTrack.style.background);
        }

        ////Slider Area Landing Page

        ////Slider Price Property Landing Page

        let sliderOneProperty = document.getElementById("silderProperty1");
        let sliderTwoProperty = document.getElementById("silderProperty2");
        let displayValOneProperty = document.getElementById("rangeProperty1");
        let displayValTwoProperty = document.getElementById("rangeProperty2");
        let minGapProperty = 0;
        let sliderTrackProperty = document.querySelector(".slider-track-property");
        let sliderMaxValueProperty = 0;

        function slideOneProperty() {
            sliderMaxValueProperty = parseInt(document.getElementById("silderProperty1").max);
            if (parseInt(sliderTwoProperty.value) - parseInt(sliderOneProperty.value) <= minGapProperty) {
                sliderOneProperty.value = parseInt(sliderTwoProperty.value) - minGapProperty;
            }
            displayValOneProperty.textContent = Locale.format(sliderOneProperty.value);
            document.getElementById("<%= HDPropertyMin.ClientID %>").value = sliderOneProperty.value;
            fillColorProperty();

        }

        function slideTwoProperty() {
            if (parseInt(sliderTwoProperty.value) - parseInt(sliderOneProperty.value) <= minGapProperty) {
                sliderTwoProperty.value = parseInt(sliderOneProperty.value) + minGapProperty;
            }
            displayValTwoProperty.textContent = Locale.format(sliderTwoProperty.value);
            document.getElementById("<%= HDPropertyMax.ClientID %>").value = sliderTwoProperty.value;
            fillColorProperty();
        }

        function fillColorProperty() {
            var percentProp1 = (sliderOneProperty.value / sliderMaxValueProperty) * 100;
            var percentProp2 = (sliderTwoProperty.value / sliderMaxValueProperty) * 100;
            sliderTrackProperty.style.background = `linear-gradient(to right, rgb(229, 229, 229) ${percentProp1}% , rgb(132, 25, 220) ${percentProp1}% ,
            rgb(132, 25, 220) ${percentProp2}% , rgb(229, 229, 229) ${percentProp2}%)`;

            console.log(sliderTrackProperty.style.background);
        }

        //Slider Price Property Landing Page

        //Slider Price Product Landing Page

        let sliderOneProduct = document.getElementById("silderProduct1");
        let sliderTwoProduct = document.getElementById("silderProduct2");
        let displayValOneProduct = document.getElementById("rangeProduct1");
        let displayValTwoProduct = document.getElementById("rangeProduct2");
        let minGapProduct = 0;
        let sliderTrackProduct = document.querySelector(".slider-track-product");
        let sliderMaxValueProduct = 0;

        function slideOneProduct() {
            sliderMaxValueProduct = parseInt(document.getElementById("silderProduct1").max);
            if (parseInt(sliderTwoProduct.value) - parseInt(sliderOneProduct.value) <= minGapProduct) {
                sliderOneProduct.value = parseInt(sliderTwoProduct.value) - minGapProduct;
            }
            displayValOneProduct.textContent = Locale.format(sliderOneProduct.value);
            document.getElementById("<%= HDProductMin.ClientID %>").value = sliderOneProduct.value;
            fillColorProduct();

        }

        function slideTwoProduct() {
            if (parseInt(sliderTwoProduct.value) - parseInt(sliderOneProduct.value) <= minGapProduct) {
                sliderTwoProduct.value = parseInt(sliderOneProduct.value) + minGapProduct;
            }
            displayValTwoProduct.textContent = Locale.format(sliderTwoProduct.value);
            document.getElementById("<%= HDProductMax.ClientID %>").value = sliderTwoProduct.value;
            fillColorProduct();
        }

        function fillColorProduct() {
            var percent1 = (sliderOneProduct.value / sliderMaxValueProduct) * 100;
            var percent2 = (sliderTwoProduct.value / sliderMaxValueProduct) * 100;
            sliderTrackProduct.style.background = `linear-gradient(to right, rgb(229, 229, 229) ${percent1}% , rgb(132, 25, 220) ${percent1}% ,
            rgb(132, 25, 220) ${percent2}% , rgb(229, 229, 229) ${percent2}%)`;

            console.log(sliderTrackProduct.style.background);
        }

        //Slider Price Product Landing Page
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

        function PopulateWilayats(element) {
            var value = element.options[element.selectedIndex].value;
            PageMethods.GetWilayats(value, OnSuccess);
      
        }

        function OnSuccess(response, userContext, methodName) {
            var json = JSON.parse(response);

            if (json.Wilayat.length > 0) {
                document.getElementById("ddMenuWilayat").disabled = false;
                var wilayats = document.getElementById("<%= LBWilayat.ClientID%>");

                var tr = wilayats.getElementsByTagName("tr");

                for (var i = 0; i < tr.length; i++) {
                    var td = tr[i].getElementsByTagName("td")[0];
                    var chk = td.getElementsByTagName("input")[0];

                    for (var j = 0; j < json.Wilayat.length; j++) {
                        if (json.Wilayat[j].WilayatID == parseInt(chk.value)) {
                            tr[i].style.display = "block";
                            break;
                        }
                        else if (j == (json.Wilayat.length - 1)) {
                            tr[i].style.display = "none";
                            chk.selected = false;
                        }
                    }
                }
            }
        }

        function CreateChat(element) {
            if (user != null && user != '') {
                var text = element.parentElement.parentElement.getElementsByTagName("textarea")[0];

                var $chatHTML = $("<div class='row chat-container' style='display:none'><div class='col-12 col-sm-12 col-md-12 col-lg-12 message-container'>" +
                    "<div class='chat-message mt-2 w-100'><div class='row'><div class='col-2 col-sm-2 col-md-2 col-lg-2 img-fluid'>" +
                    "<img src='" + url + "'></div><div class='col-6 col-sm-6 col-md-7 col-lg-7'><div class='row'>" +
                    "<div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'><h2 class='c-w'>" + fullName + "</h2><p class='c-w'>" + text.value + "</p></div>" +
                    "<div class='col-12 col-sm-12 col-md-12 col-lg-12'></div></div></div><div class='col-2 col-sm-2 col-md-2 col-lg-2 btn-reply-fluid'>" +
                    "<input type='button' class='btn bg-gray br' value='Reply' onclick='ViewSubReply(this)'></div>" +
                    "<div class='col-2 col-sm-2 col-md-1 col-lg-1 comment-icon-container'><i class='far fa-comments c-w'></i></div>" +
                    "<div class='col-12 col-sm-12 col-md-12 col-lg-12 text-reply-fluid'><div class='row'><div class='col-9 col-sm-9 col-md-10 col-lg-10'>" +
                    "<input type='text' class='form-control' placeholder='Enter Reply...' /></div><div class='col-3 col-sm-3 col-md-2 col-lg-2'>" +
                    "<input type='button' class='btn bg-gray br c-b btn-submit f-r' value='Submit' onclick='SubmitReply(this);'></div></div></div></div></div></div></div>");

                $(element.parentElement.parentElement.parentElement.getElementsByClassName("card-body")[0]).append($chatHTML);
                $chatHTML.show(500);
                text.value = '';

                var chats = element.parentElement.parentElement.parentElement.getElementsByClassName("chat-container");

                if (chats.length >= 4) {
                    chats[0].remove();
                }
            }
            else {
                $('#login').modal('show');
            }
        }

        function ViewSubReply(element) {
            var ReplyBox = element.parentElement.parentElement.getElementsByClassName("text-reply-fluid")[0];
            $(ReplyBox).toggle(500);
        }

        function SubmitReply(element) {
            if (user != null && user != '') {
                var text = element.parentElement.parentElement.getElementsByTagName("input")[0];

                var $replyHTML = $("<div class='reply'style='display:none'><div class='row reply-box'><div class='col-3 col-sm-3 col-md-2 col-lg-2 img-fluid-reply'> " +
                    "<img src='" + url + "'></div><div class='col-6 col-sm-6 col-md-8 col-lg-8'><div class='row'>" +
                    "<div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'><h6>" + fullName + "</h6>" +
                    "<p>" + text.value + "</p></div></div></div>" +
                    "<div class='col-3 col-sm-3 col-md-2 col-lg-2 btn-reply-fluid'><input type='button' class='btn bg-purple br c-w' value='Reply' onclick='ViewSubReply(this)'></div>" +
                    "<div class='col-12 col-sm-12 col-md-12 col-lg-12 text-reply-fluid'><div class='row'><div class='col-9 col-sm-9 col-md-10 col-lg-10'>" +
                    "<input type='text' class='form-control' placeholder='Enter Reply...' /></div><div class='col-3 col-sm-3 col-md-2 col-lg-2'>" +
                    "<input type='button' class='btn bg-purple br c-w btn-submit f-r' value='Submit' onclick='SubmitSubReply(this);'></div></div></div></div></div>");

                $(element.parentElement.parentElement.parentElement.parentElement.parentElement).append($replyHTML);
                $replyHTML.show(500);
                element.parentElement.parentElement.parentElement.style.display = 'none'
                text.value = '';
            }
            else {
                $('#login').modal('show');
            }
        }

        function SubmitSubReply(element) {
            if (user != null && user != '') {
                var text = element.parentElement.parentElement.getElementsByTagName("input")[0];

                var $subReplyHTML = $("<div class=' col-12 col-sm-12 col-md-12 col-lg-12 sub-reply' style='display:none'><div class='row bg-gray c-b sub-reply-box'>" +
                    "<div class='col-3 col-sm-3 col-md-2 col-lg-2 img-fluid-reply'><img src='" + url + "'></div><div class='col-6 col-sm-6 col-md-8 col-lg-8'>" +
                    "<div class='row'><div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'><h6>" + fullName + "</h6><p>" + text.value + "</p>" +
                    "</div></div></div><div class='col-3 col-sm-3 col-md-2 col-lg-2 btn-reply-fluid'><i class='fas fa-reply c-p .fs-7'></i></div></div></div>");

                $(element.parentElement.parentElement.parentElement.parentElement).append($subReplyHTML);
                $subReplyHTML.show(500);
                element.parentElement.parentElement.parentElement.style.display = 'none'
                text.value = '';
            }
            else {
                $('#login').modal('show');
            }
        }

    </script>
    <script src="Assets/js/LandingJS.js"></script>
</asp:Content>
