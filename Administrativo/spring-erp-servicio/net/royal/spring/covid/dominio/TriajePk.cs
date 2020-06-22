using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.covid.dominio
{
    public class TriajePk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("COD_Triaje")]
        public Nullable<Int32> IdTriaje { get; set; }


        public TriajePk(){}

        public TriajePk(Nullable<Int32> pIdTriaje)
        {
            this.IdTriaje = pIdTriaje;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdTriaje };
            return myObjArray;
        }
    }
}
