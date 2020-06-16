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
     * @tabla sgseguridadsys.PS_ENTIDAD_SEGURO_SOCIAL
*/
    public class PsEntidadSeguroSocialPk
    {

        [Key]
        [Display(Name = "ID_ENTIDAD")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_ENTIDAD")]
        public Nullable<Int32> IdEntidad { get; set; }

        [Key]
        [Display(Name = "ID_SEGURO_SOCIAL")]
        [MaxLength(5)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_SEGURO_SOCIAL")]
        public String IdSeguroSocial { get; set; }

        public PsEntidadSeguroSocialPk()
        {
        }

        public PsEntidadSeguroSocialPk(Nullable<Int32> pIdEntidad, String pIdSeguroSocial)
        {
            this.IdEntidad = pIdEntidad;
            this.IdSeguroSocial = pIdSeguroSocial;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdEntidad, IdSeguroSocial };
            return myObjArray;
        }
    }
}
