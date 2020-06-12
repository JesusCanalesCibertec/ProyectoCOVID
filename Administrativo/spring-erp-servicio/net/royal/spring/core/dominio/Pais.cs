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
     * @tabla dbo.Pais
*/
    [Table("PAIS")]
    public class Pais : PaisPk
    {

        private String _Descripcioncorta;

        [Display(Name = "DescripcionCorta")]
        [MaxLength(20)]
        [Column("DESCRIPCIONCORTA")]
        public String Descripcioncorta
        {
            get { return (_Descripcioncorta == null) ? "" : _Descripcioncorta.Trim(); }
            set
            {
                _Descripcioncorta = value;
            }
        }


        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }


        [Display(Name = "Nacionalidad")]
        [MaxLength(30)]
        [Column("NACIONALIDAD")]
        public String Nacionalidad { get; set; }

        public Pais()
        {
        }
    }
}
