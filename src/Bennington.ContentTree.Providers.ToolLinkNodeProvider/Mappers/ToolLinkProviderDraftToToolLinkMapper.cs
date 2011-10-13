using System.Collections.Generic;
using AutoMapperAssist;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Data;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Mappers
{
	public interface IToolLinkProviderDraftToToolLinkMapper
	{
		IEnumerable<ToolLink> CreateSet(IEnumerable<ToolLinkProviderDraft> source);
	}

	public class ToolLinkProviderDraftToToolLinkMapper : Mapper<ToolLinkProviderDraft, ToolLink>, IToolLinkProviderDraftToToolLinkMapper
	{
        public override void DefineMap(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ToolLinkProviderDraft, ToolLink>()
                    .ForMember(a => a.IconUrl, b => b.UseValue("Content/ToolLinkProviderNode/ToolLinkProviderNode.gif"))
                    .ForMember(a => a.TreeNodeId, b => b.MapFrom(c => c.Id))
                ;
        }
	}
}