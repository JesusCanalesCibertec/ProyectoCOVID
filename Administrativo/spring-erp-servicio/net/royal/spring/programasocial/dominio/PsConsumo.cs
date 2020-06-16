using net.royal.spring.rrhh.dominio.dto;
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
     * @tabla sgseguridadsys.PS_CONSUMO
*/
    [Table("PS_CONSUMO", Schema = "sgseguridadsys")]
    public class PsConsumo : PsConsumoPk
    {


        [Display(Name = "ID_PERIODO")]
        [Column("ID_PERIODO")]
        public String IdPeriodo { get; set; }

        [Display(Name = "DESCRIPCION")]
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Display(Name = "APORTANTE")]
        [Column("APORTANTE")]
        public String Aportante { get; set; }

        [Display(Name = "ESTADO")]
        [MaxLength(1)]
        [Column("ESTADO")]
        public String Estado { get; set; }

        [Column("comentario")]
        public String comentario { get; set; }

        [Column("valoracion")]
        public Int32? valoracion { get; set; }

        [Display(Name = "FECHA_INICIO")]
        [Column("FECHA_INICIO")]
        public Nullable<DateTime> Fechainicio { get; set; }

        [Display(Name = "INICIOCONSUMO")]
        [Column("INICIOCONSUMO")]
        public Nullable<DateTime> inicioConsumo { get; set; }

        [Display(Name = "FINCONSUMO")]
        [Column("FINCONSUMO")]
        public Nullable<DateTime> finConsumo { get; set; }

        [Display(Name = "FECHADESPACHO")]
        [Column("FECHADESPACHO")]
        public Nullable<DateTime> fechaDespacho { get; set; }

        [Display(Name = "FECHA_FIN")]
        [Column("FECHA_FIN")]
        public Nullable<DateTime> Fechafin { get; set; }

        [Display(Name = "FECHA_CIERRE")]
        [Column("FECHA_CIERRE")]
        public Nullable<DateTime> FechaCierre { get; set; }


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

        [NotMapped]
        public List<PsConsumoNutricional> listadetalle;

        public PsConsumo()
        {
            listadetalle = new List<PsConsumoNutricional>();
        }
    }
}
