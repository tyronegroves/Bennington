using AutoMapper;
using AutoMapperAssist;
using Paragon.ContentTree.Providers.ContentTreeContactUsNodeProvider.Data;
using Paragon.ContentTree.Providers.ContentTreeContactUsNodeProvider.Models;

namespace Paragon.ContentTree.Providers.ContentTreeContactUsNodeProvider.Mappers
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
