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
     * @tabla dbo.AS_TipoHorario
    */
    [Table("AS_TIPOHORARIO")]
    public class AsTipohorario : AsTipohorarioPk
    {

        [Display(Name = "DescripcionLocal")]
        [MaxLength(100)]
        [Column("DESCRIPCIONLOCAL")]

        public String Descripcionlocal { get; set; }

        [Display(Name = "DerechoVacacional")]
        [Column("DERECHOVACACIONAL")]
        public Nullable<Decimal> DerechoVacacional { get; set; }

        [Display(Name = "Lunes")]
        [Column("LUNES")]
        public Nullable<Int32> Lunes { get; set; }

        [Display(Name = "Martes")]
        [Column("MARTES")]

        public Nullable<Int32> Martes { get; set; }

        [Display(Name = "Miercoles")]
        [Column("MIERCOLES")]

        public Nullable<Int32> Miercoles { get; set; }

        [Display(Name = "Jueves")]
        [Column("JUEVES")]

        public Nullable<Int32> Jueves { get; set; }

        [Display(Name = "Viernes")]
        [Column("VIERNES")]

        public Nullable<Int32> Viernes { get; set; }

        [Display(Name = "Sabado")]
        [Column("SABADO")]

        public Nullable<Int32> Sabado { get; set; }

        [Display(Name = "Domingo")]
        [Column("DOMINGO")]

        public Nullable<Int32> Domingo { get; set; }

        [Display(Name = "TipoTurno")]
        [Column("TIPOTURNO")]
        public Nullable<Int32> Tipoturno { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(10)]
        [Column("ESTADO")]

        public String Estado { get; set; }

        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }


        public AsTipohorario()
        {
        }
    }
}
