USE [TEL_Event]
GO

/****** Object:  Table [dbo].[TEL_Event_RegisterOption6]    Script Date: 2022/1/19 下午 01:45:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TEL_Event_RegisterOption6](
	[id] [uniqueidentifier] NOT NULL,
	[eventid] [uniqueidentifier] NOT NULL,
	[area] [nvarchar](256) NOT NULL,
	[avaliabledate] [datetime] NOT NULL,
	[limit] [int] NOT NULL,
	[modifiedby] [nvarchar](64) NOT NULL,
	[modifieddate] [datetime] NOT NULL,
 CONSTRAINT [PK_TEL_Event_RegisterOption6] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption6', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption6', @level2type=N'COLUMN',@level2name=N'eventid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地區' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption6', @level2type=N'COLUMN',@level2name=N'area'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption6', @level2type=N'COLUMN',@level2name=N'avaliabledate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'人數上限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption6', @level2type=N'COLUMN',@level2name=N'limit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改人員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption6', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改日期時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterOption6', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

