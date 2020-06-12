/****** Object:  Table [dbo].[SY_ReporteArchivo]    Script Date: 7/01/2019 14:14:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SY_ReporteArchivo](
	[AplicacionCodigo] [varchar](2) NOT NULL,
	[ReporteCodigo] [varchar](3) NOT NULL,
	[CompaniaSocio] [varchar](8) NOT NULL,
	[Periodo] [varchar](6) NOT NULL,
	[Version] [varchar](6) NOT NULL,
	[Reporte] [varbinary](max) NULL,
	[ESTADO] [varchar](1) NULL,
	[ULTIMOUSUARIO] [varchar](50) NULL,
	[ULTIMAFECHAMODIF] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


