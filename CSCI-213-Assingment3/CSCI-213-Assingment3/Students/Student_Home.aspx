<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Student_Home.aspx.cs" Inherits="CSCI_213_Assingment3.Students.Student_Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
        .auto-style2 {
            font-size: xx-small;
        }
        .auto-style3 {
            font-size: large;
        }
        .auto-style4 {
            font-size: medium;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
    </p>
    
        <table style="width:100%;">
            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td><strong><span class="auto-style3">Your current appointments 
                    </span></strong> 
                    <br />
                    <asp:ListBox ID="ListBox1" runat="server" Height="77px" Width="196px" CssClass="auto-style4"></asp:ListBox>
                    <br />
                    <asp:Button ID="cancelBtn" runat="server" Text="Cancel Appointment" CssClass="auto-style2" OnClick="cancelBtn_Click" Width="122px" Font-Size="Small" Height="23px" />    
                    &nbsp;&nbsp;
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Students/Apointment_Page.aspx">Make a new appointment</asp:HyperLink>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
  
    <p>
    </p>
</asp:Content>
