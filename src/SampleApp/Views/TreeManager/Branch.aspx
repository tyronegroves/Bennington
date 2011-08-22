<%@ Import Namespace="Bennington.ContentTree.Models" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.master" Inherits="System.Web.Mvc.ViewPage<Bennington.ContentTree.TreeManager.Models.TreeBranchViewModel>" %>
<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    
	<% foreach (var treeNode in Model.TreeNodeSummaries) { %>
		<li id="<%=treeNode.Id %>" class="<% if (treeNode.HasChildren) { %>jstree-closed<% } %>">
			<span class="noicon">
				<a href="#TB_inline?height=155&width=300&inlineId=createFormContainer&modal=false" class="thickbox" onclick="$('#TreeNodeCreationInputModel_ParentTreeNodeId').val('<%=treeNode.Id %>');$('#createInRootLink').click();"><img src="/MANAGE/Content/PlusSign.gif" alt="Create a child" /></a>
			</span>
			<%=Html.ActionLink(treeNode.Name, treeNode.ActionToUseForModification, treeNode.ControllerToUseForModification, treeNode.RouteValuesForModification, new { @class="" }) %>
		</li>
	<% } %>
    
</asp:Content>

    