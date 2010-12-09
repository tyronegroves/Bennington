using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapperAssist;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTreeNodeProvider.Data;
using Paragon.ContentTreeNodeProvider.Models;

namespace Paragon.ContentTreeNodeProvider.Mappers
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
