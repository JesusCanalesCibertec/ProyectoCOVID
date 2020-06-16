using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpProcesoPk
    {
        [Key]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        [Key]
        [Column("ID_VERSION")]
        public Nullable<Int32> IdVersion { get; set; }

        public BpProcesoPk()
        {

        }
        public BpProcesoPk(String IdProceso, Nullable<Int32> IdVersion)
        {
            this.IdProceso = IdProceso;
            this.IdVersion = IdVersion;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdVersion };
            return myObjArray;
        }

    }
}
