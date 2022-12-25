<%@ Page Title="" Language="C#" MasterPageFile="~/hacims_masterpage_admin.master" AutoEventWireup="true" CodeFile="Decrypt.aspx.cs" Inherits="Decrypt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <%-- <asp:GridView runat="server" id="grd" AutoGenerateColumns="False" 
        DataKeyNames="RegNo" DataSourceID="SqlDataSource1" 
        onrowdatabound="grd_RowDataBound">
        <Columns>
            <asp:BoundField DataField="RegNo" HeaderText="RegNo" ReadOnly="True" 
                SortExpression="RegNo"></asp:BoundField>

                <asp:TemplateField>
               <ItemTemplate>
                 <asp:Label ID="lblNewReg" runat="server"></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField>
               <ItemTemplate>
                 <asp:Label ID="lblPFName" runat="server"></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField>
               <ItemTemplate>
                 <asp:Label ID="lblPLName" runat="server"></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Reg_Man %>" 
        SelectCommand="SELECT [RegNo],PFName,PLName FROM [Patient]" InsertCommand="insert into patientTemp(regno,regnonew,fname,lname) values(@regno,@regnonew,@fname,@lname)" ></asp:SqlDataSource>
     <insertparameters>
            <asp:ControlParameter ControlID="HiddenField_Msg_Mr_No" Name="Reg_No" PropertyName="Value"
                Type="String" />
                </insertparameters>
--%>

                
</asp:Content>
