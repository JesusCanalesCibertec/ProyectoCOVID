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
     * @tabla dbo.AC_SucursalGrupo
    */
    public class AcSucursalgrupoPk
    {

        private string _Sucursalgrupo;
        [Key]
        [Display(Name = "SucursalGrupo")]
        [MaxLength(4)]
        [Required(ErrorMessage =  "El campo {0} no puede estar vacio" )]
        [Column("SUCURSALGRUPO")]
        public String Sucursalgrupo
        {
            get { return (_Sucursalgrupo == null) ? "" : _Sucursalgrupo.Trim(); }
            set { _Sucursalgrupo = value; }
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { Sucursalgrupo };
            return myObjArray;
        }
    }
}
