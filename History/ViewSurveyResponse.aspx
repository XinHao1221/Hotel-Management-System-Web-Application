<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ViewSurveyResponse.aspx.cs" Inherits="Hotel_Management_System.History.ViewSurveyResponse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <style>

        .surveyResponseContainer{
            border: 1px solid rgb(213 213 213);
            width: 80%;
            margin: auto;
            padding:30px;
            margin-top: 20px;
            min-height:180px;
            border-radius:20px;
            position:relative;
            z-index:-10;
        }

        .cover{
            width: 80%;
            margin: auto;
            padding:30px;
            min-height:180px;
            border-radius:20px;
            display:block;
            position:absolute;
            top:0;
            z-index:10;
        }

        .likertScaleLabel{
            word-wrap: break-word;
            font-family: Helvetica, sans-serif;
            padding-bottom:10px;
        }

        .LikertScaleStyle{
            word-wrap: break-word;
            font-family: Helvetica, sans-serif;
            padding-bottom:10px;
            border: 0px;
            width: 100%;
            height: 2em;
        }

        .likerScaleQuestionStyle{
            min-height: 50px;
            font-family: Helvetica, sans-serif;
            font-weight: bold;
            line-height:50px; 
            vertical-align:middle;
        }

    </style>

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
            2. Response Details:-
        </div>

        <%--Display Reserved Room Details--%>
        <asp:Repeater ID="RepeaterSurveyResponse" runat="server" OnItemDataBound="RepeaterSurveyResponse_ItemDataBound">

            <ItemTemplate >
                
                <div class="surveyResponseContainer">  
                    <div class="cover">
                    <div class="likerScaleQuestionStyle" style="margin-left:5%;">
                        <asp:Label ID="lblQuestionID" runat="server" Text='<%# Eval("questionID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>.&nbsp;
                        <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("question") %>'></asp:Label>
                    </div>
                    <div style="height:100px; display:flex; justify-content:center;">
                        <div style="float:left; line-height:100px; vertical-align:middle; margin-right:3%; font-size:90%;" class="likertScaleLabel">
                            Strongly Disagree
                        </div>
                        <div style="float:left; line-height:100px; vertical-align:middle;" class="likertScaleLabel">
                            <asp:Label ID="lblAnswer" runat="server" Text='<%# Eval("answer") %>' Visible="false"></asp:Label>
                            <asp:RadioButtonList ID="rblSurveyAnswer" runat="server" RepeatDirection="Horizontal" CssClass="LikertScaleStyle">
                                <asp:ListItem Value="1">&nbsp;1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">&nbsp;2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">&nbsp;3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="4">&nbsp;4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">&nbsp;5</asp:ListItem>
                            </asp:RadioButtonList>

                        </div>
                        <div style="float:left; line-height:100px; vertical-align:middle; margin-left:4%; font-size:90%;" class="likertScaleLabel">
                            Strongly Agree
                        </div>
                    </div>
</div>
                </div>

            </ItemTemplate>

        </asp:Repeater>

    </div>

</asp:Content>

