<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.String>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>
<%
    var items = ViewData.ModelMetadata.AdditionalValues["SelectList"] as IEnumerable<SelectListItem>;
    var propertyName = Html.GetCurrentPropertyName();
%>
<select id="<%=propertyName.Replace(".", "_")%>" name="<%=propertyName%>" <%=(ViewData.ModelState.ContainsKey(propertyName) && ViewData.ModelState[propertyName].Errors.Any()) ? "class=\"input-validation-error\"" : string.Empty%>>
    <%
foreach (var item in items)
{%>
    <option value="<%=HttpUtility.HtmlEncode(item.Value)%>" <%=item.Value == (ViewData.Model == null ? null : ViewData.Model) ? " selected" : ""%>>
        <%=Html.Encode(item.Text)%></option>
    <%
}%>
</select>


