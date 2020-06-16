using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio {

    /**
     * 
     * 
     * @tabla sgseguridadsys.PS_ATENCION_DETALLE
*/
    [Table("PS_ATENCION_DETALLE", Schema = "sgseguridadsys")]
    public class PsAtencionDetalle : PsAtencionDetallePk {


        [Display(Name = "CANTIDAD")]
        [Column("CANTIDAD")]
        public Nullable<Int32> Cantidad { get; set; }


        [Display(Name = "ESTADO")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Display(Name = "TRATAMIENTOS_NOMBRE")]
        [MaxLength(200)]
        [Column("TRATAMIENTOS_NOMBRE")]
        public String TratamientosNombre { get; set; }

        [Display(Name = "POSICION")]
        [Column("POSICION")]
        public Nullable<Int32> Posicion { get; set; }

        

        [Display(Name = "CREACION_USUARIO")]
        [MaxLength(50)]
        [Column("CREACION_USUARIO")]
        public String CreacionUsuario { get; set; }


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

        public PsAtencionDetalle() {
        }
    }
}
