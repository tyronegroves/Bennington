using System;
using System.Configuration;
using System.Transactions;

namespace Paragon.ContentTree.ContentNodeProvider.Data
{
	public interface IDataModelDataContext
	{
		System.Data.Linq.Table<ContentNodeProviderDraft> ContentNodeProviderDrafts { get; }
		void Create(ContentNodeProviderDraft instance);
		void Update(ContentNodeProviderDraft instance);
		void Delete(ContentNodeProviderDraft instance);
		System.Data.Linq.Table<ContentNodeProviderPublishedVersion> ContentNodeProviderPublishedVersions { get; }
		void Create(ContentNodeProviderPublishedVersion instance);
		void Update(ContentNodeProviderPublishedVersion instance);
		void Delete(ContentNodeProviderPublishedVersion instance);
	}

	partial class ContentTreeNodeProviderDataModelDataContext : IDataModelDataContext
	{
		public ContentTreeNodeProviderDataModelDataContext(HttpStyleUriParser a, HttpStyleUriParser b, HttpStyleUriParser  c)
			: this()
		{ }

		partial void OnCreated()
		{
			Connection.ConnectionString = ConfigurationManager.ConnectionStrings["Paragon.ContentTreeNodeProvider"].ConnectionString;
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

		public void Create(ContentNodeProviderPublishedVersion instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				//ContentNodeProviderPublishedVersions.InsertOnSubmit(instance);
				//SubmitChanges();
				CreateContentNodeProviderPublishedVersion(instance.Key, instance.PageId, instance.TreeNodeId, instance.UrlSegment, instance.Sequence, instance.Name, instance.Action, instance.MetaTitle, instance.MetaDescription, instance.HeaderText, instance.Body, instance.MetaKeyword);
			}
		}

		public void Update(ContentNodeProviderPublishedVersion instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				UpdateContentNodeProviderPublishedVersion(instance.Key, instance.PageId, instance.TreeNodeId, instance.UrlSegment, instance.Sequence, instance.Name, instance.Action, instance.MetaTitle, instance.MetaDescription, instance.HeaderText, instance.Body, instance.MetaKeyword);
				//ContentNodeProviderPublishedVersions.Attach(instance, true);
				//SubmitChanges();
			}
		}

		public void Delete(ContentNodeProviderPublishedVersion instance)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				ContentNodeProviderPublishedVersions.DeleteAllOnSubmit(new[] { instance });
				SubmitChanges();
			}
		}
	}
}
