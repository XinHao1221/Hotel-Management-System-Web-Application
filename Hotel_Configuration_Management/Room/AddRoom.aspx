<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="AddRoom.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Room.AddRoom" %>
<%@ Register TagPrefix="Feature" TagName="Control" Src="~/Hotel_Configuration_Management/Room/AddFeature.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
<link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
             <%-- Page content --%>
            <div class="formHeader">
                Room Form
            </div>

            <asp:LinkButton ID="LBBack" runat="server" OnClick="LBBack_Click" CssClass="divLBStyle">
                <div style="margin:20px 0px 20px 50px;">
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
                1. Room Information:-
            </div>

            <table style="width:100%;">
                <tr>
                    <td class="formLabel requiredFieldLabel">
                            Room Number               
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="formInput">
                        <asp:TextBox ID="txtRoomNumber" runat="server" CssClass="inputStyle" placeholder="R101" Width="20%"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRoomNumber" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel requiredFieldLabel">
                            Floor Number
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="formInput">
                        <asp:DropDownList ID="ddlFloorNumber" runat="server" CssClass="dropDownStyle">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlFloorNumber" InitialValue="-- Please Select --" ErrorMessage="Please select an item." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel requiredFieldLabel">
                            Room Type
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="formInput">
                        <asp:DropDownList ID="ddlRoomType" runat="server" CssClass="dropDownStyle">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlRoomType" InitialValue="-- Please Select --" ErrorMessage="Please select an item." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">Status</td>
                    <td></td>
                    <td class="formInput">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropDownStyle">
                            <asp:ListItem>Active</asp:ListItem>
                            <asp:ListItem>Blocked</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>

            <div class="formSectionStyle" >
                2. Room's Feature:-
            </div>

            <Feature:Control ID="FC1" runat="server"></Feature:Control>

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

                <asp:Panel ID="PopupBack" runat="server" Visible="False" CssClass="popupWindow">

                    <%-- Popup Window Title --%>
                    <p style="color:red;" class="popupTitle">Leave without save?</p>

                    <%-- Popup Window Body --%>
                    <div class="popupBody">

                        <p>The changes have not been saved. Are you sure to close the editor?</p>

                    </div>

                    <div>&nbsp;</div>

                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnPopupCancel_Click"/>
                
                    <asp:Button ID="btnConfirmBack" runat="server" Text="Confirm" CssClass="popUpDeleteBtn" OnClick="btnConfirmBack_Click"/>
                </asp:Panel>

                <%-- Popup Cover --%>
                <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
                    &nbsp;
                </asp:Panel>
            </div>
        </div>

    </form>
</body>
</html>
