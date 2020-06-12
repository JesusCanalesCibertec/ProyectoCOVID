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
     * @tabla dbo.HR_Departamento
*/
    [Table("HR_DEPARTAMENTO")]
    public class HrDepartamento : HrDepartamentoPk
    {

        [Display(Name = "Descripcion")]
        [MaxLength(80)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Display(Name = "department")]
        [MaxLength(3)]
        [Column("DEPARTMENT")]
        public String Department { get; set; }

        [Display(Name = "Abreviacion")]
        [MaxLength(10)]
        [Column("ABREVIACION")]
        public String Abreviacion { get; set; }

        [Display(Name = "Orden")]
        [Column("ORDEN")]
        public Nullable<Int32> Orden { get; set; }

        [Display(Name = "FlagRiesgo")]
        [MaxLength(1)]
        [Column("FLAGRIESGO")]
        public String Flagriesgo { get; set; }

        [Display(Name = "Telefono1")]
        [MaxLength(12)]
        [Column("TELEFONO1")]
        public String Telefono1 { get; set; }

        [Display(Name = "Telefono2")]
        [MaxLength(12)]
        [Column("TELEFONO2")]
        public String Telefono2 { get; set; }

        [Display(Name = "Anexo1")]
        [MaxLength(12)]
        [Column("ANEXO1")]
        public String Anexo1 { get; set; }

        [Display(Name = "Anexo2")]
        [MaxLength(12)]
        [Column("ANEXO2")]
        public String Anexo2 { get; set; }

        [Display(Name = "Anexo3")]
        [MaxLength(12)]
        [Column("ANEXO3")]
        public String Anexo3 { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [NotMapped]
        public int secuencia { get; set; }

        [NotMapped]
        public string DepTrim
        {
            get
            {
                return Departamento == null ? null : Departamento.Trim();
            }
            set
            {
                this.Departamento = value;
            }
        }

        public HrDepartamento()
        {
        }
    }
}
