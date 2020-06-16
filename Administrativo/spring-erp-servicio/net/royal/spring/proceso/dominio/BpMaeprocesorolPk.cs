using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpMaeprocesorolPk
    {

        [Key]
        [Column("ID_ROL")]
        public String IdRol { get; set; }

        [Key]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        public BpMaeprocesorolPk()
        {

        }
        public BpMaeprocesorolPk(String IdProceso, String IdRol)
        {
            this.IdRol = IdRol;
            this.IdProceso = IdProceso;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdRol };
            return myObjArray;
        }

    }
}
