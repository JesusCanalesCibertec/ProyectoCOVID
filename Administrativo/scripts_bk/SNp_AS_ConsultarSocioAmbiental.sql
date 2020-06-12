
ALTER  PROCEDURE [sgseguridadsys].[SNp_AS_ConsultarSocioAmbiental]
(
@pNombreCompleto varchar(200),
@pPeriodo varchar(8) ,
@pArea varchar(10),
@pSexo varchar(1),
@pNumPag int,
@pNumReg int,
@pInstitucion varchar(6),
@pPrograma varchar(3)
)
AS


-- exec [sgseguridadsys].[SNp_AS_ConsultarSocioAmbiental] null, '2018S1',null,null,0,20,'CARID','AAM'

BEGIN

  --tabla para paginar 
	create table #tmp_socio_ambiental
	(
	[ID_ROW] [int] NOT NULL,
	[ID_INSTITUCION] [varchar](5)  NULL,
	[ID_BENEFICIARIO] [int]  NULL,
	[ID_SOCIO_AMBIENTAL] [int]  NULL,
	[ID_PERIODO] [varchar](6)  NULL,
	[FECHA_INFORME] [datetime] NULL,
	[ID_CONFLICTOS] [varchar](5) NULL,
	[ID_COMUNICACION] [varchar](5) NULL,
	[ID_EMOCIONAL] [varchar](5) NULL,
	[ID_COOPERACION] [varchar](5) NULL,
	[ID_ASERTAVIDAD] [varchar](5) NULL,
	[ID_EMPATIA] [varchar](5) NULL,
	[ID_INTEGRACION] [varchar](5) NULL,
	[ID_PARTICIPACION] [varchar](5) NULL,
	[COMENTARIOS] [varchar](1000) NULL,
	[ESTADO] [varchar](1) NULL,
	[NOMBRECOMPLETO] [varchar](200)  NULL,
	[CONTADOR] [int] NULL,

	[NOMBRE_CONFLICTOS] [varchar](100) NULL,
	[NOMBRE_COMUNICACION] [varchar](100) NULL,
	[NOMBRE_EMOCIONAL] [varchar](100) NULL,
	[NOMBRE_COOPERACION] [varchar](100) NULL,
	[NOMBRE_ASERTAVIDAD] [varchar](100) NULL,
	[NOMBRE_EMPATIA] [varchar](100) NULL,
	[NOMBRE_INTEGRACION] [varchar](100) NULL,
	[NOMBRE_PARTICIPACION] [varchar](100) NULL,
	[EVALUADO] [varchar](100) NULL
	)

	INSERT INTO #tmp_socio_ambiental
	(
	[ID_ROW] ,
	[ID_INSTITUCION],
	[ID_BENEFICIARIO],
	[FECHA_INFORME],
	[ID_SOCIO_AMBIENTAL],
	[ID_PERIODO],
	[ID_CONFLICTOS],
	[ID_COMUNICACION],
	[ID_EMOCIONAL] ,
	[ID_COOPERACION] ,
	[ID_ASERTAVIDAD],
	[ID_EMPATIA],
	[ID_INTEGRACION],
	[ID_PARTICIPACION],
	[COMENTARIOS] ,
	[ESTADO],
	[NOMBRECOMPLETO] ,
	[CONTADOR],

	[NOMBRE_CONFLICTOS],
	[NOMBRE_COMUNICACION],
	[NOMBRE_EMOCIONAL],
	[NOMBRE_COOPERACION],
	[NOMBRE_ASERTAVIDAD],
	[NOMBRE_EMPATIA],
	[NOMBRE_INTEGRACION],
	[NOMBRE_PARTICIPACION],
	[EVALUADO] 
	)
  
 SELECT 
	ROW_NUMBER() OVER(ORDER BY reg.ID_BENEFICIARIO ASC) AS sec,
	reg.ID_INSTITUCION,
    reg.ID_BENEFICIARIO,
	reg.FECHA_INFORME,
    reg.ID_SOCIO_AMBIENTAL,
    reg.ID_PERIODO,
    reg.ID_CONFLICTOS,
    reg.ID_COMUNICACION,
    reg.ID_EMOCIONAL,
    reg.ID_COOPERACION,
    reg.ID_ASERTAVIDAD,
    reg.ID_EMPATIA,
    reg.ID_INTEGRACION,
    reg.ID_PARTICIPACION,
    reg.COMENTARIOS,
    reg.ESTADO,
	reg.NOMBRECOMPLETO,
	
	(
	SELECT COUNT(1) FROM(
	select
     socioAmb.ID_BENEFICIARIO
	from sgseguridadsys.PS_SOCIO_AMBIENTAL socioAmb
    JOIN sgseguridadsys.PS_BENEFICIARIO bene ON bene.ID_BENEFICIARIO=socioAmb.ID_BENEFICIARIO
	 AND bene.ID_INSTITUCION=socioAmb.ID_INSTITUCION
    JOIN sgseguridadsys.PS_ENTIDAD entidad ON entidad.ID_ENTIDAD=bene.ID_BENEFICIARIO
    WHERE
    socioAmb.ID_PERIODO=@pPeriodo
    AND socioAmb.ID_INSTITUCION=ISNULL(@pInstitucion,socioAmb.ID_INSTITUCION)
    AND ISNULL(bene.id_area,'XX')=ISNULL(@pArea,ISNULL(bene.id_area,'XX'))
    AND ISNULL(entidad.ID_SEXO,'XX')=ISNULL(@pSexo,ISNULL(entidad.ID_SEXO,'XX'))
    AND entidad.NOMBRECOMPLETO LIKE  '%' + COALESCE(@pNombreCompleto, entidad.NOMBRECOMPLETO) + '%'
	AND bene.ID_PROGRAMA=@pPrograma
      ) reg
	)as contador,

	reg.conflictos,
	reg.comunicacion,
	reg.emocional,
	reg.cooperacion,
	reg.asertividad,
	reg.empatia,
	reg.integracion,
	reg.participacion,
	reg.EVALUADO
  

	FROM(

	select
    socioAmb.ID_INSTITUCION,
    socioAmb.ID_BENEFICIARIO,
	socioAmb.FECHA_INFORME,
    socioAmb.ID_SOCIO_AMBIENTAL,
    socioAmb.ID_PERIODO,
    socioAmb.ID_CONFLICTOS,
    socioAmb.ID_COMUNICACION,
    socioAmb.ID_EMOCIONAL,
    socioAmb.ID_COOPERACION,
    socioAmb.ID_ASERTAVIDAD,
    socioAmb.ID_EMPATIA,
    socioAmb.ID_INTEGRACION,
    socioAmb.ID_PARTICIPACION,
    socioAmb.COMENTARIOS,
    socioAmb.ESTADO,
	entidad.NOMBRECOMPLETO,

	sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('HABISOCIAL',socioAmb.ID_CONFLICTOS) as conflictos,
	sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('HABISOCIAL',socioAmb.ID_COMUNICACION) as comunicacion,
	sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('HABISOCIAL',socioAmb.ID_EMOCIONAL) as emocional,
	sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('HABISOCIAL',socioAmb.ID_COOPERACION) as cooperacion,
	sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('HABISOCIAL',socioAmb.ID_ASERTAVIDAD) as asertividad,
	sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('HABISOCIAL',socioAmb.ID_EMPATIA) as empatia,
	sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('HABISOCIAL',socioAmb.ID_INTEGRACION) as integracion,
	sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('HABISOCIAL',socioAmb.ID_PARTICIPACION) as participacion,
	socioAmb.EVALUADO

    from sgseguridadsys.PS_SOCIO_AMBIENTAL socioAmb
    JOIN sgseguridadsys.PS_BENEFICIARIO bene ON bene.ID_BENEFICIARIO=socioAmb.ID_BENEFICIARIO
	 AND bene.ID_INSTITUCION=socioAmb.ID_INSTITUCION
    JOIN sgseguridadsys.PS_ENTIDAD entidad ON entidad.ID_ENTIDAD=bene.ID_BENEFICIARIO
    WHERE
    socioAmb.ID_PERIODO=@pPeriodo
    AND socioAmb.ID_INSTITUCION=ISNULL(@pInstitucion,socioAmb.ID_INSTITUCION)
    AND ISNULL(bene.id_area,'XX')=ISNULL(@pArea,ISNULL(bene.id_area,'XX'))
    AND ISNULL(entidad.ID_SEXO,'XX')=ISNULL(@pSexo,ISNULL(entidad.ID_SEXO,'XX'))
    AND entidad.NOMBRECOMPLETO LIKE  '%' + COALESCE(@pNombreCompleto, entidad.NOMBRECOMPLETO) + '%'
	AND bene.ID_PROGRAMA=@pPrograma
	AND bene.ESTADO='ACT'
	) reg


		SELECT 
	
		temp.ID_INSTITUCION,
		temp.ID_BENEFICIARIO,
		temp.ID_SOCIO_AMBIENTAL,
		temp.ID_PERIODO,
		temp.FECHA_INFORME,
		temp.ID_CONFLICTOS,
		temp.ID_COMUNICACION,
		temp.ID_EMOCIONAL,
		temp.ID_COOPERACION,
		temp.ID_ASERTAVIDAD,
		temp.ID_EMPATIA,
		temp.ID_INTEGRACION,
		temp.ID_PARTICIPACION,
		temp.COMENTARIOS,
		temp.ESTADO,
		temp.NOMBRECOMPLETO,
		temp.CONTADOR,
		
		temp.[NOMBRE_CONFLICTOS],
		temp.[NOMBRE_COMUNICACION],
		temp.[NOMBRE_EMOCIONAL],
		temp.[NOMBRE_COOPERACION],
		temp.[NOMBRE_ASERTAVIDAD],
		temp.[NOMBRE_EMPATIA],
		temp.[NOMBRE_INTEGRACION],
		temp.[NOMBRE_PARTICIPACION],
		temp.[EVALUADO] 
	FROM #tmp_socio_ambiental temp
	WHERE  temp.ID_ROW BETWEEN  (@pNumPag + 1)  AND 
					(@pNumPag + @pNumReg) 

END