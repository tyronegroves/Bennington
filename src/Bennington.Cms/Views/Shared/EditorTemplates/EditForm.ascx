<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Cms.Models.EditForm>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>
<%@ Import Namespace="Bennington.Cms.Metadata" %>

<%:Html.ValidationSummaryForForm()%>

<%using (Html.BeginForm()){ %>
<table>
<%
    foreach (var property in Model.GetType().GetProperties())
    {
        if (property.GetCustomAttributes(false).Any(x=>x.GetType() == typeof(HiddenAttribute)))
        {
%><%:Html.Editor(property.Name)%><%
        }else if (property.GetCustomAttributes(false).Any(x=>x.GetType().BaseType == typeof(ConsoleAttribute))){
%><tr><td colspan="2"><%:Html.Editor(property.Name)%></td></tr>
   <%}else{
        %>
        <tr>
        <td><%:Html.Label(property.Name) %></td>
        <td><%:Html.Editor(property.Name)%></td></tr><%
        }
    }
    %>
</table>
    <%
    var buttons = ViewData.ModelMetadata.AdditionalValues["ActionButtons"];
    Html.RenderPartial("DisplayForObject", buttons); %>
<%
} %>