using AutoMapper;
using AutoMapperAssist;
using Paragon.ContentTree.ExampleEngineNodeProvider.Data;
using Paragon.ContentTree.ExampleEngineNodeProvider.Models;

namespace Paragon.ContentTree.ExampleEngineNodeProvider.Mappers
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
