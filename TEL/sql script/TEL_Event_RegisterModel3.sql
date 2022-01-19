USE [TEL_Event]
GO

/****** Object:  Table [dbo].[TEL_Event_RegisterModel3]    Script Date: 2022/1/19 下午 01:43:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TEL_Event_RegisterModel3](
	[id] [uniqueidentifier] NOT NULL,
	[eventid] [uniqueidentifier] NOT NULL,
	[empid] [nvarchar](64) NOT NULL,
	[registerdate] [datetime] NOT NULL,
	[examineeidentity] [nvarchar](8) NOT NULL,
	[examineename] [nvarchar](64) NOT NULL,
	[examineeidno] [nvarchar](10) NOT NULL,
	[examineebirthday] [datetime] NOT NULL,
	[examineemobile] [nvarchar](10) NOT NULL,
	[hosipital] [nvarchar](64) NOT NULL,
	[area] [nvarchar](64) NOT NULL,
	[solution] [nvarchar](256) NOT NULL,
	[gender] [nvarchar](64) NOT NULL,
	[expectdate] [datetime] NOT NULL,
	[seconddate] [datetime] NOT NULL,
	[secondsolution1] [nvarchar](256) NOT NULL,
	[secondsolution2] [nvarchar](256) NOT NULL,
	[secondsolution3] [nvarchar](256) NOT NULL,
	[optional] [nvarchar](512) NULL,
	[address] [nvarchar](256) NOT NULL,
	[meal] [nvarchar](64) NOT NULL,
	[feedback] [nvarchar](max) NULL,
	[modifiedby] [nvarchar](64) NOT NULL,
	[modifieddate] [datetime] NOT NULL,
 CONSTRAINT [PK_TEL_Event_RegisterModel3] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'eventid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'員工編號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'empid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'報名日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'registerdate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'受診者身分別' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'examineeidentity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'受診者中文姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'examineename'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'受診者身分證字號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'examineeidno'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'受診者出生年月日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'examineebirthday'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'受診者手機' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'examineemobile'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'健檢醫院' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'hosipital'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地區' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'area'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'費用&方案' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'solution'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'受診者性別' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'gender'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'期望受檢日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'expectdate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'備用受檢日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'seconddate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'健檢次方案1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'secondsolution1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'健檢次方案2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'secondsolution2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'健檢次方案3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'secondsolution3'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自費加選項目' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'optional'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'健檢包寄送地點' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'address'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'餐點樣式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'meal'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'意見/問題回饋' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'feedback'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改人員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改日期時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel3', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

