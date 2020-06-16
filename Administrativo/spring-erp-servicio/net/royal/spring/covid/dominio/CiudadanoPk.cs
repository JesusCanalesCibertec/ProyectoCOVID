using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.covid.dominio
{
    public class CiudadanoPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("COD_CIUDADANO")]
        public Nullable<Int32> IdCiudadano { get; set; }
   

        public CiudadanoPk(){}

        public CiudadanoPk(Nullable<Int32> pIdCiudadano)
        {
            this.IdCiudadano = pIdCiudadano;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdCiudadano };
            return myObjArray;
        }
    }
}
