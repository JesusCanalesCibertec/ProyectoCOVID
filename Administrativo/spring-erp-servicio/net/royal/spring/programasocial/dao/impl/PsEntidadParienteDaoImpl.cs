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
    public class PsEntidadParienteDaoImpl : GenericoDaoImpl<PsEntidadPariente>, PsEntidadParienteDao
    {
        private IServiceProvider servicioProveedor;


        public PsEntidadParienteDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psentidadpariente")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PsEntidadPariente obtenerPorId(PsEntidadParientePk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsEntidadPariente obtenerPorId(Nullable<Int32> pIdEntidad, Nullable<Int32> pIdPariente)
        {
            return obtenerPorId(new PsEntidadParientePk(pIdEntidad, pIdPariente));
        }

        public PsEntidadPariente coreInsertar(UsuarioActual usuarioActual, PsEntidadPariente bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsEntidadPariente coreActualizar(UsuarioActual usuarioActual, PsEntidadPariente bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad, Nullable<Int32> pIdPariente)
        {
            coreEliminar(new PsEntidadParientePk(pIdEntidad, pIdPariente));
        }

        public void coreEliminar(PsEntidadParientePk id)
        {
            PsEntidadPariente bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsEntidadPariente coreAnular(UsuarioActual usuarioActual, PsEntidadParientePk id)
        {
            PsEntidadPariente bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public PsEntidadPariente coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdEntidad, Nullable<Int32> pIdPariente)
        {
            return coreAnular(usuarioActual, new PsEntidadParientePk(pIdEntidad, pIdPariente));
        }

        public List<PsEntidadPariente> listarBeneficiario(string institucion, int idBeneficiario)
        {
            IQueryable<PsEntidadPariente> query = getCriteria();
            query = query.Where(x => x.IdEntidad == idBeneficiario);
            return query.ToList();

        }
    }
}
