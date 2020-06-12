using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.core.dominio.dto
{
    public class DtoUbigeo
    {
        public String Pais { get; set; }
        public String Departamento { get; set; }
        public String Provincia { get; set; }
        public String ZonaPostal { get; set; }
        public String Descripcion { get; set; }
        public String codigoElemento { get; set; }

        public String DepartamentoNombre { get; set; }
        public String ProvinciaNombre { get; set; }
        public String ZonaPostalNombre { get; set; }
    }
}
