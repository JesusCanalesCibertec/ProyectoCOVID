using net.royal.spring.programasocial.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoMarcologico
    {

        public String codigo { get; set; }
        public String nombre { get; set; }
        public String estado { get; set; }

        public String marco { get; set; }
        public String nombreMarco { get; set; }
        public String resultado { get; set; }
        public String nombreResultado { get; set; }

        /*public List<DtoMarcologicoresultado> children;

        public DtoMarcologico()
        {
            children = new List<DtoMarcologicoresultado>();
        }*/

    }
}
