<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ManageSite.Master" Inherits="System.Web.Mvc.ViewPage<Bennington.Cms.PrincipalProvider.Models.ModifyViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div style="padding-top:50px;">
    
	<% using (Html.BeginForm()) {%>

        <%= Html.ValidationSummary(true) %>
        
			<%=Html.EditorFor(a => a.UserInputModel) %>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        
    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</div>
</asp:Content>

