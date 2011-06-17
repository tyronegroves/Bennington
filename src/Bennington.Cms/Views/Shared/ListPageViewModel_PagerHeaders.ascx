<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Bennington.Cms.Metadata" %>
<%@ Import Namespace="Bennington.Cms.Sorting" %>

<%
    object model = Model;
    var genericArgument = model.GetType().GetProperties().Single(x => x.Name == "Items").PropertyType.GetGenericArguments()[0];
    var metadataForTheGenericType = ModelMetadataProviders.Current.GetMetadataForType(() => null, genericArgument);
        %>
    <thead>
    <tr>
        <%
            PaginationState paginationState = Model.PaginationState;
            foreach(var property in metadataForTheGenericType.Properties)
            {

                if (genericArgument.GetProperties()
                    .Single(x => x.Name == property.PropertyName)
                    .GetCustomAttributes(false)
                    .Any(x => x.GetType() == typeof(DoNotShowThisPropertyAttribute)))
                    continue;
                
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
