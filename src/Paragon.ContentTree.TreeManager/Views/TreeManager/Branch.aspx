<%@ Import Namespace="Paragon.ContentTree.Models" %>
<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Paragon.ContentTree.TreeManager.Models.TreeBranchViewModel>" %>
<%--<ul class="menu">--%>
	<% foreach (var treeNode in Model.TreeNodeSummaries) { %>
		<li id="<%=treeNode.Id %>" class="<% if (treeNode.HasChildren) { %>jstree-closed<% } %>">
			<%=Html.ActionLink(treeNode.Name, treeNode.ActionToUseForModification, treeNode.ControllerToUseForModification, treeNode.RouteValuesForModification, new { @class="" }) %>
			<%--<%=Html.ActionLink("[Create child]", treeNode.ActionToUseForCreation, treeNode.ControllerToUseForCreation, treeNode.RouteValuesForCreation, new { @class="noicon" }) %>--%>
			<a href="#" class="noicon" onclick="$('#ParentTreeNodeId').val('<%=treeNode.ParentTreeNodeId %>');return(false);">[Create child]</a>
		</li>
	<% } %>
<%--</ul>--%>