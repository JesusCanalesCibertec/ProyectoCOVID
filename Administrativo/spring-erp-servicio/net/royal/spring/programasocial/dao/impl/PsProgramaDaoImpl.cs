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
public class PsProgramaDaoImpl : GenericoDaoImpl<PsPrograma>, PsProgramaDao
{
        private IServiceProvider servicioProveedor;


        public PsProgramaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"psprograma")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsPrograma obtenerPorId(PsProgramaPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsPrograma obtenerPorId(String pIdPrograma)
        {
            return obtenerPorId(new PsProgramaPk( pIdPrograma));
        }

        public PsPrograma coreInsertar(UsuarioActual usuarioActual, PsPrograma bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsPrograma coreActualizar(UsuarioActual usuarioActual, PsPrograma bean, String estado)
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

        public void coreEliminar(String pIdPrograma)
        {
            coreEliminar(new PsProgramaPk( pIdPrograma));
        }

        public void coreEliminar(PsProgramaPk id)
        {
            PsPrograma bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsPrograma coreAnular(UsuarioActual usuarioActual,PsProgramaPk id)
        {
            PsPrograma bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public PsPrograma coreAnular(UsuarioActual usuarioActual,String pIdPrograma)
        {
            return coreAnular(usuarioActual,new PsProgramaPk( pIdPrograma));
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPrograma filtro)
        {
            Int32 contador = 0;

            List<PsPrograma> lstRetorno = new List<PsPrograma>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();



            if (UString.estaVacio(filtro.estado))
            {
                filtro.estado = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.estado));


            contador = this.contar("psprograma.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "psprograma.filtroPaginacion");

            while (_reader.Read())
            {
                PsPrograma bean = new PsPrograma();

                if (!_reader.IsDBNull(0))
                    bean.IdPrograma = _reader.GetString(0);

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
            IQueryable<PsPrograma> query = this.getCriteria();
            query = query.Where(p => p.Nombre == cadena);

            List<PsPrograma> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string codigo)
        {
            IQueryable<PsPrograma> query = this.getCriteria();
            query = query.Where(p => p.IdPrograma == codigo);

            List<PsPrograma> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdPrograma;
            return null;
        }

        public List<PsPrograma> listarTodosActivos() {
            IQueryable<PsPrograma> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");

            return query.ToList();
        }
    }
}
