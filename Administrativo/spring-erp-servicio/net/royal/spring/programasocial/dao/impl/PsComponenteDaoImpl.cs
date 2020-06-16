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

namespace net.royal.spring.programasocial.dao.impl
{
public class PsComponenteDaoImpl : GenericoDaoImpl<PsComponente>, PsComponenteDao
{
        private IServiceProvider servicioProveedor;


        public PsComponenteDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"pscomponente")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsComponente obtenerPorId(PsComponentePk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsComponente obtenerPorId(String pIdComponente)
        {
            return obtenerPorId(new PsComponentePk( pIdComponente));
        }

        public PsComponente coreInsertar(UsuarioActual usuarioActual, PsComponente bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsComponente coreActualizar(UsuarioActual usuarioActual, PsComponente bean, String estado)
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

        public void coreEliminar(String pIdComponente)
        {
            coreEliminar(new PsComponentePk( pIdComponente));
        }

        public void coreEliminar(PsComponentePk id)
        {
            PsComponente bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsComponente coreAnular(UsuarioActual usuarioActual,PsComponentePk id)
        {
            PsComponente bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public PsComponente coreAnular(UsuarioActual usuarioActual,String pIdComponente)
        {
            return coreAnular(usuarioActual,new PsComponentePk( pIdComponente));
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroComponente filtro)
        {
            Int32 contador = 0;

            List<PsComponente> lstRetorno = new List<PsComponente>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();



            if (UString.estaVacio(filtro.estado))
            {
                filtro.estado = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.estado));


            contador = this.contar("pscomponente.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "pscomponente.filtroPaginacion");

            while (_reader.Read())
            {
                PsComponente bean = new PsComponente();

                if (!_reader.IsDBNull(0))
                    bean.IdComponente = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Estado = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.CreacionFecha = _reader.GetDateTime(3);



                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }


        public string obtenercadena(string cadena)
        {
            IQueryable<PsComponente> query = this.getCriteria();
            query = query.Where(p => p.Nombre == cadena);

            List<PsComponente> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string codigo)
        {
            IQueryable<PsComponente> query = this.getCriteria();
            query = query.Where(p => p.IdComponente == codigo);

            List<PsComponente> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdComponente;
            return null;
        }
    }
}
