<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Admin.Master" AutoEventWireup="true" CodeBehind="Amenity.aspx.cs" Inherits="BN_HomeFinance.Admin.Amenity" %>

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
                        <h3 class="text-right">Amenity</h3>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblHeader" runat="server" Text="Amenity"></asp:Label><label class="mandatory"> *</label>
                        <asp:TextBox ID="txtAmenity" CssClass="form-control" placeholder="Amenity Name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFQHeader" ControlToValidate="txtAmenity" ValidationGroup="Val" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 t-c mt-2">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary" ValidationGroup="Val" OnClick="btnSubmit_Click" runat="server" Text="Submit" />
                        <asp:Button ID="btnUpdate" CssClass="btn btn-primary" Visible="false" ValidationGroup="Val" OnClick="btnUpdate_Click" runat="server" Text="Update" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" runat="server" Text="Cancel" />
                        <asp:Button ID="btnDelete" CssClass="btn btn-secondary" Visible="false" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you certain you want to delete this Amenity?');" runat="server" Text="Delete" />
                    </div>
                </div>
            </div>
            <div class="col-0 col-sm-0 col-md-1 col-lg-1 border-right bg-gray"></div>
        </div>
    </div>
</asp:Content>
