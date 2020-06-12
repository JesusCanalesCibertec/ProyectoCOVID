using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core.dao
{

public interface CompanyownerrecursoDao : GenericoDao<Companyownerrecurso>
{
        Companyownerrecurso obtenerPorId(CompanyownerrecursoPk pk);
        IList<Companyownerrecurso> listarPorRecurso(string tipoRecurso);
        void eliminarPorTipoRecurso(string tipoRecurso);
    }
}
