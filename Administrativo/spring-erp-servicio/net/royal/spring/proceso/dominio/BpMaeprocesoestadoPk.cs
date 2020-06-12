using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpMaeprocesoestadoPk
    {

        [Key]
        [Column("ID_ESTADO")]
        public String IdEstado { get; set; }

        [Key]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        public BpMaeprocesoestadoPk()
        {

        }
        public BpMaeprocesoestadoPk(String IdEstado, String IdProceso)
        {
            this.IdEstado = IdEstado;
            this.IdProceso = IdProceso;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdEstado };
            return myObjArray;
        }

    }
}
