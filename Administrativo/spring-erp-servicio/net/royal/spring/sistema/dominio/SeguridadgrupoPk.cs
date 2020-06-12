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
     * @tabla dbo.SeguridadGrupo
*/
    public class SeguridadgrupoPk
    {

        [Key]
        [Display(Name = "AplicacionCodigo")]
        [MaxLength(2)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("APLICACIONCODIGO")]
        public String Aplicacioncodigo { get; set; }

        [Key]
        [Display(Name = "Grupo")]
        [MaxLength(6)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("GRUPO")]
        public String Grupo { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Aplicacioncodigo, Grupo };
            return myObjArray;
        }
    }
}
