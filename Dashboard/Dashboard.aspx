<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Hotel_Management_System.Dashboard.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

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

        
    </style>

    <div style="width:97%; margin-left:auto; margin-right:auto; margin-top:2%;">

        <div style="width:78%; float:left; height:400px;">

            <%--First Row--%>
            <div class="infoContainer">
                <%--Total Arrival--%>
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

            <div style="width:5%; float:left;">
                &nbsp;
            </div>

            <%--Departures--%>
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

            <div style="width:4%; float:left;">
                &nbsp;
            </div>

            <%--Guest in House--%>
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

            <%--Seperator--%>
            <div style="width:100%; height:9%; float:left;">&nbsp;</div>

            <%--Second Row--%>
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

    </div>

</asp:Content>