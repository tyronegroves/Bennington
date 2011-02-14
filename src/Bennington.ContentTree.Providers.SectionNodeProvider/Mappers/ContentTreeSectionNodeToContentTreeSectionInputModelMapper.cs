using AutoMapper;
using AutoMapperAssist;
using Bennington.ContentTree.Providers.SectionNodeProvider.Models;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Mappers
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
