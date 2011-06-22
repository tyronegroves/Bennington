<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<System.String>>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>
<%
    var items = ViewData.ModelMetadata.AdditionalValues["SelectList"] as IEnumerable<SelectListItem>;
    var propertyName = Html.GetCurrentPropertyName();

    var currentlySelectedOptions = Model ?? new string[] {};
    string currentSelectedOptionsFlatString = String.Join(",", currentlySelectedOptions);
%>
<table class="console">
<tr>
<td>
<select size="6" multiple="multiple" class="exclude" id="<%=propertyName.Replace(".", "_")%>_Exclude" name="<%=propertyName%>_Exclude" <%=(ViewData.ModelState.ContainsKey(propertyName) && ViewData.ModelState[propertyName].Errors.Any()) ? "class=\"input-validation-error\"" : string.Empty%>>
    <%
foreach (var item in items.Where(x=>currentlySelectedOptions.Contains(x.Value) == false))
{%>
    <option value="<%=HttpUtility.HtmlEncode(item.Value)%>">
        <%=Html.Encode(item.Text)%></option>
    <%
}%>
</select>
</td>
<td>
<button type="button" class="addbutton" onclick="return false;">&gt;</button><br /><br /><button type="button" class="removebutton" onclick="return false;">&lt;</button>
</td>
<td>
<select size="6" multiple="multiple" class="include" id="<%=propertyName.Replace(".", "_")%>" name="<%=propertyName%>" <%=(ViewData.ModelState.ContainsKey(propertyName) && ViewData.ModelState[propertyName].Errors.Any()) ? "class=\"input-validation-error\"" : string.Empty%>>
    <%
foreach (var item in items.Where(x=>currentlySelectedOptions.Contains(x.Value)))
{%>
    <option value="<%=HttpUtility.HtmlEncode(item.Value)%>">
        <%=Html.Encode(item.Text)%></option>
    <%
}%>
</select>
</td>
</tr>
</table>