using AutoMapper;
using AutoMapperAssist;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.Data;

namespace Paragon.ContentTree.ContentNodeProvider.Mappers
{
	public interface IContentTreeNodeInputModelToContentTreeNodeMapper
	{
		ContentTreeNode CreateInstance(ContentTreeNodeInputModel source);
		void LoadIntoInstance(ContentTreeNodeInputModel source, ContentTreeNode destination);
	}

	public class ContentTreeNodeInputModelToContentTreeNodeMapper : Mapper<ContentTreeNodeInputModel, ContentTreeNode>, IContentTreeNodeInputModelToContentTreeNodeMapper
	{
		public override void DefineMap(IConfiguration configuration)
		{
			configuration.CreateMap<ContentTreeNodeInputModel, ContentTreeNode>()
				.ForMember(dest => dest.Key, opt => opt.Ignore())
				;
		}
	}
}
