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
     * @tabla dbo.HR_EmpleadoCapHorario
*/
    [Table("HR_EMPLEADOCAPHORARIO")]
    public class HrEmpleadocaphorario : HrEmpleadocaphorarioPk
    {
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [MaxLength(6)]
        [Column("PERIODOINICIO")]
        public String Periodoinicio { get; set; }

        [MaxLength(6)]
        [Column("PERIODOFIN")]
        public String Periodofin { get; set; }

        [Column("FECHADESDE")]
        public Nullable<DateTime> Fechadesde { get; set; }

        [Column("FECHAHASTA")]
        public Nullable<DateTime> Fechahasta { get; set; }

        [MaxLength(1)]
        [Column("LUNES")]
        public String Lunes { get; set; }

        [Column("HORAINICIOLUNES")]
        public Nullable<DateTime> Horainiciolunes { get; set; }

        [Column("HORAFINLUNES")]
        public Nullable<DateTime> Horafinlunes { get; set; }

        [MaxLength(1)]
        [Column("MARTES")]
        public String Martes { get; set; }

        [Column("HORAINICIOMARTES")]
        public Nullable<DateTime> Horainiciomartes { get; set; }

        [Column("HORAFINMARTES")]
        public Nullable<DateTime> Horafinmartes { get; set; }

        [MaxLength(1)]
        [Column("MIERCOLES")]
        public String Miercoles { get; set; }

        [Column("HORAINICIOMIERCOLES")]
        public Nullable<DateTime> Horainiciomiercoles { get; set; }

        [Column("HORAFINMIERCOLES")]
        public Nullable<DateTime> Horafinmiercoles { get; set; }

        [MaxLength(1)]
        [Column("JUEVES")]
        public String Jueves { get; set; }

        [Column("HORAINICIOJUEVES")]
        public Nullable<DateTime> Horainiciojueves { get; set; }

        [Column("HORAFINJUEVES")]
        public Nullable<DateTime> Horafinjueves { get; set; }

        [MaxLength(1)]
        [Column("VIERNES")]
        public String Viernes { get; set; }

        [Column("HORAINICIOVIERNES")]
        public Nullable<DateTime> Horainicioviernes { get; set; }

        [Column("HORAFINVIERNES")]
        public Nullable<DateTime> Horafinviernes { get; set; }

        [MaxLength(1)]
        [Column("SABADO")]
        public String Sabado { get; set; }

        [Column("HORAINICIOSABADO")]
        public Nullable<DateTime> Horainiciosabado { get; set; }

        [Column("HORAFINSABADO")]
        public Nullable<DateTime> Horafinsabado { get; set; }

        [MaxLength(1)]
        [Column("DOMINGO")]
        public String Domingo { get; set; }

        [Column("HORAINICIODOMINGO")]
        public Nullable<DateTime> Horainiciodomingo { get; set; }

        [Column("HORAFINDOMINGO")]
        public Nullable<DateTime> Horafindomingo { get; set; }

        [Column("TOTALDIAS")]
        public Nullable<Int32> Totaldias { get; set; }

        [Column("TOTALHORAS")]
        public Nullable<Int32> Totalhoras { get; set; }

        [MaxLength(10)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public HrEmpleadocaphorario()
        {
        }
    }
}
