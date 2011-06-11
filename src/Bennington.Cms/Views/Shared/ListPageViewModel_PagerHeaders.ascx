<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Bennington.Cms.Sorting" %>

<%
    object model = Model;
    var metadataForTheGenericType = ModelMetadataProviders.Current.GetMetadataForType(() => null, model.GetType().GetProperties().Single(x => x.Name == "Items").PropertyType.GetGenericArguments()[0]);
        %>
    <thead>
    <tr>
        <%
            PaginationState paginationState = Model.PaginationState;
            foreach(var property in metadataForTheGenericType.Properties)
            {
                var columnHeader = property.DisplayName ?? property.PropertyName;
                var sortUrl = "?sortBy=" + property.PropertyName;
                %>
                <%if (property.PropertyName == paginationState.SortBy)
                    {%>
                    <th class="<%:paginationState.SortOrder == "asc" ? "headerSortUp" : "headerSortDown" %>"><a href="<%:sortUrl%>&sortOrder=<%:paginationState.SortOrder == "asc" ? "desc" : "asc" %>"><%:columnHeader%></a></th>
                <%
                    }
                    else
{%>
                <th><a href="<%:sortUrl%>"><%:columnHeader%></a></th>
                <%
}
            }
        %>
        <th></th>
    </tr>
    </thead>
