﻿<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Guest.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Guest.Guest" %>

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
                    Guest
                </div>

                <%--Search Bar--%>
                <div class="mainMenuSearchBar">
                    <div style="display:flex; justify-content:center;">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="Name or ID No." CssClass="searchBarStyle" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                        <asp:LinkButton ID="LBMenuSearchBar" runat="server" OnClick="LBMenuSearchBar_Click" CssClass="optionContainer" ToolTip="Search">
                            <div class="searchIconContainer">
                                <img src="../../Image/search_icon.png" class="searchIconStyle" />
                            </div>
                        </asp:LinkButton>
                            
                    </div>
                </div>

                <%--Add Button--%>
                <div class="menuHeaderButton">
                    <div style="float:right; margin-right:0%;">
                        <div class="menuHeaderSaveBtn" onclick="location.href='AddGuest.aspx';"> 
                            <div class="menuHeaderButtonStyle">
                                <div>
                                    <img src="../../Image/plus_white.png" width="12.5px" height="12.5px" style="float:left; margin:10px 0px 0px 15px;" />
                                    <div style="float:left; margin:10px 0px 0px 10px; font-size:12.5px;">
                                        Add
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--End Menu Header--%>

            <%--Repeater Header--%>
            <div style="width: 97%; margin: auto;">
                <div style="width:100%;">
                    <div style="float:left; width:5%;" class="tableHeader">
                        <div style="text-align:center;">
                            No
                        </div>
                    </div>
                    <div style="float:left; width:25%;" class="tableHeader">

                        <div style="margin-left:5%;">
                            <asp:Label ID="ttGuestName" runat="server" Text="Name" ToolTip="Name"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:15%;" class="tableHeader">
                        <div style="margin-left:5%;">
                            <asp:Label ID="ttNationality" runat="server" Text="Nationality" ToolTip="Nationality"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:10%;" class="tableHeader">
                        <div style="margin-left:5%;">
                            <asp:Label ID="ttIDType" runat="server" Text="ID Type" ToolTip="ID Type"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:15%;" class="tableHeader">
                        <div style="margin-left:5%;">
                            <asp:Label ID="ttIDNo" runat="server" Text="ID No." ToolTip="ID No."></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:15%;" class="tableHeader">
                        <div style="margin-left:5%;">
                            <asp:Label ID="ttPhoneNo" runat="server" Text="Phone No." ToolTip="Phone No."></asp:Label>
                        </div>
                    </div>
                    <div style="width:15%; float:left;" class="tableHeader">
                        &nbsp;
                    </div>
                </div>
            </div>
            <%--End Repeater Header--%>

            <%--Repeater Content--%>
            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">

                <ItemTemplate>

                    <div style="width:97%; margin:auto;">

                        <asp:LinkButton ID="LBRepeater" runat="server" OnClick="LBRepeater_Click" CssClass="optionContainer">
                            
                            <div style="float:left; width:5%;" class="tableContent">
                                <div style="text-align:center;" class="textOverflowStyle">
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                </div>
                            </div>
                            <div style="float:left; width:25%;" class="tableContent">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblGuestID" runat="server" Text='<%# Eval("GuestID") %>' Visible="false"></asp:Label>

                                    <asp:Label ID="lblGuestName" runat="server" Text='<%# Eval("Name") %>' ToolTip='<%# Eval("Name") %>'></asp:Label>&nbsp;
                                </div>

                            </div>
                            <div style="float:left; width:15%;" class="tableContent">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblNationality" runat="server" Text='<%# Eval("Nationality")%>' ToolTip='<%# Eval("Nationality") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContent">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblIDType" runat="server" Text='<%# Eval("IDType")%>' ToolTip='<%# Eval("IDType") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:15%;" class="tableContent">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNo")%>' ToolTip='<%# Eval("IDNo") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:15%;" class="tableContent">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblPhoneNo" runat="server" Text='<%# Eval("Phone")%>' ToolTip='<%# Eval("Phone") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="min-width:13%; float:left;" class="tableContent">
                                &nbsp;
                            </div>
                        </asp:LinkButton>
                            <div style="min-width:2%; float:left; right:0;" class="tableContent">

                                <asp:ImageButton ID="IBMoreOption" runat="server" OnClick="IBMoreOption_Click" ImageUrl="~/Image/more_icon.png" CssClass="moreIcon" ToolTip="More Option"/>
                            
                            </div>
                            
                        </div>
                        

                        <asp:Panel ID="TableOptionMenu" runat="server" Visible="false" CssClass="tableOptionMenu">

                        <asp:LinkButton ID="LBEdit" runat="server" OnClick="LBEdit_Click" CssClass="optionContainer" Font-Underline="false">
                            

                            <div class="tableOption">

                                <img src="../../Image/edit_icon.png" class="tableOptionIcon" />
                                
                                Edit

                            </div>
                            
                        </asp:LinkButton>

                        <asp:LinkButton ID="LBMakeReservation" runat="server" OnClick="LBMakeReservation_Click" CssClass="optionContainer" Font-Underline="false" ToolTip="Make Reservation">
                            

                            <div class="tableOption">

                                <div style="float:left;">
                                    <img src="../../Image/booking.png" class="tableOptionIcon" />
                                </div>
                                
                                <div style="float:left; padding-top:1.5px;">
                                    <asp:Label ID="lblChangeStatus" runat="server" Text="Label">Reservation</asp:Label>
                                </div>

                                <div style="clear:both;"></div>

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
                            <div style="float:left; width:25%;" class="tableContentAlternate">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblGuestID" runat="server" Text='<%# Eval("GuestID") %>' Visible="false"></asp:Label>

                                    <asp:Label ID="lblGuestName" runat="server" Text='<%# Eval("Name") %>' ToolTip='<%# Eval("Name") %>'></asp:Label>&nbsp;
                                </div>

                            </div>
                            <div style="float:left; width:15%;" class="tableContentAlternate">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblNationality" runat="server" Text='<%# Eval("Nationality")%>' ToolTip='<%# Eval("Nationality") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContentAlternate">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblIDType" runat="server" Text='<%# Eval("IDType")%>' ToolTip='<%# Eval("IDType") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:15%;" class="tableContentAlternate">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNo")%>' ToolTip='<%# Eval("IDNo") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:15%;" class="tableContentAlternate">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblPhoneNo" runat="server" Text='<%# Eval("Phone")%>' ToolTip='<%# Eval("Phone") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="min-width:13%; float:left;" class="tableContentAlternate">
                                &nbsp;
                            </div>
                        </asp:LinkButton>
                        <div style="width:2%; float:left;" class="tableContentAlternate">
                            
                            <asp:ImageButton ID="IBMoreOption" runat="server" OnClick="IBMoreOption_Click" ImageUrl="~/Image/more_icon.png" CssClass="moreIcon" ToolTip="More Option"/>
                        </div>

                    </div>

                        <asp:Panel ID="TableOptionMenu" runat="server" Visible="false" CssClass="tableOptionMenuAlternate">

                            <asp:LinkButton ID="LBEdit" runat="server" OnClick="LBEdit_Click" CssClass="optionContainer" Font-Underline="false">
                            

                                <div class="tableOption">

                                    <img src="../../Image/edit_icon.png" class="tableOptionIcon" />
                                
                                    Edit

                                </div>
                            
                            </asp:LinkButton>

                            <asp:LinkButton ID="LBMakeReservation" runat="server" OnClick="LBMakeReservation_Click" CssClass="optionContainer" Font-Underline="false" ToolTip="Make Reservation">
                            

                            <div class="tableOption">

                                <img src="../../Image/booking.png" class="tableOptionIcon" />
                                
                                Reservation

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
             <%--End Repeater Content--%>

            <%--Start Repeater Footer--%>
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
            <%--End Table Repeater--%>
        </div>
    </div>

</asp:Content>