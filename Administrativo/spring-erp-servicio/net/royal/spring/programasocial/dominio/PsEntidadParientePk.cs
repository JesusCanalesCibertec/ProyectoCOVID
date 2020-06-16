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
 * @tabla sgseguridadsys.PS_ENTIDAD_PARIENTE
*/
public class PsEntidadParientePk {

	[Key]
	[Display(Name = "ID_ENTIDAD")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_ENTIDAD")]
	public Nullable<Int32> IdEntidad  { get; set; }

	[Key]
	[Display(Name = "ID_PARIENTE")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_PARIENTE")]
	public Nullable<Int32> IdPariente  { get; set; }

        public PsEntidadParientePk() {
        }

        public PsEntidadParientePk(Nullable<Int32> pIdEntidad,Nullable<Int32> pIdPariente) {
	this.IdEntidad = pIdEntidad;
	this.IdPariente = pIdPariente;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdEntidad,IdPariente };
            return myObjArray;
        }
 }
}
