<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="PagedList" %>
<%@ Import Namespace="PagedList.Mvc" %>
<%object model = Model;%>

<div id="content_container" style="display: block; ">
    <%
    Html.RenderPartial("ListPageViewModel_Header", model); %>
    <div id="tab1" class="tabContent">
        <div class="section">
            <div class="highlight">
                <div class="content">
                    <table cellpadding="0" cellspacing="0" class="data_table" id="data_table">
                    <%
                   Html.RenderPartial("ListPageViewModel_PagerHeaders", model);%>
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

             <%
    Html.RenderPartial("ListPageViewModel_Footer", model); %>
    
    </div>
</div>