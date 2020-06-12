using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.BP_ENLACE
    */
    [Table("BP_ENLACE", Schema = "sgseguridadsys")]
    public class BpEnlace : BpEnlacePk
    {

        public static String ESTADO_USADO = "USAD";

        [Display(Name = "FECHA_ENVIO")]
        [Column("FECHA_ENVIO")]
        public Nullable<DateTime> FechaEnvio { get; set; }

        [Display(Name = "FECHA_FIN")]
        [Column("FECHA_FIN")]
        public Nullable<DateTime> FechaFin { get; set; }

        [Display(Name = "ID_PROCESO")]
        [MaxLength(22)]
        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        [Display(Name = "ID_ESTADO")]
        [MaxLength(4)]
        [Column("ID_ESTADO")]
        public String IdEstado { get; set; }

        [Display(Name = "ID_TRANSACCION")]
        [Column("ID_TRANSACCION")]
        public Nullable<Int32> IdTransaccion { get; set; }

        [Display(Name = "ID_CLAVE_CONCATENADO")]
        [MaxLength(200)]
        [Column("ID_CLAVE_CONCATENADO")]
        public String IdClaveConcatenado { get; set; }

        [Display(Name = "ID_COMPANIASOCIO")]
        [MaxLength(8)]
        [Column("ID_COMPANIASOCIO")]
        public String IdCompaniaSocio { get; set; }

        [Display(Name = "ID_EMPLEADO")]
        [Column("ID_EMPLEADO")]
        public Nullable<Int32> IdEmpleado { get; set; }

        [Display(Name = "URL")]
        [MaxLength(2000)]
        [Column("URL")]
        public String Url { get; set; }

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

        public BpEnlace()
        {
        }
    }
}
