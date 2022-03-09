USE [TEL_Event]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Permission_MailGroup', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Permission_MailGroup', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Permission_MailGroup', @level2type=N'COLUMN',@level2name=N'mailgroupName'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Permission_MailGroup', @level2type=N'COLUMN',@level2name=N'eventid'
GO

/****** Object:  Table [dbo].[TEL_Event_Permission_MailGroup]    Script Date: 2022/3/9 下午 11:07:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEL_Event_Permission_MailGroup]') AND type in (N'U'))
DROP TABLE [dbo].[TEL_Event_Permission_MailGroup]
GO

/****** Object:  Table [dbo].[TEL_Event_Permission_MailGroup]    Script Date: 2022/3/9 下午 11:07:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TEL_Event_Permission_MailGroup](
	[eventid] [uniqueidentifier] NOT NULL,
	[mailgroupName] [nvarchar](255) NOT NULL,
	[modifiedby] [nvarchar](64) NOT NULL,
	[modifieddate] [datetime] NOT NULL,
 CONSTRAINT [PK_TEL_Event_Permission_MailGroup] PRIMARY KEY CLUSTERED 
(
	[eventid] ASC,
	[mailgroupName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Permission_MailGroup', @level2type=N'COLUMN',@level2name=N'eventid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'擁有權限的mailgroupName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Permission_MailGroup', @level2type=N'COLUMN',@level2name=N'mailgroupName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改人員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Permission_MailGroup', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改日期時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_Permission_MailGroup', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

