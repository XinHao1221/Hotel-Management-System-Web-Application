<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="~/Dashboard/RoomAvailability.aspx.cs" Inherits="Hotel_Management_System.Dashboard.RoomAvailability" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/MainMenuHeader.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <div>

        <%-- Page content --%>
        <div class="content">

            <div class="formHeader">
                Room Availability
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
                
            <%--Filtering--%>
            <div style="">
                <div style="float:left; margin-left:5%;">
                    <div class="filteringLabel">
                        Room Type
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlRoomType" runat="server" CssClass="filteringDropDown" AutoPostBack="true" OnSelectedIndexChanged="ddlRoomType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div style="clear:both; height:50px;">&nbsp;</div>

            <%--Repeater table header--%>
            <div style="width:95%; margin-left:auto; margin-right:auto;">
                <div style="float:left; width:7%; text-align:center;" class="subFormRepeaterHeader">
                    No
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
                <div style="float:left; width:18%;" class="subFormRepeaterHeader">
                    Housekeeping
                </div>
                <div style="float:left; width:10%;" class="subFormRepeaterHeader">
                    Status
                </div>
                <div style="float:left; width:5%;" class="subFormRepeaterHeader">
                    &nbsp;
                </div>
            </div>  

            <%--Repeater Table content--%>
            <asp:Repeater ID="RepeaterRoomAvailability" runat="server" OnItemDataBound="RepeaterRoomAvailability_ItemDataBound">

                <ItemTemplate>
                    <div style="width:95%; margin-left:auto; margin-right:auto;">
                        <div style="float:left; width:7%; text-align:center;" class="subFormTableContent">
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
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
                        <div style="float:left; width:18%;" class="subFormTableContent">
                            <asp:Label ID="lblHousekeepingStatus" runat="server" Text='<%# Eval("houseKeepingStatus") %>' ToolTip='<%# Eval("houseKeepingStatus") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:10%;" class="subFormTableContent">
                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblAvailability" runat="server" Text='<%# Eval("available") %>' ToolTip='<%# Eval("available") %>' Visible="false"></asp:Label>
                        </div>
                        <div style="float:left; width:5%;" class="subFormTableContent">
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
                            <asp:Label ID="lblRoomID" runat="server" Text='<%# Eval("roomID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblRoomNumber" runat="server" Text='<%# Eval("roomNo") %>' ToolTip='<%# Eval("roomNo") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:20%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblRoomType" runat="server" Text=""></asp:Label>&nbsp;
                        </div>
                        <div style="float:left; width:20%;" class="subFormTableContentAlternate">
 
                            <asp:Label ID="lblFloor" runat="server" Text='<%# Eval("floor") %>' ToolTip='<%# Eval("floor") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:18%;" class="subFormTableContentAlternate">
 
                            <asp:Label ID="lblHousekeepingStatus" runat="server" Text='<%# Eval("houseKeepingStatus") %>' ToolTip='<%# Eval("houseKeepingStatus") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:10%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblAvailability" runat="server" Text='<%# Eval("available") %>' ToolTip='<%# Eval("available") %>' Visible="false"></asp:Label>
                        </div>
                        <div style="float:left; width:5%;" class="subFormTableContentAlternate">
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