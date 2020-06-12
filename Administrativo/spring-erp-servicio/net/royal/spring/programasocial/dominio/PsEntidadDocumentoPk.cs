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
 * @tabla sgseguridadsys.PS_ENTIDAD_DOCUMENTO
*/
public class PsEntidadDocumentoPk {

	[Key]
	[Display(Name = "ID_ENTIDAD")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_ENTIDAD")]
	public Nullable<Int32> IdEntidad  { get; set; }

	[Key]
	[Display(Name = "ID_TIPO_DOCUMENTO")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_TIPO_DOCUMENTO")]
	public String IdTipoDocumento  { get; set; }

        public PsEntidadDocumentoPk() {
        }

        public PsEntidadDocumentoPk(Nullable<Int32> pIdEntidad,String pIdTipoDocumento) {
	this.IdEntidad = pIdEntidad;
	this.IdTipoDocumento = pIdTipoDocumento;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdEntidad,IdTipoDocumento };
            return myObjArray;
        }
 }
}
