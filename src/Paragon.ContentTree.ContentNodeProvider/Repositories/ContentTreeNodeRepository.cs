using System;
using System.Linq;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Repositories;
using IDataModelDataContext = Paragon.ContentTree.ContentNodeProvider.Data.IDataModelDataContext;

namespace Paragon.ContentTree.ContentNodeProvider.Repositories
{
	public interface IContentTreeNodeRepository
	{
		IQueryable<ContentTreeNode> GetAllContentTreeNodes();
		void Update(ContentTreeNode instance);
		void Create(ContentTreeNode instance);
		void Delete(ContentTreeNode instance);
	}

	public class ContentTreeNodeRepository : IContentTreeNodeRepository
	{
		private readonly IDataModelDataContext dataModelDataContext;
		private readonly ITreeNodeRepository treeNodeRepository;
		private readonly IContentNodeProviderDraftRepository contentNodeProviderDraftRepository;
		private readonly IContentNodeProviderDraftToContentTreeNodeMapper contentNodeProviderDraftToContentTreeNodeMapper;

		public ContentTreeNodeRepository(IDataModelDataContext dataModelDataContext, 
										ITreeNodeRepository treeNodeRepository, 
										IContentNodeProviderDraftRepository contentNodeProviderDraftRepository,
										IContentNodeProviderDraftToContentTreeNodeMapper contentNodeProviderDraftToContentTreeNodeMapper)
		{
			this.contentNodeProviderDraftToContentTreeNodeMapper = contentNodeProviderDraftToContentTreeNodeMapper;
			this.contentNodeProviderDraftRepository = contentNodeProviderDraftRepository;
			this.treeNodeRepository = treeNodeRepository;
			this.dataModelDataContext = dataModelDataContext;
		}

		public IQueryable<ContentTreeNode> GetAllContentTreeNodes()
		{
			return contentNodeProviderDraftToContentTreeNodeMapper.CreateSet(contentNodeProviderDraftRepository.GetAllContentNodeProviderDrafts()).AsQueryable();	
			//return dataModelDataContext.ContentTreeNodes;
		}

		public void Delete(ContentTreeNode instance)
		{
			throw new NotImplementedException();
			//treeNodeRepository.Delete(instance.TreeNodeId);
		}

		public void Update(ContentTreeNode instance)
		{
			throw new NotImplementedException();
			//dataModelDataContext.Update(instance);
		}

		public void Create(ContentTreeNode instance)
		{
			throw new NotImplementedException();
			//dataModelDataContext.Create(instance);
		}
	}
}