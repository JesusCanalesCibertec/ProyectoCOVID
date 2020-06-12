using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.constante
{
    public class ConstanteFichaEmpleado
    {
        public static String APLICACION_CODIGO = "HR";
        public static String MISCELANEOS_ESTADOS_FICHA = "FICHAEMP";

        /*si no esta configurado este se tomar por defecto*/
        public static String DIRECCION_GUARDAR_SIN_ESTADO_POSITIVO = "SINESTPOSI";
	    public static String DIRECCION_GUARDAR_ESTADO_ACTIVO_TODOS = "ESTTODOS";

        /* guardar la ruta fisica en donde se encuentra el archivo NO SE IMPLEMENTA - POR DEFECTO*/
        public static String FOLIOS_GUARDAR_FILE_LOCAL = "FOLIOLOCA";

        /* guardar la ruta de servidor en donde se almacena la informacion 
         * se copia los archivos a la ruta de servidor
         *  trabajar con este paramaetro PARAMETRO_FOLIO_RUTA_SERVIDOR
         */
        public static String FOLIOS_GUARDAR_FILE_SERVER = "FOLIOSERV";
	
	    /* guardar archivo en base de datos */
	    public static String FOLIOS_GUARDAR_BASEDATOS = "FOLIOBD";
	
	    /* guardar la url de alfresco */
	    public static String FOLIOS_GUARDAR_ALFRESCO = "FOLIOALFRE";

        public static String PARAMETRO_TAMANIO_FOLIO = "MXSIFOLIO";

        public static String PARAMETRO_FOTO_EMPLEADO_PUEDE_CAMBIAR = "FOEMCAMBI";
        public static String PARAMETRO_FOLIO_GUARDAR = "FOLIOGUARD";

        public static String PARAMETRO_FOLIO_RUTA_SERVIDOR = "PATHFOLIOS";

        public static String PARAMETRO_ARHIVO_FORMACION_RUTA_SERVIDOR = "PATHFORM";
        public static String PARAMETRO_ARHIVO_ESPECIALIZACION_RUTA_SERVIDOR = "PATHESP";

        /* Ubicacion en donde se encuentra las fotos enviadas por la aplicacion 
	     * actualizacion de ficha de empleado */
        public static String PARAMETRO_FOTO_EMPLEADO_RUTA_ENVIADA = "PATHFOTENV";

        public static String PARAMETRO_FOTO_TAMANIO = "MAXSIFORM";

        public static String PARAMETRO_FORMACION_ADJUNTO_TAMANIO = "MAXSIFOTO";

        public static String PARAMETRO_ESPECIZALIZACION_ADJUNTO_TAMANIO = "MAXSIESP";

        /* Ubicacion en donde se encuentran las fotos del empleado que aun no se han enviado */
        public static String PARAMETRO_FOTO_EMPLEADO_RUTA_GUARDADA = "PATHFOTGUA";
	
	    /* colores normales de la ficha (fondo,letra) */
	    public static String PARAMETRO_COLOR_NORMAL_FONDO = "FECOLNORMF";
	    public static String PARAMETRO_COLOR_NORMAL_LETRA = "FECOLNORML";

	    /* colores nuevo de la ficha (fondo,letra) */
	    public static String PARAMETRO_COLOR_NUEVO_FONDO = "FECOLNUEVF";
	    public static String PARAMETRO_COLOR_NUEVO_LETRA = "FECOLNUEVL";

	    /* colores modifiacion de la ficha (fondo,letra) */
	    public static String PARAMETRO_COLOR_MODIFICADO_FONDO = "FECOLMODIF";
	    public static String PARAMETRO_COLOR_MODIFICADO_LETRA = "FECOLMODIL";

	    /* colores eliminado de la ficha (fondo,letra) */
	    public static String PARAMETRO_COLOR_ELIMINADO_FONDO = "FECOLELIMF";
	    public static String PARAMETRO_COLOR_ELIMINADO_LETRA = "FECOLELIML";

        public static String PARAMETRO_TAB_DIRECCION = "FICHTABDIR";
        public static String PARAMETRO_TAB_FAMILIA = "FICHTABFAM";
        public static String PARAMETRO_TAB_FORMACION = "FICHTABFOR";
        public static String PARAMETRO_TAB_ESPECIALIZACION = "FICHTABESP";
        public static String PARAMETRO_TAB_IDIOMA = "FICHTABIDI";
        public static String PARAMETRO_TAB_INFORMATICA = "FICHTABINF";
        public static String PARAMETRO_TAB_LABORAL = "FICHTABLAB";
        public static String PARAMETRO_TAB_EXPERIENCIA = "FICHTABEXP";
        public static String PARAMETRO_TAB_REFERENCIA = "FICHTABREF";
        public static String PARAMETRO_TAB_DOCUMENTO = "FICHTABDOC";

        /* como se tomara la tabla de direccion TIPO (Se puede tomar los activos y todos o solo si nestado y solo positivos)
	     * esto se da porque en la misma tabla se almacena la direccion de los dependientes */
        public static String PARAMETRO_FICHAPERSONAL_TIPO_ALMACENAMIENTO_DIRECCION = "DIRETIPGUA";
	
	    public static String MISCELANEO_ESTADO_CIVIL = "ESTCIVIL";
	
	    public static String MISCELANEO_CONDICION_VIVIENDA = "CONDVIV";
	
	    public static String MISCELANEO_TIPO_VIVIENDA = "TIPOVIV";
	
	    public static String MISCELANEO_TIPO_BREVETE = "TIPOBREV";
	
	    public static String MISCELANEO_TIPO_PARENTESCO = "TIPOVINC";
	
	    public static String MISCELANEO_TIPO_DOCUMENTO_PERSONA = "TIPODOCI";
	
	    public static String MISCELANEO_GRUPO_SANGUIENEO = "GRUPOSANGR";
	
	    public static String MISCELANEO_NIVEL_IDIOMA = "NIVELIDIOM";
	
	    public static String MISCELANEO_MOTIVO_CESE = "MOTIVOCESE";
	
	    public static String MISCELANEO_TIPO_VIAS = "TIPOVIAS";
	
	    public static String MISCELANEO_TIPO_ZONA = "TIPOZONA";
	
	    public static String MISCELANEO_COLEGIO_PROFESIONAL = "COLEGIOSPR";
	
	    public static String MISCELANEO_TIPO_CURSO = "TIPOCURSO";

        public static String MISCELANEO_COMPANIA = "999999";


        public static String MISCELANEO_INFORMATICA = "INFORMAT";

        //public static String MISCELANEO_INFORMATICA_NIVEL = "INFORMNIVE";
        public static String MISCELANEO_INFORMATICA_NIVEL = "NIVELIDIOM";

        public static String MISCELANEO_TIPO_EXPERIENCIA = "TIPOEXPER";
	
	    public static String MISCELANEO_AREA_EXPERIENCIA = "AREAEXPER";
	
	    public static String MISCELANEO_DOCUMENTOS_PRESENTADOS = "DOCUMENTOS";
	
	    public static String MISCELANEO_TIPO_ENTIDAD = "TIPOENTID";
	
	    public static String MISCELANEO_TIPO_VINCULO_FAMILIAR = "RTPS_TB_19";
	
	    public static String MISCELANEO_TIPO_DOCUMENTO_ACREDITADO = "RTPS_TB_27";	
	
	    public static String MISCELANEO_PAIS_EMISOR_DOCUMENTO = "RTPS_TB_26";

        public static String MISCELANEO_CODIGO_POSTAL = "RTPS_TB_29";

        public static String MISCELANEO_SITUACION_EPS = "SITUAEPS";

        public static String MISCELANEO_TIPO_CUENTA = "TIPCUENTA";
                
    }
}
