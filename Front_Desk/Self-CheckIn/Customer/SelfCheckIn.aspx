<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelfCheckIn.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Self_CheckIn.Customer.SelfCheckIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

    <style>
        .welcomeMsg{
            text-align:center;
            font-size:200%;
            font-family: Helvetica, sans-serif;
            margin-top:5%;
            font-weight:bold;
            
        }

        .textBoxStyle{
            width:48%;
            height:50px;
            border: 2px solid rgb(192 192 192);
            border-radius: 8px;
            padding: 0 1% 0 1%;
            font-size:100%;
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

        .iconHelp{
            width:2%;
            height:2%;
            margin-left:1%;
        }
    </style>

<body>
    <form id="form1" runat="server">

        <div>
            <div class="welcomeMsg">
                Welcome
                <asp:Label ID="lblGuestName" runat="server" Text=""></asp:Label>
            </div>

            <div style="margin-top:5%; text-align:center;">
                <div style="font-family: Helvetica, sans-serif; width:48%; margin-left:auto; margin-right:auto; text-align:start; font-weight:bold;">
                    Password
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/help_icon.png" ToolTip="Note: Password can be obtain from email." CssClass="iconHelp"/>
                </div>
                <div>
                    <asp:TextBox ID="txtSecretPassword" runat="server" CssClass="textBoxStyle" placeholder="Enter Password Here..."></asp:TextBox>
                </div>
               
                <div style="height:50px;">&nbsp;</div>

                <asp:Button ID="btnNext" runat="server" Text="Start" CssClass="nextBtnStyle"/>

                <%--<div style="font-family: Helvetica, sans-serif; width:48%; margin-left:auto; margin-right:auto; font-size:80%; color:rgb(128 128 128);">
                    Note: Password can be obtain from email.
                </div>--%>

            </div>
            

        </div>

    </form>
</body>
</html>
