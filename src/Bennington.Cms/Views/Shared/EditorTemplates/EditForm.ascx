﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Cms.Models.EditForm>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>

<%:Html.ValidationSummaryForForm()%>

<%using (Html.BeginForm()){ %>
<table>
<%
    foreach (var property in Model.GetType().GetProperties())
    {
        %><tr>
        <td><%:Html.Label(property.Name) %></td>
        <td><%:Html.Editor(property.Name)%></td></tr><%
    }
    %>
</table>
    <input type="submit" value="submit" />
<%} %>