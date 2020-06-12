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

namespace net.royal.spring.programasocial.dao.impl {
    public class PsSaludBiomecanicaDaoImpl : GenericoDaoImpl<PsSaludBiomecanica>, PsSaludBiomecanicaDao {
        private IServiceProvider servicioProveedor;


        public PsSaludBiomecanicaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pssaludbiomecanica") {
            servicioProveedor = _servicioProveedor;
        }

        public PsSaludBiomecanica obtenerPorId(PsSaludBiomecanicaPk id) {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsSaludBiomecanica obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud, String pIdTipoAyuda) {
            return obtenerPorId(new PsSaludBiomecanicaPk(pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTipoAyuda));
        }

        public PsSaludBiomecanica coreInsertar(UsuarioActual usuarioActual, PsSaludBiomecanica bean) {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsSaludBiomecanica coreActualizar(UsuarioActual usuarioActual, PsSaludBiomecanica bean) {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud, String pIdTipoAyuda) {
            coreEliminar(new PsSaludBiomecanicaPk(pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTipoAyuda));
        }

        public void coreEliminar(PsSaludBiomecanicaPk id) {
            PsSaludBiomecanica bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsSaludBiomecanica coreAnular(UsuarioActual usuarioActual, PsSaludBiomecanicaPk id) {
            PsSaludBiomecanica bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public PsSaludBiomecanica coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud, String pIdTipoAyuda) {
            return coreAnular(usuarioActual, new PsSaludBiomecanicaPk(pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTipoAyuda));
        }

        public List<PsSaludBiomecanica> obtenerListado(string pIdInstitucion, int? pIdBeneficiario, int? pIdSalud) {
            IQueryable<PsSaludBiomecanica> queryable = getCriteria();
            queryable = queryable.Where(x => x.IdBeneficiario == pIdBeneficiario);
            queryable = queryable.Where(x => x.IdInstitucion == pIdInstitucion);
            queryable = queryable.Where(x => x.IdSalud == pIdSalud);

            return queryable.ToList();
        }
    }
}
