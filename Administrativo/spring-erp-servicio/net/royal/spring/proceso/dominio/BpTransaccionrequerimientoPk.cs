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
    public class BpTransaccionrequerimientoPk
    {
        [Key]
        [Column("ID_TRANSACCION")]
        public Nullable<Int32> IdTransaccion { get; set; }

        [Key]
        [Column("ID_REQUERIMIENTO")]
        public String IdRequerimiento { get; set; }

        public BpTransaccionrequerimientoPk() { }
        public BpTransaccionrequerimientoPk(Int32 IdTransaccion, String IdRequerimiento)
        {
            this.IdRequerimiento = IdRequerimiento;
            this.IdTransaccion = IdTransaccion;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdTransaccion, IdRequerimiento };
            return myObjArray;
        }
    }
}
