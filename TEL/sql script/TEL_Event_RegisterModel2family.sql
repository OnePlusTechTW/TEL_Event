USE [TEL_Event]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'meal'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'gender'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'birthday'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'idno'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'name'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'registerid'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'id'
GO

/****** Object:  Table [dbo].[TEL_Event_RegisterModel2family]    Script Date: 2022/3/9 下午 11:08:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEL_Event_RegisterModel2family]') AND type in (N'U'))
DROP TABLE [dbo].[TEL_Event_RegisterModel2family]
GO

/****** Object:  Table [dbo].[TEL_Event_RegisterModel2family]    Script Date: 2022/3/9 下午 11:08:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TEL_Event_RegisterModel2family](
	[id] [uniqueidentifier] NOT NULL,
	[registerid] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[idno] [nvarchar](10) NOT NULL,
	[birthday] [datetime] NOT NULL,
	[gender] [nvarchar](16) NOT NULL,
	[meal] [nvarchar](128) NOT NULL,
	[modifiedby] [nvarchar](64) NOT NULL,
	[modifieddate] [datetime] NOT NULL,
 CONSTRAINT [PK_TEL_Event_RegisterModel2family] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'報名辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'registerid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'家屬姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'家屬身分證字號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'idno'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'家屬出生年月日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'birthday'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'家屬性別' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'gender'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'家屬餐點內容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'meal'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改人員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改日期時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel2family', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

