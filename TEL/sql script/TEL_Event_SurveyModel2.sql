USE [TEL_Event]
GO

/****** Object:  Table [dbo].[TEL_Event_SurveyModel2]    Script Date: 2022/1/19 下午 01:46:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TEL_Event_SurveyModel2](
	[id] [uniqueidentifier] NOT NULL,
	[eventid] [uniqueidentifier] NOT NULL,
	[empid] [nvarchar](64) NOT NULL,
	[fillindate] [datetime] NOT NULL,
	[q1] [nvarchar](32) NOT NULL,
	[q1other] [nvarchar](64) NULL,
	[q2] [nvarchar](32) NOT NULL,
	[q3] [nvarchar](32) NOT NULL,
	[q4] [nvarchar](32) NOT NULL,
	[q5] [nvarchar](32) NOT NULL,
	[q6] [nvarchar](32) NOT NULL,
	[q7] [nvarchar](32) NULL,
	[q8] [nvarchar](32) NULL,
	[modifiedby] [nvarchar](64) NOT NULL,
	[modifieddate] [datetime] NOT NULL,
 CONSTRAINT [PK_TEL_Event_SurveyModel2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'eventid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'員工編號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'empid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'填寫日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'fillindate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'您如何得知此活動' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'q1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'您如何得知此講座課程-自填' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'q1other'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'對於活動的流程之安排' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'q2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'對於活動的內容之安排' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'q3'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'對於場地的規劃與安排' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'q4'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'對於時間的規劃與安排' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'q5'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'整體而言，對本次活動滿意程度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'q6'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建議與想法' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'q7'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有推薦公司舉辦的活動' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'q8'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改人員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改日期時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel2', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

