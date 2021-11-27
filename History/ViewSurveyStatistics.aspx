<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ViewSurveyStatistics.aspx.cs" Inherits="Hotel_Management_System.History.ViewSurveyStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <%--CSS--%>
    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupBox.css" />
    <link rel="stylesheet" href="../../StyleSheet/SubFormStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/RepeaterTable.css" />

    <style>

        .surveyResponseContainer{
            border: 1px solid rgb(203 203 203);
            width: 90%;
            margin: auto;
            padding:30px;
            margin-top: 20px;
            min-height:180px;
            border-radius:20px;
            position:relative;
            z-index:-10;
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
            Survey Responses
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

        <div style="clear:both; height:30px;">&nbsp;</div>

        <%--Display Reserved Room Details--%>
        <asp:Repeater ID="RepeaterSurveyResponse" runat="server" OnItemDataBound="RepeaterSurveyResponse_ItemDataBound">

            <ItemTemplate >
                
                <div class="surveyResponseContainer">  
                    <div class="cover">
                        <div class="likerScaleQuestionStyle" style="margin-left:5%;">
                            <asp:Label ID="lblQuestionID" runat="server" Text='<%# Eval("QuestionID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>.&nbsp;
                            <asp:Label ID="lblQuestion" runat="server"></asp:Label>
                        </div>

                        <div style="height:5px;">&nbsp;</div>

                        <div style="margin-left:5%; font-family: Helvetica, sans-serif; color:rgb(92 92 92); font-size:15px;">
                            <asp:Label ID="lblTotalResponses" runat="server"></asp:Label>
                            responses
                        </div>

                        <div style="clear:both;">&nbsp;</div>

                        <div style="text-align:center;">

                            <asp:Chart ID="ChartSurveyQuestion" runat="server" width="900px" height="400px">
                                <Series>
                                    <asp:Series Name="Series1"></asp:Series> 
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="false" Name="Defualt" LegendStyle="Row" />
                                </Legends>
                            </asp:Chart>

                        </div>
                </div>
            </div>

        </ItemTemplate>

    </asp:Repeater>

    </div>

</asp:Content>