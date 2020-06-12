using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.core.dominio
{

    /**
     * 
     * 
     * @tabla dbo.Provincia
    */
    public class ProvinciaPk {

        public ProvinciaPk() { }
        public ProvinciaPk(String _Departamento, String _Provincia) {
            Departamento = _Departamento;
            Provincia = _Provincia;
        }

        [Key]
	    [Display(Name = "Departamento")]
	    [MaxLength(3)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("DEPARTAMENTO")]
	    public String Departamento  { get; set; }

        private String _Provincia;

	    [Key]
	    [Display(Name = "Provincia")]
	    [MaxLength(3)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("PROVINCIA")]
	    public String Provincia
        {
            get { return (_Provincia == null) ? "" : _Provincia.Trim(); }
            set { _Provincia = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Departamento, Provincia };
            return myObjArray;
        }
    }
}
