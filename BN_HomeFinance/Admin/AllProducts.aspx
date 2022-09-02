<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="AllProducts.aspx.cs" Inherits="BN_HomeFinance.Admin.AllProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row mb-4 mt-4">
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <h1>All Products</h1>
        </div>
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <asp:Button ID="btnNewProduct" OnClick="btnNewProduct_Click" CssClass="btn btn-primary f-r create-button" runat="server" Text="Create New Product" />
        </div>
    </div>
    <div class="row custom-table-responsive mb-4">
        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
            <asp:GridView OnRowDataBound="gv_Products_RowDataBound" CssClass="table custom-table" EmptyDataText="No Data Found." ShowHeaderWhenEmpty="true" ID="gv_Products" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Header">
                        <ItemTemplate>
                            <%# EditLink(Eval("ProductID").ToString(), Eval("ProductName").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Product Value" DataField="Price" />
                    <asp:BoundField HeaderText="Created By" DataField="FullName" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
