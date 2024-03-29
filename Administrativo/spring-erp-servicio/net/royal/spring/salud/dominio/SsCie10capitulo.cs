using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.salud.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.RT_INDICADOR
*/
    [Table("SS_cie10_capitulo", Schema = "sgseguridadsys")]
    public class SsCie10capitulo: SsCie10capituloPk {

	
	[Display(Name = "NOMBRE")]
	[MaxLength(200)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("NOMBRE")]
	public String Nombre  { get; set; }

	
	[Display(Name = "DESCRIPCION")]
	[MaxLength(500)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

        


    [Display(Name = "ESTADO")]
	[MaxLength(2)]
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

        [Display(Name = "ID_CAPITULO_OLD")]
        [MaxLength(15)]
        [Column("ID_CAPITULO_OLD")]
        public String IdCapituloOld { get; set; }

        public SsCie10capitulo()
        {
        }
 }
}
