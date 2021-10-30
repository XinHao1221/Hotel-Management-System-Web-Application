<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"  CodeBehind="EditRegularPrice.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.PriceManager.EditRegularPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />

    <div>
            <%-- Page content --%>
        <div class="formHeader">
            Regular Price
        </div>

        <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
            <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='RegularRoomPrice.aspx';">

                <img src="../../Image/Back.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                    Back
                </div>
                        
            </div>
        </div>

        <div style="height:100px;">&nbsp;</div>
        <table style="width:100%;">
            <tr>
                <td class="formLabel requiredFieldLabel">
                        Room Type
                </td>
                <td class="tableSeperator"></td>
                <td class="formInput">
                    <asp:DropDownList ID="ddlRoomType" runat="server" CssClass="dropDownStyle" AutoPostBack="true" OnSelectedIndexChanged="ddlRoomType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlRoomType" InitialValue="-- Please Select --" ErrorMessage="Please select an item." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>

        <%--Edit Price--%>
        <div class="formContainerStyle">
            <div style="float:left; width:150px; margin-right:40px; height:100px;">
                <div class="formLabel2">
                    Mon
                </div>
                <asp:TextBox ID="txtMonPrice" runat="server" CssClass="inputStyle2" Width="91%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMonPrice" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Enter a positive decimal!" CssClass="validatorStyle" ControlToValidate="txtMonPrice" Type="Double" ValidationGroup="save" Operator="GreaterThanEqual" ValueToCompare="0"></asp:CompareValidator>
            </div>
            <div style="float:left; width:150px; margin-right:40px; height:100px;">
                <div class="formLabel2">
                    Tue
                </div>
                <asp:TextBox ID="txtTuePrice" runat="server" CssClass="inputStyle2" Width="91%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTuePrice" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Enter a positive decimal!" CssClass="validatorStyle" ControlToValidate="txtTuePrice" Type="Double" ValidationGroup="save" Operator="GreaterThanEqual" ValueToCompare="0"></asp:CompareValidator>
            </div>
            <div style="float:left; width:150px; margin-right:40px; height:100px;">
                <div class="formLabel2">
                    Wed
                </div>
                <asp:TextBox ID="txtWedPrice" runat="server" CssClass="inputStyle2" Width="91%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtWedPrice" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Enter a positive decimal!" CssClass="validatorStyle" ControlToValidate="txtWedPrice" Type="Double" ValidationGroup="save" Operator="GreaterThanEqual" ValueToCompare="0"></asp:CompareValidator>
            </div>
            <div style="float:left; width:150px; margin-right:40px; height:100px;">
                <div class="formLabel2">
                    Thu
                </div>
                <asp:TextBox ID="txtThuPrice" runat="server" CssClass="inputStyle2" Width="91%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtThuPrice" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Enter a positive decimal!" CssClass="validatorStyle" ControlToValidate="txtThuPrice" Type="Double" ValidationGroup="save" Operator="GreaterThanEqual" ValueToCompare="0"></asp:CompareValidator>
            </div>
            <div style="float:left; width:150px; margin-right:40px; height:100px;">
                <div class="formLabel2">
                    Fri
                </div>
                <asp:TextBox ID="txtFriPrice" runat="server" CssClass="inputStyle2" Width="91%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtFriPrice" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Enter a positive decimal!" CssClass="validatorStyle" ControlToValidate="txtFriPrice" Type="Double" ValidationGroup="save" Operator="GreaterThanEqual" ValueToCompare="0"></asp:CompareValidator>
            </div>
            <div style="float:left; width:150px; margin-right:40px; height:100px;">
                <div class="formLabel2">
                    Sat
                </div>
                <asp:TextBox ID="txtSatPrice" runat="server" CssClass="inputStyle2" Width="91%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSatPrice" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Enter a positive decimal!" CssClass="validatorStyle" ControlToValidate="txtSatPrice" Type="Double" ValidationGroup="save" Operator="GreaterThanEqual" ValueToCompare="0"></asp:CompareValidator>
            </div>
            <div style="float:left; width:150px; margin-right:40px; height:100px;">
                <div class="formLabel2">
                    Sun
                </div>
                <asp:TextBox ID="txtSunPrice" runat="server" CssClass="inputStyle2" Width="91%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSunPrice" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Enter a positive decimal!" CssClass="validatorStyle" ControlToValidate="txtSunPrice" Type="Double" ValidationGroup="save" Operator="GreaterThanEqual" ValueToCompare="0"></asp:CompareValidator>
            </div>

            <div style="clear:both; width:100%; height:70px;">
                &nbsp;
            </div>

            <div class="bottomBar">

                <center>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="save" />
                    &nbsp;
                </center>

            </div>
        </div>

        <%--Popup Window--%>
        <div class="popup">
            <asp:Panel ID="PopupSaved" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:#00ce1b;" class="popupTitle">Saved</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <asp:Label ID="lblPopupSavedContent" runat="server" Text="Label"></asp:Label>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnOK" runat="server" Text="OK" CssClass="popUpCancelBtn" OnClick="btnOK_Click"/>
                
                    
            </asp:Panel>
        </div>

        <%-- Popup Cover --%>
        <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
            &nbsp;
        </asp:Panel>

    </div>

</asp:Content>