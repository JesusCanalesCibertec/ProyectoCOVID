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
     * @tabla dbo.Usuario
*/
    public class UsuarioPk
    {

        public static String MSG_USUARIO_LONGITUD = "El campo USUARIO solo permite {max} caracteres";
        public static String MSG_USUARIO_ESREQUERIDO = "El campo USUARIO no puede estar vacío";

        [Key]
        [Display(Name = "Usuario")]
        [MaxLength(20)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("USUARIO")]
        public String Usuario { get; set; }

        public UsuarioPk() { }
        public UsuarioPk(String _usuario)
        {
            Usuario = _usuario;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Usuario };
            return myObjArray;
        }
    }
}
