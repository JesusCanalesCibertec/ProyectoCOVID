using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;

namespace net.royal.spring.minedu.dominio
{

    [Table("sistema_informacion")]
    public class MpSistema : MpSistemaPk
    {
        [Column("codigo_si")]
        public String Codigo { get; set; }

        [Column("nombre_si")]
        public String Nombre { get; set; }

        [Column("siglas")]
        public String Siglas { get; set; }

        [Column("estado")]
        public String Estado { get; set; }

    }


}
