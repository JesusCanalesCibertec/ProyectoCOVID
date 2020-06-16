using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;

namespace net.royal.spring.core.servicio
{

    public interface DepartamentoServicio : GenericoServicio
    {
        List<Departamento> listarTodos();

        List<Departamento> listar(FiltroDepartamento filtro);

        List<Departamento> listarActivosPorPais(String idPais);

        Departamento obtenerPorId(DepartamentoPk pk);
    }
}
