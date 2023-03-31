<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="SelfCheckIn(Error).aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Self_CheckIn.Customer.SelfCheckIn_Error_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>

        .errorIcon {
            width: 120px;
            height: 120px;
            margin-top: 50px;
        }

        .titleStyle {
            color: red;
            font-weight: bold;
            font-size: 250%;
            margin-top: 20px;
            font-family: Helvetica, sans-serif;
        }

        .contentStyle {
            font-family: Helvetica, sans-serif;
            margin-top: 10px;
            font-size:110%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <div>

            <center>

                <img src="../../../Image/error_icon.png" class="errorIcon" />

                <div class="titleStyle">
                    Error
                </div>
                <div class="contentStyle">
                    Please check in at 
                    <asp:Label ID="lblCheckInDate" runat="server" Text="Label"></asp:Label>.
                </div>

            </center>

        </div>

    </form>
</body>
</html>
