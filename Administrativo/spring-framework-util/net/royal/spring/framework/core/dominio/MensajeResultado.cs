using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio
{
    public class MensajeResultado
    {
        public Boolean FlgValido { get; set; }
        public List<MensajeUsuario> Mensajes { get; set; }

        public MensajeResultado()
        {
            FlgValido = false;
            Mensajes = new List<MensajeUsuario>();
        }

        public String getMensajesTexto()
        {
            String retorno = "";
            if (Mensajes == null)
                Mensajes = new List<MensajeUsuario>();
            foreach (MensajeUsuario mensajeUsuario in Mensajes)
            {
                retorno = retorno + "\n" + UString.obtenerValorCadenaSinNulo(mensajeUsuario.Mensaje);
            }
            return retorno;
        }
    }
}
