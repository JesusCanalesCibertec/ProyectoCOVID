using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.salud.dominio
{


    public class SsGediagnosticoPk
    {

        [Key]
        [Display(Name = "IdDiagnostico")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("IdDiagnostico")]
        public Nullable<Int32> IdDiagnostico { get; set; }

       

        public SsGediagnosticoPk()
        {
        }

        public SsGediagnosticoPk(Nullable<Int32> pIdDiagnostico)
        {
            this.IdDiagnostico = pIdDiagnostico;
           

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdDiagnostico };
            return myObjArray;
        }

    }
}
