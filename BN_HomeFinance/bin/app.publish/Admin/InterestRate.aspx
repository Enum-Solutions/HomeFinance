<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="InterestRate.aspx.cs" Inherits="BN_HomeFinance.Admin.InterestRate" %>

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
                        <h3 class="text-right">Profit Rate</h3>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblIRFrom" runat="server" Text="Tenure From"></asp:Label><label class="mandatory">*</label>
                        <asp:DropDownList ID="ddlTenureFrom" CssClass="form-select" DataTextField="Tenure" DataValueField="TenureValue" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="ddlTenureFrom" InitialValue="0" Display="Dynamic" ValidationGroup="Val" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblIRTo" runat="server" Text="Tenure To"></asp:Label><label class="mandatory">*</label>
                        <asp:DropDownList ID="ddlTenureTo" CssClass="form-select" DataTextField="Tenure" DataValueField="TenureValue" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="ddlTenureTo" InitialValue="0" Display="Dynamic" ValidationGroup="Val" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblIR" runat="server" Text="Profit Rate"></asp:Label><label class="mandatory">*</label>
                        <asp:TextBox ID="txtIR" TextMode="Number" step="any" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="txtIR" Display="Dynamic" ValidationGroup="Val" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label><label class="mandatory">*</label>
                        <asp:DropDownList ID="ddlCategory" DataTextField="PropertyType" DataValueField="PropertyTypeID" CssClass="form-select" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFQCategory" ForeColor="Red" ControlToValidate="ddlCategory" InitialValue="0" Display="Dynamic" ValidationGroup="Val" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-2">
                        <asp:Button Text="Add" CssClass="btn btn-primary" ValidationGroup="Val" ID="BtnAdd" runat="server" OnClick="AddInterestRate_Click" />
                        <asp:Button Text="Update" CssClass="btn btn-primary" Visible="false" ValidationGroup="Val" ID="BtnUpdate" runat="server" OnClick="UpdateInterestRate_Click" />
                        <asp:Button Text="Delete" CssClass="btn btn-Secondary" ValidationGroup="Val" ID="BtnDelete" runat="server" OnClick="DeleteInterestRate_Click" />
                        <asp:Button Text="Cancel" CssClass="btn btn-Secondary" ValidationGroup="Val" ID="btnCancel" runat="server" OnClick="Cancel_Click" />
                    </div>
                     <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-2">
                         <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible ="false" Text="*"></asp:Label>
                     </div>
                </div>
            </div>
            <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
        </div>
    </div>
</asp:Content>
