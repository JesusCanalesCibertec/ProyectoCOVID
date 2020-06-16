using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.proceso.dominio
{
    public class DtoEnlace
    {
        public String Enlace { get; set; }
        public String Correo { get; set; }
        public Boolean flgValida { get; set; }

        public String CodigoProceso { get; set; }

        public String Clave { get; set; }
        public String Estado { get; set; }
        public Int32 IdTransaccion { get; set; }
        public Int32 NumeroProceso { get; set; }
        public String Url { get; set; }
        public List<MensajeUsuario> listaMensaje { get; set; }

        public UsuarioActual usuarioActual;


        public Int32 empleado { get; set; }
        public String compania { get; set; }

        public String usuario { get; set; }
    }
}
