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
     * @tabla dbo.CompanyownerRecurso
*/
    [Table("COMPANYOWNERRECURSO")]
    public class Companyownerrecurso : CompanyownerrecursoPk
    {

        [Display(Name = "recurso")]
        [Column("RECURSO")]
        public byte[] Recurso { get; set; }

        [Display(Name = "nombreRecurso")]
        [MaxLength(300)]
        [Column("NOMBRERECURSO")]
        public String Nombrerecurso { get; set; }

        [Display(Name = "UltimoUsuario")]
        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "UltimaFechaModif")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        [NotMapped]
        public string AuxString { get; set; }

        public Companyownerrecurso()
        {
        }
    }
}
