<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="EquipmentCheckList.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.CheckIn.EquipmentCheckList" %>

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

        .cbStyle{
            margin-right : 30px;
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
            1. Equipment Checklist:-
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
                                    <td class="formLabel requiredFieldLabel">
                                            Room    
                                    </td>
                                    <td class="tableSeperator"></td>
                                    <td class="tableData">
                                        <asp:Label ID="lblSelectedRoomNo" runat="server" Text='<%# Eval("roomNo") %>'></asp:Label>
                                        <asp:Label ID="lblSelectedRoomID" runat="server" Text='<%# Eval("roomID") %>' Visible="false"></asp:Label>
                                    </td>
                                </tr>
                        
                            </table>

                        </div>

                        <div style="width:50%; float:left; margin-top:15px;">

                            <label class="formLabel">Equipment Checklist:</label>

                            <div style="clear:both; margin-top:10px;">&nbsp;</div>
                            <%--Display Equipment CheckList--%>
                            <asp:Repeater ID="RepeaterEquipmentCheckList" runat="server">

                                <ItemTemplate>

                                    <div style="float:left; margin-right:-20px; padding-top:7.5px;">
                                        <asp:CheckBox ID="cbEquipmentCheckList" runat="server" CssClass="formCheckBoxStyle" Width="50px"/>
                                    </div>
                                    
                                    <div style="float:left;">
                                        <asp:Label ID="lblEquipmentList" runat="server" CssClass="tableData" Text='<%# Eval("Title") %>'></asp:Label>
                                    </div>

                                    <asp:CustomValidator ID="CVEquipmentCheckList" runat="server" ErrorMessage="CustomValidator" EnableClientScript="false" 
                                        OnServerValidate="CVEquipmentCheckList_ServerValidate" ValidationGroup="next"></asp:CustomValidator>

                                    <div style="clear:both;"></div>

                                </ItemTemplate>

                            </asp:Repeater>

                        </div>
                    </div>

                </div>

            </ItemTemplate>

        </asp:Repeater>

        <div class="bottomBar">

            <center>
                <asp:Button ID="formBtnCancel" runat="server" Text="Reset" CssClass="formBtnCancel" ToolTip="Reset"/>
                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="next" />
            </center>
                    
        </div>

    </div>

</asp:Content>
