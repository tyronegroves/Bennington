<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">

    <p>This is the example controller Index view.</p>

    <p><%=Html.ActionLink("Link to example controller About view", "About", "Example") %></p>

</asp:Content>
