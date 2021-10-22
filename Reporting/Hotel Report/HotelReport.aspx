<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="HotelReport.aspx.cs" Inherits="Hotel_Management_System.Reporting.Hotel_Report.HotelReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />
    <link rel="stylesheet" href="../../StyleSheet/MainMenuHeader.css" />


    <div>

        <%-- Page content --%>
        <div class="formHeader">
            Hotel Report
        </div>

        <table style="margin-left:2.5%; margin-top:-1%;">
            <tr>
                <td class="formLabel">Date</td>
                <td></td>
                <td class="tableData">
                    <asp:Label ID="lblReportDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>