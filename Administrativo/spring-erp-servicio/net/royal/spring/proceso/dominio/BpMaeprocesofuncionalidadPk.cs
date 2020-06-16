using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpMaeprocesofuncionalidadPk
    {

        [Key]
        [Column("ID_FUNCIONALIDAD")]
        public String IdFuncionalidad { get; set; }

        [Key]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        public BpMaeprocesofuncionalidadPk()
        {

        }
        public BpMaeprocesofuncionalidadPk(String IdFuncionalidad, String IdProceso)
        {
            this.IdFuncionalidad = IdFuncionalidad;
            this.IdProceso = IdProceso;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProceso, IdFuncionalidad };
            return myObjArray;
        }

    }
}
