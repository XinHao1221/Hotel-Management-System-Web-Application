<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="OccupiedRoom.aspx.cs" Inherits="Hotel_Management_System.Dashboard.OccupiedRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/MainMenuHeader.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <div>

        <%-- Page content --%>
        <div class="content">

            <div class="formHeader">
                Occupied Room
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

            <%--Repeater table header--%>
            <div style="width:95%; margin-left:auto; margin-right:auto;">
                <div style="float:left; width:7%; text-align:center;" class="subFormRepeaterHeader">
                    No
                </div>
                <div style="float:left; width:20%;" class="subFormRepeaterHeader">
                    Guest Name
                </div>
                <div style="float:left; width:20%;" class="subFormRepeaterHeader">
                    Room Number
                </div>
                <div style="float:left; width:20%;" class="subFormRepeaterHeader">
                    Room Type
                </div>
                <div style="float:left; width:20%;" class="subFormRepeaterHeader">
                    Floor
                </div>
                <div style="float:left; width:10%;" class="subFormRepeaterHeader">
                    Status
                </div>
                <div style="float:left; width:3%;" class="subFormRepeaterHeader">
                    &nbsp;
                </div>
            </div> 

            <%--Repeater Table content--%>
            <asp:Repeater ID="RepeaterOccupiedRoom" runat="server" OnItemDataBound="RepeaterOccupiedRoom_ItemDataBound">

                <ItemTemplate>
                    <div style="width:95%; margin-left:auto; margin-right:auto;">
                        <div style="float:left; width:7%; text-align:center;" class="subFormTableContent">
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                        </div>
                        <div style="float:left; width:20%;" class="subFormTableContent">
                            <asp:Label ID="lblGuestName" runat="server" Text=""></asp:Label>&nbsp;
                        </div>
                        <div style="float:left; width:20%;" class="subFormTableContent">
                            <asp:Label ID="lblRoomID" runat="server" Text='<%# Eval("roomID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblRoomNumber" runat="server" Text='<%# Eval("roomNo") %>' ToolTip='<%# Eval("roomNo") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:20%;" class="subFormTableContent">
                            <asp:Label ID="lblRoomType" runat="server" Text=""></asp:Label>&nbsp;
                        </div>
                        <div style="float:left; width:20%;" class="subFormTableContent">
 
                            <asp:Label ID="lblFloor" runat="server" Text='<%# Eval("floor") %>' ToolTip='<%# Eval("floor") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:10%;" class="subFormTableContent">
                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblOvertimeStatus" runat="server" Text='<%# Eval("overtime") %>' ToolTip='<%# Eval("overtime") %>' Visible="false"></asp:Label>&nbsp;
                        </div>
                        <div style="float:left; width:3%;" class="subFormTableContent">
                            &nbsp;
                        </div>
                    </div>  
                </ItemTemplate>

                <AlternatingItemTemplate>
                    <div style="width:95%; margin-left:auto; margin-right:auto;">
                        <div style="float:left; width:7%; text-align:center;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                        </div>
                        <div style="float:left; width:20%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblGuestName" runat="server" Text=""></asp:Label>&nbsp;
                        </div>
                        <div style="float:left; width:20%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblRoomID" runat="server" Text='<%# Eval("roomID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblRoomNumber" runat="server" Text='<%# Eval("roomNo") %>' ToolTip='<%# Eval("roomNo") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:20%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblRoomType" runat="server" Text=""></asp:Label>&nbsp;
                        </div>
                        <div style="float:left; width:20%;" class="subFormTableContentAlternate">
 
                            <asp:Label ID="lblFloor" runat="server" Text='<%# Eval("floor") %>' ToolTip='<%# Eval("floor") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:10%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblOvertimeStatus" runat="server" Text='<%# Eval("overtime") %>' ToolTip='<%# Eval("overtime") %>' Visible="false"></asp:Label>&nbsp;
                        </div>
                        <div style="float:left; width:3%;" class="subFormTableContentAlternate">
                            &nbsp;
                        </div>
                    </div> 
                </AlternatingItemTemplate>
    
            </asp:Repeater>

            <div style="width:95%; margin-left:3%; clear:both;">

                <div class="subFormTableContent" style="padding-left:2%;">
                    <asp:Label ID="lblNoItemFound" runat="server" Text="No item found!" Visible="false"></asp:Label>
                </div>           
            </div>

        </div>

    </div>


</asp:Content>
