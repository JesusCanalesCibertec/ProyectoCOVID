using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema.servicio.impl
{

public class SeguridadautorizacionesServicioImpl : GenericoServicioImpl, SeguridadautorizacionesServicio {

        private IServiceProvider servicioProveedor;
        private SeguridadautorizacionesDao seguridadautorizacionesDao;

        public SeguridadautorizacionesServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            seguridadautorizacionesDao = servicioProveedor.GetService<SeguridadautorizacionesDao>();
        }

        public List<DtoTabla> listarAplicacionPorUsuario(string idUsuario)
        {
            return seguridadautorizacionesDao.listarAplicacionPorUsuario(idUsuario);
        }

        public List<Seguridadgrupo> listarPorAplicacionUsuario(string idAplicacion, string idUsuario)
        {
            return seguridadautorizacionesDao.listarPorAplicacionUsuario(idAplicacion, idUsuario);
        }
        public string esRRHH(string idUsuario)
        {
            return seguridadautorizacionesDao.esRRHH(idUsuario);
        }

    }
}
