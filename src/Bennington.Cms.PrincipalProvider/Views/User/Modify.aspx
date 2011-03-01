<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ManageSite.Master" Inherits="System.Web.Mvc.ViewPage<Bennington.Cms.PrincipalProvider.Models.ModifyViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div style="padding-top:50px;">
    
	<% using (Html.BeginForm()) {%>

        <%= Html.ValidationSummary(true) %>
        
			<%=Html.EditorFor(a => a.UserInputModel) %>
            
            <p>
                <input type="submit" value="Save" class="button" />
				<% if (ViewContext.RouteData.Values["Action"].ToString() == "Modify") { %>
				<input type="button" class="button important" value="Delete" onclick="if (confirm('Are you sure you want to delete this item?')) { window.location='<%=Url.Action("Delete", "User", new { id = Model.UserInputModel.Id }) %>'; }" />
				<% } %>
            </p>
        
    <% } %>

</div>
</asp:Content>

