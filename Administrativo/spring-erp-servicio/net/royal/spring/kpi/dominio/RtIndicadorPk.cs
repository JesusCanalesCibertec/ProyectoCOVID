using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.kpi.dominio
{

/**
 * 
 * 
 * @tabla sgseguridadsys.RT_INDICADOR
*/
public class RtIndicadorPk {

	[Key]
	[Display(Name = "ID_INDICADOR")]
	[MaxLength(10)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_INDICADOR")]
	public String IdIndicador  { get; set; }

        public RtIndicadorPk() {
        }

        public RtIndicadorPk(String pIdIndicador) {
	this.IdIndicador = pIdIndicador;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdIndicador };
            return myObjArray;
        }
 }
}
