using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.constante
{
    public class ConstanteAprobacion
    {
        public static String APLICACION_CODIGO = "SY";
	
	    public static String MISCELANEO_ESTADO_APROBACION = "ESTAAPROBA";
        public static String MISCELANEO_PROCESO_APROBACION = "PROINTAPRO";
        
        public static String SOLICITUD_ACCION_GUARDAR = "GUARDAR";
        public static String SOLICITUD_ACCION_APROBAR = "APROBAR";
	    public static String SOLICITUD_ACCION_RECHAZAR = "RECHAZAR";
	    public static String SOLICITUD_ACCION_DEVOLVER = "DEVOLVER";
	    public static String SOLICITUD_ACCION_SEGUIMIENTO = "SEGUIMIENTO";

        public static String PARAMETRO_TIPO_BUSQUEDA = "TIPOBUSAPR";

        /** ac_costcentermst = EMPLEADOMAST.centrocostos **/
        public static String TIPO_BUSQUEDA_CENTROCOSTO = "CENTROCOSTO";

        /** HR_UNIDADOPERATIVA = EMPLEADOMAST.UNIDADOPERATIVA **/
        public static String TIPO_BUSQUEDA_UNIDAD_OPERATIVA = "UNIDOPERA";

        /** HR_Departamento = EMPLEADOMAST.DepartamentoOperacional **/
        public static String TIPO_BUSQUEDA_DEPARTAMENTO = "DEPARTA";

        /** Departmentmst = EMPLEADOMAST.DeptoOrganizacion **/
        public static String TIPO_BUSQUEDA_DEPARTAMENTO_ORGANIZACION = "DEPAORGAN";
    }
}
