<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Reservation.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />

    <style>
        .divReservationDetails{
            float:left;
            width:70%;
            background-color:aqua;
            min-height:600px;
        }

        .divPaymentDetails{
            float:left;
            width:30%;
            height:100%;
            background-color:red;
            min-height:600px;
        }

        body{
            height:100%;
        }
    </style>

    <div>
        <a href='javascript:history.go(-1)'>Go back</a>

        <div>
            <div class="divReservationDetails">
                <div style="width:50%; float:left;">
                    <table style="width:100%;">
                        <tr>
                            <td class="formLabel requiredFieldLabel">
                                    Guest Name             
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:Label ID="lblGuestName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formLabel requiredFieldLabel">
                                    Check In           
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:Label ID="lblCheckInDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width:50%; float:left;">
                    <table style="width:100%;">
                        <tr>
                            <td class="formLabel requiredFieldLabel">
                                    Duration of Stay             
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:Label ID="lblDurationOfStay" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formLabel requiredFieldLabel">
                                    Check Out           
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:Label ID="lblCheckOutDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                
                &nbsp;
            </div>
            <div class="divPaymentDetails">
                &nbsp;
            </div>
        </div>
    </div>

</asp:Content>



        

