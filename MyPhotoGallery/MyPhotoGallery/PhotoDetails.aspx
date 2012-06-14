<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PhotoDetails.aspx.cs" Inherits="MyPhotoGallery.PhotoDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="FormView1" runat="server" DataSourceID="EDS_Photo">
        <ItemTemplate>
            <table border="0">
                    <tr>
                        <td>
                            <div><%# Eval("title") %></div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <img src="Images/<%# Eval("fileName") %>" /> 
                        </td>
                    </tr>
                </table>
        </ItemTemplate>
    </asp:FormView>
    <asp:EntityDataSource ID="EDS_Photo" runat="server" AutoGenerateWhereClause="true"
        ConnectionString="name=GalleryEntities" DefaultContainerName="GalleryEntities" 
        EnableFlattening="False" EntitySetName="Photos">
        <WhereParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="photoId" Type="Int32" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:ListView ID="ListView1" runat="server" DataSourceID="EDS_CommentsList">
        <ItemTemplate>
            <tr>
                <td><%# Eval("name") %></td>
                <td><%# Eval("text") %></td>
            </tr>        
        </ItemTemplate>
        <EmptyDataTemplate>
            <table>
                <tr><td>No comments.</td></tr>
            </table>        
        </EmptyDataTemplate>
        <LayoutTemplate>
            <table>
                <tr>
                    <td>
                        <table id="itemPlaceholderContainer" runat="server">
                            <tr>
                                <th>Name</th>
                                <th>Comment</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </table>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
    <asp:EntityDataSource ID="EDS_CommentsList" runat="server" 
        ConnectionString="name=GalleryEntities" DefaultContainerName="GalleryEntities" 
        EnableFlattening="False" EntitySetName="Comments" AutoGenerateWhereClause="true">
        <WhereParameters>
            <asp:QueryStringParameter Name="photoId" QueryStringField="photoId" Type="Int32" />
        </WhereParameters>
    </asp:EntityDataSource>

    <table>
        <tr>
            <td>Name</td>
            <td><asp:TextBox ID="tbName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Comment</td>
            <td><asp:TextBox ID="tbComment" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button Width="100%" Text="Отправить" ID="btnPostComment" runat="server" 
                    onclick="btnPostComment_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
