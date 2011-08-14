<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<object>" %>
<%@ Import Namespace="Bennington.Cms.Metadata" %>
<%

    
    var modelProperties = new RouteValueDictionary(Model);
    var buttons = ViewData.ModelMetadata.AdditionalValues.ContainsKey("IndividualRowButtons")
                      ? ViewData.ModelMetadata.AdditionalValues["IndividualRowButtons"] as IEnumerable<Bennington.Cms.Buttons.Button>
                      : null;
    if (buttons == null) buttons = new Bennington.Cms.Buttons.Button[] {};
%>

 <tr>
 <%
    var columnIndex = 0;
    foreach (var key in modelProperties.Keys)
    {
        if (Model.GetType().GetProperties()
            .Single(x => x.Name == key)
            .GetCustomAttributes(false)
            .Any(x => x.GetType() == typeof(DoNotShowThisPropertyAttribute)))
        {
            continue;
        }
        %><td class="listpagerow_<%=columnIndex%>">
<%
        if (modelProperties[key] != null)
            Html.RenderPartial("DisplayForObject", modelProperties[key]);%></td>
   
 <%
        columnIndex++;
    }%>
       <td class="actions">
          <%
    Html.RenderPartial("DisplayForObject", buttons);%>
        </td>
   </tr>

