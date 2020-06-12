using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.asistencia.dominio
{

    /**
     * 
     * 
     * @tabla dbo.AS_TipoHorario
    */
    public class AsTipohorarioPk {
        [Key]
        [Display(Name = "TipoHorario")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("TIPOHORARIO")]
        public Nullable<Int32> Tipohorario { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Tipohorario };
            return myObjArray;
        }
    }
}
