using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.core.dominio.filtro
{
    public class FiltroMiscelaneosDetalle : DominioOrden
    {
        public FiltroMiscelaneosDetalle() { }
        public FiltroMiscelaneosDetalle(String aplicacion, String tabla) {
            CodigoAplicacion = aplicacion;
            CodigoTabla = tabla;
        }

        public String CodigoAplicacion { get; set; }
        public String CodigoTabla { get; set; }
        public String CodigoCompania { get; set; }
        public String CodigoElemento { get; set; }
        public String Nombre { get; set; }
        public String Estado { get; set; }
        public List<DtoTabla> NotIn { get; set; }
        public String ValorCodigo1 { get; set; }
    }
}
