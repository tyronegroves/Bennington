<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ManageSite.Master" Inherits="System.Web.Mvc.ViewPage<Bennington.Cms.PrincipalProvider.Models.IndexViewModel>" %>
<%@ Import Namespace="Bennington.Cms.PrincipalProvider.Controllers" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="Bennington.Cms.PrincipalProvider.Models" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" charset="utf-8">
	$(document).ready(function () {
		$('#gridView').dataTable();
	});
</script>

<style>
#gridView { width: 100%; clear:both; margin-top:10px; }
#gridView th { text-align: left; }
#gridView_filter { float:left; clear:none; }
#gridView_length { clear:none; float:right; }
#container { width: 600px; }
</style>


<div id="gridContainer" style="padding-top:40px;">

	<% if (Model.Users.Count() > 0) { %>

		<% Html.Grid(Model.Users)
		   .Columns(column =>
		   {
			   column.For(item => Html.ActionLink(item.Username, "Modify", new { id = item.Id }, null)).Named("Username");
			   column.For(c => c.FirstName);
			   column.For(c => c.LastName);
			   column.For(c => c.Email);
		   }).Attributes(id => "gridView").Render();
		%>

	<% } %>

	<% if (Model.Users.Count() == 0) { %>
	No items found.
	<% } %>

	<p style="padding-top:20px;">
		<input type="button" onclick="window.location='<%=Url.Action("Create") %>';" value="Create" class="button">
	</p>

</div>
</asp:Content>