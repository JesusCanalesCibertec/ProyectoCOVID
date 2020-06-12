using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.contabilidad.dominio
{

    /**
     * 
     * 
     * @tabla dbo.AC_SucursalGrupo
    */
    [Table("AC_SUCURSALGRUPO")]
    public class AcSucursalgrupo: AcSucursalgrupoPk {

	    [Display(Name = "SucursalGrupoMayor")]
        [MaxLength(4)]
	    [Column("SUCURSALGRUPOMAYOR")]

        public String Sucursalgrupomayor  { get; set; }

	    [Display(Name = "DescripcionLocal")]
        [MaxLength(30)]
	    [Column("DESCRIPCIONLOCAL")]

        public String Descripcionlocal  { get; set; }
        
	    [Display(Name = "Estado")]
        [MaxLength(1)]
	    [Column("ESTADO")]

        public String Estado  { get; set; }

        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public AcSucursalgrupo() {
	}
 }
}
