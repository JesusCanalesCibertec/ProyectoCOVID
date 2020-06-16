using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoCambioClave
    {
        public string claveAnterior { get; set; }
        public string claveNueva { get; set; }
        public string claveRepetida { get; set; }
    }
}
