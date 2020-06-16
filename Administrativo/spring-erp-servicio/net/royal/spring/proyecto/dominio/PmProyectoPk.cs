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
    public class PmProyectoPk
    {

        [Key]
        [Display(Name = "ID_PORTAFOLIO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PORTAFOLIO")]
        public Nullable<Int32> IdPortafolio { get; set; }

        [Key]
        [Display(Name = "ID_PROGRAMA")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PROGRAMA")]
        public Nullable<Int32> IdPrograma { get; set; }

        [Key]
        [Display(Name = "ID_PROYECTO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PROYECTO")]
        public Nullable<Int32> IdProyecto { get; set; }

        public PmProyectoPk()
        {
        }

        public PmProyectoPk(Nullable<Int32> pIdPortafolio, Nullable<Int32> pIdPrograma, Nullable<Int32> pIdProyecto)
        {
            this.IdPortafolio = pIdPortafolio;
            this.IdPrograma = pIdPrograma;
            this.IdProyecto = pIdProyecto;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdPortafolio, IdPrograma, IdProyecto };
            return myObjArray;
        }
    }
}
