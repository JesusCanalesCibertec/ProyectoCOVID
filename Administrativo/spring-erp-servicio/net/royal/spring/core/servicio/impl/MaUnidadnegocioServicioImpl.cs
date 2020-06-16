using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using System.Linq;

namespace net.royal.spring.core.servicio.impl
{

public class MaUnidadnegocioServicioImpl : GenericoServicioImpl, MaUnidadnegocioServicio {

        private IServiceProvider servicioProveedor;
        private MaUnidadnegocioDao maUnidadnegocioDao;

        public MaUnidadnegocioServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            maUnidadnegocioDao = servicioProveedor.GetService<MaUnidadnegocioDao>();
}

        public List<MaUnidadnegocio> listarActivos()
        {
            return maUnidadnegocioDao.listarActivos();
        }

        public List<MaUnidadnegocio> listarTodos()
        {
            return maUnidadnegocioDao.listarTodos();
        }
    }
}
