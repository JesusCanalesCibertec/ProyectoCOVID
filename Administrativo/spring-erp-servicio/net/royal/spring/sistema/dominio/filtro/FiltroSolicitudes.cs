using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.filtro
{
    public class FiltroSolicitudes
    {
        public Int32? Tipo { get; set; }

        public String CompaniaSocio { get; set; }
        public String Aplicacion { get; set; }
        public Int32? Proceso { get; set; }
        public Int32? NumeroProceso { get; set; }
        public Int32? ProcesoInterno { get; set; }
        public String UnidadReplicacion { get; set; }
        public String Estado { get; set; }
        public String Documento { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        public Int32? IdPersonaSolicitante { get; set; }
        public Int32? IdPersonaActual { get; set; }
        public Int32? Empleado { get; set; }

        public Int32? Nivel { get; set; }
        public Int32? Plan { get; set; }

        public ParametroPaginacionGenerico paginacion { get; set; }

        public DateTime? FechaSolicitud { get; set; }
        public String Concepto { get; set; }
        public String Aprobar1 { get; set; }
        public String Aprobar2 { get; set; }


        public FiltroSolicitudes()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
