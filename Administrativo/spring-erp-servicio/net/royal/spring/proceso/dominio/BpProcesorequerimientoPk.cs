using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpProcesorequerimientoPk
    {

        [Key]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        [Key]
        [Column("ID_VERSION")]
        public Int32 IdVersion { get; set; }

        [Key]
        [Column("ID_REQUERIMIENTO")]
        public String IdRequerimiento { get; set; }

        public BpProcesorequerimientoPk()
        {

        }
        public BpProcesorequerimientoPk(String IdProceso, Int32 IdVersion, String IdRequerimiento)
        {
            this.IdProceso = IdProceso;
            this.IdVersion = IdVersion;
            this.IdRequerimiento = IdRequerimiento;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdVersion, IdRequerimiento };
            return myObjArray;
        }

    }
}
