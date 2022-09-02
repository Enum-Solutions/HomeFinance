<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="BN_HomeFinance.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .single_product_thumb .carousel-indicators {
            bottom: -86px;
            -webkit-box-pack: left;
            -ms-flex-pack: left;
            justify-content: left;
            left: 0;
            margin: 0;
            display: -moz-flex;
            display: -ms-flex;
            display: -o-flex;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            width: 100%;
        }

            .single_product_thumb .carousel-indicators li {
                background-position: center center;
                background-size: cover;
                height: 80px;
                width: 25%;
            }

        .carousel-indicators [data-bs-target] {
            border-radius: 0%;
        }
    </style>
    <%--<link rel="stylesheet" href="Assets/css/core-style.css">
    <link href="Assets/css/responsive.css" rel="stylesheet">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <div class="row">
            <h2 class="h2 mt-4">Product Details</h2>
        </div>

        <section class="single_product_details_area mt-4">
            <div class="row">
                <div class="col-12 col-md-6">
                    <div class="single_product_desc">
                        <div class="row">
                            <div class="col-sm-12">
                                <h4 class="title">
                                    <h1>
                                        <asp:Label ID="lblProductName" runat="server" Text="Hello World"></asp:Label>
                                    </h1>
                                </h4>

                                <h4 class="price">
                                    <asp:Label ID="lblPrice" runat="server" Text="100$"></asp:Label>
                                    &nbsp;OMR
                                </h4>
                                <p class="available">
                                    Category : 
                                    <span class="text-muted">
                                        <asp:Label ID="lblCategory" runat="server" Text="Yes"></asp:Label>
                                    </span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-12">
                            <div class="row">
                                <div id="accordion" role="tablist">
                                    <div class="card">
                                        <div class="card-header" role="tab" id="headingOne">
                                            <h6 class="mb-0">
                                                <h6 data-toggle="collapse" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">Description</h6>
                                            </h6>
                                        </div>
                                        <div id="collapseOne" class="collapse show" role="tabpanel" aria-labelledby="headingOne" data-parent="#accordion">
                                            <div class="card-body">
                                                <asp:Label ID="lblDescription" runat="server" Text="Description is here"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-md-6">
                    <div class="single_product_thumb">
                        <div id="product_details_slider" class="carousel carousel-dark slide" data-bs-ride="carousel">
                            <ol class="carousel-indicators">
                                <%= html %>
                            </ol>
                            <div class="carousel-inner">
                                <%=html2 %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <div class="row new-section" style="padding-top:100px;">
            <div class="row">
                <h4 class="body-headings">Contact Vendor</h4>
            </div>
            <div class="col-md-6">
                <div class="formContainer" style="border: 1px solid #888888; padding: 10px; box-shadow: 0px 2px 18px 5px #888888; border-radius: 15px; padding-left: 40px; padding-right: 40px; padding-top: 35px;min-height: 400px;">
                    <asp:TextBox ID="txtName" CssClass="form-control mt-4" runat="server" placeholder="&nbsp;&nbsp;Name"></asp:TextBox>
                    <asp:TextBox ID="txtEmail" CssClass="form-control mt-4" runat="server" placeholder="&nbsp;&nbsp;Email"></asp:TextBox>
                    <textarea id="TextAreaComments" class="form-control mt-4" cols="20" rows="3" runat="server" placeholder="Comments"></textarea><br />
                    <asp:Button ID="btnSendEmail" CssClass="btn btn-primary mt-4" runat="server" Text="Send Message" OnClick="btnSendEmail_Click" />
                    <br />
                    <br />
                </div>
            </div>

            <div class="col-md-6">
                <div class="col-md-12 formContainer" style="line-height: 28px; margin-left: -5px border: 1px solid #888888; padding: 10px; box-shadow: 0px 2px 18px 5px #888888; border-radius: 15px; padding-left: 40px; padding-right: 40px; padding-top: 35px;min-height: 400px;">
                    <asp:Label runat="server" />
                    <strong>Name</strong><br />
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                    <br />
                    <br>

                    <asp:Label runat="server" />
                    <strong>Address    </strong>
                    <br />
                    <img src="Assets/img/pinlocation.png" alt="Alternate Text" height="20" width="20" /><asp:Label ID="lblAddress" runat="server"></asp:Label><br />
                    <br />

                    <asp:Label Text="" runat="server" />
                    <strong>Whatsapp   </strong>
                    <br />
                    <img src="Assets/img/whatsappPNG.png" alt="Alternate Text" height="20" width="20" /><asp:Label ID="lblPhone" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <script src="Assets/js/popper.min.js"></script>

        <script src="Assets/js/plugins.js"></script>

        <script src="Assets/js/active.js"></script>
    </div>
</asp:Content>
