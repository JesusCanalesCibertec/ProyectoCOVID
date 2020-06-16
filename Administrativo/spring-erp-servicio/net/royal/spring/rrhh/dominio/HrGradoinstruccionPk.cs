using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.rrhh.dominio
{

    /**
     * 
     * 
     * @tabla dbo.HR_GradoInstruccion
*/
    public class HrGradoinstruccionPk
    {

        private String _Gradoinstruccion;

        [Key]
        [Display(Name = "GradoInstruccion")]
        [MaxLength(5)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("GRADOINSTRUCCION")]
        public String Gradoinstruccion
        {
            get { return (_Gradoinstruccion == null) ? "" : _Gradoinstruccion.Trim(); }
            set { _Gradoinstruccion = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Gradoinstruccion };
            return myObjArray;
        }
    }
}
