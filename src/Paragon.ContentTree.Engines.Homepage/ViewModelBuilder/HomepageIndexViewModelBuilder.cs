using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Engines.Homepage.Controllers;
using Paragon.ContentTree.Engines.Homepage.Models;
using Paragon.ContentTree.Helpers;
using Paragon.ContentTree.Repositories;

namespace Paragon.ContentTree.Engines.Homepage.ViewModelBuilder
{
	public interface IHomepageIndexViewModelBuilder
	{
		HomepageIndexViewModel BuildViewModel();
	}

	public class HomepageIndexViewModelBuilder : IHomepageIndexViewModelBuilder
	{
		private readonly ITreeNodeRepository treeNodeRepository;
		private readonly IContentTreeNodeContext contentTreeNodeContext;

		public HomepageIndexViewModelBuilder(ITreeNodeRepository treeNodeRepository,
											IContentTreeNodeContext contentTreeNodeContext)
		{
			this.contentTreeNodeContext = contentTreeNodeContext;
			this.treeNodeRepository = treeNodeRepository;
		}

		public HomepageIndexViewModel BuildViewModel()
		{
			var allTreeNodes = treeNodeRepository.GetAll();
			var homepageTreeNode = allTreeNodes.Where(a => a.Type == typeof(HomepageController).AssemblyQualifiedName).FirstOrDefault();

			var treeNodeSummary = contentTreeNodeContext.GetContentTreeNodesByTreeId(homepageTreeNode.Id).FirstOrDefault();

			return new HomepageIndexViewModel()
			       	{
						Header = treeNodeSummary.HeaderText,
						Body = treeNodeSummary.Body,
			       	};
		}
	}
}
