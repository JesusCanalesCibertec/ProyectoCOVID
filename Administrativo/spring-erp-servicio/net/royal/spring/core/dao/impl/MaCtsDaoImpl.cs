using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.core;

namespace net.royal.spring.core.dao.impl
{
public class MaCtsDaoImpl : GenericoDaoImpl<MaCts>, MaCtsDao 
{
        private IServiceProvider servicioProveedor;


	public MaCtsDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "macts") {
            servicioProveedor = _servicioProveedor;
	}

        public List<MaCts> listar(DtoFiltroEntero filtro)
        {
            IQueryable<MaCts> query = this.getCriteria();
                        
            if (!UInteger.esCeroOrNulo(filtro.Codigo))
                query = query.Where(p => p.Numerocts == filtro.Codigo);
            if (!UString.estaVacio(filtro.Nombre))
                query = query.Where(p => p.Descripcion.Contains(filtro.Nombre));
            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Estado == filtro.Estado);

            if (!UString.estaVacio(filtro.AtributoOrdenar))
                query = query.OrderBy(p => p.Descripcion);     /** order **/
            else
                query = query.OrderBy(p => filtro.AtributoOrdenar);

            return query.ToList();
        }

        public MaCts obtenerPorId(MaCtsPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
