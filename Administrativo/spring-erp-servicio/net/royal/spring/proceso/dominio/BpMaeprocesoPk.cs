using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpMaeprocesoPk
    {
        [Key]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        public BpMaeprocesoPk()
        {

        }
        public BpMaeprocesoPk(String IdProceso)
        {
            this.IdProceso = IdProceso;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso };
            return myObjArray;
        }

        internal static BpMaeproceso obtenerPorId(BpMaeprocesoPk id)
        {
            throw new NotImplementedException();
        }
    }
}
