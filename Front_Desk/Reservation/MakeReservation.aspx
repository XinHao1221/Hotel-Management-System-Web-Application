<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="MakeReservation.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Reservation.MakeReservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <%--Datalist Plugin--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="stylesheet" />

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <style>
        .guestListDropDown{
            border:none;
            border-bottom:2px solid rgb(128, 128, 128);
            padding:0 7px 0 7px;
            height:30px;
            font-size:90%;
            width:350px;
            height:60px;
        }

        .reservationFormPanel{
            border: 1px solid rgb(213 213 213);
            width: 80%;
            margin: auto;
            padding-left: 40px;
            padding-right: 40px;
            padding-top: 30px;
            padding-bottom:30px;
            margin-top: 20px;

        }

        /*Format repeater style*/
        .subFormRepeaterHeader {
            font-size:15px;
        }

        .subFormTableContent, .subFormTableContentAlternate{
            font-size:14.5px;
        }
    </style>

    

    <div>
        <%-- Page content --%>
        <div class="formHeader">
            Reservation Form
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

        <%--Form Section--%>
        <table style="width:100%;">
            <tr>
                <td class="formLabel requiredFieldLabel">
                        Guest Name             
                </td>
                <td class="tableSeperator"></td>
                <td class="formInput">
                    <asp:DropDownList ID="ddlGuest" runat="server" CssClass="guestListDropDown" AutoPostBack="True" OnSelectedIndexChanged="ddlGuest_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:LinkButton ID="LBAddGuest" runat="server" CssClass="tableData" OnClick="LBAddGuest_Click">add guest</asp:LinkButton>
                    
                </td>
            </tr>
        </table>

        <asp:Panel ID="PNStayDetails" runat="server" Visible="false">

            <%--Section Label--%>
            <div style="height:50px;">&nbsp;</div>

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
                            <td class="formLabel requiredFieldLabel">
                                    Check-In     
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:TextBox ID="txtCheckInDate" runat="server" CssClass="inputStyle inputDateStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtCheckInDate_TextChanged" AutoPostBack="true"></asp:TextBox>
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
                            <td class="formLabel requiredFieldLabel">
                                    Check-Out     
                            </td>
                            <td class="tableSeperator"></td>
                            <td class="tableData">
                                <asp:TextBox ID="txtCheckOutDate" runat="server" CssClass="inputStyle inputDateStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtCheckOutDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:Label ID="lblDurationOfStay" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Check-Out Date must be greater than Check-In date" ControlToCompare="txtCheckInDate" ControlToValidate="txtCheckOutDate" Operator="GreaterThan" Type="Date" EnableClientScript="False" CssClass="validatorStyle" ValidationGroup="save"></asp:CompareValidator>
                            </td>
                        </tr>
                    </table>

                </div> 
            </div>
        </asp:Panel>

        <div style="clear:both;">&nbsp;</div>

        <asp:Panel ID="PNReserveRoom" runat="server" Visible="false">

            <div class="formSectionStyle" style="margin-bottom:25px">
                2. Reserve Room:-
            </div>

            <div style="width:86%; margin:auto;">
                <asp:LinkButton ID="LBCheckAvailability" runat="server" OnClick="LBCheckAvailability_Click" CssClass="tableData">check room availability</asp:LinkButton>
            </div>

            <%--1st Reservation Form--%>
            <asp:Panel ID="PNReservationForm1" runat="server" Visible="true" CssClass="reservationFormPanel">
                <%--Display reservation form details--%>
                <div style="width:100%; margin:auto;">
                    <div style="width:50%; float:left;">

                        <table style="width:100%;">
                            <tr>
                                <td class="formLabel requiredFieldLabel">
                                        Room Type             
                                </td>
                                <td class="tableSeperator" style="width:5%;"></td>
                                <td class="tableData">
                                    <asp:DropDownList ID="ddlRoomType1" runat="server" OnSelectedIndexChanged="DropDownSelectRoomType" AutoPostBack="true" CssClass="dropDownStyle">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabel">
                                      Quantity    
                                </td>
                                <td class="tableSeperator"></td>
                                <td class="tableData">
                                    <asp:Label ID="lblQty1" runat="server" Text="1" CssClass="tableData"></asp:Label>
                                    <%--<asp:DropDownList ID="ddlRoomTypeQty1" runat="server" CssClass="dropDownStyle" AutoPostBack="true">
                                    </asp:DropDownList>--%>
                                </td>
                            </tr>
                        
                        </table>

                    </div>

                    <div style="width:50%; float:left;">

                        <table style="width:100%;">
                            <tr>
                                <td class="formLabel requiredFieldLabel">
                                        Adults             
                                </td>
                                <td class="tableSeperator" style="width:5%;"></td>
                                <td class="tableData">
                                    <asp:DropDownList ID="ddlAdults1" runat="server" Visible="true" CssClass="dropDownStyle" OnSelectedIndexChanged="DropDownAdults" AutoPostBack="true"> 
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtAdults1" runat="server" typr="number" Visible="false" CssClass="inputStyle" placeholder="4" Text="4" Width="20%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabel requiredFieldLabel requiredFieldLabel">
                                        Kids   
                                </td>
                                <td class="tableSeperator"></td>
                                <td class="tableData">
                                    <asp:DropDownList ID="ddlKids1" runat="server" Visible="true" CssClass="dropDownStyle" OnSelectedIndexChanged="DropDownKids" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtKids1" runat="server" typr="number" Visible="false" CssClass="inputStyle" placeholder="2" Text="0" Width="20%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabel requiredFieldLabel">
                                    <asp:Label ID="lblExtraBed1" runat="server" Text="ExtraBed" Visible="false"></asp:Label>
                                </td>
                                <td class="tableSeperator"></td>
                                <td class="tableData">
                                    <div style="padding-top:40px;">
                                        <asp:CheckBox ID="cbExtraBed1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxExtraBed" CssClass="formCheckBoxStyle" Width="50px" Visible="false"/>
                                    </div>
                                </td>
                            </tr>
                            
                        </table>

                    </div>
                </div>



                <div style="clear:both;"></div>

                <div style="float:right;">
                    <asp:LinkButton ID="LBAddReservationForm1" runat="server" OnClick="AddReservationForm" Visible="false" CssClass="tableData">add next</asp:LinkButton>
                </div>
                
                <div style="clear:both;"></div>
            </asp:Panel>

            <%--2nd Reservation Form--%>
            <asp:Panel ID="PNReservationForm2" runat="server" Visible="false" CssClass="reservationFormPanel">

                <%--Display reservation form details--%>
                <div style="width:100%; margin:auto;">
                    <div style="width:50%; float:left;">

                        <table style="width:100%;">
                            <tr>
                                <td class="formLabel requiredFieldLabel">
                                        Room Type             
                                </td>
                                <td class="tableSeperator" style="width:5%;"></td>
                                <td class="tableData">
                                    <asp:DropDownList ID="ddlRoomType2" runat="server" OnSelectedIndexChanged="DropDownSelectRoomType" AutoPostBack="true" CssClass="dropDownStyle">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabel">
                                      Quantity    
                                </td>
                                <td class="tableSeperator"></td>
                                <td class="tableData">
                                    <asp:Label ID="lblQty2" runat="server" Text="1" CssClass="tableData"></asp:Label>
                                    <%--<asp:DropDownList ID="ddlRoomTypeQty1" runat="server" CssClass="dropDownStyle" AutoPostBack="true">
                                    </asp:DropDownList>--%>
                                </td>
                            </tr>
                        
                        </table>

                    </div>

                    <div style="width:50%; float:left;">

                        <table style="width:100%;">
                            <tr>
                                <td class="formLabel requiredFieldLabel">
                                        Adults             
                                </td>
                                <td class="tableSeperator" style="width:5%;"></td>
                                <td class="tableData">
                                    <asp:DropDownList ID="ddlAdults2" runat="server" Visible="true" CssClass="dropDownStyle" OnSelectedIndexChanged="DropDownAdults" AutoPostBack="true"> 
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtAdults2" runat="server" typr="number" Visible="false" CssClass="inputStyle" placeholder="4" Text="4" Width="20%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabel requiredFieldLabel requiredFieldLabel">
                                        Kids   
                                </td>
                                <td class="tableSeperator"></td>
                                <td class="tableData">
                                    <asp:DropDownList ID="ddlKids2" runat="server" Visible="true" CssClass="dropDownStyle" OnSelectedIndexChanged="DropDownKids" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtKids2" runat="server" typr="number" Visible="false" CssClass="inputStyle" placeholder="2" Text="0" Width="20%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabel requiredFieldLabel">
                                    <asp:Label ID="lblExtraBed2" runat="server" Text="ExtraBed" Visible="false"></asp:Label>
                                </td>
                                <td class="tableSeperator"></td>
                                <td class="tableData">
                                    <div style="padding-top:40px;">
                                        <asp:CheckBox ID="cbExtraBed2" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxExtraBed" CssClass="formCheckBoxStyle" Width="50px" Visible="false"/>
                                    </div>
                                </td>
                            </tr>
                            
                        </table>

                    </div>
                </div>

                <div style="clear:both;"></div>

                <div style="float:right;">
                    <div style="float:left;">
                        <asp:LinkButton ID="LBDeleteReservationForm2" runat="server" OnClick="DeleteReservationForm" Visible="true" ForeColor="red" CssClass="tableData">delete</asp:LinkButton>
                    </div>
                  
                    <div style="float:left; margin-left:20px;">
                        <asp:LinkButton ID="LBAddReservationForm2" runat="server" OnClick="AddReservationForm" Visible="false" CssClass="tableData">add next</asp:LinkButton>
                    </div>
                    
                </div>
                
                <div style="clear:both;"></div>

            </asp:Panel>

            <%--3rd Reservation Form--%>
            <asp:Panel ID="PNReservationForm3" runat="server" Visible="false" CssClass="reservationFormPanel">

                <%--Display reservation form details--%>
                <div style="width:100%; margin:auto;">
                    <div style="width:50%; float:left;">

                        <table style="width:100%;">
                            <tr>
                                <td class="formLabel requiredFieldLabel">
                                        Room Type             
                                </td>
                                <td class="tableSeperator" style="width:5%;"></td>
                                <td class="tableData">
                                    <asp:DropDownList ID="ddlRoomType3" runat="server" OnSelectedIndexChanged="DropDownSelectRoomType" AutoPostBack="true" CssClass="dropDownStyle">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabel">
                                      Quantity    
                                </td>
                                <td class="tableSeperator"></td>
                                <td class="tableData">
                                    <asp:Label ID="lblQty3" runat="server" Text="1" CssClass="tableData"></asp:Label>
                                    <%--<asp:DropDownList ID="ddlRoomTypeQty1" runat="server" CssClass="dropDownStyle" AutoPostBack="true">
                                    </asp:DropDownList>--%>
                                </td>
                            </tr>
                        
                        </table>

                    </div>

                    <div style="width:50%; float:left;">

                        <table style="width:100%;">
                            <tr>
                                <td class="formLabel requiredFieldLabel">
                                        Adults             
                                </td>
                                <td class="tableSeperator" style="width:5%;"></td>
                                <td class="tableData">
                                    <asp:DropDownList ID="ddlAdults3" runat="server" Visible="true" CssClass="dropDownStyle" OnSelectedIndexChanged="DropDownAdults" AutoPostBack="true"> 
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtAdults3" runat="server" typr="number" Visible="false" CssClass="inputStyle" placeholder="4" Text="4" Width="20%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabel requiredFieldLabel requiredFieldLabel">
                                        Kids   
                                </td>
                                <td class="tableSeperator"></td>
                                <td class="tableData">
                                    <asp:DropDownList ID="ddlKids3" runat="server" Visible="true" CssClass="dropDownStyle" OnSelectedIndexChanged="DropDownKids" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtKids3" runat="server" typr="number" Visible="false" CssClass="inputStyle" placeholder="2" Text="0" Width="20%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="formLabel requiredFieldLabel">
                                    <asp:Label ID="lblExtraBed3" runat="server" Text="ExtraBed" Visible="false"></asp:Label>
                                </td>
                                <td class="tableSeperator"></td>
                                <td class="tableData">
                                    <div style="padding-top:40px;">
                                        <asp:CheckBox ID="cbExtraBed3" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxExtraBed" CssClass="formCheckBoxStyle" Width="50px" Visible="false"/>
                                    </div>
                                </td>
                            </tr>
                            
                        </table>

                    </div>
                </div>

                <div style="clear:both;"></div>

                <div style="float:right;">

                    <div style="float:left;">
                        <asp:LinkButton ID="LBDeleteReservationForm3" runat="server" OnClick="DeleteReservationForm" Visible="true" ForeColor="red" CssClass="tableData">delete</asp:LinkButton>
                    </div>
                    
                    <div style="float:left; margin-left:20px;">
                        <asp:LinkButton ID="LBAddReservationForm3" runat="server" OnClick="AddReservationForm" Visible="false" CssClass="tableData">add next</asp:LinkButton>
                    </div>
                    
                </div>
                
                <div style="clear:both;"></div>

            </asp:Panel>

        </asp:Panel>

        <div class="bottomBar">

            <center>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="save" />
                <asp:Button ID="formBtnCancel" runat="server" Text="Reset" OnClick="formBtnCancel_Click" CssClass="formBtnCancel" ToolTip="Reset"/>
            </center>
                    
        </div>
        

        <%--Popup Window--%>
        <div class="popup">

            <%--Popup Box--%>
            <asp:Panel ID="PopupBoxRoomAvailability" runat="server" CssClass="popupBox" Visible="false" Width="1000px">
                    
                <div class="popupBoxContainer">
   
                    <div class="popupBoxHeader">
                        <%-- Popup Window Title --%>
                        <div style="float:left;">
                            <p style="color:#00ce1b;" class="popupBoxTitle">Room Availability</p>
                        </div>

                        <div style="float:right;">
                            <asp:ImageButton ID="IBClosePopUpBox" runat="server" ImageUrl="~/Image/delete.png" CssClass="popupBoxCloseIcon" ToolTip="Close" OnClick="IBClosePopUpBox_Click"/>

                        </div>
                    </div>

                    <div style="clear:both;"></div>

                    <%-- Popup Window Body --%>
                    <div class="popupBoxBody">
                        
                        <%--Display Room Availability--%>
                        <%--Repeater table header--%>
                        <div style="width:100%; margin:auto;">
                            <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                                No
                            </div>
                            <div style="float:left; width:40%;" class="subFormRepeaterHeader">
                                Room Type
                            </div>
                            <div style="float:left; width:10%;" class="subFormRepeaterHeader">
                                Available
                            </div>
                            <div style="float:left; width:12%;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttBaseOccupancy" runat="server" Text="Base Occ" ToolTip="Base Occupancy"></asp:Label>
                            </div>
                            <div style="float:left; width:15%;" class="subFormRepeaterHeader">
                                <asp:Label ID="ttHigherOccupancy" runat="server" Text="Higher Occ" ToolTip="Higher Occupancy"></asp:Label>
                            </div>
                            <div style="float:left; width:15%;" class="subFormRepeaterHeader">
                                Status
                            </div>
                            <div style="float:left; width:0%;" class="subFormRepeaterHeader">
                                &nbsp;
                            </div>
                        </div>  

                        <%--Repeater Table content--%>
                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">

                            <ItemTemplate>
                                <div style="width:100%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center;" class="subFormTableContent">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:40%;" class="subFormTableContent">
                                        <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("roomTypeID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblRoomType" runat="server" Text='<%# Eval("roomType") %>' ToolTip='<%# Eval("roomType") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:10%;" class="subFormTableContent">
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' ToolTip='<%# Eval("quantity") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:12%;" class="subFormTableContent">
                                        <asp:Label ID="lblBaseOccupancy" runat="server" Text='<%# Eval("BaseOccupancy") %>' ToolTip='<%# Eval("BaseOccupancy") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%;" class="subFormTableContent">
                                        <asp:Label ID="lblHigherOccupancy" runat="server" Text='<%# Eval("HigherOccupancy") %>' ToolTip='<%# Eval("HigherOccupancy") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%;" class="subFormTableContent">
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status") %>' ToolTip='<%# Eval("status") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:0%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                </div>  
                            </ItemTemplate>

                            <AlternatingItemTemplate>
                                <div style="width:100%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:40%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("roomTypeID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblRoomType" runat="server" Text='<%# Eval("roomType") %>' ToolTip='<%# Eval("roomType") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:10%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' ToolTip='<%# Eval("quantity") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:12%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblBaseOccupancy" runat="server" Text='<%# Eval("BaseOccupancy") %>' ToolTip='<%# Eval("BaseOccupancy") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblHigherOccupancy" runat="server" Text='<%# Eval("HigherOccupancy") %>' ToolTip='<%# Eval("HigherOccupancy") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status") %>' ToolTip='<%# Eval("status") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:0%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                </div> 
                            </AlternatingItemTemplate>
    
                        </asp:Repeater> 

                    </div>
                </div>

            </asp:Panel> 
        </div>

        <%-- Popup Cover --%>
        <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
            &nbsp;
        </asp:Panel>

        <div style="clear:both; width:100%; height:70px;">
            &nbsp;
        </div>


    </div>

    <%--Java Script for data list--%>
    <script>
            $('#<%=ddlGuest.ClientID%>').chosen();
    </script>

</asp:Content>
