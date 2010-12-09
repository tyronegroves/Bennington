<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
	    $(document).ready(function () {
			$('.contentTree').jstree({
				'plugins' : ['themes', 'html_data', 'ui', 'checkbox']
			});
		});
	</script>

    <div id="pageheader" class="clearfix">
                
        <h1>Content Tree</h1>

        <p class="meta">
            Last updated by Neal Sharma, 11/21/2009 5:22PM CST
        </p>

        <p class="search">
            <label>Search by:</label>
            <select>
                <option>Page Name</option>
            </select>
            <label>Search Keywords</label>
            <input type="text" />
            <input type="button" value="Submit" />
        </p>
    </div>

    <div class="section">
        <ul class="tabs">
            <li><a href="#tab1">Event Info</a></li>
            <li><a href="#tab2">Meta Data</a></li>
        </ul>
    </div>
    <div id="tab1" class="tabContent">
        <div class="section">
            <div class="highlight">
                <h2>Event Information</h2>
                <div class="content">
                    Page Title: <input type="text" />
                </div>
            </div>
        </div>

        <div class="section">
            <div class="contentTreeContainer clearfix">
                <div class="contentTree">
                    <ul>
                        <li><a href="#">Test 1</a></li>
                        <li><a href="#">Test 2</a>
                            <ul>
                                <li><a href="#">Sub 1</a></li>
                                <li><a href="#">Sub 2</a></li>
                                <li><a href="#">Sub 3</a></li>
                            </ul>
                        </li>
                        <li><a href="#">Test 3</a></li>
                        <li><a href="#">Test 4</a></li>
                    </ul>
                </div>
                <div class="contentTreePreview">
                    <h2>Title of selected item</h2>
                    <div class="content">                
                        <p>Last Updated: 04/12/09 - 4:23 PM CST</p>
                        <p>Last Update by: Kristopher Knowles</p>
                        <p>Version: 4</p>
                        <p>Active: <strong>Yes</strong></p>
                    </div>
                </div>
            </div>
        </div>

    
        <div class="section">
            <h2>Toolbar Setup</h2>
            <div class="content">
                <p>
                    Put content here.
                </p>        
            </div>
        </div>

        <div class="section">
            <h2>Author Information</h2>
            <div class="content">
                Version 3 last published by:<br />    
                Version 4 last published by:<br />
                Page Created by:
            </div>
        </div>

        <div class="section">
            <div class="content">
                <input type="button" class="button important" value="Publish" />
                <input type="button" class="button" value="Delete Selected" />
            </div>
        </div>
    </div>

    <div id="tab2" class="tabContent">
        <div class="section">
            <div class="content">
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi id lacinia est. Donec consequat tristique ullamcorper. Nunc et magna ut felis mollis porttitor. In hac habitasse platea dictumst. Duis et molestie elit. In hac habitasse platea dictumst. Sed dapibus, sem eget iaculis faucibus, mauris ante ultrices quam, id viverra nisl ipsum id mi. In et volutpat libero. Donec ultricies porttitor consectetur. Mauris non orci velit. Nulla facilisi. Mauris ullamcorper consectetur erat a posuere. Sed interdum neque eu mi lobortis posuere. Nunc adipiscing ipsum in velit aliquam id mollis urna consequat.</p>

            <p>Pellentesque gravida congue molestie. Mauris turpis orci, vehicula sit amet accumsan nec, pellentesque quis felis. Aenean volutpat rhoncus egestas. Donec sit amet est id urna luctus sodales eget ac ante. Nunc gravida, quam sed blandit commodo, risus lacus semper metus, et consequat metus felis a orci. Proin sit amet diam ut ligula venenatis convallis. Maecenas sodales tortor at nisl egestas semper. Sed dictum diam magna.</p>
            </div>
        </div>

    </div>
</asp:Content>
