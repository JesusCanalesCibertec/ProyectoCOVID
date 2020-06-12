using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio {

    /**
     * 
     * 
     * @tabla sgseguridadsys.PS_ATENCION_TRATAMIENTO
*/
    public class PsAtencionTratamientoPk {


        [Key]
        [Display(Name = "ID_ATENCION")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_ATENCION")]
        public Nullable<Int32> IdAtencion { get; set; }

        [Key]
        [Display(Name = "ID_DIAGNOSTICO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_DIAGNOSTICO")]
        public Nullable<Int32> IdDiagnostico { get; set; }

        [Key]
        [Display(Name = "ID_TRATAMIENTO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_TRATAMIENTO")]
        public String IdTratamiento { get; set; }

        public PsAtencionTratamientoPk() {
        }

        public PsAtencionTratamientoPk(Nullable<Int32> pIdAtencion, Nullable<Int32> pIdDiagnostico, String pIdTratamiento) {
            this.IdAtencion = pIdAtencion;
            this.IdDiagnostico = pIdDiagnostico;
            this.IdTratamiento = pIdTratamiento;

        }

        public object[] obtenerArreglo() {
            object[] myObjArray = { IdAtencion, IdDiagnostico, IdTratamiento };
            return myObjArray;
        }
    }
}
