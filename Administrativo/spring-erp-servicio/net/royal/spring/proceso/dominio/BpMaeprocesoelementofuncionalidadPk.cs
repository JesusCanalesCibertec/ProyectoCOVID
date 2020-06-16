using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpMaeprocesoelementofuncionalidadPk
    {
        [Key]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        [Key]
        [Column("ID_ELEMENTO")]
        public String IdElemento { get; set; }

   
        [Key]
        [Column("ID_FUNCIONALIDAD")]
        public String IdFuncionalidad { get; set; }

        public BpMaeprocesoelementofuncionalidadPk()
        {

        }
        public BpMaeprocesoelementofuncionalidadPk(String IdProceso, String IdElemento, String IdFuncionalidad)
        {
            this.IdProceso = IdProceso;
            this.IdElemento = IdElemento;
            this.IdFuncionalidad = IdFuncionalidad;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdElemento, IdFuncionalidad };
            return myObjArray;
        }

    }
}
