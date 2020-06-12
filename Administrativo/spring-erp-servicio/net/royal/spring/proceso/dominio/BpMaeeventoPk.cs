using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpMaeeventoPk
    {
        [Key]
        [Column("ID_EVENTO")]
        public String IdEvento { get; set; }

        public BpMaeeventoPk()
        {

        }
        public BpMaeeventoPk(String IdEvento)
        {
            this.IdEvento = IdEvento;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdEvento };
            return myObjArray;
        }

       
    }
}
