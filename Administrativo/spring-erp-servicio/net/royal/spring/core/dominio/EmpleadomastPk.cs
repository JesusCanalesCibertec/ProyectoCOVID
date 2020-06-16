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
     * @tabla dbo.EmpleadoMast
*/
    public class EmpleadomastPk
    {
        [Key]
        [Display(Name = "CompaniaSocio")]
        [MaxLength(8)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("COMPANIASOCIO")]
        public String Companiasocio { get; set; }

        [Key]
        [Display(Name = "Empleado")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("EMPLEADO")]
        public Nullable<Int32> Empleado { get; set; }

        public EmpleadomastPk()
        {
        }

        public EmpleadomastPk(String _Companiasocio, int? _Empleado)
        {
            this.Empleado = _Empleado;
            this.Companiasocio = _Companiasocio;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companiasocio, Empleado };
            return myObjArray;
        }


    }
}