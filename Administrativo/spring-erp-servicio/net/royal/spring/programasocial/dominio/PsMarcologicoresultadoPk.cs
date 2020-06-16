using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio
{

/**
 * 
 * 
 * @tabla sgseguridadsys.PS_INSTITUCION
*/
public class PsMarcologicoresultadoPk {

	[Key]
	[Display(Name = "ID_MARCO_LOGICO")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_MARCO_LOGICO")]
	public String IdMarcologico  { get; set; }

        [Key]
        [Display(Name = "ID_RESULTADO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_RESULTADO")]
        public String IdResultado { get; set; }

        public PsMarcologicoresultadoPk() {
        }

        public PsMarcologicoresultadoPk(String pIdMarcologico, String pIdResultado) {

	        this.IdMarcologico = pIdMarcologico;
            this.IdResultado = pIdResultado;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdMarcologico,IdResultado };
            return myObjArray;
        }
 }
}
