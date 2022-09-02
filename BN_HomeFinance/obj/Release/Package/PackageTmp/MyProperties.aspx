<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MyProperties.aspx.cs" Inherits="BN_HomeFinance.MyProperties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <div class="row mb-4 mt-4">
            <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                <h1>My Properties</h1>
            </div>
            <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                <asp:button id="btnProperty" cssclass="btn btn-primary f-r create-button" onclick="btnProperty_Click" runat="server" text="Add New Property" />
            </div>
        </div>
        <div class="row custom-table-responsive mb-4">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <asp:gridview cssclass="table custom-table" OnRowDataBound="gv_Properties_RowDataBound" emptydatatext="No Data Found." showheaderwhenempty="true" id="gv_Properties" runat="server" autogeneratecolumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Title">
                        <ItemTemplate>
                            <%# EditLink(Eval("PropertyID").ToString(), Eval("PropertyName").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Address" DataField="PropertyAddress" />
                    <asp:BoundField HeaderText="Property Value" DataField="PropertyValue" />
                    <asp:BoundField HeaderText="Type" DataField="PropertyType" />
                    <asp:BoundField HeaderText="Governorate" DataField="Governorate" />
                    <asp:BoundField HeaderText="Wilayat" DataField="Wilayat" />
                </Columns>
            </asp:gridview>
            </div>
        </div>
    </div>
</asp:Content>
