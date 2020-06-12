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
public class PsInstitucionAreaTrabajoDaoImpl : GenericoDaoImpl<PsInstitucionAreaTrabajo>, PsInstitucionAreaTrabajoDao 
{
        private IServiceProvider servicioProveedor;


        public PsInstitucionAreaTrabajoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"psinstitucionareatrabajo")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsInstitucionAreaTrabajo obtenerPorId(PsInstitucionAreaTrabajoPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsInstitucionAreaTrabajo obtenerPorId()
        {
            return obtenerPorId(new PsInstitucionAreaTrabajoPk());
        }

        public PsInstitucionAreaTrabajo coreInsertar(UsuarioActual usuarioActual, PsInstitucionAreaTrabajo bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsInstitucionAreaTrabajo coreActualizar(UsuarioActual usuarioActual, PsInstitucionAreaTrabajo bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar()
        {
            coreEliminar(new PsInstitucionAreaTrabajoPk());
        }

        public void coreEliminar(PsInstitucionAreaTrabajoPk id)
        {
            PsInstitucionAreaTrabajo bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsInstitucionAreaTrabajo coreAnular(UsuarioActual usuarioActual,PsInstitucionAreaTrabajoPk id)
        {
            PsInstitucionAreaTrabajo bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public List<PsInstitucionAreaTrabajo> listarActivos() {
            IQueryable<PsInstitucionAreaTrabajo> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");

            return query.ToList();
        }
    }
}
