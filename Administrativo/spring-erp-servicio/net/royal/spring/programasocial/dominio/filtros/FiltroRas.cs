using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroRas
    {

        public String IdInstitucion { get; set; }
        public Int32 IdBeneficiario { get; set; }
        public String IdTipoEnfermedad { get; set; }
        public String IdPeriodo { get; set; }
        public Int32 Desde { get; set; }
        public Int32 Hasta { get; set; }
        public String IdSexo { get; set; }
        public String IdPrograma { get; set; }
        public String NombreCompleto { get; set; }
        public String IdTipoAtencion { get; set; }
        public String IdArea { get; set; }
        public Int32 CantidadRegistros { get; set; }
        public Nullable<Int32> IdDiagnostico { get; set; }
        public Nullable<DateTime> FechaAtencion { get; set; }
        public Nullable<DateTime> FechaAtencionFin { get; set; }
        public Nullable<DateTime> FechaAtencionInicio { get; set; }

        public Nullable<Int32> orden { get; set; }

        public Nullable<Int32> sentido { get; set; }


        public ParametroPaginacionGenerico paginacion { get; set; }

        public FiltroRas()
        {
            paginacion = new ParametroPaginacionGenerico();
        }
    }
}
