using AutoMapper;
using AutoMapperAssist;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Models;

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
				.ForMember(dest => dest.CreateBy, opt => opt.Ignore())
				.ForMember(dest => dest.CreateDate, opt => opt.Ignore())
				.ForMember(dest => dest.LastModifyBy, opt => opt.Ignore())
				.ForMember(dest => dest.LastModifyDate, opt => opt.Ignore());
		}
	}
}
