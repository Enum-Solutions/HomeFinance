<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ChatPage.aspx.cs" Inherits="BN_HomeFinance.ChatPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .rep {
            margin-left: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="HDCurrentUser" runat="server" />
    <asp:HiddenField ID="HDFullName" runat="server" />
    <asp:HiddenField ID="HDChatID" runat="server" />
    <asp:HiddenField Value="" runat="server" ID="HDUrl" />
    <!-- Section Chat-->
    <section id="sec-Chat" class="section mt-5">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <div class="row">
                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                        <h1>Chat Board</h1>
                    </div>
                </div>
            </div>
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
        var chatID = document.getElementById("<%= HDChatID.ClientID%>").value;
        var fullName = document.getElementById("<%= HDFullName.ClientID%>").value;
        var userID = document.getElementById("<%= HDCurrentUser.ClientID%>").value;
        var url = document.getElementById("<%= HDUrl.ClientID%>").value;

        function ViewSubReply(element) {
            debugger;
            var ReplyBox = element.parentElement.parentElement.getElementsByClassName("text-reply-fluid")[0];
            $(ReplyBox).toggle(500);
        }

        var txt;

        function SubmitReply(element) {
            debugger;
            txt = element.parentElement.parentElement.getElementsByTagName("input")[0];

            PageMethods.SubmitMessage(chatID, txt.value, userID, OnSuccess, OnFailure, element);

            
            $('html, body').animate({
                scrollTop: $(document).height()
            }, 'slow');
            
        }

        function OnSuccess(response, userContext , methodName) {
            var $replyHTML = $("<div class='reply'style='display:none'><div class='row reply-box'><div class='col-3 col-sm-3 col-md-2 col-lg-2 img-fluid-reply'> " +
                "<img src='" + url + "'></div><div class='col-6 col-sm-6 col-md-8 col-lg-8'><div class='row'>" +
                "<div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'><h6>" + fullName + "</h6>" +
                "<p>" + txt.value + "</p></div></div></div>" +
                "<div class='col-3 col-sm-3 col-md-2 col-lg-2 btn-reply-fluid'><input type='button' class='btn bg-purple br c-w' value='Reply' onclick='ViewSubReply(this)'></div>" +
                "<div class='col-12 col-sm-12 col-md-12 col-lg-12 text-reply-fluid'><div class='row'><div class='col-9 col-sm-9 col-md-10 col-lg-10'>" +
                "<input type='text' class='form-control' placeholder='Enter Reply...' /></div><div class='col-3 col-sm-3 col-md-2 col-lg-2'><div class='d-none m-id'><input type='text' value='" + response + "'></div>" +
                "<input type='button' class='btn bg-purple br c-w btn-submit f-r' value='Submit' onclick='SubmitSubReply(this);'></div></div></div></div></div>");

            $(userContext.parentElement.parentElement.parentElement.parentElement.parentElement).append($replyHTML);
            $replyHTML.show(500);
            userContext.parentElement.parentElement.parentElement.style.display = 'none'
            txt.value = '';
        }
        function OnFailure(response, userContext, methodName) {

        }

        function SubmitSubReply(element) {
            debugger;
            var text = element.parentElement.parentElement.getElementsByTagName("input")[0];
            var id = element.parentElement.getElementsByClassName("m-id")[0].getElementsByTagName("input")[0].value;

            PageMethods.SubmitReply(id, text.value, userID);

            var $subReplyHTML = $("<div class=' col-12 col-sm-12 col-md-12 col-lg-12 sub-reply' style='display:none'><div class='row bg-gray c-b sub-reply-box'>" +
                "<div class='col-3 col-sm-3 col-md-2 col-lg-2 img-fluid-reply'><img src='" + url + "'></div><div class='col-6 col-sm-6 col-md-8 col-lg-8'>" +
                "<div class='row'><div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'><h6>" + fullName + "</h6><p>" + text.value + "</p>" +
                "</div></div></div><div class='col-3 col-sm-3 col-md-2 col-lg-2 btn-reply-fluid'><i class='fas fa-reply c-p .fs-7'></i></div></div></div>");

            $(element.parentElement.parentElement.parentElement.parentElement).append($subReplyHTML);
            $subReplyHTML.show(500);
            element.parentElement.parentElement.parentElement.style.display = 'none'
            text.value = '';
        }
    </script>
</asp:Content>
