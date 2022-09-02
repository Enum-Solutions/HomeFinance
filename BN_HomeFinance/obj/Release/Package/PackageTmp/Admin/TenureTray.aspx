<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="TenureTray.aspx.cs" Inherits="BN_HomeFinance.Admin.TenureTray" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row mb-4 mt-4">
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <h1>Tenures</h1>
        </div>
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <asp:Button ID="btnNewTenure" CssClass="btn btn-primary f-r create-button" OnClick="btnNewTenure_Click" runat="server" Text="Add New Tenure" />
        </div>
    </div>
    <div class="row custom-table-responsive mb-4">
        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
            <asp:GridView CssClass="table custom-table" EmptyDataText="No Data Found." ShowHeaderWhenEmpty="true" ID="gv_Tenure" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" />
                    <asp:BoundField DataField="Tenure" HeaderText="Tenure" SortExpression="Tenure" />
                    <asp:TemplateField HeaderText=" ">
                        <ItemTemplate>
                            <%# EditLink(Eval("ID").ToString(), "EDIT")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" ReadOnly="True" SortExpression="ID" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Connection %>" SelectCommand="SELECT [Tenure], [ID] FROM [Tenure]"></asp:SqlDataSource>
</asp:Content>
