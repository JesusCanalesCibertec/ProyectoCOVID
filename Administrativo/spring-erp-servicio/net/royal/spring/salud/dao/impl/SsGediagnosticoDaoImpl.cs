using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.salud.dominio;
using net.royal.spring.salud.dominio.dto;

namespace net.royal.spring.salud.dao.impl
{
    public class SsGediagnosticoDaoImpl : GenericoDaoImpl<SsGediagnostico>, SsGediagnosticoDao
    {
        private IServiceProvider servicioProveedor;


        public SsGediagnosticoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "ssgediagnostico")
        {
            servicioProveedor = _servicioProveedor;
        }

        public SsGediagnostico obtenerPorId(SsGediagnosticoPk id)
        {

            return base.obtenerPorId(id.obtenerArreglo());
        }

        public SsGediagnostico obtenerPorId(int codigo)
        {
            return obtenerPorId(new SsGediagnosticoPk(codigo));
        }

        public int generarCodigo()
        {
            IQueryable<SsGediagnostico> query = this.getCriteria();

            List<SsGediagnostico> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.IdDiagnostico));
            }

            return var + 1;
        }

        public SsGediagnostico obtenerNombrePorCodigo(string codigo)
        {
            IQueryable<SsGediagnostico> query = this.getCriteria();
            query = query.Where(p => p.CodigoDiagnostic == codigo);

            List<SsGediagnostico> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0];
            return null;
        }

        public string obtenercadena(string cadena)
        {
            IQueryable<SsGediagnostico> query = this.getCriteria();
            query = query.Where(p => p.Nombre == cadena);

            List<SsGediagnostico> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public SsGediagnostico coreInsertar(UsuarioActual usuarioActual, SsGediagnostico bean, Int32? estado)
        {
            if (UInteger.esCeroOrNulo(bean.Estado))
                bean.Estado = 2;
            bean.IdDiagnostico = generarCodigo();
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public SsGediagnostico coreActualizar(UsuarioActual usuarioActual, SsGediagnostico bean, Int32? estado)
        {
            if (UInteger.esCeroOrNulo(estado))
            {
                estado = 2;
            }
            bean.Estado = estado;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> codigo)
        {
            coreEliminar(new SsGediagnosticoPk(codigo));
        }

        public void coreEliminar(SsGediagnosticoPk id)
        {
            SsGediagnostico bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }




        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroDiagnostico filtro)
        {
            Int32 contador = 0;

            List<DtoDiagnostico> lstRetorno = new List<DtoDiagnostico>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UInteger.esCeroOrNulo(filtro.estado))
                filtro.estado = null;


            parametros.Add(new ParametroPersistenciaGenerico("p_id_padre", ConstanteUtil.TipoDato.String, filtro.codigopadre));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_diagnostico", ConstanteUtil.TipoDato.String, filtro.codigodiagnostico));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.Integer, filtro.estado));
            parametros.Add(new ParametroPersistenciaGenerico("p_idCapitulo", ConstanteUtil.TipoDato.String, filtro.IdCapitulo));
            parametros.Add(new ParametroPersistenciaGenerico("p_idGrupo", ConstanteUtil.TipoDato.String, filtro.IdGrupo));

            contador = this.contar("ssgediagnostico.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "ssgediagnostico.filtroPaginacion");

            while (_reader.Read())
            {
                DtoDiagnostico bean = new DtoDiagnostico();

                if (!_reader.IsDBNull(0))
                    bean.codigopadre = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.codigodiagnostico = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.nombre = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.estado = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.fechaReg = _reader.GetDateTime(4);

                if (!_reader.IsDBNull(5))
                    bean.idDiagnostico = _reader.GetInt32(5);

                if (!_reader.IsDBNull(6))
                    bean.capitulo = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.grupo = _reader.GetString(7);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public SsGediagnostico registrarCurso(UsuarioActual usuarioActual, SsGediagnostico bean)
        {
            bean.IdDiagnostico = generarCodigo();
            return coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public List<SsGediagnostico> listarActivosFlgCronicos()
        {
            IQueryable<SsGediagnostico> query = this.getCriteria();
            query = query.Where(p => p.FlgCronico == "S");
            query = query.Where(p => p.Estado == 2);

            return query.ToList();
        }

        public SsGediagnostico coreAnular(UsuarioActual usuarioActual, SsGediagnosticoPk id)
        {
            SsGediagnostico bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, 1);
        }

        public SsGediagnostico coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdDiagnostico)
        {
            return coreAnular(usuarioActual, new SsGediagnosticoPk(pIdDiagnostico));
        }
    }
}
