using net.royal.spring.sistema.dominio.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dtorest
{
    public class DtoRestSolicitudLista
    {
        public String accion { get; set; }
        public List<DtoSolicitud> listaSolicitudes { get; set; }
    }
}
