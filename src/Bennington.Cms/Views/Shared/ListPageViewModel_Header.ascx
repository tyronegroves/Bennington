<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>
<%@ Import Namespace="Bennington.Cms.Sorting" %>
<%@ Import Namespace="PagedList" %>
<%@ Import Namespace="PagedList.Mvc" %>

<%
    object model = Model;
    var metadataForTheGenericType = ModelMetadataProviders.Current.GetMetadataForType(() => null, model.GetType().GetProperties().Single(x => x.Name == "Items").PropertyType.GetGenericArguments()[0]);
    var additionalValuesForTheGenericType = new RouteValueDictionary(metadataForTheGenericType.AdditionalValues);
    var sectionHeader = additionalValuesForTheGenericType["SectionHeader"] as string;
    var gridHeader = additionalValuesForTheGenericType["GridHeader"] as string;

    var topRightButtons = ViewData.ModelMetadata.AdditionalValues["TopRightButtons"] as IEnumerable<Bennington.Cms.Buttons.Button>;
    if (topRightButtons == null) topRightButtons = new Bennington.Cms.Buttons.Button[] { };

    PaginationState paginationState = Model.PaginationState;
    
        %>
   <div id="pageheader" class="clearfix">
   <div style="float:right"><%
   object @object = Model.SearchByOptions;
   Html.RenderPartial("EditorForObject", @object); %></div>
      <div><h2><%:sectionHeader %></h2></div>
      <%if (string.IsNullOrEmpty(gridHeader) == false)
        {%>
      <div><%:gridHeader%> </div>
      <%
        }%>
   </div>
   <%if (topRightButtons != null && topRightButtons.Any()){ %>
   <div class="section">
      <ul class="tabs">
         <li>
            <div style="float:left">
                <% Html.RenderPartial("DisplayForObject", topRightButtons); %>
             </div>
          </li>
            
      </ul>
    </div>
    <%} %>
    <div class="section extra_padding">
        <%if (paginationState.ViewingAll == true)
          {%>
           <div class="PagedList-pager"><ul><li class="PagedList-skipToPage"><a href="?page=0">View by Page</a></li></ul></div>
         <%
          }else if (Model.PagedItems.TotalItemCount > Model.PagedItems.PageSize)
           {%>
             <div class="PagedList-pager" style="float:left"><ul><li class="PagedList-skipToPage"><a href="?page=-1">View All</a></li><li> | </li></ul></div>
             <div class="content pagination" style="float:left">
                 <%:Html.PagedListPager(Model.PagedItems as IPagedList,
                                                     page => "?page=" + page,
                                                     new PagedListRenderOptions
                                                         {
                                                             DisplayLinkToLastPage = false,
                                                             DisplayLinkToFirstPage = false,
                                                             DisplayLinkToNextPage = false,
                                                             DisplayLinkToPreviousPage = false,
                                                             DisplayLinkToIndividualPages = true,
                                                             Delimiter = " | ",
                                                             FunctionToDisplayEachPageNumber = (p) =>
                                                                                                   {
                                                                                                       return ((IPagedList) Model.PagedItems)
                                                                                                           .GetPageRange(p-1)
                                                                                                           .ToString();
                                                                                                   }
                                                         })%>
             </div>

             <div class="content pagination">
                 <%:Html.PagedListPager(Model.PagedItems as IPagedList,
                                                     page => "?page=" + page,
                                                     new PagedListRenderOptions
                                                         {
                                                             DisplayLinkToLastPage = false,
                                                             DisplayLinkToFirstPage = false,
                                                             DisplayLinkToIndividualPages = false,
                                                         })%>
             </div>
             <%
           }%>
    </div>
