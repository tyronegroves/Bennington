<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Paragon.ContentTree.ContentNodeProvider.Models.ContentTreeNodeInputModel>" %>

<hr />

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
        <%: Html.LabelFor(model => model.Body) %>
    </div>
    <div class="editor-field">
        <%: Html.TextAreaFor(model => model.Body)%>
        <%: Html.ValidationMessageFor(model => model.Body)%>
    </div>

