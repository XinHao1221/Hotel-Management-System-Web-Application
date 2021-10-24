<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Completed.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Self_CheckIn.Customer.Completed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>

    <link rel="stylesheet" href="SelfCheckInStyle.css" />

    <link rel="stylesheet" href="SelfCheckInStyle.css" />
    <%--CSS--%>
    <link rel="stylesheet" href="../../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../../StyleSheet/RepeaterTable.css" />

    <style>
        .reservationFormPanel{
            border: 1px solid rgb(213 213 213);
            width: 80%;
            margin: auto;
            padding-left: 40px;
            padding-right: 40px;
            padding-top: 30px;
            padding-bottom:30px;
            margin-top: 20px;
            min-height:180px;
        }

        .popupBox{
            min-width:1000px;
            width:80%;
        }

        .facilityPanelStyle{
            width:45%; 
            float:left; 
            /*background-color:red;*/
        }

        .selectRoomContainer{
            border: 1px solid black;
            background-color:rgb(248 248 248);
            float:left;
            width:48%; 
            margin-right:2%; 
            margin-bottom:2%; 
            height:200px; 
            overflow-y:auto;

        }

        .selectRoomContainerAlternate{
            border: 1px solid black;
            background-color:rgb(248 248 248);
            float:left;
            width:48%; 
            margin-bottom:2%; 
            height:200px; 
            overflow-y:auto;
        }

        .selectRoomContainer:hover, .selectRoomContainerAlternate:hover{
            cursor:pointer;
            opacity:0.7;
            box-shadow: 3px 3px 5px rgb(149 149 149);
        }

        .divAmountDue{
            float:left;
            width:68%;
        }

        .divPaymentDetails{
            float:left;
            width:30%;
            min-height:350px;
            border: 2px solid rgb(180 180 180);
            box-shadow: 5px 10px;
            box-shadow: 3px 3px 5px rgb(149 149 149);
        }
    </style>

</head>

<body>
    <form id="form1" runat="server">

        <div>

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
                <div class="progressBarCircleSelected">
                    <img src="../../../Image/done_icon_green.png" class="progressBarIconStyle"/>
                </div>
                <div class="progressBarLabelStyleSelected">
                    Completed
                </div>
            </div>

            <div style="width:20%; float:left;">&nbsp;</div>

            <div style="clear:both; height:30px;"></div>

            <div class="content">
                <div style="height:max-content;">
                    
                    <div style="height:25px;">&nbsp;</div>

                    <div class="formSectionStyle" style="margin-bottom:25px">
                        1. Reserve Room:-
                    </div>

                    <%--Display Reserved Room Details--%>
                    <asp:Repeater ID="RepeaterReservedRoom" runat="server" OnItemDataBound="RepeaterReservedRoom_ItemDataBound">

                        <ItemTemplate >
                
                            <div class="reservationFormPanel">  

                                <div style="width:100%; margin:auto;"">
                                    <div style="width:50%; float:left;">

                                        <table style="width:100%;">
                                            <tr>
                                                <td class="formLabel">
                                                        Room Type             
                                                </td>
                                                <td class="tableSeperator" style="width:5%;"></td>
                                                <td class="tableData">
                                                    <asp:Label ID="lblReservationRoomID" runat="server" Visible="false" Text='<%# Eval("reservationRoomID") %>'></asp:Label>
                                                    <asp:Label ID="lblRoomTypeID" runat="server" Visible="false" Text='<%# Eval("roomTypeID") %>'></asp:Label>
                                                    <asp:Label ID="lblRoomType" runat="server" Text='<%# Eval("roomTypeName") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                       Date             
                                                </td>
                                                <td class="tableSeperator" style="width:5%;"></td>
                                                <td class="tableData">
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("date") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel requiredFieldLabel">
                                                        Room    
                                                </td>
                                                <td class="tableSeperator"></td>
                                                <td class="tableData">
                                                    <asp:Label ID="lblSelectedRoomNo" runat="server" Text='<%# Eval("roomNo") %>'></asp:Label>
                                                    <asp:Label ID="lblSelectedRoomID" runat="server" Text='<%# Eval("roomID") %>' Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                        
                                        </table>

                                    </div>

                                    <div style="width:50%; float:left;">

                                        <table style="width:100%;">
                                            <tr>
                                                <td class="formLabel">
                                                        Adults             
                                                </td>
                                                <td class="tableSeperator" style="width:5%;"></td>
                                                <td class="tableData">
                                                    <asp:Label ID="lblAdults" runat="server" Text='<%# Eval("adults") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                        Kids   
                                                </td>
                                                <td class="tableSeperator"></td>
                                                <td class="tableData">
                                                    <asp:Label ID="lblKids" runat="server" Text='<%# Eval("kids") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Extra Bed
                                                </td>
                                                <td class="tableSeperator"></td>
                                                <td class="tableData">
                                                    <asp:Label ID="lblExtraBed" runat="server" Text='<%# Eval("extraBedPrice") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblTrueFalse" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                            
                                        </table>

                                    </div>
                                </div>
                            </div>

                        </ItemTemplate>

                    </asp:Repeater>

                    <div style="clear:both; height:40px;">&nbsp;</div>

                    <div class="formSectionStyle" style="margin-bottom:25px">
                        2. Rented Facility:-
                    </div>

                    <%--Repeater Rented Facility--%>
                    <%--Repeater table header--%>
                    <div style="width:86%; margin:auto;">
                        <div style="float:left; width:6%; text-align:center;" class="subFormRepeaterHeader">
                            No
                        </div>
                        <div style="float:left; width:20%;" class="subFormRepeaterHeader">
                            Facility
                        </div>
                        <div style="float:left; width:10%; text-align:right;" class="subFormRepeaterHeader">
                            Price
                        </div>
                        <div style="float:left; width:10%; text-align:right;" class="subFormRepeaterHeader">
                            Quantity
                        </div>
                        <div style="float:left; width:15%; text-align:right;" class="subFormRepeaterHeader">
                            Rent Date
                        </div>
                        <div style="float:left; width:15%; text-align:right;" class="subFormRepeaterHeader">
                            Return Date
                        </div>
                        <div style="float:left; width:15%; text-align:right;" class="subFormRepeaterHeader">
                            Sub-Total
                        </div>
                        <div style="float:left; width:9%;" class="subFormRepeaterHeader">
                            &nbsp;
                        </div>
                    </div>

                    <asp:Repeater ID="RepeaterRentedFacility" runat="server" OnItemDataBound="RepeaterRentedFacility_ItemDataBound" OnItemCommand="RepeaterRentedFacility_ItemCommand">

                        <ItemTemplate>

                            <div style="width:86%; margin:auto;">
                                <div style="float:left; width:6%; text-align:center; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                </div>
                                <div style="float:left; width:20%; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblReservationFacilityID" runat="server" Text='<%# Eval("reservationFacilityID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:N2}") %>' ToolTip='<%# Eval("price", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' ToolTip='<%# Eval("quantity") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblRentDate" runat="server" Text='<%# Eval("rentDate") %>' ToolTip='<%# Eval("rentDate") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblReturnDate" runat="server" Text='<%# Eval("returnDate") %>' ToolTip='<%# Eval("returnDate") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblSubTotal" runat="server" Text='<%# Eval("subTotal", "{0:N2}") %>' ToolTip='<%# Eval("subTotal", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:6%;" class="subFormTableContent">
                                    &nbsp;
                                </div>
                                <div style="float:left; width:3%;" class="subFormTableContent">
                                    &nbsp;
                                </div>
                            </div>
                
                        </ItemTemplate>

                        <AlternatingItemTemplate>
                            <div style="width:86%; margin:auto;">

                                <div style="float:left; width:6%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                </div>
                                <div style="float:left; width:20%; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblReservationFacilityID" runat="server" Text='<%# Eval("reservationFacilityID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:N2}") %>' ToolTip='<%# Eval("price", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' ToolTip='<%# Eval("quantity") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblRentDate" runat="server" Text='<%# Eval("rentDate") %>' ToolTip='<%# Eval("rentDate") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblReturnDate" runat="server" Text='<%# Eval("returnDate") %>' ToolTip='<%# Eval("returnDate") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblSubTotal" runat="server" Text='<%# Eval("subTotal", "{0:N2}") %>' ToolTip='<%# Eval("subTotal", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:6%;" class="subFormTableContentAlternate">
                                    &nbsp;
                                </div>
                                <div style="float:left; width:3%;" class="subFormTableContentAlternate">
                                    &nbsp;
                                </div>

                            </div>
                        </AlternatingItemTemplate>

                    </asp:Repeater>

                    <div style="width: 86%; margin: auto; clear:both;">

                        <div class="subFormTableContent" style="padding-left:2%;">
                            <asp:Label ID="lblNoFacility" runat="server" Text="No item found." Visible="false"></asp:Label>
                        </div>   
            
                    </div>

                    <div style="clear:both; height:40px;">&nbsp;</div>

                    <div class="formSectionStyle" style="margin-bottom:25px">
                        3. Amount Due:-
                    </div>

                    <div class="divAmountDue">
                        <%--Repeater Rented Facility--%>
                        <%--Repeater table header--%>
                        <div style="width:80%; margin:auto;">
                            <div style="float:left; width:10%; text-align:center;" class="subFormRepeaterHeader">
                                No
                            </div>
                            <div style="float:left; width:30%;" class="subFormRepeaterHeader">
                                Item
                            </div>
                            <div style="float:left; width:15%; text-align:right;" class="subFormRepeaterHeader">
                                Price
                            </div>
                            <div style="float:left; width:15%; text-align:right;" class="subFormRepeaterHeader">
                                Quantity
                            </div>
                            <div style="float:left; width:25%; text-align:right;" class="subFormRepeaterHeader">
                                Sub-Total
                            </div>
                            <div style="float:left; width:5%;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                        </div>

                        <asp:Repeater ID="RepeaterAmountDue" runat="server" OnItemDataBound="RepeaterAmountDue_ItemDataBound">

                            <ItemTemplate>

                                <div style="width:80%; margin:auto;">
                                    <div style="float:left; width:10%; text-align:center; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:30%; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblReservationFacilityID" runat="server" Text='<%# Eval("reservationFacilityID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:N2}") %>' ToolTip='<%# Eval("price", "{0:N2}") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' ToolTip='<%# Eval("quantity") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:25%; text-align:right; font-size:90%;" class="subFormTableContent">
                                        <asp:Label ID="lblSubTotal" runat="server" Text='<%# Eval("subTotal", "{0:N2}") %>' ToolTip='<%# Eval("subTotal", "{0:N2}") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:5%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                </div>
                
                            </ItemTemplate>

                            <AlternatingItemTemplate>
                                <div style="width:80%; margin:auto;">

                                    <div style="float:left; width:10%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:30%; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblReservationFacilityID" runat="server" Text='<%# Eval("reservationFacilityID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:N2}") %>' ToolTip='<%# Eval("price", "{0:N2}") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' ToolTip='<%# Eval("quantity") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:25%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblSubTotal" runat="server" Text='<%# Eval("subTotal", "{0:N2}") %>' ToolTip='<%# Eval("subTotal", "{0:N2}") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:5%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>

                                </div>
                            </AlternatingItemTemplate>
                        </asp:Repeater>

                        <div style="width: 80%; margin: auto; clear:both;">

                        <div class="subFormTableContent" style="padding-left:2%;">
                            <asp:Label ID="lblNoAmountDue" runat="server" Text="No item found." Visible="false"></asp:Label>
                        </div>   
            
                    </div>
                    </div>

                    <%--Display and record payment details--%>
                    <div class="divPaymentDetails">

                        <div style="height:20px;">&nbsp;</div>
                        <table style="width:90%; margin:auto;">
                            <tr>
                                <td style="width:50%; text-align:left;" class="formLabel">Total</td>
                                <td class="tableData" style="text-align:right;">
                                    RM
                                    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:50%; text-align:left;" class="formLabel">Tax(6%)</td>
                                <td class="tableData" style="text-align:right;">
                                    RM
                                    <asp:Label ID="lblTax" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:50%; text-align:left; font-size:20px;" class="formLabel requiredFieldLabel">Grand Total</td>
                                <td class="tableData requiredFieldLabel" style="text-align:right; font-size:20px; font-weight:600;">
                                    RM
                                    <asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <div style="height:40px;">&nbsp;</div>

                    </div>

                    <div style="clear:both; height:70px;">&nbsp;</div>

                </div>

                <div style="height:60px;">&nbsp;

                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backBtnStyle" OnClick="btnBack_Click"/>

                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="saveBtnStyle" OnClick="btnSave_Click"/>

                </div>
            </div>

        </div>

        <div style="clear:both; height:70px;">&nbsp;</div>

        </div>

    </form>
</body>
</html>
