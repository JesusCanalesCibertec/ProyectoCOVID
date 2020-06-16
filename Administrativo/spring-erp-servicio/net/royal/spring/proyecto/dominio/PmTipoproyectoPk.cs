using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proyecto.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.PM_TIPO_PROYECTO
*/
    public class PmTipoproyectoPk
    {
        [Key]
        [Column("ID_TIPO_PROYECTO")]
        public String IdTipoProyecto { get; set; }

        public PmTipoproyectoPk()
        {
        }

        public PmTipoproyectoPk(String IdTipoProyecto)
        {
            this.IdTipoProyecto = IdTipoProyecto;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdTipoProyecto };
            return myObjArray;
        }
    }
}
