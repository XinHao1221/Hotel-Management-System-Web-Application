﻿<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"  CodeBehind="EditFloor.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Floor.EditFloor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />


    <div>
        <div class="formHeader">
                Floor Form
        </div>
        <asp:LinkButton ID="LBBack" runat="server" CssClass="divLBStyle" OnClick="LBBack_Click">
            <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
                <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='Floor.aspx';">

                    <img src="../../Image/Back.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                    <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                        Back
                    </div>
                        
                </div>
            </div>
        </asp:LinkButton>
        <table style="width:100%;">
            <tr>
                <td class="formLabel requiredFieldLabel">
                        Floor Name                 
                </td>
                <td class="tableSeperator"></td>
                <td>
                    <asp:TextBox ID="txtFloorName" runat="server" CssClass="inputStyle" placeholder="Name" Width="50%"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFloorName" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="formLabel">Floor Number </td>
                <td></td>
                <td>
                    <asp:DropDownList ID="ddlFloorNumber" runat="server" CssClass="dropDownStyle" Width="10%"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="formLabel" style="vertical-align: top; padding-top:15px;">Description </td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" Rows="8" TextMode="MultiLine" Width="50%" CssClass="inputMultiLineTxtBox" placeholder="Type floor description here..."></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="formLabel">Status</td>
                <td></td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropDownStyle">
                    <asp:ListItem>Active</asp:ListItem>
                    <asp:ListItem>Suspend</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
        
        </table>

        <div class="bottomBar">

            <center>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="save" />
                <asp:Button ID="formBtnCancel" runat="server" Text="Reset" OnClick="formBtnCancel_Click" CssClass="formBtnCancel" ToolTip="Reset"/>
            </center>
                    
        </div>

        <%-- Popup Window --%>
        <div class="popup">
            <asp:Panel ID="PopupCancel" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:red;" class="popupTitle">Reset Text Field?</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <p>All text fields will be reset!</p>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnPopupCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnPopupCancel_Click"/>
                
                <asp:Button ID="btnPopupConfirm" runat="server" Text="Confirm" CssClass="popUpDeleteBtn" OnClick="btnPopupConfirm_Click"/>
            </asp:Panel>

            <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
                &nbsp;
            </asp:Panel>
        </div>

    </div>
</asp:Content>
