<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddEquipment.ascx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Room_Type.AddEquipment" %>

<link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
<link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />


<div style="margin-left:8%; margin-top:25px;">
    <div style="float:left;">
        <div class="subFormLabel requiredFieldLabel">
            Equipment
        </div>
        <div style="float:left;">
            <div>
                <asp:TextBox ID="txtEquipment" runat="server" CssClass="inputStyle" placeholder="TV Controller" Width="100%"></asp:TextBox>
            </div>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEquipment" ErrorMessage="Please enter a value." ValidationGroup="add" CssClass="validatorStyle"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div style="float:left; margin-left:8%;">
        <div class="subFormLabel requiredFieldLabel">
            Fine Charges
        </div>
        <div style="float:left;">
            <div>
                <asp:TextBox ID="txtEquipmentPrice" runat="server" CssClass="inputStyle" placeholder="10.00" Width="50%"></asp:TextBox>
            </div>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEquipmentPrice" ErrorMessage="Please enter a value." ValidationGroup="add" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Price must be in decimal!" CssClass="validatorStyle" ControlToValidate="txtEquipmentPrice" Type="Double" ValidationGroup="add" Operator="DataTypeCheck"></asp:CompareValidator>
            </div>
            
        </div>
    </div>

    <div style="float:left;">

        <asp:Button ID="btnSaveEquipment" runat="server" Text="Add" CssClass="subFormBtnSave" OnClick="btnSaveEquipment_Click" ValidationGroup="add" ToolTip="Add Equipment"/>
    </div>
       
</div>

<div style="clear:both">&nbsp;</div>

<%--Repeater table header--%>
<div style="width:80%; margin:auto;">
    <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
        No
    </div>
    <div style="float:left; width:25%;" class="subFormRepeaterHeader">
        Equipment
    </div>
    <div style="float:left; width:15%; text-align:right" class="subFormRepeaterHeader">
        Fine Charges
    </div>
    <div style="float:left; width:47%;" class="subFormRepeaterHeader">
        &nbsp;
    </div>
    <div style="float:left; width:5%;" class="subFormRepeaterHeader">
        &nbsp;
    </div>
</div>  

<%--Repeater Table content--%>
<asp:Repeater ID="Repeater1" runat="server">

    <ItemTemplate>
        <div style="width:80%; margin:auto;">
            <div style="float:left; width:8%; text-align:center;" class="subFormTableContent">
                <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
            </div>
            <div style="float:left; width:25%;" class="subFormTableContent">
                <asp:Label ID="lblEquipmentName" runat="server" Text='<%# Eval("equipmentName") %>'></asp:Label>
            </div>
            <div style="float:left; width:15%; text-align:right;" class="subFormTableContent">
                <asp:Label ID="lblFineCharges" runat="server" Text='<%# Eval("fineCharges", "{0:N2}") %>'></asp:Label>
            </div>
            <div style="float:left; width:49%;" class="subFormTableContent">
                &nbsp;
            </div>
            <div style="float:left; width:3%;" class="subFormTableContent">
                <asp:ImageButton ID="IBDeleteEquipment" runat="server" OnClick="IBDeleteEquipment_Click" ImageUrl="~/Image/delete_icon.png" CssClass="subFormIcon" ToolTip="Delete"/>
            </div>
        </div>  
    </ItemTemplate>

    <AlternatingItemTemplate>
        <div style="width:80%; margin:auto;">
            <div style="float:left; width:8%; text-align:center;" class="subFormTableContentAlternate">
                <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
            </div>
            <div style="float:left; width:25%;" class="subFormTableContentAlternate">
                <asp:Label ID="lblEquipmentName" runat="server" Text='<%# Eval("equipmentName") %>'></asp:Label>
            </div>
            <div style="float:left; width:15%; text-align:right;" class="subFormTableContentAlternate">
                <asp:Label ID="lblFineCharges" runat="server" Text='<%# Eval("fineCharges", "{0:N2}") %>'></asp:Label>
            </div>
            <div style="float:left; width:49%;" class="subFormTableContentAlternate">
                &nbsp;
            </div>
            <div style="float:left; width:3%;" class="subFormTableContentAlternate">
                <asp:ImageButton ID="IBDeleteEquipment" runat="server" OnClick="IBDeleteEquipment_Click" ImageUrl="~/Image/delete_icon.png" CssClass="subFormIcon" ToolTip="Delete"/>
            </div>
        </div> 
    </AlternatingItemTemplate>
    
</asp:Repeater>

<div style="width: 80%; margin: auto; clear:both;">

    <div class="subFormTableContent" style="padding-left:2%;">
        <asp:Label ID="lblNoItemFound" runat="server" Text="No item found!" Visible="false"></asp:Label>
    </div>           
</div>

<%--<div style="width:80%; margin:auto;" class="subFormRepeaterHeader">
    &nbsp;
</div>--%>

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
                
        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="popUpDeleteBtn" OnClick="btnDelete_Click"/>
    </asp:Panel>
</div>

<%-- Popup Cover --%>
<asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
    &nbsp;
</asp:Panel>
