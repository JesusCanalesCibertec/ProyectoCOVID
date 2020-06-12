--RES01 DETALLE - SNp_Res01Detalle  
ALTER PROCEDURE sgseguridadsys.SNp_Res01Detalle  
(  
 @PERIODO VARCHAR(6),  
 @PROGRAMA VARCHAR(3),  
 @INSTITUCION VARCHAR(10)  
)  
AS  
  
--DECLARE @PERIODO VARCHAR(6) = '201802'  
--DECLARE @PROGRAMA VARCHAR(3) = 'AAM'  
--DECLARE @INSTITUCION VARCHAR(10) = 'DESAM'  
  
DECLARE @M_VALORACION VARCHAR(10)  
DECLARE @ID_RESI_ACTUAL VARCHAR(10)  
DECLARE @NOM_RESI_ACTUAL VARCHAR(100)  
  
DECLARE @GENERAL AS TABLE(  
 ID_RESIDENCIA VARCHAR(10),  
 NOMBRE_RESIDENCIA VARCHAR(100),  
 ID_VALOR VARCHAR(10),  
 NOMBRE_VALOR VARCHAR(100),  
 CANTIDAD INT,  
 PORCENTAJE DECIMAL(10,2)  
)  
  
DECLARE @INS_PROGRAMAS AS TABLE(  
 INSTITUCION VARCHAR(10),  
 PROGRAMA VARCHAR(3)  
)  
  
IF @PROGRAMA = 'AAM'  
 SET @M_VALORACION = 'NUTRVALADU'  
ELSE  
 SET @M_VALORACION = 'NUTRVALNIN'  
  
DECLARE CURSOR_RESIDENCIA CURSOR FOR      
SELECT A.ID_AREA, A.NOMBRE FROM sgseguridadsys.PS_INSTITUCION_AREA A WHERE A.ID_INSTITUCION = @INSTITUCION AND A.ESTADO = 'A'  
UNION ALL  
SELECT 'ALL', 'TOTAL'  
  
OPEN CURSOR_RESIDENCIA          
FETCH NEXT FROM CURSOR_RESIDENCIA INTO @ID_RESI_ACTUAL, @NOM_RESI_ACTUAL  
  
WHILE @@FETCH_STATUS = 0  
 BEGIN   
  
 DECLARE @TOTAL INT = (  
 select count(1) from   
 sgseguridadsys.PS_BENEFICIARIO Y  
 WHERE   
 (y.ID_AREA = @ID_RESI_ACTUAL OR @ID_RESI_ACTUAL = 'ALL') AND Y.ESTADO = 'ACT' AND Y.ID_INSTITUCION = @INSTITUCION AND Y.ID_PROGRAMA =@PROGRAMA)   
  
 INSERT INTO @GENERAL(ID_RESIDENCIA, NOMBRE_RESIDENCIA, ID_VALOR, NOMBRE_VALOR, CANTIDAD)  
 SELECT @ID_RESI_ACTUAL, @NOM_RESI_ACTUAL, 'ALL', 'Población total', @TOTAL  
   
 UNION ALL   
  
 SELECT @ID_RESI_ACTUAL, @NOM_RESI_ACTUAL, B.CodigoElemento, B.DescripcionLocal, (  
 select count(1) from   
 sgseguridadsys.PS_NUTRICION X JOIN    
 sgseguridadsys.PS_BENEFICIARIO Y ON Y.ID_INSTITUCION = X.ID_INSTITUCION AND Y.ID_BENEFICIARIO = X.ID_BENEFICIARIO  
 WHERE   
 (y.ID_AREA = @ID_RESI_ACTUAL OR @ID_RESI_ACTUAL = 'ALL') AND X.ID_ORIGEN = 'EVA' AND Y.ESTADO = 'ACT' AND ISNULL(X.EVALUADO, 'S') <> 'N' AND X.ID_VALORACION = B.CodigoElemento AND X.ID_INSTITUCION = @INSTITUCION AND Y.ID_PROGRAMA =@PROGRAMA and x.ID_PERIODO = @PERIODO)   
 FROM MA_MiscelaneosDetalle B WHERE B.CodigoTabla = @M_VALORACION AND AplicacionCodigo = 'PS'  
   
 UNION ALL  
  
 SELECT @ID_RESI_ACTUAL, @NOM_RESI_ACTUAL, 'NE-PO', 'No Evaluados', (  
 select count(1) from   
 sgseguridadsys.PS_NUTRICION X JOIN    
 sgseguridadsys.PS_BENEFICIARIO Y ON Y.ID_INSTITUCION = X.ID_INSTITUCION AND Y.ID_BENEFICIARIO = X.ID_BENEFICIARIO  
 WHERE   
 (y.ID_AREA = @ID_RESI_ACTUAL OR @ID_RESI_ACTUAL = 'ALL') AND X.ID_ORIGEN = 'EVA' AND Y.ESTADO = 'ACT' AND ISNULL(X.EVALUADO, 'S') = 'N' AND X.ID_INSTITUCION = @INSTITUCION AND Y.ID_PROGRAMA =@PROGRAMA and x.ID_PERIODO = @PERIODO)   
  
 UPDATE @GENERAL SET PORCENTAJE = ROUND(CANTIDAD * 100.0 / @TOTAL, 2) WHERE ID_RESIDENCIA = @ID_RESI_ACTUAL AND @TOTAL <> 0  
  
 UPDATE @GENERAL SET PORCENTAJE = 0 WHERE ID_RESIDENCIA = @ID_RESI_ACTUAL AND @TOTAL = 0  
  
 FETCH NEXT FROM CURSOR_RESIDENCIA INTO @ID_RESI_ACTUAL, @NOM_RESI_ACTUAL  
 END    
          
CLOSE CURSOR_RESIDENCIA           
DEALLOCATE CURSOR_RESIDENCIA     
  
SELECT * FROM @GENERAL  
  
  
  