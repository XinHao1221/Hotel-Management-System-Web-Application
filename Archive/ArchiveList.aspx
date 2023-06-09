﻿<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ArchiveList.aspx.cs" Inherits="Hotel_Management_System.Archive.ArchiveList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/MainMenuHeader.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />

    <div>

        <%-- Page content --%>
        <div class="content">
                
            <div class="mainMenuHeader">
                <%--Title--%>
                <div class="mainMenuTitle">
                    Archive List
                </div>

                <%--Search Bar--%>
                <div class="mainMenuSearchBar">
                    <div style="display:flex; justify-content:center;">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="ID Number" CssClass="searchBarStyle" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                        <asp:LinkButton ID="LBMenuSearchBar" runat="server" OnClick="LBMenuSearchBar_Click" CssClass="optionContainer" ToolTip="Search">
                            <div class="searchIconContainer">
                                <img src="../../Image/search_icon.png" class="searchIconStyle" />
                            </div>
                        </asp:LinkButton>
                            
                    </div>
                </div>
            </div>

            <div style="clear:both; height:20px;">&nbsp;</div>

            <div style="width: 97%; margin: auto;">
                <div style="width:100%;">
                    <div style="float:left; width:5%;" class="tableHeader">
                        <div style="text-align:center;">
                            No
                        </div>
                    </div>
                    <div style="float:left; width:20%;" class="tableHeader">

                        <div style="margin-left:5%;">
                            <asp:Label ID="ttGuestName" runat="server" Text="Guest Name" ToolTip="Guest Name"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:20%;" class="tableHeader">
                        <div style="margin-left:5%;">
                            <asp:Label ID="ttIdNo" runat="server" Text="Id Number" ToolTip="Id Number"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:12.5%; text-align:right;" class="tableHeader">
                        <div style="margin-left:5%;">
                            <asp:Label ID="ttCheckIn" runat="server" Text="Check in" ToolTip="Check In"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:12.5%; text-align:right;" class="tableHeader">
                        <div style="margin-left:5%;">
                            <asp:Label ID="ttCheckOut" runat="server" Text="Check out" ToolTip="Check out"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:12.5%; text-align:right;" class="tableHeader">
                        <div style="margin-left:5%;">
                            <asp:Label ID="ttReservationDate" runat="server" Text="Reservation Date" ToolTip="Reservation Date"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:12.5%;" class="tableHeader">
                        <div style="margin-left:5%; text-align:right;">
                            <asp:Label ID="ttArchivedDate" runat="server" Text="Date Archived" ToolTip="Date Archived"></asp:Label>
                        </div>
                    </div>
                    <div style="width:5%; float:left;" class="tableHeader">
                        &nbsp;
                    </div>
                </div>
            </div>

                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">

                    <ItemTemplate>

                        <div style="width:97%; margin:auto;">

                            <asp:LinkButton ID="LBRepeater" runat="server" OnClick="LBRepeater_Click" CssClass="optionContainer">
                            
                                <div style="float:left; width:5%;" class="tableContent">
                                    <div style="text-align:center;" class="textOverflowStyle">
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                    </div>
                                </div>
                                <div style="float:left; width:20%;" class="tableContent">
                                    <div style="margin-left:5%;" class="textOverflowStyle">
                                        <asp:Label ID="lblReservationID" runat="server" Text='<%# Eval("ReservationID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblGuestName" runat="server" Text='<%# Eval("Name") %>' ToolTip='<%# Eval("Name") %>'></asp:Label>&nbsp;
                                    </div>

                                </div>
                                <div style="float:left; width:20%;" class="tableContent">
                                    <div style="margin-left:5%;" class="textOverflowStyle">
                                        <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNo")%>' ToolTip='<%# Eval("IDNo") %>'></asp:Label>&nbsp;
                                    </div>
                                </div>
                                <div style="float:left; width:12.5%; text-align:right;" class="tableContent">
                                    <div style="margin-left:5%;" class="textOverflowStyle">
                                        <asp:Label ID="lblCheckInDate" runat="server" Text='<%# Eval("CheckInDate")%>' ToolTip='<%# Eval("CheckInDate") %>'></asp:Label>&nbsp;
                                    </div>
                                </div>
                                <div style="float:left; width:12.5%; text-align:right;" class="tableContent">
                                    <div style="margin-left:5%;" class="textOverflowStyle">
                                        <asp:Label ID="lblCheckOutDate" runat="server" Text='<%# Eval("CheckOutDate")%>' ToolTip='<%# Eval("CheckOutDate") %>'></asp:Label>&nbsp;
                                    </div>
                                </div>
                                <div style="float:left; width:12.5%; text-align:right;" class="tableContent">
                                    <div style="margin-left:5%;" class="textOverflowStyle">
                                        <asp:Label ID="lblReservationDate" runat="server" Text='<%# Eval("ReservationDate")%>' ToolTip='<%# Eval("ReservationDate") %>'></asp:Label>&nbsp;
                                    </div>
                                </div>
                                <div style="float:left; width:12.5%;" class="tableContent">
                                    <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                        <asp:Label ID="lblDateArchived" runat="server" Text=""></asp:Label>&nbsp;
                                    </div>
                                </div>
                                <div style="min-width:3%; float:left;" class="tableContent">
                                    &nbsp;
                                </div>
                            </asp:LinkButton>
                                <div style="min-width:2%; float:left; right:0;" class="tableContent">
                                        
                                    <asp:ImageButton ID="IBMoreOption" runat="server" OnClick="IBMoreOption_Click" ImageUrl="~/Image/more_icon.png" CssClass="moreIcon" ToolTip="More Option"/>
                                </div>
                            
                            </div>

                            <asp:Panel ID="TableOptionMenu" runat="server" Visible="false" CssClass="tableOptionMenu">

                                <asp:LinkButton ID="LBRestoreReservation" runat="server" OnClick="LBRestoreReservation_Click" CssClass="optionContainer" Font-Underline="false">
                            

                                    <div class="tableOption" style="color:#00ce1b;">

                                        <img src="../../Image/restore_icon.png" class="tableOptionIcon" />
                                
                                        Restore

                                    </div>
                            
                                </asp:LinkButton>

                            </asp:Panel>

                    </ItemTemplate>

                    <AlternatingItemTemplate>
                        <div style="clear:both; width:97%; margin:auto;">
                            <asp:LinkButton ID="LBRepeater" runat="server" OnClick="LBRepeater_Click" CssClass="optionContainer">
                                
                                <div style="float:left; width:5%;" class="tableContentAlternate">
                                        <div style="text-align:center;" class="textOverflowStyle">
                                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                        </div>
                                    </div>
                                <div style="float:left; width:20%;" class="tableContentAlternate">
                                    <div style="margin-left:5%;" class="textOverflowStyle">
                                        <asp:Label ID="lblReservationID" runat="server" Text='<%# Eval("ReservationID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblGuestName" runat="server" Text='<%# Eval("Name") %>' ToolTip='<%# Eval("Name") %>'></asp:Label>&nbsp;
                                    </div>

                                </div>
                                <div style="float:left; width:20%;" class="tableContentAlternate">
                                    <div style="margin-left:5%;" class="textOverflowStyle">
                                        <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNo")%>' ToolTip='<%# Eval("IDNo") %>'></asp:Label>&nbsp;
                                    </div>
                                </div>
                                <div style="float:left; width:12.5%; text-align:right;" class="tableContentAlternate">
                                    <div style="margin-left:5%;" class="textOverflowStyle">
                                        <asp:Label ID="lblCheckInDate" runat="server" Text='<%# Eval("CheckInDate")%>' ToolTip='<%# Eval("CheckInDate") %>'></asp:Label>&nbsp;
                                    </div>
                                </div>
                                <div style="float:left; width:12.5%; text-align:right;" class="tableContentAlternate">
                                    <div style="margin-left:5%;" class="textOverflowStyle">
                                        <asp:Label ID="lblCheckOutDate" runat="server" Text='<%# Eval("CheckOutDate")%>' ToolTip='<%# Eval("CheckOutDate") %>'></asp:Label>&nbsp;
                                    </div>
                                </div>
                                <div style="float:left; width:12.5%; text-align:right;" class="tableContentAlternate">
                                    <div style="margin-left:5%;" class="textOverflowStyle">
                                        <asp:Label ID="lblReservationDate" runat="server" Text='<%# Eval("ReservationDate")%>' ToolTip='<%# Eval("ReservationDate") %>'></asp:Label>&nbsp;
                                    </div>
                                </div>
                                <div style="float:left; width:12.5%;" class="tableContentAlternate">
                                    <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                        <asp:Label ID="lblDateArchived" runat="server" Text=""></asp:Label>&nbsp;
                                    </div>
                                </div>
                                <div style="min-width:3%; float:left;" class="tableContentAlternate">
                                    &nbsp;
                                </div>
                            </asp:LinkButton>
                            <div style="width:2%; float:left;" class="tableContentAlternate">
                                    
                                <asp:ImageButton ID="IBMoreOption" runat="server" OnClick="IBMoreOption_Click" ImageUrl="~/Image/more_icon.png" CssClass="moreIcon" ToolTip="More Option"/>
                            </div>

                        </div>

                        <asp:Panel ID="TableOptionMenu" runat="server" Visible="false" CssClass="tableOptionMenuAlternate">

                            <asp:LinkButton ID="LBRestoreReservation" runat="server" OnClick="LBRestoreReservation_Click" CssClass="optionContainer" Font-Underline="false">
                            

                                <div class="tableOption" style="color:#00ce1b;">

                                    <img src="../../Image/restore_icon.png" class="tableOptionIcon" />
                                
                                    Restore

                                </div>
                            
                            </asp:LinkButton>

                        </asp:Panel>

                    </AlternatingItemTemplate>
                </asp:Repeater>

                <div style="width: 97%; margin: auto; clear:both;">

                    <div class="tableContent" style="padding-left:2%;">
                        <asp:Label ID="lblNoItemFound" runat="server" Text="No item found!" Visible="false"></asp:Label>&nbsp;
                    </div>
                
                </div>

                <div style="width: 97%; margin: auto;">
                    <div class="tableFooter" style="clear:both; padding-left:1%;">

                        <div style="width:60%; float: left;">
                            <asp:ImageButton ID="IBArrowLeft" runat="server" ImageUrl="~/Image/arrow_left.png" CssClass="arrowStyle" OnClick="IBArrowLeft_Click" ToolTip="Previous Page"/>
                            <asp:TextBox ID="txtPage" runat="server" AutoPostBack="True" OnTextChanged="txtPage_TextChanged" CssClass="txtPage"></asp:TextBox>
                            <div class="lblPage">
                                /<asp:Label ID="lblPage" runat="server" Text="2"></asp:Label>
                            </div>
                            <asp:ImageButton ID="IBArrowRight" runat="server" ImageUrl="~/Image/arrow_right.png" CssClass="arrowStyle" OnClick="IBArrowRight_Click" ToolTip="Next Page"/>


                            <asp:DropDownList ID="ddlItemPerPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" class="ddlstyle">
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem Selected="True">10</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                            </asp:DropDownList>

                            <div style="margin-left:6px; font-size:80%; margin-top:1.5px; float: left; width: 112px;">
                                items per page
                            </div>
                        </div>

                        <div style="max-width:40%; float:right; padding-right:2%; font-size:80%;">
                            <div style="float:left;">
                                <asp:Label ID="lblItemDisplayed" runat="server" Text="1 - 10"></asp:Label>
                            </div>

                            <div style="float:left; margin-left:4px; margin-right:4px;">
                                of
                            </div>
                            <div style="float:left;">
                                <asp:Label ID="lblTotalNoOfItem" runat="server" Text="20"></asp:Label>
                            </div>
                            <div style="float:left; margin-left:4px;">
                                items
                            </div>
                        </div>
                        &nbsp;
                    </div>
                </div>
            </div>

        <%-- Popup Window --%>
        <div class="popup">
            <asp:Panel ID="PopupRestoreReservation" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:#00ce1b;" class="popupTitle">Restore</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <asp:Label ID="lblPopupDeleteContent" runat="server" Text="Confirm to restore?"></asp:Label>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnCancel_Click"/>
                
                <asp:Button ID="btnRestore" runat="server" Text="Restore" CssClass="popUpActiveBtn" OnClick="btnRestore_Click"/>
            </asp:Panel>
        </div>
                
        <%-- Popup Cover --%>
        <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
            &nbsp;
        </asp:Panel>

    </div>

</asp:Content>
