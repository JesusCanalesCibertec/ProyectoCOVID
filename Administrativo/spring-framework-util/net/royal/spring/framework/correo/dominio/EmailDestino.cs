using System;
using System.Collections.Generic;
using System.Text;
using static net.royal.spring.framework.correo.constante.ConstanteEmail;

namespace net.royal.spring.framework.correo.dominio
{
    public class EmailDestino
    {
        public tipo_destino Destino { get; set; }

        public String CorreoDestino { get; set; }

        public EmailDestino() { }

        public EmailDestino(String _correoDestino) {
            CorreoDestino = _correoDestino;
        }

    }
}
