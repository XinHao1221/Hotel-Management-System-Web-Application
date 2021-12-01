<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="myFYP.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <style>
       body{
           background-image:url('img/LoginBackground.jfif');
           position:fixed;
           background-size:100%;
          
       }
       
       .custForm{
           background-color:whitesmoke;
           font-size: 15px;
           font-family:Impact;
           border:5px solid black;
           margin: auto;
           width: 60%;
           padding: 10px;
       }
       .formLayout{
           position:absolute;
           top:45%;
           left:50%;
           transform: translate(-50%, -50%);
       }
       .formTitle{
           font-size: 25px;
           font-family:Impact;
       }
      .txtBoxStyle{
          border-color:black;
          border:2px solid;
          background-color:aliceblue;
          width:250px;
          height:30px;
          
      }  
      .buttonLogin{
          background-color: black;
          color: white;
          padding: 10px 20px;
          text-align: center;
          display: inline-block;
          font-size: 16px;
          margin-top:30px;
          margin-left:90px;
          margin-bottom:30px;
          margin-right:0px;
      }  
      .logo{
          color:white;
          font-size: 40px;
          font-family:Impact;
          text-align:center;
      }
      .title{
          color:white;
          font-size: 40px;
          font-family:Impact;
          text-align:center;
          font-weight:bold;
      }
      a:link{
          color:aliceblue;
      }
      a:hover{
          text-decoration:none;
      }
      a:active{
          color:yellow;
      }
    </style>

    <title></title>
</head>
<body class="formLayout">
     <div>
         <h1 class="title">Hotel Management System</h1>
        <h2 class="logo">WELCOME</h2>
     </div>

     <form runat="server">
        <div class="custForm">
            <asp:Panel ID="panelCust" runat="server" >
                <h3 class="formTitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Login</h3>
           
               <table>
                   <tr><td>User ID</td></tr>
                   <tr><td>
                       <asp:TextBox ID="txtUserID" runat="server" class="txtBoxStyle" ></asp:TextBox></td></tr>
                   <tr><td>Password</td></tr>
                   <tr><td>
                       <asp:TextBox ID="txtPass" runat="server" class="txtBoxStyle" TextMode="Password"></asp:TextBox></td></tr>
               </table>
                
                <asp:Button ID="btnLogin" runat="server" Text="Login" class="buttonLogin" OnClick="btnLogin_Click"/>
            </asp:Panel>

        </div>

      </form>
    </body>
</html>