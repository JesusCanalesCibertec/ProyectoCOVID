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

    public class PsSaludDiscapacidadServicioImpl : GenericoServicioImpl, PsSaludDiscapacidadServicio {

        private IServiceProvider servicioProveedor;
        private PsSaludDiscapacidadDao psSaludDiscapacidadDao;

        public PsSaludDiscapacidadServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psSaludDiscapacidadDao = servicioProveedor.GetService<PsSaludDiscapacidadDao>();
        }

        public PsSaludDiscapacidad coreInsertar(UsuarioActual usuarioActual, PsSaludDiscapacidad bean) {
            return psSaludDiscapacidadDao.coreInsertar(usuarioActual, bean);
        }

        public PsSaludDiscapacidad coreActualizar(UsuarioActual usuarioActual, PsSaludDiscapacidad bean) {
            return psSaludDiscapacidadDao.coreActualizar(usuarioActual, bean);
        }


        public void coreEliminar(PsSaludDiscapacidadPk id) {
            psSaludDiscapacidadDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud, String pIdEstado) {
            psSaludDiscapacidadDao.coreEliminar(pIdInstitucion, pIdBeneficiario, pIdSalud, pIdEstado);
        }


        public PsSaludDiscapacidad obtenerPorId(PsSaludDiscapacidadPk id) {
            return psSaludDiscapacidadDao.obtenerPorId(id);
        }

        public PsSaludDiscapacidad obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud, String pIdEstado) {
            return psSaludDiscapacidadDao.obtenerPorId(pIdInstitucion, pIdBeneficiario, pIdSalud, pIdEstado);
        }

    }
}
