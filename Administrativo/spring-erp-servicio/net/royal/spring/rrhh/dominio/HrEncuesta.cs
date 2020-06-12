using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.rrhh.dominio
{

    /**
     * 
     * 
     * @tabla dbo.HR_Encuesta
*/
    [Table("HR_ENCUESTA")]
    public class HrEncuesta : HrEncuestaPk
    {

        [Display(Name = "Responsable")]
        [Column("RESPONSABLE")]
        public Nullable<Int32> Responsable { get; set; }

        [Display(Name = "Observaciones")]
        [MaxLength(200)]
        [Column("OBSERVACIONES")]
        public String Observaciones { get; set; }

        [Display(Name = "UnidadNegocio")]
        [MaxLength(4)]
        [Column("UNIDADNEGOCIO")]
        public String Unidadnegocio { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(3)]
        [Column("DEPARTAMENTO")]
        public String Departamento { get; set; }

        [Display(Name = "Muestra")]
        [Column("MUESTRA")]
        public Nullable<Int32> Muestra { get; set; }

        [Display(Name = "Fecha")]
        [Column("FECHA")]
        public Nullable<DateTime> Fecha { get; set; }

        [Display(Name = "CentroCostos")]
        [MaxLength(10)]
        [Column("CENTROCOSTOS")]
        public String Centrocostos { get; set; }

        [Column("TIPO")]
        public String Tipo { get; set; }

        [Display(Name = "Titulo")]
        [MaxLength(255)]
        [Column("TITULO")]
        public String Titulo { get; set; }

        [Display(Name = "FlagDocente")]
        [MaxLength(1)]
        [Column("FLAGDOCENTE")]
        public String Flagdocente { get; set; }

        [Display(Name = "Foco")]
        [Column("FOCO")]
        public Nullable<Int32> Foco { get; set; }

        [Display(Name = "FechaFin")]
        [Column("FECHAFIN")]
        public Nullable<DateTime> Fechafin { get; set; }

        [Display(Name = "FechaInicio")]
        [Column("FECHAINICIO")]
        public Nullable<DateTime> Fechainicio { get; set; }

        [Column("FECHADESDE")]
        public Nullable<DateTime> Fechadesde { get; set; }

        [Column("FECHAHASTA")]
        public Nullable<DateTime> Fechahasta { get; set; }

        [Display(Name = "Capacitacion")]
        [MaxLength(10)]
        [Column("CAPACITACION")]
        public String Capacitacion { get; set; }

        [Display(Name = "UltimoUsuario")]
        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "UltimaFechaModif")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        [Display(Name = "INSTRUCCIONES")]
        [MaxLength(5000)]
        [Column("INSTRUCCIONES")]
        public String Instrucciones { get; set; }

        [Display(Name = "NIVELAPROBACION")]
        [Column("NIVELAPROBACION")]
        public Nullable<Int32> Nivelaprobacion { get; set; }

        [Column("ESTADO")]
        public String Estado { get; set; }

        [Column("ID_INSTITUCION")]
        public String idInstitucion { get; set; }
        [Column("ID_INSTITUCION_AREA")]
        public String idInstitucionArea { get; set; }
        [Column("ID_PROGRAMA")]
        public String idPrograma { get; set; }
        [Column("ID_COMPONENTE")]
        public String idComponente { get; set; }
        [Column("ID_MISCELANEO_HEADER1")]
        public String idMiscelaneoHeader1 { get; set; }
        [Column("ID_MISCELANEO_HEADER2")]
        public String idMiscelaneoHeader2 { get; set; }
        [Column("ID_MISCELANEO_HEADER3")]
        public String idMiscelaneoHeader3 { get; set; }
        [Column("ID_MISCELANEO_HEADER4")]
        public String idMiscelaneoHeader4 { get; set; }

        [NotMapped]
        public List<HrEncuestadetalle> listaPreguntas { get; set; }

        public HrEncuesta()
        {
            listaPreguntas = new List<HrEncuestadetalle>();
        }
    }
}
