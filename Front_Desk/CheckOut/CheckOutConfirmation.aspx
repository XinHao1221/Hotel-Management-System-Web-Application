<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="CheckOutConfirmation.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.CheckOut.CheckOutConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />
    <link rel="stylesheet" href="../../StyleSheet/MainMenuHeader.css" />

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

        .formBtnSave{
            margin-left:15px;
        }

    </style>

    <div>

        <%-- Page content --%>
        <div class="formHeader">
            Check Out
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

                <%--Other Service Charges--%>
                <div class="formSectionStyle" style="margin-bottom:25px;">
                    1. Other Services:-
                </div>

                <div style="margin-left:8%; margin-top:25px;">
                    <div style="float:left;">
                        <div class="subFormLabel requiredFieldLabel">
                            Service
                        </div>
                        <div style="float:left;">
                            <div>
                                <asp:TextBox ID="txtService" runat="server" CssClass="inputStyle" placeholder="Housekeeping" Width="100%"></asp:TextBox>
                            </div>
                            <div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtService" ErrorMessage="Please enter a value." ValidationGroup="add" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div style="float:left; margin-left:8%;">
                        <div class="subFormLabel requiredFieldLabel">
                            Charges
                        </div>
                        <div style="float:left;">
                            <div>
                                <asp:TextBox ID="txtCharges" runat="server" CssClass="inputStyle" placeholder="10.00" Width="50%"></asp:TextBox>
                            </div>
                            <div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCharges" ErrorMessage="Please enter a value." ValidationGroup="add" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Price must be in decimal!" CssClass="validatorStyle" ControlToValidate="txtCharges" Type="Double" ValidationGroup="add" Operator="DataTypeCheck"></asp:CompareValidator>
                            </div>
            
                        </div>
                    </div>

                    <div style="float:left;">

                        <asp:Button ID="btnSaveServiceCharges" runat="server" Text="Add" CssClass="subFormBtnSave" OnClick="btnSaveServiceCharges_Click" ValidationGroup="add" ToolTip="Add Equipment"/>
                    </div>
       
                </div>

                <div style="clear:both"></div>

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
                            <div style="float:left; width:49%;" class="subFormTableContent">
                                &nbsp;
                            </div>
                            <div style="float:left; width:3%;" class="subFormTableContent">
                                <asp:ImageButton ID="IBDeleteEquipment" runat="server" OnClick="IBDeleteServiceCharges_Click" ImageUrl="~/Image/delete_icon.png" CssClass="subFormIcon" ToolTip="Delete"/>
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
                            <div style="float:left; width:49%;" class="subFormTableContentAlternate">
                                &nbsp;
                            </div>
                            <div style="float:left; width:3%;" class="subFormTableContentAlternate">
                                <asp:ImageButton ID="IBDeleteEquipment" runat="server" OnClick="IBDeleteServiceCharges_Click" ImageUrl="~/Image/delete_icon.png" CssClass="subFormIcon" ToolTip="Delete"/>
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
                        2. Missing Equipment:-
                    </div>

                    <div style="width:86%; margin:auto;">
                        <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                            No
                        </div>
                        <div style="float:left; width:30%;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttEquipment" runat="server" Text="Equipment" ToolTip="Equipment"></asp:Label>
                        </div>
                        <div style="float:left; width:20%;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttRoomNo" runat="server" Text="Room No" ToolTip="Room No"></asp:Label>
                        </div>
                        <div style="float:left; width:20%; text-align:right;" class="subFormRepeaterHeader">
                            <asp:Label ID="ttFineCharges" runat="server" Text="Fine Charges" ToolTip="Fine Charges"></asp:Label>
                        </div>
                        <div style="float:left; width:22%;" class="subFormRepeaterHeader">
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
                                <div style="float:left; width:20%; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblRoomNo" runat="server" Text='<%# Eval("roomNO") %>' ToolTip='<%# Eval("roomNO") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContent">
                                    <asp:Label ID="lblFineCharges" runat="server" Text='<%# Eval("fineCharges", "{0:N2}") %>' ToolTip='<%# Eval("fineCharges", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:22%; font-size:90%;" class="subFormTableContent">
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
                                <div style="float:left; width:20%; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblRoomNo" runat="server" Text='<%# Eval("roomNO") %>' ToolTip='<%# Eval("roomNO") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:20%; text-align:right; font-size:90%;" class="subFormTableContentAlternate">
                                    <asp:Label ID="lblFineCharges" runat="server" Text='<%# Eval("fineCharges", "{0:N2}") %>' ToolTip='<%# Eval("fineCharges", "{0:N2}") %>'></asp:Label>
                                </div>
                                <div style="float:left; width:22%; font-size:90%;" class="subFormTableContentAlternate">
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
                
                <div style="clear:both; height:30px;">&nbsp;</div>

                <%--Other Service Charges--%>
                <div class="formSectionStyle" style="margin-bottom:25px;">
                    3. Feedback:-
                </div>

                <table style="width:100%; margin-left:7%;">
                    <tr>
                        <td class="formInput">
                            <asp:TextBox ID="txtFeedback" runat="server" CssClass="inputStyle" placeholder="Feedback" Width="50%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
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
            
                <asp:Button ID="formBtnBack" runat="server" Text="Back" CssClass="formBtnCancel" OnClick="LBBack_Click" ToolTip="Back"/>
                <asp:Button ID="btnCheckOut" runat="server" Text="Check Out" OnClick="btnCheckOut_Click" CssClass="formBtnSave" ToolTip="Check Out" ValidationGroup="next" />

            </center>         
        </div>
        
        <%--Popup Window--%>
        <div class="popup">
            <asp:Panel ID="PopupDelete" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:red;" class="popupTitle">Delete</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <asp:Label ID="lblPopupDeleteContent" runat="server" Text="Label"></asp:Label>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnCancel_Click"/>
                
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="popUpDeleteBtn" OnClick="btnDelete_Click"/>
            </asp:Panel>

            <asp:Panel ID="PopupCheckOut" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:#00ce1b;" class="popupTitle">Chceked Out</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <asp:Label ID="lblPopupCheckedOut" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblEmailStatus" runat="server" ></asp:Label>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnOK" runat="server" Text="OK" CssClass="popUpCancelBtn" OnClick="btnOK_Click"/>
                
                    
            </asp:Panel>

        </div>


        <%-- Popup Cover --%>
        <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
            &nbsp;
        </asp:Panel>

    </div>

</asp:Content>