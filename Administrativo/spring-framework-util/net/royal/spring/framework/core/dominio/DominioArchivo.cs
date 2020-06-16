using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio
{
    public class DominioArchivo
    {
        public static  String MSG_CARPETA_RAIZ_NO_ENCONTRADA = "CARPETARAIZNOENCONTRADA";

        public static  String MSG_ARCHIVO_ENCONTRADO = "OK";

        public static  String MSG_ARCHIVO_NO_ENCONTRADO = "ARCHIVONOENCONTRADO";

        public static  String MSG_CARPETA_NO_ENCONTRADA = "CARPETANOENCONTRADA";

        public static  String MSG_CODIGO_BUSQUEDA_VACIO = "CODIGOBUSQUEDAVACIO";

        public static  String MSG_CONFIGURACION_PENDIENTE = "CONFIGURACIONPENDIENTE";

        public static  String MSG_ARCHIVO_ACTUALIZADO = "UPDATE";
        /**
         * 
         */
        public String CodigoMensaje { get; set; }
        public String NombreArchivo { get; set; }
        public String RutaArchivo { get; set; }
        public String ExtensionArchivo { get; set; }
        public String TamanioArchivo { get; set; }
        public byte[] Archivo { get; set; }
        public String ArchivoCadena { get; set; }

        public DominioArchivo()
        {
            CodigoMensaje = MSG_CONFIGURACION_PENDIENTE;
        }
    }
}
