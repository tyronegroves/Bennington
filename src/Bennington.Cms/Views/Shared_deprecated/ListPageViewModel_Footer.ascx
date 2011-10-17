<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="PagedList" %>
<%@ Import Namespace="PagedList.Mvc" %>

<%
    var model = Model;

    var bottomLeftButtons = ViewData.ModelMetadata.AdditionalValues["BottomLeftButtons"] as IEnumerable<Bennington.Cms.Buttons.Button>;
    if (bottomLeftButtons == null) bottomLeftButtons = new Bennington.Cms.Buttons.Button[] {};
%>

         <div class="section extra_padding">

         <%if (Model.PagedItems.TotalItemCount > Model.PagedItems.PageSize)
           {%>
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

             <div class="content actions">
                <%--<input type="button" class="button" onclick="getFile('core/process.php?v=add_locations');location.replace('#v/add_locations');" value="Add A New Location">--%>
                <%
    Html.RenderPartial("DisplayForObject", bottomLeftButtons);%>
             </div>

         </div>
