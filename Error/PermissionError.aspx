<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="PermissionError.aspx.cs" Inherits="Hotel_Management_System.Errror.PermissionError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <style>

        .errorIcon {
            width: 100px;
            height: 100px;
            margin-top: 50px;
        }

        .titleStyle {
            
            font-weight: bold;
            font-size: 200%;
            margin-top: 20px;
            font-family: Helvetica, sans-serif;
        }

        .contentStyle {
            font-family: Helvetica, sans-serif;
            margin-top: 10px;
            font-size:110%;
        }

        .nextBtnStyle{
            width: 110px;
            height: 40px;
            background-color: rgb(0, 206, 27);
            color: white;
            font-weight:bold;
            border:1px solid rgb(0, 206, 27);
            border-radius:5px;
        }
    </style>

    <div>

        <center>

            <img src="../../../Image/padlock_icon.png" class="errorIcon" />

            <div class="titleStyle">
                Access denied
            </div>
            <div class="contentStyle">
                You don't have permissions to access this page.
            </div>

            <div style="height:40px;">&nbsp;</div>

            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="nextBtnStyle" OnClientClick="JavaScript:window.history.back(1); return false;"/>

        </center>

        

    </div>

</asp:Content>
