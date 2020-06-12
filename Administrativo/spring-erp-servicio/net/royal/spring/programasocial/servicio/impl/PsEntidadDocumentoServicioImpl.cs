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

namespace net.royal.spring.programasocial.servicio.impl
{

public class PsEntidadDocumentoServicioImpl : GenericoServicioImpl, PsEntidadDocumentoServicio {

        private IServiceProvider servicioProveedor;
        private PsEntidadDocumentoDao psEntidadDocumentoDao;

        public PsEntidadDocumentoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psEntidadDocumentoDao = servicioProveedor.GetService<PsEntidadDocumentoDao>();
        }

        public PsEntidadDocumento coreInsertar(UsuarioActual usuarioActual, PsEntidadDocumento bean)
        {
            return psEntidadDocumentoDao.coreInsertar(usuarioActual,bean);
        }

        public PsEntidadDocumento coreActualizar(UsuarioActual usuarioActual, PsEntidadDocumento bean)
        {
            return psEntidadDocumentoDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsEntidadDocumentoPk id)
        {
            psEntidadDocumentoDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad,String pIdTipoDocumento)
        {
            psEntidadDocumentoDao.coreEliminar( pIdEntidad, pIdTipoDocumento);
        }


        public PsEntidadDocumento obtenerPorId(PsEntidadDocumentoPk id)
        {
            return psEntidadDocumentoDao.obtenerPorId(id);
        }

        public PsEntidadDocumento obtenerPorId(Nullable<Int32> pIdEntidad,String pIdTipoDocumento)
        {
            return psEntidadDocumentoDao.obtenerPorId( pIdEntidad, pIdTipoDocumento);
        }

    }
}
