<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ReservationReport.aspx.cs" Inherits="Hotel_Management_System.Reporting.Reservation_Report.ReservationReport" %>

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
            min-height:500px;
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
            Reservation Report
        </div>

        <div>
            <%--Filtering--%>
            <div style="">
                <div style="float:left; margin-left:3%;">
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
                    <div style="float:left; margin-left:3%;">
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
                    <div style="float:left; margin-left:3%;">
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
                    <div style="float:left; margin-left:3%;">
                        <div class="filteringLabel">
                            Year
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlYear" runat="server" OnTextChanged="ddlYear_TextChanged" CssClass="filteringDropDown" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div style="clear:both; height:20px;"></div>

            <%--Reservation Report--%>
            <asp:Panel ID="PNReservationReportDetails" runat="server" Visible="false">
                <%--Room Type--%>
                <div class="formSectionStyle" style="margin-bottom:25px;">
                    1. Room Type:-
                </div>

                <%--Display Reservation Details--%>
                <div class="divReservationDetails">                

                    <div style="width: 80%; margin: auto; clear:both;">
                        <div class="subFormTableContent" style="padding-left:2%;">
                            <asp:Label ID="lblNoRoomTypeFound" runat="server" Text="No record found." Visible="false"></asp:Label>
                        </div>           
                    </div>

                     <%--Display Pie Chart--%>
                    <div style="text-align:center;">
                        <asp:Chart ID="ChartRoomType" runat="server" width="700px" height="500px">
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

                <%--Display Rented Room Type Quantity--%>
                <div class="divPaymentDetails">

                    <div style="height:20px;">&nbsp;</div>
                
                    <div style="width:90%; margin-auto;">

                        <asp:Repeater ID="RepeaterRentedRoomType" runat="server">

                            <ItemTemplate>
                                <table style="width:90%; margin:auto;">
                                    <tr>
                                        <td style="width:50%; text-align:left;" class="formLabel">
                                            <asp:Label ID="lblRoomTypeName" runat="server" Text='<%# Eval("roomTypeName") %>'></asp:Label>
                                        </td>
                                        <td class="tableData" style="text-align:right;">
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>

                        </asp:Repeater>
                    

                    </div>

                </div>

                <div style="clear:both;"></div>

                <%--Facility--%>
                <div class="formSectionStyle" style="margin-bottom:25px;">
                    2. Facility:-
                </div>

                <%--Display Reservation Details--%>
                <div class="divReservationDetails">

                    <div style="width: 80%; margin: auto; clear:both;">
                        <div class="subFormTableContent" style="padding-left:2%;">
                            <asp:Label ID="lblNoFacilityFound" runat="server" Text="No record found." Visible="false"></asp:Label>
                        </div>           
                    </div>

                     <%--Display Pie Chart--%>
                    <div style="text-align:center;">
                        <asp:Chart ID="ChartFacility" runat="server" width="700px" height="500px">
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

                <%--Display Rented Room Type Quantity--%>
                <div class="divPaymentDetails">

                    <div style="height:20px;">&nbsp;</div>
                
                    <div style="width:90%; margin-auto;">

                        <asp:Repeater ID="RepeaterFacility" runat="server">

                            <ItemTemplate>
                                <table style="width:90%; margin:auto;">
                                    <tr>
                                        <td style="width:50%; text-align:left;" class="formLabel">
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("facilityName") %>'></asp:Label>
                                        </td>
                                        <td class="tableData" style="text-align:right;">
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("quantity") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>

                        </asp:Repeater>
                    

                    </div>

                </div>
            </asp:Panel>
        </div>

    </div>

</asp:Content>