using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.core.dominio.filtro
{
    public class FiltroPaginacionEmpleado
    {
        public String IdCompaniaSocio { get; set; }
        public String IdSucursal { get; set; }
        public String IdUnidadNegocioAsignada { get; set; }
        public Int32? EmpleadoId { get; set; }
        public String EmpleaadoNombreCompleto { get; set; }
        public String EmpleadoEstado { get; set; }
        public String EmpleadoDocumento { get; set; }
        public Int32? EmpleadoJefe { get; set; }
        public String EmpleadoUsuario { get; set; }
        public String EmpleadoNoValida { get; set; }
        public Nullable<DateTime> fechaDesde { get; set; }
        public Nullable<DateTime> fechaHasta { get; set; }
        public String EmpleadoConceptoAcceso { get; set; }
        public String estado { get; set; }
        public int jefeUnidad { get; set; }
        public string IdUnidadOperativa { get; set; }
        public Nullable<Int32> puesto { get; set; }
        public string esAdministradorWeb { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroPaginacionEmpleado() {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
