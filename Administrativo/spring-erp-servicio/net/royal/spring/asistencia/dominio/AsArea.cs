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
     * @tabla dbo.AS_Area
    */
    [Table("AS_AREA")]
    public class AsArea: AsAreaPk {

	    [Display(Name = "Nombre")]
        [MaxLength(100)]
	    [Column("NOMBRE")]

        public String Nombre  { get; set; }

	    [Display(Name = "AreaPadre")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio")]
        [Column("AREAPADRE")]

        public Nullable<Decimal> Areapadre  { get; set; }

	    [Display(Name = "EmpleadoResponsable")]
        [Column("EMPLEADORESPONSABLE")]

        public Nullable<Decimal> Empleadoresponsable  { get; set; }

	    [Display(Name = "Estado")]
        [MaxLength(1)]
	    [Column("ESTADO")]

        public String Estado  { get; set; }

	    [Display(Name = "EmpleadoVisador")]
        [Column("EMPLEADOVISADOR")]

        public Nullable<Int32> Empleadovisador  { get; set; }

        [MaxLength(20)]
        [Column("ULTIMOUSUARIO")]
        public String Ultimousuario { get; set; }

        [Column("ULTIMAFECHAMODIF")]
        public Nullable<DateTime> Ultimafechamodif { get; set; }

        public AsArea() {
	}
 }
}
