using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio
{
    public class MpContratacionadendaentregablePk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_CODIGO")]
        public Nullable<Int32> IdCodigo { get; set; }


        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_CONTRATACION")]
        public Nullable<Int32> IdContratacion { get; set; }


        public MpContratacionadendaentregablePk(){}

        public MpContratacionadendaentregablePk(Nullable<Int32> pIdCodigo, Nullable<Int32> pIdContratacion)
        {
            this.IdCodigo = pIdCodigo;
            this.IdContratacion = pIdContratacion;
         
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdCodigo, IdContratacion };
            return myObjArray;
        }
    }
}
