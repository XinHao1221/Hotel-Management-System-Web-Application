﻿<%-- 
    Author: Koh Xin Hao
    Student ID: 20WMR09471
    Programme: RSF3G4
    Year: 2021
 --%>

<%@ Page Language="C#" MasterPageFile="~/Template/MainTemplate.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="EditGuest.aspx.cs" Inherits="Hotel_Management_System.Front_Desk.Guest.EditGuest" %>
<%@ Register TagPrefix="Preference" TagName="Control" Src="~/Front_Desk/Guest/EditPreference.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <link rel="stylesheet" href="../../StyleSheet/InputStyle.css" />
    <link rel="stylesheet" href="../../StyleSheet/PopupWindow.css" />

    <div>
        <%-- Page content --%>
        <div class="formHeader">
            Guest Form
        </div>

        <asp:LinkButton ID="LBBack" runat="server" OnClick="LBBack_Click" CssClass="divLBStyle">
            <div style="margin:20px 0px 20px 0px; margin-left:2.5%;">
                <div class="formBackBtn" style="float:left;" tooltip="save" onclick="location.href='Floor.aspx';">

                    <img src="../../Image/Back.png" width="15px" height="15px" style="float:left; margin:12.5px 0px 0px 15px;"/>
                    <div style="float:left; margin:12.5px 0px 0px 10px; font-size:15px;">
                        Back
                    </div>
                        
                </div>
            </div>
        </asp:LinkButton>

        <div style="clear:both;">&nbsp;</div>

        <%--Form Section--%>
        <div class="formSectionStyle" >
            1. Personal Info:-
        </div>

        <table style="width:100%;">
            <tr>
                <td class="formLabel requiredFieldLabel" style="width:20%;">
                        Title                 
                </td>
                <td class="tableSeperator" style="width:2%;"></td>
                <td class="formInput" style="width:78%;">
                    <asp:DropDownList ID="ddlTitle" runat="server" CssClass="dropDownStyle">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Mr.</asp:ListItem>
                        <asp:ListItem>Mrs.</asp:ListItem>
                        <asp:ListItem>Miss</asp:ListItem>
                        <asp:ListItem>Ms.</asp:ListItem>
                        <asp:ListItem>Mx.</asp:ListItem>
                        <asp:ListItem>Sir</asp:ListItem>
                        <asp:ListItem>Dr.</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTitle" InitialValue="" ErrorMessage="Please select an item." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="formLabel requiredFieldLabel">Name</td>
                <td></td>
                <td class="formInput">
                    <asp:TextBox ID="txtName" runat="server" CssClass="inputStyle" placeholder="Kelvin" Width="50%" ></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="formLabel requiredFieldLabel">Gender</td>
                <td></td>
                <td class="formInput">
                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="dropDownStyle">
                        <asp:ListItem>-- Please Select --</asp:ListItem>
                        <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlGender" InitialValue="-- Please Select --" ErrorMessage="Please select an item." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="formLabel requiredFieldLabel">ID Type</td>
                <td></td>
                <td class="formInput">
                    <asp:DropDownList ID="ddlIDType" runat="server" CssClass="dropDownStyle">
                        <asp:ListItem>-- Please Select --</asp:ListItem>
                        <asp:ListItem>NIC</asp:ListItem>
                        <asp:ListItem>Passport</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlIDType" InitialValue="-- Please Select --" ErrorMessage="Please select an item." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="formLabel requiredFieldLabel">ID No.</td>
                <td></td>
                <td class="formInput">
                    <asp:TextBox ID="txtIDNo" runat="server" CssClass="inputStyle" placeholder="xxxxxx-xx-xxxx" Width="50%" ></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtIDNo" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="formLabel requiredFieldLabel">Nationality</td>
                <td></td>
                <td class="formInput">
                    <asp:DropDownList ID="ddlNationality" runat="server" CssClass="dropDownStyle"> 
                        <asp:ListItem>-- Please Select --</asp:ListItem>
                        <asp:ListItem Value="Afganistan">Afghanistan</asp:ListItem>
                        <asp:ListItem Value="Albania">Albania</asp:ListItem>
                        <asp:ListItem Value="Algeria">Algeria</asp:ListItem>
                        <asp:ListItem Value="American Samoa">American Samoa</asp:ListItem>
                        <asp:ListItem Value="Andorra">Andorra</asp:ListItem>
                        <asp:ListItem Value="Angola">Angola</asp:ListItem>
                        <asp:ListItem Value="Anguilla">Anguilla</asp:ListItem>
                        <asp:ListItem Value="Antigua & Barbuda">Antigua & Barbuda</asp:ListItem>
                        <asp:ListItem Value="Argentina">Argentina</asp:ListItem>
                        <asp:ListItem Value="Armenia">Armenia</asp:ListItem>
                        <asp:ListItem Value="Aruba">Aruba</asp:ListItem>
                        <asp:ListItem Value="Australia">Australia</asp:ListItem>
                        <asp:ListItem Value="Austria">Austria</asp:ListItem>
                        <asp:ListItem Value="Azerbaijan">Azerbaijan</asp:ListItem>
                        <asp:ListItem Value="Bahamas">Bahamas</asp:ListItem>
                        <asp:ListItem Value="Bahrain">Bahrain</asp:ListItem>
                        <asp:ListItem Value="Bangladesh">Bangladesh</asp:ListItem>
                        <asp:ListItem Value="Barbados">Barbados</asp:ListItem>
                        <asp:ListItem Value="Belarus">Belarus</asp:ListItem>
                        <asp:ListItem Value="Belgium">Belgium</asp:ListItem>
                        <asp:ListItem Value="Belize">Belize</asp:ListItem>
                        <asp:ListItem Value="Benin">Benin</asp:ListItem>
                        <asp:ListItem Value="Bermuda">Bermuda</asp:ListItem>
                        <asp:ListItem Value="Bhutan">Bhutan</asp:ListItem>
                        <asp:ListItem Value="Bolivia">Bolivia</asp:ListItem>
                        <asp:ListItem Value="Bonaire">Bonaire</asp:ListItem>
                        <asp:ListItem Value="Bosnia & Herzegovina">Bosnia & Herzegovina</asp:ListItem>
                        <asp:ListItem Value="Botswana">Botswana</asp:ListItem>
                        <asp:ListItem Value="Brazil">Brazil</asp:ListItem>
                        <asp:ListItem Value="British Indian Ocean Ter">British Indian Ocean Ter</asp:ListItem>
                        <asp:ListItem Value="Brunei">Brunei</asp:ListItem>
                        <asp:ListItem Value="Bulgaria">Bulgaria</asp:ListItem>
                        <asp:ListItem Value="Burkina Faso">Burkina Faso</asp:ListItem>
                        <asp:ListItem Value="Burundi">Burundi</asp:ListItem>
                        <asp:ListItem Value="Cambodia">Cambodia</asp:ListItem>
                        <asp:ListItem Value="Cameroon">Cameroon</asp:ListItem>
                        <asp:ListItem Value="Canada">Canada</asp:ListItem>
                        <asp:ListItem Value="Canary Islands">Canary Islands</asp:ListItem>
                        <asp:ListItem Value="Cape Verde">Cape Verde</asp:ListItem>
                        <asp:ListItem Value="Cayman Islands">Cayman Islands</asp:ListItem>
                        <asp:ListItem Value="Central African Republic">Central African Republic</asp:ListItem>
                        <asp:ListItem Value="Chad">Chad</asp:ListItem>
                        <asp:ListItem Value="Channel Islands">Channel Islands</asp:ListItem>
                        <asp:ListItem Value="Chile">Chile</asp:ListItem>
                        <asp:ListItem Value="China">China</asp:ListItem>
                        <asp:ListItem Value="Christmas Island">Christmas Island</asp:ListItem>
                        <asp:ListItem Value="Cocos Island">Cocos Island</asp:ListItem>
                        <asp:ListItem Value="Colombia">Colombia</asp:ListItem>
                        <asp:ListItem Value="Comoros">Comoros</asp:ListItem>
                        <asp:ListItem Value="Congo">Congo</asp:ListItem>
                        <asp:ListItem Value="Cook Islands">Cook Islands</asp:ListItem>
                        <asp:ListItem Value="Costa Rica">Costa Rica</asp:ListItem>
                        <asp:ListItem Value="Cote DIvoire">Cote DIvoire</asp:ListItem>
                        <asp:ListItem Value="Croatia">Croatia</asp:ListItem>
                        <asp:ListItem Value="Cuba">Cuba</asp:ListItem>
                        <asp:ListItem Value="Curaco">Curacao</asp:ListItem>
                        <asp:ListItem Value="Cyprus">Cyprus</asp:ListItem>
                        <asp:ListItem Value="Czech Republic">Czech Republic</asp:ListItem>
                        <asp:ListItem Value="Denmark">Denmark</asp:ListItem>
                        <asp:ListItem Value="Djibouti">Djibouti</asp:ListItem>
                        <asp:ListItem Value="Dominica">Dominica</asp:ListItem>
                        <asp:ListItem Value="Dominican Republic">Dominican Republic</asp:ListItem>
                        <asp:ListItem Value="East Timor">East Timor</asp:ListItem>
                        <asp:ListItem Value="Ecuador">Ecuador</asp:ListItem>
                        <asp:ListItem Value="Egypt">Egypt</asp:ListItem>
                        <asp:ListItem Value="El Salvador">El Salvador</asp:ListItem>
                        <asp:ListItem Value="Equatorial Guinea">Equatorial Guinea</asp:ListItem>
                        <asp:ListItem Value="Eritrea">Eritrea</asp:ListItem>
                        <asp:ListItem Value="Estonia">Estonia</asp:ListItem>
                        <asp:ListItem Value="Ethiopia">Ethiopia</asp:ListItem>
                        <asp:ListItem Value="Falkland Islands">Falkland Islands</asp:ListItem>
                        <asp:ListItem Value="Faroe Islands">Faroe Islands</asp:ListItem>
                        <asp:ListItem Value="Fiji">Fiji</asp:ListItem>
                        <asp:ListItem Value="Finland">Finland</asp:ListItem>
                        <asp:ListItem Value="France">France</asp:ListItem>
                        <asp:ListItem Value="French Guiana">French Guiana</asp:ListItem>
                        <asp:ListItem Value="French Polynesia">French Polynesia</asp:ListItem>
                        <asp:ListItem Value="French Southern Ter">French Southern Ter</asp:ListItem>
                        <asp:ListItem Value="Gabon">Gabon</asp:ListItem>
                        <asp:ListItem Value="Gambia">Gambia</asp:ListItem>
                        <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                        <asp:ListItem Value="Germany">Germany</asp:ListItem>
                        <asp:ListItem Value="Ghana">Ghana</asp:ListItem>
                        <asp:ListItem Value="Gibraltar">Gibraltar</asp:ListItem>
                        <asp:ListItem Value="Great Britain">Great Britain</asp:ListItem>
                        <asp:ListItem Value="Greece">Greece</asp:ListItem>
                        <asp:ListItem Value="Greenland">Greenland</asp:ListItem>
                        <asp:ListItem Value="Grenada">Grenada</asp:ListItem>
                        <asp:ListItem Value="Guadeloupe">Guadeloupe</asp:ListItem>
                        <asp:ListItem Value="Guam">Guam</asp:ListItem>
                        <asp:ListItem Value="Guatemala">Guatemala</asp:ListItem>
                        <asp:ListItem Value="Guinea">Guinea</asp:ListItem>
                        <asp:ListItem Value="Guyana">Guyana</asp:ListItem>
                        <asp:ListItem Value="Haiti">Haiti</asp:ListItem>
                        <asp:ListItem Value="Hawaii">Hawaii</asp:ListItem>
                        <asp:ListItem Value="Honduras">Honduras</asp:ListItem>
                        <asp:ListItem Value="Hong Kong">Hong Kong</asp:ListItem>
                        <asp:ListItem Value="Hungary">Hungary</asp:ListItem>
                        <asp:ListItem Value="Iceland">Iceland</asp:ListItem>
                        <asp:ListItem Value="Indonesia">Indonesia</asp:ListItem>
                        <asp:ListItem Value="India">India</asp:ListItem>
                        <asp:ListItem Value="Iran">Iran</asp:ListItem>
                        <asp:ListItem Value="Iraq">Iraq</asp:ListItem>
                        <asp:ListItem Value="Ireland">Ireland</asp:ListItem>
                        <asp:ListItem Value="Isle of Man">Isle of Man</asp:ListItem>
                        <asp:ListItem Value="Israel">Israel</asp:ListItem>
                        <asp:ListItem Value="Italy">Italy</asp:ListItem>
                        <asp:ListItem Value="Jamaica">Jamaica</asp:ListItem>
                        <asp:ListItem Value="Japan">Japan</asp:ListItem>
                        <asp:ListItem Value="Jordan">Jordan</asp:ListItem>
                        <asp:ListItem Value="Kazakhstan">Kazakhstan</asp:ListItem>
                        <asp:ListItem Value="Kenya">Kenya</asp:ListItem>
                        <asp:ListItem Value="Kiribati">Kiribati</asp:ListItem>
                        <asp:ListItem Value="Korea North">Korea North</asp:ListItem>
                        <asp:ListItem Value="Korea Sout">Korea South</asp:ListItem>
                        <asp:ListItem Value="Kuwait">Kuwait</asp:ListItem>
                        <asp:ListItem Value="Kyrgyzstan">Kyrgyzstan</asp:ListItem>
                        <asp:ListItem Value="Laos">Laos</asp:ListItem>
                        <asp:ListItem Value="Latvia">Latvia</asp:ListItem>
                        <asp:ListItem Value="Lebanon">Lebanon</asp:ListItem>
                        <asp:ListItem Value="Lesotho">Lesotho</asp:ListItem>
                        <asp:ListItem Value="Liberia">Liberia</asp:ListItem>
                        <asp:ListItem Value="Libya">Libya</asp:ListItem>
                        <asp:ListItem Value="Liechtenstein">Liechtenstein</asp:ListItem>
                        <asp:ListItem Value="Lithuania">Lithuania</asp:ListItem>
                        <asp:ListItem Value="Luxembourg">Luxembourg</asp:ListItem>
                        <asp:ListItem Value="Macau">Macau</asp:ListItem>
                        <asp:ListItem Value="Macedonia">Macedonia</asp:ListItem>
                        <asp:ListItem Value="Madagascar">Madagascar</asp:ListItem>
                        <asp:ListItem Value="Malaysia">Malaysia</asp:ListItem>
                        <asp:ListItem Value="Malawi">Malawi</asp:ListItem>
                        <asp:ListItem Value="Maldives">Maldives</asp:ListItem>
                        <asp:ListItem Value="Mali">Mali</asp:ListItem>
                        <asp:ListItem Value="Malta">Malta</asp:ListItem>
                        <asp:ListItem Value="Marshall Islands">Marshall Islands</asp:ListItem>
                        <asp:ListItem Value="Martinique">Martinique</asp:ListItem>
                        <asp:ListItem Value="Mauritania">Mauritania</asp:ListItem>
                        <asp:ListItem Value="Mauritius">Mauritius</asp:ListItem>
                        <asp:ListItem Value="Mayotte">Mayotte</asp:ListItem>
                        <asp:ListItem Value="Mexico">Mexico</asp:ListItem>
                        <asp:ListItem Value="Midway Islands">Midway Islands</asp:ListItem>
                        <asp:ListItem Value="Moldova">Moldova</asp:ListItem>
                        <asp:ListItem Value="Monaco">Monaco</asp:ListItem>
                        <asp:ListItem Value="Mongolia">Mongolia</asp:ListItem>
                        <asp:ListItem Value="Montserrat">Montserrat</asp:ListItem>
                        <asp:ListItem Value="Morocco">Morocco</asp:ListItem>
                        <asp:ListItem Value="Mozambique">Mozambique</asp:ListItem>
                        <asp:ListItem Value="Myanmar">Myanmar</asp:ListItem>
                        <asp:ListItem Value="Nambia">Nambia</asp:ListItem>
                        <asp:ListItem Value="Nauru">Nauru</asp:ListItem>
                        <asp:ListItem Value="Nepal">Nepal</asp:ListItem>
                        <asp:ListItem Value="Netherland Antilles">Netherland Antilles</asp:ListItem>
                        <asp:ListItem Value="Netherlands">Netherlands (Holland, Europe)</asp:ListItem>
                        <asp:ListItem Value="Nevis">Nevis</asp:ListItem>
                        <asp:ListItem Value="New Caledonia">New Caledonia</asp:ListItem>
                        <asp:ListItem Value="New Zealand">New Zealand</asp:ListItem>
                        <asp:ListItem Value="Nicaragua">Nicaragua</asp:ListItem>
                        <asp:ListItem Value="Niger">Niger</asp:ListItem>
                        <asp:ListItem Value="Nigeria">Nigeria</asp:ListItem>
                        <asp:ListItem Value="Niue">Niue</asp:ListItem>
                        <asp:ListItem Value="Norfolk Island">Norfolk Island</asp:ListItem>
                        <asp:ListItem Value="Norway">Norway</asp:ListItem>
                        <asp:ListItem Value="Oman">Oman</asp:ListItem>
                        <asp:ListItem Value="Pakistan">Pakistan</asp:ListItem>
                        <asp:ListItem Value="Palau Island">Palau Island</asp:ListItem>
                        <asp:ListItem Value="Palestine">Palestine</asp:ListItem>
                        <asp:ListItem Value="Panama">Panama</asp:ListItem>
                        <asp:ListItem Value="Papua New Guinea">Papua New Guinea</asp:ListItem>
                        <asp:ListItem Value="Paraguay">Paraguay</asp:ListItem>
                        <asp:ListItem Value="Peru">Peru</asp:ListItem>
                        <asp:ListItem Value="Phillipines">Philippines</asp:ListItem>
                        <asp:ListItem Value="Pitcairn Island">Pitcairn Island</asp:ListItem>
                        <asp:ListItem Value="Poland">Poland</asp:ListItem>
                        <asp:ListItem Value="Portugal">Portugal</asp:ListItem>
                        <asp:ListItem Value="Puerto Rico">Puerto Rico</asp:ListItem>
                        <asp:ListItem Value="Qatar">Qatar</asp:ListItem>
                        <asp:ListItem Value="Republic of Montenegro">Republic of Montenegro</asp:ListItem>
                        <asp:ListItem Value="Republic of Serbia">Republic of Serbia</asp:ListItem>
                        <asp:ListItem Value="Reunion">Reunion</asp:ListItem>
                        <asp:ListItem Value="Romania">Romania</asp:ListItem>
                        <asp:ListItem Value="Russia">Russia</asp:ListItem>
                        <asp:ListItem Value="Rwanda">Rwanda</asp:ListItem>
                        <asp:ListItem Value="St Barthelemy">St Barthelemy</asp:ListItem>
                        <asp:ListItem Value="St Eustatius">St Eustatius</asp:ListItem>
                        <asp:ListItem Value="St Helena">St Helena</asp:ListItem>
                        <asp:ListItem Value="St Kitts-Nevis">St Kitts-Nevis</asp:ListItem>
                        <asp:ListItem Value="St Lucia">St Lucia</asp:ListItem>
                        <asp:ListItem Value="St Maarten">St Maarten</asp:ListItem>
                        <asp:ListItem Value="St Pierre & Miquelon">St Pierre & Miquelon</asp:ListItem>
                        <asp:ListItem Value="St Vincent & Grenadines">St Vincent & Grenadines</asp:ListItem>
                        <asp:ListItem Value="Saipan">Saipan</asp:ListItem>
                        <asp:ListItem Value="Samoa">Samoa</asp:ListItem>
                        <asp:ListItem Value="Samoa American">Samoa American</asp:ListItem>
                        <asp:ListItem Value="San Marino">San Marino</asp:ListItem>
                        <asp:ListItem Value="Sao Tome & Principe">Sao Tome & Principe</asp:ListItem>
                        <asp:ListItem Value="Saudi Arabia">Saudi Arabia</asp:ListItem>
                        <asp:ListItem Value="Senegal">Senegal</asp:ListItem>
                        <asp:ListItem Value="Seychelles">Seychelles</asp:ListItem>
                        <asp:ListItem Value="Sierra Leone">Sierra Leone</asp:ListItem>
                        <asp:ListItem Value="Singapore">Singapore</asp:ListItem>
                        <asp:ListItem Value="Slovakia">Slovakia</asp:ListItem>
                        <asp:ListItem Value="Slovenia">Slovenia</asp:ListItem>
                        <asp:ListItem Value="Solomon Islands">Solomon Islands</asp:ListItem>
                        <asp:ListItem Value="Somalia">Somalia</asp:ListItem>
                        <asp:ListItem Value="South Africa">South Africa</asp:ListItem>
                        <asp:ListItem Value="Spain">Spain</asp:ListItem>
                        <asp:ListItem Value="Sri Lanka">Sri Lanka</asp:ListItem>
                        <asp:ListItem Value="Sudan">Sudan</asp:ListItem>
                        <asp:ListItem Value="Suriname">Suriname</asp:ListItem>
                        <asp:ListItem Value="Swaziland">Swaziland</asp:ListItem>
                        <asp:ListItem Value="Sweden">Sweden</asp:ListItem>
                        <asp:ListItem Value="Switzerland">Switzerland</asp:ListItem>
                        <asp:ListItem Value="Syria">Syria</asp:ListItem>
                        <asp:ListItem Value="Tahiti">Tahiti</asp:ListItem>
                        <asp:ListItem Value="Taiwan">Taiwan</asp:ListItem>
                        <asp:ListItem Value="Tajikistan">Tajikistan</asp:ListItem>
                        <asp:ListItem Value="Tanzania">Tanzania</asp:ListItem>
                        <asp:ListItem Value="Thailand">Thailand</asp:ListItem>
                        <asp:ListItem Value="Togo">Togo</asp:ListItem>
                        <asp:ListItem Value="Tokelau">Tokelau</asp:ListItem>
                        <asp:ListItem Value="Tonga">Tonga</asp:ListItem>
                        <asp:ListItem Value="Trinidad & Tobago">Trinidad & Tobago</asp:ListItem>
                        <asp:ListItem Value="Tunisia">Tunisia</asp:ListItem>
                        <asp:ListItem Value="Turkey">Turkey</asp:ListItem>
                        <asp:ListItem Value="Turkmenistan">Turkmenistan</asp:ListItem>
                        <asp:ListItem Value="Turks & Caicos Is">Turks & Caicos Is</asp:ListItem>
                        <asp:ListItem Value="Tuvalu">Tuvalu</asp:ListItem>
                        <asp:ListItem Value="Uganda">Uganda</asp:ListItem>
                        <asp:ListItem Value="United Kingdom">United Kingdom</asp:ListItem>
                        <asp:ListItem Value="Ukraine">Ukraine</asp:ListItem>
                        <asp:ListItem Value="United Arab Erimates">United Arab Emirates</asp:ListItem>
                        <asp:ListItem Value="United States of America">United States of America</asp:ListItem>
                        <asp:ListItem Value="Uraguay">Uruguay</asp:ListItem>
                        <asp:ListItem Value="Uzbekistan">Uzbekistan</asp:ListItem>
                        <asp:ListItem Value="Vanuatu">Vanuatu</asp:ListItem>
                        <asp:ListItem Value="Vatican City State">Vatican City State</asp:ListItem>
                        <asp:ListItem Value="Venezuela">Venezuela</asp:ListItem>
                        <asp:ListItem Value="Vietnam">Vietnam</asp:ListItem>
                        <asp:ListItem Value="Virgin Islands (Brit)">Virgin Islands (Brit)</asp:ListItem>
                        <asp:ListItem Value="Virgin Islands (USA)">Virgin Islands (USA)</asp:ListItem>
                        <asp:ListItem Value="Wake Island">Wake Island</asp:ListItem>
                        <asp:ListItem Value="Wallis & Futana Is">Wallis & Futana Is</asp:ListItem>
                        <asp:ListItem Value="Yemen">Yemen</asp:ListItem>
                        <asp:ListItem Value="Zaire">Zaire</asp:ListItem>
                        <asp:ListItem Value="Zambia">Zambia</asp:ListItem>
                        <asp:ListItem Value="Zimbabwe">Zimbabwe</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlNationality" InitialValue="-- Please Select --" ErrorMessage="Please select an item." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="formLabel">
                    Date of Birth              
                </td>
                <td class="tableSeperator"></td>
                <td class="formInput">
                    <asp:TextBox ID="txtDOB" runat="server" CssClass="inputStyle inputDateStyle" Width="20%" type="date" Font-Size="16px" OnTextChanged="txtDOB_TextChanged" AutoPostBack="true"></asp:TextBox>
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
                <td class="formLabel requiredFieldLabel" style="width:20%;">Phone</td>
                <td class="tableSeperator" style="width:2%;"></td>
                <td class="formInput" style="width:78%;">
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="inputStyle" placeholder="60123456789" Width="50%" ></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPhone" ErrorMessage="Please enter a value." ValidationGroup="save" CssClass="validatorStyle"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Phone Number must contains digit only." ValidationExpression="\d+" ControlToValidate="txtPhone" CssClass="validatorStyle"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="formLabel">Email</td>
                <td></td>
                <td class="formInput">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="inputStyle" placeholder="abc@gmail.com" Width="50%" ></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator
                        id="regEmail"
                        ControlToValidate="txtEmail"
                        Text="Enter valid email address."
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        Runat="server" CssClass="validatorStyle" /> 
                </td>
            </tr>
        </table>

        <div class="formSectionStyle" >
            3. Preferences:-
        </div>

        <Preference:Control ID="PC1" runat="server"></Preference:Control>

        <div style="clear:both; width:100%; height:70px;">
            &nbsp;
        </div>

        <div class="bottomBar">

            <center>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="formBtnSave" ToolTip="Save" ValidationGroup="save" />
                <asp:Button ID="formBtnCancel" runat="server" Text="Reset" OnClick="formBtnCancel_Click" CssClass="formBtnCancel" ToolTip="Reset"/>
            </center>
                    
        </div>

        <%-- Popup Window --%>
        <div class="popup">
            <asp:Panel ID="PopupReset" runat="server" Visible="False" CssClass="popupWindow">

                <%-- Popup Window Title --%>
                <p style="color:red;" class="popupTitle">Reset Text Field?</p>

                <%-- Popup Window Body --%>
                <div class="popupBody">

                    <p>All text fields will be reset!</p>

                </div>

                <div>&nbsp;</div>

                <asp:Button ID="btnPopupCancel" runat="server" Text="Cancel" CssClass="popUpCancelBtn" OnClick="btnPopupCancel_Click"/>
                
                <asp:Button ID="btnPopupConfirmReset" runat="server" Text="Confirm" CssClass="popUpDeleteBtn" OnClick="btnPopupConfirmReset_Click"/>
            </asp:Panel>

            <%-- Popup Cover --%>
            <asp:Panel ID="PopupCover" runat="server" CssClass="popupCoverStyle" Visible="false">
                &nbsp;
            </asp:Panel>

        </div>

    </div>

</asp:Content>
