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
     * @tabla dbo.MA_UnidadNegocio
    */
    public class MaUnidadnegocioPk
    {
        private string _Unidadnegocio;

        [Key]
        [Display(Name = "UnidadNegocio")]
        [MaxLength(4)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("UNIDADNEGOCIO")]
        public String Unidadnegocio
        {
            get { return (_Unidadnegocio == null) ? "" : _Unidadnegocio.Trim(); }
            set { _Unidadnegocio = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Unidadnegocio };
            return myObjArray;
        }
    }
}
