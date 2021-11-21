<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="RoomMove.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.GuestInHouse.RoomMove" %>

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

        .inputStyle{
            font-size:80%;
        }
    </style>

    <div>

        <%-- Page content --%>
        <div class="formHeader">
            Room Move
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
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                         Remark
                                    </td>
                                    <td class="tableSeperator"></td>
                                    <td class="tableData">
                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("remark") %>'></asp:Label>
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

                        <div style="clear:both;"></div>

                    </div>
                    
                </div>

            </ItemTemplate>

        </asp:Repeater>

        <div style="clear:both;">&nbsp;</div>

        <div class="bottomBar">

            <center>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="next" />
            </center>
                    
        </div>

        <%--Popup Window--%>
        <div class="popup">
            <asp:Panel ID="PopupRoomMove" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:#00ce1b;" class="popupTitle">Room Moved</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <asp:Label ID="lblPopupRoomMove" runat="server" Text="Room Number updated."></asp:Label>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnOK" runat="server" Text="OK" CssClass="popUpCancelBtn" OnClick="btnOK_Click"/>
                
                    
            </asp:Panel>
        </div>

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

        <%-- Popup Cover --%>
        <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
            &nbsp;
        </asp:Panel>

        <asp:Panel ID="PopupMove" runat="server" CssClass="popupWindow" Visible="false">

            <%-- Popup Window Title --%>
            <p style="color:#00ce1b;" class="popupTitle">Move Room</p>

            <%-- Popup Window Body --%>
            <div class="popupBody">

                Move to <asp:Label ID="lblSelectedRoomNo" runat="server" Text=""></asp:Label>?
                <asp:Label ID="lblRoomID" runat="server" Text="" Visible="false"></asp:Label>

                <div style="margin-top:20px;">
                    <div style="margin-top:10px;">
                        <asp:TextBox ID="txtRemark" runat="server" placeholder="Remark" CssClass="inputStyle" Width="70%"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter a value." ValidationGroup="move" CssClass="validatorStyle" ControlToValidate="txtRemark"></asp:RequiredFieldValidator>
                    </div>
                </div>

            </div>

            <div style="height:30px;">&nbsp;</div>

            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnCancel_Click"/>
                
            <asp:Button ID="btnMove" runat="server" Text="Move" CssClass="popUpActiveBtn" ValidationGroup="move" OnClick="btnMove_Click"/>

        </asp:Panel>

        <div style="clear:both; width:100%; height:70px;">
            &nbsp;
        </div>

    </div>

</asp:Content>
