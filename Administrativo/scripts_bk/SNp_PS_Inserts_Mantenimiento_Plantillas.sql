
select * from SeguridadPerfilUsuario where Usuario = 'altorres'
select * from SeguridadAutorizaciones where Usuario = 'PERPS_FUNDA_ADMIN'
select * from SeguridadConcepto where AplicacionCodigo = 'PS' 


insert into SeguridadConcepto values ('PS','GRUPO7','PLTARE','Plantilla Tareas','N','M','m_prepayment_print','N',null,'1','1','1','1',getdate(),'1',null,'S',null,'S','plantilla-tarea-listado',null,null,null,null,null)

insert into SeguridadConcepto values ('PS','GRUPO7','PLENCU','Plantilla Encuestas','N','M','m_prepayment_print','N',null,'1','1','1','1',getdate(),'1',null,'S',null,'S','encuesta-plantilla-listado',null,null,null,null,null)

insert into SeguridadConcepto values ('PS','GRUPO7','PLCONS','Plantilla Consumos','N','M','m_prepayment_print','N',null,'1','1','1','1',getdate(),'1',null,'S',null,'S','consumo-plantilla-listado',null,null,null,null,null)


insert into SeguridadAutorizaciones values ('PS','GRUPO7','PLTARE','PERPS_FUNDA_ADMIN',NULL	,NULL,	NULL,	'A'	,NULL	,NULL	,NULL,	NULL)

insert into SeguridadAutorizaciones values ('PS','GRUPO7','PLENCU','PERPS_FUNDA_ADMIN',NULL	,NULL,	NULL,	'A'	,NULL	,NULL	,NULL,	NULL)

insert into SeguridadAutorizaciones values ('PS','GRUPO7','PLCONS','PERPS_FUNDA_ADMIN',NULL	,NULL,	NULL,	'A'	,NULL	,NULL	,NULL,	NULL)

select * from sgseguridadsys.RT_INDICADOR where ID_INDICADOR = 'DNC-03'

SELECT 
 a.ID_INDICADOR,
 a.NOMBRE as NOM_INDICADOR,
 case(a.ID_PROGRAMA)
 when 'NCD' then 'Programa Niñas, Niños y Adolescentes con Discapacidad'
 else b.NOMBRE
 end as NOM_PROGRAMA,
 c.NOMBRE as NOM_COMPONENTE,
 case a.ESTADO
 when 'A' then 'Activo'
 when 'I' then 'Inactivo'
 end as Estado,
 ROW_NUMBER() OVER (ORDER BY ID_INDICADOR desc) AS Seq
 FROM sgseguridadsys.RT_INDICADOR a left JOIN sgseguridadsys.PS_PROGRAMA b on a.ID_PROGRAMA = b.ID_PROGRAMA
				    left JOIN sgseguridadsys.PS_COMPONENTE c on a.ID_COMPONENTE = c.ID_COMPONENTE
where 
a.ID_INDICADOR like '%' +  isnull('DNC-03', '') + '%'
and a.NOMBRE like '%' + isnull(null, '') + '%'
and ISNULL(a.ESTADO, '123') = isnull(null, ISNULL(a.ESTADO, '123'))

SELECT * FROM sgseguridadsys.RT_INDICADOR WHERE ID_INDICADOR = 'DNC-03'


select * from usuario

select * from sgseguridadsys.PS_ENTIDAD where NOMBRECOMPLETO like 't%'

select a.PLANTILLA, a.DESCRIPCION, a.ESTADO, a.ID_INSTITUCION,b.NOMBRE
		,ROW_NUMBER() OVER (ORDER BY DAY(a.plantilla)) AS Seq
		from sgseguridadsys.PS_CONSUMO_PLANTILLA a 
			 left join sgseguridadsys.PS_INSTITUCION b on a.ID_INSTITUCION= b.ID_INSTITUCION
		where 
		a.id_institucion = isnull(null, a.id_institucion) and
		a.plantilla = isnull(null, a.plantilla) and
		a.descripcion like '%'+isnull(null, '')+'%' and
		a.ESTADO = isnull(null, a.ESTADO)


		select a.capacitacion, a.estado from HR_Capacitacion a 



		select * from sgseguridadsys.PS_MARCO_LOGICO_RESULTADO

		update sgseguridadsys.PS_MARCO_LOGICO_RESULTADO 
		set NOMBRE = 'Nivel de Satisfacción del Servicio de Nutrición' where ID_RESULTADO = 'RES05'



