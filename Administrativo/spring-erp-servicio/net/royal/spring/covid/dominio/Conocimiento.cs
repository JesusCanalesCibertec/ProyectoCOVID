using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.covid.dominio
{

    [Table("CONOCIMIENTO")]
    public class Conocimiento : ConocimientoPk
    {



        [Column("TIPO")]
        public String Tipo { get; set; }

        [Column("AREA")]
        public String Area { get; set; }

        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

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

        [NotMapped]
        public String Cabecera { get; set; }
    }

    
}
