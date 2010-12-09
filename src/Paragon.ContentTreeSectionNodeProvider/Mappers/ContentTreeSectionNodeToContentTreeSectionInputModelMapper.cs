using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapperAssist;
using Paragon.ContentTreeSectionNodeProvider.Data;
using Paragon.ContentTreeSectionNodeProvider.Models;

namespace Paragon.ContentTreeSectionNodeProvider.Mappers
{
	public interface IContentTreeSectionNodeToContentTreeSectionInputModelMapper
	{
		ContentTreeSectionInputModel CreateInstance(ContentTreeSectionNode source);
	}

	public class ContentTreeSectionNodeToContentTreeSectionInputModelMapper : Mapper<ContentTreeSectionNode, ContentTreeSectionInputModel>, IContentTreeSectionNodeToContentTreeSectionInputModelMapper
	{
		public override void DefineMap(IConfiguration configuration)
		{
			configuration.CreateMap<ContentTreeSectionNode, ContentTreeSectionInputModel>()
				.ForMember(dest => dest.Action, opt => opt.Ignore())
				.ForMember(dest => dest.ParentTreeNodeId, opt => opt.Ignore());
		}
	}
}
