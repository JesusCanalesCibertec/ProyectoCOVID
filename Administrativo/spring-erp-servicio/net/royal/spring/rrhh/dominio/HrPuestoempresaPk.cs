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
     * @tabla dbo.SY_Reporte
    */
    public class HrPuestoempresaPk {

	    [Key]
	    [Display(Name = "CodigoPuesto")]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("CodigoPuesto")]
	    public Int32? CodigoPuesto { get; set; }

	    

        public HrPuestoempresaPk() { }
        public HrPuestoempresaPk(Int32? pCodigoPuesto) {
            CodigoPuesto = pCodigoPuesto;
            
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { CodigoPuesto };
            return myObjArray;
        }

    }
}
