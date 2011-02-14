using System.Linq;
using Bennington.ContentTree.Data;

namespace Bennington.ContentTree.Repositories
{
	public interface ITreeNodeRepository
	{
		IQueryable<TreeNode> GetAll();
		TreeNode Create(TreeNode treeNode);
		void Delete(string id);
		void Update(TreeNode treeNode);
	}

	public class TreeNodeRepository : ITreeNodeRepository
	{
		private readonly IDataModelDataContext dataModelDataContext;

		public TreeNodeRepository(IDataModelDataContext dataModelDataContext)
		{
			this.dataModelDataContext = dataModelDataContext;
		}

		public IQueryable<TreeNode> GetAll()
		{
			return dataModelDataContext.TreeNodes;
		}

		public TreeNode Create(TreeNode treeNode)
		{
			dataModelDataContext.Create(treeNode);
			return treeNode;
		}

		public void Delete(string id)
		{
			dataModelDataContext.Delete(id);
		}

		public void Update(TreeNode treeNode)
		{
			dataModelDataContext.Update(treeNode);
		}
	}
}