using System.Linq;
using AutoMapper;
using AutoMapperAssist;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Repositories;

namespace Paragon.ContentTree.ContentNodeProvider.Mappers
{
	public interface IContentTreeNodeToContentTreeNodeInputModelMapper
	{
		ContentTreeNodeInputModel CreateInstance(ContentTreeNode source);
	}

	public class ContentTreeNodeToContentTreeNodeInputModelMapper : Mapper<ContentTreeNode, ContentTreeNodeInputModel>, IContentTreeNodeToContentTreeNodeInputModelMapper
	{
		private readonly ITreeNodeRepository treeNodeRepository;

		public ContentTreeNodeToContentTreeNodeInputModelMapper(ITreeNodeRepository treeNodeRepository)
		{
			this.treeNodeRepository = treeNodeRepository;
		}

		public override void DefineMap(IConfiguration configuration)
		{
			configuration.CreateMap<ContentTreeNode, ContentTreeNodeInputModel>()
				.ForMember(dest => dest.Action, opt => opt.Ignore())
				.ForMember(dest => dest.ParentTreeNodeId, opt => opt.Ignore());
		}

		public override ContentTreeNodeInputModel CreateInstance(ContentTreeNode source)
		{
			var returnInstance = base.CreateInstance(source);
			
			var treeNode = treeNodeRepository.GetAll().Where(a => a.Id == source.TreeNodeId).FirstOrDefault();
			if (treeNode != null)
				returnInstance.Type = treeNode.Type;
			
			return returnInstance;
		}
	}
}
