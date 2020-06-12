using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.rrhh.dominio
{

/**
 * 
 * 
 * @tabla dbo.HR_EncuestaPregunta
*/
[Table("HR_ENCUESTAPREGUNTA")]
public class HrEncuestapregunta: HrEncuestapreguntaPk {

	[Display(Name = "Descripcion")]
	[MaxLength(500)]
	[Column("DESCRIPCION")]
	public String Descripcion  { get; set; }

	[Display(Name = "Area")]
	[MaxLength(10)]
	[Column("AREA")]
	public String Area  { get; set; }

	[Display(Name = "ValorMinimo")]
	[Column("VALORMINIMO")]
	public Nullable<Int32> Valorminimo  { get; set; }

	[Display(Name = "ValorMaximo")]
	[Column("VALORMAXIMO")]
	public Nullable<Int32> Valormaximo  { get; set; }

	[Display(Name = "Leyenda")]
	[MaxLength(500)]
	[Column("LEYENDA")]
	public String Leyenda  { get; set; }

	[Display(Name = "Tipo")]
	[MaxLength(1)]
	[Column("TIPO")]
	public String Tipo  { get; set; }

	[Display(Name = "flagpersonacargo")]
	[MaxLength(1)]
	[Column("FLAGPERSONACARGO")]
	public String Flagpersonacargo  { get; set; }

	[Display(Name = "Estado")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	[Display(Name = "UltimoUsuario")]
	[MaxLength(20)]
	[Column("ULTIMOUSUARIO")]
	public String Ultimousuario  { get; set; }

	[Display(Name = "UltimaFechaModif")]
	[Column("ULTIMAFECHAMODIF")]
	public Nullable<DateTime> Ultimafechamodif  { get; set; }

	[Display(Name = "Secuencia")]
	[Column("SECUENCIA")]
	public Nullable<Int32> Secuencia  { get; set; }

	[Display(Name = "flagcapacitacion")]
	[MaxLength(1)]
	[Column("FLAGCAPACITACION")]
	public String Flagcapacitacion  { get; set; }



   
        
        [Column("TIPOENCUESTA")]
        public String Tipoencuesta { get; set; }

        [Column("RequiereFlag")]
        public String Requiereflag { get; set; }

        [Column("RequierePregunta")]
        public Nullable<Int32> Requierepregunta { get; set; }

        [Column("RequiereValor")]
        public String Requierevalor { get; set; }

        [NotMapped]
        public String nombrePregunta { get; set; }


        [NotMapped]
        public List<HrEncuestapreguntavalores> listaValores;

        public HrEncuestapregunta()
        {
            listaValores = new List<HrEncuestapreguntavalores>();
        }
 }

}