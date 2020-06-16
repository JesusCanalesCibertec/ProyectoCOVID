using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.minedu.dominio
{

    [Table("SEDEOFICINA", Schema ="minedu")]
    public class MpAreaminedu : MpAreamineduPk
    {

        [Column("Descripcion")]
        public String Descripcion { get; set; }

        [Column("DescripcionCorta")]
        public String DescripcionCorta { get; set; }

        //[Column("APELLIDO")]
        //public String Apellido { get; set; }


        //[Column("ANEXO")]
        //public String Anexo { get; set; }

        //[Column("CELULAR")]
        //public String Celular { get; set; }

        //[Column("CORREO")]
        //public String Correo { get; set; }

        //[Column("ESTADO")]
        //public String Estado { get; set; }

        //[Column("CREACION_USUARIO")]
        //public String CreacionUsuario { get; set; }

        //[Column("CREACION_FECHA")]
        //public Nullable<DateTime> CreacionFecha { get; set; }

        //[Column("CREACION_TERMINAL")]
        //public String CreacionTerminal { get; set; }

        //[Column("MODIFICACION_USUARIO")]
        //public String ModificacionUsuario { get; set; }

        //[Column("MODIFICACION_FECHA")]
        //public Nullable<DateTime> ModificacionFecha { get; set; }

        //[Column("MODIFICACION_TERMINAL")]
        //public String ModificacionTerminal { get; set; }


    }

    
}
