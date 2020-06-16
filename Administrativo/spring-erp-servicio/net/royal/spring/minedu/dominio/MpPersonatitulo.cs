using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.minedu.dominio
{

    [Table("PERSONA_TITULO", Schema = "minedu")]
    public class MpPersonatitulo : MpPersonatituloPk
    {

        [Column("GRADO")]
        public String IdGrado { get; set; }

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


    }


}
