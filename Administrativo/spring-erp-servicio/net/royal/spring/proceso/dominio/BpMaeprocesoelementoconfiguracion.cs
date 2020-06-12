using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_MAE_PROCESO_ELEMENTO_CONFIGURACION", Schema = "sgseguridadsys")]
    public class BpMaeprocesoelementoconfiguracion : BpMaeprocesoelementoconfiguracionPk
    {
        [Column("ID_TIPO_CONFIGURACION")]
        public String IdTipoConfiguracion { get; set; }

        [Column("DEPENDENCIA_ID_PROCESO")]
        public String DependenciaIdProceso { get; set; }

        [Column("DEPENDENCIA_ID_ELEMENTO")]
        public String DependenciaIdElemento { get; set; }

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

        public BpMaeprocesoelementoconfiguracion()
        {

        }
        public static String TC_ELEMENTO_EXTERNO_PERMITE_GUARDAR = "ELEEXT-PER-GUAR";
        public static String TC_ELEMENTO_EXTERNO_RESTRINGE_CONEXIONES = "ELEEXT-RES-CONE";
    }
}
