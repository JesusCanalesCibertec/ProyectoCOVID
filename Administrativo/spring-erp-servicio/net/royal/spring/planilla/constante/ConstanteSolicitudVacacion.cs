using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.planilla.constante
{
    public static class ConstanteSolicitudVacacion
    {
        public static String APLICACION_CODIGO = "PR";
	
	    public static String MISCELANEO_ESTADO_SOLICITUDVACACION = "SOLVACESTA";
	    public static String MISCELANEO_UTILIZACION_SOLICITUDVACACION = "SOLVACUTIL";
		
	    public static String ESTADO_SOLICITUD = "S";
	    public static String ESTADO_APROBADO = "A";
	    public static String ESTADO_RECHAZADO = "R";
	    public static String ESTADO_ANULADO = "N";

        public static String PARAMETRO_REGULARIZACION_VACAS = "REGULVACAS";
        public static String PARAMETRO_REGULARIZACION_SOLO_MES_VACAS = "REGMESVACA";
        public static String PARAMETRO_REGLAS_VACAS = "REGLAVACAS";
        public static String PARAMETRO_MAX_DIA_REGISTRO_VACAS = "MAXREGVACA";
        public static String PARAMETRO_MAX_DIA_APROBACION_VACAS = "MAXAPRVACA";
        public static String PARAMETRO_MIN_DIAS_VACAS = "MINDIAVACA";
        public static String PARAMETRO_VALIDAR_SALDO_VACAS = "VALSALVACA";
    }
}
