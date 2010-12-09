using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapperAssist;
using ContentTreeContactUsNodeProvider.Data;
using ContentTreeContactUsNodeProvider.Models;

namespace Paragon.ContentTreeContactUsNodeProvider.Mappers
{
	public interface IContentTreeContactUsNodeToContentTreeContactUsInputModelMapper
	{
		ContentTreeContactUsNodeInputModel CreateInstance(ContentTreeContactUsNode source);
	}

	public class ContentTreeContactUsNodeToContentTreeContactUsInputModelMapper : Mapper<ContentTreeContactUsNode, ContentTreeContactUsNodeInputModel>, IContentTreeContactUsNodeToContentTreeContactUsInputModelMapper
	{
		public override void DefineMap(IConfiguration configuration)
		{
			configuration.CreateMap<ContentTreeContactUsNode, ContentTreeContactUsNodeInputModel>()
				.ForMember(dest => dest.Action, opt => opt.Ignore())
				.ForMember(dest => dest.ParentTreeNodeId, opt => opt.Ignore());
		}
	}
}
