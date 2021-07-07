<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"  CodeBehind="ViewRoom.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Room.ViewRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <div>
        <div class="formHeader">
            Room
        </div>

        <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
            <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='Room.aspx';">

                <img src="../../Image/home_icon.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                    Menu
                </div>
                        
            </div>

            <asp:LinkButton ID="LBEdit" runat="server" CssClass="divLBStyle" OnClick="LBEdit_Click">
                <div class="formBtnEdit" style="float:left; height:40px; margin:0px 0px 0px 20px;">

                    <img src="../../Image/edit_white_icon.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;" />
                    <div style="float:left; margin:12px 0px 0px 10px; font-size:15px;">
                        Edit
                    </div>
                </div>
            </asp:LinkButton>
                
        </div>

        <div style="height:80px;">&nbsp;</div>

        <%--Form Section--%>
        <div class="formSectionStyle" >
            1. Room Information:-
        </div>

        <table style="width:100%;">
            <tr>
                <td class="formLabel">
                        Room Number               
                </td>
                <td class="tableSeperator"></td>
                <td class="tableData">
                    <asp:Label ID="lblRoomNumber" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="formLabel">
                        Floor Number
                </td>
                <td class="tableSeperator"></td>
                <td class="tableData">
                    <asp:Label ID="lblFloorNumber" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="formLabel">
                        Room Type
                </td>
                <td class="tableSeperator"></td>
                <td class="tableData">
                    <asp:Label ID="lblRoomType" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="formLabel">Status</td>
                <td></td>
                <td class="tableData">
                    <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>

            <div class="formSectionStyle" >
            2. Room's Feature:-
        </div>

        <div style="clear:both">&nbsp;</div>

        <%--Repeater table header--%>
        <div style="width:50%; margin-left:6.5%;">
            <div style="float:left; width:10%; text-align:center;" class="subFormRepeaterHeader">
                No
            </div>
            <div style="float:left; width:84%;" class="subFormRepeaterHeader">
                Feature
            </div>
    
            <div style="float:left; width:6%;" class="subFormRepeaterHeader">
                &nbsp;
            </div>
        </div>  

        <%--Repeater Table content--%>
        <asp:Repeater ID="Repeater1" runat="server">

            <ItemTemplate>
                <div style="width:50%; margin-left:6.5%;">
                    <div style="float:left; width:10%; text-align:center;" class="subFormTableContent">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:84%;" class="subFormTableContent">
                        <asp:Label ID="lblFeature" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:6%;" class="subFormTableContent">
                        &nbsp;
                    </div>
                </div>  
            </ItemTemplate>

            <AlternatingItemTemplate>
                <div style="width:50%; margin-left:6.5%;">
                    <div style="float:left; width:10%; text-align:center;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:84%;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblFeature" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:6%; align-content:center;" class="subFormTableContentAlternate">
                        &nbsp;
                    </div>
                </div> 
            </AlternatingItemTemplate>
    
        </asp:Repeater>

        <div style="width:50%; margin-left:7%; clear:both;">

            <div class="subFormTableContent" style="padding-left:2%;">
                <asp:Label ID="lblNoItemFound" runat="server" Text="No item found!" Visible="false"></asp:Label>
            </div>           
        </div>

        <div style="clear:both; height:50px">&nbsp;</div>

    </div>
</asp:Content>
