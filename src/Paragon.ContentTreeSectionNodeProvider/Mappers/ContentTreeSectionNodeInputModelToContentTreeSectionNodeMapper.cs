using AutoMapper;
using AutoMapperAssist;
using Paragon.ContentTree.SectionNodeProvider.Data;
using Paragon.ContentTree.SectionNodeProvider.Models;

namespace Paragon.ContentTree.SectionNodeProvider.Mappers
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
