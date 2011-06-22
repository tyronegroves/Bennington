<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Bennington.Cms.Models.IconMenuViewModel>" %>
<ul class="menu">
	<% foreach (var menuItem in Model.IconMenuItems) { %>
		<li><a href="<%=Url.Action(menuItem.Action, menuItem.Controller, menuItem.RouteValues, null) %>"><img src="<%=menuItem.IconUrl %>" alt="<%=menuItem.Name %>" /></a></li>
	<% } %>
</ul>