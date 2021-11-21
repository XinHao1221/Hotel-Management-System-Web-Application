<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" AutoEventWireup="true" CodeBehind="PreviewGuest.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Guest.PreviewGuest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <div>
        <%-- Page content --%>
        <div class="content">

            <div class="formHeader">
                Guest
            </div>

            <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
                <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='Guest.aspx';">

                <img src="../../Image/home_icon.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                    Menu
                </div>
                </div>

                <div class="formBtnEdit" style="float:left; height:40px; margin:0px 0px 0px 20px;" onclick="location.href='AddGuest.aspx';">

                    <img src="../../Image/plus_white.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;" />
                    <div style="float:left; margin:12px 0px 0px 10px; font-size:15px;">
                        Add
                    </div>
                </div>
            </div>

            <div style="height:80px;">&nbsp;</div>

            <%--Form Section--%>
            <div class="formSectionStyle" >
                1. Personal Info:-
            </div>

            <table style="width:100%;">
                <tr>
                    <td class="formLabel" style="width:20%;">Name</td>
                    <td class="tableSeperator" style="width:2%;"></td>
                    <td class="tableData" style="width:78%;">
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">Gender</td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">ID Type</td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblIDType" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">ID No.</td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblIDNo" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">Nationality</td>
                    <td></td>
                    <td class="tableData">
                        <asp:Label ID="lblNationality" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">
                        Date of Birth              
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="tableData">
                        <asp:Label ID="lblDOB" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">
                        Age             
                    </td>
                    <td class="tableSeperator"></td>
                    <td class="tableData">
                        <asp:Label ID="lblAge" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>

            <div class="formSectionStyle" >
                2. Communication:-
            </div>

            <table style="width:100%">
                <tr>
                    <td class="formLabel" style="width:20%;">Phone</td>
                    <td class="tableSeperator" style="width:2%;"></td>
                    <td class="tableData" style="width:78%;">
                        <asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="formLabel">Email</td>
                    <td class="tableSeperator"></td>
                    <td class="tableData">
                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>

            <div class="formSectionStyle" >
                3. Preferences:-
            </div>

            <div style="height:30px">&nbsp;</div>

            <%--Repeater table header--%>
            <div style="width:80%; margin-left:6%;">
                <div style="float:left; width:8%; text-align:center;" class="subFormRepeaterHeader">
                    No
                </div>
                <div style="float:left; width:70%;" class="subFormRepeaterHeader">
                    Preference
                </div>
                <div style="float:left; width:15%; text-align:right;" class="subFormRepeaterHeader">
                    Date Added
                </div>
                <div style="float:left; width:7%;" class="subFormRepeaterHeader">
                    &nbsp;
                </div>
            </div> 

            <%--Repeater Table content--%>
            <asp:Repeater ID="RepeaterPreferences" runat="server" OnItemDataBound="RepeaterPreferences_ItemDataBound">

                <ItemTemplate>
                    <div style="width:80%; margin-left:6%;">
                        <div style="float:left; width:8%; text-align:center;" class="subFormTableContent">
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                        </div>
                        <div style="float:left; width:70%;" class="subFormTableContent">
                            <asp:Label ID="lblPreference" runat="server" Text='<%# Eval("Preference") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:15%; text-align:right;" class="subFormTableContent">
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DateModified") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:7%;" class="subFormTableContent">
                            &nbsp;
                        </div>
                    </div>  
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <div style="width:80%; margin-left:6%;">
                        <div style="float:left; width:8%; text-align:center;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                        </div>
                        <div style="float:left; width:70%;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblPreference" runat="server" Text='<%# Eval("Preference") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:15%; text-align:right;" class="subFormTableContentAlternate">
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DateModified") %>'></asp:Label>
                        </div>
                        <div style="float:left; width:7%;" class="subFormTableContentAlternate">
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
    </div>
</asp:Content>

