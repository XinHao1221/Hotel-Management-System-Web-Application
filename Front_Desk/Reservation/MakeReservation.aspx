<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="MakeReservation.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Reservation.MakeReservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <%--Datalist Plugin--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="stylesheet" />

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />

    <style>
        .guestListDropDown{
            border:none;
            border-bottom:2px solid rgb(128, 128, 128);
            padding:0 7px 0 7px;
            height:30px;
            font-size:90%;
            width:350px;
            height:60px;
        }
    </style>

    

    <div>
        <%-- Page content --%>
        <div class="formHeader">
            Reservation Form
        </div>

        <asp:LinkButton ID="LBBack" runat="server" OnClick="LBBack_Click" CssClass="divLBStyle">
            <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
                <div class="formBackBtn" style="float:left;" tooltip="save">

                    <img src="../../Image/Back.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                    <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                        Back
                    </div>
                        
                </div>
            </div>
        </asp:LinkButton>
            
        <div style="clear:both; height:50px;">&nbsp;</div>

        <%--Form Section--%>
        <table style="width:100%;">
            <tr>
                <td class="formLabel requiredFieldLabel">
                        Guest Name             
                </td>
                <td class="tableSeperator"></td>
                <td class="formInput">
                    <asp:DropDownList ID="ddlGuest" runat="server" CssClass="guestListDropDown" AutoPostBack="True" OnSelectedIndexChanged="ddlGuest_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:LinkButton ID="LBAddGuest" runat="server" CssClass="tableData" OnClick="LBAddGuest_Click">add guest</asp:LinkButton>
                    
                </td>
            </tr>
        </table>

        <asp:Panel ID="PNStayDetails" runat="server" Visible="false">

            <%--Section Label--%>
            <div style="height:50px;">&nbsp;</div>

            <div class="formSectionStyle" style="margin-bottom:25px;">
                1. Stay Details
            </div>

            <%--Display guest details--%>
            <div style="width:90%; margin:auto;">
                <div style="width:45%; float:left;">

                    <table style="width:100%;">
                        <tr>
                            <td class="formLabel">
                                    Guest Name              
                            </td>
                            <td class="tableSeperator" style="width:5%;"></td>
                            <td class="tableData">
                                <asp:Label ID="lblGuestName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formLabel requiredFieldLabel">
                                    Check-In     
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:TextBox ID="txtCheckInDate" runat="server" CssClass="inputStyle inputDateStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtCheckInDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        
                    </table>

                </div>
                <div style="width:55%; float:left;">

                    <table style="width:90%;">
                        <tr>
                            <td class="formLabel requiredFieldLabel" style="width:10%;">
                                    IDNo          
                            </td>
                            <td class="tableSeperator" style="width:5%;"></td>
                            <td class="tableData" style="width:85%;">
                                <asp:Label ID="lblIDNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formLabel">
                                    Check-Out     
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:TextBox ID="txtCheckOutDate" runat="server" CssClass="inputStyle inputDateStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtCheckOutDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:Label ID="lblDurationOfStay" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Check-Out Date must be greater than Check-In date" ControlToCompare="txtCheckInDate" ControlToValidate="txtCheckOutDate" Operator="GreaterThan" Type="Date" EnableClientScript="False" CssClass="validatorStyle" ValidationGroup="save"></asp:CompareValidator>
                            </td>
                        </tr>
                    </table>

                </div> 
            </div>
        </asp:Panel>

        <div style="clear:both;">&nbsp;</div>

        <asp:Panel ID="PNReserveRoom" runat="server" Visible="false">

            <div class="formSectionStyle" style="margin-bottom:25px">
                2. Reserve Room:-
            </div>

        </asp:Panel>

        <div class="bottomBar">

            <center>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="save" />
                <asp:Button ID="formBtnCancel" runat="server" Text="Reset" OnClick="formBtnCancel_Click" CssClass="formBtnCancel" ToolTip="Reset"/>
            </center>
                    
        </div>

        <div style="clear:both; width:100%; height:70px;">
            &nbsp;
        </div>

    </div>

    <%--Java Script for data list--%>
    <script>
            $('#<%=ddlGuest.ClientID%>').chosen();
    </script>

</asp:Content>
