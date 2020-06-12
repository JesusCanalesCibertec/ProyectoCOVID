using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.BP_AUDITORIA
*/
    public class BpTransaccionseguimientoPk
    {
        [Key]
        [Column("ID_TRANSACCION")]
        public Nullable<Int32> IdTransaccion { get; set; }

        [Key]
        [Column("ID_SEGUIMIENTO")]
        public Nullable<Int32> IdSeguimiento { get; set; }

        public BpTransaccionseguimientoPk() { }
        public BpTransaccionseguimientoPk(Int32 IdTransaccion, Int32 IdSecuencia)
        {
            this.IdSeguimiento = IdSecuencia;
            this.IdTransaccion = IdTransaccion;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdTransaccion, IdSeguimiento };
            return myObjArray;
        }
    }
}
