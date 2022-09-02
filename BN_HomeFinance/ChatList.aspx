<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ChatList.aspx.cs" Inherits="BN_HomeFinance.ChatList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
    <asp:HiddenField Value="" runat="server" ID="hdUserName" />
    <asp:HiddenField Value="" runat="server" ID="hdFullName" />
    <asp:HiddenField Value="" runat="server" ID="hdUrl" />
    <!-- Section Chat-->
    <section id="sec-Chat" class="section mt-5">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <h1>Ask Your Query</h1>
            </div>
        </div>
        <div class="row sec-Chat"">
            <div class="col-8 col-sm-9 col-md-10 col-lg-10" style="padding-right:1%">
                <textarea class="form-control text-Chat" placeholder="Type your query here..." rows="2"></textarea>
            </div>
            <div class="col-4 col-sm-3 col-md-2 col-lg-2">
                <button class="btn bg-yellow btn-chat-submit" type="button" onclick="CreateChat(this)">Submit</button>
            </div>
        </div>
        <div class="row mb-4">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 sec-Chat">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-4">
                        <div class="shadow-lg card">
                            <div class="card-body">
                                <%= html %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- Section Chat-->

    <script>
        var username = document.getElementById("<%= hdUserName.ClientID%>").value;
        var fullname = document.getElementById("<%= hdFullName.ClientID%>").value;
        var url = document.getElementById("<%= hdUrl.ClientID%>").value;

        function route(url) {
            location.href = url;
        }

        function CreateChat(element) {
            debugger;
            if (username != "") {
                var text = element.parentElement.parentElement.getElementsByTagName("textarea")[0];

                PageMethods.SubmitChat(text.value, document.getElementById("<%= hdUserName.ClientID%>").value, OnSuccess, OnFailure, text);
            }
            else {
                alert("You need to login first.");
                location.href = "/<%= BN_HomeFinance.Helper.pg_Login%>";
            }
        }

        function OnSuccess(response, userContext, methodName) {
            debugger;
            var uri = '/ChatPage.aspx?ChatID=' + response;

            var $chatHTML = $("<div class='row mt-1 chat-container'><div class='col-12 col-sm-12 col-md-12 col-lg-12 message-container'>" +
                "<div class='chat-message shadow mt-2 w-100'><div class='row'><div class='col-2 col-sm-2 col-md-2 col-lg-2 img-fluid'>" +
                "<img src='" + url + "'></div><div class='col-6 col-sm-6 col-md-7 col-lg-7'><div class='row'>" +
                "<div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'><h6 class='c-w'>" + fullname + "</h6>" +
                "<p class='c-w'>" + userContext.value + "</p></div><div class='col-12 col-sm-12 col-md-12 col-lg-12'></div>" +
                "</div></div><div class='col-2 col-sm-2 col-md-2 col-lg-2 btn-reply-fluid'><input type='button' class='btn bg-gray br' value='View' " +
                "onclick='route(\"" + uri + "\")'></div><div class='col-2 col-sm-2 col-md-1 col-lg-1 comment-icon-container'>" +
                "<i class='far fa-comments c-w'></i></div></div></div></div></div>");

            $(document.getElementsByClassName("card-body")[0]).append($chatHTML);

            $('html, body').animate({
                scrollTop: $($chatHTML).offset().top
            }, 'slow');

            $chatHTML.show(500);
            userContext.value = '';
        }
        function OnFailure(response, userContext, methodName) { }
    </script>
</asp:Content>
