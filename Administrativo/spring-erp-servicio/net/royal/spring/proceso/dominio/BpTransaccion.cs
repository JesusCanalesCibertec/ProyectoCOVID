using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_TRANSACCION", Schema = "sgseguridadsys")]
    public class BpTransaccion : BpTransaccionPk
    {

        public static String REQUIERE_ELEMENTO_INICIAL = "No tiene un elemento inicial";
        public static String REQUIERE_ROL_PERMISOS = "No tiene permisos para ejecutar esta tarea";

        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        [Column("ID_VERSION")]
        public Nullable<Int32> IdVersion { get; set; }

        [Column("ID_ESTADO")]
        public String IdEstado { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("ACTUAL_ID_PROCESO")]
        public String ActualIdProceso { get; set; }

        [Column("ACTUAL_ID_ELEMENTO")]
        public String ActualIdElemento { get; set; }

        [Column("ACTUAL_ID_ROL")]
        public String ActualIdRol { get; set; }

        [Column("ACTUAL_ID_TIPO_SEGURIDAD")]
        public String ActualIdTipoSeguridad { get; set; }

        [Column("ACTUAL_ID_NIVEL_SEGURIDAD")]
        public String ActualIdNivelSeguridad { get; set; }

        [Column("ACTUAL_ID_PERSONA")]
        public Nullable<Int32> ActualIdPersona { get; set; }

        [Column("NUMEROPROCESO")]
        public Nullable<Int32> NumeroProceso { get; set; }

        [Column("ID_PERSONA_SOLICITANTE")]
        public Nullable<Int32> IdPersonaSolicitante { get; set; }

        [Column("ID_PERSONA_APROBADOR")]
        public Nullable<Int32> IdPersonaAprobador { get; set; }

        [Column("ID_PERSONA_RESPONSABLE")]
        public Nullable<Int32> IdPersonaReponsable { get; set; }

        [Column("FECHA_INICIO")]
        public Nullable<DateTime> FechaInicio { get; set; }

        [Column("FECHA_FIN")]
        public Nullable<DateTime> FechaFin { get; set; }

        [Column("FECHA_FIN_REAL")]
        public Nullable<DateTime> FechaFinReal { get; set; }

        [Column("PORCENTAJE_AVANCE")]
        public Nullable<Double> PorcentajeAvance { get; set; }

        [Column("ID_EXTERNO")]
        public String IdExterno { get; set; }

        [Column("FLG_OCULTAR_BANDEJA_PENDIENTE")]
        public String FlagOcultarBandejaPendiente { get; set; }

        [Column("CREACION_USUARIO")]
        public String CreacionUsuario { get; set; }

        [Column("CREACION_FECHA")]
        public Nullable<DateTime> CreacionFecha { get; set; }

        [Column("CREACION_TERMINAL")]
        public String CreacionTerminal { get; set; }

        [Column("MODIFICACION_USUARIO")]
        public String ModificacionUsuario { get; set; }

        [Column("MODIFICACION_FECHA")]
        public Nullable<DateTime> ModificacionFecha { get; set; }

        [Column("MODIFICACION_TERMINAL")]
        public String ModificacionTerminal { get; set; }

        public BpTransaccion()
        {
        }
    }
}
