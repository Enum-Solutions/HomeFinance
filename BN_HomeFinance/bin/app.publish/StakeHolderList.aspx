﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="StakeHolderList.aspx.cs" Inherits="BN_HomeFinance.StakeHolderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        a {
            text-decoration: none;
        }

        p {
            margin: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row">
        <div id="carouselExampleDark" class="carousel carousel-dark slide" data-bs-ride="carousel">
            <div class="carousel-indicators">
                <%=HTMLIndicators %>
            </div>
            <div class="carousel-inner">
                <%= HTML%>
            </div>
            <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span><span class="visually-hidden">Previous</span></button>
            <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span><span class="visually-hidden">Next</span></button>
        </div>
    </div>
    <div class="header mt-4">
        <h2>
            <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></h2>
        <%= HTMLBuilders %>
    </div>
</asp:Content>
