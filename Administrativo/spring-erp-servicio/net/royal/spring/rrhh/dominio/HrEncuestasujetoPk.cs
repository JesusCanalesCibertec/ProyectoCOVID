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
    public class HrEncuestasujetoPk
    {

        [Key]
        [Display(Name = "Secuencia")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("SECUENCIA")]
        public Nullable<Int32> Secuencia { get; set; }

        [Key]
        [Display(Name = "Sujeto")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("SUJETO")]
        public Nullable<Int32> Sujeto { get; set; }

        [Key]
        [Display(Name = "Pregunta")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("PREGUNTA")]
        public Nullable<Int32> Pregunta { get; set; }

        [Key]
        [Display(Name = "COMPANYOWNER")]
        [MaxLength(8)]
        [Column("COMPANYOWNER")]
        public String Companyowner { get; set; }

        [Key]
        [Display(Name = "PERIODO")]
        [MaxLength(6)]
        [Column("PERIODO")]
        public String Periodo { get; set; }

        public HrEncuestasujetoPk()
        {
        }

        public HrEncuestasujetoPk(Nullable<Int32> pSecuencia, Nullable<Int32> pSujeto, Nullable<Int32> pPregunta, String pCompanyowner, String pPeriodo)
        {
            this.Secuencia = pSecuencia;
            this.Sujeto = pSujeto;
            this.Pregunta = pPregunta;
            this.Periodo = pPeriodo;
            this.Companyowner = pCompanyowner;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Secuencia, Sujeto, Pregunta, Companyowner, Periodo };
            return myObjArray;
        }
    }
}
