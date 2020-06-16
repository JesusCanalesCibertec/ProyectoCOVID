using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;

namespace net.royal.spring.core.dao
{

    public interface ZonapostalDao : GenericoDao<Zonapostal>
    {
            List<Zonapostal> listar(FiltroZonaPostal filtro);
        Zonapostal obtenerPorId(ZonapostalPk pk);
    }
}
