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
    [Table("PM_PROYECTO", Schema = "sgseguridadsys")]
    public class PmProyecto : PmProyectoPk
    {

        [Display(Name = "ID_RESPONSABLE")]
        [Column("ID_RESPONSABLE")]
        public Nullable<Int32> IdResponsable { get; set; }

        [Display(Name = "ID_SOLICITANTE")]
        [Column("ID_SOLICITANTE")]
        public Nullable<Int32> IdSolicitante { get; set; }

        [Display(Name = "ID_TRANSACCION")]
        [Column("ID_TRANSACCION")]
        public Nullable<Int32> IdTransaccion { get; set; }

        [Display(Name = "NOMBRE")]
        [MaxLength(200)]
        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Display(Name = "DESCRIPCION")]
        [MaxLength(500)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Display(Name = "PLAN_FECHA_INICIO")]
        [Column("PLAN_FECHA_INICIO")]
        public Nullable<DateTime> PlanFechaInicio { get; set; }

        [Display(Name = "PLAN_FECHA_FIN")]
        [Column("PLAN_FECHA_FIN")]
        public Nullable<DateTime> PlanFechaFin { get; set; }

        [Display(Name = "PLAN_COSTO")]
        [Column("PLAN_COSTO")]
        public Nullable<Decimal> PlanCosto { get; set; }

        [Display(Name = "REAL_FECHA_INICIO")]
        [Column("REAL_FECHA_INICIO")]
        public Nullable<DateTime> RealFechaInicio { get; set; }

        [Display(Name = "REAL_FECHA_FIN")]
        [Column("REAL_FECHA_FIN")]
        public Nullable<DateTime> RealFechaFin { get; set; }

        [Display(Name = "REAL_COSTO")]
        [Column("REAL_COSTO")]
        public Nullable<Decimal> RealCosto { get; set; }

        [Display(Name = "REAL_COMPLETADO")]
        [Column("REAL_COMPLETADO")]
        public Nullable<Decimal> RealCompletado { get; set; }

        [Display(Name = "ID_TIPO_PROYECTO")]
        [MaxLength(9)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_TIPO_PROYECTO")]
        public String IdTipoProyecto { get; set; }

        [Display(Name = "ID_TIPO_EXTERNO")]
        [MaxLength(100)]
        [Column("ID_TIPO_EXTERNO")]
        public String IdTipoExterno { get; set; }

        [Display(Name = "ID_EXTERNO")]
        [MaxLength(100)]
        [Column("ID_EXTERNO")]
        public String IdExterno { get; set; }

        [Display(Name = "ESTADO")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Display(Name = "CREACION_USUARIO")]
        [MaxLength(50)]
        [Column("CREACION_USUARIO")]
        public String CreacionUsuario { get; set; }

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

        [Column("AFE")]
        public String Afe { get; set; }

        [Column("AFE_COMPANYOWNER")]
        public String AfeCompanyowner { get; set; }

        [Column("ID_INSTITUCION")]
        public String IdInstitucion { get; set; }

        [NotMapped]
        public String auxAfe { get; set; }


        [NotMapped]
        public PmTipoproyecto tipoProyecto { get; set; }

        public PmProyecto()
        {
            tipoProyecto = new PmTipoproyecto();
        }
    }
}
