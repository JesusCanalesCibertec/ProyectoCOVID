using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.salud.dominio
{


public class SsCie10grupoPk {

        [Key]
        [Display(Name = "ID_CAPITULO")]
        [MaxLength(15)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_CAPITULO")]
        public String IdCapitulo { get; set; }

        [Key]
	[Display(Name = "ID_GRUPO")]
	[MaxLength(15)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_GRUPO")]
	public String IdGrupo  { get; set; }

        public SsCie10grupoPk() {
        }

        public SsCie10grupoPk(String pIdGrupo, String pIdCapitulo) {
	        this.IdGrupo = pIdGrupo;
            this.IdCapitulo = pIdCapitulo;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdGrupo, IdCapitulo };
            return myObjArray;
        }
 }
}
