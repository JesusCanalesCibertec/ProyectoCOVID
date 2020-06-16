using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.asistencia.dao
{

    public interface AsAsistenciadiariaDao : GenericoDao<AsAsistenciadiaria>
    {
        AsAsistenciadiaria obtenerPorId(AsAsistenciadiariaPk pk);
    }
}
