using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using System.Collections.Generic;

namespace net.royal.spring.programasocial.dao.impl
{
    public class PsEntidadDocumentoDaoImpl : GenericoDaoImpl<PsEntidadDocumento>, PsEntidadDocumentoDao
    {
        private IServiceProvider servicioProveedor;


        public PsEntidadDocumentoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psentidaddocumento")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PsEntidadDocumento obtenerPorId(PsEntidadDocumentoPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsEntidadDocumento obtenerPorId(Nullable<Int32> pIdEntidad, String pIdTipoDocumento)
        {
            return obtenerPorId(new PsEntidadDocumentoPk(pIdEntidad, pIdTipoDocumento));
        }

        public PsEntidadDocumento coreInsertar(UsuarioActual usuarioActual, PsEntidadDocumento bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsEntidadDocumento coreActualizar(UsuarioActual usuarioActual, PsEntidadDocumento bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad, String pIdTipoDocumento)
        {
            coreEliminar(new PsEntidadDocumentoPk(pIdEntidad, pIdTipoDocumento));
        }

        public void coreEliminar(PsEntidadDocumentoPk id)
        {
            PsEntidadDocumento bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsEntidadDocumento coreAnular(UsuarioActual usuarioActual, PsEntidadDocumentoPk id)
        {
            PsEntidadDocumento bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public PsEntidadDocumento coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdEntidad, String pIdTipoDocumento)
        {
            return coreAnular(usuarioActual, new PsEntidadDocumentoPk(pIdEntidad, pIdTipoDocumento));
        }

        public List<PsEntidadDocumento> listarBeneficiario(string institucion, int idBeneficiario)
        {
            IQueryable<PsEntidadDocumento> query = getCriteria();
            query = query.Where(x => x.IdEntidad == idBeneficiario);
            return query.ToList();
        }
    }
}
