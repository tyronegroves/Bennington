using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.ViewModelBuilders
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