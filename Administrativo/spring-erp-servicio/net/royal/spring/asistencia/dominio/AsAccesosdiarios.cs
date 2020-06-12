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
     * @tabla dbo.AS_AccesosDiarios
*/
    [Table("AS_ACCESOSDIARIOS")]
    public class AsAccesosdiarios : AsAccesosdiariosPk
    {

        [Display(Name = "Empleado")]
        [Column("EMPLEADO")]

        public Nullable<Decimal> Empleado { get; set; }

        [Display(Name = "TipoValidacion")]
        [MaxLength(1)]
        [Column("TIPOVALIDACION")]

        public String Tipovalidacion { get; set; }

        [Display(Name = "CarnetSupervisor")]
        [MaxLength(15)]
        [Column("CARNETSUPERVISOR")]

        public String Carnetsupervisor { get; set; }

        [Display(Name = "EmpleadoSupervisor")]
        [Column("EMPLEADOSUPERVISOR")]

        public Nullable<Decimal> Empleadosupervisor { get; set; }

        [Display(Name = "Hora")]
        [Column("HORA")]

        public Nullable<DateTime> Hora { get; set; }

        [Display(Name = "Observacion")]
        [MaxLength(40)]
        [Column("OBSERVACION")]

        public String Observacion { get; set; }

        [Display(Name = "LugarRecoleccion")]
        [MaxLength(10)]
        [Column("LUGARRECOLECCION")]

        public String Lugarrecoleccion { get; set; }

        [Display(Name = "UnidadReplicacion")]
        [MaxLength(4)]
        [Column("UNIDADREPLICACION")]

        public String Unidadreplicacion { get; set; }

        [Display(Name = "Ano")]
        [Column("ANO")]

        public Nullable<Decimal> Ano { get; set; }

        [Display(Name = "Mes")]
        [Column("MES")]

        public Nullable<Decimal> Mes { get; set; }

        [Display(Name = "Dia")]
        [Column("DIA")]

        public Nullable<Decimal> Dia { get; set; }

        [NotMapped]
        public String accion { get; set; }


        [Display(Name = "UltimaFechaModif")]
        [Column("UltimaFechaModif")]
        public DateTime UltimaFechaModif { get; set; }

        [Display(Name = "UltimoUsuario")]
        [Column("UltimoUsuario")]
        public String UltimoUsuario { get; set; }

        [NotMapped]
        public String fechaDescripcion { get; set; }

        [NotMapped]
        public String horaDescripcion { get; set; }
        [NotMapped]
        public String compania { get; set; }

        [NotMapped]
        public Nullable<DateTime> inicio { get; set; }
        [NotMapped]
        public Nullable<DateTime> fin { get; set; }

        public AsAccesosdiarios()
        {
        }
    }
}
