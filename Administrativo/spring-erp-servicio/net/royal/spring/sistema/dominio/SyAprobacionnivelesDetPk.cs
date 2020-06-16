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
 * @tabla dbo.SY_AprobacionNiveles_Det
*/
    public class SyAprobacionnivelesDetPk {

	    [Key]
	    [Display(Name = "Codigo")]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("CODIGO")]
	    public Nullable<Int32> Codigo  { get; set; }

	    [Key]
	    [Display(Name = "Nivel")]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("NIVEL")]
	    public Nullable<Int32> Nivel  { get; set; }

	    [Key]
	    [Display(Name = "Usuario")]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("USUARIO")]
	    public Nullable<Int32> Usuario  { get; set; }

        [Key]
        [Display(Name = "COMPANYOWNER")]
        [MaxLength(8)]
        [Column("COMPANYOWNER")]
        public String CompanyOwner { get; set; }

        public SyAprobacionnivelesDetPk() { }
        public SyAprobacionnivelesDetPk(Nullable<Int32> _codigo, Nullable<Int32> _nivel, Nullable<Int32> _usuario) {
            this.Codigo = _codigo;
            this.Nivel = _nivel;
            this.Usuario = _usuario;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Codigo, Nivel, Usuario, CompanyOwner };
            return myObjArray;
        }
    }
}
