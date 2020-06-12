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
    public class SyReportePk {

	    [Key]
	    [Display(Name = "AplicacionCodigo")]
	    [MaxLength(2)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("APLICACIONCODIGO")]
	    public String Aplicacioncodigo  { get; set; }

	    [Key]
	    [Display(Name = "ReporteCodigo")]
	    [MaxLength(3)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("REPORTECODIGO")]
	    public String Reportecodigo  { get; set; }

        public SyReportePk() { }
        public SyReportePk(String _Aplicacioncodigo, String _Reportecodigo) {
            Aplicacioncodigo = _Aplicacioncodigo;
            Reportecodigo = _Reportecodigo;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Aplicacioncodigo,Reportecodigo };
            return myObjArray;
        }

    }
}
