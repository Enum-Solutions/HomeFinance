<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="Wilayat.aspx.cs" Inherits="BN_HomeFinance.Admin.Wilayat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background: var(--purple);
        }

        .form-control:focus {
            box-shadow: none;
            border-color: #BA68C8
        }

        .profile-button {
            background: rgb(99, 39, 120);
            box-shadow: none;
            border: none
        }

            .profile-button:hover {
                background: #682773
            }

            .profile-button:focus {
                background: #682773;
                box-shadow: none
            }

            .profile-button:active {
                background: #682773;
                box-shadow: none
            }

        .back:hover {
            color: #682773;
            cursor: pointer
        }

        .labels {
            font-size: 11px
        }

        .add-experience:hover {
            background: #BA68C8;
            color: #fff;
            cursor: pointer;
            border: solid 1px #BA68C8
        }

        span {
            font-size: 0.7rem;
        }

        .border-right {
            border-right: solid 1px var(--gray);
        }

        .carousel-item {
            margin-right: 0% !important;
        }

        #imgUploader:hover {
            cursor: pointer !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container rounded bg-white">
        <div class="row">
            <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
            <div class="col-12 col-sm-12 col-md-10 col-lg-10 border-right">
                <div class="p-3 py-5 mb-3">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h3 class="text-right">Wilayat</h3>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblGovernorate" runat="server">Governorate<span style="color:red">*</span></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlGovernorate" runat="server" CssClass="form-control"
                            DataTextField="Governorate" DataValueField="GovernorateID">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ValidationGroup="validateControls" runat="server" ControlToValidate="ddlGovernorate" ErrorMessage="Required Field" ForeColor="Red" InitialValue="0"  Display="Dynamic" />
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblWilayat" runat="server" Text="Wilayat"></asp:Label>
                        <asp:TextBox ID="txtWilayat" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="validateControls" runat="server" ControlToValidate="txtWilayat" ErrorMessage="Required Field" ForeColor="Red" Display="Dynamic" />
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-4">
                        <asp:Button Text="Add" ID="BtnAdd" CssClass="btn btn-primary" ValidationGroup="validateControls" runat="server" OnClick="BtnAdd_Click" />
                        <asp:Button Text="Update" ID="BtnUpdate" CssClass="btn btn-primary" runat="server" ValidationGroup="validateControls" OnClick="BtnUpdate_Click" />
                        <asp:Button Text="Delete" ID="BtnDelete" CssClass="btn btn-secondary" runat="server" OnClick="BtnDelete_Click" OnClientClick="return confirm('Are you certain you want to delete this Wilayat?');"/>
                        <asp:Button Text="Cancel" ID="btnCancel" CssClass="btn btn-Secondary" runat="server" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
            <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
        </div>
    </div>
</asp:Content>
