using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_PROCESO_CONEXION_EVENTO", Schema = "sgseguridadsys")]
    public class BpProcesoconexionevento : BpProcesoconexioneventoPk
    {

        [Column("ORDEN")]
        public Int32? Orden { get; set; }

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

        [Column("REPORTE_ID")]
        public String ReporteId { get; set; }

        [Column("REPORTE_ID_APLICACION")]
        public String ReporteIdAplicacion { get; set; }

       

        public BpProcesoconexionevento()
        {
        }
    }
}
