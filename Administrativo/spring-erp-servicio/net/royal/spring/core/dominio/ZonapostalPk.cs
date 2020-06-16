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
     * @tabla dbo.ZonaPostal
    */
    public class ZonapostalPk {

        public ZonapostalPk() { }
        public ZonapostalPk(String _Departamento, String _Provincia, String _Codigo_Postal)
        {
            Departamento = _Departamento;
            Provincia = _Provincia;
            _CodigoPostal = _Codigo_Postal;
        }

        [Key]
	    [Display(Name = "Departamento")]
	    [MaxLength(3)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("DEPARTAMENTO")]
	    public String Departamento  { get; set; }

	    [Key]
	    [Display(Name = "Provincia")]
	    [MaxLength(3)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("PROVINCIA")]
	    public String Provincia  { get; set; }

        private String _CodigoPostal;

	    [Key]
	    [Display(Name = "CodigoPostal")]
	    [MaxLength(3)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("CODIGOPOSTAL")]
	    public String Codigopostal
        {
            get { return (_CodigoPostal == null) ? "" : _CodigoPostal.Trim(); }
            set { _CodigoPostal = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Departamento, Provincia, Codigopostal };
            return myObjArray;
        }
    }
}
