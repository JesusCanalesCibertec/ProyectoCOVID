using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.correo.dominio
{
    public class EmailAdjunto
    {
        public String NombreArchivo { get; set; }

        public byte[] ArchivoAdjunto { get; set; }

        public EmailAdjunto() { }
        public EmailAdjunto(byte[] _archivoAdjunto) {
            ArchivoAdjunto = _archivoAdjunto;
        }
        public EmailAdjunto(String _nombreArchivo, byte[] _archivoAdjunto) {
            NombreArchivo = _nombreArchivo;
            ArchivoAdjunto = _archivoAdjunto;
        }
    }
}
