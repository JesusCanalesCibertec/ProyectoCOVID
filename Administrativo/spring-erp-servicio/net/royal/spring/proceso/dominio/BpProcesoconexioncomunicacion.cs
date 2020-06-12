using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_PROCESO_CONEXION_COMUNICACION", Schema = "sgseguridadsys")]
    public class BpProcesoconexioncomunicacion : BpProcesoconexioncomunicacionPk
    {
        public static String TIPO_COMUNICACION_CORREO = "CORR";
        public static String TIPO_COMUNICACION_NOTIFICACION = "NOTI";

        [Column("ID_TIPO_COMUNICACION")]
        public String IdTipoComunicacion { get; set; }

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

        public BpProcesoconexioncomunicacion()
        {
        }
    }
}
