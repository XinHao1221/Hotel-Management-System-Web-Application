<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Reservation.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/MainMenuHeader.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />

    <style>
        .divReservationDetails{
            float:left;
            width:68%;
            min-height:600px;
        }

        .divPaymentDetails{
            float:left;
            width:30%;
            height:100%;
            min-height:600px;
            border: 2px solid rgb(180 180 180);
            
            box-shadow: 5px 10px;
            box-shadow: 3px 3px 5px rgb(149 149 149);
        }

        body{
            height:100%;
        }
    </style>

    <div>
        <%-- Page content --%>
        <div class="formHeader">
            Reservation Form
        </div>

        <asp:LinkButton ID="LBBack" runat="server" CssClass="divLBStyle" href='javascript:history.go(-1)'>
            <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
                <div class="formBackBtn" style="float:left;" tooltip="save">

                    <img src="../../Image/Back.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                    <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                        Back
                    </div>
                        
                </div>
            </div>
        </asp:LinkButton>
            
        <div style="clear:both; height:10px;">&nbsp;</div>

        <div>
            <%--Display Reservation Details--%>
            <div class="divReservationDetails">

                <div style="clear:both; height:30px;">&nbsp;</div>

                <div style="width:50%; float:left;">
                    <table style="width:100%;">
                        <tr>
                            <td class="formLabel">
                                    Guest Name             
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:Label ID="lblGuestName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formLabel">
                                    Check In           
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:Label ID="lblCheckInDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width:50%; float:left;">
                    <table style="width:100%;">
                        <tr>
                            <td class="formLabel">
                                    Duration of Stay             
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:Label ID="lblDurationOfStay" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formLabel">
                                    Check Out           
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:Label ID="lblCheckOutDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div style="clear:both; height:30px;">&nbsp;</div>

                <%--Display Rented Room Details--%>
                <div>
                    <div class="formSectionStyle" style="margin-bottom:25px;">
                        1. Rented Room:-
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
                        <div style="float:left; width:10%; text-align:right;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttAdults" runat="server" Text="Adults" ToolTip="Adults"></asp:Label>
                        </div>
                        <div style="float:left; width:10%; text-align:right;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttKids" runat="server" Text="Kids" ToolTip="Kids"></asp:Label>
                        </div>
                        <div style="float:left; width:12.5%; text-align:right;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttPrice" runat="server" Text="Price" ToolTip="Price"></asp:Label>
                        </div>
                        <div style="float:left; width:1.5%;" class="subFormRepeaterHeader">
                            &nbsp;
                        </div>
                        <div style="float:left; width:12.5%; text-align:right;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttExtraBed" runat="server" Text="Extra Bed" ToolTip="Extra Bed"></asp:Label>
                        </div>
                        <div style="float:left; width:1%;" class="subFormRepeaterHeader">
                            &nbsp;
                        </div>
                    </div> 

                    <%--Repeater Table content--%>
                    <asp:Repeater ID="RepeaterRentedRoomType" runat="server" OnItemDataBound="RepeaterRentedRoomType_ItemDataBound">

                        <ItemTemplate>
                            <div style="width:86%; margin:auto;">
                                <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                </div>
                                <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("roomTypeID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("date") %>' ToolTip='<%# Eval("date") %>' ></asp:Label>
                                </div>
                                <div style="float:left; width:2.5%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    &nbsp;
                                </div>
                                <div style="float:left; width:27%; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblRoomTypeName" runat="server" Text='<%# Eval("roomTypeName") %>' ToolTip='<%# Eval("roomTypeName") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblAdults" runat="server" Text='<%# Eval("adults") %>' ToolTip='<%# Eval("adults") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblKids" runat="server" Text='<%# Eval("kids") %>' ToolTip='<%# Eval("kids") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:12.5%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblRoomPrice" runat="server" Text='<%# Eval("roomPrice", "{0:N2}") %>' ToolTip='<%# Eval("roomPrice", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:1.5%;" class="subFormTableContent">
                                    &nbsp;
                                </div>
                                <div style="float:left; width:12.5%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblExtraBedPrice" runat="server" Text='<%# Eval("extraBedPrice", "{0:N2}") %>' ToolTip='<%# Eval("extraBedPrice", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:1%;" class="subFormTableContent">
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
                                    <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("roomTypeID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("date") %>' ToolTip='<%# Eval("date") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:2.5%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    &nbsp;
                                </div>
                                <div style="float:left; width:27%; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblRoomTypeName" runat="server" Text='<%# Eval("roomTypeName") %>' ToolTip='<%# Eval("roomTypeName") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblAdults" runat="server" Text='<%# Eval("adults") %>' ToolTip='<%# Eval("adults") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblKids" runat="server" Text='<%# Eval("kids") %>' ToolTip='<%# Eval("kids") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:12.5%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblRoomPrice" runat="server" Text='<%# Eval("roomPrice", "{0:N2}") %>' ToolTip='<%# Eval("roomPrice", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:1.5%; font-size:90%;" class="subFormTableContentAlternate">
                                    &nbsp;
                                </div>
                                <div style="float:left; width:12.5%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblExtraBedPrice" runat="server" Text='<%# Eval("extraBedPrice", "{0:N2}") %>' ToolTip='<%# Eval("extraBedPrice", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:1%;" class="subFormTableContentAlternate">
                                    &nbsp;
                                </div>
                            </div>
                        </AlternatingItemTemplate>

                    </asp:Repeater>
                </div>

                <div style="clear:both; height:30px;">&nbsp;</div>

                <%--Display Rented Facility Details--%>
                <div>
                    <div class="formSectionStyle" style="margin-bottom:25px;">
                        2. Rented Facility:-
                    </div>

                    <div style="width:86%; margin:auto;">
                        <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                            No
                        </div>
                        <div style="float:left; width:25%;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttFacilityName" runat="server" Text="Facility Name" ToolTip="Facility Name"></asp:Label>
                        </div>
                        <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttRentDate" runat="server" Text="Rent Date" ToolTip="Rent Date"></asp:Label>
                        </div>
                        <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttReturnDate" runat="server" Text="Return Date" ToolTip="Return Date"></asp:Label>
                        </div>
                        <div style="float:left; width:10%; text-align:right;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttQty" runat="server" Text="Qty" ToolTip="Quantity"></asp:Label>
                        </div>
                        <div style="float:left; width:15%; text-align:right;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttSubTotal" runat="server" Text="Sub Total" ToolTip="Sub Total"></asp:Label>
                        </div>
                        <div style="float:left; width:2%;" class="subFormRepeaterHeader">
                            &nbsp;
                        </div>
                    </div>

                    <asp:Repeater ID="RepeaterRentedFacility" runat="server" OnItemDataBound="RepeaterRentedFacility_ItemDataBound">

                        <ItemTemplate>

                            <div style="width:86%; margin:auto;">
                                <div style="float:left; width:8%; font-size:90%; text-align:center;" class="subFormTableContent">
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                </div>
                                <div style="float:left; width:25%; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblRentDate" runat="server" Text='<%# Eval("rentDate") %>' ToolTip='<%# Eval("rentDate") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblReturnDate" runat="server" Text='<%# Eval("returnDate") %>' ToolTip='<%# Eval("returnDate") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' ToolTip='<%# Eval("quantity") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblSubTotal" runat="server" Text='<%# Eval("subTotal", "{0:N2}") %>' ToolTip='<%# Eval("subTotal", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:2%; font-size:90%;" class="subFormTableContent">
                                    &nbsp;
                                </div>
                            </div>

                        </ItemTemplate>

                        <AlternatingItemTemplate>

                            <div style="width:86%; margin:auto;">
                                <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                </div>
                                <div style="float:left; width:25%; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblRentDate" runat="server" Text='<%# Eval("rentDate") %>' ToolTip='<%# Eval("rentDate") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblReturnDate" runat="server" Text='<%# Eval("returnDate") %>' ToolTip='<%# Eval("returnDate") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' ToolTip='<%# Eval("quantity") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblSubTotal" runat="server" Text='<%# Eval("subTotal", "{0:N2}") %>' ToolTip='<%# Eval("subTotal", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:2%; font-size:90%;" class="subFormTableContentAlternate">
                                    &nbsp;
                                </div>
                            </div>

                        </AlternatingItemTemplate>

                    </asp:Repeater>

                    <div style="width: 80%; margin: auto; clear:both;">
                        <div class="subFormTableContent" style="padding-left:2%;">
                            <asp:Label ID="lblNoItemFound" runat="server" Text="No facility rented." Visible="false"></asp:Label>
                        </div>           
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
                            <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:50%; text-align:left;" class="formLabel">Tax(6%)</td>
                        <td class="tableData" style="text-align:right;">
                            <asp:Label ID="lblTax" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:50%; text-align:left; font-size:20px;" class="formLabel requiredFieldLabel">Grand Total</td>
                        <td class="tableData requiredFieldLabel" style="text-align:right; font-size:20px; font-weight:600;">
                            <asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>

                <div style="height:40px;">&nbsp;</div>

                <div style="width:90%; margin:auto;">
                    <div class="filteringLabel">
                        Payment Method
                    </div>
                    <div style="margin-top:10px;">
                        <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="dropDownStyle">
                            <asp:ListItem>Cash</asp:ListItem>
                            <asp:ListItem>Credit Card</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div style="height:30px;">&nbsp;</div>

                <div style="width:90%; margin:auto;">
                    <div class="filteringLabel">
                        Reference No
                    </div>
                    <div style="margin-top:10px;">
                        <asp:TextBox ID="txtReferenceNo" runat="server" placeholder="R0000001" CssClass="inputStyle" Width="80%"></asp:TextBox>
                    </div>
                </div>
                

            </div>
        </div>

        <div style="clear:both; width:100%; height:70px;">
            &nbsp;
        </div>

        <div class="bottomBar">

            <center>
                <asp:Button ID="btnPay" runat="server" Text="Pay" OnClick="btnPay_Click" CssClass="formBtnSave" ToolTip="Confirm Payment" />
                <asp:Button ID="formBtnCancel" runat="server" Text="Cancel" OnClick="formBtnCancel_Click" CssClass="formBtnCancel" ToolTip="Cancel Reservation"/>
            </center>
                    
        </div>

        <div class="popup">
            <asp:Panel ID="PopupCancel" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:red;" class="popupTitle">Cancel Reservation</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <p>Confirm to cancel reservation? Reservation details would not be saved.</p>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnPopupCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnPopupCancel_Click"/>
                
                <asp:Button ID="btnPopupConfirmCancelReservation" runat="server" Text="Confirm" CssClass="popUpDeleteBtn" OnClick="btnPopupConfirmCancelReservation_Click"/>
            </asp:Panel>
        </div>

        <%-- Popup Cover --%>
        <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
            &nbsp;
        </asp:Panel>

    </div>

</asp:Content>



        

