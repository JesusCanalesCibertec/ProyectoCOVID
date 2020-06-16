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
     * @tabla dbo.HR_EncuestaDetalle
*/
    public class HrEncuestadetallePk
    {

        [Key]
        [Column("CompanyOwner")]
        public String Companyowner { get; set; }

        [Key]
        [Column("Periodo")]
        public String Periodo { get; set; }

        [Key]
        [Display(Name = "Secuencia")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("SECUENCIA")]
        public Nullable<Int32> Secuencia { get; set; }

        [Key]
        [Display(Name = "Pregunta")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("PREGUNTA")]
        public Nullable<Int32> Pregunta { get; set; }

        public HrEncuestadetallePk()
        {
        }

        public HrEncuestadetallePk(Nullable<Int32> pSecuencia, Nullable<Int32> pPregunta)
        {
            this.Secuencia = pSecuencia;
            this.Pregunta = pPregunta;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Companyowner, Periodo, Secuencia, Pregunta };
            return myObjArray;
        }
    }
}
