<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddFacility.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Facility.AddFacility" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%-- Page content --%>
            <div class="formHeader">
                    Facility Form
                </div>

                <div style="margin:20px 0px 20px 50px;">
                    <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='Floor.aspx';">

                        <img src="../../Image/Back.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                        <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                            Back
                        </div>
                        
                    </div>
                </div>

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
                            <asp:TextBox ID="txtQty" runat="server" CssClass="inputStyle" placeholder="'0' = infinity" Width="30%" Text="0"></asp:TextBox>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Quantity must be a digit!" CssClass="validatorStyle" ControlToValidate="txtQty" Type="Integer" ValidationGroup="save" Operator="DataTypeCheck"></asp:CompareValidator>
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPriceType" InitialValue="-- Price Type --" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
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
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Price must be in decimal!" CssClass="validatorStyle" ControlToValidate="txtPrice" Type="Double" ValidationGroup="save" Operator="DataTypeCheck"></asp:CompareValidator>
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
                        <asp:Button ID="formBtnCancel" runat="server" Text="Reset" OnClick="formBtnCancel_Click" CssClass="formBtnCancel" ToolTip="Reset"/>
                    </center>
                        
                    
                </div>
            </div>
        
    </form>
</body>
</html>
