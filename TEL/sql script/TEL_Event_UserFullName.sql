USE [TEL_Event]
GO

/****** Object:  View [dbo].[TEL_Event_UserFullName]    Script Date: 2022/3/9 下午 11:10:28 ******/
DROP VIEW [dbo].[TEL_Event_UserFullName]
GO

/****** Object:  View [dbo].[TEL_Event_UserFullName]    Script Date: 2022/3/9 下午 11:10:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create View  [dbo].[TEL_Event_UserFullName] as
select empid,FirstNameEN+' '+LastNameEN as FullnameEN, LastNameCH+FirstNameCH as FullnameCH
from Users
GO

