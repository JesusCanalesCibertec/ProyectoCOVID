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
     * @tabla sgseguridadsys.PS_SALUD_ESTADO
*/
    public class PsSaludAyudaPk
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
        [Display(Name = "ID_SALUD")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_SALUD")]
        public Nullable<Int32> IdSalud { get; set; }

        [Key]
        [Display(Name = "ID_AYUDA")]
        [MaxLength(5)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_AYUDA")]
        public String IdAyuda { get; set; }

        public PsSaludAyudaPk()
        {
        }

        public PsSaludAyudaPk(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud, String pIdEstado)
        {
            this.IdInstitucion = pIdInstitucion;
            this.IdBeneficiario = pIdBeneficiario;
            this.IdSalud = pIdSalud;
            this.IdAyuda = pIdEstado;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion, IdBeneficiario, IdSalud, IdAyuda };
            return myObjArray;
        }
    }
}
