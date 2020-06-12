using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio {


    public class PsInstitucionperiodoPk {
        [Key]
        [MaxLength(6)]
        [Display(Name = "ID_PERIODO")]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_PERIODO")]
        public String IdPeriodo { get; set; }

        [Key]
        [Display(Name = "PS_INSTITUCION")]
        [MaxLength(5)]
        [Required(ErrorMessage = " El campo {0} no puede estar vacio ")]
        [Column("ID_INSTITUCION")]
        public String IdInstitucion { get; set; }

        [Key]
        [Display(Name = "ID_APLICACION")]
        [MaxLength(2)]
        [Required(ErrorMessage = " El campo {1} no puede estar vacio ")]
        [Column("ID_APLICACION")]
        public String IdAplicacion { get; set; }

        [Key]
        [Display(Name = "ID_GRUPO")]
        [MaxLength(6)]
        [Required(ErrorMessage = " El campo {2} no puede estar vacio ")]
        [Column("ID_GRUPO")]
        public String IdGrupo { get; set; }

        [Key]
        [Display(Name = "ID_CONCEPTO")]
        [MaxLength(6)]
        [Required(ErrorMessage = " El campo {3} no puede estar vacio ")]
        [Column("ID_CONCEPTO")]
        public String IdConcepto { get; set; }



        public PsInstitucionperiodoPk() {
        }

        public PsInstitucionperiodoPk(String pIdInstitucion, String pIdAplicacion,
            String pIdGrupo, String pIdConcepto, String pIdPeriodo) {

            this.IdInstitucion = pIdInstitucion;
            this.IdAplicacion = pIdAplicacion;
            this.IdGrupo = pIdGrupo;
            this.IdConcepto = pIdConcepto;
            this.IdPeriodo = pIdPeriodo;


        }

        public object[] obtenerArreglo() {
            object[] myObjArray = { IdInstitucion, IdAplicacion, IdGrupo, IdConcepto, IdPeriodo };
            return myObjArray;
        }
    }
}
