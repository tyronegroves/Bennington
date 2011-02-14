<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>This is the Contact Us controller's Index view.</h2>
    
	<%=Html.ActionLink("This is a link to the confirmation action", "Confirmation", new { id = 2 } ) %>
</asp:Content>
