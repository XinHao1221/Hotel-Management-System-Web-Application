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

        .facilityPanelStyle{
            width:45%; 
            float:left; 
            /*background-color:red;*/
        }

        .popupBox{
            min-width:1000px;
            width:80%;
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
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlGuest" InitialValue="-- Please Select --" ErrorMessage="Please select an item." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
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
                                <br />
                                <asp:RequiredFieldValidator ID="RFVTxtCheckInDate" runat="server" ErrorMessage="Please select an date." CssClass="validatorStyle" ControlToValidate="txtCheckInDate" ValidationGroup="save"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="txtCheckOutDate" runat="server" CssClass="inputStyle inputDateStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtCheckOutDate_TextChanged" AutoPostBack="true" Visible="false"></asp:TextBox>
                                <asp:Label ID="lblDurationOfStay" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Check-Out Date must be greater than Check-In Date" ControlToCompare="txtCheckInDate" ControlToValidate="txtCheckOutDate" Operator="GreaterThan" Type="Date" EnableClientScript="False" CssClass="validatorStyle" ValidationGroup="save"></asp:CompareValidator>
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
                                    <br />
                                    <asp:Label ID="lblNoRoomSelected" runat="server" Text="Please Select an item" CssClass="validatorStyle" Visible="false"></asp:Label>
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
                                        <asp:ListItem>0</asp:ListItem>
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
                                        <asp:ListItem>0</asp:ListItem>
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
                                        <asp:ListItem>0</asp:ListItem>
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
                        <%--<asp:LinkButton ID="LBAddReservationForm3" runat="server" OnClick="AddReservationForm" Visible="false" CssClass="tableData">add next</asp:LinkButton>--%>
                    </div>
                    
                </div>
                
                <div style="clear:both;"></div>

            </asp:Panel>

            <div style="clear:both; height:30px;">&nbsp;</div>

            <div class="formSectionStyle" style="margin-bottom:25px">
                3. Rent Facilities:-
            </div>

            <div style="width:86%; margin:auto;">
                <asp:LinkButton ID="LBCheckFacilityAvailability" runat="server" OnClick="LBCheckFacilityAvailability_Click" CssClass="tableData">check room availability</asp:LinkButton>
            </div>

            <div style="height:30px;">&nbsp;</div>

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
            
            <%--Repeater Table content--%>
            <asp:Repeater ID="RepeaterRentedFacility" runat="server">

                <ItemTemplate>
                    <div style="width:86%; margin:auto;">
                        <div style="float:left; width:6%; text-align:center;" class="subFormTableContent">
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                        </div>
                        <div style="float:left; width:20%;" class="subFormTableContent">
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

            <div style="width: 86%; margin: auto; clear:both;">

                <div class="subFormTableContent" style="padding-left:2%;">
                    <asp:Label ID="lblNoItemFound" runat="server" Text="No item found!" Visible="false"></asp:Label>
                </div>           
            </div>

        </asp:Panel>

        <div class="bottomBar">

            <center>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="save" />
                <asp:Button ID="formBtnCancel" runat="server" Text="Reset" OnClick="formBtnCancel_Click" CssClass="formBtnCancel" ToolTip="Reset"/>
            </center>
                    
        </div>
        
        <%--Popup Window--%>
        <div class="popup">
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

            <asp:Panel ID="PopupReset" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:red;" class="popupTitle">Reset Text Field?</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <p>All text fields will be reset!</p>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnPopupResetCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnPopupCancel_Click"/>
                
                <asp:Button ID="btnPopupConfirmReset" runat="server" Text="Confirm" CssClass="popUpDeleteBtn" OnClick="btnPopupConfirmReset_Click"/>
            </asp:Panel>

        </div>

        <%--Popup Window--%>
        <div class="popup">

            <%--Popup Box--%>
            <%--Popup Box Room Availability--%>
            <asp:Panel ID="PopupBoxRoomAvailability" runat="server" CssClass="popupBox" Visible="false" Width="">
                    
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

                    <div>
                        <div style="float:left;">
                            <%--Room Availability--%>
                            <asp:LinkButton ID="LBCheckRoomAvailability" runat="server" CssClass="divLBStyle" ToolTip="Check Room Availability" OnClick="LBCheckRoomAvailability_Click">
                                <div style="margin-bottom:15px;">
                                    <asp:Panel ID="PNBtnAvailability" runat="server">

                                            <div style="font-size:15px; text-align:center; margin-top:10px; ">
                                                Availability
                                            </div>

                                    </asp:Panel>
                                    
                                </div>
                            </asp:LinkButton>
                        </div>
                        <div style="float:left; margin-left:20px;">
                            <%--Room Price--%>
                            <asp:LinkButton ID="LBCheckRoomPrice" runat="server" CssClass="divLBStyle" ToolTip="Check Room Price" OnClick="LBCheckRoomPrice_Click">
                                <div>
                                    <asp:Panel ID="PNBtnPrice" runat="server">

                                            <div style="font-size:15px; text-align:center; margin-top:10px; ">
                                                Price
                                            </div>

                                    </asp:Panel>
                                </div>
                            </asp:LinkButton>

                        </div>
                    </div>

                    <div style="clear:both;"></div>

                    <%-- Popup Window Body --%>
                    <div class="popupBoxBody">
                       
                        <%--Display Room Availability--%>
                        <%--Repeater table header--%>
                        <asp:Panel ID="PNDisplayRoomAvailability" runat="server">
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
                            <asp:Repeater ID="RepeaterRoomAvailability" runat="server" OnItemDataBound="Repeater1_ItemDataBound">

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
                        </asp:Panel>

                        <%--Display Room Price--%>
                        <asp:Panel ID="PNDisplayRoomPrice" runat="server">
                                <table style="width:28%; margin-top:-10px;">
                                    <tr>
                                        <td class="formLabel requiredFieldLabel" style="text-align:left; font-size:16px;">
                                                Date             
                                        </td>
                                        <td>&nbsp;&nbsp;</td>
                                        <td class="tableData">
                                            <asp:TextBox ID="txtRoomPriceDate" runat="server" CssClass="inputStyle inputDateStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtRoomPriceDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                            <div style="clear:both;"></div>

                            <div style="width:100%; margin:auto;">
                                <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                                    No
                                </div>
                                <div style="float:left; width:40%;" class="subFormRepeaterHeader">
                                    Room Type
                                </div>
                                <div style="float:left; width:10%; text-align:right;" class="subFormRepeaterHeader">
                                    Price
                                </div>
                                <div style="float:left; width:42%;" class="subFormRepeaterHeader">
                                    &nbsp;
                                </div>
                            </div>  

                            <%--Repeater Table content--%>
                            <asp:Repeater ID="RepeaterRoomPrice" runat="server">

                                <ItemTemplate>
                                    <div style="width:100%; margin:auto;">
                                        <div style="float:left; width:8%; text-align:center;" class="subFormTableContent">
                                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                        </div>
                                        <div style="float:left; width:40%;" class="subFormTableContent">
                                            <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("roomTypeID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblRoomType" runat="server" Text='<%# Eval("roomType") %>' ToolTip='<%# Eval("roomType") %>'></asp:Label>
                                        </div>
                                        <div style="float:left; width:10%; text-align:right;" class="subFormTableContent">
                                            <asp:Label ID="lblRoomPrice" runat="server" Text='<%# Eval("roomPrice") %>' ToolTip='<%# Eval("roomPrice") %>'></asp:Label>
                                        </div>
                                        <div style="float:left; width:42%;" class="subFormTableContent">
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
                                        <div style="float:left; width:10%; text-align:right;" class="subFormTableContentAlternate">
                                            <asp:Label ID="lblRoomPrice" runat="server" Text='<%# Eval("roomPrice") %>' ToolTip='<%# Eval("roomPrice") %>'></asp:Label>
                                        </div>
                                        <div style="float:left; width:42%;" class="subFormTableContentAlternate">
                                            &nbsp;
                                        </div>
                                    </div> 
                                </AlternatingItemTemplate>
    
                            </asp:Repeater> 

                        </asp:Panel>
                    </div>
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
                        <asp:Repeater ID="RepeaterFacilityAvailability" runat="server" OnItemDataBound="Repeater2_ItemDataBound">

                            <ItemTemplate>
                                <div style="width:100%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center;" class="subFormTableContent">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:30%;" class="subFormTableContent">
                                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:12%;" class="subFormTableContent">
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("availableQty") %>' ToolTip='<%# Eval("availableQty") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%;" class="subFormTableContent">
                                        <asp:Label ID="lblPriceType" runat="server" Text='<%# Eval("priceType") %>' ToolTip='<%# Eval("priceType") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:10%; text-align:right;" class="subFormTableContent">
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:N2}") %>' ToolTip='<%# Eval("price") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:5%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:15%;" class="subFormTableContent">
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status") %>' ToolTip='<%# Eval("status") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:5%;" class="subFormTableContent">
                                        &nbsp;
                                    </div>
                                </div>  
                            </ItemTemplate>

                            <AlternatingItemTemplate>
                                <div style="width:100%; margin:auto;">
                                    <div style="float:left; width:8%; text-align:center;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:30%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblFacilityID" runat="server" Text='<%# Eval("facilityID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblFacilityName" runat="server" Text='<%# Eval("facilityName") %>' ToolTip='<%# Eval("facilityName") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:12%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("availableQty") %>' ToolTip='<%# Eval("availableQty") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:15%;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblPriceType" runat="server" Text='<%# Eval("priceType") %>' ToolTip='<%# Eval("priceType") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:10%; text-align:right;" class="subFormTableContentAlternate">
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:N2}") %>' ToolTip='<%# Eval("price") %>'></asp:Label>
                                    </div>
                                    <div style="float:left; width:5%;" class="subFormTableContentAlternate">
                                        &nbsp;
                                    </div>
                                    <div style="float:left; width:15%;" class="subFormTableContentAlternate">
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
