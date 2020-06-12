using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.kpi.dao;
using net.royal.spring.kpi.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;

namespace net.royal.spring.kpi.dao.impl
{
    public class RtIndicadorDaoImpl : GenericoDaoImpl<RtIndicador>, RtIndicadorDao
    {
        private IServiceProvider servicioProveedor;


        public RtIndicadorDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "rtindicador")
        {
            servicioProveedor = _servicioProveedor;
        }

        public RtIndicador obtenerPorId(RtIndicadorPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public RtIndicador obtenerPorId(String pIdIndicador)
        {
            return obtenerPorId(new RtIndicadorPk(pIdIndicador));
        }

        public RtIndicador coreInsertar(UsuarioActual usuarioActual, RtIndicador bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public RtIndicador coreActualizar(UsuarioActual usuarioActual, RtIndicador bean, String estado)
        {
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

        public void coreEliminar(String pIdIndicador)
        {
            coreEliminar(new RtIndicadorPk(pIdIndicador));
        }

        public void coreEliminar(RtIndicadorPk id)
        {
            RtIndicador bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public RtIndicador coreAnular(UsuarioActual usuarioActual, RtIndicadorPk id)
        {
            RtIndicador bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public RtIndicador coreAnular(UsuarioActual usuarioActual, String pIdIndicador)
        {
            return coreAnular(usuarioActual, new RtIndicadorPk(pIdIndicador));
        }



        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroIndicador filtro)
        {
            Int32 contador = 0;

            List<DtoIndicador> lstRetorno = new List<DtoIndicador>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();



            if (UString.estaVacio(filtro.estado))
            {
                filtro.estado = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.estado));


            contador = this.contar("rtindicador.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "rtindicador.filtroPaginacion");

            while (_reader.Read())
            {
                DtoIndicador bean = new DtoIndicador();

                if (!_reader.IsDBNull(0))
                    bean.codigo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nombre = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.nom_programa = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.nom_componente = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.estado = _reader.GetString(4);


                if (!_reader.IsDBNull(5))
                    bean.estado2 = _reader.GetString(5);
                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }


        public string obtenercadena(string cadena)
        {
            IQueryable<RtIndicador> query = this.getCriteria();
            query = query.Where(p => p.Nombre == cadena);

            List<RtIndicador> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string codigo)
        {
            IQueryable<RtIndicador> query = this.getCriteria();
            query = query.Where(p => p.IdIndicador == codigo);

            List<RtIndicador> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdIndicador;
            return null;
        }

        public List<RtIndicador> listarPorPrograma(string programa)
        {
            IQueryable<RtIndicador> query = this.getCriteria();
            if (!UString.estaVacio(programa) && programa != "undefined")
            {
                query = query.Where(p => p.IdPrograma == programa);
            }
            return query.ToList();
        }
    }
}
