using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.proceso.dominio.filtro
{
    public class FiltroPaginacionAuditoria
    {
        public FiltroPaginacionAuditoria()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
        public String Periodo { get; set; }
        public String IdCompaniaSocio { get; set; }
        public String IdCentroCosto { get; set; }
        public String IdSucursal { get; set; }
        public String IdProceso { get; set; }
        public String IdFuncionalidad { get; set; }
        public String IdTipoPlanilla { get; set; }
        public Int32? IdEmpleado { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public ParametroPaginacionGenerico paginacion { get; set; }
    }
}
