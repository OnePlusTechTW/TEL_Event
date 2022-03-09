USE [TEL_Event]
GO

/****** Object:  Table [dbo].[MailGroup]    Script Date: 2022/3/9 下午 11:05:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MailGroup]') AND type in (N'U'))
DROP TABLE [dbo].[MailGroup]
GO

/****** Object:  Table [dbo].[MailGroup]    Script Date: 2022/3/9 下午 11:05:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MailGroup](
	[UID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[EmpID] [nvarchar](20) NULL
) ON [PRIMARY]
GO

