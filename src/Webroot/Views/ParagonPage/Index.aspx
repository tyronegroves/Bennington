<%@ Page Title="Content Tree" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage"%>
<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
	<h2>This is the Paragon controller's Index view</h2>

	<%=Html.ActionLink("contact us index", "Index", "ContactUs", new { test = "test" }, null) %>
	<br />
	<%=Html.ActionLink("contact us confirmation", "Confirmation", "ContactUs", new { test = "test" }, null) %>

	<%=Html.ActionLink("faq index", "Index", "Faq", new { test = "test" }, null) %>
	<br />
	<%=Html.ActionLink("faq about", "About", "Faq", new { test = "test" }, null) %>
</asp:Content>
