<%@ Import Namespace="Paragon.ContentTree.ContentNodeProvider.Models" %>

<%@ Page Language="C#" MasterPageFile="~/Views/Shared/ManageSite.Master" Inherits="System.Web.Mvc.ViewPage<Paragon.ContentTree.ContentNodeProvider.Models.ContentTreeNodeViewModel>" %>

<%@ Import Namespace="System.Web.Mvc" %>

<%@ Import Namespace="System.Security.Policy" %>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" src="/Scripts/tiny_mce/tiny_mce.js"></script>

<script type="text/javascript">
	tinyMCE.init({
		// General options
		mode: "textareas",
		theme: "advanced",
		plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",

		// Theme options
		theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
		theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
		theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
		theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak",
		theme_advanced_toolbar_location: "top",
		theme_advanced_toolbar_align: "left",
		theme_advanced_statusbar_location: "bottom",
		theme_advanced_resizing: true,

		// Example content CSS (should be your site CSS)
		//content_css: "css/example.css",

		// Drop lists for link/image/media/template dialogs
		template_external_list_url: "js/template_list.js",
		external_link_list_url: "js/link_list.js",
		external_image_list_url: "js/image_list.js",
		media_external_list_url: "js/media_list.js",

		// Replace values for the template plugin
		/*
		template_replace_values: {
			username: "Some User",
			staffid: "991234"
		}*/
	});

</script>


	<% using (Html.BeginForm(Model.Action, "HomepageContentTreeNode", FormMethod.Post, new { Id = "form" })) { %>

	<div class="contentNodeProviderForm">

		<%--<%=Html.EditorFor(x => x.ContentTreeNodeInputModel)%>--%>

        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <div class="editor-field" style="display:none">
                <%: Html.TextBoxFor(model => model.ContentTreeNodeInputModel.Action) %>
                <%: Html.ValidationMessageFor(model => model.ContentTreeNodeInputModel.Action) %>
            </div>

            <div class="editor-field" style="display:none">
                <%: Html.TextBoxFor(model => model.ContentTreeNodeInputModel.FormAction) %>
                <%: Html.ValidationMessageFor(model => model.ContentTreeNodeInputModel.FormAction) %>
            </div>

            <div class="editor-field" style="display:none">
                <%: Html.TextBoxFor(model => model.ContentTreeNodeInputModel.Type) %>
                <%: Html.ValidationMessageFor(model => model.ContentTreeNodeInputModel.Type) %>
            </div>
            
            <div class="editor-field" style="display:none">
                <%: Html.TextBoxFor(model => model.ContentTreeNodeInputModel.TreeNodeId) %>
                <%: Html.ValidationMessageFor(model => model.ContentTreeNodeInputModel.TreeNodeId) %>
            </div>
            
            <div class="editor-field" style="display:none">
                <%: Html.TextBoxFor(model => model.ContentTreeNodeInputModel.PageId) %>
                <%: Html.ValidationMessageFor(model => model.ContentTreeNodeInputModel.PageId) %>
            </div>
            
            <div class="editor-field" style="display:none">
                <%: Html.TextBoxFor(model => model.ContentTreeNodeInputModel.ParentTreeNodeId) %>
                <%: Html.ValidationMessageFor(model => model.ContentTreeNodeInputModel.ParentTreeNodeId) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ContentTreeNodeInputModel.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ContentTreeNodeInputModel.Name) %>
                <%: Html.ValidationMessageFor(model => model.ContentTreeNodeInputModel.Name) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ContentTreeNodeInputModel.HeaderText) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ContentTreeNodeInputModel.HeaderText) %>
                <%: Html.ValidationMessageFor(model => model.ContentTreeNodeInputModel.HeaderText) %>
            </div>
            
            <div class="editor-field"  style="display:none">
                <%: Html.TextBoxFor(model => model.ContentTreeNodeInputModel.Sequence) %>
                <%: Html.ValidationMessageFor(model => model.ContentTreeNodeInputModel.Sequence) %>
            </div>
            
            <div class="editor-field" style="display:none">
                <%: Html.TextBoxFor(model => model.ContentTreeNodeInputModel.UrlSegment) %>
                <%: Html.ValidationMessageFor(model => model.ContentTreeNodeInputModel.UrlSegment) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ContentTreeNodeInputModel.Body) %>
            </div>
            <div class="editor-field">
                <%: Html.TextAreaFor(model => model.ContentTreeNodeInputModel.Body) %>
                <%: Html.ValidationMessageFor(model => model.ContentTreeNodeInputModel.Body) %>
            </div>
            
        </fieldset>

		<div class="commandButtonContainer">
			<input type="button" class="button" value="Cancel" onclick="window.location='<%=Url.Action("Index", "ContentTree") %>';" />
			<input type="button" class="button" value="Save" onclick="$('#ContentTreeNodeInputModel_FormAction').val(this.value);/*tinyMCE.triggerSave();*/$('#form').submit();" />
			<input type="button" class="button" value="Save And Exit" onclick="$('#ContentTreeNodeInputModel_FormAction').val(this.value);/*tinyMCE.triggerSave();*/$('#form').submit();" />
			
			<% if (ViewContext.RouteData.Values["Action"].ToString() == "Modify") { %>
			<input type="button" class="button" value="Publish" onclick="$('#ContentTreeNodeInputModel_FormAction').val(this.value);$('#form').submit();" />
			<% } %>
		</div>
	</div>
	<% } %>
</asp:Content>
