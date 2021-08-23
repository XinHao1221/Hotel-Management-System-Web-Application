<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="CheckIn.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.CheckIn.CheckIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">
    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />
    
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
    </style>

    <div>

        <%-- Page content --%>
        <div class="formHeader">
            Check In
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

        <div style="clear:both; height:50px;">&nbsp;</div>

        <div class="formSectionStyle" style="margin-bottom:25px;">
            1. Stay Details
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

        <div style="clear:both;">&nbsp;</div>

        <div class="formSectionStyle" style="margin-bottom:25px">
            2. Reserve Room:-
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
                                        <asp:Label ID="lblReservationRoomTypeID" runat="server" Visible="false" Text='<%# Eval("reservationRoomTypeID") %>'></asp:Label>
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
                                        <asp:LinkButton ID="LBSelectRoom" runat="server" ForeColor="#00ce1b" OnClick="LBSelectRoom_Click">Select Room</asp:LinkButton>
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
                                        <%--<asp:Label ID="lblExtraBed" runat="server" Text='<%# Eval("extraBedPrice") %>'></asp:Label>--%>
                                        <div style="padding-top:40px;">
                                            <%--<asp:CheckBox ID="cbExtraBed1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxExtraBed" CssClass="formCheckBoxStyle" Width="50px" Visible="false"/>--%>
                                        </div>
                                    </td>
                                </tr>
                            
                            </table>

                        </div>
                    </div>
                </div>

            </ItemTemplate>

        </asp:Repeater>

        <div style="clear:both;">&nbsp;</div>

        <div class="formSectionStyle" style="margin-bottom:25px">
            3. Rented Facility:-
        </div>

        <div style="clear:both; height:30px;">&nbsp;</div>

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

        <asp:Repeater ID="RepeaterRentedFacility" runat="server">

            <ItemTemplate>

                <div style="width:86%; margin:auto;">
                    <div style="float:left; width:6%; text-align:center;" class="subFormTableContent">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:20%;" class="subFormTableContent">
                        <asp:Label ID="lblReservationFacilityID" runat="server" Text='<%# Eval("reservationFacilityID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:10%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:N2}") %>' ToolTip='<%# Eval("price", "{0:N2}") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:10%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' ToolTip='<%# Eval("quantity") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:15%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblRentDate" runat="server" Text='<%# Eval("rentDate") %>' ToolTip='<%# Eval("rentDate") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:15%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblReturnDate" runat="server" Text='<%# Eval("returnDate") %>' ToolTip='<%# Eval("returnDate") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:15%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblSubTotal" runat="server" Text='<%# Eval("subTotal", "{0:N2}") %>' ToolTip='<%# Eval("subTotal", "{0:N2}") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:6%;" class="subFormTableContent">
                        &nbsp;
                    </div>
                    <div style="float:left; width:3%;" class="subFormTableContent">
                        <asp:ImageButton ID="IBDeleteRentedFacility" runat="server" OnClick="IBDeleteRentedFacility_Click" ImageUrl="~/Image/delete_icon.png" CssClass="subFormIcon" ToolTip="Delete"/>
                    </div>
                </div>
                
            </ItemTemplate>

            <AlternatingItemTemplate>
                <div style="width:86%; margin:auto;">

                    <div style="float:left; width:6%; text-align:center;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:20%;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblReservationFacilityID" runat="server" Text='<%# Eval("reservationFacilityID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:10%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:N2}") %>' ToolTip='<%# Eval("price", "{0:N2}") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:10%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' ToolTip='<%# Eval("quantity") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:15%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblRentDate" runat="server" Text='<%# Eval("rentDate") %>' ToolTip='<%# Eval("rentDate") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:15%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblReturnDate" runat="server" Text='<%# Eval("returnDate") %>' ToolTip='<%# Eval("returnDate") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:15%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblSubTotal" runat="server" Text='<%# Eval("subTotal", "{0:N2}") %>' ToolTip='<%# Eval("subTotal", "{0:N2}") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:6%;" class="subFormTableContentAlternate">
                        &nbsp;
                    </div>
                    <div style="float:left; width:3%;" class="subFormTableContentAlternate">
                        <asp:ImageButton ID="IBDeleteRentedFacility" runat="server" OnClick="IBDeleteRentedFacility_Click" ImageUrl="~/Image/delete_icon.png" CssClass="subFormIcon" ToolTip="Delete"/>
                    </div>

                </div>
            </AlternatingItemTemplate>

        </asp:Repeater>


        <%--Popup Box--%>
        <asp:Panel ID="PopupBoxSelectRoom" runat="server" CssClass="popupBox" Visible="false" Width="">
                    
            <div class="popupBoxContainer">
   
                <div class="popupBoxHeader">
                    <%-- Popup Window Title --%>
                    <div style="float:left;">
                        <p class="popupBoxTitle" style="color:#00ce1b;">Select Room</p>
                    </div>

                    <div style="float:right;">
                        <asp:ImageButton ID="IBClosePopUpBox" runat="server" ImageUrl="~/Image/delete.png" CssClass="popupBoxCloseIcon" ToolTip="Close" OnClick="IBClosePopUpBox_Click"/>
                    </div>
                </div>

                <div style="clear:both;"></div>
                
                <%-- Popup Window Body --%>
                <div class="popupBoxBody">
                    <div style="width:100%; margin-top:-10px;">
                        <div style=" width:50%; float:left;">
                            <table style="width:100%;">
                                <tr>
                                    <td class="formLabel requiredFieldLabel" style="text-align:left; font-size:16px;">
                                            Room Type            
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td class="tableData">
                                        <asp:Label ID="lblPopupBoxRoomType" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width:50%; float:left;">
                            <table style="width:100%;">
                                <tr>
                                    <td class="formLabel requiredFieldLabel" style="text-align:left; font-size:16px;">
                                            Date             
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td class="tableData">
                                        <asp:Label ID="lblPopupBoxDate" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <div style="clear:both; height:10px;">&nbsp;</div>

                    <div style="width:100%; margin:auto;">
                        <div style="float:left; width:8%; text-align:center; font-size:80%;" class="subFormRepeaterHeader">
                            No
                        </div>
                        <div style="float:left; width:15%; font-size:80%;" class="subFormRepeaterHeader">
                            Room No 
                        </div>
                        <div style="float:left; width:57%; font-size:80%;" class="subFormRepeaterHeader">
                            Room Features
                        </div>
                        <div style="float:left; width:15%; font-size:80%;" class="subFormRepeaterHeader">
                            Status
                        </div>
                        <div style="float:left; width:5%; font-size:80%;" class="subFormRepeaterHeader">
                            &nbsp;
                        </div>
                    </div>  

                </div>

                <div style="clear:both;">&nbsp;</div>



            </div>
        </asp:Panel> 

        <%-- Popup Cover --%>
        <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
            &nbsp;
        </asp:Panel>

    </div>

</asp:Content>
