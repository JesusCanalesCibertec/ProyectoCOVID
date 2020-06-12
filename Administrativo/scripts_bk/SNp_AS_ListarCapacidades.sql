 ALTER PROCEDURE [sgseguridadsys].[SNp_AS_ListarCapacidades] ( 
      @pNombreCompleto varchar(200), 
	  @pPeriodo varchar(10) , 
	  @pArea varchar(10), 
	  @pSexo varchar(1), 
	  @pNumPag int, 
	  @pNumReg int, 
	  @pInstitucion varchar(6), 
	  @pPrograma varchar(3) ) AS 	
	  	
	  -- exec [sgseguridadsys].[SNp_AS_ListarCapacidades] null, '201801',null,null,1,20,'CARID','NNA'    
      DECLARE 
	  @ID_INSTITUCION AS VARCHAR(30), 
	  @ID_BENEFICIARIO INT, 
	  @ID_PERIODO AS VARCHAR(6), 
	  @NOMBRECOMPLETO AS VARCHAR(200), 
	  @FECHA_INFORME DATETIME, 
	  @ESTADO AS VARCHAR(2), 
	  @ID_RIESGO_CAIDA AS varchar(10), 
	  @ID_TIPO_INSTITUCION AS varchar(10), 
	  @ID_NIVEL AS varchar(10), 
	  @ID_GRADO_EDUCATIVO AS varchar(10), 
	  @ANIO_RETRASO AS varchar(10), 
	  @COMENTARIO_RETRASO AS VARCHAR(500), 
	  @ID_TIPO_COMUNICACION AS varchar(10), 
	  @COMENTARIO_RENDIMIENTO AS VARCHAR(500), 
	  @COMENTARIO_TALLERES AS VARCHAR(500), 
	  @ID_HABILIDAD_OCUPACIONAL AS varchar(10), 
	  @ID_EVALUAR_OCUPACIONAL AS varchar(10), 
	  @COMENTARIO_CAPACIDAD AS VARCHAR(500), 
	  @GRADO_DEPENDENCIA_KATZ AS VARCHAR(500), 
	  @GRADO_DEPENDENCIA_BARTHEL AS VARCHAR(500), 
	  @PORCENTAJE_GRADO_KATZ AS NUMERIC(20, 4), 
	  @PORCENTAJE_GRADO_BARTHEL AS NUMERIC(20, 4), 
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
	  @ID_CURSO_MATEMATICA AS varchar(10), 
	  @notaLogicoMatematico AS varchar(10), 
	  @ID_CURSO_COMUNICACION AS varchar(10), 
	  @notaComunicacion AS varchar(10), 
	  @ID_CURSO_COMPRENSION AS varchar(10), 
	  @notaComprensionLectora AS varchar(10), 
	  @ID_CURSO_PERSONAL AS varchar(10), 
	  @notaPersonalSocial AS varchar(10), 
	  @ID_CURSO_AMBIENTE AS varchar(10), 
	  @notaCienciaAmbiente AS varchar(10), 
	  @ID_TALLER_ARTISTICO INT, 
	  @idNotaTallerArtistico AS VARCHAR(10), 
	  @cantidadTallerArtistico INT, 
	  @ID_TALLER_FORMATIVO AS INT, 
	  @idNotaTallerFormativo AS VARCHAR(10), 
	  @cantidadTallerFormativo INT, 
	  @ID_TALLER_DEPORTIVO AS INT, 
	  @idNotaTallerDeportivo AS VARCHAR(10), 
	  @cantidadTallerDeportivo INT, 
	  @ID_TALLER_COGNITIVO AS INT, 
	  @idNotaTallerCognitivo AS VARCHAR(10), 
	  @cantidadTallerCognitivo INT, 
	  @ID_TALLER_FISICO AS INT, 
	  @idNotaTallerFisico AS VARCHAR(10), 
	  @cantidadTallerFisico INT, 
	  @EVALUADO AS VARCHAR(10), 
	  @ID_ORIGEN AS VARCHAR(3) 
      BEGIN
           
         create table #tmp_capacidades ( 			 
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
		 [ID_PERIODO] [varchar](10) COLLATE database_default NULL, 
		 [Barthel1] [int] NULL, 
		 [Barthel2] [int] NULL, 
		 [Barthel3][int] NULL, 
		 [Barthel4][int] NULL, 
		 [Barthel5][int] NULL, 
		 [Barthel6][int] NULL, 
		 [Barthel7][int] NULL, 
		 [Barthel8][int] NULL, 
		 [Barthel9][int] NULL, 
		 [Barthel10][int] NULL, 
		 [KATZ1][int] NULL, 
		 [KATZ2][int] NULL, 
		 [KATZ3][int] NULL, 
		 [KATZ4][int] NULL, 
		 [KATZ5][int] NULL, 
		 [KATZ6] [int] NULL, 
		 [notaLogicoMatematico] [VARCHAR] (10) COLLATE database_default NULL, 
		 [notaComunicacion] [VARCHAR] (10) COLLATE database_default NULL, 
		 [notaComprensionLectora] [VARCHAR] (10) COLLATE database_default NULL,
		 [notaPersonalSocial] [VARCHAR] (10) COLLATE database_default NULL, 
		 [notaCienciaAmbiente] [VARCHAR] (10) COLLATE database_default NULL, 
		 [idNotaTallerArtistico] [VARCHAR] (10)COLLATE database_default NULL, 
		 [cantidadTallerArtistico] [INT], 
		 [idNotaTallerFormativo] [VARCHAR] (10) COLLATE database_default NULL, 
		 [cantidadTallerFormativo] [INT], 
		 [idNotaTallerDeportivo] [VARCHAR] (10) COLLATE database_default NULL, 
		 [cantidadTallerDeportivo] [INT], 
		 [idNotaTallerCognitivo] [VARCHAR] (10) COLLATE database_default NULL, 
		 [cantidadTallerCognitivo] [INT], 
		 [idNotaTallerFisico] [VARCHAR] (10) COLLATE database_default NULL, 
		 [cantidadTallerFisico] [INT], 
		 [GRADO_DEPENDENCIA_KATZ] [VARCHAR] (500) COLLATE database_default NULL, 
		 [GRADO_DEPENDENCIA_BARTHEL] [VARCHAR] (500) COLLATE database_default NULL, 
		 [PORCENTAJE_GRADO_KATZ] [NUMERIC] (20, 4), 
		 [PORCENTAJE_GRADO_BARTHEL] [NUMERIC](20, 4), 
		 [ID_CURSO] [VARCHAR] (10) COLLATE database_default NULL, 
		 [ID_TALLER] [VARCHAR] (10) COLLATE database_default NULL, 
		 [ID_CURSO_MATEMATICA][VARCHAR] (10) COLLATE database_default NULL, 
		 [ID_CURSO_COMUNICACION][VARCHAR] (10) COLLATE database_default NULL, 
		 [ID_CURSO_COMPRENSION][VARCHAR] (10) COLLATE database_default NULL, 
		 [ID_CURSO_PERSONAL][VARCHAR] (10) COLLATE database_default NULL, 
		 [ID_CURSO_AMBIENTE][VARCHAR] (10) COLLATE database_default NULL, 
		 [ID_TALLER_ARTISTICO][VARCHAR] (10) COLLATE database_default NULL, 
		 [ID_TALLER_FORMATIVO][VARCHAR] (10) COLLATE database_default NULL, 
		 [ID_TALLER_DEPORTIVO][VARCHAR] (10) COLLATE database_default NULL, 
		 [ID_TALLER_COGNITIVO][VARCHAR] (10) COLLATE database_default NULL, 
		 [ID_TALLER_FISICO][VARCHAR] (10) COLLATE database_default NULL, 
		 [EVALUADO] [VARCHAR] (2) COLLATE database_default NULL, 
		 [CONTADOR] [int] NULL, 
		 [ID_ORIGEN] [varchar](3) NULL 
		 ) 
		 
		 create table #tmp_capacidades_curso ( 
		 [FILA] int NULL, 
		 [ID_CURSO] varchar(10), 
		 [ID_NOTA] varchar(30) 
		 ) 
		 
		 create table #tmp_capacidades_taller ( 
		 [FILA] int NULL, 
		 [ID_TALLER] INT, 
		 [ID_NOTA] varchar(30), 
		 [CANTIDAD] int NULL, 
		 ) 

         DECLARE InsertaCapacidadDetalle CURSOR FOR 
         select
            capacidad.ID_INSTITUCION,
            capacidad.ID_BENEFICIARIO,
            capacidad.ID_PERIODO,
            entidad.APELLIDO_PATERNO + ' ' + entidad.APELLIDO_MATERNO + ' ' + entidad.NOMBRES as NOMBRECOMPLETO,
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
            capacidad.EVALUADO,
            capacidad.ID_ORIGEN 
         from
            sgseguridadsys.PS_CAPACIDAD capacidad 
            JOIN
               sgseguridadsys.PS_BENEFICIARIO bene 
               ON bene.ID_BENEFICIARIO = capacidad.ID_BENEFICIARIO 
               AND bene.ID_INSTITUCION = capacidad.ID_INSTITUCION 
            JOIN
               sgseguridadsys.PS_ENTIDAD entidad 
               ON entidad.ID_ENTIDAD = bene.ID_BENEFICIARIO 
         WHERE
            capacidad.ID_PERIODO = @pPeriodo 
            AND capacidad.ID_INSTITUCION = ISNULL(@pInstitucion, capacidad.ID_INSTITUCION) 
            AND ISNULL(bene.id_area, 'XX') = ISNULL(@pArea, ISNULL(bene.id_area, 'XX')) 
            AND ISNULL(entidad.ID_SEXO, 'XX') = ISNULL(@pSexo, ISNULL(entidad.ID_SEXO, 'XX')) 
            and 
            (
               entidad.APELLIDO_PATERNO + ' ' + entidad.APELLIDO_MATERNO + ' ' + entidad.NOMBRES like '%' + isnull(@pNombreCompleto, '') + '%' 
               or entidad.NOMBRES + ' ' + entidad.APELLIDO_PATERNO + ' ' + entidad.APELLIDO_MATERNO like '%' + isnull(@pNombreCompleto, '') + '%' 
            )
            AND bene.ID_PROGRAMA = @pPrograma 
            AND bene.ESTADO = 'ACT' 
            AND capacidad.ID_ORIGEN = 'EVA' 
			
			OPEN InsertaCapacidadDetalle FETCH InsertaCapacidadDetalle 
			INTO @ID_INSTITUCION,
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
            @EVALUADO,
            @ID_ORIGEN 
			
			WHILE @@fetch_status = 0 
            BEGIN
               IF OBJECT_ID('tempdb..#tmp_capacidades_curso') IS NOT NULL 
               BEGIN
                  delete
                     #tmp_capacidades_curso 
               END
               IF OBJECT_ID('tempdb..#tmp_capacidades_taller') IS NOT NULL 
               BEGIN
                  delete
                     #tmp_capacidades_taller 
               END
               -- INSERTAR CURSOS    
               INSERT INTO
                  #tmp_capacidades_curso ( [FILA], [ID_CURSO], [ID_NOTA] ) 
                  SELECT
                     ROW_NUMBER() OVER(
                  ORDER BY
                     curso.ID_CURSO ASC) AS sec,
                     curso.ID_CURSO,
                     curso.ID_NOTA 
                  FROM
                     sgseguridadsys.PS_CAPACIDAD_CURSO curso 
                  WHERE
                     curso.ID_INSTITUCION = @ID_INSTITUCION 
                     AND curso.ID_BENEFICIARIO = @ID_BENEFICIARIO 
                     AND curso.ID_CAPACIDAD = @ID_CAPACIDAD 
                     DECLARE @x INT,
                     @xID_CURSO varchar(10),
                     @xID_NOTA varchar(10) 
                  SET
                     @x = 1 
                  SET
                     @notaCienciaAmbiente = NULL 
                  SET
                     @notaComunicacion = NULL 
                  SET
                     @notaComprensionLectora = NULL 
                  SET
                     @notaLogicoMatematico = NULL 
                  SET
                     @notaPersonalSocial = NULL 
					 while @x <= (SELECT COUNT(1) FROM #tmp_capacidades_curso)

                     BEGIN
                        SELECT
                           @xID_CURSO = ID_CURSO,
                           @xID_NOTA = ID_NOTA 
                        FROM
                           #tmp_capacidades_curso 
                        WHERE
                           FILA = @x IF @xID_CURSO = 'AMBI' 
                           BEGIN
                        SET
                           @notaCienciaAmbiente = @xID_NOTA 
                        SET
                           @ID_CURSO_AMBIENTE = @xID_CURSO 
                           END
                           IF @xID_CURSO = 'COMU' 
                           BEGIN
                        SET
                           @notaComunicacion = @xID_NOTA 
                        SET
                           @ID_CURSO_COMUNICACION = @xID_CURSO 
                           END
                           IF @xID_CURSO = 'LECT' 
                           BEGIN
                        SET
                           @notaComprensionLectora = @xID_NOTA 
                        SET
                           @ID_CURSO_COMPRENSION = @xID_CURSO 
                           END
                           IF @xID_CURSO = 'MATE' 
                           BEGIN
                        SET
                           @notaLogicoMatematico = @xID_NOTA 
                        SET
                           @ID_CURSO_MATEMATICA = @xID_CURSO 
                           END
                           IF @xID_CURSO = 'SOCI' 
                           BEGIN
                        SET
                           @notaPersonalSocial = @xID_NOTA 
                        SET
                           @ID_CURSO_PERSONAL = @xID_CURSO 
                           END
                        SET
                           @x = @x + 1 
                     END


                     -- INSERTAR TALLERES    
                     INSERT INTO
                        #tmp_capacidades_taller ( [FILA], [ID_TALLER], [ID_NOTA], [CANTIDAD] ) 
                        SELECT
                           ROW_NUMBER() OVER(
                        ORDER BY
                           taller.ID_TALLER ASC) AS sec,
                           taller.ID_TALLER,
                           taller.ID_NOTA,
                           taller.CANTIDAD 
                        FROM
                           sgseguridadsys.PS_CAPACIDAD_TALLER taller 
                        WHERE
                           taller.ID_INSTITUCION = @ID_INSTITUCION 
                           AND taller.ID_BENEFICIARIO = @ID_BENEFICIARIO 
                           AND taller.ID_CAPACIDAD = @ID_CAPACIDAD 
                           DECLARE @y INT,
                           @yID_TALLER INT,
                           @yID_NOTA varchar(10),
                           @yCANTIDAD INT 
                        SET
                           @y = 1 
                        SET
                           @idNotaTallerArtistico = 
                           (
                              select
                                 DescripcionLocal 
                              from
                                 MA_MiscelaneosDetalle 
                              where
                                 CodigoTabla = 'RENDCAPPA' 
                                 and CodigoElemento in
                                 (
                                    select
                                       max(b.id_rendiemiento) 
                                    from
                                       HR_Capacitacion a 
                                       join
                                          HR_Capacitacion_Beneficiario b 
                                          on a.CompanyOwner = b.CompanyOwner 
                                          and a.Capacitacion = b.Capacitacion 
                                    where
                                       ID_BENEFICIARIO = @ID_BENEFICIARIO 
                                       and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLARTE') 
                                       and a.Estado = 'A'
                                 )
                           )
                        SET
                           @cantidadTallerArtistico = 
                           (
                              select
                                 count(1) 
                              from
                                 HR_Capacitacion a 
                                 join
                                    HR_Capacitacion_Beneficiario b 
                                    on a.CompanyOwner = b.CompanyOwner 
                                    and a.Capacitacion = b.Capacitacion 
                              where
                                 ID_BENEFICIARIO = @ID_BENEFICIARIO 
                                 and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLARTE') 
                                 and a.Estado = 'A'
                           )
                        SET
                           @idNotaTallerDeportivo = 
                           (
                              select
                                 DescripcionLocal 
                              from
                                 MA_MiscelaneosDetalle 
                              where
                                 CodigoTabla = 'RENDCAPPA' 
                                 and CodigoElemento in
                                 (
                                    select
                                       max(b.id_rendiemiento) 
                                    from
                                       HR_Capacitacion a 
                                       join
                                          HR_Capacitacion_Beneficiario b 
                                          on a.CompanyOwner = b.CompanyOwner 
                                          and a.Capacitacion = b.Capacitacion 
                                    where
                                       ID_BENEFICIARIO = @ID_BENEFICIARIO 
                                       and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLDEPO')
                                       and a.Estado = 'A'
                                 )
                           )
                        SET
                           @cantidadTallerDeportivo = 
                           (
                              select
                                 count(1) 
                              from
                                 HR_Capacitacion a 
                                 join
                                    HR_Capacitacion_Beneficiario b 
                                    on a.CompanyOwner = b.CompanyOwner 
                                    and a.Capacitacion = b.Capacitacion 
                              where
                                 ID_BENEFICIARIO = @ID_BENEFICIARIO 
                                 and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLDEPO')
                                 and a.Estado = 'A'
                           )
                        SET
                           @idNotaTallerFormativo = 
                           (
                              select
                                 DescripcionLocal 
                              from
                                 MA_MiscelaneosDetalle 
                              where
                                 CodigoTabla = 'RENDCAPPA' 
                                 and CodigoElemento in
                                 (
                                    select
                                       max(b.id_rendiemiento) 
                                    from
                                       HR_Capacitacion a 
                                       join
                                          HR_Capacitacion_Beneficiario b 
                                          on a.CompanyOwner = b.CompanyOwner 
                                          and a.Capacitacion = b.Capacitacion 
                                    where
                                       ID_BENEFICIARIO = @ID_BENEFICIARIO 
                                       and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFORM')
                                       and a.Estado = 'A'
                                 )
                           )
                        SET
                           @cantidadTallerFormativo = 
                           (
                              select
                                 count(1) 
                              from
                                 HR_Capacitacion a 
                                 join
                                    HR_Capacitacion_Beneficiario b 
                                    on a.CompanyOwner = b.CompanyOwner 
                                    and a.Capacitacion = b.Capacitacion 
                              where
                                 ID_BENEFICIARIO = @ID_BENEFICIARIO 
                                 and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFORM')
                                 and a.Estado = 'A'
                           )
                        SET
                           @idNotaTallerFisico = 
                           (
                              select
                                 DescripcionLocal 
                              from
                                 MA_MiscelaneosDetalle 
                              where
                                 CodigoTabla = 'RENDCAPPA' 
                                 and CodigoElemento in
                                 (
                                    select
                                       max(b.id_rendiemiento) 
                                    from
                                       HR_Capacitacion a 
                                       join
                                          HR_Capacitacion_Beneficiario b 
                                          on a.CompanyOwner = b.CompanyOwner 
                                          and a.Capacitacion = b.Capacitacion 
                                    where
                                       ID_BENEFICIARIO = @ID_BENEFICIARIO 
                                       and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFISI') 
                                       and a.Estado = 'A'
                                 )
                           )
                        SET
                           @cantidadTallerFisico = 
                           (
                              select
                                 count(1) 
                              from
                                 HR_Capacitacion a 
                                 join
                                    HR_Capacitacion_Beneficiario b 
                                    on a.CompanyOwner = b.CompanyOwner 
                                    and a.Capacitacion = b.Capacitacion 
                              where
                                 ID_BENEFICIARIO = @ID_BENEFICIARIO 
                                 and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFISI') 
                                 and a.Estado = 'A' 
                           )
                        SET
                           @idNotaTallerCognitivo = 
                           (
                              select
                                 DescripcionLocal 
                              from
                                 MA_MiscelaneosDetalle 
                              where
                                 CodigoTabla = 'RENDCAPPA' 
                                 and CodigoElemento in
                                 (
                                    select
                                       max(b.id_rendiemiento) 
                                    from
                                       HR_Capacitacion a 
                                       join
                                          HR_Capacitacion_Beneficiario b 
                                          on a.CompanyOwner = b.CompanyOwner 
                                          and a.Capacitacion = b.Capacitacion 
                                    where
                                       ID_BENEFICIARIO = @ID_BENEFICIARIO 
                                       and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLCOGNI') 
                                       and a.Estado = 'A'
                                 )
                           )
                        SET
                           @cantidadTallerCognitivo = 
                           (
                              select
                                 count(1) 
                              from
                                 HR_Capacitacion a 
                                 join
                                    HR_Capacitacion_Beneficiario b 
                                    on a.CompanyOwner = b.CompanyOwner 
                                    and a.Capacitacion = b.Capacitacion 
                              where
                                 ID_BENEFICIARIO = @ID_BENEFICIARIO 
                                 and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLCOGNI') 
                                 and a.Estado = 'A'
                           )
                           while @y <= 
                           (
                              SELECT
                                 COUNT(1) 
                              FROM
                                 #tmp_capacidades_taller
                           )
                           BEGIN
                              SELECT
                                 @yID_TALLER = ID_TALLER,
                                 @yID_NOTA = ID_NOTA,
                                 @yCANTIDAD = CANTIDAD 
                              FROM
                                 #tmp_capacidades_taller 
                              WHERE
                                 FILA = @y IF @yID_TALLER = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLARTE') 
                                 BEGIN
                              SET
                                 @idNotaTallerArtistico = @yID_NOTA 
                              SET
                                 @cantidadTallerArtistico = @yCANTIDAD 
                              SEt
                                 @ID_TALLER_ARTISTICO = @yID_TALLER 
                                 END
                                 IF @yID_TALLER = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLDEPO')  
                                 BEGIN
                              SET
                                 @idNotaTallerDeportivo = @yID_NOTA 
                              SET
                                 @cantidadTallerDeportivo = @yCANTIDAD 
                              SEt
                                 @ID_TALLER_DEPORTIVO = @yID_TALLER 
                                 END
                                 IF @yID_TALLER = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFORM')  
                                 BEGIN
                              SET
                                 @idNotaTallerFormativo = @yID_NOTA 
                              SET
                                 @cantidadTallerFormativo = @yCANTIDAD 
                              SEt
                                 @ID_TALLER_FORMATIVO = @yID_TALLER 
                                 END
                                 IF @yID_TALLER = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLCOGNI') 
                                 BEGIN
                              SET
                                 @idNotaTallerCognitivo = @yID_NOTA 
                              SET
                                 @cantidadTallerCognitivo = @yCANTIDAD 
                              SEt
                                 @ID_TALLER_COGNITIVO = @yID_TALLER 
                                 END
                                 IF @yID_TALLER =(select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFISI')   
                                 BEGIN
                              SET
                                 @idNotaTallerFisico = @yID_NOTA 
                              SET
                                 @cantidadTallerFisico = @yCANTIDAD 
                              SEt
                                 @ID_TALLER_FISICO = @yID_TALLER 
                                 END
                              SET
                                 @y = @y + 1 
                           END
                           INSERT INTO
                              #tmp_capacidades ( [ID_INSTITUCION] , [ID_BENEFICIARIO] , [ID_CAPACIDAD] , [FECHA_INFORME] , [ID_TIPO_INSTITUCION] , [ID_NIVEL] , [ID_GRADO_EDUCATIVO], [ANIO_RETRASO], [COMENTARIO_RETRASO], [ID_TIPO_COMUNICACION] , [COMENTARIO_RENDIMIENTO], [COMENTARIO_TALLERES], [ID_RIESGO_CAIDA], [ID_HABILIDAD_OCUPACIONAL], [ID_EVALUAR_OCUPACIONAL], [COMENTARIO_CAPACIDAD] , [ESTADO], [NOMBRECOMPLETO], [ID_PERIODO], [Barthel1], [Barthel2], [Barthel3], [Barthel4], [Barthel5], [Barthel6], [Barthel7], [Barthel8], [Barthel9], [Barthel10], [KATZ1], [KATZ2], [KATZ3], [KATZ4], [KATZ5], [KATZ6], [notaLogicoMatematico] , [notaComunicacion], [notaComprensionLectora] , [notaPersonalSocial], [notaCienciaAmbiente], [idNotaTallerArtistico], [cantidadTallerArtistico], [idNotaTallerFormativo] , [cantidadTallerFormativo], [idNotaTallerDeportivo] , [cantidadTallerDeportivo], [GRADO_DEPENDENCIA_KATZ], [GRADO_DEPENDENCIA_BARTHEL], [PORCENTAJE_GRADO_KATZ], [PORCENTAJE_GRADO_BARTHEL], [ID_CURSO_MATEMATICA], [ID_CURSO_COMUNICACION], [ID_CURSO_COMPRENSION], [ID_CURSO_PERSONAL], [ID_CURSO_AMBIENTE], [ID_TALLER_ARTISTICO], [ID_TALLER_FORMATIVO], [ID_TALLER_DEPORTIVO], [ID_TALLER_COGNITIVO], [ID_TALLER_FISICO], [idNotaTallerCognitivo], [cantidadTallerCognitivo], [idNotaTallerFisico], [cantidadTallerFisico] , [EVALUADO], [ID_ORIGEN] ) 
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
                                 @notaLogicoMatematico,
                                 @notaComunicacion,
                                 @notaComprensionLectora,
                                 @notaPersonalSocial,
                                 @notaCienciaAmbiente,
                                 @idNotaTallerArtistico,
                                 @cantidadTallerArtistico,
                                 @idNotaTallerFormativo,
                                 @cantidadTallerFormativo,
                                 @idNotaTallerDeportivo,
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
                                 @EVALUADO,
                                 @ID_ORIGEN 
                              )
                              FETCH InsertaCapacidadDetalle INTO @ID_INSTITUCION,
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
                              @EVALUADO,
                              @ID_ORIGEN 
            END
            CLOSE InsertaCapacidadDetalle DEALLOCATE InsertaCapacidadDetalle 



            select
               * 
            from
               (
                  select
                     *,
                     ROW_NUMBER() OVER(
                  ORDER BY
                     y.NOMBRECOMPLETO ASC) AS ID_ROW 
                  from
                     (
                        SELECT
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
                           [notaLogicoMatematico],
                           [notaComunicacion],
                           [notaComprensionLectora],
                           [notaPersonalSocial],
                           [notaCienciaAmbiente],
                           [idNotaTallerArtistico],
                           [cantidadTallerArtistico],
                           [idNotaTallerFormativo],
                           [cantidadTallerFormativo],
                           [idNotaTallerDeportivo],
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
                           [EVALUADO],
                           [ID_ORIGEN] 
                        FROM
                           #tmp_capacidades capacidad 
                        UNION ALL
                        select
                           bene.ID_INSTITUCION,
                           bene.ID_BENEFICIARIO,
                           null,
                           entidad.APELLIDO_PATERNO + ' ' + entidad.APELLIDO_MATERNO + ' ' + entidad.NOMBRES as NOMBRECOMPLETO,
                           GETDATE(),
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           x.Barthel1,
                           x.Barthel2,
                           x.Barthel3,
                           x.Barthel4,
                           x.Barthel5,
                           x.Barthel6,
                           x.Barthel7,
                           x.Barthel8,
                           x.Barthel9,
                           x.Barthel10,
                           x.KATZ1,
                           x.KATZ2,
                           x.KATZ3,
                           x.KATZ4,
                           x.KATZ5,
                           x.KATZ6,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           (
                              select
                                 DescripcionLocal 
                              from
                                 MA_MiscelaneosDetalle 
                              where
                                 CodigoTabla = 'RENDCAPPA' 
                                 and CodigoElemento in
                                 (
                                    select
                                       max(b.id_rendiemiento) 
                                    from
                                       HR_Capacitacion a 
                                       join
                                          HR_Capacitacion_Beneficiario b 
                                          on a.CompanyOwner = b.CompanyOwner 
                                          and a.Capacitacion = b.Capacitacion 
                                    where
                                       ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                       and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLARTE')  
                                       and a.Estado = 'A'
                                 )
                           )
,
                           (
                              select
                                 count(1) 
                              from
                                 HR_Capacitacion a 
                                 join
                                    HR_Capacitacion_Beneficiario b 
                                    on a.CompanyOwner = b.CompanyOwner 
                                    and a.Capacitacion = b.Capacitacion 
                              where
                                 ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                 and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLARTE') 
                                 and a.Estado = 'A'
                           )
,
                           (
                              select
                                 DescripcionLocal 
                              from
                                 MA_MiscelaneosDetalle 
                              where
                                 CodigoTabla = 'RENDCAPPA' 
                                 and CodigoElemento in
                                 (
                                    select
                                       max(b.id_rendiemiento) 
                                    from
                                       HR_Capacitacion a 
                                       join
                                          HR_Capacitacion_Beneficiario b 
                                          on a.CompanyOwner = b.CompanyOwner 
                                          and a.Capacitacion = b .Capacitacion 
                                    where
                                       ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                       and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFORM') 
                                       and a.Estado = 'A'
                                 )
                           )
,
                           (
                              select
                                 count(1) 
                              from
                                 HR_Capacitacion a 
                                 join
                                    HR_Capacitacion_Beneficiario b 
                                    on a.CompanyOwner = b.CompanyOwner 
                                    and a.Capacitacion = b.Capacitacion 
                              where
                                 ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                 and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFORM')  
                                 and a.Estado = 'A'
                           )
,
                           (
                              select
                                 DescripcionLocal 
                              from
                                 MA_MiscelaneosDetalle 
                              where
                                 CodigoTabla = 'RENDCAPPA' 
                                 and CodigoElemento in
                                 (
                                    select
                                       max(b.id_rendiemiento) 
                                    from
                                       HR_Capacitacion a 
                                       join
                                          HR_Capacitacion_Beneficiario b 
                                          on a.CompanyOwner = b.CompanyOwner 
                                          and a.Capacitacion = b .Capacitacion 
                                    where
                                       ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                       and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLDEPO') 
                                       and a.Estado = 'A'
                                 )
                           )
,
                           (
                              select
                                 count(1) 
                              from
                                 HR_Capacitacion a 
                                 join
                                    HR_Capacitacion_Beneficiario b 
                                    on a.CompanyOwner = b.CompanyOwner 
                                    and a.Capacitacion = b.Capacitacion 
                              where
                                 ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                 and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLDEPO') 
                                 and a.Estado = 'A'
                           )
,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           NULL,
                           (
                              select
                                 DescripcionLocal 
                              from
                                 MA_MiscelaneosDetalle 
                              where
                                 CodigoTabla = 'RENDCAPPA' 
                                 and CodigoElemento in
                                 (
                                    select
                                       max(b.id_rendiemiento) 
                                    from
                                       HR_Capacitacion a 
                                       join
                                          HR_Capacitacion_Beneficiario b 
                                          on a.CompanyOwner = b.CompanyOwner 
                                          and a.Capacitacion = b .Capacitacion 
                                    where
                                       ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                       and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLCOGNI') 
                                       and a.Estado = 'A'
                                 )
                           )
,
                           (
                              select
                                 count(1) 
                              from
                                 HR_Capacitacion a 
                                 join
                                    HR_Capacitacion_Beneficiario b 
                                    on a.CompanyOwner = b.CompanyOwner 
                                    and a.Capacitacion = b.Capacitacion 
                              where
                                 ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                 and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLCOGNI')  
                                 and a.Estado = 'A'
                           )
,
                           (
                              select
                                 DescripcionLocal 
                              from
                                 MA_MiscelaneosDetalle 
                              where
                                 CodigoTabla = 'RENDCAPPA' 
                                 and CodigoElemento in
                                 (
                                    select
                                       max(b.id_rendiemiento) 
                                    from
                                       HR_Capacitacion a 
                                       join
                                          HR_Capacitacion_Beneficiario b 
                                          on a.CompanyOwner = b.CompanyOwner 
                                          and a.Capacitacion = b .Capacitacion 
                                    where
                                       ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                       and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFISI') 
                                       and a.Estado = 'A'
                                 )
                           )
,
                           (
                              select
                                 count(1) 
                              from
                                 HR_Capacitacion a 
                                 join
                                    HR_Capacitacion_Beneficiario b 
                                    on a.CompanyOwner = b.CompanyOwner 
                                    and a.Capacitacion = b.Capacitacion 
                              where
                                 ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                 and a.Curso = (select Numero from ParametrosMast where CompaniaCodigo='999999' AND AplicacionCodigo='PS' AND ParametroClave='TALLFISI') 
                                 and a.Estado = 'A'
                           )
,
                           NULL,
                           NULL 
                        from
                           sgseguridadsys.PS_BENEFICIARIO bene 
                           JOIN
                              sgseguridadsys.PS_ENTIDAD entidad 
                              ON entidad.ID_ENTIDAD = bene.ID_BENEFICIARIO 
                           left join
                              sgseguridadsys.PS_CAPACIDAD x 
                              on x.ID_INSTITUCION = bene.ID_INSTITUCION 
                              and x.ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                              and x.ID_PERIODO = @pPeriodo 
                              and x.ID_ORIGEN = 'EVA' 
                        WHERE
                           not exists
                           (
                              SELECT
                                 capacidad.ID_BENEFICIARIO 
                              FROM
                                 #tmp_capacidades capacidad 
                              WHERE
                                 capacidad.ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                 AND capacidad.ID_INSTITUCION = bene.ID_INSTITUCION 
                                 AND capacidad.ID_PERIODO = @pPeriodo 
                                 AND bene.ESTADO = 'ACT' 
                           )
                           AND ISNULL(bene.id_area, 'XX') = ISNULL(@pArea, ISNULL(bene.id_area, 'XX')) 
                           AND ISNULL(entidad.ID_SEXO, 'XX') = ISNULL(@pSexo, ISNULL(entidad.ID_SEXO, 'XX')) 
                           and 
                           (
                              entidad.APELLIDO_PATERNO + ' ' + entidad.APELLIDO_MATERNO + ' ' + entidad.NOMBRES like '%' + isnull(@pNombreCompleto, '') + '%' 
                              or entidad.NOMBRES + ' ' + entidad.APELLIDO_PATERNO + ' ' + entidad.APELLIDO_MATERNO like '%' + isnull(@pNombreCompleto, '') + '%' 
                           )
                           AND bene.ID_INSTITUCION = @pInstitucion 
                           AND bene.ID_PROGRAMA = @pPrograma 
                           AND bene.ESTADO = 'ACT' 
                     )
                     as y
               )
               as t 
            where
               t.ID_ROW BETWEEN (@pNumPag + 1) AND 
               (
                  @pNumPag + @pNumReg
               )
      END