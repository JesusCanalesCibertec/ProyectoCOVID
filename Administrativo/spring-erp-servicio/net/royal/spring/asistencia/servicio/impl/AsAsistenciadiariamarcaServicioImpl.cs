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

public class AsAsistenciadiariamarcaServicioImpl : GenericoServicioImpl, AsAsistenciadiariamarcaServicio {

        private IServiceProvider servicioProveedor;
        private AsAsistenciadiariamarcaDao asAsistenciadiariamarcaDao;

        public AsAsistenciadiariamarcaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            asAsistenciadiariamarcaDao = servicioProveedor.GetService<AsAsistenciadiariamarcaDao>();
}

}
}
