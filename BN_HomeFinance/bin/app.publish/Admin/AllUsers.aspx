<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="AllUsers.aspx.cs" Inherits="BN_HomeFinance.Admin.AllUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="row mb-4 mt-4">
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <h1>All Users</h1>
        </div>
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <asp:Button ID="btnNewUser" OnClick="btnNewUser_Click" CssClass="btn btn-primary f-r create-button" runat="server" Text="Create New User" />
        </div>
    </div>
    <div class="row custom-table-responsive mb-4">
        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
            <asp:GridView CssClass="table custom-table" ShowHeaderWhenEmpty="true" ID="gv_Users" runat="server" EmptyDataText="No User Found." AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <%# EditLink(Eval("UserID").ToString(), Eval("FullName").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Login" DataField="LoginName" />
                    <asp:BoundField HeaderText="Email" DataField="Email" />
                    <asp:BoundField HeaderText="Type" DataField="UserType" />
                    <asp:BoundField HeaderText="Status" DataField="Status" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
