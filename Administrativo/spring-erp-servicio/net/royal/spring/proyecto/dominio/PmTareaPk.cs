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
     * @tabla sgseguridadsys.PM_TAREA
*/
    public class PmTareaPk
    {

        [Key]
        [Display(Name = "ID_PROYECTO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PROYECTO")]
        public Nullable<Int32> IdProyecto { get; set; }

        [Key]
        [Display(Name = "ID_TAREA")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_TAREA")]
        public Nullable<Int32> IdTarea { get; set; }

        public PmTareaPk()
        {
        }

        public PmTareaPk(Nullable<Int32> pIdProyecto, Nullable<Int32> pIdTarea)
        {
            this.IdProyecto = pIdProyecto;
            this.IdTarea = pIdTarea;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdProyecto, IdTarea };
            return myObjArray;
        }
    }
}
