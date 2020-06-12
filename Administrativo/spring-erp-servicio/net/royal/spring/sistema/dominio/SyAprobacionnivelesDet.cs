using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.sistema.dominio
{

    /**
     * 
     * 
     * @tabla dbo.SY_AprobacionNiveles_Det
*/
    [Table("SY_APROBACIONNIVELES_DET")]
    public class SyAprobacionnivelesDet : SyAprobacionnivelesDetPk
    {
        [Display(Name = "FlagAprobJefe")]
        [MaxLength(1)]
        [Column("FlagAprobJefe")]
        public String FlagAprobJefe { get; set; }

        [Display(Name = "FlagArea")]
        [MaxLength(1)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("FLAGAREA")]
        public String Flagarea { get; set; }

        [Display(Name = "FLAGUNIDADOPERATIVA")]
        [MaxLength(1)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("FLAGUNIDADOPERATIVA")]
        public String Flagunidadoperativa { get; set; }


        [Display(Name = "Area")]
        [MaxLength(1000)]
        [Column("AREA")]
        public String Area { get; set; }

        [Display(Name = "Valor")]
        [MaxLength(1)]
        [Column("Valor")]
        public String Valor { get; set; }

        [Display(Name = "CorreoElectronico")]
        [MaxLength(300)]
        [Column("CORREOELECTRONICO")]
        public String Correoelectronico { get; set; }

        [Display(Name = "FlagEsRRHH")]
        [MaxLength(1)]
        [Column("FLAGESRRHH")]
        public String Flagesrrhh { get; set; }

        [Display(Name = "FLAGSOLICITANTE")]
        [MaxLength(1)]
        [Column("FLAGSOLICITANTE")]
        public String Flagsolicitante { get; set; }

        [Display(Name = "FLAGSUPERIOR")]
        [MaxLength(1)]
        [Column("FLAGSUPERIOR")]
        public String Flagsuperior { get; set; }


        [Display(Name = "COMPANYOWNERUSUARIO")]
        [MaxLength(1)]
        [Column("COMPANYOWNERUSUARIO")]
        public String companyownerusuario { get; set; }

        [NotMapped]
        public String AuxIdPersona { get; set; }

        [NotMapped]
        public String AuxNombrePersona { get; set; }

        [NotMapped]
        public Int32 AuxSecuencia { get; set; }

        [NotMapped]
        public List<DtoTabla> ListaCorreos;

        [NotMapped]
        public List<HrDepartamento> ListaAreas;

        [NotMapped]
        public List<HrTipocontrato> ListaValores;

        public SyAprobacionnivelesDet()
        {
        }
    }
}
