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

public class SySeguridadautorizacionesServicioImpl : GenericoServicioImpl, SySeguridadautorizacionesServicio {

        private IServiceProvider servicioProveedor;
        private SySeguridadautorizacionesDao sySeguridadautorizacionesDao;

        public SySeguridadautorizacionesServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            sySeguridadautorizacionesDao = servicioProveedor.GetService<SySeguridadautorizacionesDao>();
        }

        public List<DtoTabla> listarEmpresasPorUsuario(string idUsuario)
        {
            return sySeguridadautorizacionesDao.listarEmpresasPorUsuario(idUsuario);
        }

        public List<DtoTabla> listarEmpresasPorUsuarioEnEmpleadoYSeguridadAutorizacion(String idUsuario) {
            return sySeguridadautorizacionesDao.listarEmpresasPorUsuarioEnEmpleadoYSeguridadAutorizacion(idUsuario);
        }

        public List<DtoTabla> listarPorGrupoYUsuario(String idUsuario)
        {
            return sySeguridadautorizacionesDao.listarPorGrupoYUsuario(idUsuario);
        }
    }
}
