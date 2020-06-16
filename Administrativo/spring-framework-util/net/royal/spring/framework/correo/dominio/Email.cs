using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.correo.dominio
{
    public class Email
    {
        public String Remitente { get; set; }
        public String Asunto { get; set; }
        public byte[] CuerpoCorreo { get; set; }
        public List<EmailDestino> ListaCorreoDestino { get; set; }
        public List<EmailAdjunto> ListaCorreoAdjunto { get; set; }

        public String CorreoDestinoPara { get; set; }
        public String CorreoDestinoCopia { get; set; }
        public String CorreoDestinoCopiaOculta { get; set; }
        public Email()
        {
            ListaCorreoDestino = new List<EmailDestino>();
            ListaCorreoAdjunto = new List<EmailAdjunto>();
        }
        public Int32 empleado { get; set; }
        public String compania { get; set; }

    }
}
