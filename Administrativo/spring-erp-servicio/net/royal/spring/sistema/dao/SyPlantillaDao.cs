using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.sistema.dao
{

    public interface SyPlantillaDao : GenericoDao<SyPlantilla>
    {
        SyPlantilla obtenerPorId(SyPlantillaPk pk);

        ParametroPaginacionGenerico solicitudListar(ParametroPaginacionGenerico paginacion, SyPlantilla filtro);
    }
}
