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
     * @tabla dbo.SY_ReporteArchivo
    */
    public class SyReportearchivoPk {

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

	    [Key]
	    [Display(Name = "CompaniaSocio")]
	    [MaxLength(8)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("COMPANIASOCIO")]
	    public String Companiasocio  { get; set; }

	    [Key]
	    [Display(Name = "Periodo")]
	    [MaxLength(6)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("PERIODO")]
	    public String Periodo  { get; set; }

	    [Key]
	    [Display(Name = "Version")]
	    [MaxLength(6)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("VERSION")]
	    public String Version  { get; set; }


        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Aplicacioncodigo, Reportecodigo,Companiasocio,Periodo,Version };
            return myObjArray;
        }

        public SyReportearchivoPk() { }
        public SyReportearchivoPk(String _aplicacioncodigo, String _reportecodigo, String _companiasocio, String _periodo, String _version)
        {

            Aplicacioncodigo = _aplicacioncodigo;
            Reportecodigo = _reportecodigo;
            Companiasocio = _companiasocio;
            Periodo = _periodo;
            Version = _version;            
        }
    }
}
