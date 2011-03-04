<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ManageSite.Master" Inherits="System.Web.Mvc.ViewPage<Bennington.Cms.PrincipalProvider.Models.IndexViewModel>" %>
<%@ Import Namespace="Bennington.Cms.PrincipalProvider.Controllers" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="Bennington.Cms.PrincipalProvider.Models" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="padding-top:40px;">

	<% if (Model.Users.Count() > 0) { %>

		<% Html.Grid(Model.Users)
		   .Columns(column =>
		   {
			   column.For(item => Html.ActionLink(item.Username, "Modify", new { id = item.Id }, null)).Named("Username");
			   column.For(c => c.FirstName);
			   column.For(c => c.LastName);
			   column.For(c => c.Email);
		   }).Attributes(id => Model.Users.GetType().FullName + "Grid").Render();
		%>

	<% } %>

	<% if (Model.Users.Count() == 0) { %>
	No items found.
	<% } %>

	<p>
		<input type="button" onclick="window.location='<%=Url.Action("Create") %>';" value="Create" class="button">
	</p>

</div>
</asp:Content>