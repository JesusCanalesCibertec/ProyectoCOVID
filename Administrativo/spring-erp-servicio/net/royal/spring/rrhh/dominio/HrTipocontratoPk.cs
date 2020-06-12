using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.rrhh.dominio
{

    public class HrTipocontratoPk
    {

        private String _Tipocontrato;

        [Key]
        [Display(Name = "TipoContrato")]
        [MaxLength(2)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("TIPOCONTRATO")]
        public String Tipocontrato
        {
            get { return (_Tipocontrato == null) ? "" : _Tipocontrato.Trim(); }
            set { _Tipocontrato = value; }
        }
        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Tipocontrato };
            return myObjArray;
        }
    }
}
