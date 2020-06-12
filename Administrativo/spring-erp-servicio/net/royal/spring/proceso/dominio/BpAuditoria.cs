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
     * @tabla sgseguridadsys.BP_AUDITORIA
*/
    [Table("BP_AUDITORIA", Schema = "sgseguridadsys")]
    public class BpAuditoria : BpAuditoriaPk
    {

        [Display(Name = "ID_PERIODO")]
        [MaxLength(8)]
        [Column("ID_PERIODO")]
        public String IdPeriodo { get; set; }

        [Display(Name = "ID_COMPANIASOCIO")]
        [MaxLength(8)]
        [Column("ID_COMPANIASOCIO")]
        public String IdCompaniasocio { get; set; }

        [Display(Name = "ID_SUCURSAL")]
        [MaxLength(4)]
        [Column("ID_SUCURSAL")]
        public String IdSucursal { get; set; }

        [Display(Name = "ID_AREA")]
        [MaxLength(10)]
        [Column("ID_AREA")]
        public String IdArea { get; set; }

        [Display(Name = "ID_CENTROCOSTO")]
        [MaxLength(10)]
        [Column("ID_CENTROCOSTO")]
        public String IdCentrocosto { get; set; }

        [Display(Name = "ID_TIPO_PLANILLA")]
        [MaxLength(10)]
        [Column("ID_TIPO_PLANILLA")]
        public String IdTipoPlanilla { get; set; }

        [Display(Name = "ID_TIPO_PROCESO")]
        [MaxLength(10)]
        [Column("ID_TIPO_PROCESO")]
        public String IdTipoProceso { get; set; }

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

        public BpAuditoria()
        {
        }
    }
}
