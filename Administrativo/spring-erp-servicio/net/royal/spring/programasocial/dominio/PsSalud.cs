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
     * @tabla sgseguridadsys.PS_SALUD
*/
    [Table("PS_SALUD", Schema = "sgseguridadsys")]
    public class PsSalud : PsSaludPk
    {


        [Display(Name = "ID_PERIODO")]
        [MaxLength(6)]
        [Column("ID_PERIODO")]
        public String IdPeriodo { get; set; }


        [Display(Name = "FECHA_INFORME")]
        [Column("FECHA_INFORME")]
        public Nullable<DateTime> FechaInforme { get; set; }

        [Display(Name = "EDAD_MENARQUIA")]
        [Column("EDAD_MENARQUIA")]
        public Nullable<Int32> EdadMenarquia { get; set; }


        [Display(Name = "ID_PRUEBA_VIH")]
        [MaxLength(6)]
        [Column("ID_PRUEBA_VIH")]
        public String IdPruebaVIH { get; set; }


        [Display(Name = "FECHA_ULTIMA_MESTRUACION")]
        [Column("FECHA_ULTIMA_MESTRUACION")]
        public Nullable<DateTime> FechaUltimaMestruacion { get; set; }

        [Display(Name = "HEMOGLOBINA")]
        [Column("HEMOGLOBINA")]
        public Nullable<Decimal> Hemoglobina { get; set; }


        [Display(Name = "HEMOGLOBINA_RESULTADO")]
        [MaxLength(100)]
        [Column("HEMOGLOBINA_RESULTADO")]
        public String HemoglobinaResultado { get; set; }


        [Display(Name = "OTRAS_ENFERMEDADES")]
        [MaxLength(1000)]
        [Column("OTRAS_ENFERMEDADES")]
        public String OtrasEnfermedades { get; set; }


        [Display(Name = "ID_TRATAMIENTO_ANEMIA")]
        [MaxLength(10)]
        [Column("ID_TRATAMIENTO_ANEMIA")]
        public String IdTratamientoAnemia { get; set; }

        [Display(Name = "ID_GRUPOSANGUINEO")]
        [MaxLength(10)]
        [Column("ID_GRUPOSANGUINEO")]
        public String IdGrupoSanguineo { get; set; }

        [Display(Name = "ID_FACTORERH")]
        [MaxLength(10)]
        [Column("ID_FACTORERH")]
        public String IdFactorRh { get; set; }


        [Display(Name = "ID_DESCARTE_TBC")]
        [MaxLength(10)]
        [Column("ID_DESCARTE_TBC")]
        public String IdDescarteTbc { get; set; }


        [Display(Name = "ID_DESCARTE_SEROLOGICO")]
        [MaxLength(10)]
        [Column("ID_DESCARTE_SEROLOGICO")]
        public String IdDescarteSerologico { get; set; }


        [Display(Name = "ID_AYUDA_MEDICA")]
        [MaxLength(10)]
        [Column("ID_AYUDA_MEDICA")]
        public String IdAyudaMedica { get; set; }


        [Display(Name = "COMENTARIOS")]
        [MaxLength(1000)]
        [Column("COMENTARIOS")]
        public String Comentarios { get; set; }

        [Display(Name = "ID_TBC")]
        [MaxLength(10)]
        [Column("ID_TBC")]
        public String IdTbc { get; set; }

        [Display(Name = "ID_HTA")]
        [MaxLength(10)]
        [Column("ID_HTA")]
        public String IdHta { get; set; }
        [Display(Name = "ID_DIABETES")]
        [MaxLength(10)]
        [Column("ID_DIABETES")]
        public String IdDiabetes { get; set; }
        [Display(Name = "ID_OSTEOARTROSIS")]
        [MaxLength(10)]
        [Column("ID_OSTEOARTROSIS")]
        public String IdOsteoatrosis { get; set; }
        [Display(Name = "ID_COGNITIVO")]
        [MaxLength(10)]
        [Column("ID_COGNITIVO")]
        public String IdCognitivo { get; set; }
        [Display(Name = "ID_AFECTIVO")]
        [MaxLength(10)]
        [Column("ID_AFECTIVO")]
        public String IdAfectivo { get; set; }


        [Display(Name = "ESTADO")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Display(Name = "ID_ORIGEN")]
        [MaxLength(1)]
        [Column("ID_ORIGEN")]
        public String IdOrigen { get; set; }



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
        public List<PsSaludBiomecanica> listaBiomecanica { get; set; }

        [NotMapped]
        public List<PsSaludTratamiento> listaTratamiento { get; set; }

        [NotMapped]
        public List<PsSaludEstado> listaEstado { get; set; }

        [NotMapped]
        public List<PsSaludExamen> listaExamen { get; set; }

        [NotMapped]
        public List<PsSaludSubcondicion> listaSubcondicion { get; set; }

        [NotMapped]
        public List<PsSaludDiagnostico> listaDiagnostico { get; set; }

        [NotMapped]
        public List<PsSaludTerapia> listaTerapia { get; set; }



        [NotMapped]
        public List<PsSaludAyuda> listaAyuda { get; set; }

        [NotMapped]
        public List<PsSaludDiscapacidad> listaDiscapacidad { get; set; }


        public PsSalud()
        {
            listaAyuda = new List<PsSaludAyuda>();
            listaEstado = new List<PsSaludEstado>();
            listaTratamiento = new List<PsSaludTratamiento>();
            listaDiscapacidad = new List<PsSaludDiscapacidad>();
            listaTerapia = new List<PsSaludTerapia>();
            listaDiagnostico = new List<PsSaludDiagnostico>();
            listaBiomecanica = new List<PsSaludBiomecanica>();
            listaExamen = new List<PsSaludExamen>();
            listaSubcondicion = new List<PsSaludSubcondicion>();
        }
    }
}
