using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroListaProfesion: DominioOrden
    {
        public String Nombre { get; set; }
        public String Estado { get; set; }
        public String IdProfesion { get; set; }
    }
}
