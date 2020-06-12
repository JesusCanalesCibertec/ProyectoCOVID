using net.royal.spring.framework.core.dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core
{
    public class UValidador
    {
        public static Boolean esListaVacia(IEnumerable lista)
        {
            if (lista == null)
                return true;
            return false;
        }

        public static Boolean esNulo(Object valor)
        {
            if (valor == null)
                return true;
            return false;
        }

        /*public static String concatenarArregloValidator(List<MensajeUsuario> listaMensajeUsuario)
        {
            String mensajeInterno = "";
            Int32 contador = 0;
            if (listaMensajeUsuario == null)
                return "";

            foreach (MensajeUsuario val in listaMensajeUsuario)
            {
                if (contador == 0)
                {
                    mensajeInterno = val.Mensaje;
                }
                else
                {
                    mensajeInterno = mensajeInterno + "\n" + val.Mensaje;
                    contador++;
                }
            }
            return mensajeInterno;
        }*/
    }
}
