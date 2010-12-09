using AutoMapperAssist;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Routing.Content;

namespace Paragon.ContentTree.Routing.Mappers
{
	public interface IContentTreeNodeToTreeNodeMapper
	{
		TreeNode CreateInstance(ContentTreeNode source);
	}

	public class ContentTreeNodeToTreeNodeMapper : Mapper<ContentTreeNode, TreeNode>, IContentTreeNodeToTreeNodeMapper
	{
		public override void DefineMap(AutoMapper.IConfiguration configuration)
		{
			configuration.CreateMap<ContentTreeNode, TreeNode>()
				.ForMember(dest => dest.CreateBy, a => a.Ignore())
				.ForMember(dest => dest.CreateDate, a => a.Ignore())
				.ForMember(dest => dest.LastModifyBy, a => a.Ignore())
				.ForMember(dest => dest.LastModifyDate, a => a.Ignore())
				.ForMember(dest => dest.ParentTreeNodeId, a => a.Ignore())
				.ForMember(dest => dest.TreeNode1, a => a.Ignore())
				.ForMember(dest => dest.Id, a => a.Ignore())
				.ForMember(dest => dest.TreeNodes, a => a.Ignore())
				;
		}
	}
}