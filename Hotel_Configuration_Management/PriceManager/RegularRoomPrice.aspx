<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" AutoEventWireup="true" CodeBehind="RegularRoomPrice.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.PriceManager.RegularRoomPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">



    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/MainMenuHeader.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />

    <style>
        body{
            width:100%;
            height:100%;
        }

        .content{

        }

       
        .auto-style1 {
            
        }

    </style>

    <div>
        <asp:LinkButton ID="LBRegular" runat="server" CssClass="divLBStyle" ToolTip="Manage Regular Price" PostBackUrl="~/Hotel_Configuration_Management/PriceManager/RegularRoomPrice.aspx">
            <div style="margin:20px 0px 20px 50px;">
                <div class="formOptionSelectedBtn" style="float:left;" tooltip="save">

                    <div style="font-size:15px; text-align:center; margin-top:10px; ">
                        Regular
                    </div>
                        
                </div>
            </div>
        </asp:LinkButton>

        <div style="float:left; width:30px;">
            &nbsp;
        </div>

        <asp:LinkButton ID="LBSpecial" runat="server" CssClass="divLBStyle" ToolTip="Manage Special Price" PostBackUrl="~/Hotel_Configuration_Management/PriceManager/SpecialRoomPrice.aspx">
            <div style="margin:20px 0px 20px 50px;">
                <div class="formBackBtn" style="float:left;" tooltip="save">

                    <div style="font-size:15px; text-align:center; margin-top:10px; ">
                        Special
                    </div>

                </div>
            </div>
        </asp:LinkButton>
            
        <div style="clear:both; margin-top:100px;">&nbsp;</div>

        <%-- Page content --%>
        <div class="content">
                
            <div class="mainMenuHeader">
                <%--Title--%>
                <div class="mainMenuTitle">
                    Manage Regular Price
                </div>

                <%--Search Bar--%>
                <div class="mainMenuSearchBar">
                    <div style="display:flex; justify-content:center;">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="Room Type" CssClass="searchBarStyle" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                        <asp:LinkButton ID="LBMenuSearchBar" runat="server" OnClick="LBMenuSearchBar_Click" CssClass="optionContainer" ToolTip="Search">
                            <div class="searchIconContainer">
                                <img src="../../Image/search_icon.png" class="searchIconStyle" />
                            </div>
                        </asp:LinkButton>
                            
                    </div>
                </div>

                <%--Edit Button--%>
                <div class="menuHeaderButton">
                    <div style="float:right; margin-right:0%;">
                        <div class="menuHeaderSaveBtn" onclick="location.href='EditRegularPrice.aspx';" style="background-color: rgb(0, 137, 250);">
                            
                                <div class="menuHeaderButtonStyle">
                                    <div>
                                        <img src="../../Image/edit_white_icon.png" width="12.5px" height="12.5px" style="float:left; margin:10px 0px 0px 15px;" />
                                        <div style="float:left; margin:10px 0px 0px 10px; font-size:12.5px;">
                                            Edit
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
            <div style="clear:both;"></div>

            <div style="width: 97%; margin: auto;">
                <div style="width:100%;">
                    <div style="float:left; width:5%;" class="tableHeader">
                        <div style="text-align:center;">
                            No
                        </div>
                    </div>
                    <div style="float:left; width:15%;" class="tableHeader">

                        <div style="margin-left:5%;">
                            <asp:Label ID="ttRoomType" runat="server" Text="Room Type" ToolTip="Room Type"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:10%;" class="tableHeader">
                        <div style="margin-left:5%; text-align:right;">
                            <asp:Label ID="ttMonPrice" runat="server" Text="Mon" ToolTip="Monday Price"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:10%;" class="tableHeader">
                        <div style="margin-left:5%; text-align:right;">
                            <asp:Label ID="ttTuePrice" runat="server" Text="Tue" ToolTip="Tuesday Price"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:10%;" class="tableHeader">
                        <div style="margin-left:5%; text-align:right;">
                            <asp:Label ID="ttWedPrice" runat="server" Text="Wed" ToolTip="Wednesday Price"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:10%;" class="tableHeader">
                        <div style="margin-left:5%; text-align:right;">
                            <asp:Label ID="ttThuPrice" runat="server" Text="Thu" ToolTip="Thursday Price"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:10%;" class="tableHeader">
                        <div style="margin-left:5%; text-align:right;">
                            <asp:Label ID="ttFriPrice" runat="server" Text="Fri" ToolTip="Friday Price"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:10%;" class="tableHeader">
                        <div style="margin-left:5%; text-align:right;">
                            <asp:Label ID="ttSun" runat="server" Text="Sun" ToolTip="Sunday Price"></asp:Label>
                        </div>
                    </div>
                    <div style="float:left; width:10%;" class="tableHeader">
                        <div style="margin-left:5%; text-align:right;">
                            <asp:Label ID="ttSatPrice" runat="server" Text="Sat" ToolTip="Saturday Price"></asp:Label>
                        </div>
                    </div>
                    <div style="width:10%; float:left;" class="tableHeader">
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
                            <div style="float:left; width:15%;" class="tableContent">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("RoomTypeID") %>' Visible="false"></asp:Label>

                                    <asp:Label ID="lblRoomType" runat="server" Text='<%# Eval("RoomType") %>' ToolTip='<%# Eval("RoomType") %>'></asp:Label>&nbsp;
                                </div>

                            </div>
                            <div style="float:left; width:10%;" class="tableContent">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblMonPrice" runat="server" Text='<%# Eval("MonPrice")%>' ToolTip='<%# Eval("MonPrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContent">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblTuePrice" runat="server" Text='<%# Eval("TuePrice")%>' ToolTip='<%# Eval("TuePrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContent">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblWedPrice" runat="server" Text='<%# Eval("WedPrice")%>' ToolTip='<%# Eval("WedPrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContent">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblThuPrice" runat="server" Text='<%# Eval("ThuPrice")%>' ToolTip='<%# Eval("ThuPrice") %>'></asp:Label>&nbsp;                                    
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContent">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblFriPrice" runat="server" Text='<%# Eval("FriPrice")%>' ToolTip='<%# Eval("FriPrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContent">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblSatPrice" runat="server" Text='<%# Eval("SatPrice")%>' ToolTip='<%# Eval("SatPrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContent">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblSunPrice" runat="server" Text='<%# Eval("SunPrice")%>' ToolTip='<%# Eval("SunPrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="min-width:8%; float:left;" class="tableContent">
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
                            <div style="float:left; width:15%;" class="tableContentAlternate">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblRoomTypeID" runat="server" Text='<%# Eval("RoomTypeID") %>' Visible="false"></asp:Label>

                                    <asp:Label ID="lblRoomType" runat="server" Text='<%# Eval("RoomType") %>' ToolTip='<%# Eval("RoomType") %>'></asp:Label>&nbsp;
                                </div>

                            </div>
                            <div style="float:left; width:10%;" class="tableContentAlternate">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblMonPrice" runat="server" Text='<%# Eval("MonPrice")%>' ToolTip='<%# Eval("MonPrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContentAlternate">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblTuePrice" runat="server" Text='<%# Eval("TuePrice")%>' ToolTip='<%# Eval("TuePrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContentAlternate">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblWedPrice" runat="server" Text='<%# Eval("WedPrice")%>' ToolTip='<%# Eval("WedPrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContentAlternate">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblThuPrice" runat="server" Text='<%# Eval("ThuPrice")%>' ToolTip='<%# Eval("ThuPrice") %>'></asp:Label>&nbsp;                                    
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContentAlternate">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblFriPrice" runat="server" Text='<%# Eval("FriPrice")%>' ToolTip='<%# Eval("FriPrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContentAlternate">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblSatPrice" runat="server" Text='<%# Eval("SatPrice")%>' ToolTip='<%# Eval("SatPrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:10%;" class="tableContentAlternate">
                                <div style="margin-left:5%; text-align:right;" class="textOverflowStyle">
                                    <asp:Label ID="lblSunPrice" runat="server" Text='<%# Eval("SunPrice")%>' ToolTip='<%# Eval("SunPrice") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="min-width:8%; float:left;" class="tableContentAlternate">
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

    </div>

</asp:Content>
