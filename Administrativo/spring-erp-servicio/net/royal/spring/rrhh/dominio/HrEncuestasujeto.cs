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
     * @tabla dbo.HR_EncuestaSujeto
*/
    [Table("HR_ENCUESTASUJETO")]
    public class HrEncuestasujeto : HrEncuestasujetoPk
    {

        [Display(Name = "Valor")]
        [Column("VALOR")]
        public String Valor { get; set; }

        [Display(Name = "Observacion")]
        [MaxLength(250)]
        [Column("OBSERVACION")]
        public String Observacion { get; set; }

        [Display(Name = "AreaPosicion")]
        [Column("AREAPOSICION")]
        public Nullable<Int32> Areaposicion { get; set; }

        [Display(Name = "CompaniaSocio")]
        [MaxLength(8)]
        [Column("COMPANIASOCIO")]
        public String Companiasocio { get; set; }

        [Display(Name = "DepartamentoOperacional")]
        [MaxLength(10)]
        [Column("DEPARTAMENTOOPERACIONAL")]
        public String Departamentooperacional { get; set; }

        [Display(Name = "PersonasACargo")]
        [Column("PERSONASACARGO")]
        public Nullable<Int32> Personasacargo { get; set; }

        [Display(Name = "Sexo")]
        [MaxLength(1)]
        [Column("SEXO")]
        public String Sexo { get; set; }

        [Display(Name = "TiempoDeTrabajo")]
        [MaxLength(20)]
        [Column("TIEMPODETRABAJO")]
        public String Tiempodetrabajo { get; set; }

        [Display(Name = "TipoContrato")]
        [MaxLength(2)]
        [Column("TIPOCONTRATO")]
        public String Tipocontrato { get; set; }

        [Display(Name = "TipoPlanilla")]
        [MaxLength(2)]
        [Column("TIPOPLANILLA")]
        public String Tipoplanilla { get; set; }

        [Display(Name = "Categoria")]
        [MaxLength(3)]
        [Column("CATEGORIA")]
        public String Categoria { get; set; }

        [Display(Name = "Sindicato")]
        [MaxLength(1)]
        [Column("SINDICATO")]
        public String Sindicato { get; set; }

        [Display(Name = "Edad")]
        [MaxLength(20)]
        [Column("EDAD")]
        public String Edad { get; set; }

        [Column("NUMERO")]
        public Int32? Numero { get; set; }

        [Display(Name = "UltimoUsuario")]
        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "UltimaFechaModif")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        [Display(Name = "UBICACION")]
        [MaxLength(4)]
        [Column("UBICACION")]
        public String Ubicacion { get; set; }

        [Display(Name = "GRADOINSTRUCCION")]
        [MaxLength(5)]
        [Column("GRADOINSTRUCCION")]
        public String Gradoinstruccion { get; set; }

        [Display(Name = "DEPTOORGANIZACION")]
        [MaxLength(3)]
        [Column("DEPTOORGANIZACION")]
        public String Deptoorganizacion { get; set; }

        [Display(Name = "GERENCIA")]
        [MaxLength(4)]
        [Column("GERENCIA")]
        public String Gerencia { get; set; }

        [Display(Name = "JEFATURA")]
        [MaxLength(10)]
        [Column("JEFATURA")]
        public String Jefatura { get; set; }

        [Display(Name = "CANTIDADENCUESTADOS")]
        [Column("CANTIDADENCUESTADOS")]
        public Nullable<Int32> Cantidadencuestados { get; set; }

        [Display(Name = "TIPO")]
        [MaxLength(1)]
        [Column("TIPO")]
        public String Tipo { get; set; }


        [Column("TIPO_GUIAIN")]
        public String TipoGuiain { get; set; }

        [Column("AFE")]
        public String afe { get; set; }


        [Column("ORDEN")]
        public Nullable<Int32> Orden { get; set; }

        [Column("ID_INSTITUCION")]
        public String idInstitucion { get; set; }
        [Column("ID_INSTITUCION_AREA")]
        public String idInstitucionArea { get; set; }
        [Column("ID_PROGRAMA")]
        public String idPrograma { get; set; }
        [Column("ID_COMPONENTE")]
        public String idComponente { get; set; }
        [Column("MISCELANEO1")]
        public String miscelaneo1 { get; set; }
        [Column("MISCELANEO2")]
        public String miscelaneo2 { get; set; }
        [Column("MISCELANEO3")]
        public String miscelaneo3 { get; set; }
        [Column("MISCELANEO4")]
        public String miscelaneo4 { get; set; }

        public HrEncuestasujeto()
        {
        }
    }
}
