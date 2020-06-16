using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core
{
    public class UException : Exception
    {
        public bool flagTabla { get; set; }
        public List<MensajeUsuario> Errors { get; set; }

        public enum Severidad
        {
            Info,
            Warn,
            Error,
            Fatal
        }

        public UException()
        { this.flagTabla = false; }

        public UException(List<MensajeUsuario> listaMensajeUsuario) : base("")
        {
            this.flagTabla = false;
            Errors = listaMensajeUsuario;
        }

        public UException(List<MensajeUsuario> listaMensajeUsuario, Boolean flagTabla) : base("")
        {
            this.flagTabla = flagTabla;
            Errors = listaMensajeUsuario;
        }

        public UException(String message) : base(message)
        {
            this.flagTabla = false;
        }

        public IList<MensajeUsuario> obtenerErrores()
        {

            var lista = this.Errors;

            if (lista == null)
            {
                lista = new List<MensajeUsuario>();
            }

            if (!UString.estaVacio(this.Message))
            {
                lista.Add(new MensajeUsuario() { Mensaje = this.Message, TipoMensaje = MensajeUsuario.tipo_mensaje.INFORMACION });
            }

            return lista;
        }

    }
}
