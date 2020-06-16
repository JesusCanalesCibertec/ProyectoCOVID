using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpMaeprocesoelementoPk
    {

        [Key]
        [Column("ID_ELEMENTO")]
        public String IdElemento { get; set; }

        [Key]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        public BpMaeprocesoelementoPk()
        {

        }
        public BpMaeprocesoelementoPk(String IdProceso, String IdElemento)
        {
            this.IdElemento = IdElemento;
            this.IdProceso = IdProceso;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdElemento };
            return myObjArray;
        }

    }
}
