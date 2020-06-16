using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_MAE_PROCESO_ROL_USUARIO", Schema = "sgseguridadsys")]
    public class BpMaeprocesorolusuario : BpMaeprocesorolusuarioPk
    {
        [Column("ID_USUARIO")]
        public String IdUsuario { get; set; }

        [Column("ID_PUESTO")]
        public String IdPuesto { get; set; }

        [Column("ID_CENTRO_COSTOS")]
        public String IdCentroCostos { get; set; }

        [Column("ID_SUCURSAL")]
        public String IdSucursal { get; set; }

        [Column("ID_PERSONA")]
        public Nullable<Int32> IdPersona { get; set; }

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

        public BpMaeprocesorolusuario()
        {
        }
    }
}
