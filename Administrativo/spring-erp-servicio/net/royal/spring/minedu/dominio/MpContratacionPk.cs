using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio
{
    public class MpContratacionPk
    {

        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_CONTRATACION")]
        public Nullable<Int32> IdContratacion { get; set; }



        public MpContratacionPk(){}

        public MpContratacionPk(Nullable<Int32> pIdContratacion)
        {
            this.IdContratacion = pIdContratacion;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdContratacion};
            return myObjArray;
        }
    }
}
