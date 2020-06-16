using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpMaerequerimientoPk
    {
        [Key]
        [Column("ID_REQUERIMIENTO")]
        public String IdRequerimiento { get; set; }

        public BpMaerequerimientoPk()
        {

        }
        public BpMaerequerimientoPk(String IdRequerimiento)
        {
            this.IdRequerimiento = IdRequerimiento;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdRequerimiento };
            return myObjArray;
        }

       
    }
}
