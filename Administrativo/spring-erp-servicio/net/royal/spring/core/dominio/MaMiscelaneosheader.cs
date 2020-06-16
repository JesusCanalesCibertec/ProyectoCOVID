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
 * @tabla dbo.MA_MiscelaneosHeader
*/
[Table("MA_MISCELANEOSHEADER")]
public class MaMiscelaneosheader: MaMiscelaneosheaderPk {

	[Display(Name = "DescripcionLocal")]
	[MaxLength(250)]
	[Column("DESCRIPCIONLOCAL")]
	public String Descripcionlocal  { get; set; }
        
	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }


        [NotMapped]
        public List<MaMiscelaneosdetalle> listadetalle;

        public MaMiscelaneosheader()
        {
            listadetalle = new List<MaMiscelaneosdetalle>();
        }

       
 }
}
