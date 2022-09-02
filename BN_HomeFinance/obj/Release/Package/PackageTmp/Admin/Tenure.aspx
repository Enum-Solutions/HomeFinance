<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="Tenure.aspx.cs" Inherits="BN_HomeFinance.Admin.Tenure" %>

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
                        <h3 class="text-right">Tenure</h3>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblTenure" runat="server" Text="Tenure"></asp:Label>
                        <asp:TextBox ID="txtTenure" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFQImage" ControlToValidate="txtTenure" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Required Field."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 mt-4 t-c">
                        <asp:Button Text="Add" ID="BtnAdd" CssClass="btn btn-primary" runat="server" OnClick="BtnAdd_Click" />
                        <asp:Button Text="Delete" ID="BtnDelete" CssClass="btn btn-primary" ValidationGroup="Val" runat="server" OnClick="BtnDelete_Click" />
                        <asp:Button Text="Update" ID="BtnUpdate" CssClass="btn btn-Secondary" ValidationGroup="Val" runat="server" OnClick="BtnUpdate_Click" />
                        <asp:Button Text="Cancel" ID="btnCancel"  CssClass="btn btn-Secondary" runat="server" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
            <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
        </div>
    </div>
</asp:Content>
