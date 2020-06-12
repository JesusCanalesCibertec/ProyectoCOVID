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
    public class PsEntidadProgramaSocialDaoImpl : GenericoDaoImpl<PsEntidadProgramaSocial>, PsEntidadProgramaSocialDao
    {
        private IServiceProvider servicioProveedor;


        public PsEntidadProgramaSocialDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psentidadprogramasocial")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PsEntidadProgramaSocial obtenerPorId(PsEntidadProgramaSocialPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsEntidadProgramaSocial obtenerPorId(Nullable<Int32> pIdEntidad, String pIdProgramaSocial)
        {
            return obtenerPorId(new PsEntidadProgramaSocialPk(pIdEntidad, pIdProgramaSocial));
        }

        public PsEntidadProgramaSocial coreInsertar(UsuarioActual usuarioActual, PsEntidadProgramaSocial bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsEntidadProgramaSocial coreActualizar(UsuarioActual usuarioActual, PsEntidadProgramaSocial bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad, String pIdProgramaSocial)
        {
            coreEliminar(new PsEntidadProgramaSocialPk(pIdEntidad, pIdProgramaSocial));
        }

        public void coreEliminar(PsEntidadProgramaSocialPk id)
        {
            PsEntidadProgramaSocial bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsEntidadProgramaSocial coreAnular(UsuarioActual usuarioActual, PsEntidadProgramaSocialPk id)
        {
            PsEntidadProgramaSocial bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public PsEntidadProgramaSocial coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdEntidad, String pIdProgramaSocial)
        {
            return coreAnular(usuarioActual, new PsEntidadProgramaSocialPk(pIdEntidad, pIdProgramaSocial));
        }

        public List<PsEntidadProgramaSocial> listarBeneficiario(string institucion, int idBeneficiario)
        {
            IQueryable<PsEntidadProgramaSocial> query = getCriteria();
            query = query.Where(x => x.IdEntidad == idBeneficiario);
            return query.ToList();

        }
    }
}
