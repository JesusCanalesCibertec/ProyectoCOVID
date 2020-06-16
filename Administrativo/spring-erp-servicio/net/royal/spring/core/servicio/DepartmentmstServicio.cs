using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio
{

public interface DepartmentmstServicio : GenericoServicio {
        List<Departmentmst> listarTodos();

        List<Departmentmst> listarActivos();

        List<Departmentmst> listar(DtoFiltro filtro);
        List<Departmentmst> listarBusqueda(DtoFiltro filtroPaginacionJefatura);
    }
}
