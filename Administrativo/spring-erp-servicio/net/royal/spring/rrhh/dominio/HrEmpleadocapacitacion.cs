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
     * @tabla dbo.HR_EmpleadoCapacitacion
*/
    [Table("HR_EMPLEADOCAPACITACION")]
    public class HrEmpleadocapacitacion : HrEmpleadocapacitacionPk
    {


        [MaxLength(10)]
        [Column("SOLICITUDCAPACITACION")]
        public String Solicitudcapacitacion { get; set; }


        [MaxLength(10)]
        [Column("CENTRORESPONSABILIDAD")]
        public String Centroresponsabilidad { get; set; }


        [MaxLength(20)]
        [Column("NUMEROCONSTANCIA")]
        public String Numeroconstancia { get; set; }


        [Column("NUMEROASISTENCIAS")]
        public Nullable<Int32> Numeroasistencias { get; set; }


        [Column("HORASASISTENCIA")]
        public Nullable<Int32> Horasasistencia { get; set; }


        [MaxLength(20)]
        [Column("CALIFICACION")]
        public String Calificacion { get; set; }


        [Column("COSTOINDIVIDUAL")]
        public Nullable<Decimal> Costoindividual { get; set; }


        [Column("NUMEROPERIODOS")]
        public Nullable<Int32> Numeroperiodos { get; set; }


        [MaxLength(4)]
        [Column("UNIDADNEGOCIO")]
        public String Unidadnegocio { get; set; }


        [MaxLength(1)]
        [Column("INTERFACEINSTRUCCION")]
        public String Interfaceinstruccion { get; set; }


        [Column("NOTA")]
        public Nullable<Decimal> Nota { get; set; }


        [Column("DIASASISTIDOS")]
        public Nullable<Int32> Diasasistidos { get; set; }


        [Column("IMPORTEGASTOEMP")]
        public Nullable<Decimal> Importegastoemp { get; set; }


        [MaxLength(150)]
        [Column("COMENTARIO")]
        public String Comentario { get; set; }


        [MaxLength(3)]
        [Column("DEPTOORGANIZACION")]
        public String Deptoorganizacion { get; set; }

        [MaxLength(255)]
        [Column("HORARIO")]
        public String Horario { get; set; }


        [Column("HORAINICIO")]
        public Nullable<DateTime> Horainicio { get; set; }


        [Column("HORAFIN")]
        public Nullable<DateTime> Horafin { get; set; }


        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }


        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }


        [MaxLength(800)]
        [Column("MOTIVODESAPROBACION")]
        public String Motivodesaprobacion { get; set; }

        [Column("FECHA")]
        public Nullable<DateTime> Fecha { get; set; }


        [Column("FECHAMINPERMANENCIA")]
        public Nullable<DateTime> Fechaminpermanencia { get; set; }

        [Column("COMPANIA_EMPLEADO")]
        public string CompaniaEmpleado { get; set; }


        /*AGREGADO POR ALEJANDRO*/
        [Column("FLAG_APROBO")]
        public String Flagaprobo { get; set; }

        [Column("FLAG_PARTICIPATIVO")]
        public String Flagparticipativo { get; set; }

        [Column("FLAG_ASISTIO")]
        public String Flagasistio { get; set; }

        [Column("MOTIVO_INASISTENCIA")]
        public String Motivoinasistencia { get; set; }
        /*AGREGADO POR ALEJANDRO*/



        [NotMapped]
        public string AuxPersonaNombreCompleto { get; set; }

        public HrEmpleadocapacitacion()
        {
        }
    }
}
