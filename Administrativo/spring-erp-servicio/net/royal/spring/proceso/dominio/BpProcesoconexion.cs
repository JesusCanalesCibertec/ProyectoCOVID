using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    [Table("BP_PROCESO_CONEXION", Schema = "sgseguridadsys")]
    public class BpProcesoconexion : BpProcesoconexionPk
    {
        [Column("ID_UNICO")]
        public String IdUnico { get; set; }

        [Column("ACCION")]
        public String Accion { get; set; }

        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }


        [Column("CAMPOS_VALIDAR")]
        public String CamposValidar { get; set; }


        [Column("ICONO_HOJA_ESTILO")]
        public String IconoHojaEstilo { get; set; }

        [Column("FLG_SALIR")]
        public String FlagSalir { get; set; }

        [Column("ENTRADA_ID_PROCESO")]
        public String EntradaIdProceso { get; set; }

        [Column("ENTRADA_ID_ELEMENTO")]
        public String EntradaIdElemento { get; set; }

        [Column("ENTRADA_PUNTO_CONEXION")]
        public Nullable<Int32> EntradaPuntoConexion { get; set; }

        [Column("SALIDA_ID_PROCESO")]
        public String SalidaIdProceso { get; set; }

        [Column("SALIDA_ID_ELEMENTO")]
        public String SalidaIdElemento { get; set; }

        [Column("SALIDA_PUNTO_CONEXION")]
        public Nullable<Int32> SalidaPuntoConexion { get; set; }

        [Column("FLG_FIRMA_DIGITAL")]
        public String FlagFirmaDigital { get; set; }

        [Column("FLG_GENERAR_NOTIFICACION")]
        public String FlagGenerarNotificación { get; set; }

        [Column("FLG_CONEXION_PROGRAMADA")]
        public String FlagConexionProgramada { get; set; }

        [Column("FLG_GENERAR_CORREO")]
        public String FlagGenerarCorreo { get; set; }

        [Column("FLG_OCULTAR_USUARIO")]
        public String FlagOcultarUsuario { get; set; }

        [Column("FLG_VALIDAR_REQUERIMIENTO")]
        public String FlagValidarRequerimiento { get; set; }

        [Column("FLG_SOLICITAR_COMENTARIO")]
        public String FlagSolicitarComentario { get; set; }

        [Column("FLG_OBLIGAR_COMENTARIO")]
        public String FlagObligarComentario { get; set; }

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

        [Column("REPORTE_ID")]
        public String ReporteId { get; set; }

        [Column("REPORTE_ID_APLICACION")]
        public String ReporteIdAplicacion { get; set; }

        [NotMapped]
        public bool AuxRequerimientoHabilitar { get; set; }

        [NotMapped]
        public String AuxDescripcion { get; set; }

        [NotMapped]
        public BpMaeprocesoelemento auxElementoEntrada { get; set; }

        [NotMapped]
        public BpMaeprocesoelemento auxElementoSalida { get; set; }

        [NotMapped]
        public Nullable<Int32> AuxTransaccion { get; set; }

        [NotMapped]
        public BpTransaccion AuxTransaccionBean { get; set; }

        [NotMapped]
        public String AuxComentario { get; set; }

        [NotMapped]
        public String IdExterno { get; set; }

        [NotMapped]
        public Int32 AuxInt { get; set; }

        public BpProcesoconexion()
        {
        }
    }
}
