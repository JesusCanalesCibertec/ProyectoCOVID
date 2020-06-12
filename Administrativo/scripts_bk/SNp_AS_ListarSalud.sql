
 ALTER PROCEDURE [sgseguridadsys].[SNp_AS_ListarSalud] ( @pNombreCompleto varchar(200), @pPeriodo varchar(8) , @pArea varchar(10), @pSexo varchar(1), @pNumPag int, @pNumReg int, @pInstitucion varchar(6), @pPrograma varchar(3) ) 
 AS 		
 -- exec [sgseguridadsys].[SNp_AS_ListarSalud] null, '2018S1',null,null,0,10,'CARID','AAM'
      BEGIN
         SELECT
            record.ID_INSTITUCION,
            record.ID_BENEFICIARIO,
            record.ID_SALUD,
            record.NOMBRECOMPLETO,
            record.FECHA_INFORME,
            record.ID_PERIODO,
            record.COMENTARIOS,
            record.ESTADO,
            record.HEMOGLOBINA,
            record.HEMOGLOBINA_RESULTADO,
            record.ID_AYUDA_MEDICA,
            record.ID_DESCARTE_SEROLOGICO,
            record.ID_DESCARTE_TBC,
            record.ID_TRATAMIENTO_ANEMIA,
            record.ID_HTA,
            record.ID_TBC,
            record.ID_DIABETES,
            record.ID_COGNITIVO,
            record.ID_AFECTIVO,
            record. ID_OSTEOARTROSIS,
            record.NOMBRE,
            record.ID_PROGRAMA,
            record.esNuevo,
            record.Id_PRUEBA_VIH,
            record.EDAD_MENARQUIA,
            record.FECHA_ULTIMA_MESTRUACION,
            record. OTRAS_ENFERMEDADES,
            record.DescripcionLocal ayudaMedica,
			record.afectivo,
			record.cognitivo,
			record.cantidad,
			record.EVALUADO,
            record.sec
         FROM
            (
               SELECT
                  ROW_NUMBER() OVER(
               ORDER BY
                  reg.ID_BENEFICIARIO ASC) AS sec,
                  reg.ID_INSTITUCION,
                  reg.ID_BENEFICIARIO,
                  reg.ID_SALUD,
                  reg.NOMBRECOMPLETO,
                  reg.FECHA_INFORME,
                  reg.ID_PERIODO,
                  reg.COMENTARIOS,
                  reg.ESTADO,
                  reg.HEMOGLOBINA,
                  reg.HEMOGLOBINA_RESULTADO,
                  reg.ID_AYUDA_MEDICA,
                  reg.ID_DESCARTE_SEROLOGICO,
                  reg.ID_DESCARTE_TBC,
                  reg.ID_TRATAMIENTO_ANEMIA,
                  reg.ID_HTA,
                  reg.ID_TBC,
                  reg.ID_DIABETES,
                  reg.ID_COGNITIVO,
                  reg.ID_AFECTIVO,
                  reg. ID_OSTEOARTROSIS,
                  reg.NOMBRE,
                  reg.ID_PROGRAMA,
                  reg.esNuevo,
                  reg.Id_PRUEBA_VIH,
                  reg.EDAD_MENARQUIA,
                  reg.FECHA_ULTIMA_MESTRUACION,
                  reg. OTRAS_ENFERMEDADES,
				  reg.DescripcionLocal,
				  reg.afectivo,
				  reg.cognitivo,
                  (
                     SELECT
                        COUNT(1) 
                     FROM
                        (
                           SELECT
                              salud.ID_BENEFICIARIO 
                           FROM
                              sgseguridadsys.PS_SALUD salud 
                              JOIN
                                 sgseguridadsys.PS_BENEFICIARIO bene 
                                 ON bene.ID_BENEFICIARIO = salud.ID_BENEFICIARIO 
								  AND bene.ID_INSTITUCION=salud.ID_INSTITUCION
                              JOIN
                                 sgseguridadsys.PS_ENTIDAD entidad 
                                 ON entidad.ID_ENTIDAD = bene.ID_BENEFICIARIO 
                              JOIN
                                 sgseguridadsys.PS_INSTITUCION ints 
                                 ON ints.ID_INSTITUCION = bene.ID_INSTITUCION 
                           WHERE
                              salud.ID_PERIODO = @pPeriodo 
                              AND salud.ID_INSTITUCION = ISNULL(@pInstitucion, salud.ID_INSTITUCION) 
                              AND ISNULL(bene.id_area, 'XX') = ISNULL(@pArea, ISNULL(bene.id_area, 'XX')) 
                              AND ISNULL(entidad.ID_SEXO, 'XX') = ISNULL(@pSexo, ISNULL(entidad.ID_SEXO, 'XX')) 
                  and 
				  (entidad.APELLIDO_PATERNO+' ' +entidad.APELLIDO_MATERNO +' '+entidad.NOMBRES like '%' + isnull(@pNombreCompleto, '') + '%'
  
				  or
				  entidad.NOMBRES+' ' +entidad.APELLIDO_PATERNO +' '+entidad.APELLIDO_MATERNO like '%' + isnull(@pNombreCompleto, '') + '%'
				  )

                              AND bene.ID_PROGRAMA = @pPrograma 
                              AND bene.ESTADO = 'ACT' 
                              AND salud.ID_ORIGEN = 'EVA' 

                           UNION ALL


                           SELECT
                              bene.ID_BENEFICIARIO 
                           from
                              sgseguridadsys.PS_BENEFICIARIO bene 
                              JOIN
                                 sgseguridadsys.PS_ENTIDAD entidad 
                                 ON entidad.ID_ENTIDAD = bene.ID_BENEFICIARIO 

                              JOIN
                                 sgseguridadsys.PS_INSTITUCION ints 
                                 ON ints.ID_INSTITUCION = bene.ID_INSTITUCION 
                           WHERE
                              not exists 
                              (
                                 SELECT
                                    salud.ID_BENEFICIARIO 
                                 FROM
                                    sgseguridadsys.PS_SALUD salud 
                                 WHERE
                                    salud.ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                                    AND salud.ID_INSTITUCION = bene.ID_INSTITUCION 
                                    AND salud.ID_PERIODO = @pPeriodo 
                                    AND bene.ESTADO = 'ACT' 
                                    AND salud.ID_ORIGEN = 'EVA' 
                              )
                              AND ISNULL(bene.id_area, 'XX') = ISNULL(@pArea, ISNULL(bene.id_area, 'XX')) 
                              AND ISNULL(entidad.ID_SEXO, 'XX') = ISNULL(@pSexo, ISNULL(entidad.ID_SEXO, 'XX')) 
                    and 
				  (entidad.APELLIDO_PATERNO+' ' +entidad.APELLIDO_MATERNO +' '+entidad.NOMBRES like '%' + isnull(@pNombreCompleto, '') + '%'
  
				  or
				  entidad.NOMBRES+' ' +entidad.APELLIDO_PATERNO +' '+entidad.APELLIDO_MATERNO like '%' + isnull(@pNombreCompleto, '') + '%'
				  )
                              AND bene.ID_INSTITUCION = @pInstitucion 
                              AND bene.ID_PROGRAMA = @pPrograma 
                              AND bene.ESTADO = 'ACT' 
                        )
                        as r 
                  )
                  AS cantidad,
				  reg.EVALUADO
               FROM
                  (
                     select
                        salud.ID_INSTITUCION,
                        salud.ID_BENEFICIARIO,
                        salud.ID_SALUD,
                        entidad.APELLIDO_PATERNO+' ' +entidad.APELLIDO_MATERNO +' '+entidad.NOMBRES as NOMBRECOMPLETO,
                        salud.FECHA_INFORME,
                        salud.ID_PERIODO,
                        salud.COMENTARIOS,
                        salud.ESTADO,
                        salud.HEMOGLOBINA,
                        salud.HEMOGLOBINA_RESULTADO,
                        salud.ID_AYUDA_MEDICA,
                        salud.ID_DESCARTE_SEROLOGICO,
                        salud.ID_DESCARTE_TBC,
                        salud.ID_TRATAMIENTO_ANEMIA,
                        ID_HTA,
                        ID_TBC,
                        ID_DIABETES,
                        ID_COGNITIVO,
                        ID_AFECTIVO,
                        ID_OSTEOARTROSIS,
                        ints.NOMBRE,
                        bene.ID_PROGRAMA,
                        'N' esNuevo,
                        Id_PRUEBA_VIH,
                        EDAD_MENARQUIA,
                        FECHA_ULTIMA_MESTRUACION,
                        OTRAS_ENFERMEDADES,
						ayudaMedica.DescripcionLocal,
						afectivo.DescripcionLocal afectivo,
						cognitivo.DescripcionLocal cognitivo,
						salud.EVALUADO

                     FROM
                        sgseguridadsys.PS_SALUD salud 
                        JOIN
                           sgseguridadsys.PS_BENEFICIARIO bene 
                           ON bene.ID_BENEFICIARIO = salud.ID_BENEFICIARIO 
						    AND bene.ID_INSTITUCION=salud.ID_INSTITUCION
                        JOIN
                           sgseguridadsys.PS_ENTIDAD entidad 
                           ON entidad.ID_ENTIDAD = bene.ID_BENEFICIARIO 
                        JOIN
                           sgseguridadsys.PS_INSTITUCION ints 
                           ON ints.ID_INSTITUCION = bene.ID_INSTITUCION 
						LEFT JOIN MA_MiscelaneosDetalle ayudaMedica ON
						  ayudaMedica.AplicacionCodigo='PS' AND
						  ayudaMedica.CodigoTabla='TIPAYUMEDI' AND 
						  ayudaMedica.CodigoElemento=salud.ID_AYUDA_MEDICA

						LEFT JOIN MA_MiscelaneosDetalle cognitivo ON
						  cognitivo.AplicacionCodigo='PS' AND
						  cognitivo.CodigoTabla='VMENCOG' AND 
						  cognitivo.CodigoElemento=salud.ID_COGNITIVO

						LEFT JOIN MA_MiscelaneosDetalle afectivo ON
						  afectivo.AplicacionCodigo='PS' AND
						  afectivo.CodigoTabla='VMENAFE' AND 
						  afectivo.CodigoElemento=salud.ID_AFECTIVO

                     WHERE
                        salud.ID_PERIODO = @pPeriodo 
                        AND salud.ID_INSTITUCION = ISNULL(@pInstitucion, salud.ID_INSTITUCION) 
                        AND ISNULL(bene.id_area, 'XX') = ISNULL(@pArea, ISNULL(bene.id_area, 'XX')) 
                        AND ISNULL(entidad.ID_SEXO, 'XX') = ISNULL(@pSexo, ISNULL(entidad.ID_SEXO, 'XX')) 
                   and 
				  (entidad.APELLIDO_PATERNO+' ' +entidad.APELLIDO_MATERNO +' '+entidad.NOMBRES like '%' + isnull(@pNombreCompleto, '') + '%'
  
				  or
				  entidad.NOMBRES+' ' +entidad.APELLIDO_PATERNO +' '+entidad.APELLIDO_MATERNO like '%' + isnull(@pNombreCompleto, '') + '%'
				  )
                        AND bene.ID_PROGRAMA = @pPrograma 
                        AND bene.ESTADO = 'ACT' 
                        AND salud.ID_ORIGEN = 'EVA' 

                     UNION ALL

                     select
                        bene.ID_INSTITUCION,
                        bene.ID_BENEFICIARIO,
                        null,
                        			entidad.APELLIDO_PATERNO+' ' +entidad.APELLIDO_MATERNO +' '+entidad.NOMBRES as NOMBRECOMPLETO,
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
                        ints.NOMBRE,
                        bene.ID_PROGRAMA,
                        'S' esNuevo,
                        null,
                        null,
                        null,
						null,
						null,
						null,
                        null,
						null 
                     from
                        sgseguridadsys.PS_BENEFICIARIO bene 
                        JOIN
                           sgseguridadsys.PS_ENTIDAD entidad 
                           ON entidad.ID_ENTIDAD = bene.ID_BENEFICIARIO 
                        JOIN
                           sgseguridadsys.PS_INSTITUCION ints 
                           ON ints.ID_INSTITUCION = bene.ID_INSTITUCION 
                     WHERE
                        not exists 
                        (
                           SELECT
                              salud.ID_BENEFICIARIO 
                           FROM
                              sgseguridadsys.PS_SALUD salud 
                           WHERE
                              salud.ID_BENEFICIARIO = bene.ID_BENEFICIARIO 
                              AND salud.ID_INSTITUCION = bene.ID_INSTITUCION 
                              AND salud.ID_PERIODO = @pPeriodo 
                              AND bene.ESTADO = 'ACT' 
                              AND salud.ID_ORIGEN = 'EVA' 
                        )
                        AND ISNULL(bene.id_area, 'XX') = ISNULL(@pArea, ISNULL(bene.id_area, 'XX')) 
                        AND ISNULL(entidad.ID_SEXO, 'XX') = ISNULL(@pSexo, ISNULL(entidad.ID_SEXO, 'XX')) 
  and 
				  (entidad.APELLIDO_PATERNO+' ' +entidad.APELLIDO_MATERNO +' '+entidad.NOMBRES like '%' + isnull(@pNombreCompleto, '') + '%'
  
				  or
				  entidad.NOMBRES+' ' +entidad.APELLIDO_PATERNO +' '+entidad.APELLIDO_MATERNO like '%' + isnull(@pNombreCompleto, '') + '%'
				  )
                        AND bene.ID_INSTITUCION = @pInstitucion 
                        AND bene.ID_PROGRAMA = @pPrograma 
                        AND bene.ESTADO = 'ACT' 
                  )
                  reg 
            )
            as record 
         WHERE
            record.sec BETWEEN (@pNumPag + 1) AND 
            (
               @pNumPag + @pNumReg
            )
      END