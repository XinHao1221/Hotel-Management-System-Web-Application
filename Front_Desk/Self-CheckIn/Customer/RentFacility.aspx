<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="RentFacility.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Self_CheckIn.Customer.RentFacility" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>

    <%--CSS--%>
    <link rel="stylesheet" href="../../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../../StyleSheet/RepeaterTable.css" />
    <link rel="stylesheet" href="SelfCheckInStyle.css" />

    <style>
        .facilityPanelStyle{
            width:45%; 
            float:left; 
            /*background-color:red;*/
        }
    </style>
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
                <div style="height:max-content;">

                    <div style="height:25px;">&nbsp;</div>

                    <div class="formSectionStyle" style="margin-bottom:25px;">
                        1. Stay Details:-
                    </div>

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

                    <div style="clear:both;">&nbsp;</div>

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

                    <div style="height:80px; clear:both;">&nbsp;</div>

                </div>

                <div style="height:60px;">&nbsp;

                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backBtnStyle" OnClick="btnBack_Click"/>

                    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="nextBtnStyle" OnClick="btnNext_Click"/>

                </div>
            </div>

            <div style="clear:both; height:70px;">&nbsp;</div>

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

            <%-- Popup Cover --%>
            <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
                &nbsp;
            </asp:Panel>

        </div>

        </div>

    </form>
</body>
</html>
