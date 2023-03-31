<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditFeature.ascx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Room.EditFeature" %>

<link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
<link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

<div style="margin-left:8%; margin-top:25px;">
    <div style="float:left;">
        <div class="subFormLabel requiredFieldLabel">
            Feature
        </div>
        <div style="float:left;">
            <div>
                <asp:TextBox ID="txtFeature" runat="server" CssClass="inputStyle" placeholder="TV Controller" Width="100%"></asp:TextBox>
            </div>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFeature" ErrorMessage="Please enter a value." ValidationGroup="add" CssClass="validatorStyle"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    
    <div style="float:left; margin-left:5%;">

        <asp:Button ID="btnSaveFeature" runat="server" Text="Add" CssClass="subFormBtnSave" OnClick="btnSaveFeature_Click" ValidationGroup="add" ToolTip="Add Feature"/>
    </div>
       
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
                <asp:Label ID="lblFeatureID" runat="server" Text='<%# Eval("FeatureID") %>' Visible="false"></asp:Label>
                <asp:Label ID="lblFeature" runat="server" Text='<%# Eval("Title") %>' ToolTip='<%# Eval("Title") %>'></asp:Label>
            </div>
            <div style="float:left; width:1%;" class="subFormTableContent">
                &nbsp;
            </div>
            <div style="float:left; width:5%;" class="subFormTableContent">
                <asp:ImageButton ID="IBDeleteFeature" runat="server" OnClick="IBDeleteFeature_Click" ImageUrl="~/Image/delete_icon.png" CssClass="subFormIcon" ToolTip="Delete"/>
            </div>
        </div>  
    </ItemTemplate>

    <AlternatingItemTemplate>
        <div style="width:50%; margin-left:6.5%;">
            <div style="float:left; width:10%; text-align:center;" class="subFormTableContentAlternate">
                <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
            </div>
            <div style="float:left; width:84%;" class="subFormTableContentAlternate">
                <asp:Label ID="lblFeatureID" runat="server" Text='<%# Eval("FeatureID") %>' Visible="false"></asp:Label>
                <asp:Label ID="lblFeature" runat="server" Text='<%# Eval("Title") %>' ToolTip='<%# Eval("Title") %>'></asp:Label>
            </div>
            <div style="float:left; width:1%;" class="subFormTableContentAlternate">
                &nbsp;
            </div>
            <div style="float:left; width:5%; align-content:center;" class="subFormTableContentAlternate">
                <asp:ImageButton ID="IBDeleteFeature" runat="server" OnClick="IBDeleteFeature_Click" ImageUrl="~/Image/delete_icon.png" CssClass="subFormIcon" ToolTip="Delete"/>
            </div>
        </div> 
    </AlternatingItemTemplate>
    
</asp:Repeater>

<div style="width:50%; margin-left:7%; clear:both;">

    <div class="subFormTableContent" style="padding-left:2%;">
        <asp:Label ID="lblNoItemFound" runat="server" Text="No item found!" Visible="false"></asp:Label>
    </div>           
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
                
        <asp:Button ID="btnDeleteFeature" runat="server" Text="Delete" CssClass="popUpDeleteBtn" OnClick="btnDeleteFeature_Click"/>
    </asp:Panel>
</div>

<%-- Popup Cover --%>
<asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
    &nbsp;
</asp:Panel>

