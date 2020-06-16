using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class FiltroGraficos
    {
        public String IdPeriodo { get; set; }
        public String IdPrograma { get; set; }
        public String IdComponente { get; set; }
        public String IdInstitucion { get; set; }
        public String Codigo { get; set; }
        public String IdSexo { get; set; }
        public Nullable<DateTime> FechaInicio { get; set; }
        public Nullable<DateTime> FechaFin { get; set; }
        public Nullable<Int32> Edad { get; set; }
        public String Nivel { get; set; }
        public String Grado { get; set; }
        public Nullable<Int32> idDiagnostico { get; set; }
        public String tipoRAS { get; set; }

        public String IdResidencia { get; set; }

        public String nomInstitucion { get; set; }
        public String nomTipoRas { get; set; }
        public String nomDiagnostico { get; set; }


        public String tipo_reporte { get; set; }
        public Nullable<Int32> secuencia { get; set; }
        public Nullable<Int32> idBeneficiario { get; set; }
        public ParametroPaginacionGenerico paginacion { get; set; }

        public Nullable<Int32> AnioInicio { get; set; }
        public Nullable<Int32> AnioFin { get; set; }

        public FiltroGraficos()
        {
            paginacion = new ParametroPaginacionGenerico();
        }

    }
}
