﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Student_Home.aspx.cs" Inherits="CSCI_213_Assingment3.Students.Student_Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            font-size: xx-small;
        }
        .auto-style3 {
            font-size: large;
        }
        .auto-style4 {
            font-size: medium;
        }
        .auto-style5 {
            height: 25px;
            width: 354px;
        }
        .auto-style6 {
            width: 354px;
        }
        .auto-style7 {
            height: 25px;
            width: 187px;
        }
        .auto-style8 {
            width: 187px;
        }
        .auto-style9 {
            width: 187px;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
    </p>
    
        <table style="width:100%;">
            <tr>
                <td class="auto-style5">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Students/Apointment_Page.aspx">Make a new appointment</asp:HyperLink>
                </td>
                <td class="auto-style7">&nbsp;</td>
                <td colspan="1" rowspan="3">
                    <asp:DetailsView ID="DetailsView2" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSource1" Height="205px" Width="605px">
                        <Fields>
                            <asp:BoundField DataField="EmailDate" HeaderText="EmailDate" SortExpression="EmailDate" />
                            <asp:BoundField DataField="EmailFrom" HeaderText="From" SortExpression="EmailFrom" />
                            <asp:BoundField DataField="EmailTo" HeaderText="To" SortExpression="EmailTo" />
                            <asp:BoundField DataField="EmailText" HeaderText="Content" SortExpression="EmailText" />
                        </Fields>
                    </asp:DetailsView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [EmailDate], [EmailFrom], [EmailTo], [EmailText] FROM [MessagesTable] ORDER BY [EmailDate], [EmailFrom]"></asp:SqlDataSource>
                </td>
                
                <td rowspan="6">
                    &nbsp;</td>
                
            </tr>
            <tr>
                <td class="auto-style6"><strong><span class="auto-style3">Your current appointments 
                    </span></strong> 
                    <br />
                    <asp:ListBox ID="ListBox1" runat="server" Height="77px" Width="196px" CssClass="auto-style4"></asp:ListBox>
                    <br />
                    <asp:Button ID="cancelBtn" runat="server" Text="Cancel Appointment" CssClass="auto-style2" OnClick="cancelBtn_Click" Width="122px" Font-Size="Small" Height="23px" />    
                    &nbsp;&nbsp;
                </td>
                <td class="auto-style8">Messages <br />
                    <asp:ListBox ID="ListBox2" runat="server" Height="96px" Width="171px"></asp:ListBox>
                    <asp:Button ID="SelectMsgBtn" runat="server" Text="Select" />
&nbsp;
                    <asp:Button ID="DelMsgBtn" runat="server" Text="Delete" />
                </td>
                
            </tr>
            <tr>
                <td class="auto-style6">
                    &nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
              
            </tr>
            <tr>
                <td class="auto-style6">
                    &nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
              
            </tr>
            <tr>
                <td class="auto-style6">
                    &nbsp;</td>
                <td class="auto-style9">New Message To Advisor:</td>
                <td rowspan="2">
                    <asp:TextBox ID="TextBox1" runat="server" Height="104px" Width="594px"></asp:TextBox><br />
                    <asp:Button ID="SendMsgBtn" runat="server" Text="Send" />
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    &nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
              
            </tr>
        </table>
  
    <p>
    </p>
</asp:Content>
