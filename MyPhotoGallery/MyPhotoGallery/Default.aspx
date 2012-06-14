<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MyPhotoGallery._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table>
            <tr>
                <td>Title:
                <asp:TextBox id="tbTitle" Width="100%" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Upload</td>
            </tr>
            <tr>
                <td><asp:FileUpload ID="FileUpload1" runat="server"/></td>
            </tr>
            <tr>
                <td>or</td>
            </tr>
            <tr>
                <td>
                    <div style="border:1px solid black; text-align:center">Drop file here</div>
                </td>
            </tr>
        </table>

        <asp:Label ID="errorLabel" runat="server"></asp:Label><br />
        <asp:Button ID="uploadButton" Text="Upload photo" runat="server" 
            onclick="uploadButton_Click"/>
    </div>
    <asp:ListView ID="ListView_Photos" runat="server" DataSourceID="EDS_Photos" GroupItemCount="5">
        <EmptyDataTemplate>
            <table runat="server">
                <tr>
                    <td>No photos.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
            <td runat="server"></td>
        </EmptyItemTemplate>
        <GroupTemplate>
            <tr id="itemPlaceholderContainer" runat="server">
                <td id="itemPlaceholder" runat="server"></td>
            </tr>
        </GroupTemplate>
        <ItemTemplate>
            <td runat="server">
                <table border="0">
                    <tr>
                        <td>
                            <a class="lightbox" href="Images/<%# Eval("fileName") %>"><img class="thumb" alt="" src="Images/<%# Eval("fileName") %>" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <a href="PhotoDetails.aspx?photoId=<%# Eval("id") %>"><%# Eval("title") %></a>
                        </td>
                    </tr>
                </table>
            </td>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="groupPlaceholderContainer" runat="server">
                            <tr id="groupPlaceholder" runat="server"></tr>
                        </table>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
    <asp:EntityDataSource ID="EDS_Photos" runat="server" 
        ConnectionString="name=GalleryEntities" DefaultContainerName="GalleryEntities" 
        EnableFlattening="False" EntitySetName="Photos"
        OrderBy="it.postingDate DESC">
    </asp:EntityDataSource>
</asp:Content>
