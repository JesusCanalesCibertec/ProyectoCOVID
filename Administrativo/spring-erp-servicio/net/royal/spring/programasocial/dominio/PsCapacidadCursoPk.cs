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
 * @tabla sgseguridadsys.PS_CAPACIDAD_CURSO
*/
public class PsCapacidadCursoPk {

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
	[Display(Name = "ID_CAPACIDAD")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_CAPACIDAD")]
	public Nullable<Int32> IdCapacidad  { get; set; }

	[Key]
	[Display(Name = "ID_CURSO")]
	[MaxLength(6)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_CURSO")]
	public String IdCurso  { get; set; }

        public PsCapacidadCursoPk() {
        }

        public PsCapacidadCursoPk(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdCapacidad,String pIdCurso) {
	this.IdInstitucion = pIdInstitucion;
	this.IdBeneficiario = pIdBeneficiario;
	this.IdCapacidad = pIdCapacidad;
	this.IdCurso = pIdCurso;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion,IdBeneficiario,IdCapacidad,IdCurso };
            return myObjArray;
        }
 }
}
