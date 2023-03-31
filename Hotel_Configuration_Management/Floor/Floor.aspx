<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="Floor.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Floor.Floor" %>

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
    </style>

    <div>
        <%-- Page content --%>
        <div class="content">
                
            <div class="mainMenuHeader">
                <%--Title--%>
                <div class="mainMenuTitle">
                    Floor 
                </div>

                <%--Search Bar--%>
                <div class="mainMenuSearchBar">
                    <div style="display:flex; justify-content:center;">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="Floor Name" CssClass="searchBarStyle" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
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
                        <div class="menuHeaderSaveBtn" onclick="location.href='AddFloor.aspx';">

                            
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

            <div>
                    
            </div>

            <div style="width: 97%; margin: auto;">
                <div style="width:100%;">
                    <div style="float:left; width:5%;" class="tableHeader">
                        <div style="text-align:center;">
                            No
                        </div>
                    </div>
                    <div style="float:left; width:25%;" class="tableHeader">

                        <div style="margin-left:5%;">
                            Name
                        </div>
                    </div>
                    <div style="float:left; width:20%;" class="tableHeader">
                        <div style="margin-left:5%;">
                            Floor Number
                        </div>
                    </div>
                    <div style="float:left; width:20%;" class="tableHeader">
                        <div style="margin-left:5%;">
                            Status
                        </div>
                    </div>
                    <div style="width:30%; float:left;" class="tableHeader">
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
                            <div style="float:left; width:25%;" class="tableContent">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblFloorID" runat="server" Text='<%# Eval("FloorID") %>' Visible="false"></asp:Label>

                                    <asp:Label ID="lblFloorName" runat="server" Text='<%# Eval("FloorName") %>' ToolTip='<%# Eval("FloorName") %>'></asp:Label>&nbsp;
                                </div>

                            </div>
                            <div style="float:left; width:20%;" class="tableContent">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblFloorNumber" runat="server" Text='<%# Eval("FloorNumber")%>' ToolTip='<%# Eval("FloorNumber") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="float:left; width:20%;" class="tableContent">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>' ToolTip='<%# Eval("Status") %>'></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div style="min-width:28%; float:left;" class="tableContent">
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

                        <asp:LinkButton ID="LBChangeStatus" runat="server" OnClick="LBChangeStatus_Click" CssClass="optionContainer" Font-Underline="false">
                            

                            <div class="tableOption" style="color:#FF0000;">

                                <asp:Image ID="IMChangeStatus" runat="server" CssClass="tableOptionIcon" ImageUrl="~/Image/activate_icon.png"/>
                                
                                <asp:Label ID="lblChangeStatus" runat="server" Text="Label" ForeColor="#00ce1b">Activate</asp:Label>

                            </div>
                            
                        </asp:LinkButton>

                        <asp:LinkButton ID="LBDelete" runat="server" OnClick="LBDelete_Click" CssClass="optionContainer" Font-Underline="false">
                            

                            <div class="tableOption" style="color:#FF0000;">

                                <img src="../../Image/delete_icon.png" class="tableOptionIcon" />
                                
                                Delete

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
                                    <asp:Label ID="lblFloorID" runat="server" Text='<%# Eval("FloorID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblFloorName" runat="server" Text='<%# Eval("FloorName") %>' ToolTip='<%# Eval("FloorName") %>'></asp:Label>&nbsp;
                                </div>

                            </div>
                            <div style="float:left; width:20%;" class="tableContentAlternate">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblFloorNumber" runat="server" Text='<%# Eval("FloorNumber") %>' ToolTip='<%# Eval("FloorNumber") %>'></asp:Label>&nbsp;
                                </div>

                            </div>

                            <div style="float:left; width:20%;" class="tableContentAlternate">
                                <div style="margin-left:5%;" class="textOverflowStyle">
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' ToolTip='<%# Eval("Status") %>'></asp:Label>&nbsp;
                                </div>

                            </div>

                            <div style="min-width:28%; float:left;" class="tableContentAlternate">
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

                            <asp:LinkButton ID="LBChangeStatus" runat="server" OnClick="LBChangeStatus_Click" CssClass="optionContainer" Font-Underline="false">
                            

                                <div class="tableOption" style="color:#FF0000;">

                                    <asp:Image ID="IMChangeStatus" runat="server" CssClass="tableOptionIcon" ImageUrl="~/Image/activate_icon.png"/>
                                
                                    <asp:Label ID="lblChangeStatus" runat="server" Text="Label" ForeColor="#00ce1b">Activate</asp:Label>

                                </div>
                            
                            </asp:LinkButton>

                            <asp:LinkButton ID="LBDelete" runat="server" OnClick="LBDelete_Click" CssClass="optionContainer" Font-Underline="false">
                            

                                <div class="tableOption" style="color:#FF0000">

                                    <img src="../../Image/delete_icon.png" class="tableOptionIcon" />
                                
                                    Delete

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
            <asp:Panel ID="PopupStatus" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <asp:Label ID="lblPopupTitle" runat="server" Text="Label" CssClass="popupTitle" ForeColor="#00ce1b">Activate</asp:Label>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <asp:Label ID="lblPopupContent" runat="server" Text="Label"></asp:Label>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnPopupCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnPopupCancel_Click"/>
                
                <asp:Button ID="btnPopupActivate" runat="server" Text="Activate" CssClass="popUpActiveBtn" OnClick="btnPopupActivate_Click"/>
            </asp:Panel>

            <asp:Panel ID="PopupDelete" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:red;" class="popupTitle">Delete</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <asp:Label ID="lblPopupDeleteContent" runat="server" Text="Label"></asp:Label>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnPopupCancel_Click"/>
                
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="popUpDeleteBtn" OnClick="btnDelete_Click"/>
            </asp:Panel>
        </div>

        <%--Popup Box--%>
        <asp:Panel ID="PopupBoxDelete" runat="server" CssClass="popupBox" Visible="false" Width="50%">
                    
            <div class="popupBoxContainer">
   
                <div class="popupBoxHeader">
                    <%-- Popup Window Title --%>
                    <div style="float:left;">
                        <p style="color:red;" class="popupBoxTitle">Confirm before delete</p>
                    </div>

                    <div style="float:right;">
                        <asp:ImageButton ID="IBClosePopUpBox" runat="server" ImageUrl="~/Image/delete.png" CssClass="popupBoxCloseIcon" ToolTip="Close" OnClick="IBClosePopUpBox_Click"/>

                    </div>
                </div>

                <div style="clear:both;"></div>

                <%-- Popup Window Body --%>
                <div class="popupBoxBody">

                    <div>
                        There are <asp:Label ID="lblTotalRoom" runat="server" Text="Label"></asp:Label> Rooms attached to this floor. <br />
                        The following rooms will be deleted:
                        <br /><br />
                    </div>

                    <asp:Label ID="lblFloorName" runat="server" Text="Label"></asp:Label><br />
                        
                    <%--Display Room Number--%>
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <div>
                                &emsp;&emsp;
                                <asp:Label ID="lblRoomNo" runat="server" Text='<%# Eval("RoomNumber")%>'></asp:Label> <br />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                        
                </div>

                <%--Delete Confirmation Checkbox--%>
                <div style="font-size:120%">
                    <div style="float:left; margin-top:2px;">
                        <asp:CheckBox ID="cbDeleteAnywhere" runat="server" CssClass="formCheckBoxStyle" Width="25px" Height="25px" OnCheckedChanged="cbDeleteAnywhere_CheckedChanged" AutoPostBack="true"/>
                    </div>
                    <div style="float:left; margin-left:5px;">
                        Delete Anywhere
                    </div>
                </div>

                <div style="clear:both;">&nbsp;</div>

                <div class="popupBoxFooter">
                        
                    <asp:Button ID="btnPopupBoxDelete" runat="server" Text="Delete" CssClass="popUpBoxDeleteBtn" OnClick="btnPopupBoxDelete_Click" Visible="false"/>
                </div>

            </div>
        </asp:Panel> 

        <%-- Popup Cover --%>
        <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
            &nbsp;
        </asp:Panel>
            
    </div>
</asp:Content>
