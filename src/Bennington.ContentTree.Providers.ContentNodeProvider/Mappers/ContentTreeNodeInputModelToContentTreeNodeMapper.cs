using AutoMapper;
using AutoMapperAssist;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Mappers
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
                    .ForMember(a => a.IconUrl, b=>b.Ignore())
                    .ForMember(a => a.LastModifyBy, b => b.Ignore())
                    .ForMember(a => a.LastModifyDate, b => b.Ignore())
                ;
		}
	}
}
