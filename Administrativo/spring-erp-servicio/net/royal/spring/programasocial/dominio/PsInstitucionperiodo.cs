using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio {

    /**
     * 
     * 
     * @tabla sgseguridadsys.PsInstitucionAreaArea
*/
    [Table("PS_INSTITUCION_PERIODO", Schema = "sgseguridadsys")]

    public class PsInstitucionperiodo : PsInstitucionperiodoPk {

        [Display(Name = "FECHA_INICIO")]
        [Column("FECHA_INICIO")]
        public Nullable<DateTime> Fechainicio { get; set; }

        [Display(Name = "FECHA_FIN")]
        [Column("FECHA_FIN")]
        public Nullable<DateTime> Fechafin { get; set; }

        [Display(Name = "FECHA_INICIO_REGISTRO")]
        [Column("FECHA_INICIO_REGISTRO")]
        public Nullable<DateTime> Fechainicioregistro { get; set; }

        [Display(Name = "FECHA_FIN_REGISTRO")]
        [Column("FECHA_FIN_REGISTRO")]
        public Nullable<DateTime> Fechafinregistro { get; set; }


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

        public PsInstitucionperiodo() {
        }
    }
}
