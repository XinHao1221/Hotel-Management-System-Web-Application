﻿<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="EditRoomType.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Room_Type.EditRoomType" %>
<%@ Register TagPrefix="Equipment" TagName="Control" Src="~/Hotel_Configuration_Management/RoomType/EditEquipment.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />

    <div>
        <%-- Page content --%>
        <div class="formHeader">
            Room Type Form
        </div>

        <asp:LinkButton ID="LBBack" runat="server" OnClick="LBBack_Click" CssClass="divLBStyle">
            <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
                <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='Floor.aspx';">

                    <img src="../../Image/Back.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                    <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                        Back
                    </div>
                        
                </div>
            </div>
        </asp:LinkButton>
            

        <div style="clear:both;">&nbsp;</div>

        <%--Form Section--%>
        <div class="formSectionStyle" >
            1. Room Details:-
        </div>

        <table style="width:100%;">
            <tr>
                <td class="formLabel requiredFieldLabel">
                        Title                 
                </td>
                <td class="tableSeperator"></td>
                <td class="formInput">
                    <asp:TextBox ID="txtTittle" runat="server" CssClass="inputStyle" placeholder="Name" Width="50%"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTittle" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="formLabel">Short Code </td>
                <td></td>
                <td class="formInput">
                    <asp:TextBox ID="txtShortCode" runat="server" CssClass="inputStyle" placeholder="SC" Width="30%" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formLabel">
                        Price               
                </td>
                <td class="tableSeperator"></td>
                <td class="tableData" style="text-align:left;">
                    <asp:LinkButton ID="LBPriceManager" runat="server" OnClick="LBPriceManager_Click">manage price</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="formLabel" style="vertical-align: top; padding-top:15px;">Description </td>
                <td></td>
                <td class="formInput">
                    <asp:TextBox ID="txtDescription" runat="server" Rows="8" TextMode="MultiLine" Width="50%" CssClass="inputMultiLineTxtBox" placeholder="Type floor description here..."></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formLabel requiredFieldLabel">Base Occupancy </td>
                <td></td>
                <td class="formInput">
                    <asp:TextBox ID="txtBaseOccupancy" runat="server" CssClass="inputStyle" placeholder="2" Width="30%" ></asp:TextBox> <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter a postive digit!" CssClass="validatorStyle" ControlToValidate="txtBaseOccupancy" Type="Integer" ValidationGroup="save" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBaseOccupancy" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="formLabel requiredFieldLabel">Higher Occupancy </td>
                <td></td>
                <td class="formInput">
                    <asp:TextBox ID="txtHigherOccupancy" runat="server" CssClass="inputStyle" placeholder="4" Width="30%" ></asp:TextBox> <br />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter a postive digit!" CssClass="validatorStyle" ControlToValidate="txtHigherOccupancy" Type="Integer" ValidationGroup="save" Operator="GreaterThanEqual" ValueToCompare="0"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHigherOccupancy" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Higher occupancy should not less than base occupancy." ControlToCompare="txtBaseOccupancy" Type="String" Operator="GreaterThanEqual" ControlToValidate="txtHigherOccupancy" CssClass="validatorStyle" ValidationGroup="save"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="formLabel">Extra Bed</td>
                <td></td>
                <td class="formInput" style="padding-bottom:0px;">
                        

                    <div style="float:left; padding-top:40px;">
                        <asp:CheckBox ID="cbExtraBed" runat="server" AutoPostBack="true" OnCheckedChanged="cbExtraBed_CheckedChanged" CssClass="formCheckBoxStyle" Width="50px"/>
                       
                    </div>
                    <div style="float:left; padding-top:20px; margin-left:-10px;">
                        <asp:Panel ID="pnExtraBedPrice" runat="server" Visible="false">
                            <asp:TextBox ID="txtExtraBedPrice" runat="server" CssClass="inputStyle" placeholder="Price" Width="50%"></asp:TextBox>
                            <br />
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Price must be in decimal!" CssClass="validatorStyle" ControlToValidate="txtExtraBedPrice" Type="Double" ValidationGroup="save" Operator="DataTypeCheck"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RFVExtraBedPrice" runat="server" ControlToValidate="txtExtraBedPrice" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle" Enabled="false"></asp:RequiredFieldValidator>
                        </asp:Panel>
                    </div>
                        
                </td>
            </tr>    
        </table>

        <div class="formSectionStyle" >
            2. Equipment:-
        </div>

        <Equipment:Control ID="EC1" runat="server"></Equipment:Control>

        <div style="clear:both; width:100%; height:70px;">
            &nbsp;
        </div>

        <div class="bottomBar">

            <center>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="save" />
                <asp:Button ID="formBtnCancel" runat="server" Text="Reset" OnClick="formBtnCancel_Click" CssClass="formBtnCancel" ToolTip="Reset"/>
            </center>
                    
        </div>

        <%-- Popup Window --%>
        <div class="popup">
            <asp:Panel ID="PopupReset" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:red;" class="popupTitle">Reset Text Field?</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <p>All text fields will be reset!</p>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnPopupCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnPopupCancel_Click"/>
                
                <asp:Button ID="btnPopupConfirmReset" runat="server" Text="Confirm" CssClass="popUpDeleteBtn" OnClick="btnPopupConfirmReset_Click"/>
            </asp:Panel>

            <%-- Popup Cover --%>
            <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
                &nbsp;
            </asp:Panel>

        </div>

    </div>
</asp:Content>
