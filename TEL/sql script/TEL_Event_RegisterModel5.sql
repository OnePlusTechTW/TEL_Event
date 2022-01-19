USE [TEL_Event]
GO

/****** Object:  Table [dbo].[TEL_Event_RegisterModel5]    Script Date: 2022/1/19 下午 01:44:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TEL_Event_RegisterModel5](
	[id] [uniqueidentifier] NOT NULL,
	[eventid] [uniqueidentifier] NOT NULL,
	[empid] [nvarchar](64) NOT NULL,
	[registerdate] [datetime] NOT NULL,
	[attachment1] [nvarchar](256) NOT NULL,
	[description1] [nvarchar](512) NULL,
	[attachment2] [nvarchar](256) NULL,
	[description2] [nvarchar](512) NULL,
	[attachment3] [nvarchar](256) NULL,
	[description3] [nvarchar](512) NULL,
	[feedback] [nvarchar](max) NULL,
	[modifiedby] [nvarchar](64) NOT NULL,
	[modifieddate] [datetime] NOT NULL,
 CONSTRAINT [PK_TEL_Event_RegisterModel5] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'eventid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'員工編號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'empid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'報名日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'registerdate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上傳附件1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上傳附件1之說明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'description1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上傳附件2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上傳附件2之說明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'description2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上傳附件3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment3'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上傳附件3之說明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'description3'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'意見/問題回饋' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'feedback'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改人員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改日期時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

