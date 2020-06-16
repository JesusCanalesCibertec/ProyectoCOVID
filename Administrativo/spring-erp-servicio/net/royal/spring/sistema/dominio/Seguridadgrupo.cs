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
     * @tabla dbo.SeguridadGrupo
*/
    [Table("SEGURIDADGRUPO")]
    public class Seguridadgrupo : SeguridadgrupoPk
    {

        [Display(Name = "Descripcion")]
        [MaxLength(30)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [NotMapped]
        public List<Seguridadconcepto> conceptos { get; set; }

        public Seguridadgrupo()
        {
        }
    }
}
