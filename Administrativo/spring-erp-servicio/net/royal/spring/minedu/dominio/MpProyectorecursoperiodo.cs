using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.minedu.dominio
{

    [Table("PROYECTO_RECURSO_PERIODO",Schema ="minedu")]
    public class MpProyectorecursoperiodo : MpProyectorecursoperiodoPk
    {

        [Column("ID_CONTRATACION")]
        public Int32? IdContratacion { get; set; }

        [Column("FECHA_INICIO")]
        public Nullable<DateTime> Fechainicio  { get; set; }

        [Column("FECHA_FIN")]
        public Nullable<DateTime> Fechafin { get; set; }

        [Column("HORAS")]
        public Int32? Horas { get; set; }

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

    }

    
}
