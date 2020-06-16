using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;

namespace net.royal.spring.core.servicio
{

public interface ZonapostalServicio : GenericoServicio {
        List<Zonapostal> listarTodos();
        List<Zonapostal> listar(FiltroZonaPostal filtro);
        List<Zonapostal> listarActivosPorProvincia(String idDepartamento, String idProvincia);

        String obtenerNombrePorId(String departamento,String provincia,String distrito);
    }
}
