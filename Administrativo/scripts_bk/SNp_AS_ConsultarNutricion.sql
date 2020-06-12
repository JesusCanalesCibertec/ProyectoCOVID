ALTER  PROCEDURE [sgseguridadsys].[SNp_AS_ConsultarNutricion]
(
@pNombreCompleto varchar(200),
@pPeriodo varchar(8) ,
@pTipoEdad varchar(10),
@pArea varchar(10),
@pSexo varchar(1),
@pNumPag int,
@pNumReg int,
@pInstitucion varchar(6),
@pPrograma varchar(3)
)
AS

DECLARE @EdadInicio INT,
        @EdadFin INT

-- exec [sgseguridadsys].[SNp_AS_ConsultarNutricion] null, '2018S2',null,null,null,0,20,'CARID','AAM'
BEGIN
      SET @EdadInicio=null
	  SET @EdadFin=null
  
    IF @pTipoEdad='NUTR2'
	   BEGIN
		   SET @EdadInicio=0
		   SET @EdadFin=2
		END

	IF @pTipoEdad='NUTRX'
	   BEGIN
		   SET @EdadInicio=2
		   SET @EdadFin=20
		END

	IF @pTipoEdad='NUTR3'
	   BEGIN
		   SET @EdadInicio=50
		   SET @EdadFin=200
		END

	

  --tabla para paginar 
	create table #tmp_nutricion
	(
	[ID_ROW] [int] NOT NULL,
	[ID_BENEFICIARIO] [int] NOT NULL,
	[NOMBRECOMPLETO] [varchar](200)  NULL,
	[FECHA_INFORME] [datetime] NULL,
	[PESO] [NUMERIC](19,2) NULL,
	[TALLA] [NUMERIC](19,2) NULL,
	[PESO_EDAD] [varchar](200)  NULL,
	[TALLA_EDAD][varchar](200)  NULL,
	[PESO_TALLA] [varchar](200)  NULL,
	[IMC] [varchar](200)  NULL,
	[ID_VALORACION] [varchar](4)  NULL,
	[ID_TIPO_DIETA] [varchar](5) NULL,
	[TIPO_DIETA_POR_DIA] [int]  NULL,
	[ID_SUPLEMENTO_NUTRICIONAL] [varchar](4)  NULL,
	[SUPLEMENTO_NUTRICIONAL_POR_DIA] [int]  NULL,
	[COMENTARIOS] [varchar](1000)  NULL,
	[ID_NUTRICION] [int]  NULL,
	[ID_INSTITUCION] [varchar](5)  NULL,
	[ESTADO] [varchar](1) NULL,
	[ID_PERIODO] [varchar](6)  NULL,
	[ID_PERIMETRO_PIERNA] [varchar](4) NULL,
	[ID_PRESION_MENSUAL] [varchar](4) NULL,
	[VARIACION_PESO] [NUMERIC](19,2) NULL,
	[CONTADOR] [int] NULL,

	[NOMBRE_VALORACION_ADULTOS] [varchar](100) NULL,
	[NOMBRE_TIPO_DIETA_ADULTOS] [varchar](100) NULL,
	[NOMBRE_VALORACION_NINOS] [varchar](100) NULL,
	[NOMBRE_TIPO_DIETA_NINOS] [varchar](100) NULL,
	[NOMBRE_SUPLEMENTO] [varchar](100) NULL,
	[NOMBRE_PERIMETRO] [varchar](100) NULL,
	[NOMBRE_PRESION] [varchar](100) NULL,
	[EVALUADO] [varchar](100) NULL,
	)

	INSERT INTO #tmp_nutricion
	(
	    ID_ROW,
	    ID_BENEFICIARIO,
		NOMBRECOMPLETO,
		FECHA_INFORME,
		PESO,
		TALLA,
		PESO_EDAD,
		TALLA_EDAD,
		PESO_TALLA,
		IMC,
		ID_VALORACION,
		ID_TIPO_DIETA,
		TIPO_DIETA_POR_DIA,
		ID_SUPLEMENTO_NUTRICIONAL,
		SUPLEMENTO_NUTRICIONAL_POR_DIA,
		COMENTARIOS,
		ID_NUTRICION,
		ID_INSTITUCION,
		estado,
		ID_PERIODO,
		ID_PERIMETRO_PIERNA,
		ID_PRESION_MENSUAL,
		VARIACION_PESO,
		CONTADOR,
		[NOMBRE_VALORACION_ADULTOS],
		[NOMBRE_TIPO_DIETA_ADULTOS] ,
		[NOMBRE_VALORACION_NINOS],
		[NOMBRE_TIPO_DIETA_NINOS] ,
		[NOMBRE_SUPLEMENTO] ,
		[NOMBRE_PERIMETRO] ,
		[NOMBRE_PRESION] ,
		[EVALUADO]
	)

	SELECT 
	 ROW_NUMBER() OVER(ORDER BY reg.ID_BENEFICIARIO ASC) AS sec,
    reg.ID_BENEFICIARIO,
    reg.NOMBRECOMPLETO,
    reg.FECHA_INFORME,
    reg.PESO,
    reg.TALLA,
    reg.PESO_EDAD,
    reg.TALLA_EDAD,
    reg.PESO_TALLA,
    reg.IMC,
    reg.ID_VALORACION,
	reg.ID_TIPO_DIETA,
    reg.TIPO_DIETA_POR_DIA,
    reg.ID_SUPLEMENTO_NUTRICIONAL,
    reg.SUPLEMENTO_NUTRICIONAL_POR_DIA,
    reg.COMENTARIOS,
    reg.ID_NUTRICION,
    reg.ID_INSTITUCION,
    reg.estado,
    reg.ID_PERIODO,
	reg.ID_PERIMETRO_PIERNA,
	reg.ID_PRESION_MENSUAL,
	reg.VARIACION_PESO,
	(
	SELECT 
	COUNT(1)
	FROM (
  select  
    bene.ID_BENEFICIARIO
   from sgseguridadsys.PS_BENEFICIARIO bene
    JOIN sgseguridadsys.PS_ENTIDAD entidad ON entidad.ID_ENTIDAD=bene.ID_BENEFICIARIO
    JOIN sgseguridadsys.PS_NUTRICION nutricion ON nutricion.ID_BENEFICIARIO=bene.ID_BENEFICIARIO
    AND nutricion.ID_INSTITUCION=bene.ID_INSTITUCION
    WHERE nutricion.ID_PERIODO=@pPeriodo
	AND ISNULL(bene.id_area,'XX')=ISNULL(@pArea,ISNULL(bene.id_area,'XX'))
	AND ISNULL(entidad.ID_SEXO,'XX')=ISNULL(@pSexo,ISNULL(entidad.ID_SEXO,'XX'))
	and ISNULL(entidad.NombreCompleto,'XX') LIKE '%' + ISNULL( @pNombreCompleto,ISNULL(entidad.NombreCompleto,'XX')) + '%'
	AND nutricion.ID_INSTITUCION=@pInstitucion
	AND ISNULL(entidad.EDAD,0) between ISNULL(@EdadInicio,0) AND ISNULL(@EdadFin,200)
	AND bene.ID_PROGRAMA=@pPrograma
	AND bene.ESTADO='ACT'
   ) record) as cantidad,

        reg.[NOMBRE_VALORACION_ADULTOS],
		reg.[NOMBRE_TIPO_DIETA_ADULTOS] ,
		 reg.[NOMBRE_VALORACION_NINOS],
		reg.[NOMBRE_TIPO_DIETA_NINOS] ,
		reg.[NOMBRE_SUPLEMENTO] ,
		reg.[NOMBRE_PERIMETRO] ,
		reg.[NOMBRE_PRESION],
		reg.EVALUADO

	FROM (
 
  select  
    bene.ID_BENEFICIARIO,
    entidad.NOMBRECOMPLETO,
    nutricion.FECHA_INFORME,
    nutricion.PESO,
    nutricion.TALLA,
    nutricion.PESO_EDAD,
    nutricion.TALLA_EDAD,
    nutricion.PESO_TALLA,
    nutricion.IMC,
    nutricion.ID_VALORACION,
	nutricion.ID_PERIMETRO_PIERNA,
	nutricion.ID_PRESION_MENSUAL,
    nutricion.ID_TIPO_DIETA,
    nutricion.TIPO_DIETA_POR_DIA,
    nutricion.ID_SUPLEMENTO_NUTRICIONAL,
    nutricion.SUPLEMENTO_NUTRICIONAL_POR_DIA,
    nutricion.COMENTARIOS,
    nutricion.ID_NUTRICION,
    nutricion.ID_INSTITUCION,
    nutricion.estado,
    nutricion.ID_PERIODO,
	nutricion.VARIACION_PESO,
	sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('SUPLENUTRI',nutricion.ID_SUPLEMENTO_NUTRICIONAL) NOMBRE_SUPLEMENTO,
	sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('PERIPIERNA',nutricion.ID_PERIMETRO_PIERNA) NOMBRE_PERIMETRO,
	sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('PRENMANU',nutricion.ID_PRESION_MENSUAL) NOMBRE_PRESION,
	valoAdultos.DescripcionLocal NOMBRE_VALORACION_ADULTOS,
	dietaAdultos.DescripcionLocal NOMBRE_TIPO_DIETA_ADULTOS,
	valoNinos.DescripcionLocal NOMBRE_VALORACION_NINOS,
	dietaNinos.DescripcionLocal NOMBRE_TIPO_DIETA_NINOS,
	nutricion.EVALUADO

    from sgseguridadsys.PS_BENEFICIARIO bene
    JOIN sgseguridadsys.PS_ENTIDAD entidad ON entidad.ID_ENTIDAD=bene.ID_BENEFICIARIO
    JOIN sgseguridadsys.PS_NUTRICION nutricion ON nutricion.ID_BENEFICIARIO=bene.ID_BENEFICIARIO
    AND nutricion.ID_INSTITUCION=bene.ID_INSTITUCION
	LEFT JOIN MA_MiscelaneosDetalle valoAdultos ON valoAdultos.AplicacionCodigo='PS' and valoAdultos.CodigoTabla ='NUTRVALADU' AND valoAdultos.CodigoElemento=nutricion.ID_VALORACION
	LEFT JOIN MA_MiscelaneosDetalle valoNinos ON valoNinos.AplicacionCodigo='PS' and valoNinos.CodigoTabla ='NUTRVALNIN' AND valoNinos.CodigoElemento=nutricion.ID_VALORACION
	LEFT JOIN MA_MiscelaneosDetalle dietaNinos ON dietaNinos.AplicacionCodigo='PS' and dietaNinos.CodigoTabla ='TIPDIENIN' AND dietaNinos.CodigoElemento=nutricion.ID_TIPO_DIETA
	LEFT JOIN MA_MiscelaneosDetalle dietaAdultos ON dietaAdultos.AplicacionCodigo='PS' and dietaAdultos.CodigoTabla ='TIPDIEADU' AND dietaAdultos.CodigoElemento=nutricion.ID_TIPO_DIETA

    WHERE nutricion.ID_PERIODO=@pPeriodo
	AND ISNULL(bene.id_area,'XX')=ISNULL(@pArea,ISNULL(bene.id_area,'XX'))
	AND ISNULL(entidad.ID_SEXO,'XX')=ISNULL(@pSexo,ISNULL(entidad.ID_SEXO,'XX'))
    and ISNULL(entidad.NombreCompleto,'XX') LIKE '%' + ISNULL( @pNombreCompleto,ISNULL(entidad.NombreCompleto,'XX')) + '%'
	AND nutricion.ID_INSTITUCION=@pInstitucion
	AND bene.ID_PROGRAMA=@pPrograma
	AND ISNULL(entidad.EDAD,0) between ISNULL(@EdadInicio,0) AND ISNULL(@EdadFin,200)
	AND bene.ESTADO='ACT'
) reg


	SELECT 
	    ID_BENEFICIARIO,
		NOMBRECOMPLETO,
		FECHA_INFORME,
		PESO,
		TALLA,
		PESO_EDAD,
		TALLA_EDAD,
		PESO_TALLA,
		IMC,
		ID_VALORACION,
		ID_TIPO_DIETA,
		TIPO_DIETA_POR_DIA,
		ID_SUPLEMENTO_NUTRICIONAL,
		SUPLEMENTO_NUTRICIONAL_POR_DIA,
		COMENTARIOS,
		ID_NUTRICION,
		ID_INSTITUCION,
		estado,
		ID_PERIODO,
		ID_PERIMETRO_PIERNA,
		ID_PRESION_MENSUAL,
		VARIACION_PESO,
		CONTADOR,
		[NOMBRE_VALORACION_ADULTOS],
		[NOMBRE_TIPO_DIETA_ADULTOS] ,
		[NOMBRE_VALORACION_NINOS],
		[NOMBRE_TIPO_DIETA_NINOS] ,
		[NOMBRE_SUPLEMENTO] ,
		[NOMBRE_PERIMETRO] ,
		[NOMBRE_PRESION],
		EVALUADO
	FROM #tmp_nutricion temp
	where  temp.ID_ROW BETWEEN  (@pNumPag + 1)  AND 
					(@pNumPag + @pNumReg) 

	END



	