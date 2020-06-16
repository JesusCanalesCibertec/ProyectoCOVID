using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_MAE_PROCESO_FUNCIONALIDAD", Schema = "sgseguridadsys")]
    public class BpMaeprocesofuncionalidad : BpMaeprocesofuncionalidadPk
    {
        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("FLG_VISIBLE_DEFECTO")]
        public String VisibleDefecto { get; set; }

        [Column("FLG_HABILITADO_DEFECTO")]
        public String HablitadoDefecto { get; set; }

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

        public BpMaeprocesofuncionalidad()
        {
        }
    }
}
