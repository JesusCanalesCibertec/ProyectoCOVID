using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

/**
 * 
 * 
 * @tabla sgseguridadsys.BP_AUDITORIA
*/
public class BpAuditoriaPk {

	[Key]
	[Display(Name = "ID_PROCESO")]
	[MaxLength(15)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_PROCESO")]
	public String IdProceso  { get; set; }

	[Key]
	[Display(Name = "ID_FUNCIONALIDAD")]
	[MaxLength(50)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_FUNCIONALIDAD")]
	public String IdFuncionalidad  { get; set; }

	[Key]
	[Display(Name = "ID_PERSONA")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_PERSONA")]
	public Nullable<Int32> IdPersona  { get; set; }

	[Key]
	[Display(Name = "ID_SECUENCIA")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_SECUENCIA")]
	public Nullable<Int32> IdSecuencia  { get; set; }
 }
}
