using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.planilla.dominio;

namespace net.royal.spring.planilla.dao
{

    public interface PrTipoprestamoDao : GenericoDao<PrTipoprestamo>
    {
        PrTipoprestamo obtenerPorId(PrTipoprestamoPk pk);
        List<PrTipoprestamo> listarActivos();
        List<PrTipoprestamo> listarSoloWeb();
    }
}
