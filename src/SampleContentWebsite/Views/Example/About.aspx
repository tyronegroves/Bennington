<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">

    <p>This is the example controller About view.</p>

    <p><%=Html.ActionLink("Link to example controller Index view", "Index", "Example") %></p>

</asp:Content>
