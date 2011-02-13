using AutoMapper;
using AutoMapperAssist;
using Paragon.ContentTree.SectionNodeProvider.Data;
using Paragon.ContentTree.SectionNodeProvider.Models;

namespace Paragon.ContentTree.SectionNodeProvider.Mappers
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
