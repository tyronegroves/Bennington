<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Cms.Models.EditForm>" %>

<%using (Html.BeginForm()){ %>

<%
    foreach (var property in Model.GetType().GetProperties())
    {
        %><%:Html.Editor(property.Name)%><%
    }
    %>
    <input type="submit" value="submit" />
<%} %>