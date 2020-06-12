using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpTipoelementoPk
    {

        [Key]
        [Column("ID_CLASE_ELEMENTO")]
        public String IdClaseelemento { get; set; }

        [Key]
        [Column("ID_TIPO_ELEMENTO")]
        public String IdTipoelemento { get; set; }

        public BpTipoelementoPk()
        {

        }
        public BpTipoelementoPk(String IdClaseelemento, String IdTipoelemento)
        {
            this.IdClaseelemento = IdClaseelemento;
            this.IdClaseelemento = IdClaseelemento;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdClaseelemento, IdTipoelemento };
            return myObjArray;
        }

    }
}
