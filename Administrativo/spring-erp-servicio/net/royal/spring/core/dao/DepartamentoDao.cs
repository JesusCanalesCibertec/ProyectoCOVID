using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;

namespace net.royal.spring.core.dao
{

    public interface DepartamentoDao : GenericoDao<Departamento>
    {
        Departamento obtenerPorId(DepartamentoPk pk);
        List<Departamento> listar(FiltroDepartamento filtro);
    }
}
