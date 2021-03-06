USE [TEL_Event]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'feedback'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'description3'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment3_name'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment3'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'description2'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment2_name'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment2'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'description1'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment1_name'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment1'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'registerdate'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'empid'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'eventid'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'id'
GO

/****** Object:  Table [dbo].[TEL_Event_RegisterModel5]    Script Date: 2022/3/9 ?????? 11:08:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEL_Event_RegisterModel5]') AND type in (N'U'))
DROP TABLE [dbo].[TEL_Event_RegisterModel5]
GO

/****** Object:  Table [dbo].[TEL_Event_RegisterModel5]    Script Date: 2022/3/9 ?????? 11:08:31 ******/
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
	[attachment1_name] [nvarchar](256) NOT NULL,
	[description1] [nvarchar](512) NULL,
	[attachment2] [nvarchar](256) NULL,
	[attachment2_name] [nvarchar](256) NULL,
	[description2] [nvarchar](512) NULL,
	[attachment3] [nvarchar](256) NULL,
	[attachment3_name] [nvarchar](256) NULL,
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

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'???????????????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'???????????????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'eventid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'empid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'registerdate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????1??????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment1_name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????1?????????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'description1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????2??????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment2_name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????2?????????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'description2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment3'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????3??????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'attachment3_name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????3?????????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'description3'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'??????/????????????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'feedback'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'??????????????????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'????????????????????????' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_RegisterModel5', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

