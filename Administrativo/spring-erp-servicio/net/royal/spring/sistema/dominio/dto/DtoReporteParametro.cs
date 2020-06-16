using net.royal.spring.framework.core;
using System;
using System.Collections.Generic;
using System.Text;
using static net.royal.spring.sistema.constante.ConstanteReporte;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoReporteParametro
    {
        public String AplicacionCodigo { get; set; }
        public String ReporteCodigo { get; set; }

        public String CompaniaSocio { get; set; }
        public String Periodo { get; set; }
        public String Version { get; set; }
        public FormatoSalia FormatoSalida { get; set; }

        public String getNombreCompletoReporte()
        {
            String msgRetorno = "";

            if (UString.estaVacio(CompaniaSocio))
                CompaniaSocio = "999999";
            if (UString.estaVacio(Periodo))
                Periodo = "999999";
            if (UString.estaVacio(Version))
                Version = "999999";

            msgRetorno = AplicacionCodigo + "_" + ReporteCodigo;
            msgRetorno = msgRetorno + "_" + CompaniaSocio;
            msgRetorno = msgRetorno + "_" + Periodo;
            msgRetorno = msgRetorno + "_" + Version;

            return msgRetorno;
        }

        public DtoReporteParametro(String _aplicacionCodigo, String _reporteCodigo, String _companiaSocio, String _periodo, String _version)
        {
            AplicacionCodigo = _aplicacionCodigo;
            ReporteCodigo = _reporteCodigo;

            CompaniaSocio = _companiaSocio;
            Periodo = _periodo;
            Version = _version;
        }
        public DtoReporteParametro(String _aplicacionCodigo, String _reporteCodigo, String _companiaSocio, String _periodo)
        {
            AplicacionCodigo = _aplicacionCodigo;
            ReporteCodigo = _reporteCodigo;

            CompaniaSocio = _companiaSocio;
            Periodo = _periodo;
        }
        public DtoReporteParametro(String _aplicacionCodigo, String _reporteCodigo, String _companiaSocio)
        {
            AplicacionCodigo = _aplicacionCodigo;
            ReporteCodigo = _reporteCodigo;
            CompaniaSocio = _companiaSocio;
        }
        public DtoReporteParametro(String _aplicacionCodigo, String _reporteCodigo)
        {
            AplicacionCodigo = _aplicacionCodigo;
            ReporteCodigo = _reporteCodigo;
        }

    }
}
