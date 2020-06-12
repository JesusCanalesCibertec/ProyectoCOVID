using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio
{
    public class MpProyectoPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PROYECTO")]
        public Nullable<Int32> IdProyecto { get; set; }
   

        public MpProyectoPk(){}

        public MpProyectoPk(Nullable<Int32> pIdProyecto)
        {
            this.IdProyecto = pIdProyecto;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProyecto };
            return myObjArray;
        }
    }
}
