using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.rrhh.dominio
{

/**
 * 
 * 
 * @tabla dbo.HR_CursoDescripcion
*/
public class HrCursodescripcionPk {

	[Key]
	[Display(Name = "Curso")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("CURSO")]
	public Nullable<Int32> Curso  { get; set; }

        public HrCursodescripcionPk() {
        }

        public HrCursodescripcionPk(Nullable<Int32> pCurso) {
	this.Curso = pCurso;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Curso };
            return myObjArray;
        }
 }
}
