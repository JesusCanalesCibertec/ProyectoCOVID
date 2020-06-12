using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core.dao
{

    public interface CompanyownerImagenesDao : GenericoDao<CompanyownerImagenes>
    {
        CompanyownerImagenes obtenerPorId(CompanyownerImagenesPk pk);
    }
}
