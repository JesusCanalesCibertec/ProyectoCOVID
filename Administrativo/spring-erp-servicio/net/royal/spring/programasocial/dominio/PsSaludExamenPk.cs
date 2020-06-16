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
     * @tabla sgseguridadsys.PS_SALUD_EXAMEN
*/
    public class PsSaludExamenPk
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
        [Display(Name = "ID_EXAMEN")]
        [MaxLength(6)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_EXAMEN")]
        public String IdExamen { get; set; }



        public PsSaludExamenPk()
        {
        }

        public PsSaludExamenPk(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud, String pIdExamen, String pIdResultado)
        {
            this.IdInstitucion = pIdInstitucion;
            this.IdBeneficiario = pIdBeneficiario;
            this.IdSalud = pIdSalud;
            this.IdExamen = pIdExamen;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion, IdBeneficiario, IdSalud, IdExamen };
            return myObjArray;
        }
    }
}
