using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace net.royal.spring.minedu.dominio
{

    [Table("CONTRATACION",Schema ="minedu")]
    public class MpContratacion : MpContratacionPk
    {

        [Column("ID_PERSONA")]
        public Int32? IdPersona { get; set; }

        [Column("NUMERO_PLAZO")]
        public Nullable<Int32> Numeroplazo { get; set; }

        [Column("FECHA_INICIO")]
        public Nullable<DateTime> Fechainicio { get; set; }
        [Column("FECHA_FIN")]
        public Nullable<DateTime> Fechafin { get; set; }

        [Column("ID_MODALIDAD")]
        public String IdModalidad { get; set; }

        [Column("NUMERO_ORDEN")]
        public String Numeroorden { get; set; }

        [Column("CARGO")]
        public String Cargo { get; set; }

        [Column("SERVICIO")]
        public String Servicio { get; set; }

        [Column("DESCRIPCION_SERVICIO")]
        public String Descripcionservicio { get; set; }

        [Column("ESTADO")]
        public String Estado { get; set; }

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

        [Column("FECHA_CESE")]
        public Nullable<DateTime> Fechacese { get; set; }

        [Column("MOTIVO_CESE")]
        public String Motivocese { get; set; }

        [NotMapped]
        public List<MpContratacionadendaentregable> listadetalle;

        public MpContratacion()
        {
            listadetalle = new List<MpContratacionadendaentregable>();
        }

    }

    
}
