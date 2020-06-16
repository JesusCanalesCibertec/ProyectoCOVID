alter table  [dbo].[UnidadesMast] add  [coeficiente] [int]

alter table  [dbo].[SY_REPORTE] add [TIPOREPORTE] [varchar](10) 
alter table  [dbo].[SY_REPORTE] add [CarpetaTemporal] [varchar](200)

alter table  [dbo].[ParametrosMast] add [EXPLICACIONADICIONAL] [varchar](100) 
alter table  [dbo].[ParametrosMast] add [TEXTO1] [varchar](100) 
alter table  [dbo].[ParametrosMast] add [TEXTO2] [varchar](100) 

alter table [dbo].[MA_MiscelaneosDetalle] add [Explicacion] [varchar](1000) 
alter table [dbo].[MA_MiscelaneosDetalle] add [orden] [int] NULL

alter table [dbo].[HR_GradoInstruccion] add [REPRESENTANTECARGO] [char](80) 
alter table [dbo].[HR_GradoInstruccion] add [FLAGTERMINO] [char](1) 


alter table [dbo].[HR_EncuestaSujeto] add [ORDEN] [int] NULL
alter table [dbo].[HR_EncuestaSujeto] add [ID_INSTITUCION] [varchar](5) 
alter table [dbo].[HR_EncuestaSujeto] add [ID_INSTITUCION_AREA] [varchar](5) 
alter table [dbo].[HR_EncuestaSujeto] add [ID_PROGRAMA] [varchar](4) 
alter table [dbo].[HR_EncuestaSujeto] add [ID_COMPONENTE] [varchar](4) 
alter table [dbo].[HR_EncuestaSujeto] add [MISCELANEO1] [varchar](10) 
alter table [dbo].[HR_EncuestaSujeto] add [MISCELANEO2] [varchar](10) 
alter table [dbo].[HR_EncuestaSujeto] add [MISCELANEO3] [varchar](10) 
alter table [dbo].[HR_EncuestaSujeto] add [MISCELANEO4] [varchar](10) 
alter table [dbo].[HR_EncuestaSujeto] add [numero] [int] NULL
alter table [dbo].[HR_EncuestaSujeto] add [AFE] [varchar](15) 
alter table [dbo].[HR_EncuestaSujeto] add [TIPO_GUIAIN] [varchar](1) 
alter table [dbo].[HR_EncuestaSujeto] ALTER COLUMN [valor] [varchar](50) 
 
alter table [dbo].[HR_EncuestaPreguntaValores] add [PESO] [decimal](6, 2) NULL


alter table [dbo].[HR_EncuestaPregunta] add [RequiereFlag] [varchar](1) 
alter table [dbo].[HR_EncuestaPregunta] add [RequierePregunta] [int] 
alter table [dbo].[HR_EncuestaPregunta] add [RequiereValor] [varchar](50) 
alter table [dbo].[HR_EncuestaPregunta] add [TipoEncuesta] [varchar](10) 

alter table [dbo].[HR_EncuestaDetalle] add [CompanyOwner] [varchar](8)
alter table [dbo].[HR_EncuestaDetalle] add [Periodo] [varchar](6) 
alter table [dbo].[HR_EncuestaDetalle] add [Orden] [int]
alter table [dbo].[HR_EncuestaDetalle] add [Ultimousuario] [varchar](6) 
alter table [dbo].[HR_EncuestaDetalle] add [Ultimafechamodif] [datetime]
alter table [dbo].[HR_EncuestaDetalle] add [ID_INDICADOR] [varchar](10) 
alter table [dbo].[HR_EncuestaDetalle] add [GRUPO] [varchar](10) 

alter table [dbo].[HR_Encuesta] add [ESTADO] [char](1) 
alter table [dbo].[HR_Encuesta] add [FECHAHASTA] [datetime] NULL
alter table [dbo].[HR_Encuesta] add [FECHADESDE] [datetime] NULL
alter table [dbo].[HR_Encuesta] add [ID_INSTITUCION] [varchar](5)
alter table [dbo].[HR_Encuesta] add [ID_INSTITUCION_AREA] [varchar](5)
alter table [dbo].[HR_Encuesta] add [ID_PROGRAMA] [varchar](4) 
alter table [dbo].[HR_Encuesta] add [ID_COMPONENTE] [varchar](4) 
alter table [dbo].[HR_Encuesta] add [ID_MISCELANEO_HEADER1] [varchar](10) 
alter table [dbo].[HR_Encuesta] add [ID_MISCELANEO_HEADER2] [varchar](10) 
alter table [dbo].[HR_Encuesta] add [ID_MISCELANEO_HEADER3] [varchar](10) 
alter table [dbo].[HR_Encuesta] add [ID_MISCELANEO_HEADER4] [varchar](10) 
alter table [dbo].[HR_Encuesta] add [TIPO] [varchar](8) 


alter table [dbo].[HR_EmpleadoCapacitacion] add [COMPANIA_EMPLEADO] [varchar](10)
alter table [dbo].[HR_EmpleadoCapacitacion] add [FLAGASISTIO] [char](1) 
alter table [dbo].[HR_EmpleadoCapacitacion] add [FLAG_APROBO] [char](1) 
alter table [dbo].[HR_EmpleadoCapacitacion] add [FLAG_PARTICIPATIVO] [char](1)
alter table [dbo].[HR_EmpleadoCapacitacion] add [FLAG_ASISTIO] [char](1) 
alter table [dbo].[HR_EmpleadoCapacitacion] add [MOTIVO_INASISTENCIA] [varchar](500)


alter table [dbo].[HR_CentroEstudios] add [estado] [char](1)


alter table [dbo].[HR_Capacitacion] add [ASIGNADO] [varchar](10) 
alter table [dbo].[HR_Capacitacion] add [CERTIFICADO] [varchar](10) 
alter table [dbo].[HR_Capacitacion] add [FINANCIADOPS] [varchar](10) 
alter table [dbo].[HR_Capacitacion] add [ID_INSTITUCION] [varchar](10) 


alter table [dbo].[EmpleadoMast] add [CorreoExterno] [varchar](200)
