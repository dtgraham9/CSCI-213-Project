<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Apointment_Page.aspx.cs" Inherits="CSCI_213_Assingment3.Students.Apointment_Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 272px;
        }
        .auto-style2 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style2">
        <tr>
            <td class="auto-style1">Select the Date of Appointment.<br />
                <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
            </td>
            
        </tr>
        <tr>
            <td class="auto-style1"> Time of the appointment
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">Reason For Appointment<asp:TextBox ID="TextBox1" runat="server" Height="51px" Width="315px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Button ID="submitBtn" runat="server" Text="Submit" />
&nbsp;&nbsp;
                <asp:Button ID="clearBtn" runat="server" Text="Clear" />
            </td>
        </tr>
    </table>
</asp:Content>
