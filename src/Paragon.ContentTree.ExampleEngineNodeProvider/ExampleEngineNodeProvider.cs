using System;
using System.Collections.Generic;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.ExampleEngineNodeProvider.Controllers;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTree.Routing.Routing.Helpers;

namespace Paragon.ContentTree.ExampleEngineNodeProvider
{
	public class ExampleEngineNodeProvider : ContentNodeProvider.ContentNodeProvider
	{
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;

		public ExampleEngineNodeProvider(IContentTreeNodeRepository contentTreeNodeRepository, ITreeNodeRepository treeNodeRepository)
			: base(contentTreeNodeRepository)
		{
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
	}
}