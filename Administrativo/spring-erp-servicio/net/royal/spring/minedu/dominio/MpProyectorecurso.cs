using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.minedu.dominio
{

    [Table("PROYECTO_RECURSO",Schema ="minedu")]
    public class MpProyectorecurso : MpProyectorecursoPk
    {
        [Column("ID_PERSONA")]
        public Int32? IdPersona { get; set; }

        [Column("AREA")]
        public Int32? Area { get; set; }

        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Column("CARGO")]
        public String Cargo { get; set; }

        [Column("ROL")]
        public String Rol { get; set; }

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


        [NotMapped]
        public String auxConocimientos { get; set; }

        [NotMapped]
        public Int32? idContratacion { get; set; }

        [NotMapped]
        public List<MpProyectorecursoperiodo> listaPeriodos;

        public MpProyectorecurso()
        {
            listaPeriodos = new List<MpProyectorecursoperiodo>();
        }
    }

    
}
