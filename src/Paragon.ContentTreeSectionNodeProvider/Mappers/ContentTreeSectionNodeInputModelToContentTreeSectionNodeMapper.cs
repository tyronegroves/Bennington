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
					;
		}
	}
}
