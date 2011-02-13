using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.ContentTree.ToolLinkNodeProvider.Models;

namespace Paragon.ContentTree.ToolLinkNodeProvider.ViewModelBuilders
{
	public interface IModifyViewModelBuilder
	{
		ModifyViewModel BuildViewModel(ToolLinkInputModel toolLinkInputModel);
	}

	public class ModifyViewModelBuilder : IModifyViewModelBuilder
	{
		public ModifyViewModel BuildViewModel(ToolLinkInputModel toolLinkInputModel)
		{
			return new ModifyViewModel()
			       	{
			       		ToolLinkInputModel = toolLinkInputModel,
			       	};
		}
	}
}