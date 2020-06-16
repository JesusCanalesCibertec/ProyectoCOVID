using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio
{
    public class MpProyectorecursoperiodoPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PROYECTO")]
        public Nullable<Int32> IdProyecto { get; set; }

        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PERSONA")]
        public Nullable<Int32> IdPersona { get; set; }

        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PERIODO")]
        public Nullable<Int32> IdPeriodo { get; set; }




        public MpProyectorecursoperiodoPk() { }

        public MpProyectorecursoperiodoPk(Nullable<Int32> pIdProyecto, Nullable<Int32> pIdPersona, Nullable<Int32> pIdPeriodo)
        {
            this.IdProyecto = pIdProyecto;
            this.IdPersona = pIdPersona;
            this.IdPeriodo = pIdPeriodo;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProyecto, IdPersona, IdPeriodo };
            return myObjArray;
        }
    }
}
