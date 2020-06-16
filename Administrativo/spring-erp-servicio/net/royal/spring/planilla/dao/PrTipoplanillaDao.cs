using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.planilla.dominio;

namespace net.royal.spring.planilla.dao
{

    public interface PrTipoplanillaDao : GenericoDao<PrTipoplanilla>
    {
        PrTipoplanilla obtenerPorId(PrTipoplanillaPk pk);
    }
}
