using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proyecto.dominio
{

    /**
     * 
     * 
     * @tabla sgseguridadsys.PM_PROYECTO
*/
    [Table("PM_TIPO_PROYECTO", Schema = "sgseguridadsys")]

    public class PmTipoproyecto : PmTipoproyectoPk
    {
        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("ID_PROCESO")]
        public String IdProceso { get; set; }

        [Column("TITULO_PROYECTO")]
        public String TituloProyecto { get; set; }

        [Column("TITULO_TAREA")]
        public String TituloTarea { get; set; }

        [Column("FLG_GENERAR_TRANSACCION")]
        public String FlagGeneraTransaccion { get; set; }

        [Column("FLG_MOSTRAR_OBJETIVO")]
        public String FlagMostrarObjetivo { get; set; }

        [Column("FLG_MOSTRAR_RECURSOS")]
        public String FlagMostrarRecursos { get; set; }

        [Column("ESTADO")]
        public String Estado { get; set; }

        [Column("TAREA_ID_PROCESO")]
        public String TareaIdProceso { get; set; }

        [Column("TAREA_TIPO1_MOSTRAR")]
        public String TareaTipo1Mostrar { get; set; }

        [Column("TAREA_TIPO1_CODIGO")]
        public String TareaTipo1Codigo { get; set; }

        [Column("TAREA_TIPO1_ETIQUETA")]
        public String TareaTipo1Etiqueta { get; set; }

        [Column("TAREA_FLG_OCULTAR_NUEVO")]
        public String TareaFlgOcultarNuevo { get; set; }

        [Column("TAREA_TIPO1_REQUERIDO")]
        public String TareaTipo1Requerido { get; set; }

        [Column("TAREA_FLG_GENERAR_TRANSACCION")]
        public String TareaFlgGeneraTransaccion { get; set; }


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

        public PmTipoproyecto()
        {

        }
    }
}
