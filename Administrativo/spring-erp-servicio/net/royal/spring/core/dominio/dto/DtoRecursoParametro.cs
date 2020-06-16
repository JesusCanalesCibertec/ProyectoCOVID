using net.royal.spring.framework.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.core.dominio.dto
{
    public class DtoRecursoParametro
    {
        public String TipoRecurso { get; set; }
        public String CompaniaSocio { get; set; }
        public String Periodo { get; set; }

        public byte[] Archivo { get; set; }

        public DtoRecursoParametro(String _TipoRecurso)
        {
            TipoRecurso = _TipoRecurso;
            Periodo = "999999";
            CompaniaSocio = "999999";
        }
        public DtoRecursoParametro(String _TipoRecurso, String _CompaniaSocio)
        {
            TipoRecurso = _TipoRecurso;
            CompaniaSocio = _CompaniaSocio;
            Periodo = "999999";
        }
        public DtoRecursoParametro(String _TipoRecurso, String _CompaniaSocio, String _Periodo) {
            TipoRecurso = _TipoRecurso;
            CompaniaSocio = _CompaniaSocio;
            Periodo = _Periodo;
        }

        public String getNombreCompleto()
        {
            String msgRetorno = "";

            if (UString.estaVacio(CompaniaSocio))
                CompaniaSocio = "999999";
            if (UString.estaVacio(Periodo))
                Periodo = "999999";
            
            msgRetorno = TipoRecurso;
            msgRetorno = msgRetorno + "_" + CompaniaSocio;
            msgRetorno = msgRetorno + "_" + Periodo;

            return msgRetorno;
        }
    }
}
