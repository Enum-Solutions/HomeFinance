<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MyPromotions.aspx.cs" Inherits="BN_HomeFinance.MyPromotions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <div class="row mb-4 mt-4">
            <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                <h1>My Promotions</h1>
            </div>
            <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                <asp:Button ID="btnNewPromotion" CssClass="btn btn-primary f-r create-button" OnClick="btnNewPromotion_Click" runat="server" Text="Add New Promotion" />
            </div>
        </div>
        <div class="row custom-table-responsive mb-4">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">

                <asp:GridView CssClass="table custom-table" EmptyDataText="No Data Found." ShowHeaderWhenEmpty="true" ID="gv_Promotions" runat="server" AutoGenerateColumns="false">
                    <Columns>
                     
                        <asp:TemplateField HeaderText="Header">
                            <ItemTemplate>
                                <%# EditLink(Eval("ID").ToString(), Eval("Header").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Created Date" DataField="DateCreated" DataFormatString="{0:MMMM, dd yyyy}" />
                        <asp:BoundField HeaderText="Created By" DataField="FullName" />
                        <asp:BoundField HeaderText="Status" DataField="Status" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
