<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Student_Home.aspx.cs" Inherits="CSCI_213_Assingment3.Students.Student_Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            font-size: large;
            background-color: #FFFFFF;
        }
        .auto-style5 {
            height: 25px;
            width: 354px;
        }
        .auto-style6 {
            width: 354px;
        }
        .auto-style8 {
            width: 187px;
        }
        .auto-style9 {
            width: 187px;
            text-align: left;
            height: 156px;
        }
        .auto-style10 {
            width: 100%;
        }
        .auto-style11 {
            width: 354px;
            height: 156px;
            text-align: right;
        }
        .auto-style12 {
            color: #FF0000;
            background-color: #FFFFFF;
        }
        .auto-style13 {
            background-color: #FFFFFF;
        }
        .auto-style14 {
            width: 187px;
            background-color: #FFFFFF;
        }
        .auto-style15 {
            width: 354px;
            background-color: #FFFFFF;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <span class="auto-style13">Welcome Back
        </span>
        <asp:Label ID="nameLbl" runat="server" Text="Label" CssClass="auto-style13"></asp:Label>
    </p>
    
        <table class="auto-style10">
            <tr>
                <td class="auto-style5" colspan="2">
                    <strong><span class="auto-style3">Your current appointments 
                    </span></strong> 
  
    <asp:GridView ID="appointmentsView" runat="server" AutoGenerateSelectButton="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="appointmentsView_SelectedIndexChanged" Width="1116px">
        <AlternatingRowStyle BackColor="White" />
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
  
    &nbsp;<asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="auto-style13" OnClick="Button1_Click" Text="New Appointment " />
&nbsp;&nbsp;&nbsp;
  
    <asp:Button ID="deleteBtn" runat="server" OnClick="deleteBtn_Click" Text="Cancel Appointment" CausesValidation="False" CssClass="auto-style13" />
  
                </td>
                
                
                
            </tr>
            <tr>
                <td class="auto-style6">
                    <br class="auto-style13" />
                    <span class="auto-style13">&nbsp;&nbsp;
                </span>
                <asp:GridView ID="EmailView" runat="server" AutoGenerateSelectButton="True" CellPadding="4" ClientIDMode="Static" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="EmailView_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
                    <asp:Button ID="deleteEmBtn" runat="server" OnClick="deleteEmBtn_Click" Text="Delete Email" CausesValidation="False" CssClass="auto-style13" />
                </td>
                <td class="auto-style8">&nbsp;<br class="auto-style13" />
                    <span class="auto-style13">&nbsp;
                    </span>
                    <asp:DetailsView ID="DetailEmailView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="156px" Width="521px">
                        <AlternatingRowStyle BackColor="White" />
                        <CommandRowStyle BackColor="#FFFFC0" Font-Bold="True" />
                        <FieldHeaderStyle BackColor="#FFFF99" Font-Bold="True" />
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    </asp:DetailsView>
                </td>
                
            </tr>
            <tr>
                <td></td>
            </tr>
           
            <tr>
                <td class="auto-style11">
                    <br class="auto-style13" />
                    <asp:Label ID="messageLb" runat="server" Text="New Message To Advisor" CssClass="auto-style13"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:TextBox ID="TextBox1" runat="server" Height="104px" Width="594px"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" CssClass="auto-style12" ErrorMessage="Enter Message"></asp:RequiredFieldValidator>
                </td>
               
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style6" rowspan="5">
                    <asp:GridView ID="StudentsView" runat="server" AutoGenerateSelectButton="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="StudentsView_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                </td>
                <td class="auto-style8">
                    <asp:Button ID="SendMsgBtn" runat="server" Text="Send" OnClick="SendMsgBtn_Click" CssClass="auto-style13" />
                </td>
              
            </tr>
            <tr>
                <td class="auto-style6">
                    </td>
                <td class="auto-style14">
                    &nbsp;</td>
              
            </tr>
            <tr>
                <td class="auto-style15">
                    &nbsp;</td>
                <td class="auto-style14">
                    &nbsp;</td>
              
            </tr>
            <tr>
                <td class="auto-style15">
                    &nbsp;</td>
                <td class="auto-style14">
                    &nbsp;</td>
              
            </tr>
        </table>
  
    <p>
        <asp:Button ID="AdvisorViewAll" runat="server" Text="Advisor View All" CausesValidation="False" CssClass="auto-style13" />
        <span class="auto-style13">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </span>
        <asp:Button ID="AdvisorViewOwn" runat="server" OnClick="AdvisorViewOwn_Click" Text="Advisor View Own" CausesValidation="False" CssClass="auto-style13" />
    </p>
</asp:Content>
