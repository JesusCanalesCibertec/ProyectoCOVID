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
    [Table("PM_PLANTILLA", Schema = "sgseguridadsys")]
    public class PmPlantilla : PmPlantillaPk
    {

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("ESTADO")]
        public String Estado { get; set; }

        [Column("ULTIMOUSUARIO")]
        public String UltimoUsuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> UltimaFechaModif { get; set; }

        [NotMapped]
        public List<PmPlantillaTareas> listadetalle;

        public PmPlantilla()
        {
            listadetalle = new List<PmPlantillaTareas>();
        }
    }
}
