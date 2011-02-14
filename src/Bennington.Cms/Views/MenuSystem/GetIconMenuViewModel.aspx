<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Bennington.Cms.Models.IconMenuViewModel>" %>
<ul class="menu">
	<% foreach (var menuItem in Model.IconMenuItems) { %>
		<li><%=Html.ActionLink(menuItem.Name, menuItem.Action, menuItem.Controller, menuItem.RouteValues, null) %></li>
	<% } %>
</ul>