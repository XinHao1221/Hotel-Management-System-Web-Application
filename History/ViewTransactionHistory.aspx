<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ViewTransactionHistory.aspx.cs" Inherits="Hotel_Management_System.Cashiering.ViewTransactionHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <style>
        .dropDownStyle, .inputStyle{
            height:20px;
            font-size: 85%;
        }

        .popupBox{
            min-width:800px;
            width:70%;
        }

    </style>
    <div>

        <%-- Page content --%>
        <div class="formHeader">
            Transaction History
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

        <asp:LinkButton ID="LBSurveyResponse" runat="server" CssClass="divLBStyle" OnClick="LBSurveyResponse_Click">
            <div class="formBtnEdit" style="float:left; height:40px; margin:0px 0px 0px 20px;">

                <img src="../../Image/survey_icon.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;" />
                <div style="float:left; margin:12px 0px 0px 10px; font-size:15px;">
                    Survey
                </div>
            </div>
        </asp:LinkButton>

        <div style="clear:both;">&nbsp;</div>

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
                    <div style="float:left; width:9%;" class="subFormTableContent">
                        &nbsp;
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
            4. Other Services:-
        </div>

        <%--Repeater table header--%>
        <div style="width:86%; margin:auto;">
            <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                No
            </div>
            <div style="float:left; width:25%;" class="subFormRepeaterHeader">
                Service
            </div>
            <div style="float:left; width:15%; text-align:right" class="subFormRepeaterHeader">
                Charges
            </div>
            <div style="float:left; width:47%;" class="subFormRepeaterHeader">
                &nbsp;
            </div>
            <div style="float:left; width:5%;" class="subFormRepeaterHeader">
                &nbsp;
            </div>
        </div>  

        <%--Repeater Table content--%>
        <asp:Repeater ID="RepeaterServiceCharges" runat="server">

            <ItemTemplate>
                <div style="width:86%; margin:auto;">
                    <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContent">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:25%; font-size:90%;" class="subFormTableContent">
                        <asp:Label ID="lblService" runat="server" Text='<%# Eval("service") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContent">
                        <asp:Label ID="lblCharges" runat="server" Text='<%# Eval("charges", "{0:N2}") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:52%;" class="subFormTableContent">
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
                        <asp:Label ID="lblService" runat="server" Text='<%# Eval("service") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:15%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblCharges" runat="server" Text='<%# Eval("charges", "{0:N2}") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:52%;" class="subFormTableContentAlternate">
                        &nbsp;
                    </div>
                </div> 
            </AlternatingItemTemplate>
    
        </asp:Repeater>

        <div style="width: 80%; margin: auto; clear:both;">

            <div class="subFormTableContent" style="padding-left:2%;">
                <asp:Label ID="lblNoServiceCharges" runat="server" Text="No item found!" Visible="false"></asp:Label>
            </div>           
        </div>

        <div style="clear:both; height:40px;">&nbsp;</div>

        <%--Display Rented Facility Details--%>
        <div>
            <div class="formSectionStyle" style="margin-bottom:25px;">
                5. Missing Equipment:-
            </div>

            <div style="width:86%; margin:auto;">
                <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                    No
                </div>
                <div style="float:left; width:30%;" class="subFormRepeaterHeader">
                    <asp:Label ID="ttEquipment" runat="server" Text="Equipment" ToolTip="Equipment"></asp:Label>
                </div>
                <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                    <asp:Label ID="ttFineCharges" runat="server" Text="Fine Charges" ToolTip="Fine Charges"></asp:Label>
                </div>
                <div style="float:left; width:42%;" class="subFormRepeaterHeader">
                    &nbsp;
                </div>
            </div>

            <asp:Repeater ID="RepeaterMissingEquipment" runat="server">

                <ItemTemplate>

                    <div style="width:86%; margin:auto;">
                        <div style="float:left; width:8%; font-size:90%; text-align:center;" class="subFormTableContent">
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                        </div>
                        <div style="float:left; width:30%; font-size:90%;" class="subFormTableContent">
                            <asp:Label ID="lblEquipmentID" runat="server" Text='<%# Eval("equipmentID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("title") %>' ToolTip='<%# Eval("title") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContent">
                            <asp:Label ID="lblFineCharges" runat="server" Text='<%# Eval("fineCharges", "{0:N2}") %>' ToolTip='<%# Eval("fineCharges", "{0:N2}") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:42%; font-size:90%;" class="subFormTableContent">
                            &nbsp;
                        </div>
                    </div>

                </ItemTemplate>

                <AlternatingItemTemplate>

                    <div style="width:86%; margin:auto;">
                        <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                        </div>
                        <div style="float:left; width:30%; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblEquipmentID" runat="server" Text='<%# Eval("equipmentID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("title") %>' ToolTip='<%# Eval("title") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblFineCharges" runat="server" Text='<%# Eval("fineCharges", "{0:N2}") %>' ToolTip='<%# Eval("fineCharges", "{0:N2}") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:42%; font-size:90%;" class="subFormTableContentAlternate">
                            &nbsp;
                        </div>
                    </div>

                </AlternatingItemTemplate>

            </asp:Repeater>

            <div style="width: 80%; margin: auto; clear:both;">
                <div class="subFormTableContent" style="padding-left:2%;">
                    <asp:Label ID="lblNoMissingEquipmentFound" runat="server" Text="No item found!" Visible="false"></asp:Label>
                </div>           
            </div>

        </div>

                <div style="clear:both; height:40px;">&nbsp;</div>

        <%--Display Rented Facility Details--%>
        <div>
            <div class="formSectionStyle" style="margin-bottom:25px;">
                6. Payment Details:-
            </div>

            <div style="width:86%; margin:auto;">
                <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                    No
                </div>
                <div style="float:left; width:15%; text-align:right;" class="subFormRepeaterHeader">
                    <asp:Label ID="ttPaymentDate" runat="server" Text="Date" ToolTip="Date"></asp:Label>
                </div>
                <div style="float:left; width:5%; font-size:90%;" class="subFormRepeaterHeader">
                    &nbsp;
                </div>
                <div style="float:left; width:20%;" class="subFormRepeaterHeader">
                    <asp:Label ID="ttPaymentMethod" runat="server" Text="Payment Method" ToolTip="Payment Method"></asp:Label>
                </div>
                <div style="float:left; width:15%;" class="subFormRepeaterHeader">
                    <asp:Label ID="ttReferenceNo" runat="server" Text="Reference No" ToolTip="Reference No"></asp:Label>
                </div>
                <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                    <asp:Label ID="ttAmount" runat="server" Text="Amount" ToolTip="Amount"></asp:Label>
                </div>
                <div style="float:left; width:17%;" class="subFormRepeaterHeader">
                    &nbsp;
                </div>
            </div>

            <asp:Repeater ID="RepeaterPayment" runat="server" OnItemDataBound="RepeaterPayment_ItemDataBound">

                <ItemTemplate>

                    <div style="width:86%; margin:auto;">
                        <div style="float:left; width:8%; font-size:90%; text-align:center;" class="subFormTableContent">
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                        </div>
                        <div style="float:left; width:15%; font-size:90%; text-align:right;" class="subFormTableContent">
                            <asp:Label ID="lblPaymentID" runat="server" Text='<%# Eval("paymentID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblPaymentDate" runat="server" Text='<%# Eval("date") %>' ToolTip='<%# Eval("date") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:5%; font-size:90%;" class="subFormTableContent">
                            &nbsp;
                        </div>
                        <div style="float:left; width:20%; font-size:90%;" class="subFormTableContent">
                            <asp:Label ID="lblPaymentMethod" runat="server" Text='<%# Eval("paymentMethod") %>' ToolTip='<%# Eval("paymentMethod") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:15%; font-size:90%;" class="subFormTableContent">
                            <asp:Label ID="lblReferenceNo" runat="server" Text='<%# Eval("referenceNo") %>' ToolTip='<%# Eval("referenceNo") %>'></asp:Label>&nbsp;
                        </div>
                        <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContent">
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("amount", "{0:N2}") %>' ToolTip='<%# Eval("amount", "{0:N2}") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:14%; font-size:90%;" class="subFormTableContent">
                            &nbsp;
                        </div>
                        <div style="float:left; width:3%; align-content:center;" class="subFormTableContent">
                            <asp:ImageButton ID="IBEditPaymentDetails" runat="server" OnClick="IBEditPaymentDetails_Click" ImageUrl="~/Image/edit_blue_icon.png" CssClass="subFormIcon" ToolTip="Edit"/>
                        </div>
                    </div>

                </ItemTemplate>

                <AlternatingItemTemplate>

                    <div style="width:86%; margin:auto;">
                        <div style="float:left; width:8%; text-align:center; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                        </div>
                        <div style="float:left; width:15%; font-size:90%; text-align:right;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblPaymentID" runat="server" Text='<%# Eval("paymentID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblPaymentDate" runat="server" Text='<%# Eval("date") %>' ToolTip='<%# Eval("date") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:5%; font-size:90%;" class="subFormTableContentAlternate">
                            &nbsp;
                        </div>
                        <div style="float:left; width:20%; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblPaymentMethod" runat="server" Text='<%# Eval("paymentMethod") %>' ToolTip='<%# Eval("paymentMethod") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:15%; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblReferenceNo" runat="server" Text='<%# Eval("referenceNo") %>' ToolTip='<%# Eval("referenceNo") %>'></asp:Label>&nbsp;
                        </div>
                        <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("amount", "{0:N2}") %>' ToolTip='<%# Eval("amount", "{0:N2}") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:14%; font-size:90%;" class="subFormTableContentAlternate">
                            &nbsp;
                        </div>
                        <div style="float:left; width:3%; align-content:center;" class="subFormTableContentAlternate">
                            <asp:ImageButton ID="IBEditPaymentDetails" runat="server" OnClick="IBEditPaymentDetails_Click" ImageUrl="~/Image/edit_blue_icon.png" CssClass="subFormIcon" ToolTip="Edit"/>
                        </div>
                    </div>

                </AlternatingItemTemplate>

            </asp:Repeater>

            <div style="width: 80%; margin: auto; clear:both;">
                <div class="subFormTableContent" style="padding-left:2%;">
                    <asp:Label ID="lblNoPaymentDetails" runat="server" Text="No item found!" Visible="false"></asp:Label>
                </div>           
            </div>

        </div>

        <%--Popup Box Facility Availability--%>
        <asp:Panel ID="PopupBoxEditPaymentDetails" runat="server" CssClass="popupBox" Visible="false" Width="">
                    
            <div class="popupBoxContainer">
   
                <div class="popupBoxHeader">
                    <%-- Popup Window Title --%>
                    <div style="float:left;">
                        <p style="color:#00ce1b;" class="popupBoxTitle">Edit Payment Details</p>
                    </div>

                    <div style="float:right;">
                        <asp:ImageButton ID="IBPopupBoxCloseIcon" runat="server" ImageUrl="~/Image/delete.png" CssClass="popupBoxCloseIcon" ToolTip="Close" OnClick="IBPopupBoxCloseIcon_Click"/>

                    </div>
                </div>

                <div style="clear:both; height:10px;"></div>

                <%-- Popup Window Body --%>
                <div class="popupBoxBody">
                        
                    <div style="width:100%; margin-top:-10px;">
                        <div style=" width:50%; float:left;">
                            <table style="width:100%;">
                                <tr>
                                    <td class="formLabel" style="text-align:left; font-size:16px; width:10%;">
                                            Date             
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td class="tableData" style="font-size:85%; width:90%;">
                                        <asp:Label ID="lblPopupBoxPaymentID" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPopupBoxDate" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width:50%; float:left;">
                            <table style="width:100%;">
                                <tr>
                                    <td class="formLabel" style="text-align:left; font-size:16px; width:20%;">
                                            Amount          
                                    </td>
                                    <td></td>
                                    <td class="tableData" style="font-size:85%; width:80%;">
                                    
                                        <asp:Label ID="lblPopupBoxAmount" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>

                    <div style="clear:both; height:10px;">&nbsp;</div>

                    <table style="width:100%;">
                        <tr>
                            <td class="formLabel requiredFieldLabel" style="text-align:right; font-size:16px; width:20%;">
                                    Payment Method               
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="formInput">
                                <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="dropDownStyle">
                                    <asp:ListItem>Cash</asp:ListItem>
                                    <asp:ListItem>Credit Card</asp:ListItem>
                                </asp:DropDownList>
                        </tr>
                        <tr>
                            <td class="formLabel" style="text-align:right; font-size:16px; width:20%;">Reference No</td>
                            <td class="tableSeperator"></td>
                            <td class="formInput">
                                <asp:TextBox ID="txtReferenceNo" runat="server" placeholder="R0000001" CssClass="inputStyle" Width="30%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="popupBoxFooter">
                        
                    <asp:Button ID="btnPopupBoxSave" runat="server" Text="Save" CssClass="popUpBoxSaveBtn" OnClick="btnPopupBoxSave_Click"/>

                </div>

            </div>

        </asp:Panel> 

        <%-- Popup Cover --%>
        <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
            &nbsp;
        </asp:Panel>

    </div>

</asp:Content>
