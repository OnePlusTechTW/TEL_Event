USE [TEL_Event]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'surveystartdate'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'surveymodel'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'registermodel'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'duplicated'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'enabled'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'image2'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'image1'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'description'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'mailgroupother'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'mailgroup'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'member'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'registerend'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'registerstart'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'limit'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'eventend'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'eventstart'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'categoryid'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'name'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'id'
GO

/****** Object:  Table [dbo].[TEL_Event_Events]    Script Date: 2022/3/9 下午 11:06:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEL_Event_Events]') AND type in (N'U'))
DROP TABLE [dbo].[TEL_Event_Events]
GO

/****** Object:  Table [dbo].[TEL_Event_Events]    Script Date: 2022/3/9 下午 11:06:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TEL_Event_Events](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[categoryid] [uniqueidentifier] NOT NULL,
	[eventstart] [datetime] NOT NULL,
	[eventend] [datetime] NOT NULL,
	[limit] [int] NULL,
	[registerstart] [datetime] NOT NULL,
	[registerend] [datetime] NOT NULL,
	[member] [varchar](1) NOT NULL,
	[mailgroup] [nvarchar](256) NULL,
	[mailgroupother] [nvarchar](256) NULL,
	[description] [nvarchar](max) NOT NULL,
	[image1] [nvarchar](64) NULL,
	[image1_name] [nvarchar](64) NULL,
	[image2] [nvarchar](64) NULL,
	[image2_name] [nvarchar](64) NULL,
	[enabled] [varchar](1) NULL,
	[duplicated] [varchar](1) NULL,
	[registermodel] [varchar](1) NOT NULL,
	[surveymodel] [varchar](1) NULL,
	[surveystartdate] [datetime] NULL,
	[initby] [nvarchar](64) NOT NULL,
	[initdate] [datetime] NOT NULL,
	[modifiedby] [nvarchar](64) NOT NULL,
	[modifieddate] [datetime] NOT NULL,
 CONSTRAINT [PK_TEL_Event_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動名稱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動分類' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'categoryid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動開始日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'eventstart'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動結束日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'eventend'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'人數上限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'limit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'報名開始日期時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'registerstart'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'報名結束日期時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'registerend'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動成員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'member'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'郵件群組' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'mailgroup'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'郵件群組自填' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'mailgroupother'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動說明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動縮圖' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'image1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動大圖' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'image2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否上架' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'enabled'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否允許重複報名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'duplicated'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'報名表模板' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'registermodel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'問卷模板' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'surveymodel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'問卷開始日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'surveystartdate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改人員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Events', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

