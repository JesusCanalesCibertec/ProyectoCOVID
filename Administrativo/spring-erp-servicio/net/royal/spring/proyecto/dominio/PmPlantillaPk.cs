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
    public class PmPlantillaPk
    {

        [Key]
        [Column("PLANTILLA")]
        public Nullable<Int32> Plantilla { get; set; }

        public PmPlantillaPk()
        {
        }

        public PmPlantillaPk(Nullable<Int32> Plantilla)
        {
            this.Plantilla = Plantilla;
        }
        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Plantilla };
            return myObjArray;
        }
    }
}
