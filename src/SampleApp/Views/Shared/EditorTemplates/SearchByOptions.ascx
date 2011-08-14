<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Cms.Models.SearchByOptions>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>
<%@ Import Namespace="Bennington.Cms.Metadata" %>
<%using (Html.BeginForm()){ %>
    <table class="searchForm">
    <tr>
        <td>
            <%:Html.Label("Search By:") %>
        </td>
        <td>
                <select id="searchBy" name="searchBy">
                   <option value="">- select -</option>
                    <%
                        var currentSearchBy = HttpContext.Current.Request.QueryString["searchBy"];
                        var currentSearchValue = HttpContext.Current.Request.QueryString["searchValue"];
                foreach (var item in Model.Options)
                {%>
                    <option value="<%=HttpUtility.HtmlEncode(item.Key)%>" <%:(item.Key == currentSearchBy) ? "selected" : "" %>>
                        <%=Html.Encode(item.Value)%></option>
                    <%
                }%>
                </select>
        </td>
                <td>
            <%:Html.Label("Search:") %>
        </td>
        <td>
            <input name="searchValue" name="searchValue" class="textfield" value="<%:currentSearchValue %>" />
        </td>
        <td>
            <a class="search_by_button">
            <img src="/MANAGE/Content/datatable/select.jpg"/>
            </a>
        </td>        
    </tr>
    </table>
<%} %>