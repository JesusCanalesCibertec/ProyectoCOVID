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
     * @tabla dbo.AC_CostCenterMst
*/
    public class AcCostcentermstPk
    {

        [Key]
        [Display(Name = "CostCenter")]
        [MaxLength(10)]
        [Required(ErrorMessage =  "El campo {0} no puede estar vacio" )]
        [Column("COSTCENTER")]
        public String Costcenter { get; set; }

        public AcCostcentermstPk()
        {
        }

        public AcCostcentermstPk(String Costcenter)
        {
            this.Costcenter = Costcenter;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Costcenter };
            return myObjArray;
        }
    }
}
