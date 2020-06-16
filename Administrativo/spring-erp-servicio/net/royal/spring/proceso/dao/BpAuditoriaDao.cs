using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.dao
{

    public interface BpAuditoriaDao : GenericoDao<BpAuditoria>
    {
        Int32? generarIdSecuencia(String idProceso, String idFuncionalidad, Int32? idPersona);

        ParametroPaginacionGenerico listar(ParametroPaginacionGenerico paginacion,
                FiltroPaginacionAuditoria filtro);
        List<DtoTabla> listarPeriodoBoleta();
    }
}
