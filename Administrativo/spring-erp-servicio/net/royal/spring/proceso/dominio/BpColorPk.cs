using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    public class BpColorPk
    {

        [Key]
        [Column("Color")]
        public String IdColor { get; set; }

        

        public BpColorPk()
        {

        }
        public BpColorPk(String IdColor)
        {
            this.IdColor = IdColor;
            this.IdColor = IdColor;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdColor };
            return myObjArray;
        }

    }
}
