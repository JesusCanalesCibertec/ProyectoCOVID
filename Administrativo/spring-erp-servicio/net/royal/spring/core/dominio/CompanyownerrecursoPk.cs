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
    public class CompanyownerrecursoPk
    {

        [Key]
        [Display(Name = "Companyowner")]
        [MaxLength(8)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("COMPANYOWNER")]
        public String Companyowner { get; set; }

        [Key]
        [Display(Name = "TipoRecurso")]
        [MaxLength(6)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("TIPORECURSO")]
        public String Tiporecurso { get; set; }

        [Key]
        [Display(Name = "Periodo")]
        [MaxLength(6)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("PERIODO")]
        public String Periodo { get; set; }

        public CompanyownerrecursoPk() { }
        public CompanyownerrecursoPk(String _Tiporecurso, String _Companyowner, String _Periodo)
        {
            Companyowner = _Companyowner;
            Tiporecurso = _Tiporecurso;
            Periodo = _Periodo;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner, Tiporecurso, Periodo };
            return myObjArray;
        }

        [NotMapped]
        public String AuxCompanyowner { get; set; }
    }
}
