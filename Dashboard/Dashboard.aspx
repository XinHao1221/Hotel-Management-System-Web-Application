<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Hotel_Management_System.Dashboard.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <style>

        body{
            background-color:#F8F8F8;
        }
        .infoContainer{
            width:30%; 
            height:45%; 
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

    <div style="width:97%; margin-left:auto; margin-right:auto; margin-top:2%;">

        <div style="width:78%; float:left; height:400px;">

            <%--First Row--%>
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
                    <div class="subInfo">
                        <div class="summaryStyle">
                            Arrived: 
                            <asp:Label ID="lblTotalArrived" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
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
                    <div class="subInfo">
                        <div class="summaryStyle">
                            Departed: 
                            <asp:Label ID="lblTotalDeparted" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
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
            

            <%--Seperator--%>
            <div style="width:100%; height:9%; float:left;">&nbsp;</div>

            <%--Second Row--%>
            <asp:HyperLink ID="HLAvailableRoom" runat="server" NavigateUrl="~/Dashboard/RoomAvailability.aspx">
                <div class="infoContainer">
                    <div class="mainInfo">
                        <div class="totalStyle" style="color:rgb(187, 0, 0);">
                            <asp:Label ID="lblTotalAvailableRoom" runat="server" Text=""></asp:Label>
                            <div style="font-size:70%; margin-top:5%; color:black;">
                                Available Room
                            </div>
                        </div>
                    </div>
                    <div class="subInfo">
                        <div class="summaryStyle">
                            Room: 
                            <asp:Label ID="lblRoomAvailability" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </asp:HyperLink>
            
            <div style="width:5%; float:left;">
                &nbsp;
            </div>

            <div class="infoContainer">
                <div class="mainInfo">
                    <div class="totalStyle" style="color:rgb(148, 12, 255);">
                        <asp:Label ID="lblTotalOccupiedRoom" runat="server" Text=""></asp:Label>
                        <div style="font-size:70%; margin-top:5%; color:black;">
                            Occupied Room
                        </div>
                    </div>
                </div>
                <div class="subInfo">
                    <div class="summaryStyle">
                        Occupancy: 
                        <asp:Label ID="lblOccupancy" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

            <div style="width:4%; float:left;">
                &nbsp;
            </div>

            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Dashboard/BlockedRoom.aspx">
                <div class="infoContainer">
                    <div class="mainInfo">
                        <div class="totalStyle" style="color:rgb(92, 92, 92);">
                            <asp:Label ID="lblTotalBlockedRoom" runat="server" Text=""></asp:Label>
                            <div style="font-size:70%; margin-top:5%; color:black;">
                                Blocked Room
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

        <div style="width:3%; float:left;">
            &nbsp;
        </div>

        <div style="" class="dateTimeContainer">
            <div class="dateStyle">
                <div style="padding:40% 0 0 0;">
                    <div style="font-size:100%; text-align:center;">
                        <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="font-size:400%; margin:3% 0% 3% 0%;">
                        <asp:Label ID="lblDay" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="font-size:100%;">
                        <asp:Label ID="lblYear" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="dateTimeStyle">
                <div style="padding: 6% 0 0 5%;">
                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>

        <div style="clear:both; height:5%; height:40px;">&nbsp;</div>

        <div class="roomAvailabilityContainer">
            <div style="padding:3%;">
                <div style="font-family: Helvetica, sans-serif; font-weight:bold;">
                    Rooms
                </div>
                <div style="clear:both; height:20px;"></div>
                <div>
                    <%--Repeater table header--%>
                    <div style="width:100%;">
                        <div style="float:left; width:10%; text-align:center;" class="subFormRepeaterHeader">
                            No
                        </div>
                        <div style="float:left; width:40%;" class="subFormRepeaterHeader">
                            Room Type
                        </div>
                        <div style="float:left; width:14%; text-align:right;" class="subFormRepeaterHeader">
                            Available
                        </div>
                        <div style="float:left; width:13%; text-align:right;" class="subFormRepeaterHeader">
                            Sold
                        </div>
                        <div style="float:left; width:5%; text-align:right;" class="subFormRepeaterHeader">
                            &nbsp;

                        </div>
                        <div style="float:left; width:18%;" class="subFormRepeaterHeader">
                            Status
                        </div>
                    </div>  

                    <%--Repeater Table content--%>
                    <asp:Repeater ID="RepeaterRoomAvailability" runat="server" OnItemDataBound="RepeaterRoomAvailability_ItemDataBound">

                        <ItemTemplate>
                            <div style="width:100%;">
                                <div style="float:left; width:10%; text-align:center;" class="subFormTableContent">
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                </div>
                                <div style="float:left; width:40%;" class="subFormTableContent">
                                    <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("roomTypeID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblRoomTypeName" runat="server" Text='<%# Eval("roomTypeName")%>'></asp:Label> 
                                </div>
                                <div style="float:left; width:14%; text-align:right;" class="subFormTableContent">
                                    <asp:Label ID="lblAvailableRoom" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div style="float:left; width:13%; text-align:right;" class="subFormTableContent">
                                    <asp:Label ID="lblSold" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div style="float:left; width:5%;" class="subFormTableContent">
                                    &nbsp;
                                </div>
                                <div style="float:left; width:18%;" class="subFormTableContent">
                                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                </div>
                            </div>  
                        </ItemTemplate>

                        <AlternatingItemTemplate>
                            <div style="width:100%;">
                                <div style="float:left; width:10%; text-align:center;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                </div>
                                <div style="float:left; width:40%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("roomTypeID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblFeature" runat="server" Text='<%# Eval("roomTypeName")%>'></asp:Label>
                                </div>
                                <div style="float:left; width:14%; text-align:right;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblAvailableRoom" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div style="float:left; width:13%; text-align:right;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblSold" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div style="float:left; width:5%;" class="subFormTableContentAlternate">
                                    &nbsp;
                                </div>
                                <div style="float:left; width:18%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                </div>
                            </div> 
                        </AlternatingItemTemplate>
    
                    </asp:Repeater>

                </div>

                <div style="clear:both;"></div>

            </div>


            
        </div>

        <div style="width:3%; float:left;">
            &nbsp;
        </div>

        <%--Guest In House Chart--%>
        <div class="guestInHouseContainer">
            <div style="padding:5%;">
                <div style="font-family: Helvetica, sans-serif; font-weight:bold;">
                    Guest in House
                </div>
                <div style="clear:both; height:20px;"></div>
                <div style="width:100%; text-align: center;">

                    <asp:Chart ID="ChartGuestInHouse" runat="server" OnCustomize="ChartGuestInHouse_Customize">
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
        </div>

        <div style="clear:both; height:70px;">&nbsp;</div>

    </div>

</asp:Content>