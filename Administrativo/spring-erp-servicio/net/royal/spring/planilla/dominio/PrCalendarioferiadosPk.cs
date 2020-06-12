using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.planilla.dominio
{
    public class PrCalendarioferiadosPk
    {
        private string _FechaMesDia;
        [Key]
        [Display(Name = "FechaMesDia")]
        [MaxLength(2)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("FechaMesDia")]
        public String FechaMesDia
        {
            get { return (_FechaMesDia == null) ? "" : _FechaMesDia.Trim(); }
            set { _FechaMesDia = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { FechaMesDia };
            return myObjArray;
        }
    }
}
