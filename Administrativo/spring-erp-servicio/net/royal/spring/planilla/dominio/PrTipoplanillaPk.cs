using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.planilla.dominio
{

    /**
     * 
     * 
     * @tabla dbo.PR_TipoPlanilla
    */
    public class PrTipoplanillaPk
    {

        private string _Tipoplanilla;
        [Key]
        [Display(Name = "TipoPlanilla")]
        [MaxLength(2)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("TIPOPLANILLA")]
        public String Tipoplanilla
        {
            get { return (_Tipoplanilla == null) ? "" : _Tipoplanilla.Trim(); }
            set { _Tipoplanilla = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Tipoplanilla };
            return myObjArray;
        }
    }
}
