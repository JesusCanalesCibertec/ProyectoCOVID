using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.core.constante
{
    public class ConstanteEmpleado
    {
        public static String APLICACION_CODIGO = "HR";
        public static String MISCELANEO_ESTADO_EMPLEADO = "EMPLESTADO";
        public static String FOTO_MODO_GUARDAR_DISCO = "DISCO";
        public static String FOTO_MODO_GUARDAR_CAMPO_TABLA_EMPLEADO = "EMPFOTO";
        public static String FOTO_RETORNO_DTO_STRING = "STRING";
        public static String FOTO_RETORNO_DTO_BYTE = "BYTE";


        /* Indica en que forma se almacenas las imagenes de las fotos del personal
	     * TEXT : DISCO (ruta en disco - PATHBITMAP)    EMPFOTO (CAMPO DE LA TABLA EMPLEADO - FOTOGRAFIA) */
        public static String PARAMETRO_FOTO_MODO_GUARDAR = "FOTTIPOALM";

        /* Ubicacion en disco en donde se encuentra las imagenes de las todos */
        public static String PARAMETRO_FOTO_EMPLEADO_RUTA_APROBADA = "PATHBITMAP";

        /* extencion por defecto que se acepta y como se obtendra de la ruta o lugar guardado */
        public static String PARAMETRO_FOTO_EMPLEADO_EXTENSION = "EXTARCHFOT";

        /* tamaño maximo aceptado si es nulo o vacio no se valida el tamaño */
        public static String PARAMETRO_FOTO_EMPLEADO_TAMANIO_MAXIMO = "MAXSIZEFOT";

        /* TEXTO : S(se toma el codigo de empleado como nombre) caso contrario se tomara numero documento personamast */
        public static String PARAMETRO_FOTO_EMPLEADO_NOMBRE_ARCHIVO = "BMPCODIGO";

        public static Int32 ANIO_BISIESTO = 2016;
    }
}
