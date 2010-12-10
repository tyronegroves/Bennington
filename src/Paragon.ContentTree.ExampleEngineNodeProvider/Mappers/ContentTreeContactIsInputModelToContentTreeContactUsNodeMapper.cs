using AutoMapper;
using AutoMapperAssist;
using Paragon.ContentTree.ExampleEngineNodeProvider.Data;
using Paragon.ContentTree.ExampleEngineNodeProvider.Models;

namespace Paragon.ContentTree.ExampleEngineNodeProvider.Mappers
{
	public interface IContentTreeContactUsInputModelToContentTreeContactUsNodeMapper
	{
		ContentTreeContactUsNode CreateInstance(ContentTreeContactUsNodeInputModel source);
		void LoadIntoInstance(ContentTreeContactUsNodeInputModel source, ContentTreeContactUsNode destination);
	}

	public class ContentTreeContactUsNodeInputModelToContentTreeContactUsNodeMapper : Mapper<ContentTreeContactUsNodeInputModel, ContentTreeContactUsNode>, IContentTreeContactUsInputModelToContentTreeContactUsNodeMapper
	{
		public override void DefineMap(IConfiguration configuration)
		{
			configuration.CreateMap<ContentTreeContactUsNodeInputModel, ContentTreeContactUsNode>()
				.ForMember(dest => dest.Key, opt => opt.Ignore())
				.ForMember(dest => dest.CreateBy, opt => opt.Ignore())
				.ForMember(dest => dest.CreateDate, opt => opt.Ignore())
				.ForMember(dest => dest.LastModifyBy, opt => opt.Ignore())
				.ForMember(dest => dest.LastModifyDate, opt => opt.Ignore());
		}
	}
}
