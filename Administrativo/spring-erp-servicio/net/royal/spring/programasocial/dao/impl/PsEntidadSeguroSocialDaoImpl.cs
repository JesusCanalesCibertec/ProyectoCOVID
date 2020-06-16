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
    public class PsEntidadSeguroSocialDaoImpl : GenericoDaoImpl<PsEntidadSeguroSocial>, PsEntidadSeguroSocialDao
    {
        private IServiceProvider servicioProveedor;


        public PsEntidadSeguroSocialDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psentidadsegurosocial")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PsEntidadSeguroSocial obtenerPorId(PsEntidadSeguroSocialPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsEntidadSeguroSocial obtenerPorId(Nullable<Int32> pIdEntidad, String pIdSeguroSocial)
        {
            return obtenerPorId(new PsEntidadSeguroSocialPk(pIdEntidad, pIdSeguroSocial));
        }

        public PsEntidadSeguroSocial coreInsertar(UsuarioActual usuarioActual, PsEntidadSeguroSocial bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsEntidadSeguroSocial coreActualizar(UsuarioActual usuarioActual, PsEntidadSeguroSocial bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad, String pIdSeguroSocial)
        {
            coreEliminar(new PsEntidadSeguroSocialPk(pIdEntidad, pIdSeguroSocial));
        }

        public void coreEliminar(PsEntidadSeguroSocialPk id)
        {
            PsEntidadSeguroSocial bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsEntidadSeguroSocial coreAnular(UsuarioActual usuarioActual, PsEntidadSeguroSocialPk id)
        {
            PsEntidadSeguroSocial bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public PsEntidadSeguroSocial coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdEntidad, String pIdSeguroSocial)
        {
            return coreAnular(usuarioActual, new PsEntidadSeguroSocialPk(pIdEntidad, pIdSeguroSocial));
        }

        public List<PsEntidadSeguroSocial> listarBeneficiario(string institucion, int idBeneficiario)
        {
            IQueryable<PsEntidadSeguroSocial> query = getCriteria();
            query = query.Where(x => x.IdEntidad == idBeneficiario);
            return query.ToList();

        }
    }
}
