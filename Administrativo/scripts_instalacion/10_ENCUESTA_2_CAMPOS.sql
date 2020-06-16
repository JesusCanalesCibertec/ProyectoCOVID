/* HR_ENCUESTA */
if(not exists(select * from sys.objects o inner join  sys.columns c on c.object_id=o.object_id 
	where o.name='HR_ENCUESTA' and c.name='ESTADO'))
	ALTER TABLE HR_ENCUESTA ADD ESTADO CHAR(1)
go
if(not exists(select * from sys.objects o inner join  sys.columns c on c.object_id=o.object_id 
	where o.name='HR_ENCUESTA' and c.name='FECHAHASTA'))
	ALTER TABLE HR_ENCUESTA ADD FECHAHASTA DATETIME
go
if(not exists(select * from sys.objects o inner join  sys.columns c on c.object_id=o.object_id 
	where o.name='HR_ENCUESTA' and c.name='FECHADESDE'))
	ALTER TABLE HR_ENCUESTA ADD FECHADESDE DATETIME
go
/* HR_ENCUESTASUJETO */
if(not exists(select * from sys.objects o inner join  sys.columns c on c.object_id=o.object_id 
	where o.name='HR_ENCUESTASUJETO' and c.name='ORDEN'))
	ALTER TABLE HR_ENCUESTASUJETO ADD ORDEN INT
go