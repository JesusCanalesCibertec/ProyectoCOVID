using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio
{

public interface CompanyownerServicio : GenericoServicio {
        List<Companyowner> listarTodos();
        string obtenerNombre(string companyowner);
        List<DtoTabla> listarActivos();
        List<DtoTabla> listarCompaniasPorSeguridad(String usuario);
        
    }
}
