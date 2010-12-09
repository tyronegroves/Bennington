using AutoMapperAssist;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.Routing.Content;

namespace Paragon.ContentTree.Routing.Mappers
{
	public interface ITreeNodeSummaryToContentTreeNodeMapper
	{
		//IEnumerable<ContentTreeNode> CreateSet(IEnumerable<TreeNodeSummary> source);
		ContentTreeNode CreateInstance(TreeNodeSummary source);
	}

	public class TreeNodeSummaryToContentTreeNodeMapper : Mapper<TreeNodeSummary, ContentTreeNode>, ITreeNodeSummaryToContentTreeNodeMapper
	{
		public override void DefineMap(AutoMapper.IConfiguration configuration)
		{
			configuration.CreateMap<TreeNodeSummary, ContentTreeNode>()
				.ForMember(dest => dest.NodeId, a => a.MapFrom(b => b.Id))
				.ForMember(dest => dest.UrlSegment, a => a.MapFrom(b => b.UrlSegment))
				.ForMember(dest => dest.Parent, a => a.Ignore())
				.ForMember(dest => dest.ChildNodes, a => a.Ignore())
				;
		}
	}
}