using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio
{


    public class PsInstitucionAreaPk {

	    [Key]
	    [Display(Name = "ID_INSTITUCION")]
	    [MaxLength(5)]
	    [Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	    [Column("ID_INSTITUCION")]
        public String IdInstitucion { get; set; }

        [Key]
        [Display(Name = "ID_AREA")]
        [MaxLength(5)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_AREA")]
        public String IdArea  { get; set; }

        public PsInstitucionAreaPk() {
        }

        public PsInstitucionAreaPk(String pIdInstitucion, String pIdArea) {

	    this.IdInstitucion = pIdInstitucion;
        this.IdArea = pIdArea;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdInstitucion, IdArea };
            return myObjArray;
        }
 }
}
