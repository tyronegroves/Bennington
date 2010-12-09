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
	public interface IContentTreeSectionInputModelToContentTreeSectionNodeMapper
	{
		ContentTreeSectionNode CreateInstance(ContentTreeSectionInputModel source);
		void LoadIntoInstance(ContentTreeSectionInputModel source, ContentTreeSectionNode destination);
	}

	public class ContentTreeSectionNodeInputModelToContentTreeSectionNodeMapper : Mapper<ContentTreeSectionInputModel, ContentTreeSectionNode>, IContentTreeSectionInputModelToContentTreeSectionNodeMapper
	{
		public override void DefineMap(IConfiguration configuration)
		{
			configuration.CreateMap<ContentTreeSectionInputModel, ContentTreeSectionNode>()
				.ForMember(dest => dest.Key, opt => opt.Ignore())
				.ForMember(dest => dest.CreateBy, opt => opt.Ignore())
				.ForMember(dest => dest.CreateDate, opt => opt.Ignore())
				.ForMember(dest => dest.LastModifyBy, opt => opt.Ignore())
				.ForMember(dest => dest.LastModifyDate, opt => opt.Ignore());
		}
	}
}
