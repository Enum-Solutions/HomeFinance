<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MessageBoard.aspx.cs" Inherits="BN_HomeFinance.MessageBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <!-- Section Purple Header -->
        <section id="sec-header" class="bg-purple t-c">
            <h1 class="c-w w-100 t-c">Story Board</h1>
        </section>
        <!-- Section Purple Header -->

        <!--Section Messages-->
        <section id="sec-messages">
            <div class="accordion accordion-flush" id="accordionFlushExample">
                <%= HTML %>
            </div>
        </section>
    </div>
    <!-- Section Messages-->
</asp:Content>
