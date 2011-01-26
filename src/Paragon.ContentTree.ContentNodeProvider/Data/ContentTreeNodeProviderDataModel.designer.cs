﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Paragon.ContentTree.ContentNodeProvider.Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Paragon")]
	public partial class ContentTreeNodeProviderDataModelDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertContentNodeProviderDraft(ContentNodeProviderDraft instance);
    partial void UpdateContentNodeProviderDraft(ContentNodeProviderDraft instance);
    partial void DeleteContentNodeProviderDraft(ContentNodeProviderDraft instance);
    #endregion
		
		public ContentTreeNodeProviderDataModelDataContext() : 
				base(global::Paragon.ContentTree.ContentNodeProvider.Properties.Settings.Default.ParagonConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public ContentTreeNodeProviderDataModelDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContentTreeNodeProviderDataModelDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContentTreeNodeProviderDataModelDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContentTreeNodeProviderDataModelDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<ContentNodeProviderDraft> ContentNodeProviderDrafts
		{
			get
			{
				return this.GetTable<ContentNodeProviderDraft>();
			}
		}
		
		public System.Data.Linq.Table<ContentNodeProviderPublishedVersion> ContentNodeProviderPublishedVersions
		{
			get
			{
				return this.GetTable<ContentNodeProviderPublishedVersion>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.UpdateContentNodeProviderPublishedVersion")]
		public int UpdateContentNodeProviderPublishedVersion([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Key", DbType="Int")] System.Nullable<int> key, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PageId", DbType="NVarChar(300)")] string pageId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TreeNodeId", DbType="NVarChar(300)")] string treeNodeId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UrlSegment", DbType="NVarChar(300)")] string urlSegment, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Sequence", DbType="Int")] System.Nullable<int> sequence, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Name", DbType="NVarChar(300)")] string name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Action", DbType="NVarChar(300)")] string action, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MetaTitle", DbType="NVarChar(300)")] string metaTitle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MetaDescription", DbType="NText")] string metaDescription, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HeaderText", DbType="NVarChar(300)")] string headerText, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Body", DbType="NText")] string body, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MetaKeyword", DbType="NText")] string metaKeyword)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), key, pageId, treeNodeId, urlSegment, sequence, name, action, metaTitle, metaDescription, headerText, body, metaKeyword);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.CreateContentNodeProviderPublishedVersion")]
		public int CreateContentNodeProviderPublishedVersion([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Key", DbType="Int")] System.Nullable<int> key, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PageId", DbType="NVarChar(300)")] string pageId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TreeNodeId", DbType="NVarChar(300)")] string treeNodeId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UrlSegment", DbType="NVarChar(300)")] string urlSegment, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Sequence", DbType="Int")] System.Nullable<int> sequence, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Name", DbType="NVarChar(300)")] string name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Action", DbType="NVarChar(300)")] string action, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MetaTitle", DbType="NVarChar(300)")] string metaTitle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MetaDescription", DbType="NText")] string metaDescription, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HeaderText", DbType="NVarChar(300)")] string headerText, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Body", DbType="NText")] string body, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MetaKeyword", DbType="NText")] string metaKeyword)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), key, pageId, treeNodeId, urlSegment, sequence, name, action, metaTitle, metaDescription, headerText, body, metaKeyword);
			return ((int)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ContentNodeProviderDraft")]
	public partial class ContentNodeProviderDraft : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Key;
		
		private System.Nullable<System.DateTime> _CreateDate;
		
		private string _CreateBy;
		
		private System.Nullable<System.DateTime> _LastModifyDate;
		
		private string _LastModifyBy;
		
		private string _PageId;
		
		private string _TreeNodeId;
		
		private string _UrlSegment;
		
		private System.Nullable<int> _Sequence;
		
		private string _Name;
		
		private string _Action;
		
		private string _MetaTitle;
		
		private string _MetaDescription;
		
		private string _HeaderText;
		
		private string _Body;
		
		private string _MetaKeyword;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnKeyChanging(int value);
    partial void OnKeyChanged();
    partial void OnCreateDateChanging(System.Nullable<System.DateTime> value);
    partial void OnCreateDateChanged();
    partial void OnCreateByChanging(string value);
    partial void OnCreateByChanged();
    partial void OnLastModifyDateChanging(System.Nullable<System.DateTime> value);
    partial void OnLastModifyDateChanged();
    partial void OnLastModifyByChanging(string value);
    partial void OnLastModifyByChanged();
    partial void OnPageIdChanging(string value);
    partial void OnPageIdChanged();
    partial void OnTreeNodeIdChanging(string value);
    partial void OnTreeNodeIdChanged();
    partial void OnUrlSegmentChanging(string value);
    partial void OnUrlSegmentChanged();
    partial void OnSequenceChanging(System.Nullable<int> value);
    partial void OnSequenceChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnActionChanging(string value);
    partial void OnActionChanged();
    partial void OnMetaTitleChanging(string value);
    partial void OnMetaTitleChanged();
    partial void OnMetaDescriptionChanging(string value);
    partial void OnMetaDescriptionChanged();
    partial void OnHeaderTextChanging(string value);
    partial void OnHeaderTextChanged();
    partial void OnBodyChanging(string value);
    partial void OnBodyChanged();
    partial void OnMetaKeywordChanging(string value);
    partial void OnMetaKeywordChanged();
    #endregion
		
		public ContentNodeProviderDraft()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Key]", Storage="_Key", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
		public int Key
		{
			get
			{
				return this._Key;
			}
			set
			{
				if ((this._Key != value))
				{
					this.OnKeyChanging(value);
					this.SendPropertyChanging();
					this._Key = value;
					this.SendPropertyChanged("Key");
					this.OnKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateDate", DbType="DateTime", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<System.DateTime> CreateDate
		{
			get
			{
				return this._CreateDate;
			}
			set
			{
				if ((this._CreateDate != value))
				{
					this.OnCreateDateChanging(value);
					this.SendPropertyChanging();
					this._CreateDate = value;
					this.SendPropertyChanged("CreateDate");
					this.OnCreateDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateBy", DbType="NVarChar(500)", UpdateCheck=UpdateCheck.Never)]
		public string CreateBy
		{
			get
			{
				return this._CreateBy;
			}
			set
			{
				if ((this._CreateBy != value))
				{
					this.OnCreateByChanging(value);
					this.SendPropertyChanging();
					this._CreateBy = value;
					this.SendPropertyChanged("CreateBy");
					this.OnCreateByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastModifyDate", DbType="DateTime", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<System.DateTime> LastModifyDate
		{
			get
			{
				return this._LastModifyDate;
			}
			set
			{
				if ((this._LastModifyDate != value))
				{
					this.OnLastModifyDateChanging(value);
					this.SendPropertyChanging();
					this._LastModifyDate = value;
					this.SendPropertyChanged("LastModifyDate");
					this.OnLastModifyDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastModifyBy", DbType="NVarChar(500)", UpdateCheck=UpdateCheck.Never)]
		public string LastModifyBy
		{
			get
			{
				return this._LastModifyBy;
			}
			set
			{
				if ((this._LastModifyBy != value))
				{
					this.OnLastModifyByChanging(value);
					this.SendPropertyChanging();
					this._LastModifyBy = value;
					this.SendPropertyChanged("LastModifyBy");
					this.OnLastModifyByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PageId", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string PageId
		{
			get
			{
				return this._PageId;
			}
			set
			{
				if ((this._PageId != value))
				{
					this.OnPageIdChanging(value);
					this.SendPropertyChanging();
					this._PageId = value;
					this.SendPropertyChanged("PageId");
					this.OnPageIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TreeNodeId", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string TreeNodeId
		{
			get
			{
				return this._TreeNodeId;
			}
			set
			{
				if ((this._TreeNodeId != value))
				{
					this.OnTreeNodeIdChanging(value);
					this.SendPropertyChanging();
					this._TreeNodeId = value;
					this.SendPropertyChanged("TreeNodeId");
					this.OnTreeNodeIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UrlSegment", DbType="NVarChar(500)", UpdateCheck=UpdateCheck.Never)]
		public string UrlSegment
		{
			get
			{
				return this._UrlSegment;
			}
			set
			{
				if ((this._UrlSegment != value))
				{
					this.OnUrlSegmentChanging(value);
					this.SendPropertyChanging();
					this._UrlSegment = value;
					this.SendPropertyChanged("UrlSegment");
					this.OnUrlSegmentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sequence", DbType="Int", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<int> Sequence
		{
			get
			{
				return this._Sequence;
			}
			set
			{
				if ((this._Sequence != value))
				{
					this.OnSequenceChanging(value);
					this.SendPropertyChanging();
					this._Sequence = value;
					this.SendPropertyChanged("Sequence");
					this.OnSequenceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Action", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				if ((this._Action != value))
				{
					this.OnActionChanging(value);
					this.SendPropertyChanging();
					this._Action = value;
					this.SendPropertyChanged("Action");
					this.OnActionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MetaTitle", DbType="NVarChar(500)", UpdateCheck=UpdateCheck.Never)]
		public string MetaTitle
		{
			get
			{
				return this._MetaTitle;
			}
			set
			{
				if ((this._MetaTitle != value))
				{
					this.OnMetaTitleChanging(value);
					this.SendPropertyChanging();
					this._MetaTitle = value;
					this.SendPropertyChanged("MetaTitle");
					this.OnMetaTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MetaDescription", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string MetaDescription
		{
			get
			{
				return this._MetaDescription;
			}
			set
			{
				if ((this._MetaDescription != value))
				{
					this.OnMetaDescriptionChanging(value);
					this.SendPropertyChanging();
					this._MetaDescription = value;
					this.SendPropertyChanged("MetaDescription");
					this.OnMetaDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HeaderText", DbType="NVarChar(500)", UpdateCheck=UpdateCheck.Never)]
		public string HeaderText
		{
			get
			{
				return this._HeaderText;
			}
			set
			{
				if ((this._HeaderText != value))
				{
					this.OnHeaderTextChanging(value);
					this.SendPropertyChanging();
					this._HeaderText = value;
					this.SendPropertyChanged("HeaderText");
					this.OnHeaderTextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Body", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string Body
		{
			get
			{
				return this._Body;
			}
			set
			{
				if ((this._Body != value))
				{
					this.OnBodyChanging(value);
					this.SendPropertyChanging();
					this._Body = value;
					this.SendPropertyChanged("Body");
					this.OnBodyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MetaKeyword", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string MetaKeyword
		{
			get
			{
				return this._MetaKeyword;
			}
			set
			{
				if ((this._MetaKeyword != value))
				{
					this.OnMetaKeywordChanging(value);
					this.SendPropertyChanging();
					this._MetaKeyword = value;
					this.SendPropertyChanged("MetaKeyword");
					this.OnMetaKeywordChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ContentNodeProviderPublishedVersion")]
	public partial class ContentNodeProviderPublishedVersion
	{
		
		private System.Nullable<int> _Key;
		
		private string _PageId;
		
		private string _TreeNodeId;
		
		private string _UrlSegment;
		
		private System.Nullable<int> _Sequence;
		
		private string _Name;
		
		private string _Action;
		
		private string _MetaTitle;
		
		private string _MetaDescription;
		
		private string _HeaderText;
		
		private string _Body;
		
		private string _MetaKeyword;
		
		public ContentNodeProviderPublishedVersion()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Key]", Storage="_Key", DbType="Int", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<int> Key
		{
			get
			{
				return this._Key;
			}
			set
			{
				if ((this._Key != value))
				{
					this._Key = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PageId", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string PageId
		{
			get
			{
				return this._PageId;
			}
			set
			{
				if ((this._PageId != value))
				{
					this._PageId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TreeNodeId", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string TreeNodeId
		{
			get
			{
				return this._TreeNodeId;
			}
			set
			{
				if ((this._TreeNodeId != value))
				{
					this._TreeNodeId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UrlSegment", DbType="NVarChar(500)", UpdateCheck=UpdateCheck.Never)]
		public string UrlSegment
		{
			get
			{
				return this._UrlSegment;
			}
			set
			{
				if ((this._UrlSegment != value))
				{
					this._UrlSegment = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sequence", DbType="Int", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<int> Sequence
		{
			get
			{
				return this._Sequence;
			}
			set
			{
				if ((this._Sequence != value))
				{
					this._Sequence = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Action", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				if ((this._Action != value))
				{
					this._Action = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MetaTitle", DbType="NVarChar(500)", UpdateCheck=UpdateCheck.Never)]
		public string MetaTitle
		{
			get
			{
				return this._MetaTitle;
			}
			set
			{
				if ((this._MetaTitle != value))
				{
					this._MetaTitle = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MetaDescription", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string MetaDescription
		{
			get
			{
				return this._MetaDescription;
			}
			set
			{
				if ((this._MetaDescription != value))
				{
					this._MetaDescription = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HeaderText", DbType="NVarChar(500)", UpdateCheck=UpdateCheck.Never)]
		public string HeaderText
		{
			get
			{
				return this._HeaderText;
			}
			set
			{
				if ((this._HeaderText != value))
				{
					this._HeaderText = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Body", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string Body
		{
			get
			{
				return this._Body;
			}
			set
			{
				if ((this._Body != value))
				{
					this._Body = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MetaKeyword", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string MetaKeyword
		{
			get
			{
				return this._MetaKeyword;
			}
			set
			{
				if ((this._MetaKeyword != value))
				{
					this._MetaKeyword = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
