<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="GovernorateTray.aspx.cs" Inherits="BN_HomeFinance.Admin.GovernorateTray" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row mb-4 mt-4">
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <h1>Governorates</h1>
        </div>
        <div class="col-6 col-sm-6 col-md-6 col-lg-6">
            <asp:Button ID="btnNewGovernorate" CssClass="btn btn-primary f-r create-button" OnClick="btnNewGovernorate_Click" runat="server" Text="Add New Governorate" />
        </div>
    </div>
    <div class="row custom-table-responsive mb-4">
        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
            <asp:GridView CssClass="table custom-table" ID="gv_Governorate" runat="server" AutoGenerateColumns="False" DataKeyNames="GovernorateID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="GovernorateID" HeaderText="GovernorateID" Visible="false" InsertVisible="False" ReadOnly="True" SortExpression="GovernorateID" />
                    <asp:BoundField DataField="Governorate" HeaderText="Governorate" SortExpression="Governorate" />
                    <asp:TemplateField HeaderText=" ">
                        <ItemTemplate>
                            <%# EditLink(Eval("GovernorateID").ToString(), "EDIT")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Connection %>" SelectCommand="SELECT [Governorate], [GovernorateID] FROM [GOVERNORATE]"></asp:SqlDataSource>
</asp:Content>
