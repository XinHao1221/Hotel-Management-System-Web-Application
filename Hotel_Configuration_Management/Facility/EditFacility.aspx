<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="EditFacility.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Facility.EditFacility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />

    <div>
        <div class="formHeader">
                Facility Form
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

        <div style="clear:both;">&nbsp;</div>

            <table style="width:100%;">
                <tr>
                    <td class="formLabel requiredFieldLabel">
                            Facility Name                 
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="formInput">
                        <asp:TextBox ID="txtFacilityName" runat="server" CssClass="inputStyle" placeholder="Name" Width="50%"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFacilityName" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
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
                    <td class="formLabel">
                            Quantity               
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="formInput">
                        <asp:TextBox ID="txtQty" runat="server" CssClass="inputStyle" placeholder="10" Width="30%" Text="0"></asp:TextBox>
                        <br />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Quantity must be a positive value!" CssClass="validatorStyle" ControlToValidate="txtQty" Type="Integer" ValidationGroup="save" Operator="GreaterThanEqual" ValueToCompare="0"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel requiredFieldLabel">
                            Price Type  
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="formInput">
                        <asp:DropDownList ID="ddlPriceType" runat="server" CssClass="dropDownStyle">
                            <asp:ListItem>-- Price Type --</asp:ListItem>
                            <asp:ListItem>Per Person</asp:ListItem>
                            <asp:ListItem>Per Night</asp:ListItem>
                            <asp:ListItem>Per Reservation</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPriceType" InitialValue="-- Price Type --" ErrorMessage="Please select an item." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel requiredFieldLabel">
                            Price               
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="formInput">
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="inputStyle" placeholder="50.00" Width="30%"></asp:TextBox>
                        <br />
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Price must be a postive decimal value!" CssClass="validatorStyle" ControlToValidate="txtPrice" Type="Double" ValidationGroup="save" Operator="GreaterThanEqual" ValueToCompare="0"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPrice" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">Status</td>
                    <td></td>
                    <td class="formInput">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropDownStyle">
                        <asp:ListItem>Active</asp:ListItem>
                        <asp:ListItem>Suspend</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>
                    
            </table>
        <div style="clear:both; width:100%; height:50px;">
            &nbsp;
        </div>
        <div class="bottomBar">

            <center>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="save" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="formBtnCancel" ToolTip="Reset"/>
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
