using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.core;

namespace net.royal.spring.sistema.dao.impl
{
public class SyUnidadreplicacionDaoImpl : GenericoDaoImpl<SyUnidadreplicacion>, SyUnidadreplicacionDao 
{
        private IServiceProvider servicioProveedor;


	    public SyUnidadreplicacionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "syunidadreplicacion") {
                servicioProveedor = _servicioProveedor;
	    }

        public List<SyUnidadreplicacion> listar(DtoFiltro filtro)
        {
            IQueryable<SyUnidadreplicacion> query = this.getCriteria();

            if (!UString.estaVacio(filtro.Codigo))
                query = query.Where(p => p.Unidadreplicacion == filtro.Codigo);
            if (!UString.estaVacio(filtro.Nombre))
                query = query.Where(p => p.Descripcionlocal.Contains(filtro.Nombre));
            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Estado == filtro.Estado);

            if (!UString.estaVacio(filtro.AtributoOrdenar))
                query = query.OrderBy(p => p.Descripcionlocal);     /** order **/
            else
                query = query.OrderBy(p => filtro.AtributoOrdenar);

            return query.ToList();
        }
    }
}
