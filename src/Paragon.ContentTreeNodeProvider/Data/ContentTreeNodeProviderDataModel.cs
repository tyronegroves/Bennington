using System;
using System.Configuration;
using System.Data.Linq;
using System.Transactions;
using Paragon.ContentTree.Models;

namespace Paragon.ContentTreeNodeProvider.Data
{
	public interface IDataModelDataContext
	{
		System.Data.Linq.Table<ContentTreeNode> ContentTreeNodes { get; }
		void Create(ContentTreeNode instance);
		void Update(ContentTreeNode instance);
		void Delete(ContentTreeNode instance);
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
	}
}
