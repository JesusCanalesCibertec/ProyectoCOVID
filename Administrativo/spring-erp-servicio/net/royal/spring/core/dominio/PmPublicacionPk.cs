using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.core.dominio
{

    /**
     * 
     * 
     * @tabla dbo.PmPublicacionPk
*/
    public class PmPublicacionPk
    {

        [Key]
        [Column("ID_APLICACION")]
        public String IdAplicacion { get; set; }

        [Key]
        [Column("ID_PUBLICACION")]
        public Nullable<Int32> IdPubicacion { get; set; }

        public PmPublicacionPk()
        {
        }

        public PmPublicacionPk(String IdAplicacion, Int32 IdPubicacion)
        {
            this.IdAplicacion = IdAplicacion;
            this.IdPubicacion = IdPubicacion;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdAplicacion, IdPubicacion };
            return myObjArray;
        }
    }
}
