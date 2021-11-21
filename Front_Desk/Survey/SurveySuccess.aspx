<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveySuccess.aspx.cs" Inherits="Hotel_Management_System.Hotel_Configuration_Management.Survey.SurveySuccess" %>

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

        .surveyFormHeaderContainer{
            padding:40px;
            background-color:white;
            border-radius:20px;
            width:55%;
            margin-left:auto;
            margin-right:auto;
            font-family: Helvetica, sans-serif;
            margin-bottom:1%;
            border:3px solid rgb(108 208 255);
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div class="surveyFormHeaderContainer">
                <div style="font-size:140%; font-weight:bold; color:rgb(0 115 169);">
                    Guest Satisfactory Survey
                </div>
                <br />
                <div style="font-size:90%;">
                    Thank you. Your response has been recorded.
                </div>
            </div>

        </div>
    </form>
</body>
</html>
