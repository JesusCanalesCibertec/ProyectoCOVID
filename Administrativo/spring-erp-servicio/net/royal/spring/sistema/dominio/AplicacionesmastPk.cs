using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.sistema.dominio
{

    /**
     * 
     * 
     * @tabla dbo.AplicacionesMast
*/
    public class AplicacionesmastPk
    {

        public static String MSG_APLICACIONCODIGO_LONGITUD = "El campo APLICACIONCODIGO solo permite {max} caracteres";
        public static String MSG_APLICACIONCODIGO_ESREQUERIDO = "El campo APLICACIONCODIGO no puede estar vacio";

        [Key]
        [Display(Name = "AplicacionCodigo")]
        [MaxLength(2)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("APLICACIONCODIGO")]
        public String Aplicacioncodigo { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Aplicacioncodigo };
            return myObjArray;
        }
    }
}
