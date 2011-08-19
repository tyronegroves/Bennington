<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.ContentTree.Providers.ContentNodeProvider.Models.ContentTreeNodeInputModel>" %>



<%--<script type="text/javascript">
    $(document).ready(function () {

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


    });

</script>
--%>

        <%: Html.ValidationSummary(true) %>
        
            
		<%: Html.HiddenFor(model => model.Action) %>
		<%: Html.HiddenFor(model => model.FormAction)%>
		<%: Html.HiddenFor(model => model.Type)%>
        <%: Html.HiddenFor(model => model.TreeNodeId)%>
		<%: Html.HiddenFor(model => model.PageId)%>
		<%: Html.HiddenFor(model => model.ParentTreeNodeId)%>
        <%: Html.ValidationMessageFor(model => model.TreeNodeId) %>

<% if ((Model.Action ?? "Index") == "Index") { %>

    <div class="editor-label">
        <%: Html.CheckBoxFor(model => model.Inactive) %>
		<%: Html.LabelFor(model => model.Inactive) %>
    </div>
    <div class="editor-field">
        <%: Html.ValidationMessageFor(model => model.Inactive)%>
    </div>

    <div class="editor-label">
        <%: Html.CheckBoxFor(model => model.Hidden) %>
		<%: Html.LabelFor(model => model.Hidden) %>
    </div>
    <div class="editor-field">
        <%: Html.ValidationMessageFor(model => model.Hidden) %>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.Name) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.Name) %>
        <%: Html.ValidationMessageFor(model => model.Name) %>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.UrlSegment) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.UrlSegment) %>
        <%: Html.ValidationMessageFor(model => model.UrlSegment) %>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.Sequence) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.Sequence) %>
        <%: Html.ValidationMessageFor(model => model.Sequence) %>
    </div>

<% } %>
            
    <div class="editor-label">
        <%: Html.LabelFor(model => model.HeaderText) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.HeaderText) %>
        <%: Html.ValidationMessageFor(model => model.HeaderText) %>
    </div>

	<div class="editor-label">
        <%: Html.LabelFor(model => model.HeaderImage) %><% if (!string.IsNullOrEmpty(Model.HeaderImage)) { %>: <i><%=Model.HeaderImage %></i>
		&nbsp;<%: Html.CheckBoxFor(model => model.RemoveHeaderImage)%> <%: Html.LabelFor(model => model.RemoveHeaderImage) %>
		<% } %>
		
    </div>
    <div class="editor-field">
		<input type="file" name="ContentTreeNodeInputModel_HeaderImage" id="ContentTreeNodeInputModel_HeaderImage" />
        <%: Html.ValidationMessageFor(model => model.HeaderImage)%>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.Body) %>
    </div>
	<div class="editor-field">
        <%: Html.TextAreaFor(model => model.Body, new { @class = "tinymce" })%>
        <%--<%: Html.EditorFor(model => model.Body) %>--%>
        <%: Html.ValidationMessageFor(model => model.Body)%>
    </div>