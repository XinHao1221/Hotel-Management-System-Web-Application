<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="EditSpecialPrice.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.PriceManager.EditSpecialPrice" %>

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
                Special Price
            </div>

            <div style="margin:20px 0px 20px 50px;">
                <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='Floor.aspx';">

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
                            Event Name               
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="formInput">
                        <asp:TextBox ID="txtEventName" runat="server" CssClass="inputStyle" placeholder="Christmas" Width="50%"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEventName" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel requiredFieldLabel">
                            Date              
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="formInput">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="inputStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate" ErrorMessage="Please select a date." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>

            <div class="formContainerStyle">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <div style="float:left; width:320px; margin-right:40px; height:100px; margin-right:100px;">
                            <div style="float:left; width:200px; margin-top:12.5px;" class="formLabel">
                                <asp:Label ID="lblRoonTypeID" runat="server" Text='<%# Eval("RoomTypeID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblRoomType" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </div>
                            <div style="width:20px; float:left;">
                                &nbsp;
                            </div>
                            <div style="float:left; width:100px;">
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="inputStyle" Width="100%" Text='<%# Eval("Price") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrice" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Enter a decimal!" CssClass="validatorStyle" ControlToValidate="txtPrice" Type="Double" ValidationGroup="save" Operator="DataTypeCheck"></asp:CompareValidator>
                            </div>
                        </div>
                    </ItemTemplate>

                </asp:Repeater>

                <asp:Repeater ID="Repeater2" runat="server">
                    <ItemTemplate>
                        <div style="float:left; width:320px; margin-right:40px; height:100px; margin-right:100px;">
                            <div style="float:left; width:200px; margin-top:12.5px;" class="formLabel">
                                <asp:Label ID="lblRoonTypeID" runat="server" Text='<%# Eval("RoomTypeID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblRoomType" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </div>
                            <div style="width:20px; float:left;">
                                &nbsp;
                            </div>
                            <div style="float:left; width:100px;">
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="inputStyle" Width="100%" Text='<%# Eval("Price") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrice" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Enter a decimal!" CssClass="validatorStyle" ControlToValidate="txtPrice" Type="Double" ValidationGroup="save" Operator="DataTypeCheck"></asp:CompareValidator>
                            </div>
                        </div>
                    </ItemTemplate>

                </asp:Repeater>
                

            <div style="clear:both; width:100%; height:70px;">
                &nbsp;
            </div>

        </div>

            <div class="bottomBar">

                <center>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="save" />
                    &nbsp;
                </center>

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
    </form>
</body>
</html>
