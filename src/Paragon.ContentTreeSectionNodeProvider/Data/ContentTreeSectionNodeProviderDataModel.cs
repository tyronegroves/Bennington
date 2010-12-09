using System;
using System.Configuration;
using System.Data.Linq;
using System.Transactions;
using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.SectionNodeProvider.Data
{
	public interface IDataModelDataContext
	{
		System.Data.Linq.Table<ContentTreeSectionNode> ContentTreeSectionNodes { get; }
		void Create(ContentTreeSectionNode instance);
		void Update(ContentTreeSectionNode instance);
		void Delete(ContentTreeSectionNode instance);
	}

	partial class ContentTreeSectionNode : IAmATreeNodeExtension
	{
	}


	partial class ContentTreeSectionNodeProviderDataModelDataContext : IDataModelDataContext
	{
		partial void OnCreated()
		{
			Connection.ConnectionString = ConfigurationManager.ConnectionStrings["Paragon.ContentTreeNodeProvider"].ConnectionString;
		}

		public void Create(ContentTreeSectionNode instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				ContentTreeSectionNodes.InsertOnSubmit(instance);
				SubmitChanges();
			}
		}

		public void Update(ContentTreeSectionNode instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				SubmitChanges();
			}
		}

		public void Delete(ContentTreeSectionNode instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				ContentTreeSectionNodes.DeleteAllOnSubmit(new[] { instance });
				SubmitChanges();
			}
		}
	}
}
