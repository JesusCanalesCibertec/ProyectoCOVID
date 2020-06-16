using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.sistema.dominio
{

    /**
     * 
     * 
     * @tabla dbo.SeguridadConcepto
*/
    [Table("SEGURIDADCONCEPTO")]
    public class Seguridadconcepto : SeguridadconceptoPk
    {

        [Display(Name = "Descripcion")]
        [MaxLength(30)]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Display(Name = "VisibleFlag")]
        [MaxLength(1)]
        [Column("VISIBLEFLAG")]
        public String Visibleflag { get; set; }

        [Display(Name = "TipodeDetalle")]
        [MaxLength(1)]
        [Column("TIPODEDETALLE")]
        public String Tipodedetalle { get; set; }

        [Display(Name = "TipodeObjeto")]
        [MaxLength(1)]
        [Column("TIPODEOBJETO")]
        public String Tipodeobjeto { get; set; }

        [Display(Name = "Objeto")]
        [MaxLength(50)]
        [Column("OBJETO")]
        public String Objeto { get; set; }

        [Display(Name = "WebPage")]
        [MaxLength(50)]
        [Column("WEBPAGE")]
        public String WebPage { get; set; }

        [Display(Name = "WebAction")]
        [MaxLength(20)]
        [Column("WEBACTION")]
        public String WebAction { get; set; }

        [Display(Name = "TipodeAcceso")]
        [MaxLength(1)]
        [Column("TIPODEACCESO")]
        public String Tipodeacceso { get; set; }

        [Display(Name = "ObjetoWindow")]
        [MaxLength(80)]
        [Column("OBJETOWINDOW")]
        public String Objetowindow { get; set; }


        [Display(Name = "WebFlag")]
        [MaxLength(1)]
        [Column("WEBFLAG")]
        public String WebFlag { get; set; }

        [Display(Name = "Orden")]
        [MaxLength(1)]
        [Column("ORDEN")]
        public Int32? Orden { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        public Seguridadconcepto()
        {
        }
    }
}
