using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proyecto.dominio
{

    public class PmPlantillaTareasPk
    {

        [Key]
        [Column("PLANTILLA")]
        public Nullable<Int32> Plantilla { get; set; }

        [Key]
        [Column("SECUENCIA")]
        public Nullable<Int32> Secuencia { get; set; }

        public PmPlantillaTareasPk()
        {
        }
        public PmPlantillaTareasPk(Nullable<Int32> pPlantilla, Nullable<Int32> pSecuencia)
        {
            this.Plantilla = pPlantilla;
            this.Secuencia = pSecuencia;

        }



        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Plantilla, Secuencia };
            return myObjArray;
        }
    }
}
