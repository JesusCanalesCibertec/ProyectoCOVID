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
 * @tabla dbo.Provincia
*/
[Table("PROVINCIA")]
public class Provincia: ProvinciaPk {

	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

	[Column("ESTADO")]
	public String Estado  { get; set; }
        
	public Provincia() {
	}
 }
}
