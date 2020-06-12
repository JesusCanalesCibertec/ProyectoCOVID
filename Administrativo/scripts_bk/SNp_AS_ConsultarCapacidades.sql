
ALTER  PROCEDURE [sgseguridadsys].[SNp_AS_ConsultarCapacidades]
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
/*
sgseguridadsys.SNp_AS_ConsultarCapacidades
        null,
        '2018S1',
        null,
        null,
        0,
        20,
        'CARID',
        'NNA'
*/



DECLARE
            @ID_INSTITUCION AS VARCHAR(30),
			@ID_BENEFICIARIO INT,
			@ID_PERIODO AS VARCHAR(6),
			@NOMBRECOMPLETO AS VARCHAR(200),
			@FECHA_INFORME DATETIME,
			@ESTADO AS VARCHAR(2),
            @ID_RIESGO_CAIDA AS VARCHAR(8),
			@ID_TIPO_INSTITUCION AS VARCHAR(8),
			@ID_NIVEL AS VARCHAR(8),
			@ID_GRADO_EDUCATIVO AS VARCHAR(8),
			@ANIO_RETRASO AS VARCHAR(8),
			@COMENTARIO_RETRASO AS VARCHAR(500),
			@ID_TIPO_COMUNICACION AS VARCHAR(8),
			@COMENTARIO_RENDIMIENTO AS VARCHAR(500),
			@COMENTARIO_TALLERES AS VARCHAR(500),
			@ID_HABILIDAD_OCUPACIONAL AS VARCHAR(8),
			@ID_EVALUAR_OCUPACIONAL AS VARCHAR(8),
			@COMENTARIO_CAPACIDAD AS VARCHAR(500),

			@GRADO_DEPENDENCIA_KATZ AS VARCHAR(500),
	        @GRADO_DEPENDENCIA_BARTHEL AS VARCHAR(500),

			@PORCENTAJE_GRADO_KATZ AS NUMERIC(20,4),
			@PORCENTAJE_GRADO_BARTHEL AS NUMERIC(20,4),

            @Barthel1 INT,
			@Barthel2 INT,
			@Barthel3 INT,
			@Barthel4 INT,
			@Barthel5 INT,
			@Barthel6 INT,
			@Barthel7 INT,
			@Barthel8 INT,
			@Barthel9 INT,
			@Barthel10 INT,
			@KATZ1 INT,
			@KATZ2 INT,
			@KATZ3 INT,
			@KATZ4 INT,
			@KATZ5 INT,
			@KATZ6 INT,
			@ID_CAPACIDAD INT,

			@ID_CURSO_MATEMATICA AS VARCHAR(8),
			@notaLogicoMatematico AS VARCHAR(8),

			@ID_CURSO_COMUNICACION AS VARCHAR(8),
			@notaComunicacion AS VARCHAR(8),

			@ID_CURSO_COMPRENSION AS VARCHAR(8),
			@notaComprensionLectora AS VARCHAR(8),

			@ID_CURSO_PERSONAL AS VARCHAR(8),
			@notaPersonalSocial AS VARCHAR(8),

			@ID_CURSO_AMBIENTE AS VARCHAR(8),
			@notaCienciaAmbiente AS VARCHAR(8),

			@ID_TALLER_ARTISTICO INT,
			@idNotaTallerArtistico AS VARCHAR(8),
            @cantidadTallerArtistico INT,

			@ID_TALLER_FORMATIVO AS INT,
			@idNotaTallerFormativo AS VARCHAR(8),
			@cantidadTallerFormativo INT,

			@ID_TALLER_DEPORTIVO AS INT,
			@idNotaTallerDeportivo AS VARCHAR(8),
			@cantidadTallerDeportivo INT,

			@ID_TALLER_COGNITIVO AS INT,
			@idNotaTallerCognitivo AS VARCHAR(8),
			@cantidadTallerCognitivo INT,

			@ID_TALLER_FISICO AS INT,
			@idNotaTallerFisico AS VARCHAR(8),
			@cantidadTallerFisico INT,
			@EVALUADO AS VARCHAR(2)


BEGIN
  
  --tabla para paginar 
	create table #tmp_capacidades
	(
	-- [ID_ROW] [int] NOT NULL,

	[ID_INSTITUCION] [varchar](5) COLLATE database_default NOT NULL ,
    [ID_BENEFICIARIO] [int] NOT NULL,
    [ID_CAPACIDAD] [int] NOT NULL,
    [FECHA_INFORME] [datetime] NULL,
	[ID_TIPO_INSTITUCION] [varchar](6) COLLATE database_default NULL,
    [ID_NIVEL] [varchar](6) COLLATE database_default NULL,
    [ID_GRADO_EDUCATIVO] [varchar](6) COLLATE database_default NULL,
	[ANIO_RETRASO] [varchar](6) COLLATE database_default NULL,
    [COMENTARIO_RETRASO] [varchar](500) COLLATE database_default NULL,
	[ID_TIPO_COMUNICACION] [varchar](6) COLLATE database_default NULL,
    [COMENTARIO_RENDIMIENTO] [varchar](500) COLLATE database_default NULL,
	[COMENTARIO_TALLERES] [varchar](500) COLLATE database_default NULL,
	[ID_RIESGO_CAIDA] [varchar](6) COLLATE database_default NULL,
    [ID_HABILIDAD_OCUPACIONAL] [varchar](6) COLLATE database_default NULL,
    [ID_EVALUAR_OCUPACIONAL] [varchar](6) COLLATE database_default NULL,
    [COMENTARIO_CAPACIDAD] [varchar](500) COLLATE database_default NULL,
	[ESTADO] [varchar](1) COLLATE database_default NULL,
	[NOMBRECOMPLETO] [varchar](200) COLLATE database_default NULL,
	[ID_PERIODO] [varchar](8) COLLATE database_default NULL,

	[Barthel1] [int]  NULL,
	[Barthel2] [int]  NULL,
	[Barthel3][int]  NULL,
	[Barthel4][int]  NULL,
	[Barthel5][int]  NULL,
	[Barthel6][int]  NULL,
	[Barthel7][int]  NULL,
	[Barthel8][int]  NULL,
	[Barthel9][int]  NULL,
	[Barthel10][int]  NULL,
	[KATZ1][int]  NULL,
	[KATZ2][int]  NULL,
	[KATZ3][int]  NULL,
	[KATZ4][int]  NULL,
	[KATZ5][int]  NULL,
	[KATZ6] [int]  NULL,

	
    [notaLogicoMatematico] [VARCHAR] (8) COLLATE database_default NULL,
	[notaComunicacion] [VARCHAR] (8) COLLATE database_default NULL,
	[notaComprensionLectora] [VARCHAR] (8) COLLATE database_default NULL,
	[notaPersonalSocial] [VARCHAR] (8) COLLATE database_default NULL,
	[notaCienciaAmbiente] [VARCHAR] (8) COLLATE database_default NULL,
	[idNotaTallerArtistico] [VARCHAR] (8)COLLATE database_default NULL,
	[cantidadTallerArtistico] [INT],
	[idNotaTallerFormativo] [VARCHAR] (8) COLLATE database_default NULL,
	[cantidadTallerFormativo] [INT],
	[idNotaTallerDeportivo] [VARCHAR] (8) COLLATE database_default NULL,
	[cantidadTallerDeportivo] [INT],
    [idNotaTallerCognitivo] [VARCHAR] (8) COLLATE database_default NULL,
	[cantidadTallerCognitivo] [INT],
	[idNotaTallerFisico] [VARCHAR] (8) COLLATE database_default NULL,
	[cantidadTallerFisico] [INT],

	[GRADO_DEPENDENCIA_KATZ] [VARCHAR] (500) COLLATE database_default NULL,
	[GRADO_DEPENDENCIA_BARTHEL] [VARCHAR] (500) COLLATE database_default NULL,

	[PORCENTAJE_GRADO_KATZ]  [NUMERIC] (20,4),
	[PORCENTAJE_GRADO_BARTHEL] [NUMERIC](20,4),

	[ID_CURSO] [VARCHAR] (8) COLLATE database_default NULL,
	[ID_TALLER] [VARCHAR] (8) COLLATE database_default NULL,

	[ID_CURSO_MATEMATICA][VARCHAR] (8) COLLATE database_default NULL,
	[ID_CURSO_COMUNICACION][VARCHAR] (8) COLLATE database_default NULL,
	[ID_CURSO_COMPRENSION][VARCHAR] (8) COLLATE database_default NULL,
	[ID_CURSO_PERSONAL][VARCHAR] (8) COLLATE database_default NULL,
	[ID_CURSO_AMBIENTE][VARCHAR] (8) COLLATE database_default NULL,

	[ID_TALLER_ARTISTICO][VARCHAR] (8) COLLATE database_default NULL,
	[ID_TALLER_FORMATIVO][VARCHAR] (8) COLLATE database_default NULL,
	[ID_TALLER_DEPORTIVO][VARCHAR] (8) COLLATE database_default NULL,
	[ID_TALLER_COGNITIVO][VARCHAR] (8) COLLATE database_default NULL,
	[ID_TALLER_FISICO][VARCHAR] (8) COLLATE database_default NULL,
	[EVALUADO][VARCHAR] (2) COLLATE database_default NULL,

	[CONTADOR] [int] NULL,
	)

	
	create table #tmp_capacidades_curso
	(
	  [FILA] int	NULL,
	  [ID_CURSO]	VARCHAR(8),
      [ID_NOTA] varchar(30)
    )


		create table #tmp_capacidades_taller
	(
	  [FILA] int	NULL,
	  [ID_TALLER]	INT,
      [ID_NOTA] varchar(30),
	  [CANTIDAD] int NULL,
    )

	DECLARE InsertaCapacidadDetalle CURSOR FOR
	 	select
			capacidad.ID_INSTITUCION,
			capacidad.ID_BENEFICIARIO,
			capacidad.ID_PERIODO,
			entidad.NOMBRECOMPLETO,
			capacidad.FECHA_INFORME,
			capacidad.ESTADO,
            capacidad.ID_RIESGO_CAIDA,
			capacidad.ID_TIPO_INSTITUCION,
			capacidad.ID_NIVEL,
			capacidad.ID_GRADO_EDUCATIVO,
			capacidad.ANIO_RETRASO,
			capacidad.COMENTARIO_RETRASO,
			capacidad.ID_TIPO_COMUNICACION,
			capacidad.COMENTARIO_RENDIMIENTO,
			capacidad.COMENTARIO_TALLERES,
			capacidad.ID_HABILIDAD_OCUPACIONAL,
			capacidad.ID_EVALUAR_OCUPACIONAL,
			capacidad.COMENTARIO_CAPACIDAD,
			capacidad.Barthel1,
			capacidad.Barthel2,
			capacidad.Barthel3,
			capacidad.Barthel4,
			capacidad.Barthel5,
			capacidad.Barthel6,
			capacidad.Barthel7,
			capacidad.Barthel8,
			capacidad.Barthel9,
			capacidad.Barthel10,
			capacidad.KATZ1,
			capacidad.KATZ2,
			capacidad.KATZ3,
			capacidad.KATZ4,
			capacidad.KATZ5,
			capacidad.KATZ6,
			capacidad.ID_CAPACIDAD,
			capacidad.GRADO_DEPENDENCIA_KATZ,
	        capacidad.GRADO_DEPENDENCIA_BARTHEL,
			capacidad.PORCENTAJE_GRADO_KATZ,
			capacidad.PORCENTAJE_GRADO_BARTHEL,
			capacidad.EVALUADO

    from sgseguridadsys.PS_CAPACIDAD capacidad
    JOIN sgseguridadsys.PS_BENEFICIARIO bene ON bene.ID_BENEFICIARIO=capacidad.ID_BENEFICIARIO
	 AND bene.ID_INSTITUCION=capacidad.ID_INSTITUCION
    JOIN sgseguridadsys.PS_ENTIDAD entidad ON entidad.ID_ENTIDAD=bene.ID_BENEFICIARIO
    WHERE
    capacidad.ID_PERIODO=@pPeriodo
    AND capacidad.ID_INSTITUCION=ISNULL(@pInstitucion,capacidad.ID_INSTITUCION)
    AND ISNULL(bene.id_area,'XX')=ISNULL(@pArea,ISNULL(bene.id_area,'XX'))
    AND ISNULL(entidad.ID_SEXO,'XX')=ISNULL(@pSexo,ISNULL(entidad.ID_SEXO,'XX'))
    AND entidad.NOMBRECOMPLETO LIKE  '%' + COALESCE(@pNombreCompleto, entidad.NOMBRECOMPLETO) + '%'
	AND bene.ID_PROGRAMA=@pPrograma


	 OPEN InsertaCapacidadDetalle
   
     FETCH InsertaCapacidadDetalle

	 INTO
	       @ID_INSTITUCION,
			@ID_BENEFICIARIO,
			@ID_PERIODO,
			@NOMBRECOMPLETO,
			@FECHA_INFORME,
			@ESTADO,
            @ID_RIESGO_CAIDA,
			@ID_TIPO_INSTITUCION,
			@ID_NIVEL,
			@ID_GRADO_EDUCATIVO,
			@ANIO_RETRASO,
			@COMENTARIO_RETRASO,
			@ID_TIPO_COMUNICACION,
			@COMENTARIO_RENDIMIENTO,
			@COMENTARIO_TALLERES,
			@ID_HABILIDAD_OCUPACIONAL,
			@ID_EVALUAR_OCUPACIONAL,
			@COMENTARIO_CAPACIDAD,
			@Barthel1,
			@Barthel2,
			@Barthel3,
			@Barthel4,
			@Barthel5,
			@Barthel6,
			@Barthel7,
			@Barthel8,
			@Barthel9,
			@Barthel10,
			@KATZ1,
			@KATZ2,
			@KATZ3,
			@KATZ4,
			@KATZ5,
			@KATZ6,
			@ID_CAPACIDAD,
			@GRADO_DEPENDENCIA_KATZ,
	        @GRADO_DEPENDENCIA_BARTHEL,
			@PORCENTAJE_GRADO_KATZ,
			@PORCENTAJE_GRADO_BARTHEL,
			@EVALUADO

		 WHILE @@fetch_status = 0
			BEGIN
					IF OBJECT_ID('tempdb..#tmp_capacidades_curso') IS NOT NULL
					 BEGIN
						  delete #tmp_capacidades_curso
					 END

					 IF OBJECT_ID('tempdb..#tmp_capacidades_taller') IS NOT NULL
					 BEGIN
						  delete #tmp_capacidades_taller
					 END

					  -- INSERTAR CURSOS
					INSERT INTO #tmp_capacidades_curso
					(
					   [FILA],
					   [ID_CURSO],
					   [ID_NOTA]
					)
					SELECT 
					  ROW_NUMBER() OVER(ORDER BY curso.ID_CURSO ASC) AS sec,
					  curso.ID_CURSO,
					  curso.ID_NOTA
					FROM sgseguridadsys.PS_CAPACIDAD_CURSO curso
					WHERE curso.ID_INSTITUCION=@ID_INSTITUCION AND curso.ID_BENEFICIARIO= @ID_BENEFICIARIO
					AND curso.ID_CAPACIDAD=@ID_CAPACIDAD
									   
					DECLARE @x INT,
					@xID_CURSO VARCHAR(8),
					@xID_NOTA  VARCHAR(8)
				
					SET @x=1
					SET @notaCienciaAmbiente=NULL
					SET @notaComunicacion=NULL
					SET @notaComprensionLectora=NULL
					SET @notaLogicoMatematico=NULL
					SET @notaPersonalSocial=NULL

					while @x<=(SELECT COUNT(1) FROM #tmp_capacidades_curso)
					BEGIN

				          SELECT 
							  @xID_CURSO=ID_CURSO,
							  @xID_NOTA=ID_NOTA
						  FROM #tmp_capacidades_curso WHERE FILA=@x

						   IF @xID_CURSO='AMBI'
							 BEGIN
							   SET @notaCienciaAmbiente=@xID_NOTA
							   SET @ID_CURSO_AMBIENTE= @xID_CURSO
							 END

						  IF @xID_CURSO='COMU'
							 BEGIN
							   SET @notaComunicacion=@xID_NOTA
							   SET @ID_CURSO_COMUNICACION = @xID_CURSO
							 END

						  IF @xID_CURSO='LECT'
							 BEGIN
							    SET @notaComprensionLectora=@xID_NOTA
								 SET @ID_CURSO_COMPRENSION = @xID_CURSO
							 END

						  IF @xID_CURSO='MATE'
							 BEGIN
							    SET @notaLogicoMatematico=@xID_NOTA
								 SET @ID_CURSO_MATEMATICA = @xID_CURSO
							 END

						  IF @xID_CURSO='SOCI'
							 BEGIN
							    SET @notaPersonalSocial=@xID_NOTA
								 SET @ID_CURSO_PERSONAL = @xID_CURSO
							 END

						SET @x=@x+1
					END

				   
				   -- INSERTAR TALLERES
					INSERT INTO #tmp_capacidades_taller
					(
					   [FILA],
					   [ID_TALLER],
					   [ID_NOTA],
					   [CANTIDAD]
					)
					SELECT 
					  ROW_NUMBER() OVER(ORDER BY taller.ID_TALLER ASC) AS sec,
					  taller.ID_TALLER,
					  taller.ID_NOTA,
					  taller.CANTIDAD
					FROM sgseguridadsys.PS_CAPACIDAD_TALLER taller
					WHERE taller.ID_INSTITUCION=@ID_INSTITUCION AND taller.ID_BENEFICIARIO= @ID_BENEFICIARIO
					AND taller.ID_CAPACIDAD=@ID_CAPACIDAD

					DECLARE @y INT,
					@yID_TALLER  INT,
					@yID_NOTA  VARCHAR(8),
					@yCANTIDAD INT
				
					SET @y=1
					SET @idNotaTallerArtistico=NULL
					SET @cantidadTallerArtistico=NULL
					SET @idNotaTallerDeportivo=NULL
					SET @cantidadTallerDeportivo=NULL
					SET @idNotaTallerFormativo=NULL
					SET @cantidadTallerFormativo=NULL

					SET @idNotaTallerFisico=NULL
					SET @cantidadTallerFisico=NULL
					SET @idNotaTallerCognitivo=NULL
					SET @cantidadTallerCognitivo=NULL

					while @y<=(SELECT COUNT(1) FROM #tmp_capacidades_taller)
					BEGIN
			         
                          SELECT 
							  @yID_TALLER=ID_TALLER,
							  @yID_NOTA=ID_NOTA,
							  @yCANTIDAD=CANTIDAD
						  FROM #tmp_capacidades_taller WHERE FILA=@y

						   IF @yID_TALLER=(select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLARTE') 
							 BEGIN
							    SET @idNotaTallerArtistico=@yID_NOTA
							    SET @cantidadTallerArtistico=@yCANTIDAD
								SEt @ID_TALLER_ARTISTICO=@yID_TALLER
							 END

						  IF @yID_TALLER=(select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLDEPO') 
							 BEGIN
							    SET @idNotaTallerDeportivo=@yID_NOTA
							    SET @cantidadTallerDeportivo=@yCANTIDAD
								SEt @ID_TALLER_DEPORTIVO =@yID_TALLER
							 END

						  IF @yID_TALLER=(select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFORM') 
							 BEGIN
							   SET @idNotaTallerFormativo=@yID_NOTA
							   SET @cantidadTallerFormativo=@yCANTIDAD
							   SEt @ID_TALLER_FORMATIVO =@yID_TALLER
							 END

			              IF @yID_TALLER=(select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLCOGNI') 
							 BEGIN
							    SET @idNotaTallerCognitivo=@yID_NOTA
							    SET @cantidadTallerCognitivo=@yCANTIDAD
								SEt @ID_TALLER_COGNITIVO =@yID_TALLER
							 END

						  IF @yID_TALLER=(select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFISI') 
							 BEGIN
							   SET @idNotaTallerFisico=@yID_NOTA
							   SET @cantidadTallerFisico=@yCANTIDAD
							   SEt @ID_TALLER_FISICO =@yID_TALLER
							 END




							 SET @y=@y+1
                      END

					
                     INSERT INTO #tmp_capacidades
			          (

						[ID_INSTITUCION] ,
						[ID_BENEFICIARIO] ,
						[ID_CAPACIDAD] ,
						[FECHA_INFORME] ,
						[ID_TIPO_INSTITUCION] ,
						[ID_NIVEL] ,
						[ID_GRADO_EDUCATIVO],
						[ANIO_RETRASO],
						[COMENTARIO_RETRASO],
						[ID_TIPO_COMUNICACION] ,
						[COMENTARIO_RENDIMIENTO],
						[COMENTARIO_TALLERES],
						[ID_RIESGO_CAIDA],
						[ID_HABILIDAD_OCUPACIONAL],
						[ID_EVALUAR_OCUPACIONAL],
						[COMENTARIO_CAPACIDAD] ,
						[ESTADO],
						[NOMBRECOMPLETO],
						[ID_PERIODO],
						    [Barthel1],
							[Barthel2],
							[Barthel3],
							[Barthel4],
							[Barthel5],
							[Barthel6],
							[Barthel7],
							[Barthel8],
							[Barthel9],
							[Barthel10],
							[KATZ1],
							[KATZ2],
							[KATZ3],
							[KATZ4],
							[KATZ5],
							[KATZ6],
                            [notaLogicoMatematico] ,
							[notaComunicacion], 
							[notaComprensionLectora] ,
							[notaPersonalSocial], 
							[notaCienciaAmbiente], 
							[idNotaTallerArtistico], 
							[cantidadTallerArtistico],
							[idNotaTallerFormativo] ,
							[cantidadTallerFormativo],
							[idNotaTallerDeportivo] ,
							[cantidadTallerDeportivo],
							[GRADO_DEPENDENCIA_KATZ],
							[GRADO_DEPENDENCIA_BARTHEL],
							[PORCENTAJE_GRADO_KATZ],
							[PORCENTAJE_GRADO_BARTHEL],

							[ID_CURSO_MATEMATICA],
							[ID_CURSO_COMUNICACION],
							[ID_CURSO_COMPRENSION],
							[ID_CURSO_PERSONAL],
							[ID_CURSO_AMBIENTE],
							[ID_TALLER_ARTISTICO],
							[ID_TALLER_FORMATIVO],
							[ID_TALLER_DEPORTIVO],

							[ID_TALLER_COGNITIVO],
							[ID_TALLER_FISICO],
							[idNotaTallerCognitivo],
							[cantidadTallerCognitivo],
							[idNotaTallerFisico],
							[cantidadTallerFisico],
							EVALUADO
							
						)
						VALUES
						(
						    @ID_INSTITUCION,
							@ID_BENEFICIARIO,
							@ID_CAPACIDAD,
							@FECHA_INFORME,
							@ID_TIPO_INSTITUCION,
							@ID_NIVEL,
							@ID_GRADO_EDUCATIVO,
							@ANIO_RETRASO,
							@COMENTARIO_RETRASO,
							@ID_TIPO_COMUNICACION,
							@COMENTARIO_RENDIMIENTO,
							@COMENTARIO_TALLERES,
							@ID_RIESGO_CAIDA,
							@ID_HABILIDAD_OCUPACIONAL,
							@ID_EVALUAR_OCUPACIONAL,
							@COMENTARIO_CAPACIDAD,
							@ESTADO,
							@NOMBRECOMPLETO,
                            @ID_PERIODO,
							@Barthel1,
							@Barthel2,
							@Barthel3,
							@Barthel4,
							@Barthel5,
							@Barthel6,
							@Barthel7,
							@Barthel8,
							@Barthel9,
							@Barthel10,
							@KATZ1,
							@KATZ2,
							@KATZ3,
							@KATZ4,
							@KATZ5,
							@KATZ6,

							@notaLogicoMatematico ,
							@notaComunicacion, 
							@notaComprensionLectora ,
							@notaPersonalSocial, 
							@notaCienciaAmbiente, 
							@idNotaTallerArtistico, 
							@cantidadTallerArtistico,
							@idNotaTallerFormativo ,
							@cantidadTallerFormativo,
							@idNotaTallerDeportivo ,
							@cantidadTallerDeportivo,
							@GRADO_DEPENDENCIA_KATZ,
	                        @GRADO_DEPENDENCIA_BARTHEL,
							@PORCENTAJE_GRADO_KATZ,
							@PORCENTAJE_GRADO_BARTHEL,
					        @ID_CURSO_MATEMATICA,
							@ID_CURSO_COMUNICACION,
							@ID_CURSO_COMPRENSION,
							@ID_CURSO_PERSONAL,
							@ID_CURSO_AMBIENTE,
							@ID_TALLER_ARTISTICO,
							@ID_TALLER_FORMATIVO,
							@ID_TALLER_DEPORTIVO,
							@ID_TALLER_COGNITIVO,
							@ID_TALLER_FISICO,
							@idNotaTallerCognitivo,
							@cantidadTallerCognitivo,
							@idNotaTallerFisico,
							@cantidadTallerFisico,
							@EVALUADO

							
						)



			 FETCH InsertaCapacidadDetalle
	  
			 INTO 
			    @ID_INSTITUCION,
				@ID_BENEFICIARIO,
				@ID_PERIODO,
				@NOMBRECOMPLETO,
				@FECHA_INFORME,
				@ESTADO,
				@ID_RIESGO_CAIDA,
				@ID_TIPO_INSTITUCION,
				@ID_NIVEL,
				@ID_GRADO_EDUCATIVO,
				@ANIO_RETRASO,
				@COMENTARIO_RETRASO,
				@ID_TIPO_COMUNICACION,
				@COMENTARIO_RENDIMIENTO,
				@COMENTARIO_TALLERES,
				@ID_HABILIDAD_OCUPACIONAL,
				@ID_EVALUAR_OCUPACIONAL,
				@COMENTARIO_CAPACIDAD,
				@Barthel1,
				@Barthel2,
				@Barthel3,
				@Barthel4,
				@Barthel5,
				@Barthel6,
				@Barthel7,
				@Barthel8,
				@Barthel9,
				@Barthel10,
				@KATZ1,
				@KATZ2,
				@KATZ3,
				@KATZ4,
				@KATZ5,
				@KATZ6,
				@ID_CAPACIDAD,
				@GRADO_DEPENDENCIA_KATZ,
	            @GRADO_DEPENDENCIA_BARTHEL,
				@PORCENTAJE_GRADO_KATZ,
			    @PORCENTAJE_GRADO_BARTHEL,
				@EVALUADO


	END
	CLOSE InsertaCapacidadDetalle
    DEALLOCATE InsertaCapacidadDetalle


	SELECT 
	   
        reg.ID_INSTITUCION,
		reg.ID_BENEFICIARIO,
		reg.ID_PERIODO,
		reg.NOMBRECOMPLETO,
		reg.FECHA_INFORME,
		reg.ESTADO,
		reg.ID_RIESGO_CAIDA,
		reg.ID_TIPO_INSTITUCION,
		reg.ID_NIVEL,
		reg.ID_GRADO_EDUCATIVO,
		reg.ANIO_RETRASO,
		reg.COMENTARIO_RETRASO,
		reg.ID_TIPO_COMUNICACION,
		reg.COMENTARIO_RENDIMIENTO,
		reg.COMENTARIO_TALLERES,
		reg.ID_HABILIDAD_OCUPACIONAL,
		reg.ID_EVALUAR_OCUPACIONAL,
		reg.COMENTARIO_CAPACIDAD,
        reg.ID_CAPACIDAD,
		reg.Barthel1,
		reg.Barthel2,
		reg.Barthel3,
		reg.Barthel4,
		reg.Barthel5,
		reg.Barthel6,
		reg.Barthel7,
		reg.Barthel8,
		reg.Barthel9,
		reg.Barthel10,
		reg.KATZ1,
		reg.KATZ2,
		reg.KATZ3,
		reg.KATZ4,
		reg.KATZ5,
		reg.KATZ6,
		reg.[notaLogicoMatematico] ,
		reg.[notaComunicacion], 
		reg.[notaComprensionLectora] ,
		reg.[notaPersonalSocial], 
		reg.[notaCienciaAmbiente], 
		reg.[idNotaTallerArtistico], 
		reg.[cantidadTallerArtistico],
		reg.[idNotaTallerFormativo] ,
		reg.[cantidadTallerFormativo],
		reg.[idNotaTallerDeportivo] ,
		reg.[cantidadTallerDeportivo],
		reg.[GRADO_DEPENDENCIA_KATZ],
		reg.[GRADO_DEPENDENCIA_BARTHEL],
		reg.[PORCENTAJE_GRADO_KATZ],
		reg.[PORCENTAJE_GRADO_BARTHEL],
		reg.[ID_CURSO_MATEMATICA],
		reg.[ID_CURSO_COMUNICACION],
		reg.[ID_CURSO_COMPRENSION],
		reg.[ID_CURSO_PERSONAL],
		reg.[ID_CURSO_AMBIENTE],
		reg.[ID_TALLER_ARTISTICO],
		reg.[ID_TALLER_FORMATIVO],
		reg.[ID_TALLER_DEPORTIVO],
        reg.[ID_TALLER_COGNITIVO],
		reg.[ID_TALLER_FISICO],
		reg.[idNotaTallerCognitivo],
		reg.[cantidadTallerCognitivo],
		reg.[idNotaTallerFisico],
		reg.[cantidadTallerFisico],
		reg.NOMBRE_HABILIDAD,
		reg.EVALUADO,
		reg.NOMBRE_TIPO_INSTITUCION,
		reg.NOMBRE_NIVEL,
		reg.NOMBRE_GRADO,
		reg.sec

		FROM
		(

     SELECT 
	    ROW_NUMBER() OVER(ORDER BY ID_BENEFICIARIO ASC) AS sec,
        capacidad.ID_INSTITUCION,
		capacidad.ID_BENEFICIARIO,
		capacidad.ID_PERIODO,
		capacidad.NOMBRECOMPLETO,
		capacidad.FECHA_INFORME,
		capacidad.ESTADO,
		capacidad.ID_RIESGO_CAIDA,
		capacidad.ID_TIPO_INSTITUCION,
		capacidad.ID_NIVEL,
		capacidad.ID_GRADO_EDUCATIVO,
		capacidad.ANIO_RETRASO,
		capacidad.COMENTARIO_RETRASO,
		capacidad.ID_TIPO_COMUNICACION,
		capacidad.COMENTARIO_RENDIMIENTO,
		capacidad.COMENTARIO_TALLERES,
		capacidad.ID_HABILIDAD_OCUPACIONAL,
		capacidad.ID_EVALUAR_OCUPACIONAL,
		capacidad.COMENTARIO_CAPACIDAD,

        capacidad.ID_CAPACIDAD,
		capacidad.Barthel1,
		capacidad.Barthel2,
		capacidad.Barthel3,
		capacidad.Barthel4,
		capacidad.Barthel5,
		capacidad.Barthel6,
		capacidad.Barthel7,
		capacidad.Barthel8,
		capacidad.Barthel9,
		capacidad.Barthel10,
		capacidad.KATZ1,
		capacidad.KATZ2,
		capacidad.KATZ3,
		capacidad.KATZ4,
		capacidad.KATZ5,
		capacidad.KATZ6,

		[notaLogicoMatematico] ,
		[notaComunicacion], 
		[notaComprensionLectora] ,
		[notaPersonalSocial], 
		[notaCienciaAmbiente], 
		[idNotaTallerArtistico], 
		[cantidadTallerArtistico],
		[idNotaTallerFormativo] ,
		[cantidadTallerFormativo],
		[idNotaTallerDeportivo] ,
		[cantidadTallerDeportivo],
		[GRADO_DEPENDENCIA_KATZ],
		[GRADO_DEPENDENCIA_BARTHEL],
		[PORCENTAJE_GRADO_KATZ],
		[PORCENTAJE_GRADO_BARTHEL],

		[ID_CURSO_MATEMATICA],
		[ID_CURSO_COMUNICACION],
		[ID_CURSO_COMPRENSION],
		[ID_CURSO_PERSONAL],
		[ID_CURSO_AMBIENTE],
		[ID_TALLER_ARTISTICO],
		[ID_TALLER_FORMATIVO],
		[ID_TALLER_DEPORTIVO],

		[ID_TALLER_COGNITIVO],
		[ID_TALLER_FISICO],
		[idNotaTallerCognitivo],
		[cantidadTallerCognitivo],
		[idNotaTallerFisico],
		[cantidadTallerFisico],
		sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('HABIOCUPA',capacidad.ID_HABILIDAD_OCUPACIONAL)NOMBRE_HABILIDAD,
		EVALUADO,
		sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('TIPINSTCAP',capacidad.ID_TIPO_INSTITUCION)NOMBRE_TIPO_INSTITUCION,
		sgseguridadsys.FN_OBTENER_NOMBRE_MISCELANEO('NIVELCAPAC',capacidad.ID_NIVEL)NOMBRE_NIVEL,
		sgseguridadsys.FN_OBTENER_NOMBRE_POR_ELEMENTO(capacidad.ID_GRADO_EDUCATIVO)NOMBRE_GRADO

		FROM #tmp_capacidades  capacidad
		)reg
		 WHERE  reg.sec BETWEEN  (@pNumPag + 1)  AND 
					(@pNumPag + @pNumReg) 
		
END