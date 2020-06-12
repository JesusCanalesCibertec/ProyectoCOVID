using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.asistencia.servicio.impl
{

public class AsAsistenciadiariaServicioImpl : GenericoServicioImpl, AsAsistenciadiariaServicio {

        private IServiceProvider servicioProveedor;
        private AsAsistenciadiariaDao asAsistenciadiariaDao;

        public AsAsistenciadiariaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            asAsistenciadiariaDao = servicioProveedor.GetService<AsAsistenciadiariaDao>();
}

}
}
