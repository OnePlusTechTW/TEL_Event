USE [TEL_Event]
GO

/****** Object:  Table [dbo].[Log]    Script Date: 2022/3/9 下午 11:05:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Log]') AND type in (N'U'))
DROP TABLE [dbo].[Log]
GO

/****** Object:  Table [dbo].[Log]    Script Date: 2022/3/9 下午 11:05:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Log](
	[Description] [nvarchar](256) NOT NULL,
	[RelatedID] [uniqueidentifier] NOT NULL,
	[SQLStr] [nvarchar](2000) NOT NULL,
	[Editer] [nvarchar](50) NOT NULL,
	[EditDate] [datetime] NOT NULL
) ON [PRIMARY]
GO

