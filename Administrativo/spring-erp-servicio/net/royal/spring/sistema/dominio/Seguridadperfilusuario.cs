using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.sistema.dominio
{

    /**
     * 
     * 
     * @tabla dbo.SeguridadPerfilUsuario
*/
    [Table("SEGURIDADPERFILUSUARIO")]
    public class Seguridadperfilusuario : SeguridadperfilusuarioPk
    {

        [Display(Name = "Estado")]
        [MaxLength(2)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Column("CREACION_FECHA")]
        public Nullable<DateTime> CreacionFecha { get; set; }

        [Column("ULTIMA_CONEXION")]
        public Nullable<DateTime> Ultimaconexion { get; set; }

        [Column("ULTIMOUSUARIO")]
        public String ModificacionUsuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> ModificacionFecha { get; set; }


        [NotMapped]
        public String Nombres;

        [NotMapped]
        public Boolean activo;

        public Seguridadperfilusuario()
        {
        }
    }
}
