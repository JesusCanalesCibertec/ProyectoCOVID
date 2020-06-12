using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.minedu.dominio
{
    public class MpPersonatituloPk
    {
        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PERSONA")]
        public Nullable<Int32> IdPersona { get; set; }

        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("CARRERA")]
        public String IdCarrera { get; set; }

        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("CENTRO")]
        public String IdCentro { get; set; }

        [Key]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("NIVEL")]
        public String IdNivel { get; set; }




        public MpPersonatituloPk(){}

        public MpPersonatituloPk(Nullable<Int32> pIdPersona, string pIdCarrera, string pIdCentro, string pIdNivel)
        {
            this.IdPersona = pIdPersona;
            this.IdCarrera = pIdCarrera;
            this.IdCentro = pIdCentro;
            this.IdNivel = pIdNivel;
        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdPersona, IdCarrera, IdCentro, IdNivel };
            return myObjArray;
        }
    }
}
