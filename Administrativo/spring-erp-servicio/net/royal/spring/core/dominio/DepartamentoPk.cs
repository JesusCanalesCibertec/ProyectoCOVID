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
     * @tabla dbo.Departamento
    */
    public class DepartamentoPk {

        private String _Departamento;

	    [Key]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("ID_DEPARTAMENTO")]
	    public String Departamento
        {
            get { return (_Departamento == null) ? "" : _Departamento.Trim(); }
            set { _Departamento = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Departamento };
            return myObjArray;
        }
    }
}
