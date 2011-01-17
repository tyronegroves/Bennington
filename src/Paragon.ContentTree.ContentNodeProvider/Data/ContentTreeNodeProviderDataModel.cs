using System.Configuration;
using System.Transactions;
using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.ContentNodeProvider.Data
{
	public interface IDataModelDataContext
	{
		System.Data.Linq.Table<ContentTreeNode> ContentTreeNodes { get; }
		System.Data.Linq.Table<ContentNodeProviderDraft> ContentNodeProviderDrafts { get; }
		void Create(ContentTreeNode instance);
		void Update(ContentTreeNode instance);
		void Delete(ContentTreeNode instance);
		void Create(ContentNodeProviderDraft instance);
		void Update(ContentNodeProviderDraft instance);
		void Delete(ContentNodeProviderDraft instance);
	}

	partial class ContentTreeNode : IAmATreeNodeExtension
	{
	}


	partial class ContentTreeNodeProviderDataModelDataContext : IDataModelDataContext
	{
		partial void OnCreated()
		{
			Connection.ConnectionString = ConfigurationManager.ConnectionStrings["Paragon.ContentTreeNodeProvider"].ConnectionString;
		}

		public void Create(ContentTreeNode instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				ContentTreeNodes.InsertOnSubmit(instance);
				SubmitChanges();
			}
		}

		public void Update(ContentTreeNode instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				SubmitChanges();
			}
		}

		public void Delete(ContentTreeNode instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				ContentTreeNodes.DeleteAllOnSubmit(new[] { instance });
				SubmitChanges();
			}
		}

		public void Create(ContentNodeProviderDraft instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				ContentNodeProviderDrafts.InsertOnSubmit(instance);
				SubmitChanges();
			}
		}

		public void Update(ContentNodeProviderDraft instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				SubmitChanges();
			}
		}

		public void Delete(ContentNodeProviderDraft instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				ContentNodeProviderDrafts.DeleteAllOnSubmit(new[] { instance });
				SubmitChanges();
			}
		}
	}
}
