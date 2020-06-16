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
using System.Collections.Generic;

namespace net.royal.spring.kpi.dao.impl
{
public class RtIndicadorMetaDaoImpl : GenericoDaoImpl<RtIndicadorMeta>, RtIndicadorMetaDao
{
        private IServiceProvider servicioProveedor;


        public RtIndicadorMetaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"rtindicadormeta")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public RtIndicadorMeta obtenerPorId(RtIndicadorMetaPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public RtIndicadorMeta obtenerPorId(String pIdIndicador,Nullable<Int32> pIdMeta)
        {
            return obtenerPorId(new RtIndicadorMetaPk( pIdIndicador, pIdMeta));
        }

        public int generarCodigo()
        {
            IQueryable<RtIndicadorMeta> query = this.getCriteria();

            List<RtIndicadorMeta> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.IdMeta));
            }

            return var + 1;
        }

        public RtIndicadorMeta coreInsertar(UsuarioActual usuarioActual, RtIndicadorMeta bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public RtIndicadorMeta coreActualizar(UsuarioActual usuarioActual, RtIndicadorMeta bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdIndicador,Nullable<Int32> pIdMeta)
        {
            coreEliminar(new RtIndicadorMetaPk( pIdIndicador, pIdMeta));
        }

        public void coreEliminar(RtIndicadorMetaPk id)
        {
            RtIndicadorMeta bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public RtIndicadorMeta coreAnular(UsuarioActual usuarioActual,RtIndicadorMetaPk id)
        {
            RtIndicadorMeta bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
   
            return coreActualizar(usuarioActual,bean);
        }

        public RtIndicadorMeta coreAnular(UsuarioActual usuarioActual,String pIdIndicador,Nullable<Int32> pIdMeta)
        {
            return coreAnular(usuarioActual,new RtIndicadorMetaPk( pIdIndicador, pIdMeta));
        }

        public List<RtIndicadorMeta> listado(string pIdIndicador)
        {
            IQueryable<RtIndicadorMeta> query = this.getCriteria();
            query = query.Where(p => p.IdIndicador == pIdIndicador);
            query = query.OrderByDescending(p => p.CreacionFecha);

            List<RtIndicadorMeta> lst = query.ToList();
            return lst;
        }
    }
}
