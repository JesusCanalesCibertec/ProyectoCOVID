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
 * @tabla sgseguridadsys.PS_SOCIO_AMBIENTAL
*/
public class PsSocioAmbientalPk {

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
	[Display(Name = "ID_SOCIO_AMBIENTAL")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_SOCIO_AMBIENTAL")]
	public Nullable<Int32> IdSocioAmbiental  { get; set; }

        public PsSocioAmbientalPk() {
        }

        public PsSocioAmbientalPk(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSocioAmbiental) {
	this.IdInstitucion = pIdInstitucion;
	this.IdBeneficiario = pIdBeneficiario;
	this.IdSocioAmbiental = pIdSocioAmbiental;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion,IdBeneficiario,IdSocioAmbiental };
            return myObjArray;
        }
 }
}
