<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>This is the Contact Us controller's Confirmation view.</h2>
    
	<%=Html.ActionLink("This is a link to the index action", "Index", new { id = 2 } ) %>
</asp:Content>
