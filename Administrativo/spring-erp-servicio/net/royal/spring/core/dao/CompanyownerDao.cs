using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.dao
{

    public interface CompanyownerDao : GenericoDao<Companyowner>
    {
        Companyowner obtenerPorId(CompanyownerPk pk);
        string obtenerNombre(string companyowner);
        List<DtoTabla> listarActivos();
        List<Companyowner> listarTodos();
        List<DtoTabla> listarCompaniasPorSeguridad(String usuario);
    }
}
