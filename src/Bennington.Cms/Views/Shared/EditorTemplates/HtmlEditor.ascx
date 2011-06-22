<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.String>" %>
<%:Html.TextAreaFor(x => x, 20, 20, new { @class = "tinymce" })%>
