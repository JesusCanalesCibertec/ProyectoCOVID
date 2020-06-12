/****** Object:  Table [dbo].[HR_Capacitacion_Empleado]    Script Date: 7/01/2019 14:14:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HR_Capacitacion_Empleado](
	[CompanyOwner] [varchar](8) NOT NULL,
	[Capacitacion] [varchar](10) NOT NULL,
	[ID_INSTITUCION] [varchar](5) NOT NULL,
	[ID_EMPLEADO] [int] NOT NULL,
	[UltimoUsuario] [char](20) NULL,
	[UltimaFechaModif] [datetime] NULL,
	[Calificacion] [char](20) NULL,
	[Comentario] [varchar](150) NULL,
	[FLAG_APROBO] [char](1) NULL,
	[FLAG_PARTICIPATIVO] [char](1) NULL,
	[FLAG_ASISTIO] [char](1) NULL,
	[MOTIVO_INASISTENCIA] [varchar](500) NULL,
	[ID_RENDIEMIENTO] [varchar](10) NULL,
	[ID_PARTICIPACION] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyOwner] ASC,
	[Capacitacion] ASC,
	[ID_INSTITUCION] ASC,
	[ID_EMPLEADO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


