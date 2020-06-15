using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.covid.dominio
{
    [Table("PAIS")]
    public class Pais : PaisPk
    {
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("NACIONALIDAD")]
        public String Nacionalidad { get; set; }
    }
}
