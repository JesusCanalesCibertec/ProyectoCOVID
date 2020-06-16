using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.BP_TRANSACCION_SEGUIMIENTO
*/
    [Table("BP_TRANSACCION_SEGUIMIENTO", Schema = "sgseguridadsys")]
    public class BpTransaccionseguimiento : BpTransaccionseguimientoPk
    {
        [Column("ENTRADA_ID_PROCESO")]
        public String EntradaIdProceso { get; set; }

        [Column("ENTRADA_ID_ELEMENTO")]
        public String EntradaIdElemento { get; set; }

        [Column("ENTRADA_NOMBRE")]
        public String EntradaNombre { get; set; }

        [Column("SALIDA_ID_PROCESO")]
        public String SalidaIdProceso { get; set; }

        [Column("SALIDA_ID_ELEMENTO")]
        public String SalidaIdElemento { get; set; }

        [Column("SALIDA_NOMBRE")]
        public String SalidaNombre { get; set; }

        [Column("FECHA_EVENTO")]
        public Nullable<DateTime> FechaEvento { get; set; }

        [Column("ID_PERSONA")]
        public Nullable<Int32> IdPersona { get; set; }

        [Column("USUARIO")]
        public String Usuario { get; set; }

        [Column("NOMBRE_COMPLETO")]
        public String NombreCompleto { get; set; }

        [Column("CARGO")]
        public String Cargo { get; set; }

        [Column("COMENTARIOS")]
        public String Comentarios { get; set; }

        [Column("ESTADO_ID_PROCESO")]
        public String EstadoIdProceso { get; set; }

        [Column("ESTADO_ID")]
        public String EstadoId { get; set; }

        [Column("CREACION_USUARIO")]
        public String CreacionUsuario { get; set; }

        [Column("CREACION_FECHA")]
        public Nullable<DateTime> CreacionFecha { get; set; }

        [Column("CREACION_TERMINAL")]
        public String CreacionTerminal { get; set; }

        [Column("MODIFICACION_USUARIO")]
        public String ModificacionUsuario { get; set; }

        [Column("MODIFICACION_FECHA")]
        public Nullable<DateTime> ModificacionFecha { get; set; }

        [Column("MODIFICACION_TERMINAL")]
        public String ModificacionTerminal { get; set; }

        public BpTransaccionseguimiento()
        {
        }
    }
}
