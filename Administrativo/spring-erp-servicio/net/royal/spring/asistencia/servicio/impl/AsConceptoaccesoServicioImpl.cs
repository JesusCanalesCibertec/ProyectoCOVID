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

public class AsConceptoaccesoServicioImpl : GenericoServicioImpl, AsConceptoaccesoServicio {

        private IServiceProvider servicioProveedor;
        private AsConceptoaccesoDao asConceptoaccesoDao;

        public AsConceptoaccesoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            asConceptoaccesoDao = servicioProveedor.GetService<AsConceptoaccesoDao>();
}

        public List<AsConceptoacceso> ListarActivos()
        {
            return asConceptoaccesoDao.ListarActivos();
        }

        public List<AsConceptoacceso> ListarActivos(string tipo)
        {
            throw new NotImplementedException();
        }

        public List<AsConceptoacceso> ListarActivosOtros(bool web)
        {
            return asConceptoaccesoDao.ListarActivosOtros(web);
        }

        public AsConceptoacceso obtenerPorId(AsConceptoaccesoPk pk)
        {
            return asConceptoaccesoDao.obtenerPorId(pk);
        }
    }
}
