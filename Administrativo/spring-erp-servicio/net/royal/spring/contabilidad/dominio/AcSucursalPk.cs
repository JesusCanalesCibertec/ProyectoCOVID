using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.contabilidad.dominio
{

    /**
     * 
     * 
     * @tabla dbo.AC_Sucursal
    */
    public class AcSucursalPk {

        private string _Sucursal;

        [Key]
	    [Display(Name = "Sucursal")]
        [MaxLength(4)]
	    [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("SUCURSAL")]

        public String Sucursal  {
            get { return (_Sucursal == null) ? "" : _Sucursal.Trim(); }
            set { _Sucursal = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Sucursal };
            return myObjArray;
        }
    }
}
