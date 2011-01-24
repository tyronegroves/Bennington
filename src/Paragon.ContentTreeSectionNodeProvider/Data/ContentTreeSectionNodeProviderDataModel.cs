using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Transactions;
using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.SectionNodeProvider.Data
{
	public interface IDataModelDataContext
	{
		//IEnumerable<ContentTreeSectionNode> ContentTreeSectionNodes { get; }
		//void Create(ContentTreeSectionNode instance);
		//void Update(ContentTreeSectionNode instance);
		//void Delete(ContentTreeSectionNode instance);
		IEnumerable<SectionNodeProviderDraft> GetAllSectionNodeProviderDrafts();
		void Create(SectionNodeProviderDraft instance);
		void Update(SectionNodeProviderDraft instance);
		void Delete(SectionNodeProviderDraft instance);
	}

	//partial class ContentTreeSectionNode : IAmATreeNodeExtension
	//{
	//}


	partial class ContentTreeSectionNodeProviderDataModelDataContext : IDataModelDataContext
	{
		partial void OnCreated()
		{
			Connection.ConnectionString = ConfigurationManager.ConnectionStrings["Paragon.ContentTreeNodeProvider"].ConnectionString;
		}

		//public void Create(ContentTreeSectionNode instance)
		//{
		//    using (new TransactionScope(TransactionScopeOption.Suppress))
		//    {
		//        ContentTreeSectionNodes.InsertOnSubmit(instance);
		//        SubmitChanges();
		//    }
		//}

		//public void Update(ContentTreeSectionNode instance)
		//{
		//    using (new TransactionScope(TransactionScopeOption.Suppress))
		//    {
		//        SubmitChanges();
		//    }
		//}

		//public void Delete(ContentTreeSectionNode instance)
		//{
		//    using (new TransactionScope(TransactionScopeOption.Suppress))
		//    {
		//        ContentTreeSectionNodes.DeleteAllOnSubmit(new[] { instance });
		//        SubmitChanges();
		//    }
		//}

		public IEnumerable<SectionNodeProviderDraft> GetAllSectionNodeProviderDrafts()
		{
			return SectionNodeProviderDrafts.ToArray();
		}

		public void Create(SectionNodeProviderDraft instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				SectionNodeProviderDrafts.InsertOnSubmit(instance);
				SubmitChanges();
			}
		}

		public void Update(SectionNodeProviderDraft instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				SubmitChanges();
			}
		}

		public void Delete(SectionNodeProviderDraft instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				SectionNodeProviderDrafts.DeleteAllOnSubmit(new[] { instance });
				SubmitChanges();
			}
		}
	}
}
