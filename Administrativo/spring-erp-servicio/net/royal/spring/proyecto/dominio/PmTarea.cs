
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
     * @tabla sgseguridadsys.PM_TAREA
*/
    [Table("PM_TAREA", Schema = "sgseguridadsys")]
    public class PmTarea : PmTareaPk
    {

        [Display(Name = "ID_TIPO_TAREA")]
        [MaxLength(3)]
        [Column("ID_TIPO_TAREA")]
        public String IdTipoTarea { get; set; }

        [Display(Name = "ID_EDT")]
        [MaxLength(20)]
        [Column("ID_EDT")]
        public String IdEdt { get; set; }

        [Display(Name = "NOMBRE")]
        [MaxLength(200)]
        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Display(Name = "DESCRIPCION")]
        [MaxLength(500)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Display(Name = "FECHA_REGISTRO")]
        [Column("FECHA_REGISTRO")]
        public Nullable<DateTime> FechaRegistro { get; set; }

        [Display(Name = "FECHA_ULTIMA_ACTUALIZACION")]
        [Column("FECHA_ULTIMA_ACTUALIZACION")]
        public Nullable<DateTime> FechaUltimaActualizacion { get; set; }

        [Display(Name = "PROGRAMACION_FECHA_INICIO")]
        [Column("PROGRAMACION_FECHA_INICIO")]
        public Nullable<DateTime> ProgramacionFechaInicio { get; set; }

        [Display(Name = "PROGRAMACION_FECHA_FIN")]
        [Column("PROGRAMACION_FECHA_FIN")]
        public Nullable<DateTime> ProgramacionFechaFin { get; set; }

        [Display(Name = "REAL_FECHA_INICIO")]
        [Column("REAL_FECHA_INICIO")]
        public Nullable<DateTime> RealFechaInicio { get; set; }

        [Display(Name = "REAL_FECHA_FIN")]
        [Column("REAL_FECHA_FIN")]
        public Nullable<DateTime> RealFechaFin { get; set; }

        [Display(Name = "REAL_DURACION")]
        [Column("REAL_DURACION")]
        public Nullable<Decimal> RealDuracion { get; set; }

        [Display(Name = "REAL_TRABAJO")]
        [Column("REAL_TRABAJO")]
        public Nullable<Decimal> RealTrabajo { get; set; }

        [Display(Name = "REAL_TRABAJO_HORAS_EXTRA")]
        [Column("REAL_TRABAJO_HORAS_EXTRA")]
        public Nullable<Decimal> RealTrabajoHorasExtra { get; set; }

        [Display(Name = "REAL_COSTO")]
        [Column("REAL_COSTO")]
        public Nullable<Decimal> RealCosto { get; set; }

        [Display(Name = "REAL_COSTO_HORAS_EXTRA")]
        [Column("REAL_COSTO_HORAS_EXTRA")]
        public Nullable<Decimal> RealCostoHorasExtra { get; set; }

        [Display(Name = "REAL_CRTR")]
        [Column("REAL_CRTR")]
        public Nullable<Decimal> RealCrtr { get; set; }

        [Display(Name = "REAL_COMPLETADO")]
        [Column("REAL_COMPLETADO")]
        public Nullable<Decimal> RealCompletado { get; set; }

        [Display(Name = "ID_TIPO_RESPONSABLE")]
        [MaxLength(3)]
        [Column("ID_TIPO_RESPONSABLE")]
        public String IdTipoResponsable { get; set; }

        [Display(Name = "ID_SOLICITANTE")]
        [Column("ID_SOLICITANTE")]
        public Nullable<Int32> IdSolicitante { get; set; }

        [Display(Name = "ID_SUPERVISOR")]
        [Column("ID_SUPERVISOR")]
        public Nullable<Int32> IdSupervisor { get; set; }

        [Display(Name = "ID_RESPONSABLE")]
        [Column("ID_RESPONSABLE")]
        public Nullable<Int32> IdResponsable { get; set; }

        [Display(Name = "FLG_HITO")]
        [MaxLength(1)]
        [Column("FLG_HITO")]
        public String FlgHito { get; set; }

        [Display(Name = "FLG_SOBREASIGNADO")]
        [MaxLength(1)]
        [Column("FLG_SOBREASIGNADO")]
        public String FlgSobreasignado { get; set; }

        [Display(Name = "FLG_REPETITIVA")]
        [MaxLength(1)]
        [Column("FLG_REPETITIVA")]
        public String FlgRepetitiva { get; set; }

        [Display(Name = "ESQUEMA_NIVEL")]
        [Column("ESQUEMA_NIVEL")]
        public Nullable<Int32> EsquemaNivel { get; set; }

        [Display(Name = "ESQUEMA_NUMERO")]
        [Column("ESQUEMA_NUMERO")]
        public Nullable<Int32> EsquemaNumero { get; set; }

        [Display(Name = "FLG_TAREA_EXTERNA")]
        [MaxLength(1)]
        [Column("FLG_TAREA_EXTERNA")]
        public String FlgTareaExterna { get; set; }

        [Display(Name = "ID_TIPO_TAREA_EXTERNO")]
        [MaxLength(100)]
        [Column("ID_TIPO_TAREA_EXTERNO")]
        public String IdTipoTareaExterno { get; set; }

        [Display(Name = "ID_TAREA_EXTERNO")]
        [MaxLength(100)]
        [Column("ID_TAREA_EXTERNO")]
        public String IdTareaExterno { get; set; }

        [Display(Name = "ID_TIPO1")]
        [MaxLength(20)]
        [Column("ID_TIPO1")]
        public String IdTipo1 { get; set; }

        [Display(Name = "ID_TIPO2")]
        [MaxLength(20)]
        [Column("ID_TIPO2")]
        public String IdTipo2 { get; set; }

        [Display(Name = "ID_TIPO3")]
        [MaxLength(20)]
        [Column("ID_TIPO3")]
        public String IdTipo3 { get; set; }

        [Display(Name = "ID_TIPO4")]
        [MaxLength(20)]
        [Column("ID_TIPO4")]
        public String IdTipo4 { get; set; }

        [Display(Name = "ID_TIPO5")]
        [MaxLength(20)]
        [Column("ID_TIPO5")]
        public String IdTipo5 { get; set; }

        [Display(Name = "ID_TIPO6")]
        [MaxLength(20)]
        [Column("ID_TIPO6")]
        public String IdTipo6 { get; set; }

        [Display(Name = "ID_TIPO7")]
        [MaxLength(20)]
        [Column("ID_TIPO7")]
        public String IdTipo7 { get; set; }

        [Display(Name = "ID_TIPO8")]
        [MaxLength(20)]
        [Column("ID_TIPO8")]
        public String IdTipo8 { get; set; }

        [Display(Name = "ID_TAREA_PADRE")]
        [Column("ID_TAREA_PADRE")]
        public Nullable<Int32> IdTareaPadre { get; set; }

        [Display(Name = "PROVEEDOR_ID")]
        [Column("PROVEEDOR_ID")]
        public Nullable<Int32> ProveedorId { get; set; }

        [Display(Name = "PROVEEDOR_DIRECCION")]
        [MaxLength(200)]
        [Column("PROVEEDOR_DIRECCION")]
        public String ProveedorDireccion { get; set; }

        [Display(Name = "ID_SUCURSAL")]
        [MaxLength(10)]
        [Column("ID_SUCURSAL")]
        public String IdSucursal { get; set; }

        [Display(Name = "ID_APLICACION")]
        [MaxLength(4)]
        [Column("ID_APLICACION")]
        public String IdAplicacion { get; set; }

        [Display(Name = "ID_GRUPO")]
        [MaxLength(6)]
        [Column("ID_GRUPO")]
        public String IdGrupo { get; set; }

        [Display(Name = "ID_CONCEPTO")]
        [MaxLength(6)]
        [Column("ID_CONCEPTO")]
        public String IdConcepto { get; set; }

        [Display(Name = "ID_PROGRAMACION")]
        [Column("ID_PROGRAMACION")]
        public Nullable<Int32> IdProgramacion { get; set; }

        [Display(Name = "ID_TRANSACCION")]
        [Column("ID_TRANSACCION")]
        public Nullable<Int32> IdTransaccion { get; set; }

        [Display(Name = "ID_TRANSACCION2")]
        [Column("ID_TRANSACCION2")]
        public Nullable<Int32> IdTransaccion2 { get; set; }

        [Display(Name = "UBICACION_ID_CLIENTE")]
        [Column("UBICACION_ID_CLIENTE")]
        public Nullable<Int32> UbicacionIdCliente { get; set; }

        [Display(Name = "UBICACION_ID")]
        [MaxLength(20)]
        [Column("UBICACION_ID")]
        public String UbicacionId { get; set; }

        [Display(Name = "EXTERNO_ID_GRUPO")]
        [MaxLength(100)]
        [Column("EXTERNO_ID_GRUPO")]
        public String ExternoIdGrupo { get; set; }

        [Display(Name = "ESTADO")]
        [MaxLength(50)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Display(Name = "ESTADO2")]
        [MaxLength(50)]
        [Column("ESTADO2")]
        public String Estado2 { get; set; }

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

        [Column("FLAG_COMISION_SERVICIO")]
        public String flagComisionServicio { get; set; }

        [Column("PERMISO")]
        public Nullable<Int32> Permiso { get; set; }

        [NotMapped]
        public PmTipoproyecto tipoProyecto { get; set; }

        public PmTarea()
        {
            tipoProyecto = new PmTipoproyecto();
        }
    }
}
