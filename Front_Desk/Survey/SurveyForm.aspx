<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyForm.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.SurveyForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>


    <style>

        body{
            min-width:1150px;
            width: auto !important;
            width:1100px;
            margin:0;
            background-color:rgb(230 247 255);
            padding:10px 0px 10px 0px;
            min-width:1000px;
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

        .questionContainer{
            padding:30px;
            background-color:white;
            border-radius:20px;
            width:55%;
            margin-left:auto;
            margin-right:auto;
            margin-bottom:1%;
        }

        .surveyFormHeaderContainer{
            padding:40px;
            background-color:white;
            border-radius:20px;
            width:55%;
            margin-left:auto;
            margin-right:auto;
            font-family: Helvetica, sans-serif;
            font-weight:bold;
            font-size:140%;
            margin-bottom:1%;
            border:3px solid rgb(108 208 255);
            color:rgb(0 115 169);
        }

        .btnSaveStyle{
            width: 110px;
            height: 40px;
            background-color: rgb(0, 206, 27);
            color: white;
            font-weight:bold;
            margin-left:21%;
            border:1px solid rgb(0, 206, 27);
            border-radius:5px;
        }

        .validatorStyle {
            color: red;
            font-size: 15px;
            vertical-align: top;
            font-family: Helvetica, sans-serif;
            margin-left:7px;
            margin-top:1px;
            position:absolute;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="surveyFormHeaderContainer">
                Guest Satisfactory Survey
            </div>

            <asp:Repeater ID="RepeaterQuestion" runat="server">
                <ItemTemplate>
                    <div class="questionContainer">
                        <div class="likerScaleQuestionStyle" style="margin-left:5%;">
                            <asp:Label ID="lblQuestionID" runat="server" Text='<%# Eval("QuestionID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>.&nbsp;
                            <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                        </div>
                        <div style="height:100px; display:flex; justify-content:center;">
                            <div style="float:left; line-height:100px; vertical-align:middle; margin-right:3%; font-size:90%;" class="likertScaleLabel">
                                Strongly Disagree
                            </div>
                            <div style="float:left; line-height:100px; vertical-align:middle;" class="likertScaleLabel">

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

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This field is required." CssClass="validatorStyle" ControlToValidate="rblSurveyAnswer" ValidationGroup="save"></asp:RequiredFieldValidator>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btnSaveStyle" ToolTip="Submit" ValidationGroup="save" />

        </div>
    </form>
</body>
</html>
