using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.covid.dominio
{
    public class PaisPk {

	    [Key]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("ID_PAIS")]
        public String IdPais { get; set; }

        public PaisPk() { }

        public PaisPk(String pIdPais)
        {
            this.IdPais = pIdPais;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdPais };
            return myObjArray;
        }
    }
}
