<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ExtendCheckOut.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.ExtendCheckOut.ExtendCheckOut" %>

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

        .facilityPanelStyle{
            width:45%; 
            float:left; 
            /*background-color:red;*/
        }
    </style>

    <div>
        <%-- Page content --%>
        <div class="formHeader">
            Extend Check Out
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
                        <td class="formLabel">
                                Check-In     
                        </td>
                        <td class="tableSeperator"></td>
                        <td class="tableData">
                            <asp:Label ID="lblCheckIn" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                        
                </table>

            </div>
            <div style="width:55%; float:left;">

                <table style="width:90%;">
                    <tr>
                        <td class="formLabel" style="width:10%;">
                                IDNo          
                        </td>
                        <td class="tableSeperator" style="width:5%;"></td>
                        <td class="tableData" style="width:85%;">
                            <asp:Label ID="lblIDNo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel">
                                Check-Out     
                        </td>
                        <td class="tableSeperator"></td>
                        <td class="tableData">
                            <asp:TextBox ID="txtCheckOutDate" runat="server" CssClass="inputStyle inputDateStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtCheckOutDate_TextChanged" AutoPostBack="true" Visible="false"></asp:TextBox>
                            <asp:Label ID="lblCheckOutDate" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel">
                                Extend    
                        </td>
                        <td class="tableSeperator"></td>
                        <td class="tableData">
                            <asp:Label ID="lblExtend" runat="server" Text=""></asp:Label>&nbsp;days
                        </td>
                    </tr>
                </table>

            </div> 
        </div>

        <div style="clear:both; height:40px;">&nbsp;</div>

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
                                        <asp:LinkButton ID="LBSelectRoom" runat="server" ForeColor="#00ce1b" OnClick="LBSelectRoom_Click">Select Room</asp:LinkButton>
                                        
                                        <asp:CustomValidator ID="CVSelectedRoomNo" runat="server" ErrorMessage="Please select a room." CssClass="validatorStyle" 
                                            ValidationGroup="next" ValidateEmptyText="true" EnableClientScript="false" OnServerValidate="CVSelectedRoomNo_ServerValidate">
                                        </asp:CustomValidator>
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

        <div style="clear:both; height:30px;">&nbsp;</div>

        <div class="formSectionStyle" style="margin-bottom:25px">
            3. Rented Facility:-
        </div>

        <div style="width:86%; margin:auto;">
            <asp:LinkButton ID="LBCheckFacilityAvailability" runat="server" OnClick="LBCheckFacilityAvailability_Click" CssClass="tableData">check room availability</asp:LinkButton>
        </div>

        <div style="clear:both; height:30px;">&nbsp;</div>

        <div style="width:86%; margin:auto;">
            <div style="width:40%; float:left;">
                    
                <table style="width:100%;">
                    <tr>
                        <td class="formLabel requiredFieldLabel" style="width:20%;">
                                Facility             
                        </td>
                        <td class="tableSeperator" style="width:7%;"></td>
                        <td class="tableData" style="width:63%;">
                            <asp:DropDownList ID="ddlFacilityName" runat="server" CssClass="dropDownStyle" OnSelectedIndexChanged="ddlFacilityName_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlFacilityName" InitialValue="-- Please Select --" ErrorMessage="Please select an item." ValidationGroup="add" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel requiredFieldLabel">
                                Quantity     
                        </td>
                        <td class="tableSeperator"></td>
                        <td class="tableData">
                            <asp:DropDownList ID="ddlFacilityQty" runat="server" CssClass="dropDownStyle">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

            </div>

                
            <asp:Panel ID="PNFacilityRentedDate" runat="server" CssClass="facilityPanelStyle" Visible="false">
                <table style="width:100%;">
                    <tr>
                        <td class="formLabel requiredFieldLabel" style="width:20%;">
                                Rent Date            
                        </td>
                        <td class="tableSeperator" style="width:7%;"></td>
                        <td class="tableData" style="width:63%;">
                            <asp:TextBox ID="txtRentDate" runat="server" CssClass="inputStyle inputDateStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtRentDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel requiredFieldLabel">
                                Return Date     
                        </td>
                        <td class="tableSeperator"></td>
                        <td class="tableData">
                            <asp:TextBox ID="txtReturnDate" runat="server" CssClass="inputStyle inputDateStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtReturnDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <br />
                            <asp:CompareValidator ID="CVFacilityRentedDate" runat="server" ErrorMessage="Return Date must be greater than Rented Date" ControlToCompare="txtRentDate" ControlToValidate="txtReturnDate" Operator="GreaterThan" Type="Date" EnableClientScript="False" CssClass="validatorStyle" ValidationGroup="add" Enabled="false"></asp:CompareValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <div style="width:15%; float:left; line-height:105px;">

                <asp:Button ID="btnAddFacility" runat="server" Text="Add" CssClass="subFormBtnSave" OnClick="btnAddFacility_Click" ValidationGroup="add" ToolTip="Add Facility" style="vertical-align:bottom;"/>

            </div>
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
                        <asp:ImageButton ID="IBDeleteRentedFacility" runat="server" OnClick="IBDeleteRentedFacility_Click" ImageUrl="~/Image/delete_icon.png" CssClass="subFormIcon" ToolTip="Delete"/>&nbsp;
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
                        <asp:ImageButton ID="IBDeleteRentedFacility" runat="server" OnClick="IBDeleteRentedFacility_Click" ImageUrl="~/Image/delete_icon.png" CssClass="subFormIcon" ToolTip="Delete"/>&nbsp;
                    </div>

                </div>
            </AlternatingItemTemplate>

        </asp:Repeater>

        <div style="width: 86%; margin: auto; clear:both;">

            <div class="subFormTableContent" style="padding-left:2%;">
                <asp:Label ID="lblNoItemFound" runat="server" Text="No item found!" Visible="false"></asp:Label>
            </div>   
            
        </div>

        <div class="bottomBar">

            <center>
                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="next" />
                <asp:Button ID="formBtnCancel" runat="server" Text="Reset" OnClick="LBBack_Click" CssClass="formBtnCancel" ToolTip="Cancel"/>
            </center>
                    
        </div>

        <div style="clear:both;">&nbsp;</div>

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
                                    <td class="formLabel requiredFieldLabel" style="text-align:left; font-size:16px; width:20%;">
                                            Room Type            
                                    </td>
                                    <td></td>
                                    <td class="tableData" style="font-size:85%; width:80%;">
                                        <asp:Label ID="lblPopupBoxRoomTypeID" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPopupBoxRoomType" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width:50%; float:left;">
                            <table style="width:100%;">
                                <tr>
                                    <td class="formLabel requiredFieldLabel" style="text-align:left; font-size:16px; width:10%;">
                                            Date             
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td class="tableData" style="font-size:85%; width:90%;">
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
                        <div style="float:left; width:52%; font-size:80%;" class="subFormRepeaterHeader">
                            Room Features
                        </div>
                        <div style="float:left; width:7%; font-size:80%; text-align:right;" class="subFormRepeaterHeader">
                            Floor
                        </div>
                        <div style="float:left; width:5%; font-size:80%; text-align:right;" class="subFormRepeaterHeader">
                            &nbsp;
                        </div>
                        <div style="float:left; width:10%; font-size:80%;" class="subFormRepeaterHeader">
                            Status
                        </div>
                        <div style="float:left; width:3%; font-size:80%;" class="subFormRepeaterHeader">
                            &nbsp;
                        </div>
                    </div>

                    <asp:Repeater ID="RepeaterAvailableRoom" runat="server" OnItemCommand="RepeaterAvailableRoom_ItemCommand" OnItemDataBound="RepeaterAvailableRoom_ItemDataBound">

                        <ItemTemplate>
                            <div style="float:left; width:8%; text-align:center; font-size:75%;" class="subFormTableContent">
                                <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                            </div>
                            <div style="float:left; width:15%; font-size:75%;" class="subFormTableContent">
                                <asp:Label ID="lblRoomID" runat="server" Text='<%# Eval("roomID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblRoomNo" runat="server" Text='<%# Eval("roomNo") %>'></asp:Label>
                            </div>
                            <div style="float:left; width:52%; font-size:75%;" class="subFormTableContent">
                                <asp:Label ID="lblRoomFeatures" runat="server" Text=""></asp:Label>&nbsp;
                            </div>
                            <div style="float:left; width:7%; font-size:75%; text-align:right;" class="subFormTableContent">
                                <asp:Label ID="lblFloorNumber" runat="server" Text='<%# Eval("floorNumber") %>'></asp:Label>
                            </div>
                            <div style="float:left; width:5%; font-size:75%; text-align:right;" class="subFormTableContent">
                                &nbsp;
                            </div>
                            <div style="float:left; width:10%; font-size:75%;" class="subFormTableContent">
                                <asp:Label ID="lblHousekeepingStatus" runat="server" Text='<%# Eval("housekeepingStatus") %>'></asp:Label>&nbsp;
                            </div>
                            <div style="float:left; width:3%; padding-top:10px; padding-bottom:10px;" class="subFormTableContent">
                                <div style="line-height:20px; height:20px;">
                                    <asp:ImageButton ID="IBSelectRoom" runat="server" OnClick="IBSelectRoom_Click" ImageUrl="~/Image/checked_icon.png" CssClass="subFormIcon" ToolTip="Select Room" Width="20px" Height="20px"/>
                                </div>
                            </div>
                        </ItemTemplate>

                        <AlternatingItemTemplate>

                            <div style="float:left; width:8%; text-align:center; font-size:75%;" class="subFormTableContentAlternate">
                                <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                            </div>
                            <div style="float:left; width:15%; font-size:75%;" class="subFormTableContentAlternate">
                                <asp:Label ID="lblRoomID" runat="server" Text='<%# Eval("roomID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblRoomNo" runat="server" Text='<%# Eval("roomNo") %>'></asp:Label>
                            </div>
                            <div style="float:left; width:52%; font-size:75%;" class="subFormTableContentAlternate">
                                <asp:Label ID="lblRoomFeatures" runat="server" Text=""></asp:Label>&nbsp;
                            </div>
                            <div style="float:left; width:7%; font-size:75%; text-align:right;" class="subFormTableContentAlternate">
                                <asp:Label ID="lblFloorNumber" runat="server" Text='<%# Eval("floorNumber") %>'></asp:Label>
                            </div>
                            <div style="float:left; width:5%; font-size:75%; text-align:right;" class="subFormTableContentAlternate">
                                &nbsp;
                            </div>
                            <div style="float:left; width:10%; font-size:80%;" class="subFormTableContentAlternate">
                                <asp:Label ID="lblHousekeepingStatus" runat="server" Text='<%# Eval("housekeepingStatus") %>'></asp:Label>
                            </div>
                            <div style="float:left; width:3%; padding-top:10px; padding-bottom:10px;" class="subFormTableContentAlternate">
                                <div style="line-height:20px; height:20px;">

                                    <asp:ImageButton ID="IBSelectRoom" runat="server" OnClick="IBSelectRoom_Click" ImageUrl="~/Image/checked_icon.png" CssClass="subFormIcon" ToolTip="Select Room" Width="20px" Height="20px"/>
                            
                                </div>
                            </div>
                        </AlternatingItemTemplate>
                    </asp:Repeater>

                    <div style="width: 90%; margin: auto; clear:both;">

                        <div class="subFormTableContent" style="">
                            <asp:Label ID="lblNoAvailableRoom" runat="server" Text="No room found!" Visible="false"></asp:Label>
                        </div>   
            
                    </div>
                </div>

                <div style="clear:both;">&nbsp;</div>

            </div>
        </asp:Panel> 

        <%--Popup Box Facility Availability--%>
        <asp:Panel ID="PopupBoxFacilityAvailability" runat="server" CssClass="popupBox" Visible="false" Width="">
                    
            <div class="popupBoxContainer">
   
                <div class="popupBoxHeader">
                    <%-- Popup Window Title --%>
                    <div style="float:left;">
                        <p style="color:#00ce1b;" class="popupBoxTitle">Facility Availability</p>
                    </div>

                    <div style="float:right;">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/delete.png" CssClass="popupBoxCloseIcon" ToolTip="Close" OnClick="IBClosePopUpBox_Click"/>

                    </div>
                </div>

                <div style="clear:both; height:10px;"></div>

                <div style="float:left;">
                    <table style="width:28%; margin-top:-10px;">
                        <tr>
                            <td class="formLabel requiredFieldLabel" style="text-align:left; font-size:16px;">
                                    Date             
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td class="tableData">
                                <asp:TextBox ID="txtCheckFacilityDate" runat="server" CssClass="inputStyle inputDateStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtCheckFacilityDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>

                <div style="clear:both;"></div>

                <%-- Popup Window Body --%>
                <div class="popupBoxBody">
                        
                    <%--Display Facility Availability--%>
                    <%--Repeater table header--%>
                    <div style="width:100%; margin:auto;">
                        <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                            No
                        </div>
                        <div style="float:left; width:30%;" class="subFormRepeaterHeader">
                            Facility 
                        </div>
                        <div style="float:left; width:12%;" class="subFormRepeaterHeader">
                            Available
                        </div>
                        <div style="float:left; width:15%;" class="subFormRepeaterHeader">
                            Price Type
                        </div>
                        <div style="float:left; width:10%; text-align:right;" class="subFormRepeaterHeader">
                            Price
                        </div>
                        <div style="float:left; width:5%;" class="subFormRepeaterHeader">
                            &nbsp;
                        </div>
                        <div style="float:left; width:15%;" class="subFormRepeaterHeader">
                            Status
                        </div>
                        <div style="float:left; width:5%;" class="subFormRepeaterHeader">
                            &nbsp;
                        </div>
                    </div>  

                    <%--Repeater Table content--%>
                    <asp:Repeater ID="RepeaterFacilityAvailability" runat="server" OnItemDataBound="RepeaterFacilityAvailability_ItemDataBound">

                        <ItemTemplate>
                            <div style="width:100%; margin:auto;">
                                <div style="float:left; width:8%; text-align:center; font-size:80%;" class="subFormTableContent">
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                </div>
                                <div style="float:left; width:30%; font-size:80%;" class="subFormTableContent">
                                    <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:12%; font-size:80%;" class="subFormTableContent">
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("availableQty") %>' ToolTip='<%# Eval("availableQty") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:15%; font-size:80%;" class="subFormTableContent">
                                    <asp:Label ID="lblPriceType" runat="server" Text='<%# Eval("priceType") %>' ToolTip='<%# Eval("priceType") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:80%;" class="subFormTableContent">
                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:N2}") %>' ToolTip='<%# Eval("price") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:5%;" class="subFormTableContent">
                                    &nbsp;
                                </div>
                                <div style="float:left; width:15%; font-size:80%;" class="subFormTableContent">
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status") %>' ToolTip='<%# Eval("status") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:5%;" class="subFormTableContent">
                                    &nbsp;
                                </div>
                            </div>  
                        </ItemTemplate>

                        <AlternatingItemTemplate>
                            <div style="width:100%; margin:auto;">
                                <div style="float:left; width:8%; text-align:center; font-size:80%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                </div>
                                <div style="float:left; width:30%; font-size:80%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:12%; font-size:80%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("availableQty") %>' ToolTip='<%# Eval("availableQty") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:15%; font-size:80%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblPriceType" runat="server" Text='<%# Eval("priceType") %>' ToolTip='<%# Eval("priceType") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:10%; text-align:right; font-size:80%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:N2}") %>' ToolTip='<%# Eval("price") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:5%; font-size:80%;" class="subFormTableContentAlternate">
                                    &nbsp;
                                </div>
                                <div style="float:left; width:15%; font-size:80%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status") %>' ToolTip='<%# Eval("status") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:5%;" class="subFormTableContentAlternate">
                                    &nbsp;
                                </div>

                            </div> 
                        </AlternatingItemTemplate>
    
                    </asp:Repeater>

                </div>
            </div>

        </asp:Panel> 

        <%--Popup Window--%>
        <div class="popup">
            <asp:Panel ID="PopupFacilityNoAvailable" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:red;" class="popupTitle">Facility Unavailable</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    Facility no available. Please try different quantity.

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnOK" runat="server" Text="OK" CssClass="popUpCancelBtn" OnClick="btnOK_Click"/>
                
                    
            </asp:Panel>

            <asp:Panel ID="PopupDelete" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:red;" class="popupTitle">Delete</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <asp:Label ID="lblPopupDeleteContent" runat="server" Text=""></asp:Label>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnPopupDeleteCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnPopupCancel_Click"/>
                
                <asp:Button ID="btnPopupDelete" runat="server" Text="Delete" CssClass="popUpDeleteBtn" OnClick="btnPopupDelete_Click"/>
            </asp:Panel>

        </div>

        <%-- Popup Cover --%>
        <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
            &nbsp;
        </asp:Panel>

    </div>

</asp:Content>
