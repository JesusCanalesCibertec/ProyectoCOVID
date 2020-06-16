using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.asistencia.dominio
{

    /**
     * 
     * 
     * @tabla dbo.AS_AsistenciaDiaria
    */
    [Table("AS_ASISTENCIADIARIA")]
    public class AsAsistenciadiaria: AsAsistenciadiariaPk {
        
	    [Display(Name = "Desde")]
        [Column("DESDE")]

        public Nullable<DateTime> Desde  { get; set; }

	    [Display(Name = "Hasta")]
        [Column("HASTA")]

        public Nullable<DateTime> Hasta  { get; set; }

	    [Display(Name = "Periodo")]
        [MaxLength(8)]
	    [Column("PERIODO")]

        public String Periodo  { get; set; }

        [Display(Name = "Hora")]
        [Column("HORA")]

        public Nullable<DateTime> Hora  { get; set; }

	    [Display(Name = "ConceptoAcceso")]
        [MaxLength(4)]
	    [Column("CONCEPTOACCESO")]

        public String Conceptoacceso  { get; set; }

	    [Display(Name = "ConJustificacion")]
        [MaxLength(1)]
	    [Column("CONJUSTIFICACION")]

        public String Conjustificacion  { get; set; }

	    [Display(Name = "ConceptoJustificacion")]
        [MaxLength(4)]
	    [Column("CONCEPTOJUSTIFICACION")]

        public String Conceptojustificacion  { get; set; }

	    [Display(Name = "TipoJustificacion")]
        [MaxLength(3)]
	    [Column("TIPOJUSTIFICACION")]

        public String Tipojustificacion  { get; set; }

	    [Display(Name = "CantidadHoras")]
        [Column("CANTIDADHORAS")]

        public Nullable<Decimal> Cantidadhoras  { get; set; }

	    [Display(Name = "HoraOficial")]
        [Column("HORAOFICIAL")]

        public Nullable<DateTime> Horaoficial  { get; set; }

	    [Display(Name = "Observaciones")]
        [MaxLength(60)]
	    [Column("OBSERVACIONES")]

        public String Observaciones  { get; set; }

	    [Display(Name = "Estado")]
        [MaxLength(1)]
	    [Column("ESTADO")]

        public String Estado  { get; set; }

	    [Display(Name = "Cantidad")]
        [Column("CANTIDAD")]

        public Nullable<Decimal> Cantidad  { get; set; }

	    [Display(Name = "ConceptoAccesoSistema")]
        [MaxLength(4)]
	    [Column("CONCEPTOACCESOSISTEMA")]

        public String Conceptoaccesosistema  { get; set; }

	    [Display(Name = "Justificacion")]
        [MaxLength(1)]
	    [Column("JUSTIFICACION")]

        public String Justificacion  { get; set; }

	    [Display(Name = "ConceptoSistema")]
        [MaxLength(4)]
	    [Column("CONCEPTOSISTEMA")]

        public String Conceptosistema  { get; set; }

	    [Display(Name = "HoraInicioDia")]
        [Column("HORAINICIODIA")]

        public Nullable<DateTime> Horainiciodia  { get; set; }

	    [Display(Name = "HoraFinDia")]
        [Column("HORAFINDIA")]

        public Nullable<DateTime> Horafindia  { get; set; }

	    [Display(Name = "TipoDia")]
        [Column("TIPODIA")]

        public Nullable<Int32> Tipodia  { get; set; }

	    [Display(Name = "CantidadReal")]
        [Column("CANTIDADREAL")]

        public Nullable<Decimal> Cantidadreal  { get; set; }

	    [Display(Name = "Origen")]
        [MaxLength(3)]
	    [Column("ORIGEN")]

        public String Origen  { get; set; }

	    [Display(Name = "CantidadAutorizada")]
        [Column("CANTIDADAUTORIZADA")]

        public Nullable<Decimal> Cantidadautorizada  { get; set; }

	    [Display(Name = "CantidadPlanilla")]
        [Column("CANTIDADPLANILLA")]

        public Nullable<Decimal> Cantidadplanilla  { get; set; }

        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public AsAsistenciadiaria() {
	}
 }
}
