using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_MAE_PROCESO_ELEMENTO", Schema = "sgseguridadsys")]
    public class BpMaeprocesoelemento : BpMaeprocesoelementoPk
    {
        [Column("ID_UNICO")]
        public String IdUnico { get; set; }

        [Column("NOMBRE")]
        public String Nombre { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }

        [Column("ID_CLASE_ELEMENTO")]
        public String IdClaseElemento { get; set; }

        [Column("ID_TIPO_ELEMENTO")]
        public String IdTipoElemento { get; set; }

        [Column("ID_ESTADO")]
        public String IdEstado { get; set; }

        [Column("ID_ROL")]
        public String IdRol { get; set; }

        [Column("ID_NIVEL_SEGURIDAD")]
        public String IdNivelSeguridad { get; set; }

        [Column("ICONO_HOJA_ESTILO")]
        public String IconoHojaEstilo { get; set; }


        [Column("PORCENTAJE_AVANCE")]
        public Nullable<Int32> PorcentajeAvance { get; set; }

        [Column("DIAS_DURACION")]
        public Nullable<Int32> DiasDuracion { get; set; }

        [Column("ID_COLOR")]
        public String IdColor { get; set; }

        [Column("RUTA_ICONO")]
        public String RutaIcono { get; set; }

        [Column("FLG_PUEDE_REGISTRAR")]
        public String FlagPuedeRegistrar { get; set; }

        [Column("FLG_PUEDE_ACTUALIZAR")]
        public String FlagPuedeActualizar { get; set; }

        [Column("FLG_PUEDE_VER")]
        public String FlagPuedeVer { get; set; }

        [Column("FLG_PUEDE_ELIMINAR")]
        public String FlagPuedeEliminar { get; set; }

        [Column("FLG_PUEDE_INACTIVAR")]
        public String FlagPuedeInactivar { get; set; }

        [Column("FLG_FINAL_EXITOSO")]
        public String FlagFinalExitoso { get; set; }

        [Column("POSICION_X")]
        public Nullable<Int32> PosicionX { get; set; }

        [Column("POSICION_Y")]
        public Nullable<Int32> PosicionY { get; set; }

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
        public Boolean auxFlgPuedeActualizar { get; set; }

        [NotMapped]
        public Boolean auxFlgPuedeConexiones { get; set; }

        [NotMapped]
        public string AuxIconoHojaEstilo { get; set; }

        public BpMaeprocesoelemento()
        {
        }
    }
}
