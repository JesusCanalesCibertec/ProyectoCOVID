using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.BP_ENLACE
    */
    public class BpEnlacePk {

	    [Key]
	    [Display(Name = "ID_ENLACE")]
	    [MaxLength(200)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("ID_ENLACE")]
	    public String IdEnlace  { get; set; }

        [Key]
        [Display(Name = "ID_CORREO")]
        [MaxLength(200)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_CORREO")]
        public String IdCorreo { get; set; }

        public BpEnlacePk() { }
        public BpEnlacePk(String _IdEnlace,String _IdCorreo) {
            IdEnlace = _IdEnlace;
            IdCorreo = _IdCorreo;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdEnlace, IdCorreo };
            return myObjArray;
        }
    }
}
