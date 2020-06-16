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
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.programasocial.dao.impl {
    public class PsInstitucionDaoImpl : GenericoDaoImpl<PsInstitucion>, PsInstitucionDao {
        private IServiceProvider servicioProveedor;


        public PsInstitucionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psinstitucion") {
            servicioProveedor = _servicioProveedor;
        }

        public PsInstitucion obtenerPorId(PsInstitucionPk id) {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsInstitucion obtenerPorId(string codigo) {
            return obtenerPorId(new PsInstitucionPk(codigo));
        }

        public PsInstitucion coreInsertar(UsuarioActual usuarioActual, PsInstitucion bean, String estado) {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsInstitucion coreActualizar(UsuarioActual usuarioActual, PsInstitucion bean, String estado) {
            if (UString.estaVacio(estado))
            {
                estado = ConstanteEstadoGenerico.ACTIVO;
            }
            bean.Estado = estado;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion) {
            coreEliminar(new PsInstitucionPk(pIdInstitucion));
        }

        public void coreEliminar(PsInstitucionPk id) {
            PsInstitucion bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsInstitucion coreAnular(UsuarioActual usuarioActual, PsInstitucionPk id) {
            PsInstitucion bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PsInstitucion coreAnular(UsuarioActual usuarioActual, String pIdInstitucion) {
            return coreAnular(usuarioActual, new PsInstitucionPk(pIdInstitucion));
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroInstitucion filtro) {
            Int32 contador = 0;

            List<DtoInstitucion> lstRetorno = new List<DtoInstitucion>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();



            if (UString.estaVacio(filtro.estado)) {
                filtro.estado = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.estado));


            contador = this.contar("psinstitucion.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "psinstitucion.filtroPaginacion");

            while (_reader.Read()) {
                DtoInstitucion bean = new DtoInstitucion();

                if (!_reader.IsDBNull(0))
                    bean.codigo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nombre = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.estado = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.fecha_registro = _reader.GetDateTime(3);



                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }


        public string obtenercadena(string cadena) {
            IQueryable<PsInstitucion> query = this.getCriteria();
            query = query.Where(p => p.Nombre == cadena);

            List<PsInstitucion> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string codigo) {
            IQueryable<PsInstitucion> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == codigo);

            List<PsInstitucion> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdInstitucion;
            return null;
        }

        public string flgSeleccionarInstitucion(UsuarioActual usuarioActual) {
            String retorno;
            if (usuarioActual.Origen=="fundacion") {
                retorno = "S";
            }
            else {
                retorno = "N";
            }
            return retorno;
        }

        public List<DtoTabla> listarPeriodos(string Institucion, String IdPrograma, string IdComponente, String IdUsuario, Nullable<Int32> IdBeneficiario) {

            List<DtoTabla> listarPeriodos = new List<DtoTabla>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, Institucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_idPrograma", ConstanteUtil.TipoDato.String, IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_idComponente", ConstanteUtil.TipoDato.String, IdComponente));
            parametros.Add(new ParametroPersistenciaGenerico("p_idUsuario", ConstanteUtil.TipoDato.String, IdUsuario));
            parametros.Add(new ParametroPersistenciaGenerico("p_idBeneficiario", ConstanteUtil.TipoDato.Integer, IdBeneficiario));

            _reader = this.listarPorQuery("psinstitucion.listarPeriodo", parametros);

            while (_reader.Read()) {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);


                listarPeriodos.Add(bean);
            }
            this.dispose();


            return listarPeriodos;
        }

        public List<PsInstitucion> listarPorPks(List<string> ids) {
            IQueryable<PsInstitucion> query = getCriteria();
            query = query.Where(x => ids.Find(y => y == x.IdInstitucion) != null);
            return query.ToList();
        }

        public List<PsInstitucion> listarTodosActivas() {
            IQueryable<PsInstitucion> query = getCriteria();

            query = query.Where(x => x.Estado == "A");

            return query.ToList();
        }

        public void eliminarAreas(string codigo)
        {

            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();



            _parametros.Add(new ParametroPersistenciaGenerico("p_idInstitucion", ConstanteUtil.TipoDato.String, codigo));
            


            this.ejecutarPorQuery("psinstitucion.eliminardetalle", _parametros);


        }
    }
}
