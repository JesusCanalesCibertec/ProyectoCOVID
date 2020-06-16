using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.salud.dominio
{


    public class SsCie10subgrupoPk
    {

        [Key]
        [Display(Name = "ID_CAPITULO")]
        [MaxLength(15)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_CAPITULO")]
        public String IdCapitulo { get; set; }

        [Key]
        [Display(Name = "ID_GRUPO")]
        [MaxLength(15)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_GRUPO")]
        public String IdGrupo { get; set; }

        [Key]
        [Display(Name = "ID_SUBGRUPO")]
        [MaxLength(15)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_SUBGRUPO")]
        public String IdSubgrupo { get; set; }

        public SsCie10subgrupoPk()
        {
        }

        public SsCie10subgrupoPk(String pIdGrupo, String pIdCapitulo, String pIdSubgrupo)
        {
            this.IdGrupo = pIdGrupo;
            this.IdCapitulo = pIdCapitulo;
            this.IdSubgrupo = pIdSubgrupo;

        }

        public object[] obtenerArreglo()
        {
            object[] myObjArray = { IdGrupo, IdCapitulo, IdSubgrupo };
            return myObjArray;
        }

    }
}
