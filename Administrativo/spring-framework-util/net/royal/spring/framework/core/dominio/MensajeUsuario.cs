using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio
{
    public class MensajeUsuario
    {
        public enum tipo_mensaje
        {
            ERROR, ADVERTENCIA, EXITO, INFORMACION
        };

        public tipo_mensaje TipoMensaje;
        public String Codigo { get; set; }
        public String Titulo { get; set; }
        public String Mensaje { get; set; }
        public String Fuente { get; set; }
        public String Solucion { get; set; }
        public List<string> mensajes { get; set; }

        public MensajeUsuario()
        {
        }

        public MensajeUsuario(tipo_mensaje _tipoMensaje, String _mensaje)
        {
            TipoMensaje = _tipoMensaje;
            Mensaje = _mensaje;
        }

        public MensajeUsuario(tipo_mensaje _tipoMensaje, String _titulo, String _mensaje)
        {
            TipoMensaje = _tipoMensaje;
            Titulo = _titulo;
            Mensaje = _mensaje;
        }

    }
}
