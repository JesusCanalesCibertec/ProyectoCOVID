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
public class BancoDaoImpl : GenericoDaoImpl<Banco>, BancoDao 
{
        private IServiceProvider servicioProveedor;


	public BancoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "banco") {
            servicioProveedor = _servicioProveedor;
	}

        public List<Banco> listar(DtoFiltro filtro)
        {
            IQueryable<Banco> query = this.getCriteria();
                       
            if (!UString.estaVacio(filtro.Codigo))
                query = query.Where(p => p.Banco == filtro.Codigo);

            if (!UString.estaVacio(filtro.Nombre))
                query = query.Where(p => p.Descripcioncorta.Contains(filtro.Nombre));

            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Estado == filtro.Estado);

            if (!UString.estaVacio(filtro.AtributoOrdenar))
                query = query.OrderBy(p => p.Descripcioncorta);     /** order **/
            else
                query = query.OrderBy(p => filtro.AtributoOrdenar);

            return query.ToList();
        }

        public Banco obtenerPorId(BancoPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
