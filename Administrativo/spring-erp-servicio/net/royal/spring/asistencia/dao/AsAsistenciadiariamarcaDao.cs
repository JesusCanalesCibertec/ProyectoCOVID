using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.asistencia.dao
{

    public interface AsAsistenciadiariamarcaDao : GenericoDao<AsAsistenciadiariamarca>
    {
        AsAsistenciadiariamarca obtenerPorId(AsAsistenciadiariamarcaPk pk);
    }
}
