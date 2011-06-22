<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<System.String>>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>
<%
    var items = ViewData.ModelMetadata.AdditionalValues["SelectList"] as IEnumerable<SelectListItem>;
    var propertyName = Html.GetCurrentPropertyName();

    var currentlySelectedOptions = Model ?? new string[] {};
    string currentSelectedOptionsFlatString = String.Join(",", currentlySelectedOptions);
%>
<input type="hidden" name="<%:propertyName %>" id="<%:propertyName%>" value="<%:currentSelectedOptionsFlatString %>" />
<select size="6" multiple="multiple" id="<%=propertyName.Replace(".", "_")%>_Exclude" name="<%=propertyName%>_Exclude" <%=(ViewData.ModelState.ContainsKey(propertyName) && ViewData.ModelState[propertyName].Errors.Any()) ? "class=\"input-validation-error\"" : string.Empty%>>
    <%
foreach (var item in items.Where(x=>currentlySelectedOptions.Contains(x.Value) == false))
{%>
    <option value="<%=HttpUtility.HtmlEncode(item.Value)%>">
        <%=Html.Encode(item.Text)%></option>
    <%
}%>
</select>

<select size="6" multiple="multiple" id="<%=propertyName.Replace(".", "_")%>_Include" name="<%=propertyName%>_Include" <%=(ViewData.ModelState.ContainsKey(propertyName) && ViewData.ModelState[propertyName].Errors.Any()) ? "class=\"input-validation-error\"" : string.Empty%>>
    <%
foreach (var item in items.Where(x=>currentlySelectedOptions.Contains(x.Value)))
{%>
    <option value="<%=HttpUtility.HtmlEncode(item.Value)%>">
        <%=Html.Encode(item.Text)%></option>
    <%
}%>
</select>


