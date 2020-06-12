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
     * @tabla dbo.SY_PLANTILLA
    */
    public class SyPlantillaPk {

        [Key]
        [Display(Name = "APLICACIONCODIGO")]
        [MaxLength(2)]
        [Column("APLICACIONCODIGO")]
        public String Aplicacioncodigo { get; set; }

        [Key]
        [Display(Name = "PLANTILLACODIGO")]
        [MaxLength(10)]
        [Column("PLANTILLACODIGO")]
        public String Plantillacodigo { get; set; }

        public SyPlantillaPk() { }
        public SyPlantillaPk(String _aplicacioncodigo, String _plantillacodigo) {
            Aplicacioncodigo = _aplicacioncodigo;
            Plantillacodigo = _plantillacodigo;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Aplicacioncodigo, Plantillacodigo };
            return myObjArray;
        }
    }
}
