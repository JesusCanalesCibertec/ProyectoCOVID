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
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("ID_DEPARTAMENTO")]
	    public String Departamento  { get; set; }

	    [Key]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("ID_PROVINCIA")]
	    public String Provincia  { get; set; }

        private String _CodigoPostal;

	    [Key]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("ID_DISTRITO")]
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
