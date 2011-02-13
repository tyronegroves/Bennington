using System.Collections.Generic;
using Paragon.ContentTree.ToolLinkNodeProvider.Mappers;
using Paragon.ContentTree.ToolLinkNodeProvider.Models;
using Paragon.ContentTree.ToolLinkNodeProvider.Repositories;

namespace Paragon.ContentTree.ToolLinkNodeProvider.Contexts
{
	public interface IToolLinkContext
	{
		IEnumerable<ToolLink> GetAllToolLinks();
	}

	public class ToolLinkContext : IToolLinkContext
	{
		private readonly IToolLinkProviderDraftToToolLinkMapper toolLinkProviderDraftToToolLinkMapper;
		private readonly IToolLinkProviderDraftRepository toolLinkProviderDraftRepository;

		public ToolLinkContext(IToolLinkProviderDraftToToolLinkMapper toolLinkProviderDraftToToolLinkMapper,
								IToolLinkProviderDraftRepository toolLinkProviderDraftRepository)
		{
			this.toolLinkProviderDraftRepository = toolLinkProviderDraftRepository;
			this.toolLinkProviderDraftToToolLinkMapper = toolLinkProviderDraftToToolLinkMapper;
		}

		public IEnumerable<ToolLink> GetAllToolLinks()
		{
			return toolLinkProviderDraftToToolLinkMapper.CreateSet(toolLinkProviderDraftRepository.GetAll());
		}
	}
}