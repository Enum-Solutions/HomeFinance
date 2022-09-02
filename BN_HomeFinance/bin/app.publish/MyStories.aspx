<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MyStories.aspx.cs" Inherits="BN_HomeFinance.MyStories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <div class="row mb-4 mt-4">
            <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                <h1>My Stories</h1>
            </div>
            <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                <asp:button id="btnNewStory" cssclass="btn btn-primary f-r create-button" onclick="btnNewStory_Click" runat="server" text="Add New Story" />
            </div>
        </div>
        <div class="row custom-table-responsive mb-4">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                <asp:gridview cssclass="table custom-table" id="gv_Stories" runat="server" emptydatatext="No Data Found." showheaderwhenempty="true" autogeneratecolumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Header">
                        <ItemTemplate>
                            <%# EditLink(Eval("ID").ToString(), Eval("Header").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Created At" DataField="DateCreated" />
                    <asp:BoundField HeaderText="Created By" DataField="FullName" />
                    <asp:BoundField HeaderText="Status" DataField="Status" />
                </Columns>
            </asp:gridview>
            </div>
        </div>
    </div>
</asp:Content>
