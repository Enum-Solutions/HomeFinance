<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SearchedProperties.aspx.cs" Inherits="BN_HomeFinance.SearchedProperties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- ======= Intro Single ======= -->
    <div class="container">
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
            <%--<div class="row">
            <div class="col-sm-12">
                <nav class="pagination-a">
                    <ul class="pagination justify-content-end">
                        <li class="page-item disabled">
                            <a class="page-link" href="#" tabindex="-1">
                                <span class="bi bi-chevron-left"></span>
                            </a>
                        </li>
                        <li class="page-item">
                            <a class="page-link" href="#">1</a>
                        </li>
                        <li class="page-item active">
                            <a class="page-link" href="#">2</a>
                        </li>
                        <li class="page-item">
                            <a class="page-link" href="#">3</a>
                        </li>
                        <li class="page-item next">
                            <a class="page-link" href="#">
                                <span class="bi bi-chevron-right"></span>
                            </a>
                        </li>
                    </ul>
                </nav>
            </div>
        </div>--%>
        </section>
    </div>
</asp:Content>
