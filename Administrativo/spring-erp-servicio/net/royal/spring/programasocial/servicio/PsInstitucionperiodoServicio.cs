using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio {

    public interface PsInstitucionperiodoServicio : GenericoServicio {
        List<DtoInstitucionperiodo> listado(string pIdInstitucion, string pIdPeriodo);
        List<DtoInstitucionperiodo> coreActualizar(List<DtoInstitucionperiodo> list);
        List<DtoTabla> periodoListarSimple(String idPeriodo);
        
        void copiarPeriodo(DtoInstitucionperiodo dtoInstitucionperiodo);
        List<DtoTabla> listarPeriodoPorComponente(string pIdInstitucion, String pIdPrograma, String pIdComponente);
    }
}
