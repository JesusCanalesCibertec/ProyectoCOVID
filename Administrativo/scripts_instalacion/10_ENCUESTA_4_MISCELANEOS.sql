/* ENCUESTA - ESTADOS */
if(not exists(select * from MA_MiscelaneosHeader where CodigoTabla='ENCUESEST' ))
	INSERT INTO MA_MiscelaneosHeader(AplicacionCodigo, CodigoTabla, Compania, DescripcionLocal, Estado) 
	VALUES('HR', 'ENCUESEST', '999999', 'Estado de encuestas', 'A')
GO
if(not exists(select * from MA_MiscelaneosDetalle where CodigoTabla='ENCUESEST' and CodigoElemento='P' ))
	INSERT INTO MA_MiscelaneosDetalle(AplicacionCodigo, CodigoTabla, Compania, CodigoElemento, DescripcionLocal, Estado) 
	VALUES('HR', 'ENCUESEST', '999999', 'P', 'En Preparación', 'A ')
GO
if(not exists(select * from MA_MiscelaneosDetalle where CodigoTabla='ENCUESEST' and CodigoElemento='A' ))
	INSERT INTO MA_MiscelaneosDetalle(AplicacionCodigo, CodigoTabla, Compania, CodigoElemento, DescripcionLocal, Estado) 
	VALUES('HR', 'ENCUESEST', '999999', 'A', 'Aprobado', 'A ')
GO
if(not exists(select * from MA_MiscelaneosDetalle where CodigoTabla='ENCUESEST' and CodigoElemento='E' ))
	INSERT INTO MA_MiscelaneosDetalle(AplicacionCodigo, CodigoTabla, Compania, CodigoElemento, DescripcionLocal, Estado) 
	VALUES('HR', 'ENCUESEST', '999999', 'E', 'En Ejecución', 'A ')
GO
if(not exists(select * from MA_MiscelaneosDetalle where CodigoTabla='ENCUESEST' and CodigoElemento='T' ))
	INSERT INTO MA_MiscelaneosDetalle(AplicacionCodigo, CodigoTabla, Compania, CodigoElemento, DescripcionLocal, Estado) 
	VALUES('HR', 'ENCUESEST', '999999', 'T', 'Terminado', 'A ')
GO
if(not exists(select * from MA_MiscelaneosDetalle where CodigoTabla='ENCUESEST' and CodigoElemento='R' ))
	INSERT INTO MA_MiscelaneosDetalle(AplicacionCodigo, CodigoTabla, Compania, CodigoElemento, DescripcionLocal, Estado) 
	VALUES('HR', 'ENCUESEST', '999999', 'R', 'Rechazado', 'A ')
GO

