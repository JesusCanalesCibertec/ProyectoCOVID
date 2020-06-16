using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio
{
    public class MpAreamineduPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("SedeOficinaId")]
        public Nullable<Int32> Sedeid { get; set; }
   

        public MpAreamineduPk(){}

        public MpAreamineduPk(Nullable<Int32> pSedeid)
        {
            this.Sedeid = pSedeid;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Sedeid };
            return myObjArray;
        }
    }
}
