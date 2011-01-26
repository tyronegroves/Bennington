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


CREATE TABLE [dbo].[ContentNodeProviderPublishedVersion](
	[Key] [int] NULL,
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
	[MetaKeyword] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

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

/****** Object:  StoredProcedure [dbo].[CreateContentNodeProviderPublishedVersion]    Script Date: 01/26/2011 01:35:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CreateContentNodeProviderPublishedVersion]
	-- Add the parameters for the stored procedure here
	@Key int, 
	@PageId nvarchar(300),
	@TreeNodeId nvarchar(300),
	@UrlSegment nvarchar(300),
	@Sequence int,
	@Name nvarchar(300),
	@Action nvarchar(300),
	@MetaTitle nvarchar(300),
	@MetaDescription ntext,
	@HeaderText nvarchar(300),
	@Body ntext,
	@MetaKeyword ntext
	--<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	-- SELECT <@Param1, sysname, @p1>, <@Param2, sysname, @p2>
	Insert Into dbo.ContentNodeProviderPublishedVersion
	(
		[Key],
			PageId,
			TreeNodeId,
			UrlSegment,
			Name,
			[Action],
			MetaTitle,
			MetaDescription,
			HeaderText,
			Body,
			MetaKeyword
	) VALUES (
		@Key,
		@PageId,
		@TreeNodeId,
		@UrlSegment,
			@Name,
			@Action,
			@MetaTitle,
			 @MetaDescription,
			@HeaderText,
			@Body,
			@MetaKeyword
		);
END


GO


CREATE PROCEDURE [dbo].[UpdateContentNodeProviderPublishedVersion]
	-- Add the parameters for the stored procedure here
	@Key int, 
	@PageId nvarchar(300),
	@TreeNodeId nvarchar(300),
	@UrlSegment nvarchar(300),
	@Sequence int,
	@Name nvarchar(300),
	@Action nvarchar(300),
	@MetaTitle nvarchar(300),
	@MetaDescription ntext,
	@HeaderText nvarchar(300),
	@Body ntext,
	@MetaKeyword ntext
	--<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	-- SELECT <@Param1, sysname, @p1>, <@Param2, sysname, @p2>
	Update dbo.ContentNodeProviderPublishedVersion
		Set [Key] = @Key,
			PageId = @PageId,
			TreeNodeId = @TreeNodeId,
			UrlSegment = @UrlSegment,
			Name = @Name,
			[Action] = @Action,
			MetaTitle = @MetaTitle,
			MetaDescription = @MetaDescription,
			HeaderText = @HeaderText,
			Body = @Body,
			MetaKeyword = @MetaKeyword
	From dbo.ContentNodeProviderPublishedVersion
		Where (ContentNodeProviderPublishedVersion.PageId Like @PageId)
END

GO

