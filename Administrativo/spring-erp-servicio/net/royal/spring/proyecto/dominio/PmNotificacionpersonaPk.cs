using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proyecto.dominio
{

    public class PmNotificacionpersonaPk
    {

        [Key]
        [Column("ID_NOTIFICACION")]
        public Nullable<Int32> IdNotificacion { get; set; }

        [Key]
        [Column("ID_PERSONA")]
        public Nullable<Int32> IdPersona { get; set; }

        public PmNotificacionpersonaPk()
        {
        }

        public PmNotificacionpersonaPk(Nullable<Int32> IdNotificacion, Nullable<Int32> IdPersona)
        {
            this.IdNotificacion = IdNotificacion;
            this.IdPersona = IdPersona;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdNotificacion, IdPersona };
            return myObjArray;
        }
    }
}
