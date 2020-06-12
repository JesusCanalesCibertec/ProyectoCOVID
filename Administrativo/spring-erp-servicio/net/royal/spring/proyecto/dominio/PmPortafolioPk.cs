using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proyecto.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.PM_PROYECTO
*/
    public class PmPortafolioPk
    {

        [Key]
        [Display(Name = "ID_PORTAFOLIO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PORTAFOLIO")]
        public Nullable<Int32> IdPortafolio { get; set; }



        public PmPortafolioPk()
        {
        }

        public PmPortafolioPk(Nullable<Int32> pIdPortafolio)
        {
            this.IdPortafolio = pIdPortafolio;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdPortafolio };
            return myObjArray;
        }
    }
}
