<%@ Page Title="Content Tree" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage"%>
<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
	Paragon Page <br />

	<%=Html.ActionLink("contact us index", "Index", "ContactUs", new { test = "test" }, null) %>
	<br />
	<%=Html.ActionLink("contact us confirmation", "Confirmation", "ContactUs", new { test = "test" }, null) %>
</asp:Content>
