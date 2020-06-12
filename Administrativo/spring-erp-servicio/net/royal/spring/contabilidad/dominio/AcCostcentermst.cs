using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.contabilidad.dominio
{

/**
 * 
 * 
 * @tabla dbo.AC_CostCenterMst
*/
[Table("AC_COSTCENTERMST")]
public class AcCostcentermst: AcCostcentermstPk {

	[Display(Name = "LocalName")]
    [MaxLength(50)]
	[Column("LOCALNAME")]

    public String Localname  { get; set; }
        
	[Display(Name = "Vendor")]
    [Column("VENDOR")]

    public Nullable<Int32> Vendor  { get; set; }
        

	[Display(Name = "Status")]
    [MaxLength(1)]
	[Column("STATUS")]

    public String Status  { get; set; }

	[Display(Name = "LastUser")]
    [MaxLength(10)]
	[Column("LASTUSER")]

    public String Lastuser  { get; set; }

	[Display(Name = "Lastdate")]
    [Column("LASTDATE")]

    public Nullable<DateTime> Lastdate  { get; set; }
        
	public AcCostcentermst() {
	}
 }
}
