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
    [Table("PM_PLANTILLA_TAREAS", Schema = "sgseguridadsys")]
    public class PmPlantillaTareas : PmPlantillaTareasPk
    {
        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("DIAS")]
        public Nullable<Int32> Dias { get; set; }

        [Column("ORDEN")]
        public Nullable<Int32> Orden { get; set; }

        [Column("TIPO")]
        public String Tipo { get; set; }

        [Column("FLAG_COMISION")]
        public String FlagComision { get; set; }

        [Column("ESTADO")]
        public String Estado { get; set; }

        [Column("ULTIMOUSUARIO")]
        public String UltimoUsuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> UltimaFechaModif { get; set; }

        public PmPlantillaTareas()
        {
        }
    }
}
