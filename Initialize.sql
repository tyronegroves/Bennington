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

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SectionNodeProviderDraft](
	[SectionId] [nvarchar](100) NOT NULL,
	[TreeNodeId] [nvarchar](100) NULL,
	[Sequence] [int] NULL,
	[Name] [nvarchar](500) NULL,
	[UrlSegment] [nvarchar](500) NULL,
	[DefaultTreeNodeId] [nvarchar](100) NULL,
 CONSTRAINT [PK_SectionNodeProviderDraft_1] PRIMARY KEY CLUSTERED 
(
	[SectionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


GO

CREATE TABLE [dbo].[ContentNodeProviderDraft](
	[Key] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](500) NULL,
	[LastModifyDate] [datetime] NULL,
	[LastModifyBy] [nvarchar](500) NULL,
	[PageId] [nvarchar](100) NULL,
	[TreeNodeId] [nvarchar](100) NULL,
	[UrlSegment] [nvarchar](500) NULL,
	[Sequence] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Action] [nvarchar](100) NULL,
	[MetaTitle] [nvarchar](500) NULL,
	[MetaDescription] [ntext] NULL,
	[HeaderText] [nvarchar](500) NULL,
	[Body] [ntext] NULL,
	[MetaKeyword] [ntext] NULL,
 CONSTRAINT [PK_ContentNodeProviderDraft] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

--Insert Into dbo.[TreeNode] (Id, [Type], ParentTreeNodeId) Values ('00000000-0000-0000-0000-000000000000', 'Paragon.ContentTree.ContentNodeProvider.ContentNodeProvider, Paragon.ContentTree.ContentNodeProvider, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', '');
--INSERT INTO [dbo].[ContentNodeProviderDraft] ([TreeNodeId]) VALUES ('00000000-0000-0000-0000-000000000000')