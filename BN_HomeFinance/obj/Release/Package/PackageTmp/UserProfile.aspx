<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="BN_HomeFinance.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
            font-size: 10px !important;
            letter-spacing: 1px !important;
        }

        #btnSendEmailinput, [type=submit]:hover {
            background-color: #8A53A4 !important;
        }
        /*Styling for contact form*/

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
        }

        .planContainer .title h2 {
            font-size: 25px;
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
            background-color: rgb(113, 46, 141);
            color: white !important;
        }

        .map {
            border: 1px solid var(--gray);
            border-radius: 15px;
            height: 400px;
            width: 100%;
        }

        .info {
            padding: 5% 7%;
        }

        .carousel-item {
            padding: 0% 10%;
        }

        .carousel-dark .carousel-control-next-icon, .carousel-dark .carousel-control-prev-icon {
            filter: none;
            height: 50px;
            width: 50px;
        }

        .promotion-item {
            padding: 0% 1%;
        }

        .contact1 {
            width: 100%;
            min-height: 100%;
            padding: 5%;
            background: #009bff;
            background: var(--gray);
            display: -webkit-box;
            display: -webkit-flex;
            display: -moz-box;
            display: -ms-flexbox;
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            align-items: center;
        }

        .container-contact1 {
            width: 100%;
            background: white;
            border-radius: 20px;
            padding: 4% 6% 4% 8%;
        }

        .contact1-pic {
            width: 100%;
            text-align: center;
        }

        .form-control:disabled, .form-control[readonly] {
            background-color: white;
            opacity: 1;
        }

        @-webkit-keyframes anim-shadow {
            to {
                box-shadow: 0px 0px 80px 30px;
                opacity: 0;
            }
        }

        @keyframes anim-shadow {
            to {
                box-shadow: 0px 0px 80px 30px;
                opacity: 0;
            }
        }

        @media Screen and (max-width:992px) {
            .timeline-card {
                height: 300px !important;
            }
        }

        @media Screen and (max-width:756px) {
            .timeline-card {
                height: 210px !important;
            }
        }
        @media Screen and (max-width:567px) {
            .timeline-card {
                height: 185px !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">

        <div class="page-content intro-single">
            <div class="container">
                <div class="cover shadow-lg bg-white">
                    <%= Background%>
                    <div class="row">
                        <div class="col-lg-4 col-md-5">
                            <div class="avatar hover-effect bg-white shadow-sm p-1">
                                <asp:Image ID="imgUSer" Height="200px" Width="200px" CssClass="img-logo-custom" runat="server" />
                            </div>
                        </div>
                        <div class="col-lg-8 col-md-7 text-center text-md-start">
                            <h2 class="h1" data-aos="fade-left" data-aos-delay="0">
                                <asp:Label ID="lblFullName" runat="server" Text="Label"></asp:Label>
                            </h2>
                            <p data-aos="fade-left" data-aos-delay="100"></p>
                        </div>
                    </div>
                </div>
                <div class="about-section pt-4 px-3 px-lg-4 mt-1">
                    <div class="row">
                        <h2 class="h3 mb-3">About Us</h2>
                        <div class="col-md-12">
                            <p><%= About %></p>
                        </div>
                        <div class="col-md-5 offset-md-1">
                            <div class="row mt-2">
                            </div>
                        </div>
                    </div>
                </div>

                <%= Links %>

                <%= Promotions %>

                <%= PropPromo%>

                <div class="row mt-4 mb-4 px-3 px-lg-3">
                    <h2 class="h3 mt-3">Contact Information</h2>
                </div>

                <div class="row">
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <div class="contact1">
                            <div class="container-contact1">
                                <div class="row">
                                    <div class="col-0 col-sm-0 col-md-6 col-lg-6">
                                        <div class="contact1-pic js-tilt" data-tilt>
                                            <img src="Assets/img/img-contact.png" alt="IMG">
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                                        <form class="contact1-form validate-form">
                                            <div class="wrap-input1 validate-input" data-validate="Name is required">
                                                Name
                                                <asp:TextBox ID="txtName" CssClass="form-control" ReadOnly runat="server"></asp:TextBox>
                                            </div>

                                            <div class="wrap-input1 validate-input" data-validate="Valid email is required: ex@abc.xyz">
                                                Address
                                                <asp:TextBox ID="txtAddress" CssClass="form-control" ReadOnly runat="server"></asp:TextBox>
                                            </div>

                                            <div class="wrap-input1 validate-input" data-validate="Subject is required">
                                                Whatsapp/Contact
                                                <asp:TextBox ID="txtWhatsapp" CssClass="form-control" ReadOnly runat="server"></asp:TextBox>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
