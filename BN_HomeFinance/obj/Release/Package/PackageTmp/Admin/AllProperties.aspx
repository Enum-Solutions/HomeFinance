<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="AllProperties.aspx.cs" Inherits="BN_HomeFinance.Admin.AllProperties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row mb-4 mt-4">
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <h1>All Properties</h1>
        </div>
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <asp:Button ID="btnNewProperty" OnClick="btnNewProperty_Click" CssClass="btn btn-primary f-r create-button" runat="server" Text="Create New Property" />
        </div>
    </div>
    <div class="row custom-table-responsive mb-4">
        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
            <asp:GridView OnRowDataBound="gv_Properties_RowDataBound" CssClass="table custom-table" EmptyDataText="No Data Found." ShowHeaderWhenEmpty="true" ID="gv_Properties" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Header">
                        <ItemTemplate>
                            <%# EditLink(Eval("PropertyID").ToString(), Eval("PropertyName").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Property Value" DataField="PropertyValue" />
                    <asp:BoundField HeaderText="Type" DataField="PropertyType" />
                    <asp:BoundField HeaderText="Governorate" DataField="Governorate" />
                    <asp:BoundField HeaderText="Wilayat" DataField="Wilayat" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
