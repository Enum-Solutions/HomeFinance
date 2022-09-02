<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="BN_HomeFinance.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <style>
        .plan:hover {
            box-shadow: 0px 2px 18px #888 !important;
        }

        .plan {
            padding: 0px 30px;
            border: 1px solid rgb(113, 46, 141);
            background: #fff;
            float: left;
            width: 100%;
            text-align: center;
            border-radius: 5px;
            margin: 0 0 20px 0;
            -webkit-box-shadow: 0 1px 3px rgba(0,0,0,0.1);
            box-shadow: 0 1px 3px rgba(0,0,0,0.1);
            min-height: 150px;
        }

        .planContainer .title h2 {
            font-size: 1rem;
            font-weight: 400;
            color: rgb(113, 46, 141);
            margin: 0;
            padding: .6em 0;
            text-align: center;
        }

            .planContainer .title h2.bestPlanTitle {
                background: rgb(113, 46, 141);
                background: -webkit-linear-gradient(top, #475975, #364761);
                background: -moz-linear-gradient(top, #475975, #364761);
                background: -o-linear-gradient(top, #475975, #364761);
                background: -ms-linear-gradient(top, #475975, #364761);
                background: linear-gradient(top, #475975, #364761);
                color: #fff;
                border-radius: 5px 5px 0 0;
            }


        .planContainer .price p {
            background: rgb(113, 46, 141);
            background: -webkit-linear-gradient(top, #475975, #364761);
            background: -moz-linear-gradient(top, #475975, #364761);
            background: -o-linear-gradient(top, #475975, #364761);
            background: -ms-linear-gradient(top, #475975, #364761);
            background: linear-gradient(top, #475975, #364761);
            color: #fff;
            font-size: 1.2em;
            font-weight: 700;
            height: 2.6em;
            line-height: 2.6em;
            margin: 0 0 1em;
        }


        .planContainer .button a {
            text-align: center;
            text-transform: uppercase;
            text-decoration: none;
            color: rgb(113, 46, 141);
            font-weight: 700;
            letter-spacing: 3px;
            line-height: 2.8em;
            border: 2px solid rgb(113, 46, 141);
            display: inline-block;
            width: 100%;
            height: 2.8em;
            border-radius: 4px;
            margin: .8em 0em;
        }

        .button :hover {
            border: 1px solid #FFC709 !important;
            background-color: #FFC709;
            /*background-color: rgb(113, 46, 141);*/
            color: white !important;
        }
    </style>

    <div class="container rounded bg-white">
        <div class='row mb-4'>
            <div class='col-12 col-sm-12 col-md-12 col-lg-12'>
                <div class="p-3">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h2 class="text-right">Admin Dashboard</h2>
                    </div>
                </div>
            </div>

            <!-- Lovs -->

            <div class='col-12 col-sm-12 col-md-12 col-lg-12'>
                <div class="p-3">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h3 class="text-right">LOVs</h3>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Governorates</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_GovernorateTray%>">Click Here</a></div>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Wilayats</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_WilayatTray %>">Click Here</a></div>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Categories</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_PropertyTypeTray %>">Click Here</a></div>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Product Categories</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_ProductCategoryTray %>">Click Here</a></div>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Property Amenities</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_AmenityTray%>">Click Here</a></div>
                    </div>
                </div>
            </div>
            <%--<div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Tenure</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_TenureTray %>">Click Here</a></div>
                    </div>
                </div>
            </div>--%>

            <!-- Lovs -->

            <!-- Home Widgets -->

            <div class='col-12 col-sm-12 col-md-12 col-lg-12'>
                <div class="p-3">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h3 class="text-right">Home Widgets</h3>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Landing Page Slider</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_AnnouncementTray %>">Click Here</a></div>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Story Board</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_MessageTray %>">Click Here</a></div>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Popup</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_AllPopups %>">Click Here</a></div>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Calculator Configurations</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_InterestTray %>">Click Here</a></div>
                    </div>
                </div>
            </div>

            <!-- Home Widgets -->

            <!-- Users -->

            <div class='col-12 col-sm-12 col-md-12 col-lg-12'>
                <div class="p-3">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h3 class="text-right">User Management</h3>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Users (Builders, Vendors, Consumers)</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_AllUsers %>">Click Here</a></div>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Paid Builder Promotions</h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_AllBuilderAnnouncements %>">Click Here</a></div>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Paid Vendor Promotions </h2>
                        </div>
                        <div class='button'><a href="<%= BN_HomeFinance.Helper.pg_AllVendorAnnouncements %>">Click Here</a></div>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>Builder/Vendor Profile Promotions</h2>
                        </div>
                        <div class='button'><a href="AllPromotions.aspx">Click Here</a></div>
                    </div>
                </div>
            </div>

            <!-- Users -->

            <!-- Property and Product -->

            <div class='col-12 col-sm-12 col-md-12 col-lg-12'>
                <div class="p-3">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h3 class="text-right">Properties and Products</h3>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>All Properties</h2>
                        </div>
                        <div class='button'><a href="AllProperties.aspx">Click Here</a></div>
                    </div>
                </div>
            </div>
            <div class='col-12 col-sm-6 col-md-4 col-lg-3 pd-1'>
                <div class='plan'>
                    <div class='planContainer'>
                        <div class='title'>
                            <h2>All Products</h2>
                        </div>
                        <div class='button'><a href="AllProducts.aspx">Click Here</a></div>
                    </div>
                </div>
            </div>

            <!-- Property and Product -->

        </div>
    </div>
</asp:Content>
