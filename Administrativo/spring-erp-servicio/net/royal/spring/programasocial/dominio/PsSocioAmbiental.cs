using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio {

    /**
     * 
     * 
     * @tabla sgseguridadsys.PS_SOCIO_AMBIENTAL
*/
    [Table("PS_SOCIO_AMBIENTAL", Schema = "sgseguridadsys")]
    public class PsSocioAmbiental : PsSocioAmbientalPk {


        [Display(Name = "ID_PERIODO")]
        [MaxLength(6)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PERIODO")]
        public String IdPeriodo { get; set; }


        [Display(Name = "ID_ORIGEN")]
        [MaxLength(3)]
        [Column("ID_ORIGEN")]
        public String IdOrigen { get; set; }

        [Display(Name = "FECHA_INFORME")]
        [Column("FECHA_INFORME")]
        public Nullable<DateTime> FechaInforme { get; set; }


        [Display(Name = "ID_CONFLICTOS")]
        [MaxLength(5)]
        [Column("ID_CONFLICTOS")]
        public String IdConflictos { get; set; }

        [Display(Name = "ID_COMUNICACION")]
        [MaxLength(5)]
        [Column("ID_COMUNICACION")]
        public String IdComunicacion { get; set; }


        [Display(Name = "ID_EMOCIONAL")]
        [MaxLength(5)]
        [Column("ID_EMOCIONAL")]
        public String IdEmocional { get; set; }


        [Display(Name = "ID_COOPERACION")]
        [MaxLength(5)]
        [Column("ID_COOPERACION")]
        public String IdCooperacion { get; set; }


        [Display(Name = "ID_ASERTAVIDAD")]
        [MaxLength(5)]
        [Column("ID_ASERTAVIDAD")]
        public String IdAsertavidad { get; set; }


        [Display(Name = "ID_EMPATIA")]
        [MaxLength(5)]
        [Column("ID_EMPATIA")]
        public String IdEmpatia { get; set; }

        [Display(Name = "ID_INTEGRACION")]
        [MaxLength(5)]
        [Column("ID_INTEGRACION")]
        public String IdIntegracion { get; set; }


        [Display(Name = "ID_PARTICIPACION")]
        [MaxLength(5)]
        [Column("ID_PARTICIPACION")]
        public String IdParticipacion { get; set; }


        [Display(Name = "COMENTARIOS")]
        [MaxLength(1000)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("COMENTARIOS")]
        public String Comentarios { get; set; }


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

        [NotMapped]
        public String NombreCompleto { get; set; }
        [NotMapped]
        public Boolean ValidarNinos { get; set; }

        [NotMapped]
        public String InstitucionNombre { get; set; }
        [NotMapped]
        public Boolean EvaluadoBoolean { get; set; }

        [Display(Name = "EVALUADO")]
        [MaxLength(2)]
        [Column("EVALUADO")]
        public String Evaluado { get; set; }

        [NotMapped]
        public String NombreConflictos { get; set; }
        [NotMapped]
        public String NombreComunicacion { get; set; }
        [NotMapped]
        public String NombreEmocional { get; set; }
        [NotMapped]
        public String NombreCooperacion { get; set; }
        [NotMapped]
        public String NombreAsertividad { get; set; }
        [NotMapped]
        public String NombreEmpatia { get; set; }
        [NotMapped]
        public String NombreIntegracion { get; set; }
        [NotMapped]
        public String NombreParticipacion { get; set; }

        [NotMapped]
        public Nullable<Int32> Edad { get; set; }

        [NotMapped]
        public String sexo { get; set; }

        [NotMapped]
        public Int32 secuencia { get; set; }

        public PsSocioAmbiental() {
        }
    }
}
