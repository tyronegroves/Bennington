<%@ Import Namespace="Paragon.ContentTree.Models" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/ManageSite.Master" Inherits="System.Web.Mvc.ViewPage<Paragon.ContentTree.Models.TreeNodeCreationInputModel>" %>
<%@ Import Namespace="System.Security.Policy" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Content Tree
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">

	<script type="text/javascript">
		$(document).ready(function () {
			$(".contentTree").jstree({
				"html_data": {
					"ajax": {
						"url": "<%=Url.Action("Branch", "TreeManager") %>",
						"data": function (n) {
							return { id: n.attr ? n.attr("id") : '00000000-0000-0000-0000-000000000000' };
						}
					}
				},
				"plugins": ["themes", "html_data"/*, 'ui', 'checkbox'*/]
			});

			$("#ProviderType").change(function () {
				var selected = $("#ProviderType option:selected");
				var output = "";
				if (selected.val() != "") {
					document.forms[0].action = $("#ProviderType").val();
				}
			});
		});
	</script>

    <div id="pageheader" class="clearfix">
                
        <%--<h1>Content Tree</h1>--%>

    </div>

    <div id="tab1" class="tabContent">
        <div class="section">
            <div class="createFormContainer">
				<% using (Html.BeginForm("Index", "ContentTree", FormMethod.Get, new {  })) { %>
					<%=Html.EditorForModel() %>
					<input type="button" class="button" value="Create" onclick="window.location=$('#ProviderType').val() + '&parentTreeNodeId=' + $('#ParentTreeNodeId').val();" />
				<% } %>
			</div>
			<div class="contentTreeContainer clearfix">
                <div class="contentTree">
                </div>
                <%--<div class="contentTreePreview">
                    <h2>Title of selected item</h2>
                    <div class="content">                
                        <p>Last Updated: 04/12/09 - 4:23 PM CST</p>
                        <p>Last Update by: Kristopher Knowles</p>
                        <p>Version: 4</p>
                        <p>Active: <strong>Yes</strong></p>
                    </div>
                </div>--%>
            </div>
			<a href="#" class="noicon" onclick="$('#ParentTreeNodeId').val('00000000-0000-0000-0000-000000000000');return(false);">[Create in Root]</a>
        </div>

    </div>

    <div id="tab2" class="tabContent">
        <div class="section">
            <div class="content">
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi id lacinia est. Donec consequat tristique ullamcorper. Nunc et magna ut felis mollis porttitor. In hac habitasse platea dictumst. Duis et molestie elit. In hac habitasse platea dictumst. Sed dapibus, sem eget iaculis faucibus, mauris ante ultrices quam, id viverra nisl ipsum id mi. In et volutpat libero. Donec ultricies porttitor consectetur. Mauris non orci velit. Nulla facilisi. Mauris ullamcorper consectetur erat a posuere. Sed interdum neque eu mi lobortis posuere. Nunc adipiscing ipsum in velit aliquam id mollis urna consequat.</p>

            <p>Pellentesque gravida congue molestie. Mauris turpis orci, vehicula sit amet accumsan nec, pellentesque quis felis. Aenean volutpat rhoncus egestas. Donec sit amet est id urna luctus sodales eget ac ante. Nunc gravida, quam sed blandit commodo, risus lacus semper metus, et consequat metus felis a orci. Proin sit amet diam ut ligula venenatis convallis. Maecenas sodales tortor at nisl egestas semper. Sed dictum diam magna.</p>
            </div>
        </div>

    </div>
</asp:Content>