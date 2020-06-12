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
     * @tabla dbo.SeguridadPerfilUsuario
*/
    public class SeguridadperfilusuarioPk
    {

        [Key]
        [Display(Name = "Perfil")]
        [MaxLength(20)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("PERFIL")]
        public String Perfil { get; set; }

        [Key]
        [Display(Name = "Usuario")]
        [MaxLength(20)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("USUARIO")]
        public String Usuario { get; set; }

        public SeguridadperfilusuarioPk() { }

        public SeguridadperfilusuarioPk(string pPerfil, string pUsuario)
        {
            this.Perfil = pPerfil;
            this.Usuario = pUsuario;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Perfil, Usuario };
            return myObjArray;
        }
    }
}
