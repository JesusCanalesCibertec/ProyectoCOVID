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

    public class AsTipohorarioServicioImpl : GenericoServicioImpl, AsTipohorarioServicio
    {

        private IServiceProvider servicioProveedor;
        private AsTipohorarioDao asTipohorarioDao;

        public AsTipohorarioServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            asTipohorarioDao = servicioProveedor.GetService<AsTipohorarioDao>();
        }

        public string[] listarHorasSemana(int? tipohorario)
        {
            return asTipohorarioDao.listarHorasSemana(tipohorario);
        }

        public List<AsTipohorario> listarTodos()
        {
            return asTipohorarioDao.listarTodos();
        }

        public AsTipohorario obtenerPorId(AsTipohorarioPk pk)
        {
            return asTipohorarioDao.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
