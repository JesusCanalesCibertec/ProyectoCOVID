using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;

namespace net.royal.spring.core.servicio
{

public interface ProvinciaServicio : GenericoServicio {
        List<Provincia> listarTodos();
        List<Provincia> listar(FiltroProvincia filtro);
        List<Provincia> listarActivosPorDepartamento(String idDepartamento);
        String obtenerNombrePorId(String departamento,String provincia);
    }
}
