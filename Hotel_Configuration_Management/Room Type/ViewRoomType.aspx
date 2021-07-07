<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="ViewRoomType.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Room_Type.ViewRoomType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />


    <div>
        <div class="formHeader">
            Room Type
        </div>

        <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
            <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='RoomType.aspx';">

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
                1. Room Details:-
            </div>

            <table style="width:100%;">
            <tr>
                <td class="formLabel">
                        Title                 
                </td>
                <td class="tableSeperator"></td>
                <td class="tableData">
                    <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="formLabel">Short Code </td>
                <td></td>
                <td class="tableData">
                    <asp:Label ID="lblShortCode" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="formLabel">
                        Price               
                </td>
                <td class="tableSeperator"></td>
                <td class="tableData">
                    <asp:LinkButton ID="LBPriceManager" runat="server" OnClick="LBPriceManager_Click">manage price</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="formLabel" style="vertical-align: top; padding-top:15px;">Description </td>
                <td></td>
                <td class="tableData">
                    <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="formLabel">Base Occupancy </td>
                <td></td>
                <td class="tableData">
                    <asp:Label ID="lblBaseOccupancy" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="formLabel">Higher Occupancy </td>
                <td></td>
                <td class="tableData">
                    <asp:Label ID="lblHigherOccupancy" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="formLabel">Extra Bed</td>
                <td></td>
                <td class="formInput" style="padding-bottom:0px;">
                    <div style="float:left; padding-top:40px;">
                        <asp:CheckBox ID="cbExtraBed" runat="server" CssClass="formCheckBoxStyle" Width="50px" Enabled="false" Visible="false" Checked="false"/>
                    </div>
                    <div style="float:left; padding-top:5px;">
                        <asp:Image ID="IChecked" runat="server" ImageUrl="~/Image/checked_icon.png" Width="20px" Height="20px" Visible="false"/>
                    </div>
                    <div style="float:left; margin-left:20px; margin-top:6px;" class="tableData">
                        <asp:Panel ID="pnExtraBedPrice" runat="server" Visible="false">
                            <asp:Label ID="lblExtraBedPrice" runat="server" Text=""></asp:Label>
                        </asp:Panel>
                    </div>
                </td>
            </tr>    
        </table>

        <div class="formSectionStyle" >
            2. Equipment:-
        </div>

            <div style="height:30px">&nbsp;</div>

        <%--Repeater table header--%>
        <div style="width:80%; margin-left:6%;">
            <div style="float:left; width:5%; text-align:center;" class="subFormRepeaterHeader">
                No
            </div>
            <div style="float:left; width:25%;" class="subFormRepeaterHeader">
                Equipment
            </div>
            <div style="float:left; width:25%;" class="subFormRepeaterHeader">
                Fine Charges
            </div>
            <div style="float:left; width:40%;" class="subFormRepeaterHeader">
                &nbsp;
            </div>
            <div style="float:left; width:5%;" class="subFormRepeaterHeader">
                &nbsp;
            </div>
        </div>  

        <%--Repeater Table content--%>
        <asp:Repeater ID="Repeater1" runat="server">

            <ItemTemplate>
                <div style="width:80%; margin-left:6%;">
                    <div style="float:left; width:5%; text-align:center;" class="subFormTableContent">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:25%;" class="subFormTableContent">
                        <asp:Label ID="lblEquipmentName" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:8%; text-align:right;" class="subFormTableContent">
                        <asp:Label ID="lblFineCharges" runat="server" Text='<%# Eval("FineCharges") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:59%;" class="subFormTableContent">
                        &nbsp;
                    </div>
                    <div style="float:left; width:3%;" class="subFormTableContent">
                        &nbsp;
                    </div>
                </div>  
            </ItemTemplate>

            <AlternatingItemTemplate>
                <div style="width:80%; margin-left:6%;">
                    <div style="float:left; width:5%; text-align:center;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                    </div>
                    <div style="float:left; width:25%;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblEquipmentName" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:8%; text-align:right;" class="subFormTableContentAlternate">
                        <asp:Label ID="lblFineCharges" runat="server" Text='<%# Eval("FineCharges") %>'></asp:Label>
                    </div>
                    <div style="float:left; width:59%;" class="subFormTableContentAlternate">
                        &nbsp;
                    </div>
                    <div style="float:left; width:3%;" class="subFormTableContentAlternate">
                        &nbsp;
                    </div>
                </div> 
            </AlternatingItemTemplate>
    
        </asp:Repeater>

        <%--Message if item no found--%>
        <div style="width: 80%; margin-left:6%; clear:both;">

            <div class="subFormTableContent" style="padding-left:2%;">
                <asp:Label ID="lblNoItemFound" runat="server" Text="No item found!" Visible="false"></asp:Label>
            </div>           
        </div>

        <div style="clear:both; height:50px">&nbsp;</div>

    </div>
</asp:Content>