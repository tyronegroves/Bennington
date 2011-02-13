<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Paragon.Cms.Models.SectionMenuViewModel>" %>
<ul class="menu clearfix">
	<% foreach (var menuItem in Model.MenuItems) { %>
		<li><%=Html.ActionLink(menuItem.Name, menuItem.Action, menuItem.Controller, menuItem.RouteValues, null) %></li>
	<% } %>
</ul>