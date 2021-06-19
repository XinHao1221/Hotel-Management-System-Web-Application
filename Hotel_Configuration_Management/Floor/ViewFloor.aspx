<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewFloor.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Floor.ViewFloor1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />

</head>

<body>
    <form id="form1" runat="server">
        <div>
            <div style="margin:20px 0px 20px 50px;">
                <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='Floor.aspx';">

                    <img src="../../Image/Back.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                    <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                        Back
                    </div>
                        
                </div>

                <div class="formBtnEdit" style="float:left; height:40px; margin:0px 0px 0px 20px;" onclick="location.href='EditFloor.aspx';">

                    <img src="../../Image/edit_white_icon.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;" />
                    <div style="float:left; margin:12px 0px 0px 10px; font-size:15px;">
                        Edit
                    </div>
                </div>
            </div>

            <div style="clear:both;">&nbsp;</div>

            <table style="width:80%; table-layout:fixed;">
                <tr>
                    <td class="formLabel">
                        Floor Name
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="tableData">
                        <asp:Label ID="lblFloorName" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">Floor Number </td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblFloorNumber" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel" style="vertical-align:top; padding-top:30px;">Description </td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblDescription" runat="server" Text="-"></asp:Label>
                            
                    </td>
                </tr>

                <tr>
                    <td class="formLabel">Status</td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblStatus" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                
            </table>
        </div>
    </form>
</body>
</html>
