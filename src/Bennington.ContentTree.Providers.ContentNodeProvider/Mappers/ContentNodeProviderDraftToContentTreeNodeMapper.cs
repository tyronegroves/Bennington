using System.Collections.Generic;
using AutoMapperAssist;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Mappers
{
	public interface IContentNodeProviderDraftToContentTreeNodeMapper
	{
		IEnumerable<Models.ContentTreeNode> CreateSet(IEnumerable<Data.ContentNodeProviderDraft> source);
	}

	public class ContentNodeProviderDraftToContentTreeNodeMapper : Mapper<Data.ContentNodeProviderDraft, Models.ContentTreeNode>, IContentNodeProviderDraftToContentTreeNodeMapper
	{
		public override void DefineMap(AutoMapper.IConfiguration configuration)
		{
			configuration.CreateMap<Data.ContentNodeProviderDraft, Models.ContentTreeNode>()
					.ForMember(a => a.Body, opt => opt.MapFrom(c => c.Body))
                    .ForMember(a => a.IconUrl, b => b.Ignore())
					.ForMember(a => a.Action, opt => opt.MapFrom(c => c.Action))
				;
		}
	}
}
