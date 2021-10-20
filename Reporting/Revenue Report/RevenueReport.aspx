<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="RevenueReport.aspx.cs" Inherits="Hotel_Management_System.Reporting.Revenue_Report.RevenueReport" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">  

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />
    <link rel="stylesheet" href="../../StyleSheet/MainMenuHeader.css" />

    <style>
        .divReservationDetails{
            float:left;
            width:68%;
            min-height:600px;
        }

        .divPaymentDetails{
            float:left;
            width:30%;
            height:100%;
            min-height:600px;
            border: 2px solid rgb(180 180 180);
            box-shadow: 5px 10px;
            box-shadow: 3px 3px 5px rgb(149 149 149);
        }

        body{
            height:100%;
        }

        .formBtnSave{
            margin-left:15px;
        }

    </style>

    <div>

        <%-- Page content --%>
        <div class="formHeader">
            Revenue Report
        </div>

        <div>
            <%--Display Reservation Details--%>
            <div class="divReservationDetails">

                <%--Filtering--%>
                <div style="">
                    <div style="float:left; margin-left:5%;">
                        <div class="filteringLabel">
                            Type
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlReportType" runat="server" OnTextChanged="ddlReportType_TextChanged" CssClass="filteringDropDown" AutoPostBack="true">
                                <asp:ListItem>Daily</asp:ListItem>
                                <asp:ListItem>Monthly</asp:ListItem>
                                <asp:ListItem>Yearly</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="PNDateFilter" runat="server" Visible="true">
                    <div style="">
                        <div style="float:left; margin-left:5%;">
                            <div class="filteringLabel">
                                Date
                            </div>
                            <div>
                                <asp:TextBox ID="txtDate" runat="server" type="date" OnTextChanged="txtDate_TextChanged" AutoPostBack="true" CssClass="filteringDropDown"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="PNMonthFilter" runat="server" Visible="false">
                    <div style="">
                        <div style="float:left; margin-left:5%;">
                            <div class="filteringLabel">
                                Month
                            </div>
                            <div>
                                <asp:TextBox ID="txtYearMonth" runat="server" type="month" OnTextChanged="txtYearMonth_TextChanged" AutoPostBack="true" CssClass="filteringDropDown"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="PNYearFilter" runat="server" Visible="false">
                    <div style="">
                        <div style="float:left; margin-left:5%;">
                            <div class="filteringLabel">
                                Year
                            </div>
                            <div>
                                <asp:DropDownList ID="ddlYear" runat="server" OnTextChanged="ddlYear_TextChanged" CssClass="filteringDropDown" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div style="clear:both; height:50px;"></div>

                <div style="font-family: Helvetica, sans-serif; font-size:120%; font-weight:bold; margin-left:5%; margin-bottom:-40px;">
                    Profit
                </div>
                

                <div style="text-align:center;">
                    <asp:Chart ID="ChartProfit" runat="server" OnCustomize="ChartGuestInHouse_Customize" Width="500px" Height="500px">
                        <Series>
                            <asp:Series Name="Series1"></asp:Series> 
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="false" Name="Defualt" LegendStyle="Row" />
                        </Legends>
                    </asp:Chart>
                </div>
                

            </div>

            <%--Display and record payment details--%>
            <div class="divPaymentDetails">

                <div style="height:20px;">&nbsp;</div>
                <table style="width:90%; margin:auto;">
                    <tr>
                        <td style="width:50%; text-align:left;" class="formLabel">Room</td>
                        <td class="tableData" style="text-align:right;">
                            RM
                            <asp:Label ID="lblRoomTotal" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:50%; text-align:left;" class="formLabel">Facility</td>
                        <td class="tableData" style="text-align:right;">
                            RM
                            <asp:Label ID="lblFacilityTotal" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:50%; text-align:left;" class="formLabel">Fine Charges</td>
                        <td class="tableData" style="text-align:right;">
                            RM
                            <asp:Label ID="lblFineChargesTotal" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:50%; text-align:left;" class="formLabel">Services</td>
                        <td class="tableData" style="text-align:right;">
                            RM
                            <asp:Label ID="lblServicesTotal" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:50%; text-align:left; font-size:20px;" class="formLabel requiredFieldLabel">PROFIT</td>
                        <td class="tableData requiredFieldLabel" style="text-align:right; font-size:20px; font-weight:600;">
                            RM
                            <asp:Label ID="lblProfit" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                </table>             

            </div>

        </div>

        <div style="clear:both; width:100%; height:70px;">
            &nbsp;
        </div>

    </div>

</asp:Content>
