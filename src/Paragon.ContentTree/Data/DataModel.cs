using System.Configuration;
using System.Transactions;
using System.Linq;

namespace Paragon.ContentTree.Data
{
	public interface IDataModelDataContext
	{
		System.Data.Linq.Table<TreeNode> TreeNodes { get; }
		TreeNode Create(TreeNode instance);
		void Delete(string id);
		void Update(TreeNode treeNode);
	}

	partial class DataModelDataContext : IDataModelDataContext
	{
		public TreeNode Create(TreeNode instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				TreeNodes.InsertOnSubmit(instance);
				SubmitChanges();
			}
			return instance;
		}

		public void Update(TreeNode instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				SubmitChanges();
			}
		}

		partial void OnCreated()
		{
			Connection.ConnectionString = ConfigurationManager.ConnectionStrings["Paragon.ContentTree"].ConnectionString;
		}

		public void Delete(string id)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				var treeNode = TreeNodes.Where(a => a.Id == id);
				TreeNodes.DeleteAllOnSubmit(treeNode);
				SubmitChanges();
			}
		}
	}
}