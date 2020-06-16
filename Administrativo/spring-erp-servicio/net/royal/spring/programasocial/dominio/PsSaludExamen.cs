using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.PS_SALUD_EXAMEN
*/
    [Table("PS_SALUD_EXAMEN", Schema = "sgseguridadsys")]
    public class PsSaludExamen : PsSaludExamenPk
    {
        [Column("ID_RESULTADO")]
        public String IdResultado { get; set; }

        [Column("CREACION_USUARIO")]
        public String CreacionUsuario { get; set; }

        [Column("ID_RESULTADO_NUMERICO")]
        public Double? IdResultadoNumerico { get; set; }

        [Display(Name = "CREACION_FECHA")]
        [Column("CREACION_FECHA")]
        public Nullable<DateTime> CreacionFecha { get; set; }


        [Display(Name = "CREACION_TERMINAL")]
        [MaxLength(50)]
        [Column("CREACION_TERMINAL")]
        public String CreacionTerminal { get; set; }


        [Display(Name = "MODIFICACION_USUARIO")]
        [MaxLength(50)]
        [Column("MODIFICACION_USUARIO")]
        public String ModificacionUsuario { get; set; }


        [Display(Name = "MODIFICACION_FECHA")]
        [Column("MODIFICACION_FECHA")]
        public Nullable<DateTime> ModificacionFecha { get; set; }


        [Display(Name = "MODIFICACION_TERMINAL")]
        [MaxLength(50)]
        [Column("MODIFICACION_TERMINAL")]
        public String ModificacionTerminal { get; set; }

        public PsSaludExamen()
        {
        }
    }
}
