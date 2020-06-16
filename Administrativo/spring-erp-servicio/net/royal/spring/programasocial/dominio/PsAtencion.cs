using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio {

    /**
     * 
     * 
     * @tabla sgseguridadsys.PS_ATENCION
*/
    [Table("PS_ATENCION", Schema = "sgseguridadsys")]
    public class PsAtencion : PsAtencionPk {


        [Display(Name = "ID_AREA")]
        [MaxLength(5)]
        [Column("ID_AREA")]
        public String IdArea { get; set; }


        [Display(Name = "ID_PERIODO")]
        [MaxLength(6)]
        [Column("ID_PERIODO")]
        public String IdPeriodo { get; set; }


        [Display(Name = "ID_TIPO_ATENCION")]
        [MaxLength(3)]
        [Column("ID_TIPO_ATENCION")]
        public String IdTipoAtencion { get; set; }


        [Display(Name = "FECHA_ATENCION")]
        [Column("FECHA_ATENCION")]
        public Nullable<DateTime> FechaAtencion { get; set; }

        [Column("ESTADO")]
        public String Estado { get; set; }



        [Display(Name = "comentarios")]
        [MaxLength(1)]
        [Column("comentarios")]
        public String comentarios { get; set; }


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
        public Boolean EvaluadoBoolean { get; set; }

        [Display(Name = "EVALUADO")]
        [MaxLength(2)]
        [Column("EVALUADO")]
        public String Evaluado { get; set; }

        public PsAtencion() {
        }
    }
}
