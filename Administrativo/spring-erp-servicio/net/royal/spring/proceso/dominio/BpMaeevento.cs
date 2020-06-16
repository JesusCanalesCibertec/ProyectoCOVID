using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_MAE_EVENTO", Schema = "sgseguridadsys")]
    public class BpMaeevento : BpMaeeventoPk
    {
        public static String TIPO_OBJETO_BD_STORE_PROCEDURE = "BDSP";
        public static String TIPO_OBJETO_JAVA_SPRING = "JVSP";

        public static String TIPO_EVENTO_VALIDACION = "VALI";
        public static String TIPO_EVENTO_EJECUCION = "EJEC";
        public static String TIPO_EVENTO_REQUERIMIENTO_CONEXION = "RQCN";

        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("OBJETO_BASE_DATOS")]
        public String ObjetoBD { get; set; }

        [Column("ID_TIPO_EVENTO")]
        public String IdTipoevento { get; set; }

        [Column("ID_TIPO_OBJETO")]
        public String IdTipoobjeto { get; set; }

        [Column("OBJETO_NET_CORE")]
        public String ObjetoNetCore { get; set; }

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

        public BpMaeevento()
        {
        }
    }
}
