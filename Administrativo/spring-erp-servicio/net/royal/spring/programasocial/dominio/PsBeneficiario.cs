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
     * @tabla sgseguridadsys.PS_BENEFICIARIO
*/
    [Table("PS_BENEFICIARIO", Schema = "sgseguridadsys")]
    public class PsBeneficiario : PsBeneficiarioPk
    {

        [Column("ID_PROGRAMA")]
        public String IdPrograma { get; set; }

        [Display(Name = "ID_AREA")]
        [MaxLength(5)]
        [Column("ID_AREA")]
        public String IdArea { get; set; }

        [Display(Name = "ID_COMPONENTE_INGRESO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_COMPONENTE_INGRESO")]
        public Nullable<Int32> IdComponenteIngreso { get; set; }

        [Display(Name = "ID_COMPONENTE_NUTRICION")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_COMPONENTE_NUTRICION")]
        public Nullable<Int32> IdComponenteNutricion { get; set; }

        [Display(Name = "ID_COMPONENTE_SALUD")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_COMPONENTE_SALUD")]
        public Nullable<Int32> IdComponenteSalud { get; set; }

        [Display(Name = "ID_COMPONENTE_CAPACIDAD")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_COMPONENTE_CAPACIDAD")]
        public Nullable<Int32> IdComponenteCapacidad { get; set; }

        [Display(Name = "ID_COMPONENTE_SOCIO_AMBIENTAL")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_COMPONENTE_SOCIO_AMBIENTAL")]
        public Nullable<Int32> IdComponenteSocioAmbiental { get; set; }

        [Display(Name = "ESTADO")]
        [MaxLength(3)]
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

        public PsBeneficiario()
        {
        }
    }
}
