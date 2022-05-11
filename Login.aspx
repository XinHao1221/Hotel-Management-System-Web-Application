<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="myFYP.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <style>
        body {
            background-color: #E0F9FF;
            margin: 0;
            font-family: Helvetica, sans-serif;
        }

        .login-form-container {
            width: 400px;
            height: 500px;
            border: 1px solid #ADADAD;
            border-radius: 20px;
            background-color: white;
            position: absolute;
            top: 50%;  
            left: 50%; 
            transform: translate(-50%, -50%);
        }

        .style-title {
            width: 100%;
            display: flex;
            justify-content: center;
            margin-top: 50px;
            font-size: 28px;
        }

        .style-text-field-container {
            position: relative;
            border-bottom: 2px solid #737373;
            width: 80%;
            margin: 0 auto 40px auto;
        }

        .style-text-field {
            border: none;
            font-size: 16px;
            padding: 0px 0px 7px 45px;
            width: 80%; 
            background: transparent;
        }

        .style-text-field:focus {
            outline: none !important;
            background: transparent;
        }

        /* Change the white to any color */
        input:-webkit-autofill,
        input:-webkit-autofill:hover, 
        input:-webkit-autofill:focus, 
        input:-webkit-autofill:active{
            -webkit-box-shadow: 0 0 0 30px white inset !important;
        }

        .style-button {
            background-color: #0089FA;
            border-radius: 10px;
            color: white;
            border: none;
            width: 80%;
            height: 50px;
            margin: 125px auto 0 auto;
            display: flex;
            justify-content: center;
            align-items: center;
            font-weight: bold;
            font-size: 15px;
        }

        .style-button:hover {
            cursor: pointer;
            opacity: 0.8;
            transition: 0.4s;
        }

        .style-icon-label {
            position: absolute;
            top: 0;
            width: 15px;
            left: 10px;
        }
    </style>

    <title></title>
</head>

    <body>

        <div class="login-form-container">
            <!-- Header -->
            <div class="style-title">
                <span style="color: #0089FA; font-weight: bold">HMS</span> 
                <span style="margin-left:10px;">Login</span>
            </div>

            <form runat="server">
                <!-- Login Form -->    
                <div style="margin-top: 80px;">
                    <!-- Login ID -->
                    <div class="style-text-field-container">
                        <img src="./Image/user_icon.png" class="style-icon-label" />
                        <asp:TextBox ID="txtUserID" runat="server" class="style-text-field" placeholder="Login ID"></asp:TextBox>
                    </div>
                    <!-- Password -->
                    <div class="style-text-field-container">
                        <img src="./Image/key_icon.png" class="style-icon-label" />
                        <asp:TextBox ID="txtPass" runat="server" class="style-text-field" TextMode="Password" placeholder="Password"></asp:TextBox>
                    </div>
                </div>

                <!-- Sign in Button -->
                <asp:Button ID="btnLogin" runat="server" Text="Login" class="style-button" OnClick="btnLogin_Click"/>
            </form>
        
        </div>
    </body>
</html>