
ALTER  PROCEDURE [sgseguridadsys].[SNp_AS_ListarNutricion]
(
@pNombreCompleto varchar(200),
@pPeriodo varchar(8) ,
@pTipoEdad varchar(10),
@pArea varchar(10),
@pSexo varchar(1),
@pNumPag int,
@pNumReg int,
@pInstitucion varchar(6),
@pPrograma varchar(3),
@pIdValoracion varchar(10)
)
AS

DECLARE @EdadInicio INT,
        @EdadFin INT

-- exec [sgseguridadsys].[SNp_AS_ListarNutricion] null, '201812','NUTRX',null,null,0,20,'PURIC','NNA',null
BEGIN
  
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
	[PESO_EDAD] [varchar](300) NULL,
	[TALLA_EDAD] [varchar](300) NULL,
	[PESO_TALLA] [varchar](300) NULL,
	[IMC]  [varchar](300) NULL,
	[ID_VALORACION] [varchar](4)  NULL,
	[ID_TIPO_DIETA] [varchar](5) NULL,
	[TIPO_DIETA_POR_DIA] [int]  NULL,
	[ID_SUPLEMENTO_NUTRICIONAL] [varchar](4)  NULL,
	[SUPLEMENTO_NUTRICIONAL_POR_DIA] [int]  NULL,
	[COMENTARIOS] [varchar](1000)  NULL,
	[ID_NUTRICION] [int]  NULL,
	[ID_INSTITUCION] [varchar](5)  COLLATE database_default NULL,
	[ESTADO] [varchar](1) COLLATE database_default NULL,
	[ID_PERIODO] [varchar](6)  NULL,
	[ID_PERIMETRO_PIERNA] [varchar](4) NULL,
	[ID_PRESION_MENSUAL] [varchar](4) NULL,
	[EVALUADO] [varchar](2) NULL,
	[VARIACION_PESO] [NUMERIC](19,2) NULL,
	[CONTADOR] [int] NULL,
	[ID_ORIGEN] [varchar](3) NULL
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
		EVALUADO,
		ID_ORIGEN
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
			reg.EVALUADO,
			reg.ID_ORIGEN

	 FROM (
 
			  select  
				bene.ID_BENEFICIARIO,
				entidad.APELLIDO_PATERNO+' ' +entidad.APELLIDO_MATERNO +' '+entidad.NOMBRES  as NOMBRECOMPLETO,
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
				nutricion.EVALUADO,
				nutricion.ID_ORIGEN
				from sgseguridadsys.PS_BENEFICIARIO bene
				JOIN sgseguridadsys.PS_ENTIDAD entidad ON entidad.ID_ENTIDAD=bene.ID_BENEFICIARIO
				JOIN sgseguridadsys.PS_NUTRICION nutricion ON nutricion.ID_BENEFICIARIO=bene.ID_BENEFICIARIO
				AND nutricion.ID_INSTITUCION=bene.ID_INSTITUCION
				WHERE nutricion.ID_PERIODO=@pPeriodo
				AND ISNULL(bene.id_area,'XX')=ISNULL(@pArea,ISNULL(bene.id_area,'XX'))
				AND ISNULL(entidad.ID_SEXO,'XX')=ISNULL(@pSexo,ISNULL(entidad.ID_SEXO,'XX'))
				AND 
				  (entidad.APELLIDO_PATERNO+' ' +entidad.APELLIDO_MATERNO +' '+entidad.NOMBRES like '%' + isnull(@pNombreCompleto, '') + '%'
  				    OR
				   entidad.NOMBRES+' ' +entidad.APELLIDO_PATERNO +' '+entidad.APELLIDO_MATERNO like '%' + isnull(@pNombreCompleto, '') + '%')
                AND nutricion.ID_INSTITUCION=@pInstitucion
				AND bene.ID_PROGRAMA=@pPrograma
				AND bene.ESTADO='ACT'
				AND nutricion.ID_ORIGEN='EVA'
				AND ISNULL(nutricion.ID_VALORACION,'XX')=ISNULL(@pIdValoracion,ISNULL(nutricion.ID_VALORACION,'XX'))
				AND ISNULL(entidad.EDAD,0) between @EdadInicio AND @EdadFin

				UNION ALL

				select  
				bene.ID_BENEFICIARIO,
				entidad.APELLIDO_PATERNO+' ' +entidad.APELLIDO_MATERNO +' '+entidad.NOMBRES as NOMBRECOMPLETO,
				SYSDATETIME(),
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null
				from sgseguridadsys.PS_BENEFICIARIO bene
				JOIN sgseguridadsys.PS_ENTIDAD entidad ON entidad.ID_ENTIDAD=bene.ID_BENEFICIARIO

				WHERE
				not  exists(
				SELECT nutricion.ID_BENEFICIARIO FROM sgseguridadsys.PS_NUTRICION nutricion
				WHERE nutricion.ID_BENEFICIARIO=bene.ID_BENEFICIARIO
				AND nutricion.ID_INSTITUCION=bene.ID_INSTITUCION
				AND nutricion.ID_PERIODO=@pPeriodo
				AND bene.ESTADO='ACT'
				AND nutricion.ID_ORIGEN='EVA'
				AND ISNULL(nutricion.ID_VALORACION,'XX')=ISNULL(@pIdValoracion,ISNULL(nutricion.ID_VALORACION,'XX'))
				AND ISNULL(entidad.EDAD,0) between @EdadInicio AND @EdadFin
				)
				AND ISNULL(bene.id_area,'XX')=ISNULL(@pArea,ISNULL(bene.id_area,'XX'))
				AND ISNULL(entidad.ID_SEXO,'XX')=ISNULL(@pSexo,ISNULL(entidad.ID_SEXO,'XX'))
	            AND
				(entidad.APELLIDO_PATERNO+' ' +entidad.APELLIDO_MATERNO +' '+entidad.NOMBRES like '%' + isnull(@pNombreCompleto, '') + '%'
  			     OR
				 entidad.NOMBRES+' ' +entidad.APELLIDO_PATERNO +' '+entidad.APELLIDO_MATERNO like '%' + isnull(@pNombreCompleto, '') + '%')
				AND bene.ID_INSTITUCION=@pInstitucion
				AND bene.ID_PROGRAMA=@pPrograma
				AND bene.ESTADO='ACT'
				AND ISNULL(entidad.EDAD,0) between @EdadInicio AND @EdadFin
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
		EVALUADO,
		(SELECT COUNT(1) FROM #tmp_nutricion) AS CANTIDAD,
		ID_ORIGEN
	FROM #tmp_nutricion temp
	where  temp.ID_ROW BETWEEN  (@pNumPag + 1)  AND 
					(@pNumPag + @pNumReg) 

	END