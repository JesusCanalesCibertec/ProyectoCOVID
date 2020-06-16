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
    [Table("PM_PROGRAMA", Schema = "sgseguridadsys")]
    public class PmPrograma : PmProgramaPk
    {
        [Display(Name = "NOMBRE")]
        [MaxLength(200)]
        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Display(Name = "DESCRIPCION")]
        [MaxLength(500)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

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

        [Display(Name = "ANIO")]
        [MaxLength(1)]
        [Column("ANIO")]
        public String Anio { get; set; }

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

        public PmPrograma()
        {
        }
    }
}
