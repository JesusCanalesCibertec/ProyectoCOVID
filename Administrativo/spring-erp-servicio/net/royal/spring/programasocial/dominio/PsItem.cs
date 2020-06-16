using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio
{

/**
 * 
 * 
 * @tabla sgseguridadsys.PS_ITEM
*/
[Table("PS_ITEM", Schema = "sgseguridadsys")]
public class PsItem: PsItemPk {

	
	[Display(Name = "ID_TIPO_ITEM")]
	[MaxLength(3)]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("ID_TIPO_ITEM")]
	public String IdTipoItem  { get; set; }

	
	[Display(Name = "NOMBRE")]
	[MaxLength(200)]
	[Column("NOMBRE")]
	public String Nombre  { get; set; }

	
	[Display(Name = "DESCRIPCION")]
	[MaxLength(500)]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

	
	[Display(Name = "ID_UNIDAD_MEDIDA")]
	[MaxLength(6)]
	[Column("ID_UNIDAD_MEDIDA")]
	public String IdUnidadMedida  { get; set; }
    
	
	[Display(Name = "KILOCALORIAS")]
	[Column("KILOCALORIAS")]
	public Nullable<Int32> Kilocalorias  { get; set; }

	
	[Display(Name = "ID_LINEA")]
	[MaxLength(4)]
	[Column("ID_LINEA")]
	public String IdLinea  { get; set; }

	
	[Display(Name = "ID_FAMILIA")]
	[MaxLength(4)]
	[Column("ID_FAMILIA")]
	public String IdFamilia  { get; set; }

	
	[Display(Name = "ID_SUBFAMILIA")]
	[MaxLength(4)]
	[Column("ID_SUBFAMILIA")]
	public String IdSubfamilia  { get; set; }

	
	[Display(Name = "ID_CLASE")]
	[MaxLength(4)]
	[Column("ID_CLASE")]
	public String IdClase  { get; set; }

	
	[Display(Name = "ID_GRUPO")]
	[MaxLength(4)]
	[Column("ID_GRUPO")]
	public String IdGrupo  { get; set; }

        [Display(Name = "COEFICIENTE")]
        [Column("COEFICIENTE")]
        public Nullable<Decimal> Coeficiente { get; set; }



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

        public PsItem()
        {
        }
 }
}
