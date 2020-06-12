using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.rrhh.dominio
{

    /**
     * 
     * 
     * @tabla dbo.HR_EncuestaPreguntaValores
*/
    [Table("HR_ENCUESTAPREGUNTAVALORES")]
    public class HrEncuestapreguntavalores : HrEncuestapreguntavaloresPk
    {

        [Display(Name = "Descripcion")]
        [MaxLength(100)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Display(Name = "Calificacion")]
        [MaxLength(1)]
        [Column("CALIFICACION")]
        public String Calificacion { get; set; }

        [Display(Name = "UltimoUsuario")]
        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "UltimaFechaModif")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        [Column("PESO")]
        public Nullable<Decimal> Peso { get; set; }

        public HrEncuestapreguntavalores()
        {
        }
    }
}
