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
    public class PsCapacidadCursoDaoImpl : GenericoDaoImpl<PsCapacidadCurso>, PsCapacidadCursoDao {
        private IServiceProvider servicioProveedor;


        public PsCapacidadCursoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pscapacidadcurso") {
            servicioProveedor = _servicioProveedor;
        }

        public PsCapacidadCurso obtenerPorId(PsCapacidadCursoPk id) {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsCapacidadCurso obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad, String pIdCurso) {
            return obtenerPorId(new PsCapacidadCursoPk(pIdInstitucion, pIdBeneficiario, pIdCapacidad, pIdCurso));
        }

        public PsCapacidadCurso coreInsertar(UsuarioActual usuarioActual, PsCapacidadCurso bean) {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsCapacidadCurso coreActualizar(UsuarioActual usuarioActual, PsCapacidadCurso bean) {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad, String pIdCurso) {
            coreEliminar(new PsCapacidadCursoPk(pIdInstitucion, pIdBeneficiario, pIdCapacidad, pIdCurso));
        }

        public void coreEliminar(PsCapacidadCursoPk id) {
            PsCapacidadCurso bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsCapacidadCurso coreAnular(UsuarioActual usuarioActual, PsCapacidadCursoPk id) {
            PsCapacidadCurso bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public PsCapacidadCurso coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad, String pIdCurso) {
            return coreAnular(usuarioActual, new PsCapacidadCursoPk(pIdInstitucion, pIdBeneficiario, pIdCapacidad, pIdCurso));
        }

        public List<PsCapacidadCurso> obtenerLista(string pIdInstitucion, int? pIdBeneficiario, int? pIdCapacidad) {
            IQueryable<PsCapacidadCurso> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == pIdInstitucion);
            query = query.Where(p => p.IdBeneficiario == pIdBeneficiario);
            query = query.Where(p => p.IdCapacidad == pIdCapacidad);

            return  query.ToList();
        }
    }
}
