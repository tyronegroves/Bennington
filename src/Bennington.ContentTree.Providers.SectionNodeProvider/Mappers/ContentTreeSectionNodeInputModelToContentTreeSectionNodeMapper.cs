using AutoMapper;
using AutoMapperAssist;
using Bennington.ContentTree.Providers.SectionNodeProvider.Models;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Mappers
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
					;
		}
	}
}
