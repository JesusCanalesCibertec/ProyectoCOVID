using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoFiltro : DominioOrden
    {
        public String compania { get; set; }
        public int empleado { get; set; }
        public String Codigo { get; set; }
        public String Nombre { get; set; }
        public String Estado { get; set; }
        public List<DtoTabla> NotIn { get; set; }
        public int jefe { get; set; }

        public DtoFiltro() { }
        public DtoFiltro(String _Codigo, String _Nombre, String _Estado) {
            Codigo = _Codigo;
            Nombre = _Nombre;
            Estado = _Estado;
        }
    }
}
