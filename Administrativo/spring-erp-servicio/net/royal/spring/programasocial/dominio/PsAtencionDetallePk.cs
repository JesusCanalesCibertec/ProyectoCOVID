using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio {

    /**
     * 
     * 
     * @tabla sgseguridadsys.PS_ATENCION_DETALLE
*/
    public class PsAtencionDetallePk {

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

        public PsAtencionDetallePk() {
        }

        public PsAtencionDetallePk(Nullable<Int32> pIdAtencion, Nullable<Int32> IdDiagnostico) {
            this.IdAtencion = pIdAtencion;
            this.IdDiagnostico = IdDiagnostico;

        }

        public object[] obtenerArreglo() {
            object[] myObjArray = {IdAtencion, IdDiagnostico };
            return myObjArray;
        }
    }
}
