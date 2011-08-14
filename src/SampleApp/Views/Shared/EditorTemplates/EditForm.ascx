<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Cms.Models.EditForm>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>
<%@ Import Namespace="Bennington.Cms.Metadata" %>

<%:Html.ValidationSummaryForForm()%>

<%
    var form = HttpContext.Current.Items["TheCurrentForm"] as MvcForm;
    if (form != null)
    {
        %>

        <table>
<%
    foreach (var property in Model.GetType().GetProperties())
    {
        if (property.GetCustomAttributes(false).Any(x=>x.GetType() == typeof(HiddenAttribute)))
        {
%><%:Html.Editor(property.Name)%><%
        }else if (property.GetCustomAttributes(false).Any(x=>x.GetType().BaseType == typeof(ConsoleAttribute)))
        {
%><tr><td colspan="2"><%:Html.Editor(property.Name)%></td></tr>
<%
        }else if (property.GetCustomAttributes(false).Any(x=>x.GetType() == typeof(TextareaAttribute) || x.GetType().BaseType == typeof(TextareaAttribute))){%>
        <tr><td colspan="2"><%:Html.Label(property.Name) %><br />
        <%:Html.Editor(property.Name) %></td></tr>
   <%}else{
        %>
        <tr>
        <td><%:Html.Label(property.Name) %></td>
        <td><%:Html.Editor(property.Name)%></td></tr><%
        }
    }
    %>
</table>
<%return; %>

    <%}
    
    %>

<%using (var the_main_form = Html.BeginForm())
  {
      HttpContext.Current.Items["TheCurrentForm"] = the_main_form;
%>
<table>
<%
    foreach (var property in Model.GetType().GetProperties())
    {
        if (property.GetCustomAttributes(false).Any(x=>x.GetType() == typeof(HiddenAttribute)))
        {
%><%:Html.Editor(property.Name)%><%
        }else if (property.GetCustomAttributes(false).Any(x=>x.GetType().BaseType == typeof(ConsoleAttribute)))
        {
%><tr><td colspan="2"><%:Html.Editor(property.Name)%></td></tr>
<%
        }else if (property.GetCustomAttributes(false).Any(x=>x.GetType() == typeof(TextareaAttribute) || x.GetType().BaseType == typeof(TextareaAttribute)))
        {%>
        <tr><td colspan="2"><%:Html.Label(property.Name)%><br />
        <%:Html.Editor(property.Name)%></td></tr>
        <%
        }else if (property.GetCustomAttributes(false).Any(x=>x.GetType() == typeof(DoNotShowALabel))){%>
        <tr><td colspan="2"><%:Html.Editor(property.Name) %></td></tr>
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
    Html.RenderPartial("ButtonsForEditForm", Model); %>
<%
      HttpContext.Current.Items["TheCurrentForm"] = null;
  } %>