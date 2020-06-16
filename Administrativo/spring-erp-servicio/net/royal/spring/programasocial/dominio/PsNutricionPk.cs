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
 * @tabla sgseguridadsys.PS_NUTRICION
*/
public class PsNutricionPk {

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
	[Display(Name = "ID_NUTRICION")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_NUTRICION")]
	public Nullable<Int32> IdNutricion  { get; set; }

        public PsNutricionPk() {
        }

        public PsNutricionPk(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdNutricion) {
	this.IdInstitucion = pIdInstitucion;
	this.IdBeneficiario = pIdBeneficiario;
	this.IdNutricion = pIdNutricion;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion,IdBeneficiario,IdNutricion };
            return myObjArray;
        }
 }
}
