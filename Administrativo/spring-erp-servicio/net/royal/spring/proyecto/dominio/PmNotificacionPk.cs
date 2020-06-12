using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proyecto.dominio
{

    public class PmNotificacionPk
    {

        [Key]
        [Column("ID_NOTIFICACION")]
        public Nullable<Int32> IdNotificacion { get; set; }

        public PmNotificacionPk()
        {
        }

        public PmNotificacionPk(Nullable<Int32> IdNotificacion)
        {
            this.IdNotificacion = IdNotificacion;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdNotificacion };
            return myObjArray;
        }
    }
}
