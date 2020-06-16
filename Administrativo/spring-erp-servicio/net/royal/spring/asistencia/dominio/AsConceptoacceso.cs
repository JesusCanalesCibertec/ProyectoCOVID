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
     * @tabla dbo.AS_ConceptoAcceso
*/
    [Table("AS_CONCEPTOACCESO")]
    public class AsConceptoacceso : AsConceptoaccesoPk
    {
        [Display(Name = "fechavidautilini")]
        [Column("fechavidautilini")]
        public Nullable<DateTime> fechavidautilini { get; set; }

        [Display(Name = "fechavidautilfin")]
        [Column("fechavidautilfin")]
        public Nullable<DateTime> fechavidautilfin { get; set; }

        [Display(Name = "FlagTopeDiaSolicitud")]
        [MaxLength(1)]
        [Column("FlagTopeDiaSolicitud")]
        public String FlagTopeDiaSolicitud { get; set; }


        [Display(Name = "FlagTopeMesesSolicitud")]
        [MaxLength(1)]
        [Column("FlagTopeMesesSolicitud")]
        public String FlagTopeMesesSolicitud { get; set; }

        [Display(Name = "CantidadTopeDiaSolicitud")]
        [Column("CantidadTopeDiaSolicitud")]
        public Nullable<Decimal> CantidadTopeDiaSolicitud { get; set; }

        [Display(Name = "CantidadTopeMesesSolicitud")]
        [Column("CantidadTopeMesesSolicitud")]
        public Nullable<Decimal> CantidadTopeMesesSolicitud { get; set; }

        [Display(Name = "FlaghorasMaximo")]
        [MaxLength(1)]
        [Column("FlaghorasMaximo")]
        public String FlaghorasMaximo { get; set; }

        [Display(Name = "CantidadHorasMaximo")]
        [Column("CantidadHorasMaximo")]
        public Nullable<Decimal> CantidadHorasMaximo { get; set; }

        [Display(Name = "CantidadHoras")]
        [Column("CantidadHoras")]
        public Nullable<DateTime> CantidadHoras { get; set; }

        [Display(Name = "DescripcionLocal")]
        [MaxLength(40)]
        [Column("DESCRIPCIONLOCAL")]

        public String Descripcionlocal { get; set; }

        [Display(Name = "DescripcionExtranjera")]
        [MaxLength(40)]
        [Column("DESCRIPCIONEXTRANJERA")]

        public String Descripcionextranjera { get; set; }

        [Display(Name = "ConceptoAccesoSistema")]
        [MaxLength(4)]
        [Column("CONCEPTOACCESOSISTEMA")]

        public String Conceptoaccesosistema { get; set; }

        [Display(Name = "TipoConceptoAcceso")]
        [MaxLength(1)]
        [Column("TIPOCONCEPTOACCESO")]

        public String Tipoconceptoacceso { get; set; }

        [Display(Name = "ConceptoPlanilla")]
        [MaxLength(4)]
        [Column("CONCEPTOPLANILLA")]

        public String Conceptoplanilla { get; set; }

        [Display(Name = " ExpresadoEn")]
        [MaxLength(1)]
        [Column("EXPRESADOEN")]

        public String Expresadoen { get; set; }

        [Display(Name = "flagParaSustitucion")]
        [MaxLength(1)]
        [Column("FLAGPARASUSTITUCION")]

        public String Flagparasustitucion { get; set; }


        [Display(Name = "ConceptoRTPS")]
        [MaxLength(2)]
        [Column("CONCEPTORTPS")]

        public String Conceptortps { get; set; }


        [Display(Name = "FlagCompensa")]
        [MaxLength(1)]
        [Column("FLAGCOMPENSA")]
        public String Flagcompensa { get; set; }

        [Display(Name = "FlagAdjunto")]
        [MaxLength(1)]
        [Column("FlagAdjunto")]
        public String FlagAdjunto { get; set; }

        [NotMapped]
        [Display(Name = "FlagEsCuponera")]
        [MaxLength(1)]
        [Column("FLAGESCUPONERA")]
        public String FlagEsCuponera { get; set; }

        [NotMapped]
        [Display(Name = "FlagEsAniversario")]
        [MaxLength(1)]
        [Column("FlagEsAniversario")]
        public String FlagEsAniversario { get; set; }


        [Display(Name = "FlagOtrosPermisos")]
        [MaxLength(1)]
        [Column("FLAGOTROSPERMISOS")]
        public String Flagotrospermisos { get; set; }

        [Display(Name = "verweb")]
        [MaxLength(1)]
        [Column("verweb")]
        public String FlagWeb { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public AsConceptoacceso()
        {
        }
        public AsConceptoacceso(String concepto, String descripcionLocal)
        {
            this.Conceptoacceso = concepto;
            this.Descripcionlocal = descripcionLocal;
        }
    }
}
