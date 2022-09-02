<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MyProducts.aspx.cs" Inherits="BN_HomeFinance.MyProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <div class="row mb-4 mt-4">
            <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                <h1>My Products</h1>
            </div>
            <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                <asp:Button ID="btnProduct" CssClass="btn btn-primary f-r create-button" OnClick="btnProduct_Click" runat="server" Text="Add New Product" />
            </div>
        </div>
        <div class="row custom-table-responsive mb-4">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <asp:GridView OnRowDataBound="gv_Products_RowDataBound" CssClass="table custom-table" EmptyDataText="No Data Found." ShowHeaderWhenEmpty="true" ID="gv_Products" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Product">
                            <ItemTemplate>
                                <%# EditLink(Eval("ProductID").ToString(), Eval("ProductName").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Category" DataField="CategoryName" />
                        <asp:BoundField HeaderText="Product Value"  DataField="Price" />
                    </Columns>
                </asp:GridView>
            </div> 
        </div>
    </div>
</asp:Content>
