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
 * @tabla dbo.Departamento
*/
[Table("DEPARTAMENTO")]
public class Departamento: DepartamentoPk {

	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }
        
	[Column("ID_PAIS")]
	public String Pais  { get; set; }

	[Column("ESTADO")]
	public String Estado  { get; set; }
        
	public Departamento() {
	}
 }
}
