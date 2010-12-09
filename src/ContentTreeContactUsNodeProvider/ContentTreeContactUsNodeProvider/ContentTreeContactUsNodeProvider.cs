using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ContentTreeContactUsNodeProvider.Controllers;
using ContentTreeContactUsNodeProvider.Repositories;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTree.TreeNodeExtensionProvider;
using Paragon.ContentTreeNodeProvider;
using Paragon.ContentTreeNodeProvider.Repositories;
using Paragon.Pages.Routing.Helpers;


namespace ContentTreeContactUsNodeProvider
{
	public class ContentTreeContactUsNodeProvider : ContentTreeNodeExtensionProvider, IAmATreeNodeExtensionProvider //, IRouteConstraint
	{
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;
		private readonly ITreeNodeRepository treeNodeRepository;

		public ContentTreeContactUsNodeProvider(IContentTreeNodeRepository contentTreeNodeRepository, ITreeNodeRepository treeNodeRepository)
			: base(contentTreeNodeRepository)
		{
			this.treeNodeRepository = treeNodeRepository;
			this.treeNodeIdToUrl = treeNodeIdToUrl;
		}

		public override string Name
		{
			get { return "Contact Us"; }
		}
		
		public override IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems
		{
			get
			{
				var contentTreeNodeContentItems = new List<ContentTreeNodeContentItem>();
				foreach (var method in typeof(ContactUsController).GetMethods())
				{
					if (method.DeclaringType == typeof(ContactUsController))
					{
						contentTreeNodeContentItems.Add(new ContentTreeNodeContentItem()
						{
							Id = method.Name,
							Name = method.Name,
						});

					}
				}
				return contentTreeNodeContentItems;
			}
			set { throw new NotImplementedException(); }
		}

		//public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		//{
		//    foreach (var treeNode in treeNodeRepository.GetAll().Where(a => a.Type == this.GetType().FullName))
		//    {
		//        if (httpContext.Request.RawUrl.StartsWith(treeNodeIdToUrl.GetUrlByTreeNodeId(treeNode.Id)))
		//            return true;
		//    }
		//    return false;
		//}

		//public virtual IRouteConstraint IgnoreConstraint
		//{
		//    get
		//    {
		//        return this;
		//    }
		//}
	}
}