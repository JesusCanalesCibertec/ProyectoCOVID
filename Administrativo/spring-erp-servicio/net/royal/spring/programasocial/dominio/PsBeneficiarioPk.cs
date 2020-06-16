using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.PS_BENEFICIARIO
*/
    public class PsBeneficiarioPk
    {

        [Key]
        [Display(Name = "ID_INSTITUCION")]
        [MaxLength(5)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_INSTITUCION")]
        public String IdInstitucion { get; set; }

        [Key]
        [Display(Name = "ID_BENEFICIARIO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_BENEFICIARIO")]
        public Nullable<Int32> IdBeneficiario { get; set; }

        public PsBeneficiarioPk()
        {
        }

        public PsBeneficiarioPk(String pIdInstitucion, Nullable<Int32> pIdBeneficiario)
        {
            this.IdInstitucion = pIdInstitucion;
            this.IdBeneficiario = pIdBeneficiario;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion, IdBeneficiario };
            return myObjArray;
        }
    }
}
