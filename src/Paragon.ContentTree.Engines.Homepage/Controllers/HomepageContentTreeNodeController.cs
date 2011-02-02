using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.ViewModelBuilders;
using Paragon.ContentTree.ContentNodeProvider.ViewModelBuilders.Helpers;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Repositories;
using Paragon.Core.Helpers;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Engines.Homepage.Controllers
{
	public class HomepageContentTreeNodeController : ContentTreeNodeController
	{
		public HomepageContentTreeNodeController(IContentTreeNodeVersionContext contentTreeNodeVersionContext, IContentTreeNodeToContentTreeNodeInputModelMapper contentTreeNodeToContentTreeNodeInputModelMapper, IContentTreeNodeInputModelToContentTreeNodeMapper contentTreeNodeInputModelToContentTreeNodeMapper, IContentTreeNodeContext contentTreeNodeContext, ITreeNodeRepository treeNodeRepository, ITreeNodeProviderContext treeNodeProviderContext, IContentTreeNodeDisplayViewModelBuilder contentTreeNodeDisplayViewModelBuilder, IRawUrlGetter rawUrlGetter, ICommandBus commandBus, IGuidGetter guidGetter) : base(contentTreeNodeVersionContext, contentTreeNodeToContentTreeNodeInputModelMapper, contentTreeNodeInputModelToContentTreeNodeMapper, contentTreeNodeContext, treeNodeRepository, treeNodeProviderContext, contentTreeNodeDisplayViewModelBuilder, rawUrlGetter, commandBus, guidGetter)
		{
		}
	}
}
