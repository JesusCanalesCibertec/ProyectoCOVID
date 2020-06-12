using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.minedu.dominio
{

    [Table("PROYECTO",Schema ="minedu")]
    public class MpProyecto : MpProyectoPk
    {

        [Column("CODIGO_PROYECTO")]
        public String Codigoproyecto { get; set; }

        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Column("NOMBRE_CORTO")]
        public String Nombrecorto { get; set; }


        [Column("GESTOR")]
        public Int32? Gestor { get; set; }

        [Column("GESTOR_ANTERIOR")]
        public String Gestoranterior { get; set; }

        [Column("EXPEDIENTE")]
        public String Expediente { get; set; }

        [Column("FECHA_EXPEDIENTE")]
        public String Fechaexpediente { get; set; }

        [Column("TIPO_PROYECTO")]
        public String Tipoproyecto { get; set; }

        [Column("COORDINACION")]
        public String Coordinacion { get; set; }

        [Column("AREA")]
        public Int32? Area { get; set; }

        [Column("FECHA_INICIO")]
        public DateTime? Fechainicio { get; set; }

        [Column("FECHA_FIN")]
        public DateTime? Fechafin { get; set; }

        [Column("ASUNTO")]
        public String Asunto { get; set; }

        [Column("ESTADO_ATENCION")]
        public String Estadoatencion { get; set; }

        [Column("FASE_GESTION")]
        public String Fasegestion { get; set; }

        [Column("TIPO_ANALISIS")]
        public String Tipoanalisis { get; set; }

        [Column("FASE_INGENIERIA")]
        public String Faseingenieria { get; set; }

        [Column("AVANCE_PLAN")]
        public Decimal? Avanceplan { get; set; }

        [Column("AVANCE_REAL")]
        public Decimal? Avancereal { get; set; }

        [Column("RUTA")]
        public String Ruta { get; set; }

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

        [Column("NUM_PARTICIPANTES")]
        public Int32? Numparticipantes { get; set; }

        [Column("ID_SISTEMA")]
        public Int32? IdSistema { get; set; }

        [Column("PROCESO")]
        public String Proceso { get; set; }

        [Column("OBSERVACION")]
        public String Observacion { get; set; }



        [NotMapped]
        public List<MpProyectorecurso> listaRecursos;

        public MpProyecto()
        {
            listaRecursos = new List<MpProyectorecurso>();
        }

    } 
}
