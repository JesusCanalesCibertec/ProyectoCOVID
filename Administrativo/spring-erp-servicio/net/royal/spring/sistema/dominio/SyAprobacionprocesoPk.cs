using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.sistema.dominio
{

/**
 * 
 * 
 * @tabla dbo.SY_AprobacionProceso
*/
    public class SyAprobacionprocesoPk
    {
        [Key]
        [Display(Name = "Aplicacion")]
        [MaxLength(2)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("APLICACION")]
        public String Aplicacion { get; set; }

        [Key]
	    [Display(Name = "CodigoProceso")]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("CODIGOPROCESO")]
	    public Nullable<Int32> Codigoproceso  { get; set; }

        public SyAprobacionprocesoPk() { }
        public SyAprobacionprocesoPk(String _aplicacion, Nullable<Int32> _codigoproceso) {
            Aplicacion = _aplicacion;
            Codigoproceso = _codigoproceso;
         }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Aplicacion, Codigoproceso };
            return myObjArray;
        }
    }
}
