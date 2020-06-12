using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrUnidadoperativaDao : GenericoDao<HrUnidadoperativa>
    {
        HrUnidadoperativa obtenerPorId(HrUnidadoperativaPk hrUnidadoperativaPk);
        List<HrUnidadoperativa> listarBusqueda(DtoFiltro filtroPaginacionJefatura);
        List<HrUnidadoperativa> listarActivos();
    }
}
