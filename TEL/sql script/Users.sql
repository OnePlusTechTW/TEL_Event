USE [TEL_Event]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 2022/3/9 下午 11:10:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 2022/3/9 下午 11:10:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserID] [nvarchar](64) NOT NULL,
	[EmpID] [nvarchar](64) NOT NULL,
	[FirstNameEN] [nvarchar](64) NOT NULL,
	[LastNameEN] [nvarchar](64) NOT NULL,
	[FirstNameCH] [nvarchar](64) NOT NULL,
	[LastNameCH] [nvarchar](64) NOT NULL,
	[Gender] [nvarchar](8) NOT NULL,
	[NationalID] [nvarchar](64) NOT NULL,
	[PassportID] [nvarchar](64) NOT NULL,
	[Birthday] [date] NULL,
	[Address] [nvarchar](512) NOT NULL,
	[UnitCode] [nvarchar](64) NOT NULL,
	[UnitName] [nvarchar](128) NOT NULL,
	[LeaderID] [nvarchar](64) NOT NULL,
	[TypeID] [nvarchar](8) NOT NULL,
	[TypeName] [nvarchar](64) NOT NULL,
	[EMail] [nvarchar](128) NOT NULL,
	[MobileNumber] [nvarchar](64) NOT NULL,
	[Level] [nvarchar](8) NOT NULL,
	[Band] [nvarchar](64) NOT NULL,
	[Title] [nvarchar](128) NOT NULL,
	[Station] [nvarchar](64) NOT NULL,
	[Mark] [nvarchar](64) NOT NULL,
	[Language] [nvarchar](64) NOT NULL,
	[AccountType] [nvarchar](64) NOT NULL,
	[ArrivalDate] [date] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

