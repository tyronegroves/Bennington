<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<dynamic>>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>
<%
    var items = ViewData.ModelMetadata.AdditionalValues["SelectList"] as IEnumerable<SelectListItem>;
    var propertyName = Html.GetCurrentPropertyName();

%>
        <%
    foreach (var option in items)
    {%>
        <input type="checkbox" name="<%:propertyName %>" value="<%:option.Value%>" <%: Model != null && Model.Any(x=>x == option.Value) ? "checked" : "" %>>
            <%:option.Text%>        <%
    }%>

