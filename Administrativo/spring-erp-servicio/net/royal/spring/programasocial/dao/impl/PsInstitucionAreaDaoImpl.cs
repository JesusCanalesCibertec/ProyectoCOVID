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
    public class PsInstitucionAreaDaoImpl : GenericoDaoImpl<PsInstitucionArea>, PsInstitucionAreaDao
    {
        private IServiceProvider servicioProveedor;


        public PsInstitucionAreaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "PsInstitucionArea")
        {
            servicioProveedor = _servicioProveedor;
        }



        public PsInstitucionArea coreInsertar(UsuarioActual usuarioActual, PsInstitucionArea bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsInstitucionArea coreActualizar(UsuarioActual usuarioActual, PsInstitucionArea bean, String estado)
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



        public string obtenercadena(string pIdInstitucion, string pIdNombre)
        {
            IQueryable<PsInstitucionArea> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == pIdInstitucion);
            query = query.Where(p => p.Nombre == pIdNombre);

            List<PsInstitucionArea> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string pIdInstitucion, string pIdArea)
        {
            IQueryable<PsInstitucionArea> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == pIdInstitucion);
            query = query.Where(p => p.IdArea == pIdArea);

            List<PsInstitucionArea> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdArea;
            return null;
        }

        public List<PsInstitucionArea> listado(string pIdInstitucion)
        {
            IQueryable<PsInstitucionArea> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == pIdInstitucion);
            query = query.OrderByDescending(p => p.CreacionFecha);

            List<PsInstitucionArea> lst = query.ToList();
            return lst;

        }

        public List<PsInstitucionArea> listadoPorPrograma(string pIdInstitucion, string idPrograma)
        {
            IQueryable<PsInstitucionArea> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == pIdInstitucion);
            query = query.Where(p => p.IdPrograma == idPrograma);
            query = query.Where(p => p.Estado == "A");
            query = query.OrderByDescending(p => p.CreacionFecha);

            List<PsInstitucionArea> lst = query.ToList();
            return lst;
        }

        public List<PsInstitucionArea> listarTodo()
        {
            IQueryable<PsInstitucionArea> query = this.getCriteria();
            List<PsInstitucionArea> lst = query.ToList();
            return lst;
        }

        public PsInstitucionArea coreAnular(UsuarioActual usuarioActual, PsInstitucionAreaPk id)
        {
            PsInstitucionArea bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;

            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PsInstitucionArea coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, String pIdArea)
        {
            return coreAnular(usuarioActual, new PsInstitucionAreaPk(pIdInstitucion, pIdArea));
        }
    }
}
