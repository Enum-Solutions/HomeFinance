<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="ProductCategoryTray.aspx.cs" Inherits="BN_HomeFinance.Admin.ProductCategoryTray" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row mb-4 mt-4">
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <h1>Product Categories</h1>
        </div>
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <asp:Button ID="btnNewProductCategory" CssClass="btn btn-primary f-r create-button" OnClick="btnNewProductCategory_Click" runat="server" Text="Add New Category" />
        </div>
    </div>
    <div class="row custom-table-responsive mb-4">
        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
            <asp:GridView runat="server" ID="gv_Categories" CssClass="table custom-table" AutoGenerateColumns="False" DataKeyNames="CategoryID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="CategoryID" Visible="false" HeaderText="CategoryID" InsertVisible="False" ReadOnly="True" SortExpression="CategoryID" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" SortExpression="CategoryName" />
                    <asp:TemplateField HeaderText=" ">
                        <ItemTemplate>
                            <%# EditLink(Eval("CategoryID").ToString(), "EDIT")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Connection %>" SelectCommand="SELECT [CategoryID], [CategoryName] FROM [ProductCategories]"></asp:SqlDataSource>
</asp:Content>
