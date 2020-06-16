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
 * @tabla dbo.companyowner_imagenes
*/
[Table("COMPANYOWNER_IMAGENES")]
public class CompanyownerImagenes: CompanyownerImagenesPk {

	[Display(Name = "imgPrincipal")]
	[Column("IMGPRINCIPAL")]
	public byte[] Imgprincipal  { get; set; }

	[Display(Name = "imgPrincipalNombreArchivo")]
	[MaxLength(300)]
	[Column("IMGPRINCIPALNOMBREARCHIVO")]
	public String Imgprincipalnombrearchivo  { get; set; }

	[Display(Name = "imgCompania")]
	[Column("IMGCOMPANIA")]
	public byte[] Imgcompania  { get; set; }

	[Display(Name = "imgCompaniaNombreArchivo")]
	[MaxLength(300)]
	[Column("IMGCOMPANIANOMBREARCHIVO")]
	public String Imgcompanianombrearchivo  { get; set; }

	[Display(Name = "imgfirmaBoleta")]
	[Column("IMGFIRMABOLETA")]
	public byte[] Imgfirmaboleta  { get; set; }

	[Display(Name = "imgfirmaBoletaNombreArchivo")]
	[MaxLength(300)]
	[Column("IMGFIRMABOLETANOMBREARCHIVO")]
	public String Imgfirmaboletanombrearchivo  { get; set; }

	[Display(Name = "imgfirmaCts")]
	[Column("IMGFIRMACTS")]
	public byte[] Imgfirmacts  { get; set; }

	[Display(Name = "imgfirmaCtsNombreArchivo")]
	[MaxLength(300)]
	[Column("IMGFIRMACTSNOMBREARCHIVO")]
	public String Imgfirmactsnombrearchivo  { get; set; }

	[Display(Name = "imgfirmaUtlidades")]
	[Column("IMGFIRMAUTLIDADES")]
	public byte[] Imgfirmautlidades  { get; set; }

	[Display(Name = "imgfirmaUtlidadesNombreArchivo")]
	[MaxLength(300)]
	[Column("IMGFIRMAUTLIDADESNOMBREARCHIVO")]
	public String Imgfirmautlidadesnombrearchivo  { get; set; }

	[Display(Name = "imgfirmaCalculoRenta")]
	[Column("IMGFIRMACALCULORENTA")]
	public byte[] Imgfirmacalculorenta  { get; set; }

	[Display(Name = "imgfirmaCalculoRentaNombreArchivo")]
	[MaxLength(300)]
	[Column("IMGFIRMACALCULORENTANOMBREARCHIVO")]
	public String Imgfirmacalculorentanombrearchivo  { get; set; }

	[Display(Name = "imgfirmaQuinta")]
	[Column("IMGFIRMAQUINTA")]
	public byte[] Imgfirmaquinta  { get; set; }

	[Display(Name = "imgfirmaQuintaNombreArchivo")]
	[MaxLength(300)]
	[Column("IMGFIRMAQUINTANOMBREARCHIVO")]
	public String Imgfirmaquintanombrearchivo  { get; set; }

	[Display(Name = "imgfirmaConstancia")]
	[Column("IMGFIRMACONSTANCIA")]
	public byte[] Imgfirmaconstancia  { get; set; }

	[Display(Name = "imgfirmaConstanciaNombreArchivo")]
	[MaxLength(300)]
	[Column("IMGFIRMACONSTANCIANOMBREARCHIVO")]
	public String Imgfirmaconstancianombrearchivo  { get; set; }

	public CompanyownerImagenes() {
	}
 }
}
