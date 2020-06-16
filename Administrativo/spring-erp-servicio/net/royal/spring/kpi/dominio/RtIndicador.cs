using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.kpi.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.RT_INDICADOR
*/
    [Table("RT_INDICADOR", Schema = "sgseguridadsys")]
    public class RtIndicador: RtIndicadorPk {

	
	[Display(Name = "ID_PROGRAMA")]
	[MaxLength(4)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_PROGRAMA")]
	public String IdPrograma  { get; set; }

	
	[Display(Name = "ID_COMPONENTE")]
	[MaxLength(4)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_COMPONENTE")]
	public String IdComponente  { get; set; }

	
	[Display(Name = "NOMBRE")]
	[MaxLength(200)]
	[Column("NOMBRE")]
	public String Nombre  { get; set; }

	
	[Display(Name = "DESCRIPCION")]
	[MaxLength(500)]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

	
	[Display(Name = "ESTADO")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	
	[Display(Name = "CREACION_USUARIO")]
	[MaxLength(50)]
	[Column("CREACION_USUARIO")]
	public String CreacionUsuario  { get; set; }

	
	[Display(Name = "CREACION_FECHA")]
	[Column("CREACION_FECHA")]
	public Nullable<DateTime> CreacionFecha  { get; set; }

	
	[Display(Name = "CREACION_TERMINAL")]
	[MaxLength(50)]
	[Column("CREACION_TERMINAL")]
	public String CreacionTerminal  { get; set; }

	
	[Display(Name = "MODIFICACION_USUARIO")]
	[MaxLength(50)]
	[Column("MODIFICACION_USUARIO")]
	public String ModificacionUsuario  { get; set; }

	
	[Display(Name = "MODIFICACION_FECHA")]
	[Column("MODIFICACION_FECHA")]
	public Nullable<DateTime> ModificacionFecha  { get; set; }

	
	[Display(Name = "MODIFICACION_TERMINAL")]
	[MaxLength(50)]
	[Column("MODIFICACION_TERMINAL")]
	public String ModificacionTerminal  { get; set; }

        public RtIndicador()
        {
        }
 }
}
