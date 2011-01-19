using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapperAssist;

namespace Paragon.ContentTree.ContentNodeProvider.Mappers
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
					.ForMember(a => a.Content, opt => opt.MapFrom(c => c.Body))
					.ForMember(a => a.ContentItemId, opt => opt.MapFrom(c => c.Action))
				;
		}
	}
}
