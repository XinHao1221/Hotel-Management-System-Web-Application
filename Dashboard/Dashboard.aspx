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

    </style>
    <div style="width:97%; margin-left:auto; margin-right:auto; margin-top:2%;">

        <div style="width:78%; float:left; height:400px;">

            <%--First Row--%>
            <div class="infoContainer">
                <%--Total Arrival--%>
                <div class="mainInfo">
                    <div class="totalStyle" style="color:rgb(0, 206, 27);">
                        10
                        <div style="font-size:70%; margin-top:5%; color:black;">
                            Total Arrival
                        </div>
                    </div>
                </div>
                <div class="subInfo">
                    <div class="summaryStyle">
                        Arrived: 5
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
                        10
                        <div style="font-size:70%; margin-top:5%; color:black;">
                            Departures
                        </div>
                    </div>
                </div>
                <div class="subInfo">
                    <div class="summaryStyle">
                        Departed: 5
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
                        10
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
                &nbsp;
            </div>

            <div style="width:5%; float:left;">
                &nbsp;
            </div>

            <div class="infoContainer">
                &nbsp;
            </div>

            <div style="width:4%; float:left;">
                &nbsp;
            </div>

            <div class="infoContainer">
                &nbsp;
            </div>
        </div>

        <div style="width:3%; float:left;">
            &nbsp;
        </div>

        <div style="width:19%; background-color:aqua; float:left; height:400px;">
            &nbsp;
        </div>

    </div>

</asp:Content>