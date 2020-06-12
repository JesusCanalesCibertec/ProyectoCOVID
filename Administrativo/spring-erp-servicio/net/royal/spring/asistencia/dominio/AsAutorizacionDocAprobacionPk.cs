using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.planilla.dominio
{

    /**
     * 
     * 
     * @tabla dbo.PR_SolicitudVacacionDocAprobacion
*/
    public class AsAutorizacionDocAprobacionPk
    {
        [Key]
        [Display(Name = "NUMEROPROCESO")]
        [Column("NUMEROPROCESO")]
        public Nullable<Int32> NumeroProceso { get; set; }

        [Key]
        [Display(Name = "SECUENCIAL")]
        [Column("SECUENCIAL")]
        public Nullable<Int32> Secuencial { get; set; }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { NumeroProceso, Secuencial };
            return myObjArray;
        }
    }
}
