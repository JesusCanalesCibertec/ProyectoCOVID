using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.asistencia.dominio
{

    /**
     * 
     * 
     * @tabla dbo.AS_Periodo
    */
    [Table("AS_PERIODO")]
    public class AsPeriodo : AsPeriodoPk
    {



        [Display(Name = "FechaDesde")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio ")]
        [Column("FECHADESDE")]

        public Nullable<DateTime> Fechadesde { get; set; }

        [Display(Name = "Tipo")]
        [MaxLength(3)]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("TIPO")]

        public String Tipo { get; set; }

        [Display(Name = "FechaHasta")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("FECHAHASTA")]

        public Nullable<DateTime> Fechahasta { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50)]
        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Display(Name = "PeriodoPlanilla")]
        [MaxLength(8)]
        [Column("PERIODOPLANILLA")]
        public String Periodoplanilla { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("ESTADO")]

        public String Estado { get; set; }

        [Display(Name = "TipoPlanilla")]
        [MaxLength(2)]
        [Column("TipoPlanilla")]
        public String TipoPlanilla { get; set; }

        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public AsPeriodo()
        {
        }
    }
}
