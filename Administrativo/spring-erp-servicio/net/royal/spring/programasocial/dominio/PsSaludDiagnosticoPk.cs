using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio {

    /**
     * 
     * 
     * @tabla sgseguridadsys.PS_SALUD_DIAGNOSTICO
*/
    public class PsSaludDiagnosticoPk {

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
        [Display(Name = "ID_DIAGNOSTICO")]
        [MaxLength(5)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_DIAGNOSTICO")]
        public String IdDiagnostico { get; set; }

        public PsSaludDiagnosticoPk() {
        }

        public PsSaludDiagnosticoPk(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud, String pIdDiagnostico) {
            this.IdInstitucion = pIdInstitucion;
            this.IdBeneficiario = pIdBeneficiario;
            this.IdSalud = pIdSalud;
            this.IdDiagnostico = pIdDiagnostico;

        }

        public object[] obtenerArreglo() {
            object[] myObjArray = { IdInstitucion, IdBeneficiario, IdSalud, IdDiagnostico };
            return myObjArray;
        }
    }
}
