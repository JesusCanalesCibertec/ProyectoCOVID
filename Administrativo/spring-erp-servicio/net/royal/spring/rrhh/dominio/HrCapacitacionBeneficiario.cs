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
     * @tabla dbo.HR_Capacitacion_Beneficiario
*/
    [Table("HR_CAPACITACION_BENEFICIARIO")]
    public class HrCapacitacionBeneficiario : HrCapacitacionBeneficiarioPk
    {

        [Display(Name = "UltimoUsuario")]
        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "UltimaFechaModif")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        [Display(Name = "CALIFICACION")]
        [MaxLength(20)]
        [Column("CALIFICACION")]
        public String Calificacion { get; set; }

        [Display(Name = "COMENTARIO")]
        [Column("COMENTARIO")]
        public String Comentario { get; set; }

        [Display(Name = "FLAG_APROBO")]
        [MaxLength(20)]
        [Column("FLAG_APROBO")]
        public String Flagaprobo { get; set; }

        [Display(Name = "FLAG_PARTICIPATIVO")]
        [MaxLength(20)]
        [Column("FLAG_PARTICIPATIVO")]
        public String Flagparticipativo { get; set; }

        [Display(Name = "FLAG_ASISTIO")]
        [MaxLength(20)]
        [Column("FLAG_ASISTIO")]
        public String Flagasistio { get; set; }

        [Display(Name = "MOTIVO_INASISTENCIA")]
        [Column("MOTIVO_INASISTENCIA")]
        public String Motivoinasistencia { get; set; }

        [Column("ID_RENDIEMIENTO")]
        public String idRedimiento { get; set; }

        [Column("ID_PARTICIPACION")]
        public String idParticipacion { get; set; }

        [NotMapped]
        public String auxInstitucion { get; set; }

        [NotMapped]
        public String auxPersonaNombreCompleto { get; set; }

        [NotMapped]
        public String sexo { get; set; }

        [NotMapped]
        public Int32? edad { get; set; }

        public HrCapacitacionBeneficiario()
        {
        }
    }
}
