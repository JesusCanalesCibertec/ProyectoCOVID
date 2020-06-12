using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.asistencia.constante
{
    public class ConstanteAutorizacionRegla
    {
        /**
         * Requiere Marca de Asistencia del dia solicitado
         **/
        public static String REQUIERE_MARCA_ASISTENCIA = "REQASIMARC";

        /**
         * Requiere Periodo de Asistencia Abierto por Tipo de Planilla
         **/
        public static String REQUIERE_PERIODO_ASISTENCIA = "REQPERIASI";

        /**
         * Permitir el registro si esta en el mismo mes de tu cumpleaños
         **/
        public static String REQUIERE_MES_CUMPLEANIO = "REQMESCUMP";

        /**
         * No debes tener vacaciones aprobadas
         **/
        public static String REQUIERE_NO_VACACIONES_APROBADAS = "REQNOVACAP";

        /**
         * Solo los usuarios fiscalizados puede registrar permisos
         **/
        public static String REQUIERE_SOLO_FISCALIZADOS = "REQSOLFISC";

        /**
         * Hora Inicio / Hora fin debe estar fuera del horario del empleado
         **/
        public static String REQUIERE_HORA_INICIO_FUERA_HORARIO = "RQHIFUEHOR";

        /**
         * Horario Inicio y Hora Fin debe estar dentro del horario del empleado
         **/
        public static String REQUIERE_HORA_INICIOFIN_DENTRO_HORARIO = "RQHIFDENHO";

        /**
         * Hora Inicio - Hora Fin no se debe cruzar con el mismo concepto y dia hora inicio/fin
         **/
        public static String CRUZAR_HORA_INICIOFIN_MISMO_CONCEPTO = "CRHIFCONIG";

        /**
         * Hora Inicio - Hora Fin no se debe cruzar con todos concepto y dia hora inicio/fin
         **/
        public static String CRUZAR_HORA_INICIOFIN_TODOS_CONCEPTO = "CRHIFCONAL";

        /**
         * Hora inicial debe ser distinta a la Hora Final
         **/
        public static String HORA_INICIO_FIN_DISTINTAS = "HINFINDIST";

        /**
         * Cuantas veces por Dia ###
         **/
        public static String VECES_DIA_NRO = "VECEDIANRO";

        /**
         * Cuantas veces por periodo ###
         **/
        public static String VECES_PERIODO_NRO = "VECEPERNRO";


    }
}
