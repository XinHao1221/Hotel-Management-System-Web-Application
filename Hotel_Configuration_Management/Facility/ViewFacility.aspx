<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="ViewFacility.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Facility.ViewFacility" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="formHeader">
                Facility
            </div>

            <div style="margin:20px 0px 20px 50px;">
                <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='Facility.aspx';">

                    <img src="../../Image/home_icon.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                    <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                        Menu
                    </div>
                        
                </div>

                <asp:LinkButton ID="LBEdit" runat="server" CssClass="divLBStyle" OnClick="LBEdit_Click">
                    <div class="formBtnEdit" style="float:left; height:40px; margin:0px 0px 0px 20px;">

                        <img src="../../Image/edit_white_icon.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;" />
                        <div style="float:left; margin:12px 0px 0px 10px; font-size:15px;">
                            Edit
                        </div>
                    </div>
                </asp:LinkButton>
                
            </div>

            <div style="clear:both;">&nbsp;</div>

            <table style="width:100%;">
                <tr>
                    <td class="formLabel">
                            Facility Name                 
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="tableData">
                        <asp:Label ID="lblFacilityName" runat="server" Text="Label"></asp:Label>
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
                    <td class="formLabel">
                            Quantity               
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="tableData">
                        <asp:Label ID="lblQty" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel requiredFieldLabel">
                            Price Type  
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="tableData">
                        <asp:Label ID="lblPriceType" runat="server" Text="Label"></asp:Label>
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
                    <td class="formLabel">Status</td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                    
            </table>

        </div>
    </form>
</body>
</html>
