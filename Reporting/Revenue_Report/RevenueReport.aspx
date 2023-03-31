<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

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
            width:25%;
            min-height:500px;
            border: 2px solid rgb(180 180 180);
            box-shadow: 5px 10px;
            box-shadow: 3px 3px 5px rgb(149 149 149);
            position:fixed;
            right:2.5%;
            left:72.5%;
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

                <div style="clear:both; height:40px;"></div>

                <asp:Panel ID="PNReportDetails" runat="server" Visible="false">
                    <%--<div style="font-family: Helvetica, sans-serif; font-size:120%; font-weight:bold; margin-left:5%; margin-bottom:-40px;">
                        Profit 
                    </div>--%>

                    <div style="height:10px;">&nbsp;</div>
                
                    <%--Display Pie Chart--%>
                    <div style="text-align:center;">
                        <asp:Chart ID="ChartProfit" runat="server" OnCustomize="ChartGuestInHouse_Customize" Width="500px" Height="500px">
                            <Titles>
                                <asp:Title Font="Helvetica, 12pt, style=Bold" Name="Title1" 
                                    Text="Pie Chart of Hotel's Revenue">
                                </asp:Title>
                            </Titles>
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
                
                    <%--Display Details--%>
                    <%--Room Profit Details--%>
                    <div>
                        <div class="formSectionStyle" style="margin-bottom:25px;">
                            1. Room:-
                        </div>

                        <div style="width:86%; margin:auto;">
                            <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttNo" runat="server" Text="No" ToolTip="No"></asp:Label>
                            </div>
                            <div style="float:left; width:15%; text-align:right;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttDate" runat="server" Text="Date" ToolTip="Date"></asp:Label>
                            </div>
                            <div style="float:left; width:2.5%; text-align:right;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                            <div style="float:left; width:27%;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttRoomType" runat="server" Text="RoomType" ToolTip="Room Type"></asp:Label>
                            </div>
                            <div style="float:left; width:14%; text-align:right;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttPrice" runat="server" Text="Price" ToolTip="Price"></asp:Label>
                            </div>
                            <div style="float:left; width:1.5%;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                            <div style="float:left; width:14%; text-align:right;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttExtraBed" runat="server" Text="Extra Bed" ToolTip="Extra Bed"></asp:Label>
                            </div>
                            <div style="float:left; width:1.5%;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                            <div style="float:left; width:14%; text-align:right;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttSubTotal" runat="server" Text="Sub Total" ToolTip="SubTotal"></asp:Label>
                            </div>
                            <div style="float:left; width:2.5%;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                        </div> 

                        <%--Repeater Table content--%>
                        <asp:Repeater ID="RepeaterRoom" runat="server" OnItemDataBound="RepeaterRoom_ItemDataBound">

                            <ItemTemplate>
                                <div style="width:86%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("RoomTypeID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>' ToolTip='<%# Eval("Date") %>' ></asp:Label>
                                    </div>
                                    <div style="float:left; width:2.5%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:27%; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblRoomTypeName" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div style="float:left; width:14%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblRoomPrice" runat="server" Text='<%# Eval("RoomPrice") %>' ToolTip='<%# Eval("RoomPrice") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:1.5%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:14%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblExtraBedPrice" runat="server" Text='<%# Eval("ExtraBedCharges") %>' ToolTip='<%# Eval("ExtraBedCharges") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:1.5%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:14%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                                    </div>
                                    <div style="float:left; width:2.5%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                </div>

                            </ItemTemplate>

                            <AlternatingItemTemplate>
                                <div style="width:86%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("RoomTypeID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>' ToolTip='<%# Eval("Date") %>' ></asp:Label>
                                    </div>
                                    <div style="float:left; width:2.5%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:27%; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblRoomTypeName" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div style="float:left; width:14%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblRoomPrice" runat="server" Text='<%# Eval("RoomPrice") %>' ToolTip='<%# Eval("RoomPrice") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:1.5%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:14%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblExtraBedPrice" runat="server" Text='<%# Eval("ExtraBedCharges") %>' ToolTip='<%# Eval("ExtraBedCharges") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:1.5%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:14%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                                    </div>
                                    <div style="float:left; width:2.5%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                </div>
                            </AlternatingItemTemplate>

                        </asp:Repeater>

                        <div style="width: 80%; margin: auto; clear:both;">
                            <div class="subFormTableContent" style="padding-left:2%;">
                                <asp:Label ID="lblNoRoomFound" runat="server" Text="No item found." Visible="false"></asp:Label>
                            </div>           
                        </div>

                    </div>

                    <div style="clear:both; height:30px;">&nbsp;</div>

                    <%--Facility Details--%>
                    <div>
                        <div class="formSectionStyle" style="margin-bottom:25px;">
                            2. Facility:-
                        </div>

                        <div style="width:86%; margin:auto;">
                            <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                                <asp:Label ID="Label1" runat="server" Text="No" ToolTip="No"></asp:Label>
                            </div>
                            <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttFacilityRentedDate" runat="server" Text="Date" ToolTip="Date"></asp:Label>
                            </div>
                            <div style="float:left; width:4%; text-align:right;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                            <div style="float:left; width:30%;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttFacility" runat="server" Text="Facility" ToolTip="Facility"></asp:Label>
                            </div>
                            <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttFacilityPrice" runat="server" Text="Price" ToolTip="Price"></asp:Label>
                            </div>
                            <div style="float:left; width:18%;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                        </div> 

                        <%--Repeater Table content--%>
                        <asp:Repeater ID="RepeaterFacility" runat="server" OnItemDataBound="RepeaterFacility_ItemDataBound">

                            <ItemTemplate>
                                <div style="width:86%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("FacilityID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblDateRented" runat="server" Text='<%# Eval("DateRented") %>' ToolTip='<%# Eval("DateRented") %>' ></asp:Label>
                                    </div>
                                    <div style="float:left; width:4%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:30%; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblFacilityName" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblFacilityrice" runat="server" Text='<%# Eval("Price") %>' ToolTip='<%# Eval("Price") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:18%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                </div>

                            </ItemTemplate>

                            <AlternatingItemTemplate>
                                <div style="width:86%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("FacilityID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblDateRented" runat="server" Text='<%# Eval("DateRented") %>' ToolTip='<%# Eval("DateRented") %>' ></asp:Label>
                                    </div>
                                    <div style="float:left; width:4%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:30%; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblFacilityName" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblFacilityrice" runat="server" Text='<%# Eval("Price") %>' ToolTip='<%# Eval("Price") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:18%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                </div>
                            </AlternatingItemTemplate>

                        </asp:Repeater>

                        <div style="width: 80%; margin: auto; clear:both;">
                            <div class="subFormTableContent" style="padding-left:2%;">
                                <asp:Label ID="lblNoFacilityFound" runat="server" Text="No item found." Visible="false"></asp:Label>
                            </div>           
                        </div>
                    </div>

                    <div style="clear:both; height:30px;">&nbsp;</div>

                    <%--Room Fine Details--%>
                    <div>
                        <div class="formSectionStyle" style="margin-bottom:25px;">
                            3. Fine:-
                        </div>

                        <div style="width:86%; margin:auto;">
                            <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                                <asp:Label ID="Label3" runat="server" Text="No" ToolTip="No"></asp:Label>
                            </div>
                            <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttFineDate" runat="server" Text="Date" ToolTip="Date"></asp:Label>
                            </div>
                            <div style="float:left; width:4%; text-align:right;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                            <div style="float:left; width:30%;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttMissingEquipment" runat="server" Text="Missing Equipment" ToolTip="Missing Equipment"></asp:Label>
                            </div>
                            <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttFine" runat="server" Text="Fine" ToolTip="Fine"></asp:Label>
                            </div>
                            <div style="float:left; width:18%;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                        </div> 

                        <%--Repeater Table content--%>
                        <asp:Repeater ID="RepeaterFine" runat="server" OnItemDataBound="RepeaterFine_ItemDataBound">

                            <ItemTemplate>
                                <div style="width:86%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DateCreated") %>' ToolTip='<%# Eval("DateCreated") %>' ></asp:Label>
                                    </div>
                                    <div style="float:left; width:4%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:30%; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblMissingEquipment" runat="server" Text='<%# Eval("Title") %>' ToolTip='<%# Eval("Title") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblFineCharges" runat="server" Text='<%# Eval("Amount") %>' ToolTip='<%# Eval("Amount") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:18%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                </div>

                            </ItemTemplate>

                            <AlternatingItemTemplate>
                                <div style="width:86%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DateCreated") %>' ToolTip='<%# Eval("DateCreated") %>' ></asp:Label>
                                    </div>
                                    <div style="float:left; width:4%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:30%; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblMissingEquipment" runat="server" Text='<%# Eval("Title") %>' ToolTip='<%# Eval("Title") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblFineCharges" runat="server" Text='<%# Eval("Amount") %>' ToolTip='<%# Eval("Amount") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:18%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                </div>
                            </AlternatingItemTemplate>

                        </asp:Repeater>

                        <div style="width: 80%; margin: auto; clear:both;">
                            <div class="subFormTableContent" style="padding-left:2%;">
                                <asp:Label ID="lblNoFineCharges" runat="server" Text="No item found." Visible="false"></asp:Label>
                            </div>           
                        </div>
                    </div>

                    <div style="clear:both; height:30px;">&nbsp;</div>

                    <%--Services Details--%>
                    <div>
                        <div class="formSectionStyle" style="margin-bottom:25px;">
                            4. Services:-
                        </div>

                        <div style="width:86%; margin:auto;">
                            <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                                <asp:Label ID="Label2" runat="server" Text="No" ToolTip="No"></asp:Label>
                            </div>
                            <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttServiceDate" runat="server" Text="Date" ToolTip="Date"></asp:Label>
                            </div>
                            <div style="float:left; width:4%; text-align:right;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                            <div style="float:left; width:30%;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttServices" runat="server" Text="Services" ToolTip="Services"></asp:Label>
                            </div>
                            <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttCharges" runat="server" Text="Charges" ToolTip="Charges"></asp:Label>
                            </div>
                            <div style="float:left; width:18%;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                        </div> 

                        <%--Repeater Table content--%>
                        <asp:Repeater ID="RepeaterServices" runat="server" OnItemDataBound="RepeaterServices_ItemDataBound">

                            <ItemTemplate>
                                <div style="width:86%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DateCreated") %>' ToolTip='<%# Eval("DateCreated") %>' ></asp:Label>
                                    </div>
                                    <div style="float:left; width:4%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:30%; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblServices" runat="server" Text='<%# Eval("Title") %>' ToolTip='<%# Eval("Title") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblCharges" runat="server" Text='<%# Eval("Amount") %>' ToolTip='<%# Eval("Amount") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:18%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                </div>

                            </ItemTemplate>

                            <AlternatingItemTemplate>
                                <div style="width:86%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DateCreated") %>' ToolTip='<%# Eval("DateCreated") %>' ></asp:Label>
                                    </div>
                                    <div style="float:left; width:4%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:30%; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblServices" runat="server" Text='<%# Eval("Title") %>' ToolTip='<%# Eval("Title") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblCharges" runat="server" Text='<%# Eval("Amount") %>' ToolTip='<%# Eval("Amount") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:18%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                </div>
                            </AlternatingItemTemplate>

                        </asp:Repeater>

                        <div style="width: 80%; margin: auto; clear:both;">
                            <div class="subFormTableContent" style="padding-left:2%;">
                                <asp:Label ID="lblNoServicesCharges" runat="server" Text="No item found." Visible="false"></asp:Label>
                            </div>           
                        </div>
                    </div>
                </asp:Panel>

                <div style="width: 80%; margin: auto; clear:both;">
                    <div class="subFormTableContent" style="padding-left:2%;">
                        <asp:Label ID="lblNoDetailsFound" runat="server" Text="No revenue found." Visible="false"></asp:Label>
                    </div>           
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
