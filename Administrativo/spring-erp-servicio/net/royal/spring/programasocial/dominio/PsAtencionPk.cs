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
 * @tabla sgseguridadsys.PS_ATENCION
*/
public class PsAtencionPk {

	[Key]
	[Display(Name = "ID_INSTITUCION")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_INSTITUCION")]
	public String IdInstitucion  { get; set; }

	[Key]
	[Display(Name = "ID_BENEFICIARIO")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_BENEFICIARIO")]
	public Nullable<Int32> IdBeneficiario  { get; set; }

	[Key]
	[Display(Name = "ID_ATENCION")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_ATENCION")]
	public Nullable<Int32> IdAtencion  { get; set; }

        public PsAtencionPk() {
        }

        public PsAtencionPk(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdAtencion) {
	this.IdInstitucion = pIdInstitucion;
	this.IdBeneficiario = pIdBeneficiario;
	this.IdAtencion = pIdAtencion;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion,IdBeneficiario,IdAtencion };
            return myObjArray;
        }
 }
}
