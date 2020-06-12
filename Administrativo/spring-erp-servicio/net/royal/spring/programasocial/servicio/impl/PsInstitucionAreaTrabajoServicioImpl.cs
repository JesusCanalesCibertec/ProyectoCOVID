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

namespace net.royal.spring.programasocial.servicio.impl {

    public class PsInstitucionAreaTrabajoServicioImpl : GenericoServicioImpl, PsInstitucionAreaTrabajoServicio {

        private IServiceProvider servicioProveedor;
        private PsInstitucionAreaTrabajoDao psInstitucionAreaTrabajoDao;

        public PsInstitucionAreaTrabajoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psInstitucionAreaTrabajoDao = servicioProveedor.GetService<PsInstitucionAreaTrabajoDao>();
        }

        public PsInstitucionAreaTrabajo coreInsertar(UsuarioActual usuarioActual, PsInstitucionAreaTrabajo bean) {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return psInstitucionAreaTrabajoDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public PsInstitucionAreaTrabajo coreActualizar(UsuarioActual usuarioActual, PsInstitucionAreaTrabajo bean) {
            return psInstitucionAreaTrabajoDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public PsInstitucionAreaTrabajo coreAnular(UsuarioActual usuarioActual, PsInstitucionAreaTrabajoPk id) {
            return psInstitucionAreaTrabajoDao.coreAnular(usuarioActual, id);
        }


        public void coreEliminar(PsInstitucionAreaTrabajoPk id) {
            psInstitucionAreaTrabajoDao.coreEliminar(id);
        }

        public void coreEliminar() {
            psInstitucionAreaTrabajoDao.coreEliminar();
        }


        public PsInstitucionAreaTrabajo obtenerPorId(PsInstitucionAreaTrabajoPk id) {
            return psInstitucionAreaTrabajoDao.obtenerPorId(id);
        }

        public PsInstitucionAreaTrabajo obtenerPorId() {
            return psInstitucionAreaTrabajoDao.obtenerPorId();
        }

        public List<PsInstitucionAreaTrabajo> listarActivos() {
            return psInstitucionAreaTrabajoDao.listarActivos();
        }
    }
}
