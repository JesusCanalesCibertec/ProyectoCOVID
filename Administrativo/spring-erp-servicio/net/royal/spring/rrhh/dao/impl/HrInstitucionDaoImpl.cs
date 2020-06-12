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
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.rrhh.dao.impl
{
    public class HrInstitucionDaoImpl : GenericoDaoImpl<PsInstitucion>, HrInstitucionDao
    {
        private IServiceProvider servicioProveedor;


        public HrInstitucionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrInstitucion")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PsInstitucion obtenerPorId(PsInstitucionPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsInstitucion obtenerPorId(string codigo)
        {
            return obtenerPorId(new PsInstitucionPk(codigo));
        }


        public string obtenerDescripcion(string nombre)
        {
            IQueryable<PsInstitucion> query = this.getCriteria();
            query = query.Where(p => p.Nombre == nombre);

            List<PsInstitucion> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public PsInstitucion coreInsertar(UsuarioActual usuarioActual, PsInstitucion bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
           
            this.registrar(bean);
            return bean;
        }

        public PsInstitucion coreActualizar(UsuarioActual usuarioActual, PsInstitucion bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(string codigo)
        {
            coreEliminar(new PsInstitucionPk(codigo));
        }

        public void coreEliminar(PsInstitucionPk id)
        {
            PsInstitucion bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsInstitucion coreAnular(UsuarioActual usuarioActual, PsInstitucionPk id)
        {
            PsInstitucion bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PsInstitucion coreAnular(UsuarioActual usuarioActual, string codigo)
        {
            return coreAnular(usuarioActual, new PsInstitucionPk(codigo));
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPaginacionCurso filtro)
        {
            return null;
        }

        public PsInstitucion registrarCurso(UsuarioActual usuarioActual, PsInstitucion bean)
        {
            return null;
        }

        public void coreEliminar(PsInstitucion id)
        {
            throw new NotImplementedException();
        }
    }
}
