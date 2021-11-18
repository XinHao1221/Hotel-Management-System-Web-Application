<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ReservationDetails.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.GuestInHouse.ReservationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <div>

        <%-- Page content --%>
        <div class="formHeader">
            Guest in House
        </div>

        <asp:LinkButton ID="LBBack" runat="server" OnClick="LBBack_Click" CssClass="divLBStyle">
            <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
                <div class="formBackBtn" style="float:left;" tooltip="save">

                    <img src="../../Image/Back.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                    <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                        Back
                    </div>
                        
                </div>
            </div>
        </asp:LinkButton>

        <asp:LinkButton ID="LBRoomMove" runat="server" CssClass="divLBStyle" OnClick="LBRoomMove_Click">
            <div class="formBtnEdit" style="float:left; height:40px; margin:0px 0px 0px 20px; background-color:rgb(0, 137, 250);">
                <div style="float:left; margin:12px 0px 0px -5px; font-size:15px;">
                    <div style="font-size:15px; margin:0px 0px 10px 15px; color:white;">
                            Room Move
                    </div>
                </div>
            </div>
        </asp:LinkButton>

        <asp:LinkButton ID="LBExtendCheckOut" runat="server" CssClass="divLBStyle" OnClick="LBExtendCheckOut_Click">
            <div class="formBtnEdit" style="float:left; height:40px; margin:0px 0px 0px 20px; background-color:rgb(0, 206, 27);">
                <div style="float:left; margin:12px 0px 0px 12.5px; font-size:15px;">
                    <div style="font-size:15px; margin:0px 0px 10px 15px; color:white;">
                            Extend
                    </div>
                </div>
            </div>
        </asp:LinkButton>

        <asp:LinkButton ID="LBCheckOut" runat="server" CssClass="divLBStyle" OnClick="LBCheckOut_Click">
            <div class="formBtnEdit" style="float:left; height:40px; margin:0px 0px 0px 20px; background-color:red;">
                <div style="float:left; margin:12px 0px 0px 0px; font-size:15px;">
                    <div style="font-size:15px; margin:0px 0px 10px 15px; color:white;">
                            Check Out
                    </div>
                </div>
            </div>
        </asp:LinkButton>

        <div style="clear:both; height:40px;">&nbsp;</div>

        <div class="formSectionStyle" style="margin-bottom:25px;">
            1. Stay Details:-
        </div>

        <%--Display guest details--%>
        <div style="width:90%; margin:auto;">
            <div style="width:45%; float:left;">

                <table style="width:100%;">
                    <tr>
                        <td class="formLabel">
                                Guest Name              
                        </td>
                        <td class="tableSeperator" style="width:5%;"></td>
                        <td class="tableData">
                            <asp:Label ID="lblGuestName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width:10%;">
                                IDNo          
                        </td>
                        <td class="tableSeperator" style="width:5%;"></td>
                        <td class="tableData" style="width:85%;">
                            <asp:Label ID="lblIDNo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>   
                </table>

            </div>
            <div style="width:55%; float:left;">

                <table style="width:90%;">
                    <tr>
                        <td class="formLabel">
                                Check-In     
                        </td>
                        <td class="tableSeperator"></td>
                        <td class="tableData">
                            <asp:Label ID="lblCheckIn" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel">
                                Check-Out     
                        </td>
                        <td class="tableSeperator"></td>
                        <td class="tableData">
                            <asp:Label ID="lblCheckOut" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel">
                                Duration of Stay    
                        </td>
                        <td class="tableSeperator"></td>
                        <td class="tableData">
                            <asp:Label ID="lblDurationOfStay" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>

            </div> 

        </div>

        <div style="clear:both; height:40px;">&nbsp;</div>

        <div class="formSectionStyle" style="margin-bottom:25px">
            2. Reserve Room:-
        </div>

        <div>
            <div style="width:86%; margin:auto;">
                <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                    <asp:Label ID="ttNo" runat="server" Text="No" ToolTip="No"></asp:Label>
                </div>
                <div style="float:left; width:10%; text-align:right;" class="subFormRepeaterHeader">
                    <asp:Label ID="ttDate" runat="server" Text="Date" ToolTip="Date"></asp:Label>
                </div>
                <div style="float:left; width:2.5%; text-align:right;" class="subFormRepeaterHeader">
                    &nbsp;
                </div>
                <div style="float:left; width:20%;" class="subFormRepeaterHeader">
                    <asp:Label ID="ttRoomType" runat="server" Text="Room Type" ToolTip="Room Type"></asp:Label>
                </div>
                <div style="float:left; width:12%;" class="subFormRepeaterHeader">
                    <asp:Label ID="ttRoomNo" runat="server" Text="Room No" ToolTip="Room No"></asp:Label>
                </div>
                <div style="float:left; width:8.5%; text-align:right;" class="subFormRepeaterHeader">
                    <asp:Label ID="ttAdults" runat="server" Text="Adults" ToolTip="Adults"></asp:Label>
                </div>
                <div style="float:left; width:8.5%; text-align:right;" class="subFormRepeaterHeader">
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
                <div style="float:left; width:4%;" class="subFormRepeaterHeader">
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
                        <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContent">
                            <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("roomTypeID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("date") %>' ToolTip='<%# Eval("date") %>' ></asp:Label>
                        </div>
                        <div style="float:left; width:2.5%; text-align:right; font-size:90%;" class="subFormTableContent">
                            &nbsp;
                        </div>
                        <div style="float:left; width:20%; font-size:90%;" class="subFormTableContent">
                            <asp:Label ID="lblRoomTypeName" runat="server" Text='<%# Eval("roomTypeName") %>' ToolTip='<%# Eval("roomTypeName") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:12%; font-size:90%;" class="subFormTableContent">
                            <asp:Label ID="lblRoomID" runat="server" Text='<%# Eval("roomID") %>' ToolTip='<%# Eval("roomID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblRoomNo" runat="server" Text='<%# Eval("roomNo") %>' ToolTip='<%# Eval("roomNo") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:8.5%; text-align:right; font-size:90%;" class="subFormTableContent">
                            <asp:Label ID="lblAdults" runat="server" Text='<%# Eval("adults") %>' ToolTip='<%# Eval("adults") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:8.5%; text-align:right; font-size:90%;" class="subFormTableContent">
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
                        <div style="float:left; width:4%;" class="subFormTableContent">
                            &nbsp;
                        </div>
                    </div>

                </ItemTemplate>

                <AlternatingItemTemplate>
                    <div style="width:86%; margin:auto;">
                        <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                        </div>
                        <div style="float:left; width:10%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("roomTypeID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("date") %>' ToolTip='<%# Eval("date") %>' ></asp:Label>
                        </div>
                        <div style="float:left; width:2.5%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                            &nbsp;
                        </div>
                        <div style="float:left; width:20%; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblRoomTypeName" runat="server" Text='<%# Eval("roomTypeName") %>' ToolTip='<%# Eval("roomTypeName") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:12%; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblRoomNo" runat="server" Text='<%# Eval("roomNo") %>' ToolTip='<%# Eval("roomNo") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:8.5%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblAdults" runat="server" Text='<%# Eval("adults") %>' ToolTip='<%# Eval("adults") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:8.5%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblKids" runat="server" Text='<%# Eval("kids") %>' ToolTip='<%# Eval("kids") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:12.5%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblRoomPrice" runat="server" Text='<%# Eval("roomPrice", "{0:N2}") %>' ToolTip='<%# Eval("roomPrice", "{0:N2}") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:1.5%;" class="subFormTableContentAlternate">
                            &nbsp;
                        </div>
                        <div style="float:left; width:12.5%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblExtraBedPrice" runat="server" Text='<%# Eval("extraBedPrice", "{0:N2}") %>' ToolTip='<%# Eval("extraBedPrice", "{0:N2}") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:4%;" class="subFormTableContentAlternate">
                            &nbsp;
                        </div>
                    </div>
                </AlternatingItemTemplate>

            </asp:Repeater>

        </div>
        
        <div style="clear:both; height:40px;">&nbsp;</div>

        <div class="formSectionStyle" style="margin-bottom:25px">
            3. Rented Facility:-
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

        <asp:Repeater ID="RepeaterRentedFacility" runat="server" OnItemDataBound="RepeaterRentedFacility_ItemDataBound">

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
                    <div style="float:left; width:9%;" class="subFormTableContent">
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
                    <div style="float:left; width:9%;" class="subFormTableContentAlternate">
                        &nbsp;
                    </div>
                </div>
            </AlternatingItemTemplate>

        </asp:Repeater>

        <div style="width: 86%; margin: auto; clear:both;">

            <div class="subFormTableContent" style="padding-left:2%;">
                <asp:Label ID="lblNoFacilityFound" runat="server" Text="No item found!" Visible="false"></asp:Label>
            </div>   
            
        </div>

        <div style="clear:both; height:40px;">&nbsp;</div>

        <div class="formSectionStyle" style="margin-bottom:25px">
            4. Room Move History:-
        </div>

        <div style="width:86%; margin:auto;">
            <div style="float:left; width:6%; text-align:center;" class="subFormRepeaterHeader">
                No
            </div>
            <div style="float:left; width:15%;" class="subFormRepeaterHeader">
                From Room
            </div>
            <div style="float:left; width:15%;" class="subFormRepeaterHeader">
                To Room
            </div>
            <div style="float:left; width:30%;" class="subFormRepeaterHeader">
                Remark
            </div>
            <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                Date Move
            </div>
            <div style="float:left; width:14%;" class="subFormRepeaterHeader">
                &nbsp;
            </div>
        </div>

        <asp:Repeater ID="RepeaterRoomMoveHistory" runat="server" OnItemDataBound="RepeaterRoomMoveHistory_ItemDataBound">

            <ItemTemplate>

                <div style="width:86%; margin:auto;">
                    <div style="float:left; width:6%; text-align:center; font-size:90%;" class="subFormTableContent">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:15%; font-size:90%;" class="subFormTableContent">
                        <asp:Label ID="lblReservationFacilityID" runat="server" Text='<%# Eval("RoomMoveID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("FromRoom") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:15%; font-size:90%;" class="subFormTableContent">
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("ToRoom") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:30%; font-size:90%;" class="subFormTableContent">
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:20%; font-size:90%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:14%;" class="subFormTableContent">
                        &nbsp;
                    </div>
                </div>
                
            </ItemTemplate>

            <AlternatingItemTemplate>
                <div style="width:86%; margin:auto;">

                    <div style="float:left; width:6%; text-align:center;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:15%; font-size:90%;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblReservationFacilityID" runat="server" Text='<%# Eval("RoomMoveID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("FromRoom") %>' Visible="false"></asp:Label>
                    </div>
                    <div style="float:left; width:15%; font-size:90%;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("ToRoom") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:30%; font-size:90%;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:20%; font-size:90%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:14%;" class="subFormTableContentAlternate">
                        &nbsp;
                    </div>
                </div>
            </AlternatingItemTemplate>

        </asp:Repeater>

        <div style="width: 86%; margin: auto; clear:both;">

            <div class="subFormTableContent" style="padding-left:2%;">
                <asp:Label ID="lblNoRoomMoveHistory" runat="server" Text="No item found!" Visible="false"></asp:Label>
            </div>   
            
        </div>

        <div style="clear:both; height:40px;">&nbsp;</div>

    </div>

</asp:Content>
