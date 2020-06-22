/****** Object:  Table [dbo].[TRIAJE]    Script Date: 21/06/2020 19:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRIAJE](
	[COD_TRIAJE] [int] NOT NULL,
	[COD_CIUDADANO] [int] NULL,
	[DIS_GUS] [varchar](2) NULL,
	[TOS] [varchar](2) NULL,
	[DOLOR] [varchar](2) NULL,
	[DIFI] [varchar](2) NULL,
	[NASAL] [varchar](2) NULL,
	[FIEBRE] [varchar](2) NULL,
	[FECHA_INICIO] [date] NULL,
	[SITUACION1] [varchar](2) NULL,
	[SITUACION2] [varchar](2) NULL,
	[SITUACION3] [varchar](2) NULL,
	[OBESIDAD] [varchar](2) NULL,
	[PULMONAR] [varchar](2) NULL,
	[ASMA] [varchar](2) NULL,
	[DIABETES] [varchar](2) NULL,
	[HIPERTENSION] [varchar](2) NULL,
	[CARDIO] [varchar](2) NULL,
	[RENAL] [varchar](2) NULL,
	[CANCEL] [varchar](2) NULL,
	[ESTADO] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[COD_TRIAJE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (1, 1, N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', CAST(N'2020-02-03' AS Date), N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (2, 2, N'NO', N'NO', N'NO', N'NO', N'NO', N'si', CAST(N'2020-02-01' AS Date), N'NO', N'NO', N'NO', N'NO', N'NO', N'NO', N'NO', N'NO', N'NO', N'NO', N'NO', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (3, 3, N'NO', N'SI', N'SI', N'SI', N'NO', N'SI', CAST(N'2020-02-02' AS Date), N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (4, 4, N'SI', N'NO', N'SI', N'NO', N'SI', N'NO', CAST(N'2020-03-03' AS Date), N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (5, 5, N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', CAST(N'2020-05-05' AS Date), N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (6, 6, N'SI', N'NO', N'SI', N'NO', N'SI', N'NO', CAST(N'2020-01-10' AS Date), N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (7, 7, N'NO', N'SI', N'SI', N'SI', N'NO', N'SI', CAST(N'2020-05-20' AS Date), N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (8, 8, N'SI', N'SI', N'SI', N'NO', N'SI', N'NO', CAST(N'2020-05-15' AS Date), N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (9, 9, N'SI', N'SI', N'NO', N'SI', N'SI', N'NO', CAST(N'2020-03-03' AS Date), N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (10, 10, N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', CAST(N'2020-01-06' AS Date), N'NO', N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (11, 11, N'NO', N'SI', N'NO', N'NO', N'NO', N'NO', CAST(N'2020-02-01' AS Date), N'NO', N'NO', N'NO', N'NO', N'NO', N'NO', N'NO', N'NO', N'NO', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (12, 12, N'SI', N'SI', N'NO', N'SI', N'NO', N'NO', CAST(N'2020-04-06' AS Date), N'SI', N'SI', N'NO', N'SI', N'NO', N'NO', N'NO', N'SI', N'NO', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (13, 2, N'SI', N'NO', N'NO', N'SI', N'NO', N'NO', CAST(N'2020-04-12' AS Date), N'SI', N'SI', N'NO', N'NO', N'NO', N'NO', N'NO', N'SI', N'NO', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (14, 3, N'SI', N'SI', N'NO', N'SI', N'NO', N'NO', CAST(N'2020-02-13' AS Date), N'SI', N'SI', N'NO', N'SI', N'NO', N'NO', N'NO', N'SI', N'NO', N'SI', N'NO', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (15, 4, N'SI', N'SI', N'NO', N'NO', N'NO', N'NO', CAST(N'2020-04-14' AS Date), N'NO', N'SI', N'NO', N'NO', N'NO', N'NO', N'NO', N'SI', N'NO', N'NO', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (16, 5, N'SI', N'SI', N'NO', N'SI', N'NO', N'NO', CAST(N'2020-05-06' AS Date), N'SI', N'SI', N'NO', N'NO', N'SI', N'NO', N'NO', N'SI', N'NO', N'SI', N'NO', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (17, 6, N'SI', N'SI', N'NO', N'SI', N'SI', N'NO', CAST(N'2020-01-07' AS Date), N'SI', N'SI', N'NO', N'SI', N'NO', N'NO', N'NO', N'SI', N'NO', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (18, 7, N'SI', N'NO', N'NO', N'SI', N'SI', N'NO', CAST(N'2020-01-20' AS Date), N'SI', N'SI', N'NO', N'SI', N'NO', N'NO', N'NO', N'SI', N'NO', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (19, 8, N'SI', N'SI', N'NO', N'SI', N'SI', N'NO', CAST(N'2020-04-10' AS Date), N'NO', N'NO', N'NO', N'SI', N'NO', N'NO', N'NO', N'NO', N'NO', N'NO', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (20, 5, N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', CAST(N'2020-05-09' AS Date), N'SI', N'NO', N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'NO', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (21, 5, N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', CAST(N'2020-02-18' AS Date), N'SI', N'SI', N'NO', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (22, 2, N'SI', N'SI', N'SI', N'SI', N'SI', N'NO', CAST(N'2020-02-16' AS Date), N'SI', N'SI', N'NO', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (23, 4, N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', CAST(N'2020-02-15' AS Date), N'SI', N'NO', N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (24, 12, N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', CAST(N'2020-02-16' AS Date), N'NO', N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (25, 14, N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', CAST(N'2020-04-16' AS Date), N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (26, 10, N'SI', N'SI', N'SI', N'NO', N'SI', N'NO', CAST(N'2020-05-03' AS Date), N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (27, 11, N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', CAST(N'2020-02-15' AS Date), N'SI', N'NO', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (28, 9, N'SI', N'SI', N'SI', N'NO', N'SI', N'NO', CAST(N'2020-04-19' AS Date), N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (29, 8, N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', CAST(N'2020-05-03' AS Date), N'NO', N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'NO', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (30, 5, N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', CAST(N'2020-02-01' AS Date), N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (31, 6, N'NO', N'SI', N'SI', N'NO', N'SI', N'NO', CAST(N'2020-02-05' AS Date), N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', 1)
INSERT [dbo].[TRIAJE] ([COD_TRIAJE], [COD_CIUDADANO], [DIS_GUS], [TOS], [DOLOR], [DIFI], [NASAL], [FIEBRE], [FECHA_INICIO], [SITUACION1], [SITUACION2], [SITUACION3], [OBESIDAD], [PULMONAR], [ASMA], [DIABETES], [HIPERTENSION], [CARDIO], [RENAL], [CANCEL], [ESTADO]) VALUES (32, 3, N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', CAST(N'2020-05-06' AS Date), N'SI', N'SI', N'NO', N'SI', N'SI', N'SI', N'SI', N'SI', N'NO', N'SI', N'SI', 1)
