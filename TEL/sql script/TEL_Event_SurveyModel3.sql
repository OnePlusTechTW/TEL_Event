USE [TEL_Event]
GO

/****** Object:  Table [dbo].[TEL_Event_SurveyModel3]    Script Date: 2022/1/19 下午 01:47:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TEL_Event_SurveyModel3](
	[id] [uniqueidentifier] NOT NULL,
	[eventid] [uniqueidentifier] NOT NULL,
	[empid] [nvarchar](64) NOT NULL,
	[fillindate] [datetime] NOT NULL,
	[q1] [nvarchar](64) NOT NULL,
	[q2] [nvarchar](32) NOT NULL,
	[q2reason] [nvarchar](max) NULL,
	[q3] [nvarchar](32) NOT NULL,
	[q3reason] [nvarchar](max) NULL,
	[q4] [nvarchar](32) NOT NULL,
	[q4reason] [nvarchar](max) NULL,
	[q5] [nvarchar](32) NOT NULL,
	[q5reason] [nvarchar](max) NULL,
	[q6] [nvarchar](32) NOT NULL,
	[q6reason] [nvarchar](max) NULL,
	[q7] [nvarchar](32) NOT NULL,
	[q7reason] [nvarchar](max) NULL,
	[q8] [nvarchar](max) NOT NULL,
	[q9] [nvarchar](32) NOT NULL,
	[modifiedby] [nvarchar](64) NOT NULL,
	[modifieddate] [datetime] NOT NULL,
 CONSTRAINT [PK_TEL_Event_SurveyModel3] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活動辨識碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'eventid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'員工編號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'empid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'填寫日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'fillindate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'健檢流程順暢度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'q4'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'健檢流程順暢度-填答不滿意，請說明原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'q4reason'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'整體環境滿意度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'q5'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'整體環境滿意度-填答不滿意，請說明原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'q5reason'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'餐點滿意度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'q6'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'餐點滿意度-填答不滿意，請說明原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'q6reason'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'健檢中心整體滿意度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'q7'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'健檢中心整體滿意度-填答不滿意，請說明原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'q7reason'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'關於公司提供的年度健檢安排，您是否有其他的建議' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'q8'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'請問是否需要安排社內駐廠醫師提供健檢報告解說' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'q9'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改人員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'modifiedby'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最後修改日期時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TEL_Event_SurveyModel3', @level2type=N'COLUMN',@level2name=N'modifieddate'
GO

