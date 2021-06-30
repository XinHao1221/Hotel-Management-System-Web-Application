<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRoom.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Room.EditRoom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%-- Page content --%>
            <div class="formHeader">
                Room Type Form
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

        </div>
    </form>
</body>
</html>
