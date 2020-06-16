using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.PS_CAPACIDAD
*/
    [Table("PS_CAPACIDAD", Schema = "sgseguridadsys")]
    public class PsCapacidad : PsCapacidadPk
    {


        [Display(Name = "FECHA_INFORME")]
        [Column("FECHA_INFORME")]
        public Nullable<DateTime> FechaInforme { get; set; }


        [Display(Name = "ID_TIPO_INSTITUCION")]
        [MaxLength(6)]
        [Column("ID_TIPO_INSTITUCION")]
        public String IdTipoInstitucion { get; set; }

        [Display(Name = "OCUPACION_ANTERIOR")]
        [MaxLength(100)]
        [Column("OCUPACION_ANTERIOR")]
        public String OcupacionAnterior { get; set; }

        [Display(Name = "ID_PERIODO")]
        [MaxLength(6)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PERIODO")]
        public String IdPeriodo { get; set; }

        [Display(Name = "ID_ORIGEN")]
        [MaxLength(3)]
        [Column("ID_ORIGEN")]
        public String IdOrigen { get; set; }

        [Display(Name = "ID_SAANEE")]
        [MaxLength(3)]
        [Column("ID_SAANEE")]
        public String IdSaanee { get; set; }

        [Display(Name = "ID_ILETRADO")]
        [MaxLength(3)]
        [Column("ID_ILETRADO")]
        public String IdIletrado { get; set; }

        [Display(Name = "ID_NIVEL")]
        [MaxLength(6)]
        [Column("ID_NIVEL")]
        public String IdNivel { get; set; }


        [Display(Name = "ID_GRADO_EDUCATIVO")]
        [MaxLength(6)]
        [Column("ID_GRADO_EDUCATIVO")]
        public String IdGradoEducativo { get; set; }


        [Display(Name = "ANIO_RETRASO")]
        [MaxLength(6)]
        [Column("ANIO_RETRASO")]
        public String AnioRetraso { get; set; }


        [Display(Name = "COMENTARIO_RETRASO")]
        [MaxLength(500)]
        [Column("COMENTARIO_RETRASO")]
        public String ComentarioRetraso { get; set; }


        [Display(Name = "ID_TIPO_COMUNICACION")]
        [MaxLength(6)]
        [Column("ID_TIPO_COMUNICACION")]
        public String IdTipoComunicacion { get; set; }


        [Display(Name = "COMENTARIO_RENDIMIENTO")]
        [MaxLength(500)]
        [Column("COMENTARIO_RENDIMIENTO")]
        public String ComentarioRendimiento { get; set; }


        [Display(Name = "COMENTARIO_TALLERES")]
        [MaxLength(500)]
        [Column("COMENTARIO_TALLERES")]
        public String ComentarioTalleres { get; set; }


        [Display(Name = "ID_RIESGO_CAIDA")]
        [MaxLength(6)]
        [Column("ID_RIESGO_CAIDA")]
        public String IdRiesgoCaida { get; set; }

        [Display(Name = "ID_RIESGO_CAIDA_DETALLE")]
        [MaxLength(2)]
        [Column("ID_RIESGO_CAIDA_DETALLE")]
        public String IdRiesgoCaidaDetalle { get; set; }

        [Display(Name = "ID_HABILIDAD_OCUPACIONAL")]
        [MaxLength(6)]
        [Column("ID_HABILIDAD_OCUPACIONAL")]
        public String IdHabilidadOcupacional { get; set; }


        [Display(Name = "ID_EVALUAR_OCUPACIONAL")]
        [MaxLength(6)]
        [Column("ID_EVALUAR_OCUPACIONAL")]
        public String IdEvaluarOcupacional { get; set; }


        [Display(Name = "COMENTARIO_CAPACIDAD")]
        [MaxLength(500)]
        [Column("COMENTARIO_CAPACIDAD")]
        public String ComentarioCapacidad { get; set; }


        [Display(Name = "KATZ1")]
        [Column("KATZ1")]
        public Nullable<Int32> Katz1 { get; set; }


        [Display(Name = "KATZ2")]
        [Column("KATZ2")]
        public Nullable<Int32> Katz2 { get; set; }


        [Display(Name = "KATZ3")]
        [Column("KATZ3")]
        public Nullable<Int32> Katz3 { get; set; }


        [Display(Name = "KATZ4")]
        [Column("KATZ4")]
        public Nullable<Int32> Katz4 { get; set; }


        [Display(Name = "KATZ5")]
        [Column("KATZ5")]
        public Nullable<Int32> Katz5 { get; set; }


        [Display(Name = "KATZ6")]
        [Column("KATZ6")]
        public Nullable<Int32> Katz6 { get; set; }


        [Display(Name = "Barthel1")]
        [Column("BARTHEL1")]
        public Nullable<Int32> Barthel1 { get; set; }


        [Display(Name = "Barthel2")]
        [Column("BARTHEL2")]
        public Nullable<Int32> Barthel2 { get; set; }


        [Display(Name = "Barthel3")]
        [Column("BARTHEL3")]
        public Nullable<Int32> Barthel3 { get; set; }


        [Display(Name = "Barthel4")]
        [Column("BARTHEL4")]
        public Nullable<Int32> Barthel4 { get; set; }


        [Display(Name = "Barthel5")]
        [Column("BARTHEL5")]
        public Nullable<Int32> Barthel5 { get; set; }


        [Display(Name = "Barthel6")]
        [Column("BARTHEL6")]
        public Nullable<Int32> Barthel6 { get; set; }


        [Display(Name = "Barthel7")]
        [Column("BARTHEL7")]
        public Nullable<Int32> Barthel7 { get; set; }


        [Display(Name = "Barthel8")]
        [Column("BARTHEL8")]
        public Nullable<Int32> Barthel8 { get; set; }


        [Display(Name = "Barthel9")]
        [Column("BARTHEL9")]
        public Nullable<Int32> Barthel9 { get; set; }


        [Display(Name = "Barthel10")]
        [Column("BARTHEL10")]
        public Nullable<Int32> Barthel10 { get; set; }

        [Display(Name = "GRADO_DEPENDENCIA_BARTHEL")]
        [MaxLength(500)]
        [Column("GRADO_DEPENDENCIA_BARTHEL")]
        public String GradoDependenciaBarthel { get; set; }

        [Display(Name = "GRADO_DEPENDENCIA_KATZ")]
        [MaxLength(500)]
        [Column("GRADO_DEPENDENCIA_KATZ")]
        public String GradoDependenciaKatz { get; set; }

        [Display(Name = "PORCENTAJE_GRADO_BARTHEL")]
        [Column("PORCENTAJE_GRADO_BARTHEL")]
        public Nullable<Decimal> PorcentajeGradoBarthel { get; set; }

        [Display(Name = "PORCENTAJE_GRADO_KATZ")]
        [Column("PORCENTAJE_GRADO_KATZ")]
        public Nullable<Decimal> PorcentajeGradoKatz { get; set; }

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
        public Boolean EvaluadoBoolean { get; set; }

        [Display(Name = "EVALUADO")]
        [MaxLength(2)]
        [Column("EVALUADO")]
        public String Evaluado { get; set; }

        [NotMapped]
        public List<PsCapacidadCurso> listaCursos { get; set; }

        [NotMapped]
        public List<PsCapacidadTaller> listaTaller { get; set; }

        public PsCapacidad()
        {
            listaTaller = new List<PsCapacidadTaller>();
            listaCursos = new List<PsCapacidadCurso>();
        }
    }
}
