<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>
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

        %>
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
    <div class="section">
         <%if (Model.PagedItems.TotalItemCount > Model.PagedItems.PageSize)
           {%>
             <div class="content pagination" style="float:left">
                 <%:Html.PagedListPager(Model.PagedItems as IPagedList,
                                                     page => Url.Action("Index", new {page}),
                                                     new PagedListRenderOptions
                                                         {
                                                             DisplayLinkToLastPage = false,
                                                             DisplayLinkToFirstPage = false,
                                                             DisplayLinkToNextPage = false,
                                                             DisplayLinkToPreviousPage = false,
                                                             DisplayLinkToIndividualPages = true,
                                                             FunctionToDisplayAPageNumber = (p) => ((IPagedList)Model.PagedItems).GetPageRange(p).ToString()
                                                         })%>
             </div>

             <div class="content pagination">
                 <%:Html.PagedListPager(Model.PagedItems as IPagedList,
                                                     page => Url.Action("Index", new {page}),
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
