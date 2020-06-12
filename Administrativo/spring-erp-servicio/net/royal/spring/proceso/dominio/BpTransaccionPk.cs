using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpTransaccionPk
    {
        [Key]
        [Column("ID_TRANSACCION")]
        public Nullable<Int32> IdTransaccion { get; set; }

        public BpTransaccionPk()
        {

        }
        public BpTransaccionPk(Nullable<Int32> IdTransaccion)
        {
            this.IdTransaccion = IdTransaccion;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdTransaccion };
            return myObjArray;
        }

    }
}
