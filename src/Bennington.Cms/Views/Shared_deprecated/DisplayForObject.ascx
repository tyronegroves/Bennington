<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<object>" %>
<%
    if (ViewData.ModelMetadata.AdditionalValues.ContainsKey("DoNotShowThisProperty"))
        return;%>
<%:Html.DisplayForModel() %>

