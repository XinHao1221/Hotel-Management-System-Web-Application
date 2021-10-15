<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ViewSurveyResponse.aspx.cs" Inherits="Hotel_Management_System.History.ViewSurveyResponse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <div>

        <%-- Page content --%>
        <div class="formHeader">
            Survey Response
        </div>

        <asp:LinkButton ID="LBBack" runat="server" OnClick="LBBack_Click" CssClass="divLBStyle">
            <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
                <div class="formBackBtn" style="float:left;" tooltip="save">

                    <img src="../../Image/Back.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                    <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                        Back
                    </div>
                        
                </div>
            </div>
        </asp:LinkButton>

        <div style="clear:both;">&nbsp;</div>

        <div class="formSectionStyle" style="margin-bottom:25px;">
            1. Stay Details:-
        </div>

        <%--Display guest details--%>
        <div style="width:90%; margin:auto;">
            <div style="width:45%; float:left;">

                <table style="width:100%;">
                    <tr>
                        <td class="formLabel">
                                Guest Name              
                        </td>
                        <td class="tableSeperator" style="width:5%;"></td>
                        <td class="tableData">
                            <asp:Label ID="lblGuestName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width:10%;">
                                IDNo.          
                        </td>
                        <td class="tableSeperator" style="width:5%;"></td>
                        <td class="tableData" style="width:85%;">
                            <asp:Label ID="lblIDNo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>   
                    <tr>
                        <td class="formLabel" style="width:10%;">
                                Status         
                        </td>
                        <td class="tableSeperator" style="width:5%;"></td>
                        <td class="tableData" style="width:85%;">
                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>  
                </table>

            </div>
            <div style="width:55%; float:left;">

                <table style="width:90%;">
                    <tr>
                        <td class="formLabel">
                                Check-In     
                        </td>
                        <td class="tableSeperator"></td>
                        <td class="tableData">
                            <asp:Label ID="lblCheckIn" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel">
                                Check-Out     
                        </td>
                        <td class="tableSeperator"></td>
                        <td class="tableData">
                            <asp:Label ID="lblCheckOut" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel">
                                Duration of Stay    
                        </td>
                        <td class="tableSeperator"></td>
                        <td class="tableData">
                            <asp:Label ID="lblDurationOfStay" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>

            </div> 
        </div>

        <div style="clear:both;">&nbsp;</div>

        <div class="formSectionStyle" style="margin-bottom:25px">
            3. Response Details:-
        </div>

    </div>

</asp:Content>

