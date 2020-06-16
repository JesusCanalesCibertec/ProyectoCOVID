using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.core.dominio.dto
{
    public class DtoRecursoConfiguracionResultado
    {
        public Boolean FlgOk { get; set; }
        public String RutaCompleta { get; set; }
        public byte[] Recurso { get; set; }

        public DtoRecursoConfiguracionResultado()
        {
            FlgOk = false;
        }
    }
}
