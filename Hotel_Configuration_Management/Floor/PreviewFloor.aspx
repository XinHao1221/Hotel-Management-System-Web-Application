<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="PreviewFloor.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Floor.ViewFloor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />

    <div>
        <%-- Page content --%>
        <div class="content">

            <div class="formHeader">
                Floor
            </div>

            <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
                <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='Floor.aspx';">

                <img src="../../Image/home_icon.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
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
</asp:Content>
