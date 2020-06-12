using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.kpi.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.salud.dominio;

namespace net.royal.spring.salud.dao
{

    public interface SsCie10subgrupoDao : GenericoDao<SsCie10subgrupo>
    {
        List<SsCie10subgrupo> listarPorGrupo(string codigo);
    }
}
