using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace net.royal.spring.framework.correo.dominio
{
    public class EmailConfiguracion
    {
        public SmtpClient SessionCorreo { get; set; }

        public String FlgCorreoPrueba { get; set; }
        public String CorreoPrueba { get; set; }
        public String TipoConfiguracion { get; set; }

        public String EmailCuenta { get; set; }
        public String EmailClave { get; set; }
        public String EmailPerfil { get; set; }
        public String RutaRaizAdjuntos { get; set; }

        public Boolean FlgResultadoEnvioCorreo { get; set; }
        public String MensajeEnvioCorreo { get; set; }
        public String EmailHost { get; set; }
        public String EmailPuerto { get; set; }
        public String EmailFlagSsl { get; set; }

        public String EnvioAsincrono { get; set; }

        public String FlgEnviar { get; set; }
    }
}
