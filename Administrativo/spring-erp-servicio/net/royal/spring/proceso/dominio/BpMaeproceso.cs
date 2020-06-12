using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_MAE_PROCESO", Schema = "sgseguridadsys")]
    public class BpMaeproceso : BpMaeprocesoPk
    {
        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("COMPONENTE")]
        public String Componente { get; set; }

        [Column("NOMBRE_CORTO")]
        public String NombreCorto { get; set; }

        [Column("ICONO_HOJA_ESTILO")]
        public String IconoHojaEstilo { get; set; }

        [Column("FLG_SEGURIDAD_ROL")]
        public String FlagSeguridadRol { get; set; }

        [Column("ID_REPOSITORIO")]
        public String IdRepositorio { get; set; }

        [Column("FLG_OCULTAR_BANDEJA_PENDIENTE")]
        public String FlagOcultarBandejaPendiente { get; set; }

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

        public BpMaeproceso()
        {
        }
    }
}
