<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="HotelReport.aspx.cs" Inherits="Hotel_Management_System.Reporting.Hotel_Report.HotelReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <style>

        body{
            
        }
        .infoContainer{
            width:30%; 
            height:50%; 
            min-height:180px;
            float:left; 
            border:1px solid rgb(205 205 205);
            background-color:white;
        }

        .mainInfo{
            width:100%;
            height:80%;
            /*background-color:aquamarine;*/
        }

        .subInfo{
            width:100%;
            height:20%;
            /*background-color:yellow;*/
        }

        .totalStyle{
            text-align:center; 
            padding: 12.5% 0 0 0; 
            font-size:155%; 
            font-family: Helvetica, sans-serif;
            font-weight:bold;
        }

        .summaryStyle{
            font-family: Helvetica, sans-serif;
            padding: 3% 0 0 5%;
            font-size:85%;
        }

        .dateTimeContainer{
            width:18%; 
            background-color:white; 
            float:left; 
            height:400px;
            border:1px solid rgb(205 205 205);
            
        }

        .dateStyle{
            width:100%;
            height:90%;
            text-align:center;
            font-family: Helvetica, sans-serif;
            
        }

        .dateTimeStyle{
            width:100%;
            height:10%;
            /*background-color:yellow;*/
            font-family: Helvetica, sans-serif;
            font-size:80%;
        }

        .roomAvailabilityContainer{
            background-color:white;
            width:60%;
            float:left;

        }

        .guestInHouseContainer{
            background-color:white;
            width:36%;
            float:left;
        }
    </style>

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

        <div style="width:95%; margin-left:auto; margin-right:auto;">

            <%--Total Arrival--%>
            <asp:HyperLink ID="HLTotalArrival" runat="server" NavigateUrl="~/Front_Desk/CheckIn/CheckIn.aspx">
                <div class="infoContainer">
                    <div class="mainInfo">
                        <div class="totalStyle" style="color:rgb(0, 206, 27);">
                            <asp:Label ID="lblTotalArrival" runat="server" Text=""></asp:Label>
                            <div style="font-size:70%; margin-top:5%; color:black;">
                                Total Arrival
                            </div>
                        </div>
                    </div>
                    <%--<div class="subInfo">
                        <div class="summaryStyle">
                            Arrived: 
                            <asp:Label ID="lblTotalArrived" runat="server" Text=""></asp:Label>
                        </div>
                    </div>--%>
                </div>
            </asp:HyperLink>
                
            <div style="width:5%; float:left;">
                &nbsp;
            </div>

            <%--Departures--%>
            <asp:HyperLink ID="HLDepartures" runat="server" NavigateUrl="~/Front_Desk/CheckOut/CheckOut.aspx">
                <div class="infoContainer">
                    <div class="mainInfo">
                        <div class="totalStyle" style="color:rgb(255, 0, 0);">
                            <asp:Label ID="lblTotalDeparture" runat="server" Text=""></asp:Label>
                            <div style="font-size:70%; margin-top:5%; color:black;">
                                Departures
                            </div>
                        </div>
                    </div>
                    <%--<div class="subInfo">
                        <div class="summaryStyle">
                            Departed: 
                            <asp:Label ID="lblTotalDeparted" runat="server" Text=""></asp:Label>
                        </div>
                    </div>--%>
                </div>
            </asp:HyperLink>
            

            <div style="width:4%; float:left;">
                &nbsp;
            </div>

            <%--Guest in House--%>
            <asp:HyperLink ID="HLGuestInHouse" runat="server" NavigateUrl="~/Front_Desk/GuestInHouse/GuestInHouse.aspx">
                <div class="infoContainer">
                    <div class="mainInfo">
                        <div class="totalStyle" style="color:rgb(0, 133, 255);">
                            <asp:Label ID="lblTotalInHouseGuest" runat="server" Text=""></asp:Label>
                            <div style="font-size:70%; margin-top:5%; color:black;">
                                Guest in House
                            </div>
                        </div>
                    </div>
                    <div class="subInfo">
                        <div class="summaryStyle">
                            &nbsp;
                        </div>
                    </div>
                </div>
            </asp:HyperLink>

        </div>
         
        <div style="clear:both; height:20px;">&nbsp;</div>

        <%--Display room rented info --%>
        <div class="formSectionStyle" style="margin-bottom:25px">
            1. Room:-
        </div>

        <div style="width:86%; margin:auto;">
            <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                <asp:Label ID="Label1" runat="server" Text="No" ToolTip="No"></asp:Label>
            </div>
            <div style="float:left; width:35%;" class="subFormRepeaterHeader">
                <asp:Label ID="ttRoomType" runat="server" Text="Room Type" ToolTip="Room Type"></asp:Label>
            </div>
            <div style="float:left; width:18%; text-align:right;" class="subFormRepeaterHeader">
                <asp:Label ID="ttRoomCheckIn" runat="server" Text="Check In" ToolTip="Check In"></asp:Label>
            </div>
            <div style="float:left; width:18%; text-align:right;" class="subFormRepeaterHeader">
                <asp:Label ID="ttRoomInHouse" runat="server" Text="In House" ToolTip="In House"></asp:Label>
            </div>
            <div style="float:left; width:18%; text-align:right;" class="subFormRepeaterHeader">
                <asp:Label ID="ttRoomCheckOut" runat="server" Text="Check Out" ToolTip="Check Out"></asp:Label>
            </div>
            <div style="float:left; width:3%;" class="subFormRepeaterHeader">
                &nbsp;
            </div>
        </div> 

        <asp:Repeater ID="RepeaterRoom" runat="server" OnItemDataBound="RepeaterRoom_ItemDataBound">

            <ItemTemplate>
                <div style="width:86%; margin:auto;">
                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContent">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:35%; font-size:90%;" class="subFormTableContent">
                        <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("RoomTypeID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>' ToolTip='<%# Eval("Title") %>' ></asp:Label>
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblCheckIn" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblInHouse" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblCheckOut" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:3%;" class="subFormTableContent">
                        &nbsp;
                    </div>
                </div>

            </ItemTemplate>

            <AlternatingItemTemplate>
                <div style="width:86%; margin:auto;">
                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:35%; font-size:90%;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("RoomTypeID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>' ToolTip='<%# Eval("Title") %>' ></asp:Label>
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblCheckIn" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblInHouse" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblCheckOut" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:3%;" class="subFormTableContentAlternate">
                        &nbsp;
                    </div>
                </div>
            </AlternatingItemTemplate>

        </asp:Repeater>
        
        <div style="clear:both; height:20px;">&nbsp;</div>

        <div class="formSectionStyle" style="margin-bottom:25px">
            2. Facility:-
        </div>

        <div style="width:86%; margin:auto;">
            <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                <asp:Label ID="Label2" runat="server" Text="No" ToolTip="No"></asp:Label>
            </div>
            <div style="float:left; width:35%;" class="subFormRepeaterHeader">
                <asp:Label ID="ttFacility" runat="server" Text="Facility" ToolTip="Facility"></asp:Label>
            </div>
            <div style="float:left; width:18%; text-align:right;" class="subFormRepeaterHeader">
                <asp:Label ID="ttFacilityCheckIn" runat="server" Text="Check In" ToolTip="Check In"></asp:Label>
            </div>
            <div style="float:left; width:18%; text-align:right;" class="subFormRepeaterHeader">
                <asp:Label ID="ttFacilityInHouse" runat="server" Text="In House" ToolTip="In House"></asp:Label>
            </div>
            <div style="float:left; width:18%; text-align:right;" class="subFormRepeaterHeader">
                <asp:Label ID="ttFacilityCheckOut" runat="server" Text="Check Out" ToolTip="Check Out"></asp:Label>
            </div>
            <div style="float:left; width:3%;" class="subFormRepeaterHeader">
                &nbsp;
            </div>
        </div> 

        <asp:Repeater ID="RepeaterFacility" runat="server" OnItemDataBound="RepeaterFacility_ItemDataBound">

            <ItemTemplate>
                <div style="width:86%; margin:auto;">
                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContent">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:35%; font-size:90%;" class="subFormTableContent">
                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("FacilityID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("FacilityName") %>' ToolTip='<%# Eval("FacilityName") %>' ></asp:Label>
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblCheckIn" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblInHouse" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblCheckOut" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:3%;" class="subFormTableContent">
                        &nbsp;
                    </div>
                </div>

            </ItemTemplate>

            <AlternatingItemTemplate>
                <div style="width:86%; margin:auto;">
                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:35%; font-size:90%;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("FacilityID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("FacilityName") %>' ToolTip='<%# Eval("FacilityName") %>' ></asp:Label>
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblCheckIn" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblInHouse" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:18%; font-size:90%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblCheckOut" runat="server" Text=""></asp:Label>&nbsp;
                    </div>
                    <div style="float:left; width:3%;" class="subFormTableContentAlternate">
                        &nbsp;
                    </div>
                </div>
            </AlternatingItemTemplate>

        </asp:Repeater>

        <div style="clear:both; height:70px;">&nbsp;</div>
    </div> 

</asp:Content>