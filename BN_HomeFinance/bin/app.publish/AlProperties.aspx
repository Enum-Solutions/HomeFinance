<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AlProperties.aspx.cs" Inherits="BN_HomeFinance.AlProperties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <!-- ======= Intro Single ======= -->
        <section class="intro-single">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 col-lg-8">
                        <div class="title-single-box">
                            <h1 class="title-single">Properties</h1>
                        </div>
                    </div>
                    <div class="col-md-12 col-lg-4">
                    </div>
                </div>
            </div>
        </section>
        <!-- End Intro Single-->
        <!-- Section List -->
        <section id="sec-List" class="section property-grid">
            <div class="row">
                <%= Properties %>
            </div>
        </section>
    <!-- Section List -->
    </div>
</asp:Content>
