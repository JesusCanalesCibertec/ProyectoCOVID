using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.programasocial.servicio.impl
{

public class PsEmpleadoServicioImpl : GenericoServicioImpl, PsEmpleadoServicio {

        private IServiceProvider servicioProveedor;
        private PsEmpleadoDao psEmpleadoDao;

        public PsEmpleadoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psEmpleadoDao = servicioProveedor.GetService<PsEmpleadoDao>();
        }

        public PsEmpleado coreInsertar(UsuarioActual usuarioActual, PsEmpleado bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return psEmpleadoDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public PsEmpleado coreActualizar(UsuarioActual usuarioActual, PsEmpleado bean)
        {
            return psEmpleadoDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public PsEmpleado coreAnular(UsuarioActual usuarioActual, PsEmpleadoPk id)
        {
            return psEmpleadoDao.coreAnular(usuarioActual,id);
        }

        public PsEmpleado coreAnular(UsuarioActual usuarioActual, String pIdInstitucion,Nullable<Int32> pIdEmpleado)
        {
            return psEmpleadoDao.coreAnular(usuarioActual,  pIdInstitucion, pIdEmpleado);
        }

        public void coreEliminar(PsEmpleadoPk id)
        {
            psEmpleadoDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdEmpleado)
        {
            psEmpleadoDao.coreEliminar( pIdInstitucion, pIdEmpleado);
        }


        public PsEmpleado obtenerPorId(PsEmpleadoPk id)
        {
            return psEmpleadoDao.obtenerPorId(id);
        }

        public PsEmpleado obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdEmpleado)
        {
            return psEmpleadoDao.obtenerPorId( pIdInstitucion, pIdEmpleado);
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPsEmpleado filtro)
        {
            return psEmpleadoDao.listarPaginacion(paginacion, filtro);
        }

        public IList<DtoTabla> listarAreas()
        {
            return psEmpleadoDao.listarAreas();
        }
    }
}
