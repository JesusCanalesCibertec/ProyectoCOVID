using net.royal.spring.framework.core.dominio;
using System;


namespace net.royal.spring.covid.dominio.filtro
{
    public class FiltroCiudadano
    {
        public String documento { get; set; }
        public String nombre { get; set; }
        public String pais { get; set; }
        public String departamento { get; set; }
        public String provincia { get; set; }
        public String distrito { get; set; }
        public Nullable<Int32> estado { get; set; }
        public Nullable<Int32> tipodocumento { get; set; }
        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroCiudadano()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
