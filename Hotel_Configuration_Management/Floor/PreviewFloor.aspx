<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewFloor.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Floor.ViewFloor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%-- Page content --%>
            <div class="content">

                <div style="margin:20px 0px 20px 50px;">
                    <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='Floor.aspx';">

                        <img src="../../Image/Back.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                        <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                            Menu
                        </div>
                        
                    </div>

                    <div class="formBtnEdit" style="float:left; height:40px; margin:0px 0px 0px 20px;" onclick="location.href='AddFloor.aspx';">

                        <img src="../../Image/plus_white.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;" />
                        <div style="float:left; margin:12px 0px 0px 10px; font-size:15px;">
                            Add
                        </div>
                    </div>
                </div>

                <div style="height:80px;">&nbsp;</div>

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
        </div>
    </form>
</body>
</html>
