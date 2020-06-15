using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;

namespace net.royal.spring.core.dao
{

    public interface ProvinciaDao : GenericoDao<Provincia>
    {
        Provincia obtenerPorId(ProvinciaPk pk);
        List<Provincia> listar(FiltroProvincia filtro);
        List<Provincia> listar(string idDepartamento);
    }
}
