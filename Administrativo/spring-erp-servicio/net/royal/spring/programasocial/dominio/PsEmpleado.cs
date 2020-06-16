using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.PS_EMPLEADO
*/
    [Table("PS_EMPLEADO", Schema = "sgseguridadsys")]
    public class PsEmpleado : PsEmpleadoPk
    {

        [Display(Name = "CODIGOUSUARIO")]
        [MaxLength(4)]
        [Column("CODIGOUSUARIO")]
        public String CodigoUsuario { get; set; }

        [Display(Name = "ESTADO")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Display(Name = "FechaIngreso")]
        [Column("FECHA_INGRESO")]
        public Nullable<DateTime> FechaIngreso { get; set; }

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

        [NotMapped]
        public PsEntidad psEntidad { get; set; }

        public PsEmpleado()
        {
            psEntidad = new PsEntidad();
        }


    }
}
