using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.constante
{
    public class ConstanteReporte
    {
        public enum FormatoSalia
        {
            HTML,
            PDF
        }

        public enum TipoImagen
        {
            LOGO,
            FIRMA
        }

        public static String APLICACION_CODIGO = "SY";
        public static String APLICACION_CODIGO_SPRINGNET = "SN";
        public static String TIPO_REPORTE_POWERBUILDER = "POWER";
	    public static String TIPO_REPORTE_JASPER = "JASPE";
	    public static String TIPO_FORMATO_HTML = "HTML";
        
        public static String PARAMETRO_RUTA_REPORTES_WEB = "RUTAREPWEB";

        public static String PARAMETRO_BOLETA_ENVIAR_PERIODO_FORMATO = "CENVPERBOL";

        // Codigo de Aplicacion Reporte CTS
        public static String CODIGO_REPORTE_CTS = "001";
        public static String CODIGO_REPORTE_IMPUESTO = "002";
        public static String CODIGO_REPORTE_LIQUIDACION= "003";
        public static String CODIGO_REPORTE_BOLETA = "004";
        public static String CODIGO_REPORTE_CONVENIO = "006";

        public static String CODIGO_FIRMA_CTS = "FIRCTS";
        public static String CODIGO_FIRMA_BOLETA = "FIRBOL";
        public static String CODIGO_FIRMA_CONSTANCIAS = "FIRCON";
        public static String CODIGO_FIRMA_QUINTA = "FIRQUI";
        public static String CODIGO_FIRMA_RENTAS = "FIRREN";
        public static String CODIGO_FIRMA_UTILIDADES = "FIRUTI";

        public static String CODIGO_IMAGEN_COMPANIA = "IMGCOM";
        public static String CODIGO_IMAGEN_PRINCIPAL = "IMGPRI";
        public static String CODIGO_IMAGEN_FORMATO = "IMGFOR";
    }
}
