using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.core.dominio
{

    /**
     * 
     * 
     * @tabla dbo.afemst
    */
    public class AfemstPk
    {

        private String _afe;

        [Key]
        [Display(Name = "afe")]
        [MaxLength(15)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("AFE")]
        public String Afe
        {
            get { return (_afe == null) ? "" : _afe.Trim(); }
            set { _afe = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Afe };
            return myObjArray;
        }
    }
}
