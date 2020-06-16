using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.salud.dominio {


    [Table("SS_ge_diagnostico", Schema = "sgseguridadsys")]
    public class SsGediagnostico : SsGediagnosticoPk {
        [Display(Name = "CodigoDiagnostic")]
        [MaxLength(15)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("CodigoDiagnostic")]
        public String CodigoDiagnostic { get; set; }

        [Display(Name = "CodigoPadre")]
        [MaxLength(15)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("CodigoPadre")]
        public String CodigoPadre { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(200)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("Nombre")]
        public String Nombre { get; set; }


        [Display(Name = "Descripcion")]
        [MaxLength(500)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("Descripcion")]
        public String Descripcion { get; set; }

        [Display(Name = "ID_CAPITULO")]
        [MaxLength(15)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_CAPITULO")]
        public String IdCapitulo { get; set; }


        [Display(Name = "ID_GRUPO")]
        [MaxLength(15)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_GRUPO")]
        public String IdGrupo { get; set; }


        [Display(Name = "ID_SUBGRUPO")]
        [MaxLength(15)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_SUBGRUPO")]
        public String IdSubgrupo { get; set; }

        [Display(Name = "CREACION_USUARIO")]
        [MaxLength(50)]
        [Column("CREACION_USUARIO")]
        public String CreacionUsuario { get; set; }


        [Display(Name = "ESTADO")]
        [Column("ESTADO")]
        public Nullable<Int32> Estado { get; set; }


        [Display(Name = "FLG_CRONICO")]
        [MaxLength(1)]
        [Column("FLG_CRONICO")]
        public String FlgCronico { get; set; }


        [Display(Name = "CREACION_FECHA")]
        [Column("CREACION_FECHA")]
        public Nullable<DateTime> CreacionFecha { get; set; }


        [Display(Name = "CREACION_TERMINAL")]
        [MaxLength(50)]
        [Column("CREACION_TERMINAL")]
        public String CreacionTerminal { get; set; }


        [Display(Name = "MODIFICACION_USUARIO")]
        [MaxLength(50)]
        [Column("MODIFICACION_USUARIO")]
        public String ModificacionUsuario { get; set; }


        [Display(Name = "MODIFICACION_FECHA")]
        [Column("MODIFICACION_FECHA")]
        public Nullable<DateTime> ModificacionFecha { get; set; }


        [Display(Name = "MODIFICACION_TERMINAL")]
        [MaxLength(50)]
        [Column("MODIFICACION_TERMINAL")]
        public String ModificacionTerminal { get; set; }


        public SsGediagnostico() {
        }
    }
}
