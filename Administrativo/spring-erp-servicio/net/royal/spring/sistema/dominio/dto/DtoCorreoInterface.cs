using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto

{
    public class DtoCorreoInterface
    {
        public String asunto { get; set; }
        public byte[] cuerpoCorreo { get; set; }
        public List<String> listaCorreos { get; set; }
    }
}
