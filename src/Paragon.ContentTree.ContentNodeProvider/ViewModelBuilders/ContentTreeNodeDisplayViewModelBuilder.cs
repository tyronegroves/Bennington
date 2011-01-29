using System.Linq;
using System.Web.Routing;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.ViewModelBuilders.Helpers;

namespace Paragon.ContentTree.ContentNodeProvider.ViewModelBuilders
{
	public interface IContentTreeNodeDisplayViewModelBuilder
	{
		ContentTreeNodeDisplayViewModel BuildViewModel(string rawUrl, RouteData routeData);
	}

	public class ContentTreeNodeDisplayViewModelBuilder : IContentTreeNodeDisplayViewModelBuilder
	{
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;
		private readonly IContentTreeNodeContext contentTreeNodeContext;
		private readonly IGetParentRouteDataDictionaryFromChildActionRouteData getParentRouteDataDictionaryFromChildActionRouteData;

		public ContentTreeNodeDisplayViewModelBuilder(ITreeNodeSummaryContext treeNodeSummaryContext, IContentTreeNodeContext contentTreeNodeContext, IGetParentRouteDataDictionaryFromChildActionRouteData getParentRouteDataDictionaryFromChildActionRouteData)
		{
			this.getParentRouteDataDictionaryFromChildActionRouteData = getParentRouteDataDictionaryFromChildActionRouteData;
			this.contentTreeNodeContext = contentTreeNodeContext;
			this.treeNodeSummaryContext = treeNodeSummaryContext;
		}

		public ContentTreeNodeDisplayViewModel BuildViewModel(string rawUrl, RouteData routeData)
		{
			var nodeSegments = ScrubUrlAndReturnEnumerableOfNodeSegments(rawUrl);

			string workingTreeNodeId = GetTreeNodeIdFromTreeNodeSummaryContextUsingNodeSegments(nodeSegments);

			var viewModel = new ContentTreeNodeDisplayViewModel()
			       	{
			       		Body = string.Empty,
						Header = string.Empty,
			       	};
			if (string.IsNullOrEmpty(workingTreeNodeId)) return viewModel;

			var data = getParentRouteDataDictionaryFromChildActionRouteData.GetRouteValues(routeData);
			var action = GetAction(data);

			var contentTreeNodes = contentTreeNodeContext.GetContentTreeNodesByTreeId(workingTreeNodeId).Where(a => a.Action == action);
			if (contentTreeNodes.Count() == 0) return viewModel;

			return (from item in contentTreeNodes
			       select new ContentTreeNodeDisplayViewModel
			              	{
								Body = item.Body,
								Header = item.HeaderText,
			              	}).FirstOrDefault();
		}

		private string GetTreeNodeIdFromTreeNodeSummaryContextUsingNodeSegments(string[] nodeSegments)
		{
			var workingTreeNodeId = ContentTreeNodeContext.RootNodeId;
			foreach (var nodeSegment in nodeSegments)
			{
				var childNodes = treeNodeSummaryContext.GetChildren(workingTreeNodeId);
				var treeNodeSummary = childNodes.Where(a => a.UrlSegment.ToUpper() == nodeSegment.ToUpper()).FirstOrDefault();
				if (treeNodeSummary == null) break;
				workingTreeNodeId = treeNodeSummary.Id;
			}
			return workingTreeNodeId;
		}

		private string[] ScrubUrlAndReturnEnumerableOfNodeSegments(string rawUrl)
		{
			if (rawUrl == null) rawUrl = string.Empty;
			if ((rawUrl.StartsWith("/")) && (rawUrl.Length > 1)) rawUrl = rawUrl.Substring(1, rawUrl.Length - 1);
			if (rawUrl.Contains("?"))
				rawUrl = rawUrl.Split('?')[0];
			return rawUrl.Split('/');
		}

		private string GetAction(RouteData routeData)
		{
			var action = "Index";
			if (routeData == null) return action;
			if (routeData.Values == null) return action;

			var actionValue = routeData.Values["Action"];
			if (actionValue!= null)
				action = routeData.Values["Action"].ToString();
			return action;
		}
	}
}
