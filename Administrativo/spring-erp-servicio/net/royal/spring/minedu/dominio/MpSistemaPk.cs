using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio
{
    public class MpSistemaPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("id_si")]
        public Nullable<Int32> IdSistema { get; set; }
   

        public MpSistemaPk(){}

        public MpSistemaPk(Nullable<Int32> pIdSistema)
        {
            this.IdSistema = pIdSistema;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdSistema };
            return myObjArray;
        }
    }
}
