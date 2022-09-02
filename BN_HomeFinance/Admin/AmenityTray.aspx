<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="AmenityTray.aspx.cs" Inherits="BN_HomeFinance.Admin.AmenityTray" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row mb-4 mt-4">
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <h1>Property Amenities</h1>
        </div>
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <asp:Button ID="btnNewAmenity" CssClass="btn btn-primary f-r create-button" OnClick="btnNewAmenity_Click" runat="server" Text="Create New Amenity" />
        </div>
    </div>
    <div class="row custom-table-responsive mb-4">
        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
            <asp:GridView CssClass="table custom-table" EmptyDataText="No Data Found." ShowHeaderWhenEmpty="true" ID="gv_Amenities" runat="server" AutoGenerateColumns="false" DataKeyNames="AmenityID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="AmenityID" Visible="false" HeaderText="AmenityID" InsertVisible="False" ReadOnly="True" SortExpression="AmenityID" />
                    <asp:BoundField DataField="Amenity" HeaderText="Amenity" SortExpression="Amenity" />
                    <asp:TemplateField HeaderText=" ">
                        <ItemTemplate>
                            <%# EditLink(Eval("AmenityID").ToString(), "EDIT")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Connection %>" SelectCommand="SELECT [Amenity], [AmenityID] FROM [AMENITIES]"></asp:SqlDataSource>
</asp:Content>
