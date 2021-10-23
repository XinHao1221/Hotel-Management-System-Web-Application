<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RentFacility.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Self_CheckIn.Customer.RentFacility" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>

    <link rel="stylesheet" href="SelfCheckInStyle.css" />
</head>
<body>
    <form id="form1" runat="server">

        <div style="width:70%; margin-left:auto; margin-right:auto; margin-top:30px;">

            <div style="width:20%; float:left;">&nbsp;</div>

            <div class="progressBarContainer">
                <div class="progressBarCircleSelected">
                    <img src="../../../Image/bed.png" class="progressBarIconStyle"/>
                </div>
                <div class="progressBarLabelStyleSelected">
                    Select Room
                </div>
            </div>

            <div class="progressBarContainer">
                <div class="progressBarCircleSelected">
                    <img src="../../../Image/facility_icon_green.png" class="progressBarIconStyle"/>
                </div>
                <div class="progressBarLabelStyleSelected">
                    Rent Facility
                </div>
            </div>

            <div class="progressBarContainer">
                <div class="progressBarCircle">
                    <img src="../../../Image/done_icon_grey.png" class="progressBarIconStyle"/>
                </div>
                <div class="progressBarLabelStyle">
                    Completed
                </div>
            </div>

            <div style="width:20%; float:left;">&nbsp;</div>

            <div style="clear:both; height:30px;"></div>

            <div class="content">
                <div style="height:200px;">
                    &nbsp;
                </div>
                <div style="height:60px;">&nbsp;

                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backBtnStyle" OnClick="btnBack_Click"/>

                    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="nextBtnStyle" OnClick="btnNext_Click"/>

                </div>
            </div>

        </div>

    </form>
</body>
</html>
