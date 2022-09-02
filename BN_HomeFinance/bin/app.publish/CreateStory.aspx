<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CreateStory.aspx.cs" Inherits="BN_HomeFinance.CreateStory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <div class="container">
            <div class="row">
                <div class="col-sm-12 t-a-c">
                    <img src="/img/logo.png" alt="Logo" class="img-fluid-s" />
                </div>
            </div>
            <div class="row">
                <h2 class="h2">Add New Story</h2>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:Label ID="lblHeader" runat="server" Text="Header"></asp:Label>
                    <asp:TextBox CssClass="form-control" ID="txtHeader" placeholder="Story Header" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                    <textarea id="txtDescription" class="form-control" placeholder="Story Description" runat="server" rows="4"></textarea>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                        <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                        <asp:ListItem Text="InActive" Value="InActive"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3"></div>
                <div class="col-sm-2">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" runat="server" Text="Submit" />
                    <asp:Button ID="btnUpdate" CssClass="btn btn-primary" Visible="false" OnClick="btnUpdate_Click" runat="server" Text="Update" />
                </div>
                <div class="col-sm-2">
                    <asp:Button ID="btnCancel" CssClass="btn" OnClick="btnCancel_Click" runat="server" Text="Cancel" />
                </div>
                <div class="col-sm-2">
                    <asp:Button ID="btnDelete" CssClass="btn btn-primary" Visible="false" OnClick="btnDelete_Click" runat="server" Text="Delete" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
