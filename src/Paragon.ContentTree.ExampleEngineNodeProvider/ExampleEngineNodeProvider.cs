using System;
using System.Collections.Generic;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.ExampleEngineNodeProvider.Controllers;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTree.Routing.Routing.Helpers;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.ExampleEngineNodeProvider
{
	public class ExampleEngineNodeProvider : ContentNodeProvider.ContentNodeProvider, IAmATreeNodeExtensionProvider //, IRouteConstraint
	{
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;
		private readonly ITreeNodeRepository treeNodeRepository;

		public ExampleEngineNodeProvider(IContentTreeNodeRepository contentTreeNodeRepository, ITreeNodeRepository treeNodeRepository)
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