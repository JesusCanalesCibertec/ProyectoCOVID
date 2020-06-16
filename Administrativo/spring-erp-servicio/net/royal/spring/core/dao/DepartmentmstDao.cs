using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.dao
{

    public interface DepartmentmstDao : GenericoDao<Departmentmst>
    {
        Departmentmst obtenerPorId(DepartmentmstPk pk);
        List<Departmentmst> listar(DtoFiltro filtro);
        List<Departmentmst> listarBusqueda(DtoFiltro filtroPaginacionJefatura);
    }
}
