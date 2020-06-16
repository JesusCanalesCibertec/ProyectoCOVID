using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.core.dominio
{

    /**
     * 
     * 
     * @tabla dbo.accountmst
*/
    [Table("PM_PUBLICACION", Schema = "sgseguridadsys")]
    public class PmPublicacion : PmPublicacionPk
    {
        [Column("ID_TIPO_PUBLICACION")]
        public String IdTipoPublicacion { get; set; }

        [Column("ID_TIPO_SEGURIDAD")]
        public String IdTipoSeguridad { get; set; }

        [Column("FECHA_INICIO")]
        public Nullable<DateTime> FechaInicio { get; set; }

        [Column("FECHA_FIN")]
        public Nullable<DateTime> FechaFin { get; set; }

        [Column("PUBLICACION")]
        public Byte[] Publicacion { get; set; }

        [NotMapped]
        public String PublicacionString { get; set; }

        [Column("ESTADO")]
        public String Estado { get; set; }

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

        [Column("ALTURA")]
        public Nullable<Double> Altura { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("TAMANIO")]
        public String Tamanio { get; set; }

        public PmPublicacion()
        {
        }
    }
}
