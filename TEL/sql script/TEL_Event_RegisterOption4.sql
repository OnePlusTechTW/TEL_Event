USE [TEL_Event]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'limit'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'avaliabledate'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'secondoption3'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'secondoption2'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'secondoption1'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'gender'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'description'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'area'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'hosipital'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'eventid'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'id'
GO

/****** Object:  Table [dbo].[TEL_Event_RegisterOption4]    Script Date: 2022/3/9 下午 11:09:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEL_Event_RegisterOption4]') AND type in (N'U'))
DROP TABLE [dbo].[TEL_Event_RegisterOption4]
GO

/****** Object:  Table [dbo].[TEL_Event_RegisterOption4]    Script Date: 2022/3/9 下午 11:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TEL_Event_RegisterOption4](
	[id] [uniqueidentifier] NOT NULL,
	[eventid] [uniqueidentifier] NOT NULL,
	[hosipital] [nvarchar](64) NOT NULL,
	[area] [nvarchar](64) NOT NULL,
	[description] [nvarchar](256) NOT NULL,
	[gender] [nvarchar](64) NOT NULL,
	[secondoption1] [nvarchar](256) NULL,
	[secondoption2] [nvarchar](256) NULL,
	[secondoption3] [nvarchar](256) NULL,
	[avaliabledate] [datetime] NOT NULL,
	[limit] [int] NOT NULL,
	[modifiedby] [nvarchar](64) NOT NULL,
	[modifieddate] [datetime] NOT NULL,
 CONSTRAINT [PK_TEL_Event_RegisterOption4] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'eventid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'醫院' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'hosipital'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地區' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'area'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'費用&方案' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性別' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'gender'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'次方案1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'secondoption1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'次方案2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'secondoption2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'次方案3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'secondoption3'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'avaliabledate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'人數上限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'limit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改人員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改日期時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption4', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

