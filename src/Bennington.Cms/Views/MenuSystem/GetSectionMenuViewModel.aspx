<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Bennington.Cms.Models.SectionMenuViewModel>" %>

<div id="menucontainer" class="clearfix">

    <ul class="menu clearfix">
	<% foreach (var menuItem in Model.MenuItems) { %>
		<li><%=Html.ActionLink(menuItem.Name, menuItem.Action, menuItem.Controller, menuItem.RouteValues, null) %></li>
	<% } %>
    </ul>

</div>