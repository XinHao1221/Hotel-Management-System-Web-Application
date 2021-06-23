<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddEquipment.ascx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Room_Type.AddEquipment" %>

<link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />


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
                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Price must be in decimal!" CssClass="validatorStyle" ControlToValidate="txtEquipmentPrice" Type="Double" ValidationGroup="save" Operator="DataTypeCheck"></asp:CompareValidator>
            </div>
            
        </div>
    </div>

    <div style="float:left;">

        <asp:Button ID="btnSaveEquipment" runat="server" Text="Add" CssClass="formBtnSave" OnClick="btnSaveEquipment_Click" ValidationGroup="add"/>
    </div>
       
</div>

<div style="clear:both">&nbsp;</div>

<%--Repeater table header--%>
<div style="width:80%; margin:auto;">
    <div style="float:left; width:5%; text-align:center;" class="subFormRepeaterHeader">
        No
    </div>
    <div style="float:left; width:25%;" class="subFormRepeaterHeader">
        Equipment
    </div>
    <div style="float:left; width:25%;" class="subFormRepeaterHeader">
        Fine Charges
    </div>
    <div style="float:left; width:43%;" class="subFormRepeaterHeader">
        &nbsp;
    </div>
    <div style="float:left; width:2%;" class="subFormRepeaterHeader">
        &nbsp;
    </div>
</div>  

<div style="clear:both">&nbsp;</div>

<%--Repeater Table content--%>
<asp:Repeater ID="Repeater1" runat="server">

    <ItemTemplate>
        <div style="width:80%; margin:auto;">
            <div style="float:left; width:5%; text-align:center;" class="subFormTableContent">
                <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
            </div>
            <div style="float:left; width:25%;" class="subFormTableContent">
                <asp:Label ID="lblEquipmentName" runat="server" Text='<%# Eval("equipmentName") %>'></asp:Label>
            </div>
            <div style="float:left; width:25%;" class="subFormTableContent">
                <asp:Label ID="lblFineCharges" runat="server" Text='<%#string.Format("{0:n2}",Eval("fineCharges")) %>'></asp:Label>
            </div>
            <div style="float:left; width:43%;" class="subFormTableContent">
                &nbsp;
            </div>
            <div style="float:left; width:2%;" class="subFormTableContent">

            </div>
        </div>  
    </ItemTemplate>

    <AlternatingItemTemplate>
        <div style="width:80%; margin:auto;">
            <div style="float:left; width:5%; text-align:center;" class="subFormTableContentAlternate">
                <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
            </div>
            <div style="float:left; width:25%;" class="subFormTableContentAlternate">
                <asp:Label ID="lblEquipmentName" runat="server" Text='<%# Eval("equipmentName") %>'></asp:Label>
            </div>
            <div style="float:left; width:25%;" class="subFormTableContentAlternate">
                <asp:Label ID="lblFineCharges" runat="server" Text='<%#string.Format("{0:n2}",Eval("fineCharges")) %>'></asp:Label>
            </div>
            <div style="float:left; width:43%;" class="subFormTableContentAlternate">
                &nbsp;
            </div>
            <div style="float:left; width:2%;" class="subFormTableContentAlternate">
                &nbsp;
            </div>
        </div> 
    </AlternatingItemTemplate>
    
</asp:Repeater>

<div style="width: 80%; margin: auto; clear:both;">

    <div class="subFormTableContent" style="padding-left:2%;">
        <asp:Label ID="lblNoItemFound" runat="server" Text="No item found!" Visible="false"></asp:Label>&nbsp;
    </div>           
</div>

<%--<div style="width:80%; margin:auto;" class="subFormRepeaterHeader">
    &nbsp;
</div>--%>


