using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dominio.dto;

namespace net.royal.spring.sistema.dominio
{

    /**
     * 
     * 
     * @tabla dbo.SY_AprobacionNiveles
*/
    [Table("SY_APROBACIONNIVELES")]
    public class SyAprobacionniveles : SyAprobacionnivelesPk
    {

        [Display(Name = "COMPANYOWNERUSUARIO")]
        [Column("COMPANYOWNERUSUARIO")]
        public String CompanyOwnerusuario { get; set; }

        [Display(Name = "Aplicacion")]
        [MaxLength(2)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("APLICACION")]
        public String Aplicacion { get; set; }

        [Display(Name = "CodigoProceso")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("CODIGOPROCESO")]
        public Nullable<Int32> Codigoproceso { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(80)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Display(Name = "FlagAplicaDetalle")]
        [MaxLength(1)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("FLAGAPLICADETALLE")]
        public String Flagaplicadetalle { get; set; }

        [Display(Name = "ValorDetalle")]
        [MaxLength(15)]
        [Column("VALORDETALLE")]
        public String Valordetalle { get; set; }

        [Display(Name = "ValorDetalle_V")]
        [MaxLength(15)]
        [Column("VALORDETALLE_V")]
        public String ValordetalleV { get; set; }

        [Display(Name = "NroNiveles")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("NRONIVELES")]
        public Nullable<Int32> Nroniveles { get; set; }

        [Display(Name = "FlagAplicaExclusion")]
        [MaxLength(1)]
        [Column("FLAGAPLICAEXCLUSION")]
        public String Flagaplicaexclusion { get; set; }

        [Display(Name = "ValorExclusion")]
        [MaxLength(15)]
        [Column("VALOREXCLUSION")]
        public String Valorexclusion { get; set; }

        [Display(Name = "ValorExclusion_V")]
        [MaxLength(15)]
        [Column("VALOREXCLUSION_V")]
        public String ValorexclusionV { get; set; }

        [Display(Name = "FlagUsuarioSuper")]
        [MaxLength(1)]
        [Column("FLAGUSUARIOSUPER")]
        public String Flagusuariosuper { get; set; }

        [Display(Name = "Usuario")]
        [Column("USUARIO")]
        public Nullable<Int32> Usuario { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Display(Name = "USUARIOADMINISTRADOR")]
        [Column("USUARIOADMINISTRADOR")]
        public Nullable<Int32> Usuarioadministrador { get; set; }

        [Display(Name = "CORREOELECTRONICO")]
        [MaxLength(1000)]
        [Column("CORREOELECTRONICO")]
        public String Correoelectronico { get; set; }

        [Display(Name = "FLAGSOLICITANTE")]
        [MaxLength(1)]
        [Column("FLAGSOLICITANTE")]
        public String Flagsolicitante { get; set; }

        [Display(Name = "FLAGSUPERIOR")]
        [MaxLength(1)]
        [Column("FLAGSUPERIOR")]
        public String Flagsuperior { get; set; }

        [Display(Name = "FLAGUSUARIOADMINISTRADOR")]
        [MaxLength(1)]
        [Column("FLAGUSUARIOADMINISTRADOR")]
        public String Flagusuarioadministrador { get; set; }

        [Display(Name = "FLAGAREA")]
        [MaxLength(1)]
        [Column("FLAGAREA")]
        public String Flagarea { get; set; }

        [Display(Name = "AREA")]
        [MaxLength(1000)]
        [Column("AREA")]
        public String Area { get; set; }

        [Display(Name = "FLAGREVERSION")]
        [MaxLength(1)]
        [Column("FLAGREVERSION")]
        public String Flagreversion { get; set; }

        [Display(Name = "ULTIMOUSUARIO")]
        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Display(Name = "ULTIMAFECHAMODIF")]
        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        [NotMapped]
        public String AuxAplicacionNombre;

        [NotMapped]
        public String AuxEstadoNombre;

        [NotMapped]
        public String AuxUsuarioNombre { get; set; }

        [NotMapped]
        public String AuxUsuarioAdmNombre { get; set; }

        [NotMapped]
        public List<DtoNivelDetalle> ListaNiveles { get; set; }

        [NotMapped]
        public List<DtoTabla> ListaCorreos { get; set; }

        [NotMapped]
        public Boolean FlagusuariosuperB { get; set; }

        [NotMapped]
        public Boolean FlagusuarioadministradorB { get; set; }

        [NotMapped]
        public bool FlagsolicitanteB { get; set; }

        [NotMapped]
        public List<DtoTabla> ListaCorreosConfirmacion { get; set; }

        [NotMapped]
        public bool FlagsuperiorB { get; set; }

        [Display(Name = "CORREOELECTRONICO")]
        [MaxLength(1000)]
        [Column("NIVEL0CORREOELECTRONICO")]
        public String Nivel0Correoelectronico { get; set; }

        [Display(Name = "FLAGSOLICITANTE")]
        [MaxLength(1)]
        [Column("NIVEL0FLAGSOLICITANTE")]
        public String Nivel0Flagsolicitante { get; set; }

        [Display(Name = "FLAGSUPERIOR")]
        [MaxLength(1)]
        [Column("NIVEL0FLAGSUPERIOR")]
        public String Nivel0Flagsuperior { get; set; }


        public SyAprobacionniveles()
        {
        }
    }
}
