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
     * @tabla sgseguridadsys.PS_BENEFICIARIO_INGRESO
*/
    public class PsBeneficiarioIngresoPk
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

        [Key]
        [Display(Name = "ID_INGRESO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_INGRESO")]
        public Nullable<Int32> IdIngreso { get; set; }

        public PsBeneficiarioIngresoPk()
        {
        }

        public PsBeneficiarioIngresoPk(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdIngreso)
        {
            this.IdInstitucion = pIdInstitucion;
            this.IdBeneficiario = pIdBeneficiario;
            this.IdIngreso = pIdIngreso;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion, IdBeneficiario, IdIngreso };
            return myObjArray;
        }
    }
}
