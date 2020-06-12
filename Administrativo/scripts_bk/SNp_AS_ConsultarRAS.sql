
ALTER  PROCEDURE [sgseguridadsys].[SNp_AS_ConsultarRAS]
(
@pNombreCompleto varchar(200),
@pFechaInicio DATETIME,
@pFechaFin DATETIME,
@pNumPag int,
@pNumReg int,
@pInstitucion varchar(6),
@pPrograma varchar(3),
@pArea varchar(10),
@pSexo varchar(1),
@TipoRas  varchar(1),
@pIdDiagnosticos  INT
)
AS 
    DECLARE @v_ID_ATENCION AS INT 
	DECLARE @v_ID_INSTITUCION  AS VARCHAR(6)
	DECLARE @v_ID_BENEFICIARIO AS INT
	DECLARE @v_NOMBRECOMPLETO  AS VARCHAR(300)
	DECLARE @v_DOCUMENTO  AS VARCHAR(20)
	DECLARE @v_RESIDENCIA  AS VARCHAR(200)
	DECLARE @v_ID_PERIODO  AS VARCHAR(10)
	DECLARE @v_FECHA_ATENCION AS DATETIME
	DECLARE @v_ID_AREA  AS VARCHAR(8)
	
	DECLARE @v_ID_DIAGNOSTICO AS INT 
	DECLARE @v_DIAGNOSTICO_POSICION AS INT 
	DECLARE @v_DIAGNOSTICO_NOMBRE AS VARCHAR(200)

	DECLARE @v_ID_TRATAMIENTO1 AS VARCHAR(10)
	DECLARE @v_ID_TRATAMIENTO2 AS VARCHAR(10)
	DECLARE @v_ID_TRATAMIENTO3 AS VARCHAR(10)

	DECLARE @v_ID_TRATAMIENTO1_NOMBRE AS VARCHAR(300)
	DECLARE @v_ID_TRATAMIENTO2_NOMBRE AS VARCHAR(300)
	DECLARE @v_ID_TRATAMIENTO3_NOMBRE AS VARCHAR(300)


  -- exec [sgseguridadsys].[SNp_AS_ConsultarRAS] null, '2018-12-01','2019-12-19 23:59:59',1,10,'PURIC','NNA',null,NULL,'I',NULL 
  BEGIN 
      CREATE TABLE #tmp_atencion 
        ( 
           [id_institucion]  [VARCHAR](5) COLLATE database_default, 
           [id_beneficiario] [INT], 
           [nombrecompleto]  [VARCHAR](200) COLLATE database_default, 
           [fecha_atencion]  [DATETIME] NULL, 
           [documento]       [VARCHAR](30) COLLATE database_default, 
           [id_periodo]      [VARCHAR](6) COLLATE database_default, 
		   [residencia]      [VARCHAR](200) COLLATE database_default, 
		   [id_area]         [VARCHAR](8) COLLATE database_default, 
           [id_atencion]     INT NULL,
		   [diagnostico1_id]     INT NULL,
		   [diagnostico1_nombre]     varchar(200) NULL,
		   [diagnostico1_idtratamiento1]     varchar(10) NULL,
		   [diagnostico1_idtratamiento2]     varchar(10) NULL,
		   [diagnostico1_idtratamiento3]     varchar(10) NULL,
		   [diagnostico1_idtratamiento1_nombre]     varchar(300) NULL,
		   [diagnostico1_idtratamiento2_nombre]     varchar(300) NULL,
		   [diagnostico1_idtratamiento3_nombre]     varchar(300) NULL,

		   [diagnostico2_id]     INT NULL,
		   [diagnostico2_nombre]     varchar(200) NULL,
		   [diagnostico2_idtratamiento1]     varchar(10) NULL,
		   [diagnostico2_idtratamiento2]     varchar(10) NULL,
		   [diagnostico2_idtratamiento3]     varchar(10) NULL,
		   [diagnostico2_idtratamiento1_nombre]     varchar(300) NULL,
		   [diagnostico2_idtratamiento2_nombre]     varchar(300) NULL,
		   [diagnostico2_idtratamiento3_nombre]     varchar(300) NULL,

		   [diagnostico3_id]     INT NULL,
		   [diagnostico3_nombre]     varchar(200) NULL,
		   [diagnostico3_idtratamiento1]     varchar(10) NULL,
		   [diagnostico3_idtratamiento2]     varchar(10) NULL,
		   [diagnostico3_idtratamiento3]     varchar(10) NULL,
		   [diagnostico3_idtratamiento1_nombre]     varchar(300) NULL,
		   [diagnostico3_idtratamiento2_nombre]     varchar(300) NULL,
		   [diagnostico3_idtratamiento3_nombre]     varchar(300) NULL,

		   [diagnostico4_id]     INT NULL,
		   [diagnostico4_nombre]     varchar(200) NULL,
		   [diagnostico4_idtratamiento1]     varchar(10) NULL,
		   [diagnostico4_idtratamiento2]     varchar(10) NULL,
		   [diagnostico4_idtratamiento3]     varchar(10) NULL,
		   [diagnostico4_idtratamiento1_nombre]     varchar(300) NULL,
		   [diagnostico4_idtratamiento2_nombre]     varchar(300) NULL,
		   [diagnostico4_idtratamiento3_nombre]     varchar(300) NULL,

		   [diagnostico5_id]     INT NULL,
		   [diagnostico5_nombre]     varchar(200) NULL,
		   [diagnostico5_idtratamiento1]     varchar(10) NULL,
		   [diagnostico5_idtratamiento2]     varchar(10) NULL,
		   [diagnostico5_idtratamiento3]     varchar(10) NULL,
		   [diagnostico5_idtratamiento1_nombre]     varchar(300) NULL,
		   [diagnostico5_idtratamiento2_nombre]     varchar(300) NULL,
		   [diagnostico5_idtratamiento3_nombre]     varchar(300) NULL
        ) 

      INSERT INTO #tmp_atencion 
                  (
				  id_atencion,
				  id_institucion,
				  id_beneficiario,
				  nombrecompleto,
				  documento,
				  residencia,
				  id_periodo,
				  fecha_atencion,
				  id_area
				  ) 
      SELECT 
	        atencion.id_atencion,
		    atencion.ID_INSTITUCION,
			bene.ID_BENEFICIARIO,
			entidad.NOMBRECOMPLETO,
			entidad.DOCUMENTO,
			area.NOMBRE RESIDENCIA,
			atencion.ID_PERIODO,
			atencion.FECHA_ATENCION,
			area.ID_AREA
      	from sgseguridadsys.PS_BENEFICIARIO bene
			JOIN sgseguridadsys.PS_ENTIDAD entidad ON entidad.ID_ENTIDAD=bene.ID_BENEFICIARIO
			LEFT JOIN sgseguridadsys.PS_INSTITUCION_AREA area ON area.ID_INSTITUCION=bene.ID_INSTITUCION
			AND bene.ID_AREA=area.ID_AREA
            JOIN sgseguridadsys.PS_ATENCION atencion ON atencion.ID_BENEFICIARIO=bene.ID_BENEFICIARIO
			AND atencion.ID_INSTITUCION=bene.ID_INSTITUCION
        WHERE atencion.FECHA_ATENCION between @pFechaInicio and @pFechaFin 
            AND ISNULL(entidad.NombreCompleto,'XX') LIKE '%' + ISNULL(@pNombreCompleto,ISNULL(entidad.NombreCompleto,'XX')) + '%'
			AND atencion.ID_INSTITUCION=@pInstitucion
			AND bene.ID_PROGRAMA=@pPrograma
            AND ISNULL(bene.id_area,'XX')=ISNULL(@pArea,ISNULL(bene.id_area,'XX'))
            AND ISNULL(entidad.ID_SEXO,'XX')=ISNULL(@pSexo,ISNULL(entidad.ID_SEXO,'XX'))
			AND atencion.ID_TIPO_ATENCION=@TipoRas

      DECLARE insertaatenciondetalle CURSOR FOR 

        SELECT 
			id_atencion,
			id_institucion,
			id_beneficiario,
			nombrecompleto,
			documento,
			residencia,
			id_periodo,
			fecha_atencion,
			id_area
        FROM   #tmp_atencion 

      OPEN insertaatenciondetalle 
      FETCH insertaatenciondetalle 
	  INTO @v_ID_ATENCION,
	       @v_ID_INSTITUCION,
	       @v_ID_BENEFICIARIO,
	       @v_NOMBRECOMPLETO,
	       @v_DOCUMENTO,  
	       @v_RESIDENCIA,  
	       @v_ID_PERIODO, 
	       @v_FECHA_ATENCION,
	       @v_ID_AREA 

      WHILE @@fetch_status = 0 
        BEGIN 
			/**/	

				DECLARE cur_diagnostico CURSOR FOR 

					SELECT ROW_NUMBER() OVER(ORDER BY DD.POSICION),DD.ID_DIAGNOSTICO, dia.Nombre
					FROM sgseguridadsys.PS_atencion_detalle dd
							inner join sgseguridadsys.SS_ge_diagnostico dia
							on dd.ID_DIAGNOSTICO = dia.IdDiagnostico
					WHERE dd.ID_ATENCION = @v_ID_ATENCION
					-- AND dd.ID_DIAGNOSTICO=ISNULL(@pIdDiagnosticos,dd.ID_DIAGNOSTICO)
					ORDER BY DD.POSICION

				  OPEN cur_diagnostico 
				  FETCH cur_diagnostico INTO @v_DIAGNOSTICO_POSICION, @v_ID_DIAGNOSTICO, @v_DIAGNOSTICO_NOMBRE
				  WHILE @@fetch_status = 0 
					BEGIN 
						/**/						
						PRINT @v_ID_ATENCION
						PRINT @v_ID_DIAGNOSTICO
						SET @v_ID_TRATAMIENTO1 = NULL;
						SET @v_ID_TRATAMIENTO2 = NULL;
						SET @v_ID_TRATAMIENTO3 = NULL;

						SET @v_ID_TRATAMIENTO1_NOMBRE = NULL;
						SET @v_ID_TRATAMIENTO2_NOMBRE = NULL;
						SET @v_ID_TRATAMIENTO3_NOMBRE = NULL;


						SET @v_ID_TRATAMIENTO1 = 
						(SELECT ID_TRATAMIENTO
						    FROM 
						(SELECT ROW_NUMBER() OVER(ORDER BY T.POSICION_TRATAMIENTO) POS, T.ID_TRATAMIENTO
						FROM sgseguridadsys.PS_atencion_TRATAMIENTO T
						WHERE T.ID_ATENCION = @v_ID_ATENCION
						AND T.ID_DIAGNOSTICO = @v_ID_DIAGNOSTICO
						) SC
						WHERE SC.POS=1)


						SET @v_ID_TRATAMIENTO2 = (SELECT ID_TRATAMIENTO
						FROM 
						(SELECT ROW_NUMBER() OVER(ORDER BY T.POSICION_TRATAMIENTO) POS, T.ID_TRATAMIENTO
						FROM sgseguridadsys.PS_atencion_TRATAMIENTO T
						WHERE T.ID_ATENCION = @v_ID_ATENCION
						AND T.ID_DIAGNOSTICO = @v_ID_DIAGNOSTICO
						) SC
						WHERE SC.POS=2)

						SET @v_ID_TRATAMIENTO3 = (SELECT ID_TRATAMIENTO
						FROM 
						(SELECT ROW_NUMBER() OVER(ORDER BY T.POSICION_TRATAMIENTO) POS, T.ID_TRATAMIENTO
						FROM sgseguridadsys.PS_atencion_TRATAMIENTO T
						WHERE T.ID_ATENCION = @v_ID_ATENCION
						AND T.ID_DIAGNOSTICO = @v_ID_DIAGNOSTICO
						) SC
						WHERE SC.POS=3)

						--//***************NOMBRES ********************

					

					 SET @v_ID_TRATAMIENTO1_NOMBRE= 
						(SELECT DescripcionLocal
						    FROM 
						(SELECT ROW_NUMBER() OVER(ORDER BY T.POSICION_TRATAMIENTO) POS, detalle.DescripcionLocal
						FROM sgseguridadsys.PS_atencion_TRATAMIENTO T
						JOIN MA_MiscelaneosDetalle detalle ON detalle.AplicacionCodigo='PS'
						AND detalle.CodigoTabla='RASTRATA'
						AND detalle.CodigoElemento=T.ID_TRATAMIENTO
						WHERE T.ID_ATENCION = @v_ID_ATENCION
						AND T.ID_DIAGNOSTICO = @v_ID_DIAGNOSTICO
						) SC
						WHERE SC.POS=1)


					 SET @v_ID_TRATAMIENTO2_NOMBRE= 
						(SELECT DescripcionLocal
						    FROM 
						(SELECT ROW_NUMBER() OVER(ORDER BY T.POSICION_TRATAMIENTO) POS, detalle.DescripcionLocal
						FROM sgseguridadsys.PS_atencion_TRATAMIENTO T
						JOIN MA_MiscelaneosDetalle detalle ON detalle.AplicacionCodigo='PS'
						AND detalle.CodigoTabla='RASTRATA'
						AND detalle.CodigoElemento=T.ID_TRATAMIENTO
						WHERE T.ID_ATENCION = @v_ID_ATENCION
						AND T.ID_DIAGNOSTICO = @v_ID_DIAGNOSTICO
						) SC
						WHERE SC.POS=2)

					 SET @v_ID_TRATAMIENTO3_NOMBRE= 
						(SELECT DescripcionLocal
						    FROM 
						(SELECT ROW_NUMBER() OVER(ORDER BY T.POSICION_TRATAMIENTO) POS, detalle.DescripcionLocal
						FROM sgseguridadsys.PS_atencion_TRATAMIENTO T
						JOIN MA_MiscelaneosDetalle detalle ON detalle.AplicacionCodigo='PS'
						AND detalle.CodigoTabla='RASTRATA'
						AND detalle.CodigoElemento=T.ID_TRATAMIENTO
						WHERE T.ID_ATENCION = @v_ID_ATENCION
						AND T.ID_DIAGNOSTICO = @v_ID_DIAGNOSTICO
						) SC
						WHERE SC.POS=3)


						if @v_DIAGNOSTICO_POSICION = 1  
							begin
								update #tmp_atencion set 
									[diagnostico1_id] = @v_ID_DIAGNOSTICO
									,[diagnostico1_nombre] = @v_DIAGNOSTICO_NOMBRE
									,[diagnostico1_idtratamiento1] = @v_ID_TRATAMIENTO1
									,[diagnostico1_idtratamiento2] = @v_ID_TRATAMIENTO2
									,[diagnostico1_idtratamiento3] = @v_ID_TRATAMIENTO3
									,[diagnostico1_idtratamiento1_nombre] = @v_ID_TRATAMIENTO1_NOMBRE
									,[diagnostico1_idtratamiento2_nombre] = @v_ID_TRATAMIENTO2_NOMBRE
									,[diagnostico1_idtratamiento3_nombre] = @v_ID_TRATAMIENTO3_NOMBRE

								where id_atencion = @v_ID_ATENCION
							end
						if @v_DIAGNOSTICO_POSICION = 2  
							begin
								update #tmp_atencion set 
									[diagnostico2_id] = @v_ID_DIAGNOSTICO
									,[diagnostico2_nombre] = @v_DIAGNOSTICO_NOMBRE
									,[diagnostico2_idtratamiento1] = @v_ID_TRATAMIENTO1
									,[diagnostico2_idtratamiento2] = @v_ID_TRATAMIENTO2
									,[diagnostico2_idtratamiento3] = @v_ID_TRATAMIENTO3
									,[diagnostico2_idtratamiento1_nombre] = @v_ID_TRATAMIENTO1_NOMBRE
									,[diagnostico2_idtratamiento2_nombre] = @v_ID_TRATAMIENTO2_NOMBRE
									,[diagnostico2_idtratamiento3_nombre] = @v_ID_TRATAMIENTO3_NOMBRE
								where id_atencion = @v_ID_ATENCION
							end
						if @v_DIAGNOSTICO_POSICION = 3  
							begin
								update #tmp_atencion set 
									[diagnostico3_id] = @v_ID_DIAGNOSTICO
									,[diagnostico3_nombre] = @v_DIAGNOSTICO_NOMBRE
									,[diagnostico3_idtratamiento1] = @v_ID_TRATAMIENTO1
									,[diagnostico3_idtratamiento2] = @v_ID_TRATAMIENTO2
									,[diagnostico3_idtratamiento3] = @v_ID_TRATAMIENTO3
									,[diagnostico3_idtratamiento1_nombre] = @v_ID_TRATAMIENTO1_NOMBRE
									,[diagnostico3_idtratamiento2_nombre] = @v_ID_TRATAMIENTO2_NOMBRE
									,[diagnostico3_idtratamiento3_nombre] = @v_ID_TRATAMIENTO3_NOMBRE
								where id_atencion = @v_ID_ATENCION
							end
						if @v_DIAGNOSTICO_POSICION = 4  
							begin
								update #tmp_atencion set 
									[diagnostico4_id] = @v_ID_DIAGNOSTICO
									,[diagnostico4_nombre] = @v_DIAGNOSTICO_NOMBRE
									,[diagnostico4_idtratamiento1] = @v_ID_TRATAMIENTO1
									,[diagnostico4_idtratamiento2] = @v_ID_TRATAMIENTO2
									,[diagnostico4_idtratamiento3] = @v_ID_TRATAMIENTO3
									,[diagnostico4_idtratamiento1_nombre] = @v_ID_TRATAMIENTO1_NOMBRE
									,[diagnostico4_idtratamiento2_nombre] = @v_ID_TRATAMIENTO2_NOMBRE
									,[diagnostico4_idtratamiento3_nombre] = @v_ID_TRATAMIENTO3_NOMBRE
								where id_atencion = @v_ID_ATENCION
							end
						if @v_DIAGNOSTICO_POSICION = 5  
							begin
								update #tmp_atencion set 
									[diagnostico5_id] = @v_ID_DIAGNOSTICO
									,[diagnostico5_nombre] = @v_DIAGNOSTICO_NOMBRE
									,[diagnostico5_idtratamiento1] = @v_ID_TRATAMIENTO1
									,[diagnostico5_idtratamiento2] = @v_ID_TRATAMIENTO2
									,[diagnostico5_idtratamiento3] = @v_ID_TRATAMIENTO3
									,[diagnostico5_idtratamiento1_nombre] = @v_ID_TRATAMIENTO1_NOMBRE
									,[diagnostico5_idtratamiento2_nombre] = @v_ID_TRATAMIENTO2_NOMBRE
									,[diagnostico5_idtratamiento3_nombre] = @v_ID_TRATAMIENTO3_NOMBRE
								where id_atencion = @v_ID_ATENCION
							end
						/**/
						FETCH cur_diagnostico INTO @v_DIAGNOSTICO_POSICION, @v_ID_DIAGNOSTICO, @v_DIAGNOSTICO_NOMBRE
					END 
				  CLOSE cur_diagnostico 
				  DEALLOCATE cur_diagnostico 
			
			/**/
            FETCH insertaatenciondetalle 
			INTO   @v_ID_ATENCION,
				   @v_ID_INSTITUCION,
				   @v_ID_BENEFICIARIO,
				   @v_NOMBRECOMPLETO,
				   @v_DOCUMENTO,  
				   @v_RESIDENCIA,  
				   @v_ID_PERIODO, 
				   @v_FECHA_ATENCION,
				   @v_ID_AREA
        END 
      CLOSE insertaatenciondetalle 
      DEALLOCATE insertaatenciondetalle 
	     
		 
		  SELECT
		              reg.id_institucion,
					  reg.id_beneficiario,
					  reg.nombrecompleto,
					  reg.fecha_atencion,
					  reg.documento,
					  reg.id_periodo,
					  reg.residencia,
					  reg.id_area,
					  reg.id_atencion,
					  reg.diagnostico1_id,
					  reg.diagnostico1_nombre,
					  reg.diagnostico1_idtratamiento1,
					  reg.diagnostico1_idtratamiento2,
					  reg.diagnostico1_idtratamiento3,
					  reg.diagnostico2_id,
					  reg.diagnostico2_nombre,
					  reg.diagnostico2_idtratamiento1,
					  reg.diagnostico2_idtratamiento2,
					  reg.diagnostico2_idtratamiento3,
					  reg.diagnostico3_id,
					  reg.diagnostico3_nombre,
					  reg.diagnostico3_idtratamiento1,
					  reg.diagnostico3_idtratamiento2,
					  reg.diagnostico3_idtratamiento3,
					  reg.diagnostico4_id,
					  reg.diagnostico4_nombre,
					  reg.diagnostico4_idtratamiento1,
					  reg.diagnostico4_idtratamiento2,
					  reg.diagnostico4_idtratamiento3,
					  reg.diagnostico5_id,
					  reg.diagnostico5_nombre,
					  reg.diagnostico5_idtratamiento1,
					  reg.diagnostico5_idtratamiento2,
					  reg.diagnostico5_idtratamiento3,
					  reg.diagnostico1_idtratamiento1_nombre,
					  reg.diagnostico1_idtratamiento2_nombre,
					  reg.diagnostico1_idtratamiento3_nombre,

					  reg.diagnostico2_idtratamiento1_nombre,
					  reg.diagnostico2_idtratamiento2_nombre,
					  reg.diagnostico2_idtratamiento3_nombre,

					  reg.diagnostico3_idtratamiento1_nombre,
					  reg.diagnostico3_idtratamiento2_nombre,
					  reg.diagnostico3_idtratamiento3_nombre,
					  					  
					  reg.diagnostico4_idtratamiento1_nombre,
					  reg.diagnostico4_idtratamiento2_nombre,
					  reg.diagnostico4_idtratamiento3_nombre,

					  reg.diagnostico5_idtratamiento1_nombre,
					  reg.diagnostico5_idtratamiento2_nombre,
					  reg.diagnostico5_idtratamiento3_nombre,
					  reg.sec
		  FROM
		  (

				  SELECT 
				      ROW_NUMBER() OVER(ORDER BY ID_BENEFICIARIO ASC) AS sec,
					  id_institucion,
					  id_beneficiario,
					  nombrecompleto,
					  fecha_atencion,
					  documento,
					  id_periodo,
					  residencia,
					  id_area,
					  id_atencion,
					  diagnostico1_id,
					  diagnostico1_nombre,
					  diagnostico1_idtratamiento1,
					  diagnostico1_idtratamiento2,
					  diagnostico1_idtratamiento3,
					  diagnostico2_id,
					  diagnostico2_nombre,
					  diagnostico2_idtratamiento1,
					  diagnostico2_idtratamiento2,
					  diagnostico2_idtratamiento3,
					  diagnostico3_id,
					  diagnostico3_nombre,
					  diagnostico3_idtratamiento1,
					  diagnostico3_idtratamiento2,
					  diagnostico3_idtratamiento3,
					  diagnostico4_id,
					  diagnostico4_nombre,
					  diagnostico4_idtratamiento1,
					  diagnostico4_idtratamiento2,
					  diagnostico4_idtratamiento3,
					  diagnostico5_id,
					  diagnostico5_nombre,
					  diagnostico5_idtratamiento1,
					  diagnostico5_idtratamiento2,
					  diagnostico5_idtratamiento3,
					  diagnostico1_idtratamiento1_nombre,
					  diagnostico1_idtratamiento2_nombre,
					  diagnostico1_idtratamiento3_nombre,

					  diagnostico2_idtratamiento1_nombre,
					  diagnostico2_idtratamiento2_nombre,
					  diagnostico2_idtratamiento3_nombre,

					  diagnostico3_idtratamiento1_nombre,
					  diagnostico3_idtratamiento2_nombre,
					  diagnostico3_idtratamiento3_nombre,
					  					  
					  diagnostico4_idtratamiento1_nombre,
					  diagnostico4_idtratamiento2_nombre,
					  diagnostico4_idtratamiento3_nombre,

					  diagnostico5_idtratamiento1_nombre,
					  diagnostico5_idtratamiento2_nombre,
					  diagnostico5_idtratamiento3_nombre
					   
				  FROM   #tmp_atencion 
				  WHERE 
				  (
				  ISNULL(diagnostico1_id,0)=ISNULL(@pIdDiagnosticos,ISNULL(diagnostico1_id,0))
				  OR
				  ISNULL(diagnostico2_id,0)=ISNULL(@pIdDiagnosticos,ISNULL(diagnostico2_id,0))
				  OR
				  ISNULL(diagnostico3_id,0)=ISNULL(@pIdDiagnosticos,ISNULL(diagnostico3_id,0))
				  )
           ) as reg

				  WHERE  reg.sec BETWEEN  (@pNumPag + 1)  AND 
					(@pNumPag + @pNumReg) 
				
				 
	     
		 END