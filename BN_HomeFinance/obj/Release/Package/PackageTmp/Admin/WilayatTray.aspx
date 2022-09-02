<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="WilayatTray.aspx.cs" Inherits="BN_HomeFinance.Admin.WilayatTray" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row mb-4 mt-4">
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <h1>Wilayats</h1>
        </div>
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <asp:Button ID="btnNewWilayat" CssClass="btn btn-primary f-r create-button" OnClick="btnNewWilayat_Click" runat="server" Text="Add New Wilayat" />
        </div>
    </div>
    <div class="row custom-table-responsive mb-4">
        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
            <asp:GridView ID="gv_Wilayats"  CssClass="table custom-table" EmptyDataText="No Data Found." ShowHeaderWhenEmpty="true" runat="server" AutoGenerateColumns="False" DataKeyNames="WilayatID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="WilayatID" Visible="false" HeaderText="WilayatID" InsertVisible="False" ReadOnly="True" SortExpression="WilayatID" />
                    <asp:BoundField DataField="Wilayat" HeaderText="Wilayat" SortExpression="Wilayat" />
                    <asp:BoundField DataField="GovernorateID" Visible="false" HeaderText="GovernorateID" SortExpression="GovernorateID" />
                    <asp:BoundField DataField="Governorate" HeaderText="Governorate" SortExpression="Governorate" />
                    <asp:TemplateField HeaderText=" ">
                        <ItemTemplate>
                            <%# EditLink(Eval("WilayatID").ToString(), "Edit")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Connection %>" SelectCommand="ReadAllWilayat" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
</asp:Content>
