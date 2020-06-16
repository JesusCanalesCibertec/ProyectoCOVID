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
    public class BpTransaccionelementoPk
    {
        [Key]
        [Column("ID_TRANSACCION")]
        public Nullable<Int32> IdTransaccion { get; set; }

        [Key]
        [Column("ACTUAL_ID_PROCESO")]
        public String ActualIdProceso { get; set; }

        [Key]
        [Column("ACTUAL_ID_ELEMENTO")]
        public String ActualIdElemento { get; set; }

        [Key]
        [Column("SIGUIENTE_ID_PROCESO")]
        public String SiguienteIdProceso { get; set; }

        [Key]
        [Column("SIGUIENTE_ID_ELEMENTO")]
        public String SiguienteIdElemento { get; set; }

        public BpTransaccionelementoPk() { }
        public BpTransaccionelementoPk(Int32 IdTransaccion, String ActualIdProceso, String ActualIdElemento, String SiguienteIdProceso, String SiguienteIdElemento)
        {
            this.ActualIdProceso = ActualIdProceso;
            this.IdTransaccion = IdTransaccion;
            this.SiguienteIdElemento = SiguienteIdElemento;
            this.SiguienteIdProceso = SiguienteIdProceso;
            this.ActualIdElemento = ActualIdElemento;
            this.ActualIdProceso = ActualIdProceso;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdTransaccion, ActualIdProceso, ActualIdElemento
            , SiguienteIdProceso, SiguienteIdElemento};
            return myObjArray;
        }
    }
}
