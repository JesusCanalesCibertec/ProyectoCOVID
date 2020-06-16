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
 * @tabla sgseguridadsys.PS_SALUD_TERAPIA
*/
public class PsSaludTerapiaPk {

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
	[Display(Name = "ID_SALUD")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_SALUD")]
	public Nullable<Int32> IdSalud  { get; set; }

	[Key]
	[Display(Name = "ID_TERAPIA")]
	[MaxLength(5)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_TERAPIA")]
	public String IdTerapia  { get; set; }

        public PsSaludTerapiaPk() {
        }

        public PsSaludTerapiaPk(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTerapia) {
	this.IdInstitucion = pIdInstitucion;
	this.IdBeneficiario = pIdBeneficiario;
	this.IdSalud = pIdSalud;
	this.IdTerapia = pIdTerapia;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion,IdBeneficiario,IdSalud,IdTerapia };
            return myObjArray;
        }
 }
}
