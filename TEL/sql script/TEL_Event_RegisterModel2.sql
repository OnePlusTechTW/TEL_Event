USE [TEL_Event]
GO

/****** Object:  Table [dbo].[TEL_Event_RegisterModel2]    Script Date: 2022/1/19 下午 01:43:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TEL_Event_RegisterModel2](
	[id] [uniqueidentifier] NOT NULL,
	[eventid] [uniqueidentifier] NOT NULL,
	[empid] [nvarchar](64) NOT NULL,
	[registerdate] [datetime] NOT NULL,
	[selectedoption] [nvarchar](128) NOT NULL,
	[mobile] [nvarchar](10) NOT NULL,
	[traffic] [nvarchar](128) NOT NULL,
	[meal] [nvarchar](128) NOT NULL,
	[feedback] [nvarchar](max) NULL,
	[modifiedby] [nvarchar](64) NOT NULL,
	[modifieddate] [datetime] NOT NULL,
 CONSTRAINT [PK_TEL_Event_RegisterModel2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2', @level2type=N'COLUMN',@level2name=N'eventid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'員工編號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2', @level2type=N'COLUMN',@level2name=N'empid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'報名日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2', @level2type=N'COLUMN',@level2name=N'registerdate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'欲參加的內容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2', @level2type=N'COLUMN',@level2name=N'selectedoption'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手機號碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2', @level2type=N'COLUMN',@level2name=N'mobile'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交通車' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2', @level2type=N'COLUMN',@level2name=N'traffic'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'餐點內容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2', @level2type=N'COLUMN',@level2name=N'meal'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'意見/問題回饋' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2', @level2type=N'COLUMN',@level2name=N'feedback'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改人員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改日期時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

