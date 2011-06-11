<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Bennington.Cms.Sorting" %>

<%
    object model = Model;
    var metadataForTheGenericType = ModelMetadataProviders.Current.GetMetadataForType(() => null, model.GetType().GetProperties().Single(x => x.Name == "Items").PropertyType.GetGenericArguments()[0]);
    var additionalValuesForTheGenericType = new RouteValueDictionary(metadataForTheGenericType.AdditionalValues);
    var sectionHeader = additionalValuesForTheGenericType["SectionHeader"] as string;
    var gridHeader = additionalValuesForTheGenericType["GridHeader"] as string;

    var topRightButtons = ViewData.ModelMetadata.AdditionalValues["TopRightButtons"] as IEnumerable<Bennington.Cms.Buttons.Button>;
    if (topRightButtons == null) topRightButtons = new Bennington.Cms.Buttons.Button[] { };

    var bottomLeftButtons = ViewData.ModelMetadata.AdditionalValues["BottomLeftButtons"] as IEnumerable<Bennington.Cms.Buttons.Button>;
    if (bottomLeftButtons == null) bottomLeftButtons = new Bennington.Cms.Buttons.Button[] { };
    
    
    
        %>

<div id="content_container" style="display: block; ">
   <div id="pageheader" class="clearfix">
      <h1><%:sectionHeader %></h1>
   </div>
   <div class="section">
      <ul class="tabs">
         <li><%:gridHeader %> 
            <div style="float:right">
                <% Html.RenderPartial("DisplayForObject", topRightButtons); %>
             </div>
          </li>
            
      </ul>
    </div>
    <div id="tab1" class="tabContent">
        <div class="section">
            <div class="highlight">
                <div class="content">
                    <table cellpadding="0" cellspacing="0" class="data_table" id="data_table">
                <thead>
                <tr>
                    <%
                        PaginationState paginationState = Model.PaginationState;
                        foreach(var property in metadataForTheGenericType.Properties)
                        {
                            var columnHeader = property.DisplayName ?? property.PropertyName;
                            %>
                            <%if (property.PropertyName == paginationState.SortBy)
                              {%>
                              <th class="headerSortUp"><%:columnHeader%></th>
                            <%
                              }
                              else
{%>
                            <th><%:columnHeader%></th>
                            <%
}
                        }
                    %>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                <%foreach(var item in Model.PagedItems)
                  {
                      Html.RenderPartial("SingleRowForListPage", item as object);
                  }%>
                </tbody>
                </table>
                </div>
            </div>
         </div>
         <div class="section">
             <div class="content actions">
                <%--<input type="button" class="button" onclick="getFile('core/process.php?v=add_locations');location.replace('#v/add_locations');" value="Add A New Location">--%>
                <% Html.RenderPartial("DisplayForObject", bottomLeftButtons); %>
             </div>
         </div>
    </div>
</div>