﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Login.Models.LoginForm>" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>

<div id="canvas">
	<div id="login_form">
			<div class="login_message">Please Enter Your Username/Password</div>
	<fieldset>
        <%:Html.ValidationSummaryForForm() %>
		<%=Html.EditorFor(x => x.Username)%>
        <%=Html.EditorFor(x => x.Password)%>
		<input type="submit" value="Sign In" class="button"/>
	</fieldset>
</div>