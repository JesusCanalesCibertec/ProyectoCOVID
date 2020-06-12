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
 * @tabla sgseguridadsys.RT_INDICADOR_META
*/
public class RtIndicadorMetaPk {

	[Key]
	[Display(Name = "ID_INDICADOR")]
	[MaxLength(10)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_INDICADOR")]
	public String IdIndicador  { get; set; }

	[Key]
	[Display(Name = "ID_META")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_META")]
	public Nullable<Int32> IdMeta  { get; set; }

        public RtIndicadorMetaPk() {
        }

        public RtIndicadorMetaPk(String pIdIndicador,Nullable<Int32> pIdMeta) {
	this.IdIndicador = pIdIndicador;
	this.IdMeta = pIdMeta;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdIndicador,IdMeta };
            return myObjArray;
        }
 }
}
