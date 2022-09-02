<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="AllVendorAnnouncements.aspx.cs" Inherits="BN_HomeFinance.Admin.AllVendorAnnouncements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row mb-4 mt-4">
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <h1>Paid Vendor Promotions</h1>
        </div>
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <asp:Button ID="btnNewAnnouncement" CssClass="btn btn-primary f-r create-button" OnClick="btnNewAnnouncement_Click" runat="server" Text="Add New Vendor Annoouncement" />
        </div>
    </div>
    <div class="row custom-table-responsive mb-4">
        <div class="col-12 col-sm-12 col-md-12 col-lg-12">

            <asp:GridView CssClass="table custom-table" EmptyDataText="No Data Found." ShowHeaderWhenEmpty="true" ID="gv_Announcements" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <%# EditLink(Eval("ID").ToString(), Eval("Header").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Created At" DataField="DateCreated" />
                    <asp:BoundField HeaderText="Created By" DataField="CreatedBy" />
                    <asp:BoundField HeaderText="Status" DataField="Status" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
