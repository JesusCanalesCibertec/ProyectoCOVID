using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio
{
    public class MpProyectorecursoPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PROYECTO")]
        public Nullable<Int32> IdProyecto { get; set; }

            [Key]
            [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
            [Column("ID_RECURSO")]
            public Nullable<Int32> IdRecurso { get; set; }



        public MpProyectorecursoPk(){}

        public MpProyectorecursoPk(Nullable<Int32> pIdProyecto, Nullable<Int32> pIdRecurso)
        {
            this.IdProyecto = pIdProyecto;
            this.IdRecurso = pIdRecurso;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProyecto, IdRecurso };
            return myObjArray;
        }
    }
}
