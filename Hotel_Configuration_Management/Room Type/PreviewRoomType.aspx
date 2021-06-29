<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewRoomType.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Room_Type.PreviewRoomType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%-- Page content --%>
            <div class="content">

                <div class="formHeader">
                    Room Type
                </div>

                <div style="margin:20px 0px 20px 50px;">
                    <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='RoomType.aspx';">

                    <img src="../../Image/home_icon.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                    <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                        Menu
                    </div>
                    </div>

                    <div class="formBtnEdit" style="float:left; height:40px; margin:0px 0px 0px 20px;" onclick="location.href='AddRoomType.aspx';">

                        <img src="../../Image/plus_white.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;" />
                        <div style="float:left; margin:12px 0px 0px 10px; font-size:15px;">
                            Add
                        </div>
                    </div>
                </div>

                <div style="height:80px;">&nbsp;</div>

                <table style="width:100%;">
                <tr>
                    <td class="formLabel">
                            Title                 
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="tableData">
                        <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">Short Code </td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblShortCode" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel requiredFieldLabel">
                            Price               
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="tableData">
                        <asp:Label ID="lblPrice" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel" style="vertical-align: top; padding-top:15px;">Description </td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel requiredFieldLabel">Base Occupancy </td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblBaseOccupancy" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel requiredFieldLabel">Higher Occupancy </td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblHigherOccupancy" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">Extra Bed</td>
                    <td></td>
                    <td class="formInput" style="padding-bottom:0px;">
                        <div style="float:left; padding-top:40px;">
                            <asp:CheckBox ID="cbExtraBed" runat="server" CssClass="formCheckBoxStyle" Width="50px" Enabled="false" Visible="false" Checked="false"/>
                        </div>
                        <div style="float:left; padding-top:5px;">
                            <asp:Image ID="IChecked" runat="server" ImageUrl="~/Image/checked_icon.png" Width="20px" Height="20px" Visible="false"/>
                        </div>
                        <div style="float:left; padding-top:20px; margin-left:-10px;">
                            <asp:Panel ID="pnExtraBedPrice" runat="server" Visible="false">
                                <asp:Label ID="lblExtraBedPrice" runat="server" Text=""></asp:Label>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>    
            </table>

            <div class="formSectionStyle" >
                2. Equipment:-
            </div>
        </div>
    </form>
</body>
</html>
