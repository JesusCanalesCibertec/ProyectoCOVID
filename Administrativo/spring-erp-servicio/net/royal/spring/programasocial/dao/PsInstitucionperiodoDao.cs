using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao
{

    public interface PsInstitucionperiodoDao : GenericoDao<PsInstitucionperiodo>
    {
        List<DtoInstitucionperiodo> listado(string pIdInstitucion, string pIdPeriodo);
        List<DtoTabla> listarPeriodoPorComponente(string pIdInstitucion, String pIdPrograma, String pIdComponente);
        void copiarPeriodo(DtoInstitucionperiodo dtoInstitucionperiodo);
        List<DtoTabla> periodoListarSimple(String idPeriodo);
    }
}
