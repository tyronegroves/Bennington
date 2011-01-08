CREATE TABLE TreeNode
(
	CreateDate DATETIME,
	CreateBy nvarchar(500),
	LastModifyDate DATETIME,
	LastModifyBy nvarchar(500),
	Id nvarchar(100) PRIMARY KEY,
	Type nvarchar(500),
	ParentTreeNodeId nvarchar(100)
);

CREATE TABLE [dbo].[ContentTreeNode](
	[Key] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](500) NULL,
	[LastModifyDate] [datetime] NULL,
	[LastModifyBy] [nvarchar](500) NULL,
	[TreeNodeId] [nvarchar](100) NULL,
	[UrlSegment] [nvarchar](500) NULL,
	[Sequence] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Content] [ntext] NULL,
 CONSTRAINT [PK_ContentTreeNode] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[ContentTreeSectionNode](
	[Key] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](500) NULL,
	[LastModifyDate] [datetime] NULL,
	[LastModifyBy] [nvarchar](500) NULL,
	[TreeNodeId] [nvarchar](100) NULL,
	[Sequence] [int] NULL,
	[Name] [nvarchar](500) NULL,
	[UrlSegment] [nvarchar](500) NULL,
	[Title] [nvarchar](100) NULL,
	[DefaultTreeNodeId] [nvarchar](100) NULL,
 CONSTRAINT [PK_ContentTreeSectionNode] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


Insert Into dbo.[ContentTreeNode] (TreeNodeId, UrlSegment, Sequence) Values ('00000000-0000-0000-0000-000000000000', '', Null);
Insert Into dbo.[ContentTreeNode] (TreeNodeId, UrlSegment, Sequence, [Name]) Values ('Home', 'Index_', 10, 'Homepage');

Insert Into dbo.[TreeNode] (Id, [Type], ParentTreeNodeId) Values ('00000000-0000-0000-0000-000000000000', 'Paragon.ContentTreeNodeProvider.ContentTreeNodeExtensionProvider', '-1');
Insert Into dbo.[TreeNode] (Id, [Type], ParentTreeNodeId) Values ('Home', 'Paragon.ContentTreeNodeProvider.ContentTreeNodeExtensionProvider', '0');