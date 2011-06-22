<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>
<%
    var items = ViewData.ModelMetadata.AdditionalValues["SelectList"] as IEnumerable<SelectListItem>;
    var propertyName = Html.GetCurrentPropertyName();
%>
<%
    var index = 0;
    foreach (var item in items)
    {%>
<input id="<%=propertyName%>_<%=index%>" name="<%=propertyName%>" type="radio" value="<%=item.Value%>" <%:(Model ?? "").ToString() == item.Value ? "checked" : ""%> /><span class="radio-text"><%=item.Text%></span><%
        index += 1;
    }%>
