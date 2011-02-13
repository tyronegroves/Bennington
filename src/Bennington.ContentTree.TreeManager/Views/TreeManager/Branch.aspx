<%@ Import Namespace="Paragon.ContentTree.Models" %>
<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Paragon.ContentTree.TreeManager.Models.TreeBranchViewModel>" %>
	<% foreach (var treeNode in Model.TreeNodeSummaries) { %>
		<li id="<%=treeNode.Id %>" class="<% if (treeNode.HasChildren) { %>jstree-closed<% } %>">
			<span class="noicon">
				<a href="#TB_inline?height=155&width=300&inlineId=createFormContainer&modal=false" class="thickbox" onclick="$('#TreeNodeCreationInputModel_ParentTreeNodeId').val('<%=treeNode.Id %>');$('#createInRootLink').click();">[Create a child page from here]</a>
			</span>
			<%=Html.ActionLink(treeNode.Name, treeNode.ActionToUseForModification, treeNode.ControllerToUseForModification, treeNode.RouteValuesForModification, new { @class="" }) %>
		</li>
	<% } %>