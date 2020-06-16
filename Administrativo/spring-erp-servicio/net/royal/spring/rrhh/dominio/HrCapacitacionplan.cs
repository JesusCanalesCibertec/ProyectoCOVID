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
 * @tabla dbo.HR_CapacitacionPlan
*/
[Table("HR_CAPACITACIONPLAN")]
public class HrCapacitacionplan: HrCapacitacionplanPk {

	[Display(Name = "NumeroVacantes")]
	[Column("NUMEROVACANTES")]
	public Nullable<Decimal> Numerovacantes  { get; set; }

	[Display(Name = "CostoSubtotal")]
	[Column("COSTOSUBTOTAL")]
	public Nullable<Decimal> Costosubtotal  { get; set; }

	[Display(Name = "CostoTotal")]
	[Column("COSTOTOTAL")]
	public Nullable<Decimal> Costototal  { get; set; }

	[Display(Name = "CostoTotalExt")]
	[Column("COSTOTOTALEXT")]
	public Nullable<Decimal> Costototalext  { get; set; }

	[Display(Name = "CostoSubTotalExt")]
	[Column("COSTOSUBTOTALEXT")]
	public Nullable<Decimal> Costosubtotalext  { get; set; }

	[Display(Name = "CostoImpuestosExt")]
	[Column("COSTOIMPUESTOSEXT")]
	public Nullable<Decimal> Costoimpuestosext  { get; set; }

	[Display(Name = "CostoImpuestos")]
	[Column("COSTOIMPUESTOS")]
	public Nullable<Decimal> Costoimpuestos  { get; set; }

	[Display(Name = "ESTADO")]
	[MaxLength(1)]
	[Column("ESTADO")]
	public String Estado  { get; set; }

	[Display(Name = "NumeroParticipantes")]
	[Column("NUMEROPARTICIPANTES")]
	public Nullable<Decimal> Numeroparticipantes  { get; set; }

	[Display(Name = "TotalDias")]
	[Column("TOTALDIAS")]
	public Nullable<Decimal> Totaldias  { get; set; }

	[Display(Name = "TotalHoras")]
	[Column("TOTALHORAS")]
	public Nullable<Decimal> Totalhoras  { get; set; }

	[Display(Name = "CostoTotalEstimadoLocal")]
	[Column("COSTOTOTALESTIMADOLOCAL")]
	public Nullable<Decimal> Costototalestimadolocal  { get; set; }

	[Display(Name = "CostoTotalEstimadoExtranjero")]
	[Column("COSTOTOTALESTIMADOEXTRANJERO")]
	public Nullable<Decimal> Costototalestimadoextranjero  { get; set; }

	[Display(Name = "CostoUnitarioLocal")]
	[Column("COSTOUNITARIOLOCAL")]
	public Nullable<Decimal> Costounitariolocal  { get; set; }

	[Display(Name = "CostoUnitarioExtranjero")]
	[Column("COSTOUNITARIOEXTRANJERO")]
	public Nullable<Decimal> Costounitarioextranjero  { get; set; }

	[Display(Name = "MontoMaxAsumido")]
	[Column("MONTOMAXASUMIDO")]
	public Nullable<Decimal> Montomaxasumido  { get; set; }

	 
	[Display(Name = "MontoAsumido")]
	[Column("MONTOASUMIDO")]
	public Nullable<Decimal> Montoasumido  { get; set; }

	 
	[Display(Name = "BenefTotLocal")]
	[Column("BENEFTOTLOCAL")]
	public Nullable<Decimal> Beneftotlocal  { get; set; }

	 
	[Display(Name = "BenefTotExtranjero")]
	[Column("BENEFTOTEXTRANJERO")]
	public Nullable<Decimal> Beneftotextranjero  { get; set; }

	 
	[Display(Name = "UltimoUsuario")]
	[MaxLength(20)]
	[Column("ULTIMOUSUARIO")]
	public String Ultimousuario  { get; set; }

	 
	[Display(Name = "UltimaFechaModif")]
	[Column("ULTIMAFECHAMODIF")]
	public Nullable<DateTime> Ultimafechamodif  { get; set; }

	 
	[Display(Name = "MOTIVO_RECHAZO")]
	[MaxLength(5000)]
	[Column("MOTIVO_RECHAZO")]
	public String MotivoRechazo  { get; set; }

	 
	[Display(Name = "NUMEROPROCESO")]
	[Required(ErrorMessage = " El campo {0} no puede estar vacio " )]
	[Column("NUMEROPROCESO")]
	public Nullable<Int32> Numeroproceso  { get; set; }

	 
	[Display(Name = "NIVELAPROBACION")]
	[Column("NIVELAPROBACION")]
	public Nullable<Int32> Nivelaprobacion  { get; set; }

	 
	[Display(Name = "CODIGOPROCESO")]
	[MaxLength(2)]
	[Column("CODIGOPROCESO")]
	public String Codigoproceso  { get; set; }

        public HrCapacitacionplan()
        {
        }
 }
}
