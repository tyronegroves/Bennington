using System.Linq;
using AutoMapper;
using AutoMapperAssist;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using Bennington.ContentTree.Repositories;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Mappers
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
				.ForMember(dest => dest.FormAction, opt => opt.Ignore())
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
