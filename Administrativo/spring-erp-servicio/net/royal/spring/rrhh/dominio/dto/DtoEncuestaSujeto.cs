using net.royal.spring.framework.core.dominio.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoEncuestaSujeto
    {

        public String compania { get; set; }
        public String periodo { get; set; }
        public int secuencia { get; set; }
        public String sexo { get; set; }
        public String edad { get; set; }
        public int? sujeto { get; set; }
        public String institucion { get; set; }
        public Nullable<DateTime> fecha { get; set; }

        public String tipo { get; set; }
        public String residencia { get; set; }
        public DtoEncuestaSujeto()
        {

        }
    }
}